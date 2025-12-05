using HarmonyLib;

namespace HelloWorldMod.Patches

{
    public static class FakeInventory

    {
        public static bool IsOpen { get; private set; }
        public static void Open()  => IsOpen = true;
        public static void Close() => IsOpen = false;
    }


    [HarmonyPatch(typeof(InventoryGui), nameof(InventoryGui.IsVisible))]
    public static class FakeInventoryOpenPatch
    {
        static void Postfix(ref bool __result)
        {
           if (FakeInventory.IsOpen)
                __result = true;
     
        }
    }
}