using System;
using UnityEngine;

namespace FlagCapturing.Entities.Movement
{
    public abstract class BaseMovement : MonoBehaviour
    {
        [SerializeField] Transform _target;
        [SerializeField] float _speed;
        private Vector2 _axisValues = Vector2.zero;
        private Vector3 _movementVector = Vector3.zero;

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        public void SetSpeed(float value)
        {
            _speed = value;
            _RecalculateMovementVector();
        }

        private void _RecalculateMovementVector()
        {
            Vector2 scaledAxisValues = _speed * _axisValues;
            _movementVector = ConvertAxisValues(scaledAxisValues);
        }

        public void SetMovementDirection(Vector2 axisValues)
        {
            _axisValues = axisValues;
            _RecalculateMovementVector();
        }

        protected abstract Vector3 ConvertAxisValues(Vector2 axisValues);

        protected abstract void Move(Transform target, in Vector3 movementVector);

        private void Update()
        {
            Move(_target, Time.deltaTime * _movementVector);
        }
    }
}
