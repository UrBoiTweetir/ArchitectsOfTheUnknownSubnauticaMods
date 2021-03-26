﻿using System.Reflection;
using HarmonyLib;
using QModManager.API.ModLoading;
using QModManager.Utility;

namespace ArchitectsLibrary
{
    [QModCore]
    public static class Main
    {
        private static Assembly myAssembly = Assembly.GetExecutingAssembly();

        [QModPatch]
        public static void Load()
        {
            Logger.Log(Logger.Level.Info, "ArchitectsLibrary started Patching.");
            
            Initializer.PatchAllDictionaries();
            
            Harmony.CreateAndPatchAll(myAssembly, $"ArchitectsOfTheUnknown_{myAssembly.GetName().Name}");
            
            Logger.Log(Logger.Level.Info, "ArchitectsLibrary successfully finished Patching!");
        }
        
    }
}