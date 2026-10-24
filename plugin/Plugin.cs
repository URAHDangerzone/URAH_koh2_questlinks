using System;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace URAH_QuestLinks
{
    /// <summary>
    /// Makes the goal provinces of union quests clickable in the quest window (QuestGoalLinkPatch).
    /// No ModLink: it only rewrites UI text and changes no game data, so it works with any map and mod
    /// list, vanilla included, and players with and without it can play together. [General] Enabled
    /// switches it on and off at runtime (the URAH_PluginManager convention): the patch comes off, and
    /// the quest window shows plain text again the next time it fills its conditions.
    ///
    /// Patching UIQuestsInfoWindow at load is safe: it is a UI class with no static constructor that
    /// reads defs, settings or the ModManager (unlike UserSettings, see URAH_Zoom).
    /// </summary>
    [BepInPlugin(Guid, Name, Version)]
    public class Plugin : BaseUnityPlugin
    {
        public const string Guid = "urah.koh2.questlinks";
        public const string Name = "URAH_QuestLinks";
        public const string Version = URAH.BuildVersion.Value; // plugin/version.txt

        internal static ManualLogSource Log;

        private ConfigEntry<bool> enabledSetting;
        private Harmony harmony;
        private bool active;

        private void Awake()
        {
            Log = Logger;
            enabledSetting = Config.Bind("General", "Enabled", true,
                "Switch the plugin on and off (takes effect right away)");
            harmony = new Harmony(Guid);
            enabledSetting.SettingChanged += (sender, e) => Apply();
            Apply();
            Log.LogInfo($"{Name} {Version} loaded, {(enabledSetting.Value ? "enabled" : "disabled")}");
        }

        /// <summary>Patch on while Enabled is on, off otherwise.</summary>
        private void Apply()
        {
            bool want = enabledSetting.Value;
            if (want == active)
            {
                return;
            }
            active = want;
            if (want)
            {
                Patch(harmony, typeof(QuestGoalLinkPatch));
            }
            else
            {
                harmony.UnpatchSelf();
            }
            Log.LogInfo($"Union goal links {(want ? "patched" : "unpatched")}");
        }

        /// <summary>Each patch on its own, so one that no longer fits the game does not take the others with it.</summary>
        private static void Patch(Harmony harmony, Type patch)
        {
            try
            {
                harmony.CreateClassProcessor(patch).Patch();
            }
            catch (Exception e)
            {
                Log.LogError($"Patch {patch.Name} failed: {e}");
            }
        }
    }
}
