using UnityEngine;

namespace FinishOne.GeneralUtilities
{
    [CreateAssetMenu(fileName = nameof(StringGameEvent), menuName = "GameEvents/"+nameof(StringGameEvent))]
    public class StringGameEvent : BaseGameEvent<string> {}
}