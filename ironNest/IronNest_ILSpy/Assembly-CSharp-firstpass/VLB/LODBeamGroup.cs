using System;
using Cpp2ILInjected;
using UnityEngine;

namespace VLB;

public class LODBeamGroup : MonoBehaviour
{
	private VolumetricLightBeamAbstractBase[] m_LODBeams;

	private bool m_ResetAllLODsLocalTransform;

	private BeamProps m_LOD0PropsToCopy;

	private bool m_CopyLOD0PropsEachFrame;

	private bool m_CullVolumetricDustParticles;

	private LODGroup m_LODGroup;

	private void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		LODGroup lODGroup = default(LODGroup);
		m_LODGroup = lODGroup;
		SetupLodGroupData();
	}

	private void Start()
	{
		UnifyBeamsProperties();
	}

	public LOD[] GetLODsFromLODGroup()
	{
		if ((object)m_LODGroup != null)
		{
			return m_LODGroup.GetLODs();
		}
		return (LOD[])(object)new NullReferenceException();
	}

	private void SetLODRenderer(int lodIdx, Renderer renderer)
	{
		//IL_011c: Expected I, but got O
		//IL_005f: Expected I, but got O
		//IL_0138: Expected I, but got O
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Expected I, but got Unknown
		//IL_0101: Expected I4, but got O
		//IL_0092: Expected I, but got O
		//IL_00c3: Expected I, but got O
		Renderer renderer2 = default(Renderer);
		if ((bool)renderer2)
		{
			Renderer[] array = new Renderer[1];
			bool flag = array == null;
			int num = 1;
			nint num2 = (nint)typeof(Renderer[]);
			if (flag)
			{
				goto IL_0142;
			}
			if ((object)renderer2 != null)
			{
				nint num3 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v157 @ rdx_v11 (Il2CppClass<UnityEngine.Renderer>)+40]");
				num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj = default(object);
				bool flag2 = obj == null;
				num2 = (nint)renderer2;
				if (flag2)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj2 = default(object);
					throw obj2;
				}
			}
			num2 = (nint)(array + 32);
			array[0] = renderer2;
			Renderer renderer3 = (Renderer)(object)array;
			num = (int)renderer2;
		}
		else
		{
			Renderer renderer3 = null;
			int num = 0;
			nint num2 = (nint)renderer2;
		}
		if ((object)this != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 136 Invalid \"Jump target not found in method: 0x18037CFC0\"");
			int num = lodIdx;
			nint num2 = (nint)this;
		}
		goto IL_0142;
		IL_0142:
		throw new NullReferenceException();
	}

	private void SetLODRenderers(int lodIdx, Renderer[] renderers)
	{
		//IL_0021: Expected O, but got I4
		LOD[] lODs = m_LODGroup.GetLODs();
		object obj = lodIdx + lodIdx;
		m_LODGroup.SetLODs(lODs);
	}

	private void SetLOD(int lodIdx)
	{
		//IL_0085: Expected O, but got I
		//IL_00bb: Expected I, but got O
		//IL_0135: Expected O, but got I
		//IL_017d: Expected O, but got I
		//IL_0444: Expected O, but got I4
		//IL_0545: Expected O, but got I
		//IL_01ce: Expected O, but got I
		//IL_0476: Expected O, but got I4
		//IL_0486: Expected O, but got I
		//IL_04bb: Expected O, but got I4
		//IL_04cb: Expected O, but got I
		//IL_0512: Expected O, but got I
		//IL_0512: Expected O, but got I
		//IL_038f: Expected O, but got I
		//IL_032d: Expected I, but got O
		//IL_0367: Expected O, but got I
		//IL_03b4: Expected I, but got O
		bool flag = (object)m_LODGroup == null;
		int num = lodIdx;
		UnityEngine.Object lODGroup = m_LODGroup;
		LOD[] lODs;
		if (!flag)
		{
			lODs = m_LODGroup.GetLODs();
			if (!Utils.IsValidIndex(lODs, lodIdx))
			{
				return;
			}
			lODGroup = (UnityEngine.Object)(object)m_LODBeams;
			bool flag2 = m_LODBeams == null;
			num = lodIdx;
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ rcx_v5 (UnityEngine.Object)+20+lodIdx @ rdx (System.Int32)*8]");
				lODGroup = (UnityEngine.Object)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ rcx_v5 (UnityEngine.Object)+20+lodIdx @ rdx (System.Int32)*8]");
				bool flag3 = (nint)0 == 0;
				num = lodIdx;
				if (!flag3)
				{
					nint num2 = (nint)lODGroup;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v476 @ rdx_v10 (Il2CppClass<UnityEngine.Object>)+178] (should have been resolved before IL gen)");
					UnityEngine.Object obj = default(UnityEngine.Object);
					if (!obj)
					{
						return;
					}
					bool flag4 = (object)obj == null;
					num = 0;
					lODGroup = obj;
					if (!flag4)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v479 @ rax_v16 (UnityEngine.Object)+20]");
						if (!(UnityEngine.Object)0)
						{
							return;
						}
						bool flag5 = !m_CullVolumetricDustParticles;
						num = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v479 @ rax_v16 (UnityEngine.Object)+20]");
						lODGroup = (UnityEngine.Object)0;
						if (flag5)
						{
							goto IL_041a;
						}
						lODGroup = (UnityEngine.Object)(object)m_LODBeams;
						bool flag6 = m_LODBeams == null;
						num = 0;
						if (!flag6)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ rcx_v5 (UnityEngine.Object)+20+lodIdx @ rdx (System.Int32)*8]");
							lODGroup = (UnityEngine.Object)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ rcx_v5 (UnityEngine.Object)+20+lodIdx @ rdx (System.Int32)*8]");
							bool flag7 = (nint)0 == 0;
							num = 0;
							if (!flag7)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
								UnityEngine.Object obj2 = default(UnityEngine.Object);
								bool flag8 = obj2;
								bool flag9 = !flag8;
								num = 0;
								lODGroup = obj2;
								if (flag9)
								{
									goto IL_041a;
								}
								bool flag10 = (object)obj2 == null;
								num = 0;
								lODGroup = obj2;
								if (!flag10)
								{
									ParticleSystemRenderer particleSystemRenderer = ((VolumetricDustParticles)obj2).FindRenderer();
									bool flag11 = particleSystemRenderer;
									bool flag12 = !flag11;
									num = 0;
									lODGroup = particleSystemRenderer;
									if (flag12)
									{
										goto IL_041a;
									}
									Renderer[] array = new Renderer[2];
									bool flag13 = array == null;
									num = 2;
									lODGroup = (UnityEngine.Object)(object)typeof(Renderer[]);
									if (!flag13)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v479 @ rax_v16 (UnityEngine.Object)+20]");
										if ((nint)0 != 0)
										{
											nint num3 = (nint)array;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v633 @ rdx_v30 (Il2CppClass<UnityEngine.Renderer[]>)+40]");
											num = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
											object obj3 = default(object);
											bool flag14 = obj3 == null;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v479 @ rax_v16 (UnityEngine.Object)+20]");
											lODGroup = (UnityEngine.Object)0;
											if (flag14)
											{
												bool flag15 = Utils.IsValidIndex((LOD[])(object)lODGroup, num);
												throw flag15;
											}
										}
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v479 @ rax_v16 (UnityEngine.Object)+20]");
										array[0] = (Renderer)0;
										if ((object)particleSystemRenderer != null)
										{
											nint num4 = (nint)array;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v658 @ rdx_v28 (Il2CppClass<UnityEngine.Renderer[]>)+40]");
											int idx = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
											object obj4 = default(object);
											bool flag16 = obj4 == null;
											LOD[] array2 = (LOD[])(object)particleSystemRenderer;
											if (flag16)
											{
												bool flag17 = Utils.IsValidIndex(array2, idx);
												throw flag17;
											}
										}
										array[1] = particleSystemRenderer;
										SetLODRenderers(lodIdx, array);
										return;
									}
								}
							}
						}
					}
				}
			}
		}
		goto IL_054a;
		IL_041a:
		if (lODs != null)
		{
			object obj5 = lodIdx + lodIdx;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rax_v12 (UnityEngine.LOD[])+28+v550 @ rax_v23*8]");
			if ((nint)0 != 0)
			{
				object obj6 = lodIdx + lodIdx;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rax_v12 (UnityEngine.LOD[])+28+v564 @ rax_v27*8]");
				LOD lOD = (LOD)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v565 @ rax_v28 (UnityEngine.LOD)+18]");
				if ((nint)0 == 1)
				{
					object obj7 = lodIdx + lodIdx;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rax_v12 (UnityEngine.LOD[])+28+v85 @ rax_v30*8]");
					lODGroup = (UnityEngine.Object)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rax_v12 (UnityEngine.LOD[])+28+v85 @ rax_v30*8]");
					if ((nint)0 == 0)
					{
						goto IL_054a;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ rcx_v5 (UnityEngine.Object)+20]");
					nint num5 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v479 @ rax_v16 (UnityEngine.Object)+20]");
					if (!((UnityEngine.Object)num5 != (UnityEngine.Object)0))
					{
						return;
					}
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v479 @ rax_v16 (UnityEngine.Object)+20]");
			SetLODRenderer(lodIdx, (Renderer)0);
			return;
		}
		goto IL_054a;
		IL_054a:
		throw new NullReferenceException();
	}

	private void OnBeamGeometryGenerated(VolumetricLightBeamAbstractBase beam)
	{
		//IL_0063: Expected O, but got I4
		//IL_00a3: Expected O, but got I
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Expected O, but got Unknown
		LOD[] lODs = m_LODGroup.GetLODs();
		if (lODs == null || m_LODBeams == null)
		{
			return;
		}
		VolumetricLightBeamAbstractBase[] lODBeams = m_LODBeams;
		object obj = 32;
		int num = 0;
		int num2 = 0;
		while (num2 < lODBeams.Length)
		{
			VolumetricLightBeamAbstractBase[] lODBeams2 = m_LODBeams;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ rsi_v5+v66 @ rax_v8 (VLB.VolumetricLightBeamAbstractBase[])]");
			if ((UnityEngine.Object)0 != beam)
			{
				lODBeams = m_LODBeams;
				num++;
				obj += 8;
				num2 = num;
				continue;
			}
			SetLOD(num);
			break;
		}
	}

	private unsafe void SetupLodGroupData()
	{
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Expected Ref, but got Unknown
		//IL_00d2: Expected O, but got I4
		//IL_010f: Expected O, but got I
		//IL_015b: Expected O, but got I
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Expected O, but got Unknown
		if (!(m_LODGroup != null))
		{
			return;
		}
		LOD[] lODs = m_LODGroup.GetLODs();
		if (lODs == null)
		{
			return;
		}
		if (m_LODBeams != null)
		{
			VolumetricLightBeamAbstractBase[] lODBeams = m_LODBeams;
			if (lODBeams.Length >= lODs.Length)
			{
				goto IL_00ba;
			}
		}
		Utils.ResizeArray(ref *(VolumetricLightBeamAbstractBase[]*)(this + 32), lODs.Length);
		goto IL_00ba;
		IL_00ba:
		VolumetricLightBeamAbstractBase[] lODBeams2 = m_LODBeams;
		object obj = 32;
		int num = 0;
		for (int num2 = 0; num2 < lODBeams2.Length; num2 = num)
		{
			VolumetricLightBeamAbstractBase[] lODBeams3 = m_LODBeams;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v152 @ rsi_v7+v174 @ rax_v14 (VLB.VolumetricLightBeamAbstractBase[])]");
			if ((UnityEngine.Object)0 != null)
			{
				VolumetricLightBeamAbstractBase[] lODBeams4 = m_LODBeams;
				VolumetricLightBeamAbstractBase.BeamGeometryGeneratedHandler callback = OnBeamGeometryGenerated;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v152 @ rsi_v7+v175 @ rax_v20 (VLB.VolumetricLightBeamAbstractBase[])]");
				((VolumetricLightBeamAbstractBase)0).RegisterBeamGeometryGeneratedCallback(callback);
			}
			else if (num < lODs.Length)
			{
				SetLODRenderer(num, null);
			}
			lODBeams2 = m_LODBeams;
			num++;
			obj += 8;
		}
	}

	private unsafe void UnifyBeamsProperties()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Expected O, but got Unknown
		//IL_0047: Expected O, but got I4
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Expected O, but got Unknown
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Expected O, but got Unknown
		//IL_01ad: Expected O, but got I4
		//IL_01b6: Expected O, but got I4
		//IL_01bf: Expected O, but got I4
		//IL_0097: Expected O, but got Ref
		//IL_00bb: Expected O, but got Ref
		//IL_01e3: Expected O, but got I
		//IL_01f9: Expected O, but got I
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Expected O, but got Unknown
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Expected O, but got Unknown
		//IL_00e0: Expected O, but got Ref
		//IL_021e: Expected I, but got O
		//IL_023e: Expected O, but got I
		//IL_0254: Expected O, but got I
		//IL_026f: Expected O, but got I
		//IL_0285: Expected O, but got I
		if (m_LODBeams == null)
		{
			return;
		}
		if (m_ResetAllLODsLocalTransform)
		{
			VolumetricLightBeamAbstractBase[] lODBeams = m_LODBeams;
			object obj = m_LODBeams + 32;
			object obj2 = 0;
			Vector3 zeroVector = default(Vector3);
			Quaternion identityQuaternion = default(Quaternion);
			Vector3 vector = default(Vector3);
			while ((nint)obj2 < lODBeams.Length)
			{
				if ((bool)(UnityEngine.Object)obj)
				{
					Transform transform = ((Component)obj).transform;
					transform.localPosition = (Vector3)(&zeroVector);
					Transform transform2 = ((Component)obj).transform;
					transform2.localRotation = (Quaternion)(&identityQuaternion);
					Transform transform3 = ((Component)obj).transform;
					transform3.localScale = (Vector3)(&vector);
					identityQuaternion = Quaternion.identityQuaternion;
					zeroVector = Vector3.zeroVector;
				}
				obj2++;
				obj += 8;
			}
		}
		if (m_LOD0PropsToCopy == (BeamProps)0)
		{
			return;
		}
		VolumetricLightBeamAbstractBase[] lODBeams2 = m_LODBeams;
		if (lODBeams2.Length <= 1 || !(lODBeams2[0] != null))
		{
			return;
		}
		VolumetricLightBeamAbstractBase[] lODBeams3 = m_LODBeams;
		object obj3 = 1;
		object obj4 = 40;
		object obj5 = 1;
		while ((nint)obj5 < lODBeams3.Length)
		{
			VolumetricLightBeamAbstractBase[] lODBeams4 = m_LODBeams;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v303 @ r14_v8+v396 @ rax_v14 (VLB.VolumetricLightBeamAbstractBase[])]");
			UnityEngine.Object obj6 = (UnityEngine.Object)0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v303 @ r14_v8+v396 @ rax_v14 (VLB.VolumetricLightBeamAbstractBase[])]");
			if ((bool)(UnityEngine.Object)0)
			{
				nint num = (nint)obj6;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v677 @ r9_v7 (Il2CppClass<UnityEngine.Object>)+1C8] (should have been resolved before IL gen)");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v303 @ r14_v8+v396 @ rax_v14 (VLB.VolumetricLightBeamAbstractBase[])]");
				UtilsBeamProps.SetColorFromLight((VolumetricLightBeamAbstractBase)0, fromLight: false);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v303 @ r14_v8+v396 @ rax_v14 (VLB.VolumetricLightBeamAbstractBase[])]");
				UtilsBeamProps.SetFallOffEndFromLight((VolumetricLightBeamAbstractBase)0, fromLight: false);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v303 @ r14_v8+v396 @ rax_v14 (VLB.VolumetricLightBeamAbstractBase[])]");
				UtilsBeamProps.SetIntensityFromLight((VolumetricLightBeamAbstractBase)0, fromLight: false);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v303 @ r14_v8+v396 @ rax_v14 (VLB.VolumetricLightBeamAbstractBase[])]");
				UtilsBeamProps.SetSpotAngleFromLight((VolumetricLightBeamAbstractBase)0, fromLight: false);
			}
			lODBeams3 = m_LODBeams;
			obj3++;
			obj4 += 8;
			obj5 = obj3;
		}
	}

	private void Update()
	{
		if (m_CopyLOD0PropsEachFrame)
		{
			UnifyBeamsProperties();
		}
	}

	public LODBeamGroup()
	{
		//IL_000f: Expected I4, but got I8
		m_LOD0PropsToCopy = (BeamProps)(-1);
		m_CullVolumetricDustParticles = true;
		base._002Ector();
	}
}
