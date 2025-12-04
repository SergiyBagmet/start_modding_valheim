/*using UnityEngine;

namespace HelloWorldMod.UI
{
    /// <summary>
    /// Управляет видимостью курсора и отключением камеры,
    /// когда ALT зажат (режим редактирования HUD).
    /// </summary>
    public class InputCursorManager : MonoBehaviour
    {
        public static InputCursorManager Instance;

        private bool lastState = false; // прошлое состояние (editing / not)

        private void Awake()
        {
            // простой синглтон, чтобы не плодить экземпляры
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Возвращает true если удерживается ALT.
        /// </summary>
        public bool IsEditing =>
            ZInput.GetKey(KeyCode.LeftAlt) ||
            ZInput.GetKey(KeyCode.RightAlt);

        private void Update()
        {
            bool editing = IsEditing;

            if (editing != lastState)
            {
                lastState = editing;

                if (editing)
                {
                    EnableCursor();
                }
                else
                {
                    DisableCursor();
                }
            }
        }

        private void EnableCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            if (GameCamera.instance != null)
                GameCamera.instance.enabled = false;
        }

        private void DisableCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            if (GameCamera.instance != null)
                GameCamera.instance.enabled = true;
        }
    }
}*/