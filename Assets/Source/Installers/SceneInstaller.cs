using System.Threading.Tasks;
using UnityEngine;
using FlagCapturing.Controllers;
using FlagCapturing.Entities;
using FlagCapturing.Entities.Effects;
using FlagCapturing.Minigames;
using FlagCapturing.Configs;
using FlagCapturing.Connectors;
using FlagCapturing.Flags;
using FlagCapturing.Core;
using FlagCapturing.Triggers;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] SceneConfigLoader _sceneConfigLoader;
    [Space(10)]
    [SerializeField] SuccessZoneClickMinigame _minigame;
    [SerializeField] Joystick _controller;
    [SerializeField] Player _player;
    [Space(10)]
    [SerializeField] FlagSpawner _flagSpawner;
    [SerializeField] FlagHandler _flagHandler;
    [SerializeField] TriggerHandler _triggerHandler;
    [Space(10)]
    [SerializeField] Game _game;

    public override async void InstallBindings()
    {
        await _BindConfig();
        _BindPlayer();
        _BindFlags();
        _BindCore();
        _BindUI();
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
        Container.Bind<Flag.Settings>()
            .FromInstance(sceneConfig.flagSettings)
            .AsSingle();
        Container.Bind<FlagSpawner.Settings>()
            .FromInstance(sceneConfig.flagSpawnerSettings)
            .AsSingle();
        Container.Bind<EffectRegistry.Settings>()
            .FromInstance(sceneConfig.effectRegistrySettings)
            .AsSingle();
        Container.Bind<CapturingResolver.Settings>()
            .FromInstance(sceneConfig.resolverSettings)
            .AsSingle();
        Container.Bind<Game.Settings>()
            .FromInstance(sceneConfig.gameSettings)
            .AsSingle();
    }

    private void _BindPlayer()
    {
        Container.Bind<EffectRegistry>()
        .AsSingle();
        Container.Bind<IPlayerController>()
            .To<Joystick>()
            .FromInstance(_controller)
            .AsSingle();
        Container.Bind<Player>()
            .FromInstance(_player)
            .AsSingle();
        Container.Bind<PlayerControllerConnector>()
            .AsSingle()
            .NonLazy();
    }

    private void _BindFlags()
    {
        Container.BindFactory<UnityEngine.Object, Flag, Flag.Factory>()
        .FromFactory<PrefabFactory<Flag>>();
        Container.Bind<TriggerHandler>()
            .FromInstance(_triggerHandler)
            .AsSingle();
        Container.BindInterfacesAndSelfTo<FlagSpawner>()
            .FromInstance(_flagSpawner)
            .AsSingle();
        Container.Bind<FlagHandler>()
            .FromInstance(_flagHandler)
            .AsSingle();
        Container.Bind<TriggerHandlerConnector>()
            .AsSingle()
            .NonLazy();
    }

    private void _BindCore()
    {
        Container.Bind<IMinigame>()
            .To<SuccessZoneClickMinigame>()
            .FromInstance(_minigame)
            .AsSingle();
        Container.Bind<CapturingResolver>()
            .AsSingle()
            .NonLazy();
        Container.Bind<Game>()
            .FromInstance(_game)
            .AsSingle();
        Container.Bind<FlagAndGameConnector>()
            .AsSingle()
            .NonLazy();
    }

    private void _BindUI()
    {
        Container.Bind<EffectHandler>()
            .FromInstance(_player.Effects)
            .AsSingle();
    }
}
