using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

public class Compass : MonoBehaviour
{
	[StructLayout((LayoutKind)3)]
	private struct _003C_003Ec__DisplayClass14_0
	{
		public float angWorldMax;

		public float angWorldMin;

		public float angUiMin;

		public float angUiMax;

		public Vector2 compArcOrigin;

		public Compass _003C_003E4__this;
	}

	public Vector2 position;

	public float width = 1f;

	public float lineThickness = 0.1f;

	public float bendRadius = 1f;

	public float fieldOfView = (float)Math.PI / 2f;

	public int ticksPerQuarterTurn = 12;

	public float tickSize = 0.1f;

	public float tickEdgeFadeFraction = 0.1f;

	public float fontSizeTickLabel = 1f;

	public float tickLabelOffset = 0.01f;

	public float fontSizeLookLabel = 1f;

	public Vector2 lookAngLabelOffset;

	public float triangleNootSize = 0.1f;

	private string[] directionLabels = new string[4] { "S", "W", "N", "E" };

	public unsafe void DrawCompass(Vector3 worldDir)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Expected O, but got Unknown
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Expected Ref, but got Unknown
		//IL_0373: Expected O, but got I
		//IL_0381: Expected I, but got O
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Expected I4, but got Unknown
		//IL_005d: Expected I, but got O
		//IL_03f5: Expected I, but got O
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Expected O, but got Unknown
		//IL_0534: Expected O, but got Ref
		//IL_0418: Expected I, but got O
		//IL_0430: Expected I, but got O
		//IL_05c8: Expected O, but got Ref
		//IL_05c8: Expected F4, but got I
		//IL_00d3: Expected O, but got I4
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Expected O, but got Unknown
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Expected O, but got Unknown
		//IL_0140: Expected O, but got Ref
		//IL_0153: Expected F4, but got I
		//IL_0470: Expected O, but got I4
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Expected O, but got Unknown
		//IL_0227: Invalid comparison between F4 and I4
		//IL_0239: Expected O, but got I4
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Expected O, but got Unknown
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Expected O, but got Unknown
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Expected O, but got Unknown
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = obj2 - 184;
		_ = 0;
		_ = 0;
		ref Vector2 value = ref *(Vector2*)(obj + 200);
		float num = width * 0.5f;
		float num2 = num / bendRadius;
		float num3 = (float)Math.PI / 2f - num2;
		Vector2 vector = Vector2.Normalize(ref value);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033D7B0");
		float num4 = fieldOfView * 0.5f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+DC]");
		float a = 0f - num4;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+DC]");
		nint num5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		object obj3 = num5 ^ 0;
		nint num6 = (nint)typeof(Vector2);
		float num7 = (float)obj3 * 57.29578f;
		float num8 = num7 + 180f;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
		int num9 = obj + 192;
		string text = ((int*)num9)->ToString();
		string text2 = text + "°";
		nint num10 = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v215 @ rax_v17 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num11 = 0;
		_ = 1;
		nint num12 = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v248 @ rax_v20 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num13 = 0;
		_ = lineThickness;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180F9B590");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180F9BD60");
		object obj4 = obj - 96;
		float num14 = default(float);
		DiscColors discColors = (Color)(&num14);
		_ = discColors.innerEnd;
		_ = discColors.outerEnd;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180F9AE50");
		MatrixStack.Pop();
		nint num15 = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v418 @ rax_v37 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num16 = 0;
		_ = fontSizeLookLabel;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18106DCC0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180F9B590");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180F9BD60");
		nint num17 = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v540 @ rcx_v37 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num18 = 0;
		float radius = triangleNootSize;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v553 @ rax_v48 (Il2CppStaticFields<Shapes.Draw>)+18C]");
		bool flag = default(bool);
		float roundness = default(float);
		float angle = default(float);
		Draw.RegularPolygon_Internal(3, radius, 0f, (Color)(&num14), flag, roundness, angle);
		MatrixStack.Pop();
		object obj5 = ticksPerQuarterTurn * 4;
		object obj6 = obj5 - 4;
		if ((nint)obj6 <= 0)
		{
			return;
		}
		object obj7 = obj6 >> 31;
		object obj8 = obj7 & 3;
		object obj9 = obj8 + obj6;
		object obj10 = obj9 >> 2;
		Color color = (Color)(&num14);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v553 @ rax_v48 (Il2CppStaticFields<Shapes.Draw>)+18C]");
		float num19 = 0f;
		Compass compass = null;
		string text3 = null;
		object obj14 = default(object);
		float b = default(float);
		do
		{
			object obj11 = (object)text3 % obj10;
			object obj12 = (object)text3 / obj6;
			float num20 = (float)obj12 * ((float)Math.PI * 2f);
			bool flag2 = obj11 != null;
			string text4 = null;
			if (!flag2)
			{
				float num21 = 1f - (float)obj12;
				float num22 = num21 * 4f;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
				string[] array = directionLabels;
				object obj13 = obj14 & 0x80000003L;
				if ((nint)directionLabels < 0)
				{
					object obj15 = obj13 - 1;
					object obj16 = obj15 | -4;
					obj13 = obj16 + 1;
				}
				text4 = array[obj13];
				compass = (Compass)(object)directionLabels;
			}
			float num23 = ShapesMath.InverseLerpAngleRad(a, b, num20);
			bool flag3 = !(1f > num23);
			color = (Color)0;
			num19 = num20;
			if (!flag3)
			{
				bool flag4 = !(num23 > 0f);
				color = (Color)0;
				num19 = num20;
				if (!flag4)
				{
					num19 = ((obj11 != null) ? 0.5f : 0.8f);
					_003CDrawCompass_003Eg__DrawTick_007C14_0(num20, num19, text4, ref *(flag ? ((_003C_003Ec__DisplayClass14_0*)1) : ((_003C_003Ec__DisplayClass14_0*)null)));
					color = (Color)text4;
					compass = this;
				}
			}
			text3++;
		}
		while (System.Runtime.CompilerServices.Unsafe.As<string, UIntPtr>(ref text3) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6));
	}

	private unsafe void _003CDrawCompass_003Eg__DrawTick_007C14_0(float worldAng, float size, [Optional][DefaultParameterValue(null)] string label, ref _003C_003Ec__DisplayClass14_0 P_3)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_01d2: Expected O, but got I
		//IL_01eb: Expected F4, but got I
		//IL_01eb: Expected F4, but got O
		//IL_01f8: Invalid comparison between I4 and F4
		//IL_0093: Expected F4, but got I4
		//IL_0229: Expected O, but got I
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Expected O, but got Unknown
		//IL_02b7: Invalid comparison between I4 and F4
		//IL_00b2: Invalid comparison between I4 and F4
		//IL_0103: Expected I, but got O
		//IL_0142: Expected O, but got Ref
		//IL_0142: Expected O, but got Ref
		//IL_0177: Expected I, but got O
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = obj2 - 40;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1+50]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ rbx_v1+4]");
		float num = ShapesMath.InverseLerpAngleRad((float)obj3, 0f, worldAng);
		float num2;
		if (!(0f > num))
		{
			bool flag = !(num > 1f);
			num2 = num;
			if (!flag)
			{
				num2 = 1f;
			}
		}
		else
		{
			num2 = 0f;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ rbx_v1+C]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ rbx_v1+8]");
		object obj4 = num3 - 0;
		float num4 = (float)obj4 * num2;
		float num5 = num4;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ rbx_v1+8]");
		float num6 = num5 + 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033DE70");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
		float num7 = num + num;
		float num8 = num7 - 1f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj5 = num8 & 0;
		float num9 = 1f - (float)obj5;
		bool flag2 = 0f == tickEdgeFadeFraction;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018106FF31h\"");
		if (!flag2)
		{
			float num10 = num9 / tickEdgeFadeFraction;
			if (!(0f > num10) && !(num10 > 1f))
			{
			}
		}
		nint num11 = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v273 @ rax_v10 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num12 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v277 @ rcx_v7 (Il2CppStaticFields<Shapes.Draw>)+190]");
		Vector3 vector = default(Vector3);
		Vector3 euler = default(Vector3);
		Color colorStart = default(Color);
		Color colorEnd = default(Color);
		float thickness = default(float);
		Draw.Line_Internal(LineEndCap.None, ThicknessSpace.Meters, (Vector3)(&vector), (Vector3)(&euler), colorStart, colorEnd, thickness);
		if (label != null)
		{
			nint num13 = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v489 @ rax_v21 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num14 = 0;
			_ = fontSizeTickLabel;
			Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1+38]");
			object obj6 = 0 * tickLabelOffset;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18106DEC0");
		}
	}
}
