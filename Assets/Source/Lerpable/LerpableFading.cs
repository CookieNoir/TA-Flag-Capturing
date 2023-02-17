using System;
using System.Collections;
using UnityEngine;

namespace FlagCapturing.Lerpable
{
    public static class LerpableFading
    {
        private static IEnumerator Lerp<T>(bool isAppearing, Lerpable<T> lerpable, float duration)
        {
            Func<float, float> factorModifier = isAppearing ? (x => x) : (x => 1f - x);
            float timeElampsed = 0f;
            while (timeElampsed < duration)
            {
                lerpable.LerpAndSend(factorModifier(timeElampsed / duration));
                yield return null;
                timeElampsed += Time.deltaTime;
            }
            lerpable.LerpAndSend(factorModifier(1f));
        }

        public static IEnumerator Fade<T>(Lerpable<T> lerpable, float duration)
        {
            return Lerp(false, lerpable, duration);
        }

        public static IEnumerator Appear<T>(Lerpable<T> lerpable, float duration)
        {
            return Lerp(true, lerpable, duration);
        }
    }
}
