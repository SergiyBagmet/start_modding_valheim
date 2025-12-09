using UnityEngine;
using HelloWorldMod.UI.Menu;

namespace HelloWorldMod.UI
{
    /// <summary>
    /// Отвечает за то, *когда* включается режим редактирования.
    /// Не управляет курсором, не рисует HUD — только решает логически.
    /// </summary>
    public class HudEditModeController : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            
            bool editing = false;

            // 1. классическое — ALT
            editing |= ZInput.GetKey(MenuManager.Instance.dragModifier);
            InputCursorManager.Instance?.SetEditing(editing);
        }
    }
}