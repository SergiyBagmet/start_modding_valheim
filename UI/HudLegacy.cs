
namespace HelloWorldMod.UI
{
    public static class HudLegacy
    {
        /// <summary>
        /// Показывает короткое сообщение слева сверху.
        /// </summary>
        public static void ShowLeft(string text)
        {
            MessageHud.instance?.ShowMessage(
                MessageHud.MessageType.TopLeft,
                text
            );
        }

        /// <summary>
        /// Показывает центральное большое сообщение.
        /// </summary>
        public static void ShowCenter(string text)
        {
            MessageHud.instance?.ShowMessage(
                MessageHud.MessageType.Center,
                text
            );
        }
    }
}