using System.Collections;
using ChiciStudios.BrigittesPlight.Actions;
using ChiciStudios.BrigittesPlight.Cards.Entity;
using ChiciStudios.BrigittesPlight.Triggers;

namespace ChiciStudios.BrigittesPlight.GameEvents
{
    public class GameEventContext
    {
        public GameEventType Type { get; }
        public CardEntity CastingCard { get; }
        public BattleAction Action { get; }
        public bool IsSimulated { get; }

        public GameEventContext(GameEventType type, CardEntity castingCard = null, BattleAction action = null, bool isSimulated = false)
        {
            Type = type;
            CastingCard = castingCard;
            Action = action;
            IsSimulated = isSimulated;
        }

        public override string ToString()
        {
            return $"{nameof(GameEventContext)} Details: \nType: {Type} \nCallingAction:{Action} \nSimulated: {IsSimulated}";
        }
    }
}