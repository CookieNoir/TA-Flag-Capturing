using System.Collections.Generic;
using UnityEngine;

namespace FlagCapturing.Triggers
{
    public class TriggerHandler : MonoBehaviour
    {
        internal class TriggerState
        {
            internal bool Visited;
        }

        Dictionary<BaseTrigger, TriggerState> _triggers = new();
        HashSet<Transform> _transforms = new();

        public void AddTransform(Transform transform)
        {
            _transforms.Add(transform);
        }

        public void RemoveTransform(Transform transform)
        {
            _transforms.Remove(transform);
        }

        public void AddTrigger(BaseTrigger trigger)
        {
            _triggers[trigger] = new TriggerState() { Visited = false };
        }

        public void RemoveTrigger(BaseTrigger trigger)
        {
            _triggers.Remove(trigger);
        }

        private void _CheckTriggers()
        {
            foreach (var keyValuePair in _triggers)
            {
                BaseTrigger trigger = keyValuePair.Key;
                TriggerState state = keyValuePair.Value;
                bool resultValue = false;
                foreach (var transform in _transforms)
                {
                    if (trigger.IsInside(transform))
                    {
                        resultValue = true;
                        break;
                    }
                }
                if (state.Visited != resultValue)
                {
                    if (resultValue) trigger.OnTriggerEnterInternal();
                    else trigger.OnTriggerExitInternal();
                }
                state.Visited = resultValue;
            }
        }

        private void Update()
        {
            _CheckTriggers();
        }
    }
}
