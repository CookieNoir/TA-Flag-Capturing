using System;
using FlagCapturing.Entities.Movement;
using FlagCapturing.Entities.Effects;
using UnityEngine;
using Zenject;

namespace FlagCapturing.Entities
{
    public class Player : MonoBehaviour
    {
        [field: SerializeField] public BaseMovement Movement { get; private set; }
        [field: SerializeField] public EffectHandler Effects { get; private set; }
        [SerializeField] Settings _settings;

        [Inject]
        public void Construct(Settings settings)
        {
            _settings = settings;
            _ApplySettings();
        }

        private void _ApplySettings()
        {
            Movement?.SetSpeed(_settings.movementSpeed);
        }

        private void OnValidate()
        {
            _ApplySettings();
        }

        [Serializable]
        public class Settings
        {
            [Min(0.01f)] public float movementSpeed;
        }
    }
}
