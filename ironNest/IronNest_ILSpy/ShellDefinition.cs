using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using SleepyNodes;
using UnityEngine;

public class ShellDefinition : ScriptableObject
{
	public string ShellId;

	public string DisplayName;

	public string Description;

	public ShellBlueprint BlueprintPrefab;

	public ImpactLocation ImpactEffectPrefab;

	public float ShellSpeed;

	public float shellSpeedVariationPercent;

	public int Damage;

	public float ImpactRadius;

	public int projectilesPerShell;

	public float horizontalDispersion;

	public float verticalDispersion;

	public bool IgnoreInTrackingShotsFired;

	public ImpactGraph Graph;

	public int maxPowderCharges;

	public int defaultPowderCharge;

	public PowderChargeRangeMapping[] chargeRangeMappings;

	public AnimationCurve chargeToSpeedMultiplier;

	public AnimationCurve chargeToHorizontalDispersionMultiplier;

	public AnimationCurve chargeToVerticalDispersionMultiplier;

	public unsafe ShellDefinition()
	{
		//IL_0008: Expected O, but got Ref
		//IL_08e7: Expected O, but got I
		//IL_08f7: Expected O, but got I
		//IL_0084: Expected O, but got I4
		//IL_00ce: Expected O, but got I4
		//IL_00ec: Expected I, but got O
		//IL_00fc: Expected O, but got I
		//IL_0164: Expected O, but got I4
		//IL_019e: Expected I, but got O
		//IL_01ae: Expected O, but got I
		//IL_0216: Expected O, but got I4
		//IL_0250: Expected I, but got O
		//IL_0260: Expected O, but got I
		//IL_02c8: Expected O, but got I4
		//IL_0302: Expected I, but got O
		//IL_0312: Expected O, but got I
		//IL_037a: Expected O, but got I4
		//IL_03b4: Expected I, but got O
		//IL_03c4: Expected O, but got I
		//IL_042c: Expected O, but got I4
		//IL_0466: Expected I, but got O
		//IL_0476: Expected O, but got I
		//IL_050d: Expected O, but got I4
		//IL_0516: Expected O, but got I4
		//IL_0528: Expected O, but got I4
		//IL_0530: Expected O, but got Ref
		//IL_05d5: Expected O, but got I4
		//IL_05e7: Expected O, but got I4
		//IL_05f0: Expected O, but got I4
		//IL_0602: Expected O, but got I4
		//IL_060a: Expected O, but got Ref
		//IL_0626: Expected O, but got Ref
		//IL_0658: Expected native int or pointer, but got O
		//IL_0670: Expected O, but got Ref
		//IL_06b7: Expected native int or pointer, but got O
		//IL_0726: Expected O, but got Ref
		//IL_0746: Expected native int or pointer, but got O
		//IL_0763: Expected O, but got I4
		//IL_0775: Expected O, but got I4
		//IL_077e: Expected O, but got I4
		//IL_0790: Expected O, but got I4
		//IL_07b4: Expected O, but got Ref
		//IL_07fb: Expected native int or pointer, but got O
		//IL_0835: Expected native int or pointer, but got O
		//IL_0843: Expected native int or pointer, but got O
		//IL_0851: Expected native int or pointer, but got O
		Keyframe keyframe2 = default(Keyframe);
		Keyframe keyframe = (Keyframe)(&keyframe2);
		ShellId = "HE";
		DisplayName = "HE";
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rax_v6+B8]");
		object description = 0;
		Description = (string)description;
		ShellSpeed = 0.7f;
		Damage = 1;
		ImpactRadius = 1f;
		projectilesPerShell = 1;
		maxPowderCharges = 6;
		defaultPowderCharge = 3;
		PowderChargeRangeMapping[] array = new PowderChargeRangeMapping[6];
		PowderChargeRangeMapping powderChargeRangeMapping = new PowderChargeRangeMapping();
		bool flag = powderChargeRangeMapping == null;
		object obj2 = 0;
		PowderChargeRangeMapping powderChargeRangeMapping2 = powderChargeRangeMapping;
		if (!flag)
		{
			powderChargeRangeMapping.chargeLevel = 1;
			powderChargeRangeMapping.maxRange = 5f;
			bool flag2 = array == null;
			obj2 = 0;
			powderChargeRangeMapping2 = powderChargeRangeMapping;
			if (!flag2)
			{
				nint num = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v279 @ rdx_v20 (Il2CppClass<PowderChargeRangeMapping[]>)+40]");
				obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj3 = default(object);
				bool flag3 = obj3 == null;
				powderChargeRangeMapping2 = powderChargeRangeMapping;
				if (flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					PowderChargeRangeMapping powderChargeRangeMapping3 = default(PowderChargeRangeMapping);
					throw powderChargeRangeMapping3;
				}
				array[0] = powderChargeRangeMapping;
				PowderChargeRangeMapping powderChargeRangeMapping4 = new PowderChargeRangeMapping();
				bool flag4 = powderChargeRangeMapping4 == null;
				obj2 = 0;
				powderChargeRangeMapping2 = powderChargeRangeMapping4;
				if (!flag4)
				{
					powderChargeRangeMapping4.chargeLevel = 2;
					powderChargeRangeMapping4.maxRange = 10f;
					nint num2 = (nint)array;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v688 @ rdx_v24 (Il2CppClass<PowderChargeRangeMapping[]>)+40]");
					object obj4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj5 = default(object);
					bool flag5 = obj5 == null;
					PowderChargeRangeMapping powderChargeRangeMapping5 = powderChargeRangeMapping4;
					if (flag5)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						PowderChargeRangeMapping powderChargeRangeMapping6 = default(PowderChargeRangeMapping);
						throw powderChargeRangeMapping6;
					}
					array[1] = powderChargeRangeMapping4;
					PowderChargeRangeMapping powderChargeRangeMapping7 = new PowderChargeRangeMapping();
					bool flag6 = powderChargeRangeMapping7 == null;
					obj2 = 0;
					powderChargeRangeMapping2 = powderChargeRangeMapping7;
					if (!flag6)
					{
						powderChargeRangeMapping7.chargeLevel = 3;
						powderChargeRangeMapping7.maxRange = 15f;
						nint num3 = (nint)array;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v827 @ rdx_v28 (Il2CppClass<PowderChargeRangeMapping[]>)+40]");
						object obj6 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						object obj7 = default(object);
						bool flag7 = obj7 == null;
						PowderChargeRangeMapping powderChargeRangeMapping8 = powderChargeRangeMapping7;
						if (flag7)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
							PowderChargeRangeMapping powderChargeRangeMapping9 = default(PowderChargeRangeMapping);
							throw powderChargeRangeMapping9;
						}
						array[2] = powderChargeRangeMapping7;
						PowderChargeRangeMapping powderChargeRangeMapping10 = new PowderChargeRangeMapping();
						bool flag8 = powderChargeRangeMapping10 == null;
						obj2 = 0;
						powderChargeRangeMapping2 = powderChargeRangeMapping10;
						if (!flag8)
						{
							powderChargeRangeMapping10.chargeLevel = 4;
							powderChargeRangeMapping10.maxRange = 20f;
							nint num4 = (nint)array;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v851 @ rdx_v32 (Il2CppClass<PowderChargeRangeMapping[]>)+40]");
							object obj8 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							object obj9 = default(object);
							bool flag9 = obj9 == null;
							PowderChargeRangeMapping powderChargeRangeMapping11 = powderChargeRangeMapping10;
							if (flag9)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								PowderChargeRangeMapping powderChargeRangeMapping12 = default(PowderChargeRangeMapping);
								throw powderChargeRangeMapping12;
							}
							array[3] = powderChargeRangeMapping10;
							PowderChargeRangeMapping powderChargeRangeMapping13 = new PowderChargeRangeMapping();
							bool flag10 = powderChargeRangeMapping13 == null;
							obj2 = 0;
							powderChargeRangeMapping2 = powderChargeRangeMapping13;
							if (!flag10)
							{
								powderChargeRangeMapping13.chargeLevel = 5;
								powderChargeRangeMapping13.maxRange = 25f;
								nint num5 = (nint)array;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v858 @ rdx_v36 (Il2CppClass<PowderChargeRangeMapping[]>)+40]");
								object obj10 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
								object obj11 = default(object);
								bool flag11 = obj11 == null;
								PowderChargeRangeMapping powderChargeRangeMapping14 = powderChargeRangeMapping13;
								if (flag11)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
									PowderChargeRangeMapping powderChargeRangeMapping15 = default(PowderChargeRangeMapping);
									throw powderChargeRangeMapping15;
								}
								array[4] = powderChargeRangeMapping13;
								PowderChargeRangeMapping powderChargeRangeMapping16 = new PowderChargeRangeMapping();
								bool flag12 = powderChargeRangeMapping16 == null;
								obj2 = 0;
								powderChargeRangeMapping2 = powderChargeRangeMapping16;
								if (!flag12)
								{
									powderChargeRangeMapping16.chargeLevel = 6;
									powderChargeRangeMapping16.maxRange = 30f;
									nint num6 = (nint)array;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v865 @ rdx_v40 (Il2CppClass<PowderChargeRangeMapping[]>)+40]");
									object obj12 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
									object obj13 = default(object);
									bool flag13 = obj13 == null;
									PowderChargeRangeMapping powderChargeRangeMapping17 = powderChargeRangeMapping16;
									if (flag13)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
										object obj14 = default(object);
										throw obj14;
									}
									array[5] = powderChargeRangeMapping16;
									chargeRangeMappings = array;
									Keyframe[] array2 = new Keyframe[2];
									Keyframe keyframe3 = new Keyframe(1f, 0.3f);
									bool flag14 = array2 == null;
									float num7 = 1f;
									object obj15 = 0;
									object obj16 = 0;
									float num8 = 0.3f;
									obj2 = 2;
									powderChargeRangeMapping2 = (PowderChargeRangeMapping)(&keyframe3);
									if (!flag14)
									{
										_ = 0;
										_ = 0;
										_ = 0;
										Keyframe keyframe4 = new Keyframe(6f, 1f);
										_ = 0;
										_ = 0;
										_ = 0;
										chargeToSpeedMultiplier = new AnimationCurve(array2);
										Keyframe[] array3 = new Keyframe[3];
										Keyframe keyframe5 = new Keyframe(1f, 1f);
										bool flag15 = array3 == null;
										object obj17 = 0;
										num7 = 1f;
										obj15 = 0;
										obj16 = 0;
										num8 = 1f;
										obj2 = 3;
										powderChargeRangeMapping2 = (PowderChargeRangeMapping)(&keyframe5);
										if (!flag15)
										{
											Keyframe keyframe6 = (Keyframe)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref keyframe2, 128));
											_ = 0;
											_ = 0;
											_ = 0;
											_ = 0;
											_ = 0;
											_ = 0;
											*(Keyframe*)(nint)keyframe6 = new Keyframe(3f, 1f);
											Keyframe keyframe7 = (Keyframe)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref keyframe2, 96));
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1 (UnityEngine.Keyframe)-80]");
											_ = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1 (UnityEngine.Keyframe)-70]");
											_ = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1 (UnityEngine.Keyframe)-68]");
											_ = 0;
											_ = 0;
											_ = 0;
											_ = 0;
											*(Keyframe*)(nint)keyframe7 = new Keyframe(6f, 1f);
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1 (UnityEngine.Keyframe)-60]");
											_ = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1 (UnityEngine.Keyframe)-50]");
											_ = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1 (UnityEngine.Keyframe)-48]");
											_ = 0;
											chargeToHorizontalDispersionMultiplier = new AnimationCurve(array3);
											Keyframe[] array4 = new Keyframe[3];
											Keyframe keyframe8 = (Keyframe)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref keyframe2, 64));
											_ = 0;
											_ = 0;
											_ = 0;
											*(Keyframe*)(nint)keyframe8 = new Keyframe(1f, 1f);
											bool flag16 = array4 == null;
											obj17 = 0;
											num7 = 1f;
											obj15 = 0;
											obj16 = 0;
											num8 = 1f;
											obj2 = 3;
											powderChargeRangeMapping2 = (PowderChargeRangeMapping)keyframe8;
											if (!flag16)
											{
												Keyframe keyframe9 = (Keyframe)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref keyframe2, 32));
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1 (UnityEngine.Keyframe)-40]");
												_ = 0;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1 (UnityEngine.Keyframe)-30]");
												_ = 0;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1 (UnityEngine.Keyframe)-28]");
												_ = 0;
												_ = 0;
												_ = 0;
												_ = 0;
												*(Keyframe*)(nint)keyframe9 = new Keyframe(3f, 1f);
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1 (UnityEngine.Keyframe)-20]");
												_ = 0;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1 (UnityEngine.Keyframe)-10]");
												_ = 0;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1 (UnityEngine.Keyframe)-8]");
												_ = 0;
												((Keyframe*)(nint)keyframe)->m_WeightedMode = 0;
												((Keyframe*)(nint)keyframe)->m_OutWeight = 0f;
												((Keyframe*)(nint)keyframe)->m_Time = 0f;
												keyframe2 = new Keyframe(6f, 1f);
												_ = keyframe.m_Time;
												_ = keyframe.m_WeightedMode;
												_ = keyframe.m_OutWeight;
												chargeToVerticalDispersionMultiplier = new AnimationCurve(array4);
												base._002Ector();
												return;
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		throw new NullReferenceException();
	}
}
