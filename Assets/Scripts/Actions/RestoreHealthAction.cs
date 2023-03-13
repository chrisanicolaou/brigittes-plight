﻿using System;
using ChiciStudios.BrigittesPlight.Battle;
using ChiciStudios.BrigittesPlight.Cards;
using ChiciStudios.BrigittesPlight.Cards.Entity;
using ChiciStudios.BrigittesPlight.GameEvents;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ChiciStudios.BrigittesPlight.Actions
{
    public class RestoreHealthAction : BattleAction
    {
        public override GameEventType PrePhaseEvent => GameEventType.PreHealPhase;
        public override GameEventType PhaseEvent => GameEventType.HealPhase;

        public override async UniTask<int> ExecuteInternal(BattleContext context, CardEntity castingCard, CastReserve castReserve)
        {
            Debug.Log($"Restoring {Value} health to {Target}!");
            return Value;
        }
    }
}