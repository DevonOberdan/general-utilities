using UnityEngine;

namespace FinishOne.GeneralUtilities
{
    [CreateAssetMenu(fileName = nameof(BoolGameEvent), menuName = "GameEvents/"+nameof(BoolGameEvent))]
    public class BoolGameEvent : BaseGameEvent<bool> {}
}