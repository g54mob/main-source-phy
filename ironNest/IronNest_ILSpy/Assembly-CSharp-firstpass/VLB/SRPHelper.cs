using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;

namespace VLB;

public static class SRPHelper
{
	private static bool m_IsRenderPipelineCached;

	private static RenderPipeline m_RenderPipelineCached;

	public static string renderPipelineScriptingDefineSymbolAsString
	{
		get
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39D54]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			return "VLB_URP";
		}
	}

	public static RenderPipeline projectRenderPipeline
	{
		get
		{
			//IL_0154: Expected I4, but got O
			RenderPipeline renderPipelineCached;
			if (!m_IsRenderPipelineCached)
			{
				RenderPipelineAsset defaultRenderPipeline = GraphicsSettings.defaultRenderPipeline;
				if ((bool)defaultRenderPipeline)
				{
					if ((object)defaultRenderPipeline != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
						object obj = default(object);
						if (obj != null)
						{
							object obj2 = obj;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v264 @ rdx_v8+168] (should have been resolved before IL gen)");
							string text = default(string);
							if (text != null)
							{
								if (!text.Contains("Universal") && !text.Contains("Lightweight"))
								{
									if (!text.Contains("HD"))
									{
										goto IL_0138;
									}
									renderPipelineCached = RenderPipeline.HDRP;
								}
								else
								{
									renderPipelineCached = RenderPipeline.URP;
								}
								goto IL_017a;
							}
						}
					}
					NullReferenceException ex = new NullReferenceException();
					return (RenderPipeline)ex;
				}
				goto IL_0138;
			}
			goto IL_0192;
			IL_0138:
			renderPipelineCached = RenderPipeline.BuiltIn;
			goto IL_017a;
			IL_017a:
			m_RenderPipelineCached = renderPipelineCached;
			m_IsRenderPipelineCached = true;
			goto IL_0192;
			IL_0192:
			return m_RenderPipelineCached;
		}
	}

	private static RenderPipeline ComputeRenderPipeline()
	{
		//IL_013e: Expected I4, but got O
		//IL_0107: Expected O, but got I4
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Expected I4, but got Unknown
		RenderPipelineAsset defaultRenderPipeline = GraphicsSettings.defaultRenderPipeline;
		if ((bool)defaultRenderPipeline)
		{
			if ((object)defaultRenderPipeline != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
				object obj = default(object);
				if (obj != null)
				{
					object obj2 = obj;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v182 @ rdx_v4+168] (should have been resolved before IL gen)");
					string text = default(string);
					if (text != null)
					{
						if (!text.Contains("Universal") && !text.Contains("Lightweight"))
						{
							bool flag = text.Contains("HD");
							object obj3 = 0 - (flag ? 1 : 0);
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sbb eax,eax\"");
							return (RenderPipeline)(obj3 & 2);
						}
						return RenderPipeline.URP;
					}
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (RenderPipeline)ex;
		}
		return RenderPipeline.BuiltIn;
	}

	public static bool IsUsingCustomRenderPipeline()
	{
		if (RenderPipelineManager.s_CurrentPipeline != null)
		{
			return true;
		}
		RenderPipelineAsset defaultRenderPipeline = GraphicsSettings.defaultRenderPipeline;
		return defaultRenderPipeline != null;
	}

	public static void RegisterOnBeginCameraRendering(Action<ScriptableRenderContext, Camera> cb)
	{
		if (IsUsingCustomRenderPipeline())
		{
			RenderPipelineManager.beginCameraRendering -= cb;
			RenderPipelineManager.beginCameraRendering += cb;
		}
	}

	public static void UnregisterOnBeginCameraRendering(Action<ScriptableRenderContext, Camera> cb)
	{
		if (IsUsingCustomRenderPipeline())
		{
			RenderPipelineManager.beginCameraRendering -= cb;
		}
	}
}
