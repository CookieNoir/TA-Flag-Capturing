using System;
using UnityEngine;

namespace FlagCapturing.Entities.Effects
{
    [Serializable]
    public class BaseEffect
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField, Min(0.01f)] public float Duration { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }

        public BaseEffect(string name, float duration, Sprite icon)
        {
            Name = name;
            Duration = duration;
            Icon = icon;
        }
    }
}
