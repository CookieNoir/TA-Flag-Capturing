using System;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace FlagCapturing.Core
{
    public class Game: MonoBehaviour
    {
        [SerializeField] Settings _settings;
        public UnityEvent<int> OnValueChanged;
        public UnityEvent OnWin;
        int _capturedCount = 0;

        [Inject]
        public Game(Settings settings)
        {
            _settings = settings;
            OnValueChanged.Invoke(_capturedCount);
            _CheckForWin();
        }

        public void FlagCaptured()
        {
            _capturedCount++;
            OnValueChanged.Invoke(_capturedCount);
            _CheckForWin();
        }

        private void _CheckForWin()
        {
            if (_capturedCount < _settings.flagsForWin) return;
            OnWin.Invoke();
        }

        [Serializable]
        public class Settings
        {
            [Min(0)] public int flagsForWin;
        }
    }
}
