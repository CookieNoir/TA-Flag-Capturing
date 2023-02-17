using FlagCapturing.Controllers;
using FlagCapturing.Entities;
using FlagCapturing.Entities.Effects;
using FlagCapturing.Minigames;
using FlagCapturing.Flags;
using FlagCapturing.Core;
using UnityEngine;

namespace FlagCapturing.Configs
{
    [CreateAssetMenu(fileName = "Config", menuName = "Configs/Scene Config", order = 1)]
    public class SceneConfig : Config
    {
        public Player.Settings playerSettings;
        public Joystick.Settings joystickSettings;
        public EffectRegistry.Settings effectRegistrySettings;
        [Space(10)]
        public FlagSpawner.Settings flagSpawnerSettings;
        public Flag.Settings flagSettings;
        [Space(10)]
        public SuccessZoneClickMinigame.Settings minigameSettings;
        public CapturingResolver.Settings resolverSettings;
        public Game.Settings gameSettings;
    }
}
