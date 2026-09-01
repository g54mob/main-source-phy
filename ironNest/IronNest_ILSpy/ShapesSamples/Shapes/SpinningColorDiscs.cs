using System;
using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

public class SpinningColorDiscs : ImmediateModeShapeDrawer
{
	public int discCount = 24;

	public float discRadius = 0.1f;

	public unsafe override void DrawShapes(Camera cam)
	{
		//IL_0060: Expected O, but got Ref
		//IL_029d: Expected I, but got O
		//IL_02f0: Expected O, but got F4
		//IL_0317: Expected O, but got I4
		//IL_0328: Expected O, but got I4
		//IL_01c8: Expected O, but got Ref
		//IL_021f: Expected O, but got I4
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Expected O, but got Unknown
		//IL_0245: Expected O, but got I4
		DrawCommand drawCommand = Draw.Command(cam);
		DrawCommand drawCommand2 = default(DrawCommand);
		object obj = (object)(&drawCommand2);
		Draw.ResetAllDrawStates();
		Transform transform = base.transform;
		if ((object)transform != null)
		{
			Matrix4x4 localToWorldMatrix = transform.localToWorldMatrix;
			nint num = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v137 @ rax_v13 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num2 = 0;
			Draw.matrix = (Matrix4x4)localToWorldMatrix.m00;
			_ = localToWorldMatrix.m01;
			_ = localToWorldMatrix.m02;
			_ = localToWorldMatrix.m03;
			object obj2 = 0;
			Transform transform2 = transform;
			object obj3 = 0;
			bool flag = default(bool);
			float num15 = default(float);
			float angleRadStart = default(float);
			float angleRadEnd = default(float);
			ArcEndCap arcEndCaps = default(ArcEndCap);
			while ((nint)obj2 < discCount)
			{
				float num3 = (float)obj2 / (float)discCount;
				Color color = Color.HSVToRGB(num3, 1f, 1f, flag);
				float time = Time.time;
				float num4 = time * ((float)Math.PI * 2f);
				float num5 = num4 * 0.25f;
				float num6 = num3 * ((float)Math.PI * 2f);
				float time2 = Time.time;
				float num7 = time2 * ((float)Math.PI * 2f);
				float num8 = num7 * 0.5f;
				float num9 = num6 + num5;
				float num10 = num9 + num9;
				float num11 = num10 + num8;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033DE70");
				float num12 = num11 * 0.16f;
				float num13 = num6 + num5;
				float num14 = num12 + num13;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033DE70");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
				DiscColors discColors = (Color)(&num15);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180F9B590");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180F9BD60");
				Draw.DiscCore(false, false, discRadius, 0f, (DiscColors)flag, angleRadStart, angleRadEnd, arcEndCaps);
				MatrixStack.Pop();
				obj2++;
				transform2 = null;
				obj3 = 0;
			}
			if (obj != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
			return;
		}
		throw new NullReferenceException();
	}

	private Vector2 GetDiscPosition(float t)
	{
		float time = Time.time;
		float num = time * ((float)Math.PI * 2f);
		float num2 = t * ((float)Math.PI * 2f);
		float num3 = num * 0.25f;
		float num4 = num3 + num2;
		float time2 = Time.time;
		float num5 = time2 * ((float)Math.PI * 2f);
		float num6 = num4 + num4;
		float num7 = num5 * 0.5f;
		float num8 = num7 + num6;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033DE70");
		float num9 = num8 * 0.16f;
		float num10 = num9 + num4;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033DE70");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
		Vector2 result = default(Vector2);
		return result;
	}
}
