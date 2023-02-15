using System;
using UnityEngine;
using UnityEngine.Events;

namespace FlagCapturing.Minigames
{
    internal class MovingPart : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] float _startPosition;
        [SerializeField, Min(0.01f)] float _movingSpeed;
        [SerializeField] bool _movingRightAsStart;
        public UnityEvent<float> OnPositionChanged;
        public float CurrentPosition { get; private set; }
        float _directedSpeed;

        public void SetMovingSpeed(float value)
        {
            if (value < 0.01f) return;
            _movingSpeed = value;
        }

        public void Launch()
        {
            CurrentPosition = _startPosition;
            _directedSpeed = _movingRightAsStart ?
                _movingSpeed :
                -_movingSpeed;
            enabled = true;
        }

        public void Stop()
        {
            enabled = false;
        }

        private void _ChangeDirection()
        {
            _directedSpeed *= -1f;
        }

        private void _Move(in float timestep)
        {
            float newPosition = CurrentPosition + timestep * _directedSpeed;
            CurrentPosition = Mathf.Clamp01(newPosition);
            OnPositionChanged.Invoke(CurrentPosition);
            if (CurrentPosition == 0 || CurrentPosition == 1)
            {
                _ChangeDirection();
            }
        }

        private void Update()
        {
            _Move(Time.deltaTime);
        }
    }
}
