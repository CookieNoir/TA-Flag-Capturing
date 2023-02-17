using System;
using FlagCapturing.Triggers;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace FlagCapturing.Flags
{
    public class Flag : MonoBehaviour
    {
        [field: SerializeField] public BaseTrigger Trigger { get; private set; }
        public Capturable Capturable { get; private set; }
        [SerializeField] Settings _settings;
        public UnityEvent<float> OnCapturing;

        [Inject]
        public void Construct(Settings settings)
        {
            _settings = settings;
            Capturable = new(_settings.timeForCapturing);
            Capturable.OnCapturing += (x => OnCapturing.Invoke(x));
            Trigger.OnTriggerEnter += Capturable.AllowCapturing;
            Trigger.OnTriggerExit += Capturable.ForbidCapturing;
            _ApplySettings();
        }

        private void _ApplySettings()
        {
            Trigger.Resizeable.SetSize(_settings.size);
        }

        private void OnValidate()
        {
            _ApplySettings();
        }

        [Serializable]
        public class Settings
        {
            [Min(0.01f)] public float size;
            [Min(0f)] public float timeForCapturing;
        }

        public class Factory : PlaceholderFactory<UnityEngine.Object, Flag> { }
    }
}
