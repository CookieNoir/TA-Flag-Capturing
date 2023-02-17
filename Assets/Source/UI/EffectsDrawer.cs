using System.Collections;
using System.Collections.Generic;
using FlagCapturing.Entities.Effects;
using UnityEngine;
using Zenject;

namespace FlagCapturing.UI
{
    public class EffectsDrawer : MonoBehaviour
    {
        internal class EffectData
        {
            public FillableImage fillableImage;
            float _additionTime;
            float _expirationTime;

            public EffectData(FillableImage fillableImage, float additionTime, float expirationTime)
            {
                this.fillableImage = fillableImage;
                _additionTime = additionTime;
                _expirationTime = expirationTime;
            }

            public float GetRemainder(float currentTime)
            {
                return 1f - ((currentTime - _additionTime) / (_expirationTime - _additionTime));
            }
        }

        [SerializeField] Transform _parentTransform;
        [SerializeField] GameObject _fillablePrefab;
        EffectHandler _effectHandler;
        Dictionary<BaseEffect, EffectData> _effects = new();

        [Inject]
        public void Construct(EffectHandler effectHandler)
        {
            _effectHandler = effectHandler;

            _effectHandler.OnEffectAdded += AddEffect;
            _effectHandler.OnEffectRemoved += RemoveEffect;
        }

        public void AddEffect(BaseEffect effect, float additionTime, float expirationTime)
        {
            GameObject newEffect = Instantiate(_fillablePrefab, _parentTransform);
            FillableImage fillableImage = newEffect.GetComponent<FillableImage>();
            fillableImage.SetSprite(effect.Icon);
            EffectData effectData = new EffectData(fillableImage, additionTime, expirationTime);
            _effects[effect] = effectData;
        }

        public void RemoveEffect(BaseEffect effect)
        {
            if (_effects.ContainsKey(effect))
            {
                Destroy(_effects[effect].fillableImage.gameObject);
            }
        }

        private void _Refill(float currentTime)
        {
            foreach (var keyValuePair in _effects)
            {
                EffectData effectData = keyValuePair.Value;
                effectData.fillableImage.SetFillAmount(effectData.GetRemainder(currentTime));
            }
        }

        private void Update()
        {
            _Refill(Time.time);
        }
    }
}
