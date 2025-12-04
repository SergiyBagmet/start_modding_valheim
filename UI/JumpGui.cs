using UnityEngine;

namespace HelloWorldMod.UI
{
    public class JumpGui : DraggableWindow
    {
        public static JumpGui instance;

        //хранение и выгрузка состояний
        private WindowStateManager state;
        private bool loadedValues = false;

        public float level = 0f;
        public float progress = 0f;

        public static bool hudEnabled = true;

        // открыто ли редактирование – нужно, если потом будем патчить ZInput.UpdateCursorVisible
        public bool IsEditingMode => isEditing;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;

             // 1. Создаём менеджер состояния, передаём дефолтный windowRect
            state = new WindowStateManager("JumpHUD", windowRect);

            // 2. Грузим сохранённые значения из конфига
            windowRect = state.Load();
            DontDestroyOnLoad(gameObject);
        }

        protected override void Update()
        {   //грузим доп параметры с конфига 1 раз меняя флаг после плеера
            //по идеи можно прописать в Awake() без флага
            if (!loadedValues && Player.m_localPlayer != null)
            {
                loadedValues = true;

                level = state.LoadValue("level", level);
                progress = state.LoadValue("progress", progress);
            }
        }

        protected void OnDestroy()
        {
            // 3. На всякий случай сохраняем перед уничтожением
            state?.Save(windowRect);
        }

        protected override void AfterWindowChanged()  
        {
            state?.Save(windowRect);
        }
        // говорим базовому классу, когда окно действительно нужно показывать
        protected override bool IsHudEnabled => hudEnabled;

        protected override bool RequirePlayer => true;

        // Сюда переехал старый DrawHud, только теперь принимает Rect
        protected override void DrawWindow(Rect rect)
        {
            float x = rect.x;
            float y = rect.y;
            float width = rect.width;
            float height = rect.height;

            // фон
            GUI.color = new Color(0, 0, 0, 0.55f);
            GUI.DrawTexture(rect, Texture2D.whiteTexture);

            // стиль текста
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.fontSize = 24;
            style.alignment = TextAnchor.MiddleCenter;
            GUI.color = Color.white;

            // текст уровня
            GUI.Label(new Rect(x, y + 5f, width, 30f),
                $"Jump — {level:0} lvl", style);

            float barWidth = width - 40f;
            float barHeight = 20f;
            float bx = x + 20f;
            float by = y + height - barHeight - 10f;

            // фон полоски
            GUI.color = new Color(1, 1, 1, 0.2f);
            GUI.DrawTexture(new Rect(bx, by, barWidth, barHeight), Texture2D.whiteTexture);

            // заполнение
            GUI.color = new Color(0.3f, 0.8f, 1f, 0.9f);
            GUI.DrawTexture(new Rect(bx, by, barWidth * progress, barHeight), Texture2D.whiteTexture);

            // процент
            GUI.color = Color.white;
            GUIStyle pctStyle = new GUIStyle(GUI.skin.label);
            pctStyle.fontSize = 16;
            pctStyle.alignment = TextAnchor.MiddleCenter;
            GUI.Label(new Rect(bx, by, barWidth, barHeight), $"{progress * 100:0}%", pctStyle);

            GUI.color = Color.white;
        }

        public void SetData(float lvl, float pct)
        {
            level = lvl;
            progress = pct;

            //сохраняем значение в конфиг через WindowStateManager
            state?.SaveValue("level", level);
            state?.SaveValue("progress", progress);
        }
    }
}