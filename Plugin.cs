using BepInEx;
using HarmonyLib;
using UnknownDrugs.Core;
using UnknownDrugs.Items;
using UnknownDrugs.Localization;

namespace UnknownDrugs
{
    public static class PluginInfo
    {
        public const string Guid = "com.yakovsava.unknowndrugs";
        public const string Name = "Unknown Drugs";
        public const string Version = "0.2.0";
        public const string CoreLibGuid = "net.cucorelib";
    }

    [BepInPlugin(PluginInfo.Guid, PluginInfo.Name, PluginInfo.Version)]
    [BepInDependency(PluginInfo.CoreLibGuid)]
    public sealed class Plugin : BaseUnityPlugin
    {
        private bool itemsRegistered;

        private void Awake()
        {
            DrugRegistry.Register();
            new Harmony(PluginInfo.Guid).PatchAll(typeof(Plugin).Assembly);
            LocaleInjector.Inject();
            Logger.LogInfo($"{PluginInfo.Name}: registered {DrugRegistry.All.Count} liquids");
            TryRegisterItems();
        }

        private void Update()
        {
            if (itemsRegistered)
            {
                return;
            }
            TryRegisterItems();
        }

        private void TryRegisterItems()
        {
            if (!ItemCatalog.TryRegister())
            {
                return;
            }
            CustomRecipes.Register();
            LocaleInjector.Inject();
            itemsRegistered = true;
            Logger.LogInfo($"{PluginInfo.Name}: registered {ItemCatalog.All.Count} items and {CustomRecipes.Count} recipes");
        }
    }
}
