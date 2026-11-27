# Unknown Drugs

Unofficial content mod for **Casualties: Unknown** that adds 49 new liquids — opioids, stimulants, sedatives, hormones, antidotes, psychoactive substances, alcohol, poisons, and craft-only drug mixtures — plus two rare fungal items and a small crafting tree.

**Version:** 0.2.0

[Download on Nexus Mods](https://www.nexusmods.com/scavprototype/mods/1075)

Nexus Mods is the primary download. This repository contains the source code.

## Requirements

- Casualties: Unknown (full game)
- BepInEx 5 (x64)
- CUCoreLib 1.1 or newer (put `CUCoreLib.dll` into `BepInEx/plugins/`)

CUCoreLib provides the item, recipe, and status registration used by the fungi and the new crafting recipes. No other mods or libraries are required.

## Installation

1. Install BepInEx 5 into the game folder.
2. Copy `CUCoreLib.dll` into `BepInEx/plugins/`.
3. Copy `UnknownDrugs.dll` into `BepInEx/plugins/`.
4. Launch the game.

A subfolder such as `BepInEx/plugins/UnknownDrugs/` also works for `UnknownDrugs.dll`. A successful load is logged as:

```text
Unknown Drugs: registered 49 liquids
Unknown Drugs: registered 2 items and 6 recipes
```

## Main features

- 49 new liquids with custom effects, tolerance, addiction, and withdrawal.
- Opioids, stimulants, sedatives, hormones, antidotes, psychedelics, alcohol, poisons, and craft-only mixtures.
- Two rare fungal items that use the ordinary Fungus texture.
- Crafting recipes for Psilocybe brew, Vasopressin-Leptin, Goofball, Tranq, Krokodil, and Speedball.
- Dangerous combinations: opioid + benzodiazepine, opioid + alcohol, cocaine + alcohol, tramadol + antidepressants, and the named mixtures.
- Custom statuses and withdrawal effects.
- Drunk and injected routes, with injection sickness and gangrene risks.
- Russian and English names and descriptions.
- Vanilla alcohol integration.

## Console usage

Liquids can be spawned with the game console:

```text
addliquid <liquidId> <amountMl>
```

Examples:

```text
addliquid methadone 50
addliquid carfentanil 1
addliquid trazodone 50
addliquid ddvap 40
addliquid vasopressinleptin 100
addliquid mptp 10
addliquid xylazine 40
addliquid goofball 100
addliquid tranq 50
addliquid krokodil 100
addliquid speedball 100
addliquid vodka 300
```

For drinking, it is better to pour more and drink 100 ml at a time. A canteen (300 ml) or minibarrel is recommended.

## Opioids

| Liquid | Dose | What to expect |
|---|---:|---|
| `methadone` | 50 ml (up to 150) | Slow rise of opioids; doses pile up |
| `desomorphine` | 40 ml | Strong opioid; injection: 75% gangrene |
| `oxycodone` | 50 ml | Mild opioid, rapidly increasing tolerance, nausea |
| `codeine` | 50 ml | Delayed effect |
| `tramadol` | 40 ml safe; 100 ml = 33% seizure; 200 ml = 100% | Weak pain relief |
| `buprenorphine` | 40 ml | Ceiling effect. On top of morphine or desomorphine it displaces them and causes withdrawal |
| `oxylitin` | 30 ml | Opioid plus breathing support |
| `carfentanil` | 0.03 ml severe but survivable; 1 ml = death | Strong anesthesia; stops breathing. Large amounts of naloxone are needed |

## Stimulants

| Liquid | Dose | What to expect |
|---|---:|---|
| `mephedrone` | 40 ml | High tide, then recede |
| `alphapvp` | 30 ml (100 ml = 50 thrust) | Short high, erratic behaviour, then a long abstinence |
| `pervitin` | 50 ml (100 ml = 240 s) | No sleep, then a collapse |
| `cocaine` | 30 ml safe; 100 ml = 50% fibrillation; 160+ ml = 100% | Euphoria, numbs pain |
| `modafinil` | 50 ml | Focus, then sleep debt |
| `nicotine` | 5 ml calmly; 25 ml = seizure; 50 ml = cardiac arrest | Poisoning in large doses |
| `kinetic` | 60 ml | Involuntary kicks, screen shaking, then lethargy |
| `strychninetonic` | 20 ml safe; 60 ml = cramps; 150+ ml = death | Cheerfulness, then seizures |

## Sedatives and antidotes

| Liquid | Dose | What to expect |
|---|---:|---|
| `diazepam` | 40 ml | Calm; 100 ml together with an opioid is an overdose |
| `phenobarbital` | 50 ml (200 ml = overdose threshold) | Strong sleeping pills |
| `chloralhydrate` | 50 ml | Sleep, nausea |
| `trazodone` | 50 ml | After 30–50 seconds the legs give way and the eyes close; sleep comes easily |
| `xylazine` | 40 ml | Veterinary tranquilizer: slows the heart and breathing, heavy sedation, skin damage around repeated injections |
| `sodiumoxybate` | 20 ml relaxation; 50 ml = loss of consciousness; 150 ml = hypoxia | Nonlinear dose response |
| `flumazenil` | 30 ml after diazepam | Wakes the patient; 40% chance of seizure |
| `propranolol` | 25 ml after a stimulant | Extinguishes the stimulant, lowers blood pressure |

## Hormones and metabolism

| Liquid | Dose | What to expect |
|---|---:|---|
| `ddvap` | 40 ml | Water is held in; after a minute, hyponatremia: nausea, vomiting, 1–3 points of brain damage. Only time clears it |
| `leptin` | 50 ml | Appetite vanishes; energy drains very quickly |
| `vasopressinleptin` | 50 ml (craftable, also found in barrels) | Hyponatremia and fatigue, but hunger and thirst are slowed significantly |

Hyponatremia is a status effect. It appears after a delay, causes nausea and occasional vomiting, applies a small amount of brain damage, and cannot be cured by an antidote — only by waiting it out.

## Psychoactive

| Liquid | Dose | What to expect |
|---|---:|---|
| `ketamine` | 40 ml | Body goes limp, no pain, screen distortion |
| `scopolamine` | 40 ml | Delirium, inverted controls, lapses of consciousness |
| `lsd` | 50 ml (100 ml = 300 s) | Strong distortion; 20% bad trip; a hard withdrawal after the trip |
| `psilocybin` | 50 ml (crafted from Psilocybe Fungus) | Nausea, then strong distortion; stronger than eating the mushroom |
| `liquidcannabis` | 50 ml | Relaxation and mild pain relief, followed by intense hunger |
| `nanites` | 50 ml | Repairs skin and muscle, slows bleeding, damages the brain, may cause seizures |

## Fungi

Every source that already drops ordinary Fungus can also drop two additional items. Both look exactly like ordinary Fungus and both are edible.

| Item | Drop chance | What to expect |
|---|---:|---|
| `psilocybefungus` | 5% from any source that drops Fungus | Mild hallucinations, raised mood, increased appetite, a little nausea |
| `amanitamuscaria` | 1% from any source that drops Fungus | Long euphoric trip, heavy brain damage, then an immediate hard withdrawal |

Crafting:

| Recipe | Ingredients | Result |
|---|---|---|
| Psilocybe brew | `psilocybefungus` + 20 ml `biochem` | 100 ml `psilocybin` |
| Vasopressin-Leptin | 40 ml `ddvap` + 40 ml `trazodone` + 20 ml `biochem` | 100 ml `vasopressinleptin` |
| Goofball | 50 ml `pervitin` + 50 ml `heroin` | 100 ml `goofball` |
| Speedball | 50 ml `cocaine` + 50 ml `desomorphine` | 100 ml `speedball` |
| Tranq | 5 ml `fentanyl` + 45 ml `xylazine` | 50 ml `tranq` |
| Krokodil | 50 ml `desomorphine` + 50 ml `bleach` | 100 ml `krokodil` |

## Mixtures and dangerous combinations

The four named mixtures are craft-only and never spawn in barrels. Every listed combination also triggers when the two substances are taken separately, as long as both are still active.

### Craft-only mixtures

| Mixture | Recipe | What to expect |
|---|---|---|
| `goofball` | `pervitin` + `heroin` | Methamphetamine over heroin. The stimulant hides the opioid's respiratory depression. High fibrillation risk |
| `speedball` | `cocaine` + `desomorphine` | Cocaine plus a short-acting opioid. The stimulant masks the overdose until the heart or breathing fails |
| `tranq` | `fentanyl` + `xylazine` | Tachycardia, respiratory depression, and progressive skin necrosis. Naloxone reverses the opioid part but not the xylazine |
| `krokodil` | `desomorphine` + `bleach` | Immediate tissue necrosis, blood clots, pulmonary embolism, and sepsis. Usually fatal |

### Interaction rules

| Combination | Effect |
|---|---|
| Opioid + benzodiazepine (`diazepam`) | Immediate overdose: deep respiratory depression, loss of consciousness, vomiting |
| Opioid + alcohol | Respiratory depression, low blood oxygen, vomiting, collapse |
| Cocaine + alcohol | Cocaethylene formation: fibrillation starts |
| Tramadol + antidepressants | Serotonin syndrome: rigidity, hyperthermia, shock, death if it is not stopped |
| `pervitin` + `heroin` | Goofball effect: strong mixed high, then fibrillation risk |
| `fentanyl` + `xylazine` | Tranq effect: tachycardia and necrosis that naloxone cannot stop |
| `desomorphine` + `bleach` | Krokodil effect: instant necrosis, thromboembolism, sepsis |
| `cocaine` + `desomorphine` | Speedball effect: forced fibrillation and respiratory depression |

## Neurotoxins

| Liquid | Dose | What to expect |
|---|---:|---|
| `mptp` | 10 ml | A short burst of pleasure, then the brain dies. Mood collapses to −100 and stays there; even carfentanil cannot restore it. Brain damage continues until the brain is gone. Braingrow is the only treatment |

The in-game description of MPTP is literally `???`. This is intentional.

## Alcohol and poisons

| Liquid | Dose | What to expect |
|---|---:|---|
| `vodka` | 300 ml | 100 ml = +10 intoxication. Tipsy from 8, drunk from 25, nausea from 50, poisoning from 85 |
| `wine` | 300 ml | 100 ml = +3 intoxication |
| `beer` | 300 ml | 100 ml = +1.25 intoxication |
| `cognac` | 300 ml | Like vodka |
| `moonshine` | 300 ml | 100 ml = +11; 10% chance of a bad batch with methanol |
| `grandpastincture` | 150 ml | 150 ml = one eye goes blind; 235 ml = both. Effect is delayed by about a minute |
| `isopropanol` | 60 ml | Nausea, vomiting, brain damage |
| `methanol` | 25 ml = one eye; 50 ml = both | Vision loss after 1-2 minutes. Antidote: drink any alcohol immediately |
| `antifreeze` | 60 ml = brain damage; 150 ml = collapse | Delayed. Antidote: alcohol |
| `fowlerssolution` | 100 ml, several times | Arsenic accumulates: nausea, skin and brain damage |
| `radithor` | 100 ml | +10 radiation; 6 doses are hard on the body. A slight burst of energy |

## Notes

- Doses above are examples, not hard caps.
- Most drugs can be injected; alcoholic drinks cannot.
- Desomorphine injection has a high gangrene risk.
- Methanol and antifreeze poisoning are treated by drinking ethanol.
- LSD leaves a hard withdrawal after the trip.
- Amanita muscaria leaves a much harder withdrawal and heavy brain damage.
- Vasopressin-Leptin appears in random barrels, like most other non-lethal liquids.
- Goofball, Speedball, Tranq, and Krokodil are craft-only and never appear in random barrels.
- Naloxone does not stop the xylazine part of Tranq.
- Krokodil is usually fatal even with treatment.
- MPTP is a real neurotoxin that destroys the part of the brain responsible for pleasure. In real life it causes irreversible parkinsonism. The mod is fiction; do not use it as medical information.
- The mod hooks into vanilla alcohol and adds its own intoxication, tolerance, craving, and withdrawal components.

## Building from source

Requirements: .NET SDK with .NET Framework 4.7.2 targeting support, and the full game with BepInEx and CUCoreLib installed.

```bash
dotnet build -c Release
```

`UnknownDrugs.csproj` references the game assemblies and `CUCoreLib.dll` through the `GameDir` property. It points to the full game installation:

```xml
<GameDir>E:\SteamLibrary\steamapps\common\Casualties Unknown</GameDir>
```

Override it on the command line if the game is installed elsewhere:

```bash
dotnet build -c Release -p:GameDir="D:\Games\Casualties Unknown"
```

Output: `bin/Release/net472/UnknownDrugs.dll`.

## Project layout

```text
Core/              drug base class, registry, timed effects, recipes, vanilla alcohol hook
Drugs/             opioids, stimulants, sedatives, hormones, neurotoxins, psychoactive, alcohol, poisons, mixtures
Effects/           intoxication, toxin loads, withdrawal, hyponatremia, trips, convulsions, gangrene
Items/             custom fungal items, registration, mushroom drop hook
Localization/      English and Russian names and descriptions
Plugin.cs          BepInEx entry point
```

## Credits

- **Casualties: Unknown** — Orsoniks
- **BepInEx** and **HarmonyX** — their respective authors
- **CUCoreLib** — its authors
- **Unknown Drugs** — YakovSava
- Thanks to **Yuzvynsky Yehor**

## License

MIT. See [LICENSE](LICENSE).

This is an unofficial fan-made mod and is not affiliated with the developers of Casualties: Unknown. For in-game use only.
