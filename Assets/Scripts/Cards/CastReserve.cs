using System.Collections.Generic;

namespace ChiciStudios.BrigittesPlight.Cards
{
    public class CastReserve
    {
        private readonly List<int> _reservedInts = new();

        public int GetReservedInt(int index)
        {
            return _reservedInts[index];
        }

        public void SetReservedInt(int index, int value, bool additively = true)
        {
            if (additively)
            {
                _reservedInts[index] += value;
                return;
            }
            _reservedInts[index] = value;
        }
    }
}