using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Lofelt.NiceVibrations;

public class WobbleDemoManager : DemoManager
{
	public Camera ButtonCamera;

	public RectTransform ContentZone;

	public WobbleButton WobbleButtonPrefab;

	public Vector2 PrefabSize;

	public float Margin;

	public float Padding;

	protected List<WobbleButton> Buttons;

	protected Canvas _canvas;

	protected Vector3 _position;

	protected unsafe virtual void Start()
	{
		//IL_01b5: Invalid comparison between F8 and I4
		//IL_05a8: Invalid comparison between F8 and I4
		//IL_048c: Invalid comparison between I4 and F8
		//IL_04ee: Expected O, but got Ref
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Expected O, but got Unknown
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Expected O, but got Unknown
		//IL_0263: Expected O, but got F8
		//IL_050e: Expected O, but got I4
		//IL_03fa: Expected O, but got Ref
		//IL_0404: Expected F8, but got O
		//IL_0412: Expected O, but got Ref
		//IL_043f: Expected O, but got I4
		//IL_0447: Invalid comparison between O and F8
		//IL_0457: Expected O, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696530");
		Canvas canvas = default(Canvas);
		_canvas = canvas;
		Rect rect = ContentZone.rect;
		Rect rect2 = ContentZone.rect;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm6\"");
		double num = Math.Floor(0.0);
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm7\"");
		double num2 = Math.Floor(0.0);
		Rect rect3 = ContentZone.rect;
		double num3 = num - 1.0;
		double num4 = num3 * (double)Margin;
		double num5 = num * (double)PrefabSize;
		float num6 = Padding + Padding;
		object obj = default(object);
		float num7 = (float)obj - num6;
		double num8 = (double)num7 - num5;
		double num9 = num8 - num4;
		double num10 = num9 * 0.5;
		Rect rect4 = ContentZone.rect;
		double num11 = num2 - 1.0;
		double num12 = num11 * (double)Margin;
		double num13 = num2 * (double)PrefabSize;
		float num14 = Padding + Padding;
		float num15 = (float)obj - num14;
		double num16 = (double)num15 - num13;
		double num17 = num16 - num12;
		double num18 = num17 * 0.5;
		List<WobbleButton> buttons = new List<WobbleButton>();
		Buttons = buttons;
		bool flag = !(num > 0.0);
		object obj3 = default(object);
		object obj2 = obj3;
		double num19 = num12;
		double num20 = num14;
		int num21 = 0;
		RectTransform rectTransform = default(RectTransform);
		if (!flag)
		{
			Vector2 anchorMin = default(Vector2);
			Vector2 anchorMax = default(Vector2);
			int num31 = default(int);
			int num32 = default(int);
			object obj6 = default(object);
			List<WobbleButton>.Enumerator enumerator = default(List<WobbleButton>.Enumerator);
			bool flag3;
			do
			{
				if (num2 > 0.0)
				{
					bool flag2;
					do
					{
						double num22 = num10 + (double)Padding;
						float num23 = (float)PrefabSize * 0.5f;
						double num24 = num22 + (double)num23;
						object obj4 = PrefabSize + Margin;
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm0,eax\"");
						object obj5 = obj4 * 0;
						double num25 = (double)obj5 + num24;
						_position = (Vector3)num25;
						double num26 = num18 + (double)Padding;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Lofelt.NiceVibrations.WobbleDemoManager)+54]");
						float num27 = 0f * 0.5f;
						num19 = num26 + (double)num27;
						float num28 = Margin;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Lofelt.NiceVibrations.WobbleDemoManager)+54]");
						float num29 = num28 + 0f;
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm0,ecx\"");
						float num30 = num29 * 0f;
						num20 = (double)num30 + num19;
						_ = 0;
						WobbleButton wobbleButton = UnityEngine.Object.Instantiate(WobbleButtonPrefab);
						Transform transform = wobbleButton.transform;
						Transform parentInternal = ContentZone.transform;
						transform.parentInternal = parentInternal;
						Buttons.Add(wobbleButton);
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371660");
						rectTransform.anchorMin = anchorMin;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371660");
						rectTransform.anchorMax = anchorMax;
						string text = num31.ToString();
						string text2 = num32.ToString();
						string text3 = "WobbleButton" + text + text2;
						wobbleButton.name = text3;
						Transform transform2 = wobbleButton.transform;
						transform2.localScale = (Vector3)(&obj6);
						num13 = (double)_position;
						rectTransform.anchoredPosition3D = (Vector3)(&enumerator);
						wobbleButton.TargetCamera = ButtonCamera;
						wobbleButton.Initialization();
						object obj7 = num32 + 1;
						flag2 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj7) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num2);
						obj2 = 0;
						canvas = (Canvas)(object)rectTransform;
						num21 = num31;
					}
					while (flag2);
				}
				num21++;
				flag3 = (double)num21 < num;
				num12 = num19;
			}
			while (flag3);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		int num33 = 0;
		List<WobbleButton>.Enumerator enumerator2 = default(List<WobbleButton>.Enumerator);
		while (true)
		{
			if (enumerator2.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				List<WobbleButton> buttons2 = Buttons;
				bool flag4 = Buttons == null;
				object obj8 = (object)(&enumerator2);
				if (flag4)
				{
					break;
				}
				object obj9 = num33 / buttons2._size;
				float num34 = (float)obj9 * 0.7f;
				float num35 = num34 + 0.3f;
				object obj10 = rectTransform;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v846 @ r8_v23+1A8] (should have been resolved before IL gen)");
				num33++;
				continue;
			}
			enumerator2.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	public WobbleDemoManager()
	{
		//IL_000b: Expected O, but got I4
		//IL_003a: Expected I, but got O
		PrefabSize = (Vector2)1128792064;
		_ = 1128792064;
		Margin = 20f;
		Padding = 20f;
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		_position = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rcx_v2 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		((MonoBehaviour)this)._002Ector();
	}
}
