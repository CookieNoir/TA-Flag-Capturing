using System;
using System.Collections.Generic;
using UnityEngine;

namespace FlagCapturing.Entities.Effects
{
    public class EffectHandler : MonoBehaviour
    {
        public event Action OnEffectsChanged;
        public event Action<BaseEffect, float, float> OnEffectAdded;
        public event Action<BaseEffect> OnEffectRemoved;
        public Dictionary<BaseEffect, float> _currentEffects = new();
        Queue<BaseEffect> _effectsToRemove = new();

        public void AddEffect(BaseEffect effect)
        {
            float expirationTime = Time.time + effect.Duration;
            _currentEffects[effect] = expirationTime;
            _ChangeEnabled();
            OnEffectAdded?.Invoke(effect, Time.time, expirationTime);
            OnEffectsChanged?.Invoke();
        }

        public bool HasEffect(BaseEffect effect)
        {
            return _currentEffects.ContainsKey(effect);
        }

        public void RemoveEffect(BaseEffect effect)
        {
            _RemoveSingleEffect(effect);
            OnEffectsChanged?.Invoke();
        }

        private void _RemoveSingleEffect(BaseEffect effect)
        {
            _currentEffects.Remove(effect);
            _ChangeEnabled();
            OnEffectRemoved?.Invoke(effect);
        }

        private void _RemoveEffectsFromQueue()
        {
            if (_effectsToRemove.Count == 0) return;
            while (_effectsToRemove.Count > 0)
            {
                _RemoveSingleEffect(_effectsToRemove.Dequeue());
            }
            _ChangeEnabled();
            OnEffectsChanged?.Invoke();
        }

        private void _ChangeEnabled()
        {
            enabled = _currentEffects.Count > 0;
        }

        private void _CheckEffects()
        {
            foreach (var keyValuePair in _currentEffects)
            {
                if (Time.time > keyValuePair.Value)
                {
                    _effectsToRemove.Enqueue(keyValuePair.Key);
                }
            }
            _RemoveEffectsFromQueue();
        }

        private void Update()
        {
            _CheckEffects();
        }
    }
}
