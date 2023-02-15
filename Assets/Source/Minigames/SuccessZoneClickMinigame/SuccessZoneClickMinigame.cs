using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using FlagCapturing.Utils;
using Random = UnityEngine.Random;
using Zenject;

namespace FlagCapturing.Minigames
{
    public class SuccessZoneClickMinigame : MonoBehaviour, IMinigame
    {
        [SerializeField] MovingPart _movingPart;
        [SerializeField] Settings _settings;
        public event Action OnSuccess;
        public event Action OnFailure;
        public UnityEvent<Vector2> OnSuccessZoneBordersSet;
        public UnityEvent OnMinigameStarted;
        public UnityEvent OnMinigameStopped;
        bool _started = false;
        Vector2 _successZoneBorders;
        IEnumerator _startedTimer = null;

        [Inject]
        public void Construct(Settings settings)
        {
            _settings = settings;
            _movingPart.SetMovingSpeed(_settings.movingPartSpeed);
        }

        private void _StartTimer()
        {
            _StopTimer();
            _startedTimer = IEnumeratorTimer.StartTimer(_settings.maxDuration, Lose);
            StartCoroutine(_startedTimer);
        }

        private void _StopTimer()
        {
            if (_startedTimer != null)
            {
                StopCoroutine(_startedTimer);
                _startedTimer = null;
            } 
        }

        public void Launch()
        {
            if (_started) return;
            OnMinigameStarted.Invoke();
            _SetComponents();
            _started = true;
        }

        private void _SetComponents()
        {
            _SetSuccessZone();
            _movingPart.Launch();
            _StartTimer();
        }

        private void _SetSuccessZone()
        {
            float center = Random.Range(
                _settings.successZoneSettings.startPointRange.x,
                _settings.successZoneSettings.startPointRange.y);
            float size = Random.Range(
                _settings.successZoneSettings.sizeRange.x,
                _settings.successZoneSettings.sizeRange.y);
            float leftBorder = Mathf.Clamp01(center - size);
            float rightBorder = Mathf.Clamp01(center + size);
            _successZoneBorders = new Vector2(leftBorder, rightBorder);
            OnSuccessZoneBordersSet.Invoke(_successZoneBorders);
        }

        public void TryToSolve()
        {
            if (!_started) return;
            _StopMinigame(Helpers.InRange(
                _movingPart.CurrentPosition, 
                _successZoneBorders.x, 
                _successZoneBorders.y));
        }

        public void Win()
        {
            if (!_started) return;
            _StopMinigame(true);
        }

        public void Lose()
        {
            if (!_started) return;
            _StopMinigame(false);
        }

        private void _StopMinigame(bool result)
        {
            _started = false;
            _InvokeOnResult(result);
            _StopComponents();
            OnMinigameStopped.Invoke();
        }

        private void _StopComponents()
        {
            _StopTimer();
            _movingPart.Stop();
        }

        private void _InvokeOnResult(bool result)
        {
            if (result) OnSuccess?.Invoke();
            else OnFailure?.Invoke();
        }

        [Serializable]
        public class SuccessZoneSettings
        {
            public Vector2 startPointRange;
            public Vector2 sizeRange;
        }

        [Serializable]
        public class Settings
        {
            public SuccessZoneSettings successZoneSettings;
            [Min(0.01f)] public float movingPartSpeed;
            [Min(0.01f)] public float maxDuration;
        }
    }
}
