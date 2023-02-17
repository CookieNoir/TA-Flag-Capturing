using UnityEngine;

public class TransformScaler : MonoBehaviour
{
    [SerializeField] Transform _targetTransform;
    [SerializeField] float _scaleModifier = 1f;

    public void SetScale(float value)
    {
        float modifiedValue = _scaleModifier * value;
        _targetTransform.localScale = new Vector3(modifiedValue, modifiedValue, modifiedValue);
    }
}
