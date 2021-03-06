using HarmonyLib;
using UnityEngine;

namespace RotA.Patches
{
    [HarmonyPatch]
    public static class InspectOnFirstPickup_Patches
    {
        [HarmonyPatch(typeof(InspectOnFirstPickup), nameof(InspectOnFirstPickup.Start))]
        [HarmonyPostfix]
        public static void InspectOnFirstPickup_Start_Patch(InspectOnFirstPickup __instance)
        {
            TechTag techTag = __instance.GetComponent<TechTag>();
            if (techTag == null)
            {
                return;
            }
            if (techTag.type == TechType.PrecursorKey_White || techTag.type == TechType.PrecursorKey_Red)
            {
                BoxCollider boxCol = __instance.pickupAble.GetComponent<BoxCollider>();
                if (boxCol)
                {
                    boxCol.isTrigger = false;
                }
            }
        }

        [HarmonyPatch(typeof(InspectOnFirstPickup), nameof(InspectOnFirstPickup.OnInspectObjectBegin))]
        [HarmonyPostfix]
        public static void InspectOnFirstPickup_InspectBegin_Patch(InspectOnFirstPickup __instance)
        {
            TechTag techTag = __instance.GetComponent<TechTag>();
            if (techTag == null)
            {
                return;
            }
            if (techTag.type == Mod.gargEgg.TechType)
            {
                __instance.gameObject.transform.localScale = Vector3.one * 0.25f / 1.2f;
            }
            if (techTag.type == TechType.PrecursorKey_Red)
            {
                __instance.gameObject.transform.localScale = Vector3.one;
            }
        }

        [HarmonyPatch(typeof(InspectOnFirstPickup), nameof(InspectOnFirstPickup.OnInspectObjectDone))]
        [HarmonyPostfix]
        public static void InspectOnFirstPickup_InspectEnd_Patch(InspectOnFirstPickup __instance)
        {
            TechTag techTag = __instance.GetComponent<TechTag>();
            if (techTag == null)
            {
                return;
            }
            if (techTag.type == Mod.gargEgg.TechType)
            {
                __instance.gameObject.transform.localScale = Vector3.one;
            }
        }
    }
}
