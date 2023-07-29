using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using static FinishOne.GeneralUtilities.EasingFunction;

namespace FinishOne.GeneralUtilities
{
    public class InteractionBuffer : MonoBehaviour
    {
        [SerializeField] private float totalSeconds = 1.25f;
        [SerializeField] private float decaySeconds = .5f;
        [SerializeField] private float cooldownSeconds = 1f;

        public UnityEvent<float> OnPercentageChanged;
        public UnityEvent OnComplete;

        [SerializeField] private bool curvedBuffer;

        [DrawIf(nameof(curvedBuffer), true)][SerializeField] private bool forceLinearDecay;
        [DrawIf(nameof(curvedBuffer), true)][SerializeField] private Ease bufferEase = Ease.EaseOutQuad;

        private Function EaseFunction;

        private float currentTime;
        private float decayFactor;
        private bool coolingDown;

        private bool interacting;

        const float MAX_TIME = 5f;

        #region Properties
        private bool UseCurve => curvedBuffer && (Interacting || (!Interacting && !forceLinearDecay));
        private float Percentage => currentTime / totalSeconds;

        public float CurrentTime {
            get => currentTime;
            private set {
                currentTime = Mathf.Clamp(value, 0, totalSeconds);

                if (UseCurve)
                    OnPercentageChanged?.Invoke(EaseFunction(0, 1, Percentage));
                else
                    OnPercentageChanged?.Invoke(Percentage);

                if (currentTime >= totalSeconds)
                    OnComplete?.Invoke();
            }
        }

        public bool Interacting {
            get => interacting;
            set {

                if (value == interacting)
                    return;

                interacting = value;

                if (curvedBuffer && forceLinearDecay && interacting == false)
                    currentTime = EaseFunction(0, 1, Percentage) * totalSeconds;
            }
        }
        #endregion

        void Start()
        {
            EaseFunction = GetEasingFunction(bufferEase);
            decayFactor = totalSeconds / decaySeconds;
            OnComplete.AddListener(() => StartCoroutine(Cooldown()));
        }

        void Update()
        {
            if (coolingDown)
                return;

            float factor = Interacting ? 1 : -1 * decayFactor;
            CurrentTime += Time.deltaTime * factor;
        }

        IEnumerator Cooldown()
        {
            coolingDown = true;
            Interacting = false;

            yield return new WaitForSeconds(cooldownSeconds);

            currentTime = 0;
            coolingDown = false;
        }

        private void OnValidate()
        {
            if (!curvedBuffer)
                forceLinearDecay = false;

            totalSeconds = Mathf.Clamp(totalSeconds, 0, MAX_TIME);
            decaySeconds = Mathf.Clamp(decaySeconds, 0, MAX_TIME);
            cooldownSeconds = Mathf.Clamp(cooldownSeconds, 0, MAX_TIME);

            decayFactor = totalSeconds / decaySeconds;
        }
    }
}