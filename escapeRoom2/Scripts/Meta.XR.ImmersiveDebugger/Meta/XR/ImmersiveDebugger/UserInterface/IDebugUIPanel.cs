using Meta.XR.ImmersiveDebugger.Utils;

namespace Meta.XR.ImmersiveDebugger.UserInterface
{
	internal interface IDebugUIPanel
	{
		IInspector RegisterInspector(InstanceHandle instance, string category);

		void UnregisterInspector(InstanceHandle instance, string category, bool allCategories);

		IInspector GetInspector(InstanceHandle instance, string category);
	}
}
