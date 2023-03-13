using System;
using ChiciStudios.BrigittesPlight.Battle;
using ChiciStudios.BrigittesPlight.Cards;
using ChiciStudios.BrigittesPlight.Cards.Entity;
using UnityEngine;

namespace ChiciStudios.BrigittesPlight.Conditions
{
    public class CastReserveCondition : Condition
    {
        [SerializeField] private int _castResultIndex;
        [SerializeField] private Operator _operator;
        [SerializeField] private int _expectedResult;
        
        public override bool IsSatisfied(BattleContext context, CardEntity castingCard, CastReserve castReserve)
        {
            var intToEvaluate = castReserve.GetReservedInt(0);
            return _operator switch
            {
                Operator.EqualTo => intToEvaluate == _expectedResult,
                Operator.LessThan => intToEvaluate < _expectedResult,
                Operator.GreaterThan => intToEvaluate > _expectedResult,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}