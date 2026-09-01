using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

public class Crosshair : MonoBehaviour
{
	public float crosshairCrossInnerRad = 0.1f;

	public float crosshairCrossOuterRad = 0.3f;

	public float crosshairCrossThickness = 0.2f;

	public float crosshairHitCrossInnerRad = 0.1f;

	public float crosshairHitCrossOuterRad = 0.3f;

	public float crosshairHitCrossThickness = 0.2f;

	public float scaleFire = 0.1f;

	public Decayer fireDecayer;

	public Decayer hitDecayer;

	public void Fire()
	{
		Decayer decayer = fireDecayer;
		decayer.t = 1f;
	}

	public void FireHit()
	{
		Decayer decayer = hitDecayer;
		decayer.t = 1f;
	}

	public void UpdateCrosshairDecay()
	{
		fireDecayer.Update();
		hitDecayer.Update();
	}

	public void DrawCrosshair()
	{
		//IL_0169: Expected I, but got O
		//IL_018c: Expected I, but got O
		//IL_01af: Expected I, but got O
		//IL_01d2: Expected I, but got O
		//IL_00d4: Invalid comparison between I4 and F4
		//IL_011f: Expected F4, but got I4
		Vector2[] dirs = new Vector2[4];
		nint num = (nint)typeof(Vector2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rcx_v4 (Il2CppClass<UnityEngine.Vector2>)+B8]");
		nint num2 = 0;
		_ = Vector2.upVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v57 @ rdx_v2 (Il2CppStaticFields<UnityEngine.Vector2>)+14]");
		_ = 0;
		nint num3 = (nint)typeof(Vector2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v255 @ rax_v10 (Il2CppClass<UnityEngine.Vector2>)+B8]");
		nint num4 = 0;
		_ = Vector2.rightVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v261 @ rcx_v8 (Il2CppStaticFields<UnityEngine.Vector2>)+2C]");
		_ = 0;
		nint num5 = (nint)typeof(Vector2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v256 @ rax_v12 (Il2CppClass<UnityEngine.Vector2>)+B8]");
		nint num6 = 0;
		_ = Vector2.downVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v262 @ rcx_v10 (Il2CppStaticFields<UnityEngine.Vector2>)+1C]");
		_ = 0;
		nint num7 = (nint)typeof(Vector2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v257 @ rax_v14 (Il2CppClass<UnityEngine.Vector2>)+B8]");
		nint num8 = 0;
		_ = Vector2.leftVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v263 @ rcx_v12 (Il2CppStaticFields<UnityEngine.Vector2>)+24]");
		_ = 0;
		Vector2[] dirs2 = new Vector2[4];
		Vector2 value = default(Vector2);
		Vector2 vector = Vector2.Normalize(ref value);
		Vector2 vector2 = Vector2.Normalize(ref value);
		Vector2 vector3 = Vector2.Normalize(ref value);
		Vector2 vector4 = Vector2.Normalize(ref value);
		Decayer decayer = fireDecayer;
		float num9 = decayer.t;
		if (!(0f > decayer.t))
		{
			if (num9 > 1f)
			{
				num9 = 1f;
			}
		}
		else
		{
			num9 = 0f;
		}
		float num10 = scaleFire - 1f;
		float num11 = num10 * num9;
		float num12 = num11 + 1f;
		float thickness = num12 * crosshairCrossThickness;
		float radialOffset = default(float);
		Color color = default(Color);
		_003CDrawCrosshair_003Eg__DrawCross_007C12_0(dirs, crosshairCrossInnerRad, crosshairCrossOuterRad, thickness, radialOffset, color);
		_003CDrawCrosshair_003Eg__DrawCross_007C12_0(dirs2, crosshairHitCrossInnerRad, crosshairHitCrossOuterRad, crosshairHitCrossThickness, radialOffset, color);
	}

	public Crosshair()
	{
		Decayer decayer = new Decayer();
		fireDecayer = decayer;
		hitDecayer = new Decayer();
		base._002Ector();
	}

	internal unsafe static void _003CDrawCrosshair_003Eg__DrawCross_007C12_0(Vector2[] dirs, float radInner, float radOuter, float thickness, float radialOffset, Color color)
	{
		//IL_0028: Expected O, but got I4
		//IL_0031: Expected O, but got I4
		//IL_0053: Expected I, but got O
		//IL_008d: Expected O, but got Ref
		//IL_008d: Expected O, but got Ref
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Expected O, but got Unknown
		object obj = 0;
		object obj2 = 0;
		object obj3 = default(object);
		object obj4 = default(object);
		Color colorStart = default(Color);
		Color colorEnd = default(Color);
		float thickness2 = default(float);
		while ((nint)obj2 < dirs.Length)
		{
			nint num = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v385 @ rax_v13 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v387 @ rdx_v3 (Il2CppStaticFields<Shapes.Draw>)+190]");
			Draw.Line_Internal(LineEndCap.Round, ThicknessSpace.Meters, (Vector3)(&obj3), (Vector3)(&obj4), colorStart, colorEnd, thickness2);
			obj++;
			obj2 = obj;
		}
	}
}
