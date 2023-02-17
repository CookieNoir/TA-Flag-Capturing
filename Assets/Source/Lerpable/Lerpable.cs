using System;
using UnityEngine;

namespace FlagCapturing.Lerpable
{
    [Serializable]
    public abstract class Lerpable<T>
    {
        [field: SerializeField] public T MinValue { get; private set; }
        [field: SerializeField] public T MaxValue { get; private set; }
        public event Action<T> OnValueSent;

        public Lerpable(T minValue, T maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public T Lerp(in float factor)
        {
            if (factor <= 0f) return MinValue;
            if (factor >= 1f) return MaxValue;
            return Lerp01(factor);
        }

        public void LerpAndSend(in float factor)
        {
            T result = Lerp(factor);
            OnValueSent?.Invoke(result);
        }

        protected abstract T Lerp01(in float factor);
    }
}
