using System;
using Cpp2ILInjected;
using UnityEngine.Rendering;

namespace Kamgam.SettingsGenerator;

public static class RenderPipelineDetector
{
	public enum RenderPiplelineType
	{
		URP,
		HDRP,
		BuiltIn
	}

	public static RenderPiplelineType GetCurrentRenderPiplelineType()
	{
		//IL_00dc: Expected I4, but got O
		RenderPipelineAsset currentRenderPipeline = GraphicsSettings.currentRenderPipeline;
		if (!(currentRenderPipeline != null))
		{
			return RenderPiplelineType.BuiltIn;
		}
		if ((object)currentRenderPipeline != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
			object obj = default(object);
			if (obj != null)
			{
				object obj2 = obj;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v160 @ rdx_v4+1B8] (should have been resolved before IL gen)");
				string text = default(string);
				if (text != null)
				{
					bool flag = text.Contains("Universal");
					return (!flag) ? RenderPiplelineType.HDRP : RenderPiplelineType.URP;
				}
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (RenderPiplelineType)ex;
	}

	public static bool IsURP()
	{
		RenderPiplelineType currentRenderPiplelineType = GetCurrentRenderPiplelineType();
		return currentRenderPiplelineType == RenderPiplelineType.URP;
	}

	public static bool IsHDRP()
	{
		//IL_0017: Expected O, but got I4
		RenderPiplelineType currentRenderPiplelineType = GetCurrentRenderPiplelineType();
		object obj = currentRenderPiplelineType - 1;
		return obj == null;
	}

	public static bool IsBuiltIn()
	{
		//IL_0017: Expected O, but got I4
		RenderPiplelineType currentRenderPiplelineType = GetCurrentRenderPiplelineType();
		object obj = currentRenderPiplelineType - 2;
		return obj == null;
	}
}
