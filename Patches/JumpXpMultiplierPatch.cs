using HarmonyLib;
using UnityEngine;

namespace HelloWorldMod.Patches
{
    [HarmonyPatch(typeof(Skills), nameof(Skills.RaiseSkill))]
    public static class JumpXpMultiplierPatch
    {
        // Коэффициент ускорения прокачки (можешь менять)
        private static float jumpXpMultiplier = 10f;

        static void Prefix(Skills __instance, Skills.SkillType skillType, ref float factor)
        {
            if (skillType == Skills.SkillType.Jump)
            {
                factor *= jumpXpMultiplier;
                // Debug.Log($"Jump XP boosted! New factor = {factor}");
            }
        }
    }
}