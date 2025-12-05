using HarmonyLib;

namespace HelloWorldMod.Patches
{
    [HarmonyPatch(typeof(Skills), nameof(Skills.RaiseSkill))]
    public static class JumpXpMultiplierPatch
    {
        // Коэффициент ускорения прокачки 
        private static float jumpXpMultiplier = 1f;

        public static void SetMultiplier(float value)
        {
            jumpXpMultiplier = value;
        }

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