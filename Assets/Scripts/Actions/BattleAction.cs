using System;
using System.Linq;
using ChiciStudios.BrigittesPlight.Battle;
using ChiciStudios.BrigittesPlight.Cards;
using ChiciStudios.BrigittesPlight.Cards.Entity;
using ChiciStudios.BrigittesPlight.Conditions;
using ChiciStudios.BrigittesPlight.GameEvents;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;
using Vertx.Attributes;

namespace ChiciStudios.BrigittesPlight.Actions
{
    [Serializable]
    public abstract class BattleAction
    {
        #region For your benefit, leave me closed
        
        // ReSharper disable once InconsistentNaming
        // PLEASE DON'T TOUCH! Needed to create array element labels in the CardModel inspector of the action type (because Unity).
        [HideInInspector] [SerializeField] private string name;
        protected BattleAction() { name = this.GetType().Name; }
        
        #endregion

        [SerializeField] private TargetType _target;
        [SerializeField] private int _value;
        [SerializeField] private bool _hasConditions;
        [SerializeReference, ReferenceDropdown] [EnableIf("_hasConditions")] private Condition[] _conditions;
        [SerializeField] private bool _writeToCastReserve;
        [SerializeField] private bool _readFromCastReserve;
        [SerializeField] [EnableIf(EConditionOperator.Or,"_writeToCastReserve", "_readFromCastReserve")] private int _reserveIndex;

        public TargetType Target => _target;
        protected int Value => _value;
        public abstract GameEventType PrePhaseEvent { get; }
        public abstract GameEventType PhaseEvent { get; }

        public virtual async UniTask Execute(BattleContext context, CardEntity castingCard, CastReserve castReserve)
        {
            // Needs to queue phase events
            var result = await ExecuteInternal(context, castingCard, castReserve);
            if (_writeToCastReserve) castReserve.SetReservedInt(0, result);
        }

        public abstract UniTask<int> ExecuteInternal(BattleContext context, CardEntity castingCard, CastReserve castReserve);

        public virtual bool CanExecute(BattleContext context, CardEntity castingCard, CastReserve castReserve)
        {
            return _conditions?.All(condition => condition.IsSatisfied(context, castingCard, castReserve)) ?? true;
        }
    }
}