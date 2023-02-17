using System;
using System.Collections;
using FlagCapturing.Entities;
using FlagCapturing.Entities.Effects;
using FlagCapturing.Flags;
using FlagCapturing.Minigames;
using FlagCapturing.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FlagCapturing.Core
{
    public class CapturingResolver
    {
        Settings _settings;
        Player _player;
        FlagHandler _flagHandler;
        FlagSpawner _flagSpawner;
        IMinigame _minigame;
        EffectRegistry _effectRegistry;
        BaseEffect _onFailureEffect;
        IEnumerator _startedCoroutine;

        public CapturingResolver(Settings settings,
            Player player, 
            FlagHandler flagHandler,
            FlagSpawner flagSpawner,
            IMinigame minigame, 
            EffectRegistry effectRegistry)
        {
            _settings = settings;
            _player = player;
            _flagHandler = flagHandler;
            _flagSpawner = flagSpawner;
            _minigame = minigame;
            _effectRegistry = effectRegistry;

            _Connect();
        }

        private void _Connect()
        {
            _onFailureEffect = _effectRegistry.GetEffectByName(_settings.onFailureEffectName);
            _minigame.OnFailure += _ApplyFailureEffect;

            _flagHandler.OnEnabledChecked += (x => Resolve());
            _player.Effects.OnEffectsChanged += Resolve;
            _minigame.OnSuccess += Resolve;

            _flagSpawner.OnFlagSpawned += (flag => flag.Capturable.OnCaptured += Stop);
        }

        private void _ApplyFailureEffect()
        {
            _player.Effects.AddEffect(_onFailureEffect);
        }

        public void Resolve()
        {
            if (_player.Effects.HasEffect(_onFailureEffect))
            {
                _flagHandler.ForbidCapturing();
            }
            else
            {
                _flagHandler.AllowCapturing();
                if (_flagHandler.enabled)
                {
                    _StartDelayedMinigame();
                }
                else
                {
                    _StopCoroutine();
                }
            }
        }

        private void _StartDelayedMinigame()
        {
            float delay = Random.Range(0f, _settings.maxDelayBeforeMinigame);
            _startedCoroutine = IEnumeratorTimer.StartTimer(delay, _minigame.Launch);
            _player.StartCoroutine(_startedCoroutine);
        }

        private void _StopCoroutine()
        {
            if (_startedCoroutine != null)
            {
                _player.StopCoroutine(_startedCoroutine);
                _startedCoroutine = null;
            }
        }

        public void Stop()
        {
            _StopCoroutine();
            _minigame.Stop();
        }

        [Serializable]
        public class Settings
        {
            public string onFailureEffectName;
            [Min(0f)] public float maxDelayBeforeMinigame;
        }
    }
}
