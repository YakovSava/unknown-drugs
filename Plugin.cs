using BepInEx;
using HarmonyLib;
using UnknownDrugs.Core;
using UnknownDrugs.Localization;

namespace UnknownDrugs
{
    public static class PluginInfo
    {
        public const string Guid = "com.yakovsava.unknowndrugs";
        public const string Name = "Unknown Drugs";
        public const string Version = "0.1.0";
    }

    [BepInPlugin(PluginInfo.Guid, PluginInfo.Name, PluginInfo.Version)]
    public sealed class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            DrugRegistry.Register();
            new Harmony(PluginInfo.Guid).PatchAll(typeof(Plugin).Assembly);
            LocaleInjector.Inject();
            Logger.LogInfo($"{PluginInfo.Name}: registered {DrugRegistry.All.Count} liquids");
        }
    }
}
