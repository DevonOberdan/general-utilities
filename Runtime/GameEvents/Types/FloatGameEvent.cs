using UnityEngine;

namespace FinishOne.GeneralUtilities
{
    [CreateAssetMenu(fileName = nameof(FloatGameEvent), menuName = "GameEvents/"+nameof(FloatGameEvent))]
    public class FloatGameEvent : BaseGameEvent<float> {}
}