using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace FlagCapturing.Flags
{
    public class FlagHandler: MonoBehaviour
    {
        public event Action<bool> OnEnabledChecked;
        HashSet<Flag> _flags = new();
        [SerializeField] bool _capturable = false;
        FlagSpawner _flagSpawner;

        [Inject]
        public void Construct(FlagSpawner flagSpawner)
        {
            _flagSpawner = flagSpawner;
            _flagSpawner.OnFlagSpawned += AddFlag;
            _ChangeEnabled();
        }

        public void AllowCapturing()
        {
            _capturable = true;
        }

        public void ForbidCapturing()
        {
            _capturable = false;
        }

        public void AddFlag(Flag flag)
        {
            _flags.Add(flag);
            flag.Capturable.OnCapturableChanged += (x => _ChangeEnabled());
        }

        private void _ChangeEnabled()
        {
            bool result = false;
            foreach (var flag in _flags)
            {
                if (flag.Capturable.IsCapturable)
                {
                    result = true;
                    break;
                }
            }
            enabled = result;
            OnEnabledChecked?.Invoke(enabled);
        }

        private void _TryToCapture(float timestep)
        {
            foreach (var flag in _flags)
            {
                flag.Capturable.CaptureForTime(timestep);
            }
        }

        private void Update()
        {
            if (_capturable) _TryToCapture(Time.deltaTime);
        }

        private void OnDestroy()
        {
            if (_flagSpawner) _flagSpawner.OnFlagSpawned -= AddFlag;
        }
    }
}
