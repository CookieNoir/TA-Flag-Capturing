using UnityEngine;
using UnityEngine.Events;

namespace FlagCapturing.Utils
{
    public class Resizeable: MonoBehaviour
    {
        [field: SerializeField] public float Size { get; private set; }
        public UnityEvent<float> OnSizeChanged;

        public void SetSize(float value)
        {
            Size = value;
            OnSizeChanged.Invoke(Size);
        }

        private void OnValidate()
        {
            SetSize(Size);
        }
    }
}
