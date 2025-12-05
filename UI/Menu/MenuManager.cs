using System;
using UnityEngine;

namespace HelloWorldMod.UI.Menu
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance;

        private JumpSettingsMenu jumpMenu;

        private bool isWaiting = false;
        private Action<KeyCode> onKeySelected;

        public KeyCode hudToggleKey = KeyCode.F9;
        public KeyCode dragModifier = KeyCode.LeftAlt;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            jumpMenu = gameObject.AddComponent<JumpSettingsMenu>();
        }

        public void WaitForKey(Action<KeyCode> callback)
        {
            isWaiting = true;
            onKeySelected = callback;
        }

        public void FinishWaiting(KeyCode key)
        {
            isWaiting = false;
            onKeySelected?.Invoke(key);
            onKeySelected = null;
        }

        public bool IsWaiting() => isWaiting;

        public void ToggleJumpMenu()
        {
            if (jumpMenu.IsOpen)
                jumpMenu.Close();
            else
                jumpMenu.Open();
        }
    }
}