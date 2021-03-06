using HarmonyLib;
using RotA.Prefabs.Signals;
using System.Reflection;
using UnityEngine;

namespace RotA.Patches
{
    public static class PingMapIcon_Patch
    {
        [HarmonyPrefix]
        public static bool Prefix(object __instance)
        {
            FieldInfo field = __instance.GetType().GetField("ping");
            PingInstance ping = field.GetValue(__instance) as PingInstance;
            if (GenericSignalPrefab.registeredPingTypes.Contains(ping.pingType))
            {
                FieldInfo field2 = __instance.GetType().GetField("icon");
                uGUI_Icon icon = field2.GetValue(__instance) as uGUI_Icon;
                icon.sprite = SpriteManager.Get(SpriteManager.Group.Pings, "EggBase");
                icon.color = Color.black;
                RectTransform rectTransform = icon.rectTransform;
                rectTransform.sizeDelta = Vector2.one * 28f;
                rectTransform.localPosition = Vector3.zero;
                return false;
            }
            return true;
        }
    }
}
