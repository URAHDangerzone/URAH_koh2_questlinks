# URAH_QuestLinks

A BepInEx 5 plugin for **Knights of Honor II: Sovereign** that makes union quests easier to follow.
The quest window lists a union's goal provinces as plain text; URAH_QuestLinks turns each into
"Province (Town)" with the game's object link: hover shows the province tooltip, click selects it,
double-click looks at it. While the kingdom that blocks the union ("[union] must not exist") still
stands, its name is shown and linked too, so you can see whom to topple.

Needs no mod, works with any map and in vanilla. It only changes UI text, so players with and without
it can play together.

Setting in `BepInEx/config/urah.koh2.questlinks.cfg`: `[General] Enabled` switches the plugin on and off,
right away, no restart needed.

## Install

BepInEx 5 in the game folder, then `URAH_QuestLinks.dll` into `BepInEx/plugins/URAH_QuestLinks/`.

## Releases

Built and signed releases are in [URAH_koh2_releases](https://github.com/URAHDangerzone/URAH_koh2_releases/releases?q=URAH_QuestLinks&expanded=true): [`URAH_QuestLinks-v<version>`](https://github.com/URAHDangerzone/URAH_koh2_releases/releases?q=URAH_QuestLinks&expanded=true) is this plugin alone
(`BepInEx/plugins/URAH_QuestLinks/`), [`URAH_Starter`](https://github.com/URAHDangerzone/URAH_koh2_releases/releases?q=URAH_Starter&expanded=true) bundles BepInEx 5 with URAH_Loader, URAH_Net and
URAH_PluginManager, which installs this plugin from Settings → Plugins (URAH).
Every release is signed with the URAH release key; [URAH_koh2_releases](https://github.com/URAHDangerzone/URAH_koh2_releases) explains how to check it.

## Building

```sh
git clone --recursive <this repo>      # external/URAH_shared is a submodule
cd plugin && dotnet build -c Release -p:Deploy=true
```

The plugin compiles against your own game install with BepInEx 5 (`GameDir`, default: Steam on Linux).
