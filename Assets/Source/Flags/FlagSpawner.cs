using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;
using Bounds = FlagCapturing.Utils.Bounds;

namespace FlagCapturing.Flags
{
    public class FlagSpawner : MonoBehaviour, IInitializable
    {
        public event Action<Flag> OnFlagSpawned;
        [SerializeField] GameObject _flagPrefab;
        [SerializeField] Vector2 _bound1;
        [SerializeField] Vector2 _bound2;
        Settings _settings;
        Flag.Factory _flagFactory;

        [Inject]
        public void Construct(Settings settings, Flag.Factory flagFactory)
        {
            _settings = settings;
            _flagFactory = flagFactory;
        }

        public void Initialize()
        {
            Bounds bounds = new(_bound1, _bound2);
            for (int i = 0; i < _settings.flagsQuantity; ++i)
            {
                float positionX = Random.Range(bounds.RangeX.x, bounds.RangeX.y),
                      positionZ = Random.Range(bounds.RangeY.x, bounds.RangeY.y);
                Flag flag = _flagFactory.Create(_flagPrefab);
                flag.transform.position = new Vector3(positionX, 0f, positionZ);
                OnFlagSpawned?.Invoke(flag);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            float centerX = (_bound1.x + _bound2.x) / 2f,
                  centerZ = (_bound1.y + _bound2.y) / 2f,
                  sizeX = Mathf.Abs(_bound1.x - _bound2.x),
                  sizeZ = Mathf.Abs(_bound1.y - _bound2.y);
            Gizmos.DrawWireCube(new Vector3(centerX, 0f, centerZ),
                                new Vector3(sizeX, 0f, sizeZ));
        }

        [Serializable]
        public class Settings
        {
            [Min(0)] public int flagsQuantity;
        }
    }
}
