using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;

namespace Kamgam.SettingsGenerator;

public static class RenderUtils
{
	public enum RenderPipe
	{
		BuiltIn,
		URP,
		HDRP
	}

	private static Camera[] _tmpAllCameras;

	public static RenderPipe GetCurrentRenderPipeline()
	{
		//IL_0157: Expected I4, but got O
		//IL_0126: Expected O, but got I4
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Expected I4, but got Unknown
		RenderPipelineAsset currentRenderPipeline = GraphicsSettings.currentRenderPipeline;
		bool flag = currentRenderPipeline == null;
		bool flag2 = !flag;
		RenderPipelineAsset renderPipelineAsset = currentRenderPipeline;
		if (!flag2)
		{
			RenderPipelineAsset defaultRenderPipeline = GraphicsSettings.defaultRenderPipeline;
			renderPipelineAsset = defaultRenderPipeline;
		}
		if (renderPipelineAsset != null)
		{
			if ((object)renderPipelineAsset != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
				object obj = default(object);
				if (obj != null)
				{
					object obj2 = obj;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v210 @ rdx_v5+1B8] (should have been resolved before IL gen)");
					string text = default(string);
					if (text == "UniversalRenderPipelineAsset")
					{
						return RenderPipe.URP;
					}
					bool flag3 = text == "HDRenderPipelineAsset";
					object obj3 = 0 - (flag3 ? 1 : 0);
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sbb eax,eax\"");
					return (RenderPipe)(obj3 & 2);
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (RenderPipe)ex;
		}
		return RenderPipe.BuiltIn;
	}

	public unsafe static int GetAllCameras(out Camera[] cameras)
	{
		//IL_0034: Expected O, but got I4
		//IL_0056: Expected O, but got I4
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Expected O, but got Unknown
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Expected O, but got Unknown
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Expected O, but got Unknown
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Expected O, but got Unknown
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Expected O, but got Unknown
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Expected O, but got Unknown
		//IL_0149: Expected O, but got I4
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Expected O, but got Unknown
		//IL_0173: Expected I4, but got O
		int allCamerasCount = Camera.GetAllCamerasCount();
		Camera[] tmpAllCameras = _tmpAllCameras;
		if (allCamerasCount > tmpAllCameras.Length)
		{
			object obj = allCamerasCount + 5;
			Camera[] tmpAllCameras2 = new Camera[obj];
			_tmpAllCameras = tmpAllCameras2;
		}
		int allCameras = Camera.GetAllCameras(_tmpAllCameras);
		Camera[] tmpAllCameras3 = _tmpAllCameras;
		bool flag = (nint)_tmpAllCameras < 0;
		object obj2 = tmpAllCameras3.Length - 1;
		if (!flag)
		{
			object obj3 = obj2 * 8;
			object obj4 = obj3 + 32;
			object obj5 = obj2;
			object obj8;
			do
			{
				object obj6 = obj5 - allCamerasCount;
				bool flag2 = (nint)obj6 < 0;
				if ((nint)obj5 >= allCamerasCount)
				{
					Camera[] tmpAllCameras4 = _tmpAllCameras;
					object obj7 = obj2 - tmpAllCameras4.Length;
					flag2 = (nint)obj7 < 0;
					if ((nint)obj2 >= tmpAllCameras4.Length)
					{
						IndexOutOfRangeException ex = new IndexOutOfRangeException();
						return (int)ex;
					}
					_ = 0;
				}
				obj5--;
				obj4 -= 8;
				obj2--;
				obj8 = !flag2;
			}
			while (obj8 != null);
		}
		ref Camera[] reference = ref *(Camera[]*)_tmpAllCameras;
		return allCamerasCount;
	}

	public static Camera GetCurrentRenderingCamera(bool checkForMarker)
	{
		//IL_003c: Expected O, but got I4
		//IL_0213: Expected O, but got I4
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Expected O, but got Unknown
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected O, but got Unknown
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Expected O, but got Unknown
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Expected O, but got Unknown
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Expected O, but got Unknown
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_045d: Expected O, but got Unknown
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Expected O, but got Unknown
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Expected O, but got Unknown
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Expected O, but got Unknown
		//IL_00b6: Expected O, but got I
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a6: Expected O, but got Unknown
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Expected O, but got Unknown
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c2: Expected O, but got Unknown
		//IL_029e: Expected O, but got I
		//IL_00f3: Expected O, but got I
		//IL_02db: Expected O, but got I
		//IL_02f7: Invalid comparison between F4 and I4
		//IL_032d: Expected O, but got I
		//IL_037e: Expected O, but got I
		//IL_03a0: Invalid comparison between F4 and I4
		//IL_03af: Invalid comparison between O and F4
		//IL_03d5: Expected O, but got I
		//IL_03f7: Invalid comparison between F4 and I4
		//IL_0406: Invalid comparison between O and F4
		//IL_042c: Expected O, but got I
		//IL_0449: Expected O, but got I
		bool flag = !checkForMarker;
		int num = 0;
		Camera[] cameras = null;
		if (!flag)
		{
			int allCameras = GetAllCameras(out cameras);
			bool flag2 = 0 < 0;
			Behaviour behaviour = (Behaviour)(cameras.Length - 1);
			num = allCameras;
			if (!flag2)
			{
				object obj = behaviour * 8;
				object obj2 = obj + 32;
				Behaviour behaviour2 = behaviour;
				Camera[] array = null;
				while (true)
				{
					bool flag4;
					if ((nint)behaviour2 < allCameras)
					{
						if ((nint)behaviour >= array.Length)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v192 @ rdi_v13+v217 @ rcx_v32 (UnityEngine.Camera[])]");
						bool flag3 = ((Component)0).TryGetComponent<SettingsMainCameraMarker>(out var _);
						flag4 = (flag3 ? 1 : 0) < (false ? 1 : 0);
						if (flag3)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v192 @ rdi_v13+v217 @ rcx_v32 (UnityEngine.Camera[])]");
							return (Camera)0;
						}
					}
					else
					{
						object obj3 = behaviour - array.Length;
						flag4 = (nint)obj3 < 0;
						if ((nint)behaviour >= array.Length)
						{
							break;
						}
						_ = 0;
					}
					behaviour2 = (Behaviour)(behaviour2 - 1);
					obj2 -= 8;
					behaviour = (Behaviour)(behaviour - 1);
					num = allCameras;
					if (!flag4)
					{
						array = null;
						continue;
					}
					goto IL_018c;
				}
				goto IL_0511;
			}
		}
		goto IL_018c;
		IL_04dd:
		Camera result;
		return result;
		IL_018c:
		Camera main = Camera.main;
		bool flag5 = main == null;
		bool flag6 = !flag5;
		result = main;
		if (!flag6)
		{
			Camera[] array2 = null;
			if (!checkForMarker)
			{
				int allCameras2 = GetAllCameras(out var cameras2);
				num = allCameras2;
				array2 = cameras2;
			}
			bool flag7 = (nint)array2 < 0;
			object obj4 = array2.Length - 1;
			result = main;
			if (!flag7)
			{
				object obj5 = obj4 * 8;
				object obj6 = obj5 + 32;
				float num2 = -3.4028235E+38f;
				object obj7 = obj4;
				result = main;
				Camera[] array3 = array2;
				object obj8 = default(object);
				while (true)
				{
					bool flag8;
					if ((nint)obj7 < num)
					{
						if ((nint)obj4 >= array3.Length)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v198 @ rsi_v9+v218 @ rcx_v12 (UnityEngine.Camera[])]");
						bool isActiveAndEnabled = ((Behaviour)0).isActiveAndEnabled;
						flag8 = (isActiveAndEnabled ? 1 : 0) < (false ? 1 : 0);
						if (isActiveAndEnabled)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v198 @ rsi_v9+v218 @ rcx_v12 (UnityEngine.Camera[])]");
							float depth = ((Camera)0).depth;
							float num3 = depth - num2;
							flag8 = num3 < 0f;
							if (depth > num2)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v198 @ rsi_v9+v218 @ rcx_v12 (UnityEngine.Camera[])]");
								RenderTexture targetTexture = ((Camera)0).targetTexture;
								bool flag9 = targetTexture == null;
								flag8 = (flag9 ? 1 : 0) < (false ? 1 : 0);
								if (flag9)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v198 @ rsi_v9+v218 @ rcx_v12 (UnityEngine.Camera[])]");
									Rect rect = ((Camera)0).rect;
									float num4 = (float)obj8 - 1f;
									flag8 = num4 < 0f;
									if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj8) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1f))
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v198 @ rsi_v9+v218 @ rcx_v12 (UnityEngine.Camera[])]");
										Rect rect2 = ((Camera)0).rect;
										float num5 = (float)obj8 - 1f;
										flag8 = num5 < 0f;
										if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj8) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1f))
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v198 @ rsi_v9+v218 @ rcx_v12 (UnityEngine.Camera[])]");
											depth = ((Camera)0).depth;
											num2 = depth;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v198 @ rsi_v9+v218 @ rcx_v12 (UnityEngine.Camera[])]");
											result = (Camera)0;
										}
									}
								}
							}
						}
					}
					else
					{
						object obj9 = obj4 - array3.Length;
						flag8 = (nint)obj9 < 0;
						if ((nint)obj4 >= array3.Length)
						{
							break;
						}
						_ = 0;
					}
					obj7--;
					obj6 -= 8;
					obj4--;
					if (!flag8)
					{
						array3 = array2;
						continue;
					}
					goto IL_04dd;
				}
				goto IL_0511;
			}
		}
		goto IL_04dd;
		IL_0511:
		return (Camera)(object)new IndexOutOfRangeException();
	}

	static RenderUtils()
	{
		Camera[] tmpAllCameras = new Camera[10];
		_tmpAllCameras = tmpAllCameras;
	}
}
