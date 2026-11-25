# Unknown Drugs

Unofficial content mod for **Casualties: Unknown** that adds 39 new liquids: opioids, stimulants, sedatives, antidotes, psychoactive substances, alcohol, and poisons.

**Version:** 0.1.0

[Download on Nexus Mods](https://www.nexusmods.com/scavprototype/mods/1075)

Nexus Mods is the primary download. This repository contains the source code.

## Requirements

- Casualties: Unknown (full game)
- BepInEx 5 (x64)

No other mods or libraries are required.

## Installation

1. Install BepInEx 5 into the game folder.
2. Copy `UnknownDrugs.dll` into `BepInEx/plugins/`.
3. Launch the game.

A subfolder such as `BepInEx/plugins/UnknownDrugs/` also works. A successful load is logged as:

```text
Unknown Drugs: registered 39 liquids
```

## Main features

- 39 new liquids with custom effects, tolerance, addiction, and withdrawal.
- Opioids, stimulants, sedatives, antidotes, psychedelics, alcohol, and poisons.
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
| `sodiumoxybate` | 20 ml relaxation; 50 ml = loss of consciousness; 150 ml = hypoxia | Nonlinear dose response |
| `flumazenil` | 30 ml after diazepam | Wakes the patient; 40% chance of seizure |
| `propranolol` | 25 ml after a stimulant | Extinguishes the stimulant, lowers blood pressure |

## Psychoactive

| Liquid | Dose | What to expect |
|---|---:|---|
| `ketamine` | 40 ml | Body goes limp, no pain, screen distortion |
| `scopolamine` | 40 ml | Delirium, inverted controls, lapses of consciousness |
| `lsd` | 50 ml (100 ml = 300 s) | Strong distortion; 20% bad trip |
| `psilocybin` | 50 ml | Nausea, then distortion |
| `liquidcannabis` | 50 ml | Relaxation and mild pain relief, followed by intense hunger |
| `nanites` | 50 ml | Repairs skin and muscle, slows bleeding, damages the brain, may cause seizures |

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

- Doses above are examples from the Nexus page, not hard caps.
- Most drugs can be injected; alcoholic drinks cannot.
- Desomorphine injection has a high gangrene risk.
- Methanol and antifreeze poisoning are treated by drinking ethanol.
- The mod hooks into vanilla alcohol and adds its own intoxication, tolerance, craving, and withdrawal components.

## Building from source

Requirements: .NET SDK with .NET Framework 4.7.2 targeting support.

```bash
dotnet build -c Release
```

`UnknownDrugs.csproj` references the game assemblies through the `GameDir` property. It currently points to the demo installation:

```xml
<GameDir>E:\SteamLibrary\steamapps\common\Casualties Unknown Demo</GameDir>
```

Change it to the full game, or override it on the command line:

```bash
dotnet build -c Release -p:GameDir="E:\SteamLibrary\steamapps\common\Casualties Unknown"
```

Output: `bin/Release/net472/UnknownDrugs.dll`.

## Project layout

```text
Core/              drug base class, registry, timed effects, vanilla alcohol hook
Drugs/             opioids, stimulants, sedatives, psychoactive, alcohol, poisons
Effects/           intoxication, toxin loads, withdrawal, convulsions, gangrene
Localization/      English and Russian names and descriptions
Plugin.cs          BepInEx entry point
```

## Credits

- **Casualties: Unknown** — Orsoniks
- **BepInEx** and **HarmonyX** — their respective authors
- **Unknown Drugs** — YakovSava
- Thanks to **Yuzvynsky Yehor**

## License

MIT. See [LICENSE](LICENSE).

This is an unofficial fan-made mod and is not affiliated with the developers of Casualties: Unknown. For in-game use only.
