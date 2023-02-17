using UnityEngine;
using UnityEngine.UI;

namespace FlagCapturing.UI
{
    public class FillableImage : MonoBehaviour
    {
        [SerializeField] Image _image;

        public void SetSprite(Sprite sprite)
        {
            _image.sprite = sprite;
            _image.type = Image.Type.Filled;
        }

        public void SetFillAmount(float value)
        {
            _image.fillAmount = value;
        }
    }
}
