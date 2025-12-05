using UnityEngine;

namespace HelloWorldMod.UI.Menu
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance;

        private JumpSettingsMenu jumpMenu;

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

        public void ToggleJumpMenu()
        {
            if (jumpMenu.IsOpen)
                jumpMenu.Close();
            else
                jumpMenu.Open();
        }
    }
}