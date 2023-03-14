using ChiciStudios.BrigittesPlight.Actions;

namespace ChiciStudios.BrigittesPlight.GameEvents
{
    public class GameEventContext
    {
        public GameEventType Type { get; }
        
        public BattleAction Action { get; }
        public bool IsSimulated { get; }

        public GameEventContext(GameEventType type, BattleAction action = null, bool isSimulated = false)
        {
            Type = type;
            Action = action;
            IsSimulated = isSimulated;
        }

        public override string ToString()
        {
            return $"{nameof(GameEventContext)} Details: \nType: {Type} \nCallingAction:{Action} \nSimulated: {IsSimulated}";
        }
    }
}