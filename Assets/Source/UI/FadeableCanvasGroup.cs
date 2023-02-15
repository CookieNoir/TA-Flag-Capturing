using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using FlagCapturing.UI.Lerpable;
using FlagCapturing.UI.Fadeable;

namespace FlagCapturing.UI
{
    public class FadeableCanvasGroup : MonoBehaviour, IFadeable
    {
        [SerializeField] CanvasGroup _canvasGroup;
        [SerializeField, Min(0.01f)] float _duration;
        [SerializeField] LerpableFloat _valueRange;
        public UnityEvent OnFaded;
        public UnityEvent OnAppeared;

        private void Awake()
        {
            _valueRange.OnValueSent += SetAlpha;
        }

        private void OnDestroy()
        {
            _valueRange.OnValueSent -= SetAlpha;
        }

        public void SetAlpha(float value)
        {
            _canvasGroup.alpha = value;
        }

        public void Appear()
        {
            StopAllCoroutines();
            OnAppeared.Invoke();
            StartCoroutine(LerpableFading.Appear(_valueRange, _duration));
        }

        public void Fade()
        {
            StopAllCoroutines();
            StartCoroutine(_FadeWithInvoke());
        }

        private IEnumerator _FadeWithInvoke()
        {
            yield return StartCoroutine(LerpableFading.Fade(_valueRange, _duration));
            OnFaded.Invoke();
        }
    }
}
