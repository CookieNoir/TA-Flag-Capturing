using FlagCapturing.Controllers;
using FlagCapturing.Entities;
using FlagCapturing.Minigames;
using UnityEngine;

namespace FlagCapturing.Configs
{
    [CreateAssetMenu(fileName = "Config", menuName = "Configs/Scene Config", order = 1)]
    public class SceneConfig : Config
    {
        public SuccessZoneClickMinigame.Settings minigameSettings;
        public Joystick.Settings joystickSettings;
        public Player.Settings playerSettings;
    }
}
