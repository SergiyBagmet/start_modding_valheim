using UnityEngine;

namespace HelloWorldMod.UI
{
    /// <summary>
    /// Базовый класс: окно, которое можно двигать и ресайзить при зажатом ALT.
    /// </summary>
    public abstract class DraggableWindow : MonoBehaviour
    {
        // Прямоугольник окна (позиция + размер)
        protected Rect windowRect = new Rect(300, 300, 400, 80);

        // Режимы редактирования
        protected bool isEditing = false;
        protected bool isDragging = false;
        protected bool isResizing = false;
        protected Vector2 dragOffset;
        protected const int resizeZone = 16;

        // Включено ли вообще окно (можно переопределить в наследнике)
        protected virtual bool IsHudEnabled => true;

        // Нужно ли требовать, чтобы игрок был в мире
        protected virtual bool RequirePlayer => true;

        // Наследник должен реализовать отрисовку содержимого
        protected abstract void DrawWindow(Rect rect);

        //вызывает Юнити
        protected virtual void Update()
        {
            /// Клавиша, при зажатии которой включаем режим редактирования.
            isEditing = InputCursorManager.Instance != null &&
            InputCursorManager.Instance.IsEditingMode;
        }

        //метод для сохранения состояний
        protected virtual void AfterWindowChanged() {}

        //вызывает сама Юнити
        protected virtual void OnGUI()
        {
            if (!IsHudEnabled)
                return;

            if (RequirePlayer && Player.m_localPlayer == null)
                return;

            Event e = Event.current;

            // сначала обработка редактирования (мышь)
            if (isEditing)
            {
                HandleEditing(e);
            }

            // отрисовка только на Repaint
            if (e.type == EventType.Repaint)
            {
                DrawWindow(windowRect);
            }
        }

        private void HandleEditing(Event e)
        {
            Vector2 mouse = e.mousePosition;

            Rect resizeRect = new Rect(
                windowRect.xMax - resizeZone,
                windowRect.yMax - resizeZone,
                resizeZone,
                resizeZone
            );

            switch (e.type)
            {
                case EventType.MouseDown:
                    if (resizeRect.Contains(mouse))
                    {
                        isResizing = true;
                        e.Use();
                    }
                    else if (windowRect.Contains(mouse))
                    {
                        isDragging = true;
                        dragOffset = mouse - new Vector2(windowRect.x, windowRect.y);
                        e.Use();
                    }
                    break;

                case EventType.MouseUp:
                    isDragging = false;
                    isResizing = false;
                    break;

                case EventType.MouseDrag:
                    if (isDragging)
                    {
                        windowRect.x = mouse.x - dragOffset.x;
                        windowRect.y = mouse.y - dragOffset.y;
                        e.Use();
                        AfterWindowChanged();
                    }
                    else if (isResizing)
                    {
                        windowRect.width = Mathf.Max(150, mouse.x - windowRect.x);
                        windowRect.height = Mathf.Max(60, mouse.y - windowRect.y);
                        e.Use();
                        AfterWindowChanged();
                    }
                    break;
            }

            // Визуальная рамка и угол ресайза – показываем только в режиме редактирования
            if (e.type == EventType.Repaint)
            {
                GUI.color = new Color(1, 1, 0, 0.4f);
                GUI.DrawTexture(windowRect, Texture2D.whiteTexture);
                GUI.DrawTexture(resizeRect, Texture2D.whiteTexture);
                GUI.color = Color.white;
            }
        }
    }
}