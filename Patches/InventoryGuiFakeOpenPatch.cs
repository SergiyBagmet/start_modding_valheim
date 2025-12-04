using HarmonyLib;
using HelloWorldMod.UI;

namespace HelloWorldMod.Patches
{
    [HarmonyPatch(typeof(InventoryGui), nameof(InventoryGui.IsVisible))]
    public static class FakeInventoryOpenPatch
    {
        static void Postfix(ref bool __result)
        {
            // Если режим редактирования включён → говорим игре, что инвентарь "видим"
            if (InputCursorManager.Instance != null &&
                InputCursorManager.Instance.IsEditingMode)
            {
                __result = true;
            }
        }
    }
}