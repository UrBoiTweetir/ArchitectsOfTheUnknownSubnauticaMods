using System.Collections.Generic;
using ArchitectsLibrary.Interfaces;
using HarmonyLib;
using UnityEngine;

namespace ArchitectsLibrary.Patches
{
    [HarmonyPatch(typeof(SeaMoth))]
    public class SeaMothPatches
    {
        internal static Dictionary<TechType, ISeaMothOnUse> seaMothOnUses = new();
        internal static Dictionary<TechType, ISeaMothOnEquip> SeaMothOnEquips = new();
        
        [HarmonyPatch(nameof(SeaMoth.OnUpgradeModuleUse))]
        [HarmonyPostfix]
        static void OnUpgradeModuleUse_Postfix(SeaMoth __instance, TechType techType, int slotID)
        {
            if (seaMothOnUses.TryGetValue(techType, out ISeaMothOnUse seaMothOnUse))
            {
                seaMothOnUse.OnUpgradeUse(slotID, __instance);
                
                __instance.quickSlotTimeUsed[slotID] = Time.time;
                __instance.quickSlotCooldown[slotID] = seaMothOnUse.Cooldown;
            }
        }

        [HarmonyPatch(nameof(SeaMoth.OnUpgradeModuleChange))]
        [HarmonyPostfix]
        static void OnUpgradeModuleChange(SeaMoth __instance, TechType techType, int slotID)
        {
            if (SeaMothOnEquips.TryGetValue(techType, out ISeaMothOnEquip seaMothOnEquip))
            {
                seaMothOnEquip.OnEquip(slotID, __instance);
            }
        }
    }
}