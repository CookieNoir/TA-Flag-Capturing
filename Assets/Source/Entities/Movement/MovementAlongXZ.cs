using UnityEngine;

namespace FlagCapturing.Entities.Movement
{
    public class MovementAlongXZ : BaseMovement
    {
        protected override Vector3 ConvertAxisValues(Vector2 axisValues)
        {
            return new Vector3(axisValues.x, 0f, axisValues.y);
        }

        protected override void Move(Transform target, in Vector3 movementVector)
        {
            target.Translate(movementVector);
        }
    }
}
