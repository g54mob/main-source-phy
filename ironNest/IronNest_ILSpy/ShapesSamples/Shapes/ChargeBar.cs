using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

public class ChargeBar : MonoBehaviour
{
	private float chargeSpeed;

	private float chargeDecaySpeed;

	[NonSerialized]
	public bool isCharging;

	private float charge;

	public Color tickColor;

	public Gradient chargeFillGradient;

	public float tickSizeSmol;

	public float tickSizeLorge;

	public float tickTickness;

	public float fontSize;

	public float fontSizeLorge;

	public float percentLabelOffset;

	public float fontGrowRangePrev;

	public float fontGrowRangeNext;

	public AnimationCurve chargeFillCurve;

	public AnimationCurve animChargeShakeMagnitude;

	public float chargeShakeMagnitude;

	public float chargeShakeSpeed;

	public void UpdateCharge()
	{
		//IL_00d9: Invalid comparison between I4 and F4
		//IL_00e8: Expected F4, but got I4
		float num2;
		if (!isCharging)
		{
			float deltaTime = Time.deltaTime;
			float num = deltaTime * chargeDecaySpeed;
			num2 = charge - num;
		}
		else
		{
			float deltaTime2 = Time.deltaTime;
			float num3 = deltaTime2 * chargeSpeed;
			float num4 = num3 + charge;
			num2 = num4;
		}
		charge = num2;
		bool flag = 0f > num2;
		float num5 = 0f;
		if (!flag)
		{
			bool flag2 = num2 > 1f;
			num5 = 1f;
			if (!flag2)
			{
				charge = num2;
				return;
			}
		}
		charge = num5;
	}

	public unsafe void DrawBar(FpsController fpsController, float barRadius)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		//IL_018e: Invalid comparison between I4 and F4
		//IL_01e3: Expected F4, but got I4
		//IL_020e: Expected O, but got Ref
		//IL_0252: Expected O, but got Ref
		//IL_0320: Expected O, but got Ref
		//IL_0337: Expected O, but got Ref
		//IL_038c: Expected O, but got Ref
		//IL_06f2: Expected I, but got O
		//IL_0918: Expected O, but got I4
		//IL_0928: Expected F4, but got I
		//IL_0930: Expected O, but got Ref
		//IL_0941: Expected O, but got I4
		//IL_094a: Expected O, but got I4
		//IL_088c: Invalid comparison between I4 and F4
		//IL_03d2: Expected F4, but got I4
		//IL_0782: Unknown result type (might be due to invalid IL or missing references)
		//IL_0787: Expected O, but got Unknown
		//IL_095d: Expected O, but got Ref
		//IL_0972: Expected O, but got Ref
		//IL_0986: Expected O, but got Ref
		//IL_09b8: Invalid comparison between I4 and F4
		//IL_09ca: Expected O, but got I4
		//IL_07c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cc: Expected O, but got Unknown
		//IL_07ec: Invalid comparison between I4 and F4
		//IL_041b: Expected F4, but got I4
		//IL_04b3: Expected O, but got Ref
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Expected O, but got Unknown
		//IL_0512: Expected F4, but got I
		//IL_0532: Expected O, but got I4
		//IL_054a: Expected O, but got Ref
		//IL_0553: Expected O, but got I4
		//IL_05bd: Expected O, but got Ref
		//IL_05ea: Expected O, but got Ref
		//IL_0693: Expected F4, but got I
		//IL_0693: Expected F4, but got I
		//IL_0693: Expected F4, but got I
		//IL_0693: Expected O, but got F4
		//IL_08ae: Expected I, but got O
		//IL_08c1: Expected I, but got O
		//IL_0a11: Expected O, but got Ref
		//IL_0a32: Expected O, but got Ref
		//IL_0a76: Expected O, but got Ref
		//IL_08d4: Expected I, but got O
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		float ammoBarAngularSpanRad = fpsController.ammoBarAngularSpanRad;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		object obj3 = ammoBarAngularSpanRad ^ 0;
		_ = fpsController.ammoBarThickness;
		_ = fpsController.ammoBarOutlineThickness;
		float num = (float)obj3 * 0.5f;
		float num2 = fpsController.ammoBarThickness * 0.5f;
		float num3 = fpsController.ammoBarAngularSpanRad * 0.5f;
		float num4 = num2 + barRadius;
		float num5 = num + (float)Math.PI;
		float num6 = num3 + (float)Math.PI;
		float num7 = chargeFillCurve.Evaluate(charge);
		float num8 = animChargeShakeMagnitude.Evaluate(num7);
		float num9 = num8 * chargeShakeMagnitude;
		float time = Time.time;
		float num10 = time * chargeShakeSpeed;
		float num11 = MathF.Floor(num10);
		float time2 = num10 - num11;
		float num12 = fpsController.shakeAnimX.Evaluate(time2);
		float num13 = fpsController.shakeAnimY.Evaluate(time2);
		float num14 = num13 * num9;
		float num15 = num12 * num9;
		float num16 = ((0f > num7) ? 0f : ((num7 > 1f) ? 1f : num7));
		float num17 = num5 - num6;
		float num18 = num17 * num16;
		float num19 = num18 + num6;
		_ = chargeFillGradient.Evaluate(num7).r;
		float num20 = default(float);
		DiscColors discColors = (Color)(&num20);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180F9B590");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180F9BD60");
		_ = discColors.innerStart;
		object obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 32));
		_ = discColors.outerStart;
		obj = discColors.innerEnd;
		_ = discColors.outerEnd;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180F9AE50");
		MatrixStack.Pop();
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033DE70");
		float num21 = num19;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+170]");
		float num22 = num21 * 0f;
		float num23 = num22 + num15;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
		float num24 = num19;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+170]");
		float num25 = num24 * 0f;
		float num26 = num25;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+168]");
		float num27 = num26 + 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033DE70");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
		DiscColors discColors2 = (Color)(&num20);
		string text = (string)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 32));
		_ = discColors2.outerStart;
		_ = discColors2.innerStart;
		_ = discColors2.outerEnd;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-7C]");
		float num28 = 0f * 0.5f;
		obj = discColors2.innerEnd;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18106CFF0");
		float num29 = default(float);
		object obj5 = (object)(&num29);
		nint num30 = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v570 @ rax_v22 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num31 = 0;
		float num32 = num5 - num6;
		_ = 0;
		_ = 0;
		_ = 0;
		Vector3 vector = (Vector3)0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+20]");
		num20 = 0f;
		Color color = (Color)(&num20);
		float num33 = num7;
		object obj6 = 0;
		object obj7 = 96;
		Vector3 euler = default(Vector3);
		bool flag2;
		float num54 = default(float);
		do
		{
			float num34 = (float)vector / 6f;
			float num35 = ((0f > num34) ? 0f : ((num34 > 1f) ? 1f : num34));
			float num36 = num35 * num32;
			float num37 = num36 + num6;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033DE70");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
			float num38 = num37;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-60]");
			float num39 = num38 * 0f;
			float num40 = num39;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+168]");
			float num41 = num40 + 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mul esi\"");
			object obj8 = (object)color >> 1;
			object obj9 = obj8 * 2;
			object obj10 = obj8 + obj9;
			if ((object)vector != obj10)
			{
			}
			_ = tickColor;
			object obj11 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 112));
			_ = tickColor;
			object obj12 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 64));
			_ = 0;
			object obj13 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 48));
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18106D310");
			float num42 = num34 - num7;
			bool flag = !(0f > num42);
			object obj14 = 100;
			if (!flag)
			{
				obj14 = obj7;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj15 = num42 & 0;
			float num43 = (float)obj15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v790 @ rax_v32+this @ rcx (Shapes.ChargeBar)]");
			float num44 = num43 / 0f;
			if (!(0f > num44))
			{
				if (num44 > 1f)
				{
					num44 = 1f;
				}
			}
			else
			{
				num44 = 0f;
			}
			float num45 = num44 * (float)Math.PI;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033DE70");
			float num46 = num45 * -0.5f;
			float num47 = num46 + 0.5f;
			float num48 = 1f - num47;
			float num49 = 1f - num48;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
			float num50 = fontSize * fontSizeLorge;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18106E210");
			float num51 = num34 * 100f;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
			int num52 = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 128));
			string text2 = ((int*)num52)->ToString();
			string text3 = text2 + "%";
			Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
			color = (Color)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 112));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-58]");
			float num53 = 0f * percentLabelOffset;
			_ = quaternion.x;
			num33 = num53 + num41;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18106D940");
			vector++;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-54]");
			num32 = 0f;
			flag2 = (nint)vector < 7;
			num20 = num54;
			obj6 = 5;
			text = text3;
			num28 = num54;
			obj5 = (object)(&num20);
			obj7 = 96;
		}
		while (flag2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-7C]");
		float num55 = 0f * 0.5f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-44]");
		float num56 = 0f * 0.5f;
		float num57 = num56 + num55;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18106D140");
		Color color2 = (Color)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 112));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+20]");
		_ = 0;
		DiscColors discColors3 = color2;
		object obj16 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 32));
		_ = discColors3.innerStart;
		_ = discColors3.outerStart;
		obj = discColors3.innerEnd;
		_ = discColors3.outerEnd;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-44]");
		float num58 = 0f * 0.5f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-7C]");
		float num59 = 0f * 0.5f;
		float num60 = num59 - num58;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18106CFF0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+170]");
		nint num61 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-7C]");
		nint num62 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-44]");
		float angStart = default(float);
		float angEnd = default(float);
		FpsController.DrawRoundedArcOutline((Vector2)num54, num61, num62, 0f, angStart, angEnd);
		nint num63 = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v998 @ rax_v50 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num64 = 0;
		_ = 2;
		nint num65 = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1031 @ rax_v54 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num66 = 0;
		Color outer = (Color)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 112));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+20]");
		_ = 0;
		_ = 0;
		Color inner = (Color)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 32));
		_ = 2;
		DiscColors discColors4 = DiscColors.Radial(inner, outer);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-7C]");
		float num67 = 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-7C]");
		float num68 = num67 + 0f;
		object obj17 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 32));
		_ = discColors4.innerStart;
		_ = discColors4.outerStart;
		obj = discColors4.innerEnd;
		_ = discColors4.outerEnd;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18106CFF0");
		nint num69 = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1065 @ rax_v60 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num70 = 0;
		_ = 1;
	}

	public ChargeBar()
	{
		//IL_0012: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		tickColor = (Color)0;
		chargeSpeed = 1f;
		chargeDecaySpeed = 1f;
		tickSizeSmol = 0.1f;
		tickSizeLorge = 0.1f;
		fontSize = 0.1f;
		fontSizeLorge = 0.1f;
		percentLabelOffset = 0.1f;
		fontGrowRangePrev = 0.1f;
		fontGrowRangeNext = 0.1f;
		animChargeShakeMagnitude = AnimationCurve.Linear(0f, 0f, 1f, 1f);
		chargeShakeMagnitude = 0.1f;
		chargeShakeSpeed = 1f;
		base._002Ector();
	}
}
