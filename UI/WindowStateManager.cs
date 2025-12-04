using BepInEx.Configuration;
using UnityEngine;

namespace HelloWorldMod.UI
{
    /// <summary>
    /// Управляет сохранением/загрузкой позиции и размера окна.
    /// Универсальный класс для любого HUD.
    /// </summary>
    public class WindowStateManager
    {
        private readonly ConfigEntry<float> cfgX;
        private readonly ConfigEntry<float> cfgY;
        private readonly ConfigEntry<float> cfgW;
        private readonly ConfigEntry<float> cfgH;

        public WindowStateManager(string windowName, Rect defaultRect)
        {
            cfgX = Plugin.Instance.Config.Bind(windowName, "X", defaultRect.x);
            cfgY = Plugin.Instance.Config.Bind(windowName, "Y", defaultRect.y);
            cfgW = Plugin.Instance.Config.Bind(windowName, "Width", defaultRect.width);
            cfgH = Plugin.Instance.Config.Bind(windowName, "Height", defaultRect.height);
        }

        public Rect Load()
        {
            return new Rect(cfgX.Value, cfgY.Value, cfgW.Value, cfgH.Value);
        }

        public void Save(Rect rect)
        {
            cfgX.Value = rect.x;
            cfgY.Value = rect.y;
            cfgW.Value = rect.width;
            cfgH.Value = rect.height;
        }
    }
}