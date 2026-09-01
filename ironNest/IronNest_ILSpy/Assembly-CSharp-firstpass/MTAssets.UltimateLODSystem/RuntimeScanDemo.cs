using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MTAssets.UltimateLODSystem;

public class RuntimeScanDemo : MonoBehaviour
{
	public UltimateLevelOfDetail ulodOfScene;

	public Text buttonText;

	public GameObject buttonObj;

	public Text scanStatus;

	public Animator cameraAnimator;

	private void Start()
	{
		UltimateLevelOfDetail ultimateLevelOfDetail = ulodOfScene;
		UnityAction call = delegate
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39E43]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			scanStatus.text = "Scan Done! Showing LOD Demo";
			cameraAnimator.SetBool("runLoop", value: true);
			buttonObj.SetActive(value: true);
		};
		ultimateLevelOfDetail.onDoneScan.AddListener(call);
		UltimateLevelOfDetail ultimateLevelOfDetail2 = ulodOfScene;
		UnityAction call2 = delegate
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39E44]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			scanStatus.text = "No Scan Performed Yet";
			cameraAnimator.SetBool("runLoop", value: false);
			buttonObj.SetActive(value: true);
		};
		ultimateLevelOfDetail2.onUndoScan.AddListener(call2);
	}

	private void Update()
	{
		//IL_00a4: Expected I, but got O
		//IL_00b4: Expected O, but got I
		//IL_00c4: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39E41]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (ulodOfScene.isMeshesCurrentScannedAndLodsWorkingInThisComponent())
		{
			buttonText.text = "Undo Current Scan And Delete Generated LODs";
		}
		if (!ulodOfScene.isMeshesCurrentScannedAndLodsWorkingInThisComponent())
		{
			Text text = buttonText;
			nint num = (nint)text;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v154 @ r8_v3 (Il2CppClass<UnityEngine.UI.Text>)+5E8]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v154 @ r8_v3 (Il2CppClass<UnityEngine.UI.Text>)+5F0]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v126 @ rax_v7 (should have been resolved before IL gen)");
		}
	}

	public void StartUndoScan()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39E42]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (!ulodOfScene.isMeshesCurrentScannedAndLodsWorkingInThisComponent())
		{
			if (!ulodOfScene.isMeshesCurrentScannedAndLodsWorkingInThisComponent())
			{
				scanStatus.text = "Scanning...";
				buttonObj.SetActive(value: false);
				ulodOfScene.ScanAllMeshesAndGenerateLodsGroups();
			}
		}
		else
		{
			scanStatus.text = "Undoing Scan...";
			buttonObj.SetActive(value: false);
			ulodOfScene.UndoCurrentScanWorkingAndDeleteGeneratedMeshes(runMonoIl2CppGc: true, runUnityGc: true);
		}
	}

	private void _003CStart_003Eb__5_0()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39E43]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		scanStatus.text = "Scan Done! Showing LOD Demo";
		cameraAnimator.SetBool("runLoop", value: true);
		buttonObj.SetActive(value: true);
	}

	private void _003CStart_003Eb__5_1()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39E44]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		scanStatus.text = "No Scan Performed Yet";
		cameraAnimator.SetBool("runLoop", value: false);
		buttonObj.SetActive(value: true);
	}
}
