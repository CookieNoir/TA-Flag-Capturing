using System;
using System.Collections.Generic;

namespace FlagCapturing.Entities.Effects
{
    public class EffectRegistry
    {
        Dictionary<string, BaseEffect> _effects = new();

        public EffectRegistry(Settings settings)
        {
            _FillRegistry(settings.effects);
        }

        private void _FillRegistry(IEnumerable<BaseEffect> effects)
        {
            foreach (BaseEffect effect in effects)
            {
                _effects[effect.Name] = effect;
            }
        }

        public BaseEffect GetEffectByName(string name)
        {
            BaseEffect result;
            _effects.TryGetValue(name, out result);
            return result;
        }

        [Serializable]
        public class Settings
        {
            public BaseEffect[] effects;
        }
    }
}
