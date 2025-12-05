using HelloWorldMod.Patches;
using UnityEngine;

namespace HelloWorldMod.UI
{
    public class InputCursorManager : MonoBehaviour
    {
        public static InputCursorManager Instance;

        private bool editingMode = false; // теперь мы сами управляем этим флагом
        private bool lastState = false;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Включение/выключение режима редактирования извне.
        /// </summary>
        public void SetEditing(bool enabled)
        {
            editingMode = enabled;
        }

        public bool IsEditingMode => editingMode;

        private void Update()
        {
            if (editingMode != lastState)
            {
                lastState = editingMode;

                if (editingMode)
                    EnableCursor();
                else
                    DisableCursor();
            }
        }

        private void EnableCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            if (GameCamera.instance != null)
                GameCamera.instance.enabled = false;
            
            //отключить управление игрока (вызвав фейк инвентарь)
            FakeInventory.Open();
        }

        private void DisableCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            if (GameCamera.instance != null)
                GameCamera.instance.enabled = true;

            //включить управление игрока (отмена фейк инвентарь)
            FakeInventory.Close();
        }
    }
}