using System;
using UnityEngine;
using FlagCapturing.Utils;

namespace FlagCapturing.Triggers
{
    public abstract class BaseTrigger: MonoBehaviour
    {
        public event Action OnTriggerEnter;
        public event Action OnTriggerExit;
        [field: SerializeField] public Resizeable Resizeable { get; private set; }

        internal abstract bool IsInside(Transform transform);

        internal void OnTriggerEnterInternal()
        {
            OnTriggerEnter?.Invoke();
        }

        internal void OnTriggerExitInternal()
        {
            OnTriggerExit.Invoke();
        }
    }
}
