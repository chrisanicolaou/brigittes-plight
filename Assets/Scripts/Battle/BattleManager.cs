using System;
using System.Threading.Tasks;
using ChiciStudios.BrigittesPlight.Cards.Entity;
using ChiciStudios.BrigittesPlight.GameEvents;
using Cysharp.Threading.Tasks;

namespace ChiciStudios.BrigittesPlight.Battle
{
    public class BattleManager
    {
        private BattleContext _battleContext = new BattleContext();
        
        public async UniTask CastCard(CardEntity card)
        {
            await FireGameEvent(GameEventType.PreCastPhase);
            await card.Cast(_battleContext);
            await FireGameEvent(GameEventType.CastPhase);
        }

        public async Task FireGameEvent(GameEventType gameEvent)
        {
        }
    }
}