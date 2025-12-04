using BepInEx.Configuration;
using System.Collections.Generic;
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

        // секция в конфиге
        private readonly string section;

        // кэш динамических значений
        private readonly Dictionary<string, ConfigEntry<string>> extra
            = new Dictionary<string, ConfigEntry<string>>();

        public WindowStateManager(string windowName, Rect defaultRect)
        {
            section = windowName;

            cfgX = Plugin.Instance.Config.Bind(section, "X", defaultRect.x);
            cfgY = Plugin.Instance.Config.Bind(section, "Y", defaultRect.y);
            cfgW = Plugin.Instance.Config.Bind(section, "Width", defaultRect.width);
            cfgH = Plugin.Instance.Config.Bind(section, "Height", defaultRect.height);
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

        // -------------------------
        // ДОПОЛНИТЕЛЬНЫЕ ЗНАЧЕНИЯ
        // -------------------------
        public void SaveValue(string key, object value)
        {
            if (!extra.ContainsKey(key))
            {
                extra[key] = Plugin.Instance.Config.Bind(section, key, value.ToString());
            }

            extra[key].Value = value.ToString(); // живут локально из за 
            Plugin.Instance.Config.Save(); // сохраняем в конфиг
        }

        public T LoadValue<T>(string key, T defaultValue)
        {
            if (!extra.ContainsKey(key))
            {
                extra[key] = Plugin.Instance.Config.Bind(section, key, defaultValue.ToString());
            }

            string raw = extra[key].Value;

            try
            {
                return (T)System.Convert.ChangeType(raw, typeof(T));
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}