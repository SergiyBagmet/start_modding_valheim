namespace HelloWorldMod.Util
{
    public static class ProgressBar
    {
        public static string Make(float pct, int size = 20)
        {
            int filled = (int)(pct * size);
            int empty = size - filled;

            return "[" + new string('=', filled) + new string('-', empty) + "]";
        }
    }
}