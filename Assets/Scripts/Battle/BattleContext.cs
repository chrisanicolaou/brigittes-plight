namespace ChiciStudios.BrigittesPlight.Battle
{
    public class BattleContext
    {
        private int _damageDealt;
        private int _healthRestored;
        private BattleManager _manager;

        public int DamageDealt => _damageDealt;
        public int HealthRestored => _healthRestored;
        public BattleManager Manager => _manager;

        public BattleContext(BattleManager manager)
        {
            _manager = manager;
        }
    }
}