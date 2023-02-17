using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlagCapturing.Utils
{
    public class SceneReloader : MonoBehaviour
    {
        public void Reload()
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
