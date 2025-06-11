using UnityEngine;
using UnityEngine.Events;
using static FinishOne.GeneralUtilities.EasingFunction;

namespace FinishOne.GeneralUtilities
{
    public class InteractionBuffer : MonoBehaviour
    {
        [SerializeField] private float totalSeconds = 1.25f;
        [SerializeField] private float decaySeconds = .5f;

        public UnityEvent<float> OnPercentageChanged;
        public UnityEvent OnComplete;
        public UnityEvent OnReset;

        public bool CooldownAndReset = true;

        [DrawIf(nameof(CooldownAndReset), true)][SerializeField] private float cooldownSeconds = 1f;

        [SerializeField] private bool curvedBuffer;

        [DrawIf(nameof(curvedBuffer), true)][SerializeField] private bool forceLinearDecay;
        [DrawIf(nameof(curvedBuffer), true)][SerializeField] private Ease bufferEase = Ease.EaseOutQuad;

        private Function EaseFunction;

        private bool coolingDown;
        private bool interacting;

        private float currentTime;
        private float decayFactor;

        private float currentCooldownTime;

        #region Properties
        private bool UseCurve => curvedBuffer && (Interacting || (!Interacting && !forceLinearDecay));
        public float Percentage => currentTime / totalSeconds;

        public float CurrentTime 
        {
            get => currentTime;
            set 
            {
                float previousTime = currentTime;
                currentTime = Mathf.Clamp(value, 0, totalSeconds);

                if (UseCurve)
                    OnPercentageChanged?.Invoke(EaseFunction(0, 1, Percentage));
                else
                    OnPercentageChanged?.Invoke(Percentage);

                if (currentTime >= totalSeconds && previousTime < totalSeconds)
                    OnComplete?.Invoke();
            }
        }

        public bool Interacting 
        {
            get => interacting;
            set 
            {
                if (value == interacting)
                    return;

                interacting = value;

                if (curvedBuffer && forceLinearDecay && interacting == false)
                    currentTime = EaseFunction(0, 1, Percentage) * totalSeconds;
            }
        }
        #endregion

        private void Start()
        {
            EaseFunction = GetEasingFunction(bufferEase);
            decayFactor = totalSeconds / decaySeconds;

            if (CooldownAndReset)
            {
                OnComplete.AddListener(StartCooldown);
            }
        }

        private void Update()
        {
            if (coolingDown)
            {
                ProcessCooldown();
                return;
            }

            float factor = Interacting ? 1 : -1 * decayFactor;
            CurrentTime += Time.deltaTime * factor;
        }

        public void Complete()
        {
            CurrentTime = totalSeconds;
        }

        private void ProcessCooldown()
        {
            currentCooldownTime += Time.deltaTime;

            if(currentCooldownTime >= cooldownSeconds)
            {
                currentTime = 0;
                currentCooldownTime = 0;
                coolingDown = false;
                OnReset.Invoke();
            }
        }

        private void StartCooldown()
        {
            coolingDown = true;
            Interacting = false;
            currentCooldownTime = 0;
        }

        private void OnValidate()
        {
            if (!curvedBuffer)
                forceLinearDecay = false;

            decayFactor = totalSeconds / decaySeconds;
        }
    }
}