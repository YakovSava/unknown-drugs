using System;
using System.Collections.Generic;
using UnityEngine;
using UnknownDrugs.Localization;

namespace UnknownDrugs.Core
{
    public abstract class Drug
    {
        private static readonly IReadOnlyList<string> NoEffects = Array.Empty<string>();

        public abstract string Id { get; }

        public abstract Color Color { get; }

        public abstract float ValuePerLiter { get; }

        public abstract DrugText Text { get; }

        public virtual DrugClass Class => DrugClass.Other;

        public virtual bool Injectable => true;

        public virtual float InjectionSickness => 0f;

        public virtual bool IsLethalForCans => false;

        public virtual bool IsCraftOnly => false;

        public virtual float InjectionGangreneChance => 0f;

        public virtual IReadOnlyList<string> ActiveEffectIds => NoEffects;

        public virtual float InteractionSeconds(float ml) => 300f;

        public abstract void Apply(float ml, Body body);
    }
}
