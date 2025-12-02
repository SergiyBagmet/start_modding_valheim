using HarmonyLib;
using UnityEngine;
using System.Linq;
using HelloWorldMod.Util;
using HelloWorldMod.UI;

namespace HelloWorldMod.Patches
{
    [HarmonyPatch(typeof(Player), "OnJump")]
    public static class PlayerJumpPatch
    {
        private static int jumpCount = 0;

        public static void Postfix(Player __instance)
        {
            if (__instance != Player.m_localPlayer)
                return;

            Skills skills = __instance.GetSkills();
            if (skills == null)
                return;

            var jumpSkill = skills
                .GetSkillList()
                .FirstOrDefault(s => s.m_info != null &&
                                     s.m_info.m_skill == Skills.SkillType.Jump);

            if (jumpSkill == null)
                return;

            float level = jumpSkill.m_level;
            float progress = jumpSkill.GetLevelPercentage();

            // Вызовы "UI-класса" (HudLegacy.ShowLeft стандарт вальхейма topleft)
            HudLegacy.ShowLeft($"Прыжок #{jumpCount}");

            /*
            HudLegacy.ShowCenter(
                $"Уровень: {level}\n" +
                $"{ProgressBar.Make(progress)}  {progress * 100:0}%"
            );
            */
            
            // Обновляем GUI HUD
            JumpGui.instance?.SetData(level, progress);

            Debug.Log($"[HelloWorldMod] Прыжок #{jumpCount} | уровень {level} | прогресс {progress:0.00}");
            jumpCount++;

        }
    }
}