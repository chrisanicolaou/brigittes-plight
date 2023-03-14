using System;
using ChiciStudios.BrigittesPlight.Battle;
using ChiciStudios.BrigittesPlight.Cards.Entity;
using Cysharp.Threading.Tasks;

namespace ChiciStudios.BrigittesPlight.Cards.Controller
{
    [Serializable]
    public abstract class CardController
    {
        public abstract UniTask OnCast(BattleContext battleContext);
        public abstract CardController AsNew();
    }
}