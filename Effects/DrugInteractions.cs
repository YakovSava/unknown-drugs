using UnityEngine;
using Unity.VisualScripting;

namespace UnknownDrugs.Effects
{
    public sealed class DrugInteractions : MonoBehaviour
    {
        private const float CheckInterval = 0.25f;
        private const float OpioidThreshold = 15f;
        private const float AlcoholThreshold = 8f;
        private const float AntidepressantThreshold = 5f;

        private const float OpioidBenzoCooldown = 60f;
        private const float OpioidAlcoholCooldown = 20f;
        private const float CocaineAlcoholCooldown = 45f;
        private const float SerotoninCooldown = 20f;
        private const float GoofballCooldown = 45f;
        private const float TranqCooldown = 60f;
        private const float KrokodilCooldown = 60f;
        private const float SpeedballCooldown = 45f;

        private const float TranqNecrosisSeconds = 300f;
        private const float TranqHeartRatePerSecond = 1.5f;
        private const float TranqSepticShockPerSecond = 0.06f;
        private const float TranqSkinDamagePerSecond = 0.1f;
        private const float TranqInfectionPerSecond = 0.05f;
        private const float TranqInfectionChance = 0.01f;

        private Body body;
        private float timer;
        private float nextOpioidBenzo;
        private float nextOpioidAlcohol;
        private float nextCocaineAlcohol;
        private float nextSerotonin;
        private float nextGoofball;
        private float nextTranq;
        private float nextKrokodil;
        private float nextSpeedball;

        private void Awake()
        {
            body = GetComponent<Body>();
        }

        private void Update()
        {
            if (!body.alive)
            {
                return;
            }
            timer -= Time.deltaTime;
            if (timer > 0f)
            {
                return;
            }
            timer = CheckInterval;
            CheckOpioidBenzo();
            CheckOpioidAlcohol();
            CheckCocaineAlcohol();
            CheckSerotoninSyndrome();
            CheckGoofball();
            CheckTranq();
            CheckKrokodil();
            CheckSpeedball();
        }

        private void CheckOpioidBenzo()
        {
            if (Time.time < nextOpioidBenzo)
            {
                return;
            }
            if (!OpioidActive() || !Active("diazepam"))
            {
                return;
            }
            nextOpioidBenzo = Time.time + OpioidBenzoCooldown;
            body.overdoseIndex = 2;
            body.GetOrAddComponent<Painkillers>().opiateAmount += 40f;
            body.GetOrAddComponent<SleepingPills>().amount += 150f;
            body.respiratoryRate = Mathf.Min(body.respiratoryRate, 20f);
            body.consciousness = Mathf.Max(0f, body.consciousness - 25f);
            body.Ragdoll();
            if (Random.value < 0.5f)
            {
                body.vomiter.Vomit();
            }
        }

        private void CheckOpioidAlcohol()
        {
            if (Time.time < nextOpioidAlcohol)
            {
                return;
            }
            if (!OpioidActive() || !AlcoholActive())
            {
                return;
            }
            nextOpioidAlcohol = Time.time + OpioidAlcoholCooldown;
            body.respiratoryRate = Mathf.Min(body.respiratoryRate, 45f);
            body.bloodOxygen -= 4f;
            body.consciousness = Mathf.Max(0f, body.consciousness - 10f);
            if (Random.value < 0.4f)
            {
                body.vomiter.Vomit();
            }
            if (Random.value < 0.2f)
            {
                body.Ragdoll();
            }
        }

        private void CheckCocaineAlcohol()
        {
            if (Time.time < nextCocaineAlcohol)
            {
                return;
            }
            if (!Active("cocaine") || !AlcoholActive())
            {
                return;
            }
            nextCocaineAlcohol = Time.time + CocaineAlcoholCooldown;
            body.TryStartFibrillation(forced: true);
            body.heartRate += 25f;
            body.bloodPressure += 15f;
        }

        private void CheckSerotoninSyndrome()
        {
            if (Time.time < nextSerotonin)
            {
                return;
            }
            if (!Active("tramadol") || !AntidepressantActive())
            {
                return;
            }
            nextSerotonin = Time.time + SerotoninCooldown;
            body.temperature += 3.5f;
            body.miscShakeIntensity += 5f;
            body.temporarySlowdown = Mathf.Max(body.temporarySlowdown, 0.8f);
            body.heartRate += 20f;
            body.bloodPressure += 10f;
            body.shock += 10f;
        }

        private void CheckGoofball()
        {
            if (Time.time < nextGoofball)
            {
                return;
            }
            if (!Active("pervitin") || !Active("heroin"))
            {
                return;
            }
            nextGoofball = Time.time + GoofballCooldown;
            body.happiness += 40f;
            body.adrenaline += 40f;
            body.stamina += 15f;
            body.heartRate += 15f;
            body.bloodPressure += 10f;
            body.respiratoryRate = Mathf.Min(body.respiratoryRate, 60f);
            if (Random.value < 0.2f)
            {
                body.TryStartFibrillation();
            }
        }

        private void CheckTranq()
        {
            if (Time.time < nextTranq)
            {
                return;
            }
            if (!Active("fentanyl") || !Active("xylazine"))
            {
                return;
            }
            nextTranq = Time.time + TranqCooldown;
            body.heartRate += 30f;
            body.bloodPressure -= 10f;
            body.respiratoryRate = Mathf.Min(body.respiratoryRate, 50f);
            body.septicShock += 5f;
            DamageNecrosis(body);
            CoUtils.instance.DoTimedOp(
                "tranqnecrosis",
                () => TranqTick(body),
                TranqNecrosisSeconds);
        }

        private void CheckKrokodil()
        {
            if (Time.time < nextKrokodil)
            {
                return;
            }
            if (!Active("desomorphine") || !Active("bleach"))
            {
                return;
            }
            nextKrokodil = Time.time + KrokodilCooldown;
            body.septicShock += 40f;
            body.internalBleeding += 20f;
            body.bloodViscosity += 25f;
            body.hasPulmonaryEmbolism = true;
            body.TryStartFibrillation(forced: true);
            body.vomiter.Vomit();
            foreach (Limb limb in body.limbs)
            {
                limb.infected = true;
                limb.infectionAmount += 10f;
                limb.skinHealth = Mathf.Max(0f, limb.skinHealth - 8f);
                limb.muscleHealth = Mathf.Max(0f, limb.muscleHealth - 8f);
                limb.pain += 20f;
            }
        }

        private void CheckSpeedball()
        {
            if (Time.time < nextSpeedball)
            {
                return;
            }
            if (!Active("cocaine") || !Active("desomorphine"))
            {
                return;
            }
            nextSpeedball = Time.time + SpeedballCooldown;
            body.TryStartFibrillation(forced: true);
            body.respiratoryRate = Mathf.Min(body.respiratoryRate, 55f);
            body.happiness += 30f;
            body.adrenaline += 30f;
            body.heartRate += 20f;
        }

        private static void TranqTick(Body body)
        {
            body.heartRate += TranqHeartRatePerSecond;
            body.septicShock += TranqSepticShockPerSecond;
            foreach (Limb limb in body.limbs)
            {
                limb.skinHealth = Mathf.Max(0f, limb.skinHealth - TranqSkinDamagePerSecond);
                limb.infectionAmount += TranqInfectionPerSecond;
                if (Random.value < TranqInfectionChance)
                {
                    limb.infected = true;
                }
            }
        }

        private static void DamageNecrosis(Body body)
        {
            foreach (Limb limb in body.limbs)
            {
                limb.skinHealth = Mathf.Max(0f, limb.skinHealth - 2f);
                limb.infectionAmount += 1f;
            }
        }

        private bool OpioidActive()
        {
            return body.TryGetComponent(out Painkillers painkillers)
                && painkillers.opiateAmount >= OpioidThreshold;
        }

        private bool AlcoholActive()
        {
            return body.TryGetComponent(out Intoxication intoxication)
                && intoxication.Level >= AlcoholThreshold;
        }

        private bool AntidepressantActive()
        {
            return body.TryGetComponent(out Antidepressants antidepressants)
                && antidepressants.currentAmount >= AntidepressantThreshold;
        }

        private bool Active(string id)
        {
            return SubstanceTracker.IsActive(body, id);
        }
    }
}
