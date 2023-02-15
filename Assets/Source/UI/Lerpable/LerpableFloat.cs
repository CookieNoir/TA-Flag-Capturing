using System;
using UnityEngine;

namespace FlagCapturing.UI.Lerpable
{
    [Serializable]
    public class LerpableFloat : Lerpable<float>
    {
        public LerpableFloat(float minValue, float maxValue) : base(minValue, maxValue)
        {
        }

        protected override float Lerp01(in float factor)
        {
            return Mathf.Lerp(MinValue, MaxValue, factor);
        }
    }
}
