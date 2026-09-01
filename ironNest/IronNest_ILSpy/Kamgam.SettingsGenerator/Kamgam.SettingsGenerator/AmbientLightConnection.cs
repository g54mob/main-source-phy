using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;

namespace Kamgam.SettingsGenerator;

public class AmbientLightConnection : Connection<float>
{
	public float MinColorIntensity = 0.01f;

	public float MaxColorIntensity = 2f;

	public override float Get()
	{
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Expected O, but got Unknown
		//IL_01dc: Expected O, but got I4
		//IL_01e5: Expected O, but got I4
		//IL_01f2: Invalid comparison between O and F4
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Expected O, but got Unknown
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Expected O, but got Unknown
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Expected O, but got Unknown
		//IL_0211: Expected F4, but got O
		float outMin = default(float);
		float outAnchor = default(float);
		float outMax = default(float);
		bool clamp = default(bool);
		if (RenderSettings.ambientMode == AmbientMode.Skybox)
		{
			Material skybox = RenderSettings.skybox;
			if (skybox != null)
			{
				float ambientIntensity = RenderSettings.ambientIntensity;
				return MathUtils.MapWithAnchor(ambientIntensity, 0f, 1f, 8f, outMin, outAnchor, outMax, clamp);
			}
		}
		Color ambientLight = RenderSettings.ambientLight;
		float[] array = new float[3];
		float num3;
		if (array.Length > 0)
		{
			array[0] = ambientLight.r;
			if (array.Length > 1)
			{
				float num = default(float);
				array[1] = num;
				if (array.Length > 2)
				{
					array[2] = num;
					if (array.Length == 0)
					{
						goto IL_0233;
					}
					bool flag = array.Length <= 0;
					float num2 = num;
					if (!flag)
					{
						num3 = array[0];
						if (array.Length <= 1)
						{
							goto IL_0216;
						}
						object obj = array + 36;
						float num4 = array[0];
						object obj2 = 1;
						object obj3 = 1;
						while (true)
						{
							bool flag2 = (nint)obj3 >= array.Length;
							num2 = num;
							if (flag2)
							{
								break;
							}
							if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num4))
							{
								num4 = (float)obj;
							}
							obj3++;
							obj2++;
							obj += 4;
							bool flag3 = (nint)obj2 < array.Length;
							num3 = num4;
							if (flag3)
							{
								continue;
							}
							goto IL_0216;
						}
					}
				}
			}
		}
		throw new IndexOutOfRangeException();
		IL_02c5:
		float inAnchor = MaxColorIntensity * 0.5f;
		return MathUtils.MapWithAnchor(num3, 0f, inAnchor, MaxColorIntensity, outMin, outAnchor, outMax, clamp);
		IL_0216:
		if (!(num3 > 0.01f))
		{
			goto IL_0233;
		}
		goto IL_02c5;
		IL_0233:
		num3 = 0.01f;
		goto IL_02c5;
	}

	public unsafe override void Set(float intensity)
	{
		//IL_0114: Expected F4, but got O
		//IL_0126: Expected F4, but got O
		//IL_0140: Expected F4, but got I4
		//IL_0301: Expected O, but got Ref
		//IL_0313: Expected O, but got I4
		//IL_023e: Expected I, but got O
		//IL_024e: Expected O, but got I
		//IL_025e: Expected O, but got I
		//IL_0187: Expected O, but got I4
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Expected O, but got Unknown
		//IL_01ab: Expected O, but got I4
		//IL_009e: Expected O, but got I4
		//IL_01b9: Invalid comparison between O and F4
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Expected O, but got Unknown
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Expected O, but got Unknown
		//IL_01d8: Expected F4, but got O
		float outMin = default(float);
		float outAnchor = default(float);
		float outMax = default(float);
		bool clamp = default(bool);
		object obj2 = default(object);
		float num8 = default(float);
		while (true)
		{
			bool flag = intensity > MinColorIntensity;
			float inValue = intensity;
			if (!flag)
			{
				inValue = MinColorIntensity;
			}
			float num2;
			float ambientIntensity;
			object obj;
			if (RenderSettings.ambientMode == AmbientMode.Skybox)
			{
				Material skybox = RenderSettings.skybox;
				if (skybox != null)
				{
					ambientIntensity = MathUtils.MapWithAnchor(inValue, 0f, 50f, 100f, outMin, outAnchor, outMax, clamp);
					RenderSettings.ambientIntensity = ambientIntensity;
					float num = 50f;
					num2 = 100f;
					obj = 0;
					goto IL_0239;
				}
			}
			float num3 = MathUtils.MapWithAnchor(inValue, 0f, 50f, 100f, outMin, outAnchor, outMax, clamp);
			Color ambientLight = RenderSettings.ambientLight;
			float[] array = new float[3]
			{
				ambientLight.r,
				(float)obj2,
				(float)obj2
			};
			bool flag2 = array.Length == 0;
			float num4 = 0f;
			if (!flag2)
			{
				bool flag3 = array.Length <= 1;
				num4 = ambientLight.r;
				if (!flag3)
				{
					object obj3 = array.Length;
					object obj4 = array + 36;
					float num5 = ambientLight.r;
					object obj5 = 1;
					bool flag4;
					do
					{
						if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num5))
						{
							num5 = (float)obj4;
						}
						obj5++;
						obj4 += 4;
						flag4 = (nint)obj5 < array.Length;
						num4 = num5;
					}
					while (flag4);
				}
			}
			float num6 = num3 / num4;
			num2 = num6 * ambientLight.r;
			if (!(2f > num2))
			{
				num2 = 2f;
			}
			float num7 = (float)obj2 * num6;
			if (2f > num7)
			{
				float num = (float)obj2 * num6;
				if (!(2f > num))
				{
					num = 2f;
				}
			}
			RenderSettings.ambientLight = (Color)(&num8);
			ambientIntensity = 2f;
			obj = 0;
			goto IL_0239;
			IL_0239:
			nint num9 = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v350 @ r8_v2 (Il2CppClass<Kamgam.SettingsGenerator.AmbientLightConnection>)+258]");
			object obj6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v350 @ r8_v2 (Il2CppClass<Kamgam.SettingsGenerator.AmbientLightConnection>)+260]");
			object obj7 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v353 @ rax_v4 (should have been resolved before IL gen)");
		}
	}
}
