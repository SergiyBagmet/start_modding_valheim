using UnityEngine;
using HelloWorldMod.Patches; // для множителя XP
using HelloWorldMod.UI;     // чтобы управлять HUD

namespace HelloWorldMod.UI.Menu
{
    public class JumpSettingsMenu : MenuBase
    {
        private float multiplier = 1f;
        private string sliderText => $"{multiplier:0.0}x";

        private KeyCode hudToggleKey = KeyCode.F10;

        protected override void RenderContents()
        {
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
            GUI.Label(new Rect(20, y, 250, 25), $"Show HUD key: {hudToggleKey}");
            if (GUI.Button(new Rect(280, y, 120, 25), "Change"))
            {
                // позже добавим режим ожидания нажатия
            }

            y += 40;

            // Кнопка включить режим перетаскивания HUD
            if (GUI.Button(new Rect(20, y, 380, 35),
                "Enable HUD Drag Mode (hold + LMB)"))
            {
                InputCursorManager.Instance.SetEditing(true);
            }
        }
    }
}