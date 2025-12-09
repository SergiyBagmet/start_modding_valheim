using UnityEngine;
using HelloWorldMod.Patches; // для множителя XP
using HelloWorldMod.UI;     // чтобы управлять HUD

namespace HelloWorldMod.UI.Menu
{
    public class JumpSettingsMenu : MenuBase
    {
        private float multiplier = 1f;
        private string sliderText => $"{multiplier:0.0}x";
        
        private WindowStateManager state;
        private KeyCode hudKey;
        private KeyCode dragKey;

        private void Start()
        {
            state = new WindowStateManager("JumpSettingsMenu", windowRect);

            // грузим начальные настройки или по ключу секции сохраненные значения
            windowRect = state.Load();
            multiplier = state.LoadValue("JumpMultiplier", 1f);
            hudKey = (KeyCode)state.LoadValue("HudToggleKey", (int)KeyCode.F9);
            dragKey =(KeyCode)state.LoadValue("HudToggleKey", (int)KeyCode.LeftAlt);

            // применяем в игру
            JumpXpMultiplierPatch.SetMultiplier(multiplier);
            MenuManager.Instance.hudToggleKey = hudKey;
            MenuManager.Instance.dragModifier = dragKey;
        }
        protected override void RenderContents()
        {
            // Фон окна
            GUI.color = new Color(0, 0, 0, 0.6f); 
            GUI.Box(windowRect, GUIContent.none);
            GUI.color = Color.white;

            float y = 10f;

            // Заголовок
            GUIStyle title = new GUIStyle(GUI.skin.label)
            {
                fontSize = 20,
                alignment = TextAnchor.MiddleCenter
            };
            GUI.Label(new Rect(0, y, windowRect.width, 30), "Jump Settings", title);

            // X-КНОПКА
            if (GUI.Button(new Rect(windowRect.width - 40, 10, 30, 30), "X"))
            {
                Close();
            }

            y += 50;

            // Слайдер множителя XP
            GUI.Label(new Rect(20, y, 200, 25), "XP Multiplier:");
            multiplier = GUI.HorizontalSlider(new Rect(150, y + 5, 200, 20), multiplier, 0.1f, 10f);
            GUI.Label(new Rect(360, y, 60, 25), sliderText);

            // применяем к патчу
            JumpXpMultiplierPatch.SetMultiplier(multiplier);

            y += 50;

            // Бинд отображения HUD окна
            GUI.Label(new Rect(20, y, 250, 25), $"Show HUD key: {MenuManager.Instance.hudToggleKey}");
            if (MenuManager.Instance.WhatAreWeWaitingFor() != WaitingFor.HudToggleKey)
            {
                if (GUI.Button(new Rect(280, y, 120, 25), "Change"))
                {
                    MenuManager.Instance.WaitForKey(WaitingFor.HudToggleKey,key =>
                    {
                        MenuManager.Instance.hudToggleKey = key;
                    });
                }
            }
            else GUI.Label(new Rect(280, y, 120, 25), "Press key...");

            y += 40;

            // Кнопка включить для бинда - режим перетаскивания HUD

            GUI.Label(new Rect(20, y, 250, 25), $"Show dragKey: {MenuManager.Instance.dragModifier} + LMB");
            if (MenuManager.Instance.WhatAreWeWaitingFor() != WaitingFor.DragModifierKey)
            {
                if (GUI.Button(new Rect(280, y, 120, 25), "Change"))
                {
                    MenuManager.Instance.WaitForKey(WaitingFor.DragModifierKey,key =>
                    {
                        MenuManager.Instance.dragModifier = key;
                    });
                }
            }
            else GUI.Label(new Rect(280, y, 120, 25), "Press key...");
          


            y += 50;

            // DEFAULTS
            if (GUI.Button(new Rect(20, y, 120, 35), "Reset to Defaults"))
            {
                multiplier = 1f;
                MenuManager.Instance.hudToggleKey = KeyCode.F9;
                MenuManager.Instance.dragModifier = KeyCode.LeftAlt;
            }
        }

        public override void SaveChangesToCfg()
        {
            // сохраняем
            state.SaveValue("JumpMultiplier", multiplier);
            state.SaveValue("HudToggleKey", (int)hudKey);
            state.SaveValue("dragModifierKey", (int)dragKey);
            state.Save(windowRect);
        }
    }
}