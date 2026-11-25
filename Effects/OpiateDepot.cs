using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;

namespace UnknownDrugs.Effects
{
    public abstract class OpiateDepot : MonoBehaviour
    {
        private const float EmptyThreshold = 0.1f;

        [JsonProperty]
        protected float pool;

        private Body body;

        protected abstract float ReleaseFractionPerSecond { get; }

        public void Add(float amount)
        {
            pool += amount;
        }

        private void Awake()
        {
            body = GetComponent<Body>();
        }

        private void Update()
        {
            if (pool < EmptyThreshold)
            {
                Destroy(this);
                return;
            }
            float released = pool * ReleaseFractionPerSecond * Time.deltaTime;
            pool -= released;
            body.GetOrAddComponent<Painkillers>().opiateAmount += released;
        }
    }

    [Saveable]
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class MethadoneDepot : OpiateDepot
    {
        protected override float ReleaseFractionPerSecond => 0.012f;
    }

    [Saveable]
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class CodeineDepot : OpiateDepot
    {
        protected override float ReleaseFractionPerSecond => 0.03f;
    }
}
