using UnityEngine;

namespace FlagCapturing.UI
{
    public class RectTransformAnchorXSetter : MonoBehaviour
    {
        [SerializeField] RectTransform _rectTransform;

        public void SetAnchors(float x)
        {
            SetAnchors(x, x);
        }

        public void SetAnchors(Vector2 range)
        {
            SetAnchors(range.x, range.y);
        }

        public void SetAnchors(float minX, float maxX)
        {
            _rectTransform.anchorMin = new Vector2(minX, _rectTransform.anchorMin.y);
            _rectTransform.anchorMax = new Vector2(maxX, _rectTransform.anchorMax.y);
        }
    }
}
