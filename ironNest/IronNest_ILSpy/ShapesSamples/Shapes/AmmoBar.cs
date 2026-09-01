using System;
using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

public class AmmoBar : MonoBehaviour
{
	public int totalBullets = 20;

	public int bullets = 15;

	public float bulletThicknessScale = 1f;

	public float bulletEjectScale = 0.5f;

	public float bulletDisappearTime = 1f;

	public float bulletEjectAngSpeed = 0.5f;

	public float ejectRotSpeedVariance = 1f;

	public AnimationCurve bulletEjectX = AnimationCurve.Constant(0f, 1f, 0f);

	public AnimationCurve bulletEjectY = AnimationCurve.Constant(0f, 1f, 0f);

	private float[] bulletFireTimes;

	public bool HasBulletsLeft
	{
		get
		{
			int num = bullets ^ bullets;
			int num2 = bullets & num;
			bool flag = num2 < 0;
			bool flag2 = bullets < 0;
			bool flag3 = bullets == 0;
			bool flag4 = flag2 == flag;
			bool flag5 = !flag3;
			return flag5 & flag4;
		}
	}

	private Vector2 GetBulletEjectPos(Vector2 origin, float t)
	{
		if (bulletEjectX != null)
		{
			float num = bulletEjectX.Evaluate(t);
			if (bulletEjectY != null)
			{
				float num2 = bulletEjectY.Evaluate(t);
				Vector2 result = default(Vector2);
				return result;
			}
		}
		return (Vector2)new NullReferenceException();
	}

	public void Fire()
	{
		//IL_0042: Expected O, but got I4
		float[] array = bulletFireTimes;
		int num = bullets - 1;
		bullets = num;
		float time = Time.time;
		object obj = bullets - 1;
		array[obj] = time;
	}

	public void Reload()
	{
		bullets = totalBullets;
	}

	private void Awake()
	{
		float[] array = new float[totalBullets];
		bulletFireTimes = array;
	}

	public unsafe void DrawBar(FpsController fpsController, float barRadius)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		//IL_005c: Expected I, but got O
		//IL_014f: Expected O, but got I4
		//IL_0160: Expected O, but got I4
		//IL_0422: Invalid comparison between I4 and F4
		//IL_019c: Expected F4, but got I4
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Expected O, but got Unknown
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Expected O, but got Unknown
		//IL_02c6: Expected O, but got I4
		//IL_02d6: Expected O, but got Ref
		//IL_0219: Invalid comparison between I4 and F4
		//IL_0264: Expected F4, but got I4
		//IL_0393: Expected O, but got F4
		//IL_03ef: Expected O, but got F4
		FpsController fpsController2 = default(FpsController);
		float ammoBarAngularSpanRad = fpsController2.ammoBarAngularSpanRad;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
		object obj = ammoBarAngularSpanRad ^ 0;
		float num = (float)obj * 0.5f;
		float num2 = fpsController2.ammoBarAngularSpanRad * 0.5f;
		nint num3 = (nint)typeof(Draw);
		float num4 = fpsController2.ammoBarThickness * 0.5f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v320 @ rax_v9 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num5 = 0;
		float num6 = barRadius - num4;
		_ = 2;
		float num7 = num6 * fpsController2.ammoBarAngularSpanRad;
		float num8 = num7 / (float)totalBullets;
		float num9 = num8 * bulletThicknessScale;
		bool flag = totalBullets <= 0;
		float radius = barRadius;
		Vector2 vector3 = default(Vector2);
		if (!flag)
		{
			float num10 = num2 - num;
			float num11 = fpsController2.ammoBarThickness * 0.5f;
			float num12 = fpsController2.ammoBarOutlineThickness * 1.5f;
			float num13 = num11 - num12;
			float num14 = barRadius;
			object obj2 = 32;
			float num15 = barRadius;
			object obj3 = 0;
			Vector2 vector4 = default(Vector2);
			while (true)
			{
				float num16 = (float)totalBullets - 1f;
				float num17 = (float)obj3 / num16;
				if (!(0f > num17))
				{
					if (num17 > 1f)
					{
						num17 = 1f;
					}
				}
				else
				{
					num17 = 0f;
				}
				float num18 = num17 * num10;
				float num19 = num18 + num;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033DE70");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
				float num20 = num19 * num15;
				float num21 = num19 * num13;
				bool flag2 = (nint)obj3 < bullets;
				float num22 = num10;
				if (!flag2)
				{
					bool isPlaying = Application.isPlaying;
					bool flag3 = !isPlaying;
					num22 = num10;
					if (!flag3)
					{
						float time = Time.time;
						float[] array = bulletFireTimes;
						float num23 = time;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v162 @ rsi_v6+v192 @ rax_v24 (System.Single[])]");
						float num24 = num23 - 0f;
						float num25 = num24 / bulletDisappearTime;
						if (!(0f > num25))
						{
							if (num25 > 1f)
							{
								num25 = 1f;
							}
						}
						else
						{
							num25 = 0f;
						}
						Vector2 bulletEjectPos = GetBulletEjectPos((Vector2)num20, num25);
						float num26 = (float)obj3 * 92372.8f;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033DE70");
						float num27 = num26 * ejectRotSpeedVariance;
						float num28 = num27 + bulletEjectAngSpeed;
						float num29 = num28 * num24;
						Vector2 vector = ShapesMath.Rotate((Vector2)num21, num29);
						num22 = num29;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18106D310");
				obj3++;
				obj2 += 4;
				if ((nint)obj3 >= totalBullets)
				{
					break;
				}
				object obj4 = 1065353216;
				Vector2 vector2 = vector3;
				fpsController2 = (FpsController)(&vector4);
				num14 = num9;
				num5 = (nint)(&vector2);
				num15 = barRadius;
			}
			radius = barRadius;
		}
		float angStart = default(float);
		float angEnd = default(float);
		FpsController.DrawRoundedArcOutline(vector3, radius, fpsController2.ammoBarThickness, fpsController2.ammoBarOutlineThickness, angStart, angEnd);
	}
}
