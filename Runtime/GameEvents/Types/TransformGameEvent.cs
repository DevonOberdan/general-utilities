using UnityEngine;

namespace FinishOne.GeneralUtilities
{
    [CreateAssetMenu(fileName = nameof(TransformGameEvent), menuName = "GameEvents/"+nameof(TransformGameEvent))]
    public class TransformGameEvent : BaseGameEvent<Transform> {}
}