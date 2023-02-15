using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Zenject;

namespace FlagCapturing.Controllers
{
    public class Joystick : EventTrigger, IPlayerController
    {
        [SerializeField] RectTransform _background;
        [SerializeField] RectTransform _foreground;
        [SerializeField, Min(0.1f)] float _maxDistanceFromCenter;
        [SerializeField] Settings _settings;
        public UnityEvent OnDragStarted;
        public UnityEvent OnDragEnded;
        public event Action<Vector2> OnAxisValuesChanged;
        Vector3 _startPosition;

        [Inject]
        public void Construct(Settings settings)
        {
            _settings = settings;
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            _startPosition = eventData.position;
            _background.position = _startPosition;
            OnDragStarted.Invoke();
        }

        public override void OnDrag(PointerEventData eventData)
        {
            Vector3 currentPosition = eventData.position;
            Vector3 direction = currentPosition - _startPosition;
            Vector2 outputAxis = _GetOutputAxis(direction);
            OnAxisValuesChanged?.Invoke(outputAxis);
            _foreground.anchoredPosition = outputAxis * _maxDistanceFromCenter;
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            OnAxisValuesChanged?.Invoke(Vector2.zero);
            _foreground.anchoredPosition = Vector2.zero;
            OnDragEnded.Invoke();
        }

        private Vector2 _GetOutputAxis(in Vector3 direction)
        {
            float strength = direction.magnitude / _maxDistanceFromCenter;
            if (strength < _settings.minOutputStrength) return Vector2.zero;
            float clampedStrength = Mathf.Min(strength, 1f);
            Vector2 normalizedDirection = direction.normalized;
            return clampedStrength * normalizedDirection;
        }

        [Serializable]
        public class Settings
        {
            [Range(0f, 1f)] public float minOutputStrength;
        }
    }
}
