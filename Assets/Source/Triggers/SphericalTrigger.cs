using UnityEngine;

namespace FlagCapturing.Triggers
{
    public class SphericalTrigger : BaseTrigger
    {
        internal override bool IsInside(Transform outerTransform)
        {
            float sqrMagnitude = (outerTransform.position - transform.position).sqrMagnitude;
            return sqrMagnitude <= (Resizeable.Size * Resizeable.Size);
        }
    }
}
