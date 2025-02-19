﻿using HarmonyLib;
using KerbalConstructionTime;
using KSP.Localization;
using KSP.UI.TooltipTypes;
using RP0.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RP0.Harmony
{
    [HarmonyPatch(typeof(ScienceWidget))]
    internal class PatchScienceWidget
    {
        [HarmonyPostfix]
        [HarmonyPatch("DelayedStart")]
        internal static void Postfix_DelayedStart(FundsWidget __instance)
        {
            var tooltip = __instance.gameObject.AddComponent<TooltipController_TextFunc>();
            var prefab = AssetBase.GetPrefab<Tooltip_Text>("Tooltip_Text");
            tooltip.prefab = prefab;
            tooltip.getStringAction = GetTooltipText;
            tooltip.continuousUpdate = true;
        }

        private static string GetTooltipText()
        {
            return Localizer.Format("#rp0_Widgets_Science_Tooltip",
                                    System.Math.Max(0, KerbalConstructionTimeData.Instance.SciPointsTotal).ToString("N1"),
                                    UnlockCreditHandler.Instance.TotalCredit.ToString("N0"));
        }
    }
}
