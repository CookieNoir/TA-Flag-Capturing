using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace FlagCapturing.Core
{
    public class GameSettingsReceiver : MonoBehaviour
    {
        public UnityEvent<int> OnWinConditionReceived;

        [Inject]
        public void Construct(Game.Settings settings)
        {
            OnWinConditionReceived.Invoke(settings.flagsForWin);
        }
    }
}
