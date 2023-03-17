using System;
using ChiciStudios.BrigittesPlight.Battle;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChiciStudios.BrigittesPlight.GameEvents
{
    public class GameEventExecutor
    {
        private static GameEventExecutor _instance;
        public static GameEventExecutor Instance => _instance ??= new GameEventExecutor();
        
        private int _triggerDepth;
        
        public async UniTask<GameEventContext> FireGameEvent(GameEventContext eventContext, BattleContext battleContext = null)
        {
            _triggerDepth++;
            if (_triggerDepth > 256) throw new Exception("Recursive event loop detected!");
            Debug.Log($"Firing GameEvent: {eventContext.Type}");
            if (battleContext == null)
            {
                await FireNonBattleGameEvent(eventContext);
            }
            else
            {
                await FireBattleGameEvent(eventContext, battleContext);
            }
            _triggerDepth--;
            if (_triggerDepth == 0 && battleContext != null)
            {
                await battleContext.Manager.EvaluateBattleState();
            }
            return eventContext;
        }

        private async UniTask FireBattleGameEvent(GameEventContext eventContext, BattleContext battleContext)
        {
            var triggers = battleContext.GetSubscribingTriggers(eventContext.Type);
            foreach (var trigger in triggers)
            {
                Debug.Log($"Trigger found! Firing {trigger}");
                await trigger.OnGameEventFired(battleContext, eventContext);
                Debug.Log($"Resolving back to: {eventContext}");
            }
        }

        private async UniTask FireNonBattleGameEvent(GameEventContext eventContext)
        {
            return;
        }
    }
}