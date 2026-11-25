using System.Collections.Generic;

namespace UnknownDrugs.Core
{
    public abstract class TimedEffectDrug : Drug
    {
        private const float LastTickSeconds = 1f;

        public abstract string EffectId { get; }

        public override IReadOnlyList<string> ActiveEffectIds => new[] { EffectId };

        protected abstract float SecondsPerHundredMl { get; }

        protected abstract void Tick(Body body);

        protected virtual void OnImmediate(float ml, Body body)
        {
        }

        protected virtual void OnEnd(Body body)
        {
        }

        public sealed override void Apply(float ml, Body body)
        {
            OnImmediate(ml, body);
            float duration = SecondsPerHundredMl * ml * 0.01f;
            CoUtils.instance.DoTimedOp(EffectId, () => Step(body), duration);
        }

        private void Step(Body body)
        {
            Tick(body);
            if (CoUtils.instance.DurationOf(EffectId) <= LastTickSeconds)
            {
                OnEnd(body);
            }
        }
    }
}
