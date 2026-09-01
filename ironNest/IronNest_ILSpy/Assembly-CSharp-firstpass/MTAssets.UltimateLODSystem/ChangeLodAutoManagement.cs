using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.UI;

namespace MTAssets.UltimateLODSystem;

public class ChangeLodAutoManagement : MonoBehaviour
{
	public Text buttonText;

	public Text explanation;

	private void Update()
	{
		//IL_009f: Expected I, but got O
		//IL_00af: Expected O, but got I
		//IL_00bf: Expected O, but got I
		if (UltimateLevelOfDetailGlobal.enableGlobalUlodSystem)
		{
			buttonText.text = "Disable Global Ultimate LOD System";
			explanation.text = "ULOD System is Enabled. Consult the documentation for more details on the feature of enabling/disabling the ULOD system while the game is running.";
		}
		if (!UltimateLevelOfDetailGlobal.enableGlobalUlodSystem)
		{
			buttonText.text = "Enable Global Ultimate LOD System";
			Text text = explanation;
			nint num = (nint)text;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v256 @ r8_v4 (Il2CppClass<UnityEngine.UI.Text>)+5E8]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v256 @ r8_v4 (Il2CppClass<UnityEngine.UI.Text>)+5F0]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v225 @ rax_v13 (should have been resolved before IL gen)");
		}
	}

	public void buttonEnableAutoManagement()
	{
		//IL_0027: Expected I, but got O
		nint num = (nint)typeof(UltimateLevelOfDetailGlobal);
		bool enableGlobalUlodSystem = !UltimateLevelOfDetailGlobal.enableGlobalUlodSystem;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v89 @ rcx_v6 (Il2CppClass<MTAssets.UltimateLODSystem.UltimateLevelOfDetailGlobal>)+E4]");
		if ((nint)0 == 0)
		{
			UltimateLevelOfDetailGlobal.enableGlobalUlodSystem = enableGlobalUlodSystem;
		}
		else
		{
			UltimateLevelOfDetailGlobal.enableGlobalUlodSystem = enableGlobalUlodSystem;
		}
	}
}
