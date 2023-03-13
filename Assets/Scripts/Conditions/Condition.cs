using System;
using ChiciStudios.BrigittesPlight.Battle;
using ChiciStudios.BrigittesPlight.Cards;
using ChiciStudios.BrigittesPlight.Cards.Entity;
using UnityEngine;

namespace ChiciStudios.BrigittesPlight.Conditions
{
    [Serializable]
    public abstract class Condition
    {
        #region For your benefit, leave me closed
        
        // ReSharper disable once InconsistentNaming
        // PLEASE DON'T TOUCH! Needed to create array element labels in the CardModel inspector of the action type (because Unity).
        [HideInInspector] [SerializeField] private string name;
        protected Condition() { name = this.GetType().Name; }
        
        #endregion
        
        public abstract bool IsSatisfied(BattleContext context, CardEntity castingCard, CastReserve castReserve);
    }
}