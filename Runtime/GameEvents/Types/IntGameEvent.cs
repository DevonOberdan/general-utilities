using UnityEngine;

namespace FinishOne.GeneralUtilities
{
    [CreateAssetMenu(fileName = nameof(IntGameEvent), menuName = "GameEvents/"+nameof(IntGameEvent))]
    public class IntGameEvent : BaseGameEvent<int> {}
}