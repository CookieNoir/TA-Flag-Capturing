using System.Collections;
using System.Collections.Generic;
using FlagCapturing.Entities;
using FlagCapturing.Triggers;
using FlagCapturing.Flags;
using UnityEngine;

namespace FlagCapturing.Connectors
{
    public class TriggerHandlerConnector
    {
        TriggerHandler _triggerHandler;
        Player _player;
        FlagSpawner _flagSpawner;

        public TriggerHandlerConnector(TriggerHandler triggerHandler, Player player, FlagSpawner flagSpawner)
        {
            _triggerHandler = triggerHandler;
            _player = player;
            _flagSpawner = flagSpawner;

            _flagSpawner.OnFlagSpawned += _AddTrigger;
            _triggerHandler.AddTransform(_player.transform);
        }

        private void _AddTrigger(Flag flag)
        {
            _triggerHandler.AddTrigger(flag.Trigger);
        }
    }
}
