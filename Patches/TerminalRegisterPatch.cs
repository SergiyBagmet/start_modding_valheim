using HarmonyLib;
using HelloWorldMod.UI.Menu;

namespace HelloWorldMod.Patches
{

    [HarmonyPatch(typeof(Chat), "InputText")]
    public static class ChatPatch
    {
        static bool Prefix(Chat __instance)
        {
            // Получаем текст из чата через рефлексию
            var inputField = __instance.GetType().GetField("m_input")?.GetValue(__instance);
            
            if (inputField != null)
            {
                // Получаем свойство text
                var textProperty = inputField.GetType().GetProperty("text");
                if (textProperty != null)
                {
                    string text = (string)textProperty.GetValue(inputField);
                    
                    if (text.StartsWith("/jumpm", System.StringComparison.OrdinalIgnoreCase))
                    {
                        // Переключаем HUD настроек вкл/выкл по команде /jumpm в чате
                        MenuManager.Instance.ToggleJumpMenu();

                        // Очищаем поле ввода
                        textProperty.SetValue(inputField, "");
                        
                        // Отменяем оригинальный метод
                        return false;
                    }
                }
            }
            
            return true;
        }
    }
}