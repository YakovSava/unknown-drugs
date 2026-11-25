using Newtonsoft.Json;
using UnityEngine;

namespace UnknownDrugs.Effects
{
    public abstract class ToxinLoad : MonoBehaviour
    {
        [JsonProperty]
        protected float load;

        protected Body body;

        protected abstract float DecayPerSecond { get; }

        protected virtual bool HasPendingWork => false;

        public void Add(float amount)
        {
            load += amount;
        }

        protected virtual void Absorb(float deltaTime)
        {
        }

        protected abstract void ApplySymptoms(float deltaTime);

        private void Awake()
        {
            body = GetComponent<Body>();
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            Absorb(deltaTime);
            ApplySymptoms(deltaTime);
            load = Mathf.Max(0f, load - DecayPerSecond * deltaTime);
            if (load <= 0f && !HasPendingWork)
            {
                Destroy(this);
            }
        }
    }
}
