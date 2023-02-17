using System;
using UnityEngine;

namespace FlagCapturing.Lerpable
{
    [Serializable]
    public class LerpableColor : Lerpable<Color>
    {
        public LerpableColor(Color minValue, Color maxValue) : base(minValue, maxValue) { }

        protected override Color Lerp01(in float factor)
        {
            return Color.Lerp(MinValue, MaxValue, factor);
        }
    }
}
