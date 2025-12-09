using System;
using UnityEngine;

namespace HelloWorldMod.UI.Menu
{
    public enum WaitingFor
    {
        None,
        HudToggleKey,
        DragModifierKey
    }
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance;

        private JumpSettingsMenu jumpMenu;

        public bool AnyMenuOpen => jumpMenu != null && jumpMenu.IsOpen;

        // private bool isWaiting = false;
        private Action<KeyCode> onKeySelected;
        private WaitingFor waitingFor = WaitingFor.None;
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

        public bool IsWaiting() => waitingFor != WaitingFor.None;
        public WaitingFor WhatAreWeWaitingFor() => waitingFor;

        public void WaitForKey(WaitingFor what, Action<KeyCode> callback)
        {
            waitingFor = what;
            onKeySelected = callback;
        }

        public void FinishWaiting(KeyCode key)
        {
            waitingFor = WaitingFor.None;
            onKeySelected?.Invoke(key);
            onKeySelected = null;
        }

        public void ToggleJumpMenu()
        {
            if (jumpMenu.IsOpen)
                jumpMenu.Close();
            else
                jumpMenu.Open();
        }
    }
}