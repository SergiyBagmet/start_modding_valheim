using UnityEngine;
using HelloWorldMod.Patches;

namespace HelloWorldMod.UI.Menu
{
    public abstract class MenuBase : MonoBehaviour
    {
        protected Rect windowRect = new Rect(300, 200, 420, 260);

        public bool IsOpen { get; private set; } = false;

        public void Open()
        {
            IsOpen = true;
            FakeInventory.Open();
        }

        public virtual void SaveChangesToCfg()
        {
            
        }
        public void Close()
        {
            IsOpen = false;
            FakeInventory.Close();
            SaveChangesToCfg();
        }

        protected virtual void OnGUI()
        {
            if (!IsOpen) return;

            // 🔥 ЛОВИМ КНОПКИ ТУТ — в GUI, там где Event.current работает!
            if (MenuManager.Instance != null && MenuManager.Instance.IsWaiting())
            {
                Event e = Event.current;

                if (e.type == EventType.KeyUp && e.keyCode != KeyCode.None)
                {
                    MenuManager.Instance.FinishWaiting(e.keyCode);
                }
            }

            windowRect = GUI.Window(GetInstanceID(), windowRect, DrawWindow, "");
        }

        private void DrawWindow(int id)
        {
            RenderContents();
            GUI.DragWindow(new Rect(0, 0, windowRect.width, 25)); 
        }

        protected abstract void RenderContents();
    }
}