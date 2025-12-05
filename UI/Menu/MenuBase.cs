using UnityEngine;
using HelloWorldMod.Patches;

namespace HelloWorldMod.UI.Menu
{
    public abstract class MenuBase : MonoBehaviour
    {
        protected Rect windowRect = new Rect(300, 200, 420, 260);

        protected bool dragging = false;
        protected Vector2 dragOffset;

        public bool IsOpen { get; private set; } = false;

        public void Open()
        {
            IsOpen = true;
            FakeInventory.Open();
            //Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;
        }

        public void Close()
        {
            IsOpen = false;
            FakeInventory.Close();
            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
        }

        protected virtual void OnGUI()
        {
            if (!IsOpen) return;

            windowRect = GUI.Window(GetInstanceID(), windowRect, DrawWindow, "");
        }

        private void DrawWindow(int id)
        {
            HandleDrag();
            RenderContents();
            GUI.DragWindow(new Rect(0, 0, windowRect.width, 25)); // draggable bar
        }

        protected abstract void RenderContents();

        private void HandleDrag()
        {
            // handled by GUI.DragWindow, оставляем пустым
        }
    }
}