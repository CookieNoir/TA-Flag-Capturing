using System.Threading.Tasks;
using UnityEngine;
using FlagCapturing.Controllers;
using FlagCapturing.Entities;
using FlagCapturing.Minigames;
using FlagCapturing.Configs;
using FlagCapturing.Connectors;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] SceneConfigLoader _sceneConfigLoader;
    [Space(10)]
    [SerializeField] SuccessZoneClickMinigame _minigame;
    [SerializeField] Joystick _joystick;
    [SerializeField] Player _player;

    public override async void InstallBindings()
    {
        await _BindConfig();
        _BindCore();
        _BindConnectors();
    }

    private async Task _BindConfig()
    {
        SceneConfig sceneConfig = await _sceneConfigLoader.LoadConfigAsync();
        Container.Bind<SuccessZoneClickMinigame.Settings>()
            .FromInstance(sceneConfig.minigameSettings)
            .AsSingle();
        Container.Bind<Joystick.Settings>()
            .FromInstance(sceneConfig.joystickSettings)
            .AsSingle();
        Container.Bind<Player.Settings>()
            .FromInstance(sceneConfig.playerSettings)
            .AsSingle();
    }

    private void _BindCore()
    {
        Container.Bind<IMinigame>()
            .To<SuccessZoneClickMinigame>()
            .FromInstance(_minigame)
            .AsSingle();
        Container.Bind<IPlayerController>()
            .To<Joystick>()
            .FromInstance(_joystick)
            .AsSingle();
        Container.Bind<Player>()
            .FromInstance(_player)
            .AsSingle();
    }

    private void _BindConnectors()
    {
        Container.Bind<PlayerControllerConnector>()
            .AsSingle()
            .NonLazy();
    }
}