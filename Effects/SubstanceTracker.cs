using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Unity.VisualScripting;

namespace UnknownDrugs.Effects
{
    [Saveable]
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class SubstanceTracker : MonoBehaviour
    {
        [JsonProperty]
        private Dictionary<string, float> remaining = new Dictionary<string, float>();

        private readonly List<string> keys = new List<string>();

        private void Awake()
        {
            this.GetOrAddComponent<DrugInteractions>();
        }

        public static void Mark(Body body, string id, float seconds)
        {
            if (body == null || string.IsNullOrWhiteSpace(id) || seconds <= 0f)
            {
                return;
            }
            body.GetOrAddComponent<SubstanceTracker>().Mark(id, seconds);
            body.GetOrAddComponent<DrugInteractions>();
        }

        public static bool IsActive(Body body, string id)
        {
            if (body == null || string.IsNullOrWhiteSpace(id))
            {
                return false;
            }
            return body.TryGetComponent(out SubstanceTracker tracker)
                && tracker.remaining.TryGetValue(id, out float seconds)
                && seconds > 0f;
        }

        private void Mark(string id, float seconds)
        {
            remaining.TryGetValue(id, out float current);
            remaining[id] = Mathf.Max(current, seconds);
        }

        private void Update()
        {
            keys.Clear();
            keys.AddRange(remaining.Keys);
            float deltaTime = Time.deltaTime;
            foreach (string id in keys)
            {
                float next = remaining[id] - deltaTime;
                if (next <= 0f)
                {
                    remaining.Remove(id);
                }
                else
                {
                    remaining[id] = next;
                }
            }
        }
    }
}
