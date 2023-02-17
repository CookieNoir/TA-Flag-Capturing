using FlagCapturing.Lerpable;
using UnityEngine;

namespace FlagCapturing.Utils
{
    public class LerpableSpriteRenderer : MonoBehaviour
    {
        [SerializeField] LerpableColor _lerpableColor;
        [SerializeField] SpriteRenderer _spriteRenderer;

        public void SetValue(float value)
        {
            _spriteRenderer.color = _lerpableColor.Lerp(value);
        }
    }
}
