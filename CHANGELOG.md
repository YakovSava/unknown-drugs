# Unknown Drugs — Changelog

## 0.2.0 — 2026-11-27

This release expands Unknown Drugs from a drug pack into a small drug-interaction sandbox: 10 new liquids, two rare fungal items, six crafting recipes, and a set of potentially lethal combinations.

**In-game tested.** The build is clean and the mod loads with 49 liquids, 2 items, and 6 recipes.

### Added — liquids (49 total)

| Liquid | What it does |
|---|---|
| `trazodone` | Sedating antidepressant. After about 35 seconds the legs give way, the eyes close, and sleep becomes easy. Weak "chloroform" effect without full unconsciousness |
| `ddvap` | Desmopressin. Holds water in; after about a minute causes hyponatremia: nausea, vomiting, and 1–3 points of brain damage. Only time clears it |
| `leptin` | Satiety hormone. Hunger disappears, but energy drains very quickly |
| `vasopressinleptin` | Fictional vasopressin–leptin chimera. Hyponatremia and fatigue, but hunger and thirst are slowed significantly. Found in barrels and craftable |
| `mptp` | In-game description is `???`. Short euphoria, then the brain dies. Mood collapses to −100 and stays there; even carfentanil does not restore it. Only Braingrow helps |
| `xylazine` | Veterinary tranquilizer. Slows the heart and breathing, causes heavy sedation and skin damage around repeated injections. Spawns in barrels |
| `goofball` | Craft-only: methamphetamine + heroin. Strong mixed high, hidden respiratory depression, fibrillation risk |
| `speedball` | Craft-only: cocaine + short-acting opioid. The stimulant masks the overdose until the heart or breathing fails |
| `tranq` | Craft-only: fentanyl + xylazine. Tachycardia, respiratory depression, and progressive skin necrosis. Naloxone does not stop the xylazine part |
| `krokodil` | Craft-only: desomorphine + bleach. Immediate tissue necrosis, blood clots, pulmonary embolism, and sepsis. Usually fatal |

### Added — fungal items

Both use the ordinary Fungus texture and can drop from any source that already drops ordinary Fungus.

| Item | Drop chance | Effect |
|---|---:|---|
| `psilocybefungus` | 5% | Mild hallucinations, raised mood, increased appetite, a little nausea |
| `amanitamuscaria` | 1% | Long euphoric trip, heavy brain damage, then an immediate hard withdrawal |

### Added — crafting

| Recipe | Ingredients | Result |
|---|---|---|
| Psilocybe brew | `psilocybefungus` + 20 ml `biochem` | 100 ml `psilocybin` |
| Vasopressin-Leptin | 40 ml `ddvap` + 40 ml `trazodone` + 20 ml `biochem` | 100 ml `vasopressinleptin` |
| Goofball | 50 ml `pervitin` + 50 ml `heroin` | 100 ml `goofball` |
| Speedball | 50 ml `cocaine` + 50 ml `desomorphine` | 100 ml `speedball` |
| Tranq | 5 ml `fentanyl` + 45 ml `xylazine` | 50 ml `tranq` |
| Krokodil | 50 ml `desomorphine` + 50 ml `bleach` | 100 ml `krokodil` |

Craft-only mixtures are added to the vanilla danger list, so they never spawn in random barrels. `xylazine` and `vasopressinleptin` do spawn in barrels.

### Added — drug interactions

Interactions trigger while both substances are active, even if they were taken separately rather than mixed beforehand.

| Combination | Effect |
|---|---|
| Opioid + benzodiazepine (`diazepam`) | Immediate overdose: deep respiratory depression, loss of consciousness, vomiting |
| Opioid + alcohol | Respiratory depression, low blood oxygen, vomiting, collapse |
| Cocaine + alcohol | Cocaethylene formation: fibrillation starts |
| Tramadol + antidepressants | Serotonin syndrome: rigidity, hyperthermia, shock, death if not stopped |
| `pervitin` + `heroin` | Goofball effect: strong mixed high, then fibrillation risk |
| `fentanyl` + `xylazine` | Tranq effect: tachycardia and necrosis that naloxone cannot stop |
| `desomorphine` + `bleach` | Krokodil effect: instant necrosis, thromboembolism, sepsis |
| `cocaine` + `desomorphine` | Speedball effect: forced fibrillation and respiratory depression |

### Added — statuses and effects

- Hyponatremia status with nausea, vomiting, and delayed brain damage.
- LSD withdrawal after the trip ends.
- Psilocybe and Amanita trip components.
- MPTP pleasure-center damage: locked mood, ongoing brain loss, and the Braingrow-only recovery rule.
- Substance tracking and a reusable interaction system for future combinations.
- Custom moodles for hyponatremia and MPTP brain damage.

### Changed

- Added a hard dependency on **CUCoreLib 1.1+** (`net.cucorelib`) for custom items, recipes, and moodles.
- The build now targets the full game installation instead of the demo installation.
- `psilocybin` is now displayed as **Psilocybe** and is craftable from the Psilocybe Fungus.
- README fully translated and updated in English.

### Requirements

- Casualties: Unknown (full game)
- BepInEx 5 (x64)
- CUCoreLib 1.1 or newer in `BepInEx/plugins/`

### Notes

- MPTP is a real neurotoxin. Its in-game description is intentionally `???`; the README explains what it does.
- Tranq cannot be fully reversed with naloxone; the xylazine part keeps working.
- Krokodil is usually fatal even with treatment.
- Back up existing saves before updating.
