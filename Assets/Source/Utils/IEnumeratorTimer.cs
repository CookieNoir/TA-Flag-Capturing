using System;
using System.Collections;
using UnityEngine;

namespace FlagCapturing.Utils
{
    public static class IEnumeratorTimer
    {
        public static IEnumerator StartTimer(float waitingTime, Action onTimeOut)
        {
            yield return new WaitForSeconds(waitingTime);
            onTimeOut?.Invoke();
        }
    }
}
