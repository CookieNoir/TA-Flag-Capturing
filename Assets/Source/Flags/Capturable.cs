using System;

namespace FlagCapturing.Flags
{
    public class Capturable
    {
        public bool IsCapturable { get; private set; } = false;
        public event Action<bool> OnCapturableChanged;
        public event Action OnCaptured;
        public event Action<float> OnCapturing;
        float _timeForCapturing;
        float _timeSpent = 0f;
        public bool IsCaptured { get; private set; } = false;

        public Capturable(float timeForCapturing)
        {
            _timeForCapturing = timeForCapturing;
            _CheckForCapturing();
        }

        public void AllowCapturing()
        {
            if (IsCaptured) return;
            IsCapturable = true;
            OnCapturableChanged?.Invoke(IsCapturable);
        }

        public void ForbidCapturing()
        {
            if (IsCaptured) return;
            IsCapturable = false;
            OnCapturableChanged?.Invoke(IsCapturable);
        }

        private void _CheckForCapturing()
        {
            if (_timeSpent < _timeForCapturing)
            {
                OnCapturing?.Invoke(_timeSpent / _timeForCapturing);
            }
            else
            {
                ForbidCapturing();
                IsCaptured = true;
                OnCaptured?.Invoke();
            }
        }

        public void CaptureForTime(float timestep)
        {
            if (IsCaptured || !IsCapturable) return;
            _timeSpent += timestep;
            _CheckForCapturing();
        }

        public void CaptureSimultaneously()
        {
            if (IsCaptured || !IsCapturable) return;
            _timeSpent = _timeForCapturing;
            _CheckForCapturing();
        }
    }
}
