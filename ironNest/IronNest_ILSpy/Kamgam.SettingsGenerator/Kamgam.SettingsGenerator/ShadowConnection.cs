using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Kamgam.SettingsGenerator;

public class ShadowConnection : Connection<bool>
{
	protected Dictionary<RenderPipelineAsset, float> previousValue;

	public override bool Get()
	{
		//IL_0038: Expected I, but got O
		//IL_0046: Expected I, but got O
		//IL_0056: Expected O, but got I
		//IL_015d: Expected I4, but got O
		//IL_0092: Expected O, but got I
		//IL_00b7: Expected O, but got I4
		//IL_012c: Invalid comparison between I and F4
		RenderPipelineAsset currentRenderPipeline = GraphicsSettings.currentRenderPipeline;
		bool flag = (object)currentRenderPipeline == null;
		UnityEngine.Object obj = null;
		UnityEngine.Object obj4;
		if (!flag)
		{
			nint num = (nint)currentRenderPipeline;
			nint num2 = (nint)typeof(UniversalRenderPipelineAsset);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ r8_v3 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r9_v2 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ r8_v3 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ r9_v2 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+C8]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v118 @ rax_v18+FFFFFFF8+v63 @ rax_v14*8]");
				bool flag2 = 0 == (nint)typeof(UniversalRenderPipelineAsset);
				obj4 = (UnityEngine.Object)1;
				if (flag2)
				{
					goto IL_0167;
				}
			}
			obj4 = null;
			goto IL_0167;
		}
		goto IL_00dc;
		IL_0167:
		bool flag3 = (object)obj4 == null;
		obj = null;
		if (!flag3)
		{
			obj = currentRenderPipeline;
		}
		goto IL_00dc;
		IL_00dc:
		if (obj != null)
		{
			remember();
			if ((object)obj == null)
			{
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ rbx_v2 (UnityEngine.Object)+BC]");
			if (0f > 0.001f)
			{
				return true;
			}
		}
		return false;
	}

	public override void Set(bool enable)
	{
		//IL_0038: Expected I, but got O
		//IL_0046: Expected I, but got O
		//IL_0056: Expected O, but got I
		//IL_0092: Expected O, but got I
		//IL_00b7: Expected O, but got I4
		RenderPipelineAsset currentRenderPipeline = GraphicsSettings.currentRenderPipeline;
		bool flag = (object)currentRenderPipeline == null;
		UnityEngine.Object obj = null;
		UnityEngine.Object obj4;
		if (!flag)
		{
			nint num = (nint)currentRenderPipeline;
			nint num2 = (nint)typeof(UniversalRenderPipelineAsset);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ r8_v9 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v64 @ r9_v2 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ r8_v9 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v64 @ r9_v2 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+C8]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v121 @ rax_v21+FFFFFFF8+v66 @ rax_v17*8]");
				bool flag2 = 0 == (nint)typeof(UniversalRenderPipelineAsset);
				obj4 = (UnityEngine.Object)1;
				if (flag2)
				{
					goto IL_0143;
				}
			}
			obj4 = null;
			goto IL_0143;
		}
		goto IL_00dc;
		IL_0143:
		bool flag3 = (object)obj4 == null;
		obj = null;
		if (!flag3)
		{
			obj = currentRenderPipeline;
		}
		goto IL_00dc;
		IL_00dc:
		if (obj != null)
		{
			remember();
			if (!enable)
			{
				((UniversalRenderPipelineAsset)obj).shadowDistance = 0f;
			}
			else
			{
				revert();
			}
			base.NotifyListenersIfChanged(enable);
		}
	}

	protected unsafe void remember()
	{
		//IL_0038: Expected I, but got O
		//IL_0046: Expected I, but got O
		//IL_0056: Expected O, but got I
		//IL_0092: Expected O, but got I
		//IL_00b7: Expected O, but got I4
		//IL_0188: Invalid comparison between I and F4
		//IL_01f0: Expected F4, but got Ref
		//IL_01c5: Expected F4, but got Ref
		RenderPipelineAsset currentRenderPipeline = GraphicsSettings.currentRenderPipeline;
		bool flag = (object)currentRenderPipeline == null;
		UnityEngine.Object obj = null;
		UnityEngine.Object obj4;
		if (!flag)
		{
			nint num = (nint)currentRenderPipeline;
			nint num2 = (nint)typeof(UniversalRenderPipelineAsset);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r8_v10 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ r9_v7 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r8_v10 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ r9_v7 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+C8]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v124 @ rax_v33+FFFFFFF8+v67 @ rax_v29*8]");
				bool flag2 = 0 == (nint)typeof(UniversalRenderPipelineAsset);
				obj4 = (UnityEngine.Object)1;
				if (flag2)
				{
					goto IL_01fb;
				}
			}
			obj4 = null;
			goto IL_01fb;
		}
		goto IL_00dc;
		IL_01fb:
		bool flag3 = (object)obj4 == null;
		obj = null;
		if (!flag3)
		{
			obj = currentRenderPipeline;
		}
		goto IL_00dc;
		IL_00dc:
		if (!(obj != null))
		{
			return;
		}
		if (previousValue == null)
		{
			Dictionary<RenderPipelineAsset, float> dictionary = new Dictionary<RenderPipelineAsset, float>();
			previousValue = dictionary;
		}
		RenderPipelineAsset currentRenderPipeline2 = GraphicsSettings.currentRenderPipeline;
		object obj5 = default(object);
		if (previousValue.ContainsKey(currentRenderPipeline2))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v82 @ rdi_v2 (UnityEngine.Object)+BC]");
			if (0f > 0.01f)
			{
				RenderPipelineAsset currentRenderPipeline3 = GraphicsSettings.currentRenderPipeline;
				previousValue.set_Item(currentRenderPipeline3, (float)(nint)(&obj5));
			}
		}
		else
		{
			RenderPipelineAsset currentRenderPipeline4 = GraphicsSettings.currentRenderPipeline;
			previousValue.Add(currentRenderPipeline4, (nint)(&obj5));
		}
	}

	protected unsafe void revert()
	{
		//IL_0018: Expected O, but got I4
		//IL_007d: Expected O, but got Ref
		//IL_0093: Expected I, but got O
		//IL_00a1: Expected I, but got O
		//IL_00b1: Expected O, but got I
		//IL_00ed: Expected O, but got I
		//IL_012a: Expected O, but got I
		//IL_0132: Expected I, but got O
		//IL_0142: Expected O, but got I
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Expected O, but got Unknown
		//IL_0366: Invalid comparison between F4 and I
		//IL_01cd: Expected O, but got Ref
		//IL_01e3: Expected I, but got O
		//IL_01f1: Expected I, but got O
		//IL_0201: Expected O, but got I
		//IL_0227: Expected O, but got Ref
		//IL_0245: Expected O, but got I
		//IL_0274: Expected O, but got I
		//IL_0292: Expected O, but got I
		//IL_029a: Expected I, but got O
		//IL_02aa: Expected O, but got I
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18082BED0");
		KeyValuePair<RenderPipelineAsset, float> keyValuePair = (KeyValuePair<RenderPipelineAsset, float>)0;
		Dictionary<RenderPipelineAsset, float>.Enumerator enumerator = default(Dictionary<RenderPipelineAsset, float>.Enumerator);
		UnityEngine.Object obj = default(UnityEngine.Object);
		KeyValuePair<RenderPipelineAsset, float> keyValuePair2 = default(KeyValuePair<RenderPipelineAsset, float>);
		KeyValuePair<RenderPipelineAsset, float> keyValuePair4 = default(KeyValuePair<RenderPipelineAsset, float>);
		float shadowDistance = default(float);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803710D0");
				if (!(obj != null))
				{
					continue;
				}
				RenderPipelineAsset key = keyValuePair2.Key;
				bool flag = (object)key == null;
				KeyValuePair<RenderPipelineAsset, float> keyValuePair3 = (KeyValuePair<RenderPipelineAsset, float>)(&keyValuePair2);
				if (flag)
				{
					break;
				}
				nint num = (nint)key;
				nint num2 = (nint)typeof(UniversalRenderPipelineAsset);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v151 @ r8_v8 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
				keyValuePair3 = (KeyValuePair<RenderPipelineAsset, float>)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v301 @ rax_v17 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+130]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v151 @ r8_v8 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
				if (num3 < 0)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v301 @ rax_v17 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v318 @ rax_v18+FFFFFFF8+v95 @ rcx_v12 (System.Collections.Generic.KeyValuePair`2<UnityEngine.Rendering.RenderPipelineAsset, System.Single>)*8]");
				if (0 != (nint)typeof(UniversalRenderPipelineAsset))
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v151 @ r8_v8 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
				object obj3 = 0;
				nint num4 = (nint)key;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v345 @ rax_v19 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+C8]");
				object obj4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v346 @ rcx_v15+FFFFFFF8+v147 @ rdx_v11*8]");
				object obj5 = 0 - typeof(UniversalRenderPipelineAsset);
				bool flag2 = obj5 == null;
				bool flag3 = !flag2;
				RenderPipelineAsset renderPipelineAsset = null;
				if (!flag3)
				{
					renderPipelineAsset = key;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v159 @ rcx_v17 (UnityEngine.Rendering.RenderPipelineAsset)+BC]");
				bool flag4 = !(0.001f > 0f);
				keyValuePair = keyValuePair2;
				if (flag4)
				{
					continue;
				}
				RenderPipelineAsset key2 = keyValuePair4.Key;
				float value = keyValuePair.Value;
				bool flag5 = (object)key2 == null;
				keyValuePair3 = (KeyValuePair<RenderPipelineAsset, float>)(&keyValuePair);
				if (!flag5)
				{
					nint num5 = (nint)key2;
					nint num6 = (nint)typeof(UniversalRenderPipelineAsset);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v390 @ r8_v10 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
					KeyValuePair<RenderPipelineAsset, float> keyValuePair5 = (KeyValuePair<RenderPipelineAsset, float>)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v389 @ rdx_v15 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+130]");
					nint num7 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v390 @ r8_v10 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
					bool flag6 = num7 < 0;
					keyValuePair3 = (KeyValuePair<RenderPipelineAsset, float>)(&keyValuePair);
					if (!flag6)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v389 @ rdx_v15 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+C8]");
						object obj6 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v405 @ rax_v27+FFFFFFF8+v391 @ rax_v26 (System.Collections.Generic.KeyValuePair`2<UnityEngine.Rendering.RenderPipelineAsset, System.Single>)*8]");
						bool flag7 = 0 != (nint)typeof(UniversalRenderPipelineAsset);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v390 @ r8_v10 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
						keyValuePair3 = (KeyValuePair<RenderPipelineAsset, float>)0;
						if (!flag7)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v390 @ r8_v10 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
							object obj7 = 0;
							nint num8 = (nint)key2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v417 @ rax_v28 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+C8]");
							object obj8 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v418 @ rcx_v22+FFFFFFF8+v146 @ rdx_v16*8]");
							object obj9 = 0 - typeof(UniversalRenderPipelineAsset);
							bool flag8 = obj9 == null;
							bool flag9 = !flag8;
							UniversalRenderPipelineAsset universalRenderPipelineAsset = null;
							if (!flag9)
							{
								universalRenderPipelineAsset = (UniversalRenderPipelineAsset)key2;
							}
							universalRenderPipelineAsset.shadowDistance = shadowDistance;
							continue;
						}
					}
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}
}
