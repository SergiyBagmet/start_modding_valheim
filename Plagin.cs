using BepInEx;
using HarmonyLib;
using UnityEngine;
using HelloWorldMod.UI;
using HelloWorldMod.UI.Menu;

namespace HelloWorldMod
{
    [BepInPlugin("com.serge.helloworld", "Hello World Mod", "1.0.1")]
    public class Plugin : BaseUnityPlugin
    {
        private Harmony _harmony;
        public static Plugin Instance; 

        private void Awake()
        {
            Instance = this;
            Logger.LogInfo("[HelloWorldMod] Мод загружен!");

            _harmony = new Harmony("com.serge.helloworld");
            _harmony.PatchAll(); // активировать все патчи

            // создаём GUI HUD
            new GameObject("JumpGui").AddComponent<JumpGui>();
            // подключаем контроллеры
            new GameObject("InputCursorManager").AddComponent<InputCursorManager>();
            new GameObject("HudEditModeController").AddComponent<HudEditModeController>();
            new GameObject("MenuManager").AddComponent<MenuManager>();        
        }

        private void Update()
        {
            /*if (ZInput.GetKeyDown(KeyCode.F9)) // например F9
            {
                JumpGui.hudEnabled = !JumpGui.hudEnabled;
                Logger.LogInfo("HUD toggled: " + JumpGui.hudEnabled);
            }*/
        }

        private void OnDestroy()
        {
            _harmony?.UnpatchSelf();
        }
    }
}