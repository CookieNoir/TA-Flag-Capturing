using System.Threading.Tasks;
using UnityEngine;

namespace FlagCapturing.Configs
{
    public class SceneConfigLoader : MonoBehaviour, IConfigLoader<SceneConfig>
    {
        [SerializeField] SceneConfig _config;

        public Task<SceneConfig> LoadConfigAsync()
        {
            return Task.FromResult(_config);
        }
    }
}
