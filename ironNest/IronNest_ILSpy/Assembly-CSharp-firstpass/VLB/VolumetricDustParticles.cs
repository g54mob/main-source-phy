using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace VLB;

public class VolumetricDustParticles : MonoBehaviour
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Action<ParticleSystem> _003C_003E9__34_0;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal void _003CInstantiateParticleSystem_003Eb__34_0(ParticleSystem ps)
		{
			GameObject gameObject = ps.gameObject;
			UnityEngine.Object.DestroyImmediate(gameObject);
		}
	}

	public const string ClassName = "VolumetricDustParticles";

	public float alpha;

	public float size;

	public ParticlesDirection direction;

	public Vector3 velocity;

	public float speed;

	public float density;

	public MinMaxRangeFloat spawnDistanceRange;

	public float spawnMinDistance;

	public float spawnMaxDistance;

	public bool cullingEnabled;

	public float cullingMaxDistance;

	private bool _003CisCulled_003Ek__BackingField;

	private float m_AlphaAdditionalRuntime;

	private ParticleSystem m_Particles;

	private ParticleSystemRenderer m_Renderer;

	private Material m_Material;

	private Gradient m_GradientCached;

	private bool m_RuntimePropertiesDirty;

	private VolumetricLightBeamAbstractBase m_Master;

	public bool isCulled
	{
		get
		{
			return _003CisCulled_003Ek__BackingField;
		}
		private set
		{
			_003CisCulled_003Ek__BackingField = value;
		}
	}

	public float alphaAdditionalRuntime
	{
		get
		{
			return m_AlphaAdditionalRuntime;
		}
		set
		{
			bool flag = m_AlphaAdditionalRuntime == value;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018039F55Ch\"");
			if (!flag)
			{
				m_AlphaAdditionalRuntime = value;
				m_RuntimePropertiesDirty = true;
			}
		}
	}

	public bool particlesAreInstantiated => m_Particles;

	public int particlesCurrentCount
	{
		get
		{
			//IL_0074: Expected I4, but got O
			if ((bool)m_Particles)
			{
				if ((object)m_Particles != null)
				{
					return m_Particles.particleCount;
				}
				NullReferenceException ex = new NullReferenceException();
				return (int)ex;
			}
			return 0;
		}
	}

	public int particlesMaxCount
	{
		get
		{
			//IL_0081: Expected I4, but got O
			if ((bool)m_Particles)
			{
				if ((object)m_Particles != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18049AB00");
					ParticleSystem.MainModule mainModule = default(ParticleSystem.MainModule);
					return mainModule.maxParticles;
				}
				NullReferenceException ex = new NullReferenceException();
				return (int)ex;
			}
			return 0;
		}
	}

	public ParticleSystemRenderer FindRenderer()
	{
		if (!m_Renderer)
		{
			if ((object)m_Particles != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				ParticleSystemRenderer result = default(ParticleSystemRenderer);
				return result;
			}
			return (ParticleSystemRenderer)(object)new NullReferenceException();
		}
		return m_Renderer;
	}

	private void Start()
	{
		//IL_00d9: Expected O, but got I4
		_003CisCulled_003Ek__BackingField = false;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		VolumetricLightBeamAbstractBase master = default(VolumetricLightBeamAbstractBase);
		m_Master = master;
		VolumetricLightBeamAbstractBase master2 = m_Master;
		if (master2.pluginVersion != -1 && master2.pluginVersion != 20205)
		{
			if (master2.pluginVersion < 1880)
			{
				bool flag = direction == ParticlesDirection.Random;
				Vector3 vector = default(Vector3);
				velocity = vector;
				direction = (flag ? ParticlesDirection.LocalSpace : ParticlesDirection.Random);
				_ = speed;
			}
			else if (master2.pluginVersion >= 1940)
			{
				goto IL_010c;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18037FDF0");
			spawnDistanceRange = (MinMaxRangeFloat)0;
		}
		goto IL_010c;
		IL_010c:
		InstantiateParticleSystem();
		SetActiveAndPlay();
	}

	private void InstantiateParticleSystem()
	{
		GameObject self = base.gameObject;
		Action<ParticleSystem> lambda = _003C_003Ec._003C_003E9__34_0;
		if (_003C_003Ec._003C_003E9__34_0 == null)
		{
			lambda = (_003C_003Ec._003C_003E9__34_0 = delegate(ParticleSystem ps)
			{
				GameObject obj = ps.gameObject;
				UnityEngine.Object.DestroyImmediate(obj);
			});
		}
		Utils.ForeachComponentsInDirectChildrenOnly(self, lambda, includeInactive: true);
		Config instance = Config.Instance;
		ParticleSystem particles = instance.NewVolumetricDustParticles();
		m_Particles = particles;
		if ((bool)m_Particles)
		{
			Transform transform = m_Particles.transform;
			Transform parent = base.transform;
			transform.SetParent(parent, worldPositionStays: false);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			ParticleSystemRenderer renderer = default(ParticleSystemRenderer);
			m_Renderer = renderer;
			Material sharedMaterial = ((Renderer)m_Renderer).GetSharedMaterial();
			Material material = new Material(sharedMaterial);
			m_Material = material;
			((Renderer)m_Renderer).SetMaterial(m_Material);
		}
	}

	private void OnEnable()
	{
		SetActiveAndPlay();
	}

	private void SetActive(bool active)
	{
		if ((bool)m_Particles)
		{
			GameObject gameObject = m_Particles.gameObject;
			gameObject.SetActive(active);
		}
	}

	private void SetActiveAndPlay()
	{
		SetActive(active: true);
		if ((bool)m_Particles)
		{
			SetParticleProperties();
			m_Particles.Simulate(0f);
			m_Particles.Play(withChildren: true);
		}
	}

	private void Play()
	{
		if ((bool)m_Particles)
		{
			SetParticleProperties();
			m_Particles.Simulate(0f);
			m_Particles.Play(withChildren: true);
		}
	}

	private void OnDisable()
	{
		SetActive(active: false);
	}

	private void OnDestroy()
	{
		if ((bool)m_Particles)
		{
			GameObject obj = m_Particles.gameObject;
			UnityEngine.Object.DestroyImmediate(obj);
			m_Particles = null;
		}
		if ((bool)m_Material)
		{
			UnityEngine.Object.DestroyImmediate(m_Material);
			m_Material = null;
		}
	}

	private unsafe void Update()
	{
		//IL_0013: Expected I, but got O
		//IL_001b: Expected I, but got O
		//IL_002b: Expected O, but got I
		//IL_0067: Expected O, but got I
		//IL_008c: Expected O, but got I4
		//IL_016f: Expected O, but got Ref
		UpdateCulling();
		VolumetricLightBeamAbstractBase master = m_Master;
		bool flag = (object)m_Master == null;
		UnityEngine.Object obj = null;
		UnityEngine.Object obj4;
		if (!flag)
		{
			nint num = (nint)typeof(VolumetricLightBeamSD);
			nint num2 = (nint)master;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rdx_v15 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r8_v7 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rdx_v15 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r8_v7 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v122 @ rax_v24+FFFFFFF8+v67 @ rax_v20*8]");
				bool flag2 = 0 == (nint)typeof(VolumetricLightBeamSD);
				obj4 = (UnityEngine.Object)1;
				if (flag2)
				{
					goto IL_01b8;
				}
			}
			obj4 = null;
			goto IL_01b8;
		}
		goto IL_00b3;
		IL_01b8:
		bool flag3 = (object)obj4 == null;
		obj = null;
		if (!flag3)
		{
			obj = m_Master;
		}
		goto IL_00b3;
		IL_00b3:
		if ((bool)obj)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ rbx_v2 (UnityEngine.Object)+120]");
			if ((nint)0 == 0)
			{
				goto IL_0109;
			}
		}
		SetParticleProperties();
		goto IL_0109;
		IL_0109:
		if (m_RuntimePropertiesDirty && m_Material != null)
		{
			object obj5 = default(object);
			m_Material.SetColor(ShaderProperties.ParticlesTintColor, (Color)(&obj5));
			m_RuntimePropertiesDirty = false;
		}
	}

	private unsafe void SetParticleProperties()
	{
		//IL_0008: Expected O, but got Ref
		//IL_1526: Expected O, but got I4
		//IL_00a5: Expected I, but got O
		//IL_00ad: Expected I, but got O
		//IL_00bd: Expected O, but got I
		//IL_00f9: Expected O, but got I
		//IL_011e: Expected O, but got I4
		//IL_029d: Expected O, but got Ref
		//IL_019a: Expected I, but got O
		//IL_01a2: Expected I, but got O
		//IL_01b2: Expected O, but got I
		//IL_01ee: Expected O, but got I
		//IL_0213: Expected O, but got I4
		//IL_15c6: Expected I, but got O
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Expected O, but got Unknown
		//IL_15fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1600: Expected O, but got Unknown
		//IL_1610: Unknown result type (might be due to invalid IL or missing references)
		//IL_1615: Expected O, but got Unknown
		//IL_0385: Expected O, but got Ref
		//IL_15a5: Expected F4, but got O
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Expected O, but got Unknown
		//IL_163a: Invalid comparison between F4 and O
		//IL_0338: Expected O, but got I4
		//IL_03fc: Expected O, but got Ref
		//IL_041c: Expected O, but got Ref
		//IL_0448: Expected O, but got Ref
		//IL_165c: Expected I, but got O
		//IL_167c: Expected F4, but got I
		//IL_1685: Expected F4, but got O
		//IL_0465: Expected O, but got Ref
		//IL_047e: Expected O, but got Ref
		//IL_048b: Expected O, but got Ref
		//IL_049e: Expected O, but got Ref
		//IL_04b9: Expected O, but got Ref
		//IL_04ea: Expected O, but got Ref
		//IL_0514: Expected O, but got Ref
		//IL_0543: Expected O, but got Ref
		//IL_0550: Expected O, but got Ref
		//IL_055e: Expected O, but got Ref
		//IL_05bb: Expected I, but got O
		//IL_05c3: Expected I, but got O
		//IL_05d3: Expected O, but got I
		//IL_060f: Expected O, but got I
		//IL_0634: Expected O, but got I4
		//IL_06b0: Expected I, but got O
		//IL_06b8: Expected I, but got O
		//IL_06c8: Expected O, but got I
		//IL_0704: Expected O, but got I
		//IL_0729: Expected O, but got I4
		//IL_0aa9: Expected I, but got O
		//IL_0ab1: Expected I, but got O
		//IL_0ac1: Expected O, but got I
		//IL_080c: Expected I, but got O
		//IL_0814: Expected I, but got O
		//IL_0824: Expected O, but got I
		//IL_0afd: Expected O, but got I
		//IL_0b22: Expected O, but got I4
		//IL_0860: Expected O, but got I
		//IL_0885: Expected O, but got I4
		//IL_0c6c: Expected O, but got Ref
		//IL_0c79: Expected O, but got Ref
		//IL_0c87: Expected O, but got Ref
		//IL_09c4: Expected O, but got I
		//IL_09d9: Expected O, but got I
		//IL_0b9e: Expected I, but got O
		//IL_0ba6: Expected I, but got O
		//IL_0bb6: Expected O, but got I
		//IL_09eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f0: Expected O, but got Unknown
		//IL_0cfd: Expected F4, but got I4
		//IL_0901: Expected I, but got O
		//IL_0909: Expected I, but got O
		//IL_0919: Expected O, but got I
		//IL_183d: Expected O, but got Ref
		//IL_1856: Expected O, but got Ref
		//IL_1863: Expected O, but got Ref
		//IL_0cef: Expected F4, but got I
		//IL_0bf2: Expected O, but got I
		//IL_0c17: Expected O, but got I4
		//IL_0d3c: Expected O, but got Ref
		//IL_0a73: Expected O, but got I
		//IL_0a08: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0d: Expected O, but got Unknown
		//IL_0a26: Expected O, but got F4
		//IL_0a2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a34: Expected O, but got Unknown
		//IL_0955: Expected O, but got I
		//IL_097a: Expected O, but got I4
		//IL_0d86: Expected O, but got I4
		//IL_0da3: Expected O, but got Ref
		//IL_0dc9: Expected O, but got Ref
		//IL_0dd8: Expected F4, but got O
		//IL_0deb: Expected O, but got Ref
		//IL_0e00: Expected F4, but got I
		//IL_0e0e: Expected O, but got Ref
		//IL_0e23: Expected F4, but got I
		//IL_0e36: Expected O, but got Ref
		//IL_0e43: Expected I4, but got O
		//IL_0e88: Expected O, but got Ref
		//IL_0eb3: Invalid comparison between I4 and F4
		//IL_0f08: Expected F4, but got I4
		//IL_1886: Expected O, but got Ref
		//IL_18da: Invalid comparison between I4 and F4
		//IL_0f4c: Expected F4, but got I4
		//IL_1951: Expected F4, but got O
		//IL_0f88: Expected F4, but got I4
		//IL_199c: Expected O, but got Ref
		//IL_19d5: Expected O, but got Ref
		//IL_19f0: Expected O, but got Ref
		//IL_19fd: Expected O, but got Ref
		//IL_0f9b: Expected O, but got Ref
		//IL_1a10: Expected O, but got Ref
		//IL_0fda: Expected F4, but got I4
		//IL_1009: Expected O, but got Ref
		//IL_1034: Expected O, but got Ref
		//IL_1092: Expected I, but got O
		//IL_109a: Expected I, but got O
		//IL_10aa: Expected O, but got I
		//IL_107a: Expected I, but got O
		//IL_10e6: Expected O, but got I
		//IL_110b: Expected O, but got I4
		//IL_118f: Expected I, but got O
		//IL_1197: Expected I, but got O
		//IL_11a7: Expected O, but got I
		//IL_12bf: Expected I, but got O
		//IL_12c7: Expected I, but got O
		//IL_12d7: Expected O, but got I
		//IL_12a7: Expected I, but got O
		//IL_11e3: Expected O, but got I
		//IL_1208: Expected O, but got I4
		//IL_1313: Expected O, but got I
		//IL_1338: Expected O, but got I4
		//IL_1260: Expected I, but got O
		//IL_14b6: Expected O, but got I
		//IL_14ca: Expected I4, but got O
		//IL_13bc: Expected I, but got O
		//IL_13c4: Expected I, but got O
		//IL_13d4: Expected O, but got I
		//IL_1410: Expected O, but got I
		//IL_1435: Expected O, but got I4
		//IL_148f: Expected I, but got O
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		obj = 0;
		if (!m_Particles)
		{
			return;
		}
		GameObject gameObject = m_Particles.gameObject;
		if (!gameObject.activeSelf)
		{
			return;
		}
		Transform transform = m_Particles.transform;
		VolumetricLightBeamAbstractBase master = m_Master;
		UnityEngine.Object obj3;
		if ((object)m_Master == null)
		{
			obj3 = null;
			goto IL_0145;
		}
		nint num = (nint)typeof(VolumetricLightBeamSD);
		nint num2 = (nint)master;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v954 @ rdx_v109 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v955 @ r8_v87 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v954 @ rdx_v109 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		UnityEngine.Object obj6;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v955 @ r8_v87 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
			object obj5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1010 @ rax_v246+FFFFFFF8+v956 @ rax_v242*8]");
			bool flag = 0 == (nint)typeof(VolumetricLightBeamSD);
			obj6 = (UnityEngine.Object)1;
			if (flag)
			{
				goto IL_1553;
			}
		}
		obj6 = null;
		goto IL_1553;
		IL_122f:
		UnityEngine.Object obj7;
		int sortingLayerID = default(int);
		if (!obj7)
		{
			sortingLayerID = 0;
		}
		else
		{
			nint num4 = (nint)obj7;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v3010 @ rdx_v82 (Il2CppClass<UnityEngine.Object>)+1F8] (should have been resolved before IL gen)");
		}
		goto IL_1284;
		IL_16b1:
		UnityEngine.Object obj8;
		bool flag2 = (object)obj8 == null;
		UnityEngine.Object obj9 = null;
		if (!flag2)
		{
			obj9 = m_Master;
		}
		goto IL_065b;
		IL_0b49:
		UnityEngine.Object obj10;
		UnityEngine.Object obj11;
		VolumetricLightBeamAbstractBase master2;
		UnityEngine.Object obj14;
		if (!obj10)
		{
			if ((object)m_Master == null)
			{
				obj11 = null;
				goto IL_0c3e;
			}
			nint num5 = (nint)typeof(VolumetricLightBeamHD);
			nint num6 = (nint)master2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2126 @ rdx_v95 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
			object obj12 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2127 @ r8_v75 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
			nint num7 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2126 @ rdx_v95 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
			if (num7 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2127 @ r8_v75 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
				object obj13 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2204 @ rax_v172+FFFFFFF8+v2128 @ rax_v168*8]");
				bool flag3 = 0 == (nint)typeof(VolumetricLightBeamHD);
				obj14 = (UnityEngine.Object)1;
				if (flag3)
				{
					goto IL_17f6;
				}
			}
			obj14 = null;
			goto IL_17f6;
		}
		goto IL_1818;
		IL_1367:
		UnityEngine.Object obj15;
		UnityEngine.Object obj16;
		UnityEngine.Object master3;
		UnityEngine.Object obj19;
		nint num9;
		if (!obj15)
		{
			if ((object)m_Master == null)
			{
				obj16 = null;
				goto IL_145c;
			}
			nint num8 = (nint)typeof(VolumetricLightBeamHD);
			num9 = (nint)master3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3130 @ rdx_v78 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
			object obj17 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v337 @ r8_v62 (Il2CppClass<UnityEngine.Object>)+130]");
			nint num10 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3130 @ rdx_v78 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
			if (num10 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v337 @ r8_v62 (Il2CppClass<UnityEngine.Object>)+C8]");
				object obj18 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3187 @ rax_v119+FFFFFFF8+v3132 @ rax_v116*8]");
				bool flag4 = 0 == (nint)typeof(VolumetricLightBeamHD);
				obj19 = (UnityEngine.Object)1;
				if (flag4)
				{
					goto IL_1af0;
				}
			}
			obj19 = null;
			goto IL_1af0;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v475 @ rdi_v7 (UnityEngine.Object)+128]");
		UnityEngine.Object obj20 = (UnityEngine.Object)0;
		goto IL_14bb;
		IL_065b:
		UnityEngine.Object obj21;
		VolumetricLightBeamAbstractBase master4;
		UnityEngine.Object obj24;
		if (!obj9)
		{
			if ((object)m_Master == null)
			{
				obj21 = null;
				goto IL_0750;
			}
			nint num11 = (nint)typeof(VolumetricLightBeamHD);
			nint num12 = (nint)master4;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1641 @ rdx_v100 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
			object obj22 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1642 @ r8_v78 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
			nint num13 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1641 @ rdx_v100 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
			if (num13 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1642 @ r8_v78 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
				object obj23 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1696 @ rax_v196+FFFFFFF8+v1643 @ rax_v192*8]");
				bool flag5 = 0 == (nint)typeof(VolumetricLightBeamHD);
				obj24 = (UnityEngine.Object)1;
				if (flag5)
				{
					goto IL_16d8;
				}
			}
			obj24 = null;
			goto IL_16d8;
		}
		Config instance = Config.Instance;
		ColorMode colorMode;
		if (instance.featureEnabledColorGradient != FeatureEnabledColorGradient.Off)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v349 @ r14_v9 (UnityEngine.Object)+3C]");
			colorMode = ColorMode.Flat;
		}
		else
		{
			colorMode = ColorMode.Flat;
		}
		goto IL_16fa;
		IL_1553:
		bool flag6 = (object)obj6 == null;
		obj3 = null;
		if (!flag6)
		{
			obj3 = m_Master;
		}
		goto IL_0145;
		IL_173e:
		UnityEngine.Object obj25;
		bool flag7 = (object)obj25 == null;
		UnityEngine.Object obj26 = null;
		if (!flag7)
		{
			obj26 = m_Master;
		}
		goto IL_08ac;
		IL_1ac1:
		UnityEngine.Object obj27;
		bool flag8 = (object)obj27 == null;
		nint num14;
		num9 = num14;
		obj15 = null;
		if (!flag8)
		{
			num9 = num14;
			obj15 = m_Master;
		}
		goto IL_1367;
		IL_0145:
		UnityEngine.Object obj28;
		UnityEngine.Object obj31;
		if (!obj3)
		{
			if ((object)m_Master == null)
			{
				obj28 = null;
				goto IL_023a;
			}
			nint num15 = (nint)typeof(VolumetricLightBeamHD);
			nint num16 = (nint)master;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1069 @ rdx_v108 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
			object obj29 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1070 @ r8_v86 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
			nint num17 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1069 @ rdx_v108 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
			if (num17 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1070 @ r8_v86 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
				object obj30 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1129 @ rax_v239+FFFFFFF8+v1071 @ rax_v235*8]");
				bool flag9 = 0 == (nint)typeof(VolumetricLightBeamHD);
				obj31 = (UnityEngine.Object)1;
				if (flag9)
				{
					goto IL_157a;
				}
			}
			obj31 = null;
			goto IL_157a;
		}
		Quaternion beamInternalLocalRotation = ((VolumetricLightBeamSD)obj3).beamInternalLocalRotation;
		goto IL_027e;
		IL_17f6:
		bool flag10 = (object)obj14 == null;
		obj11 = null;
		if (!flag10)
		{
			obj11 = m_Master;
		}
		goto IL_0c3e;
		IL_1765:
		UnityEngine.Object obj32;
		bool flag11 = (object)obj32 == null;
		obj26 = null;
		if (!flag11)
		{
			obj26 = m_Master;
		}
		goto IL_09a1;
		IL_09a1:
		bool flag12 = obj26;
		goto IL_09b3;
		IL_16d8:
		bool flag13 = (object)obj24 == null;
		obj21 = null;
		if (!flag13)
		{
			obj21 = m_Master;
		}
		goto IL_0750;
		IL_1818:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18058A9A0");
		Vector3 vector2 = default(Vector3);
		Vector3 vector = vector2;
		goto IL_0c5e;
		IL_157a:
		bool flag14 = (object)obj31 == null;
		obj28 = null;
		if (!flag14)
		{
			obj28 = m_Master;
		}
		goto IL_023a;
		IL_08ac:
		if ((bool)obj26)
		{
			goto IL_09b3;
		}
		if ((object)m_Master == null)
		{
			obj26 = null;
			goto IL_09a1;
		}
		nint num18 = (nint)typeof(VolumetricLightBeamHD);
		UnityEngine.Object master5;
		nint num19 = (nint)master5;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2275 @ rdx_v43 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
		object obj33 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2276 @ r8_v33 (Il2CppClass<UnityEngine.Object>)+130]");
		nint num20 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2275 @ rdx_v43 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
		if (num20 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2276 @ r8_v33 (Il2CppClass<UnityEngine.Object>)+C8]");
			object obj34 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2376 @ rax_v59+FFFFFFF8+v2277 @ rax_v55*8]");
			bool flag15 = 0 == (nint)typeof(VolumetricLightBeamHD);
			obj32 = (UnityEngine.Object)1;
			if (flag15)
			{
				goto IL_1765;
			}
		}
		obj32 = null;
		goto IL_1765;
		IL_0c3e:
		if ((bool)obj11)
		{
		}
		goto IL_1818;
		IL_023a:
		if ((bool)obj28)
		{
			beamInternalLocalRotation = ((VolumetricLightBeamHD)obj28).beamInternalLocalRotation;
			goto IL_027e;
		}
		float num21 = (float)Quaternion.identityQuaternion;
		goto IL_0290;
		IL_0750:
		if (!obj21)
		{
			goto IL_0a78;
		}
		colorMode = ((VolumetricLightBeamHD)obj21).colorMode;
		goto IL_16fa;
		IL_027e:
		num21 = beamInternalLocalRotation.x;
		goto IL_0290;
		IL_14bb:
		m_Renderer.sortingOrder = (int)obj20;
		return;
		IL_0290:
		float num22 = default(float);
		transform.localRotation = (Quaternion)(&num22);
		Transform transform2 = m_Particles.transform;
		if (m_Master.IsScalable())
		{
			num22 = num21;
		}
		else
		{
			nint num23 = (nint)typeof(Vector3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v421 @ rax_v209 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num24 = 0;
			Vector3 lossyScale = m_Master.GetLossyScale();
			_ = lossyScale.x;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+30]");
			object obj35 = vector2 * 0;
			float num25 = (float)obj35 * lossyScale.z;
			float num26 = 0f - num25;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj36 = num25 & 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			object obj37 = num26 & 0;
			if ((nint)obj36 <= 0)
			{
				obj36 = 0;
			}
			float num27 = (float)obj36 * 1E-06f;
			float num28 = Mathf.Epsilon * 8f;
			if (!(num27 > num28))
			{
				num27 = num28;
			}
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num27) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj37))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v448 @ rcx_v168 (Il2CppStaticFields<UnityEngine.Vector3>)+14]");
				float num29 = 0f / lossyScale.z;
				num22 = num21;
			}
			else
			{
				nint num30 = (nint)typeof(Vector3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1438 @ rax_v217 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num31 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1439 @ rcx_v174 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
				float num29 = 0f;
				num22 = (float)Vector3.zeroVector;
			}
		}
		Vector3 vector3 = default(Vector3);
		transform2.localScale = (Vector3)(&vector3);
		float fallOffEnd = UtilsBeamProps.GetFallOffEnd(m_Master);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.VolumetricDustParticles)+44]");
		object obj38 = 0 - spawnDistanceRange;
		float num32 = (float)obj38 * fallOffEnd;
		float num33 = num32 * density;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvttss2si r12d,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18049AB00");
		ParticleSystem.MainModule mainModule = (ParticleSystem.MainModule)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 240));
		ParticleSystem.MinMaxCurve startLifetime = ((ParticleSystem.MainModule*)mainModule)->startLifetime;
		object obj39 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 80));
		_ = startLifetime.m_Mode;
		_ = startLifetime.m_CurveMax;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1805E11B0");
		object obj40 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 80));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18191C020");
		object obj41 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 80));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18191C030");
		ParticleSystem.MainModule mainModule2 = (ParticleSystem.MainModule)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 240));
		((ParticleSystem.MainModule*)mainModule2)->startLifetime = (ParticleSystem.MinMaxCurve)(&num22);
		ParticleSystem.MainModule mainModule3 = (ParticleSystem.MainModule)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 240));
		ParticleSystem.MinMaxCurve startSize = ((ParticleSystem.MainModule*)mainModule3)->startSize;
		object obj42 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 48));
		_ = startSize.m_Mode;
		_ = startSize.m_CurveMax;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1805E11B0");
		object obj43 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 48));
		float num34 = size * 0.9f;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18191C020");
		object obj44 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 48));
		float num35 = size * 1.1f;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18191C030");
		ParticleSystem.MainModule mainModule4 = (ParticleSystem.MainModule)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 240));
		((ParticleSystem.MainModule*)mainModule4)->startSize = (ParticleSystem.MinMaxCurve)(&num22);
		ParticleSystem.MainModule mainModule5 = (ParticleSystem.MainModule)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 240));
		ParticleSystem.MinMaxGradient startColor = ((ParticleSystem.MainModule*)mainModule5)->startColor;
		master4 = m_Master;
		_ = startColor.m_GradientMax;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1492 @ rax_v35 (UnityEngine.ParticleSystem+MinMaxGradient)+20]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1492 @ rax_v35 (UnityEngine.ParticleSystem+MinMaxGradient)+30]");
		_ = 0;
		if ((object)m_Master == null)
		{
			obj9 = null;
			goto IL_065b;
		}
		nint num36 = (nint)typeof(VolumetricLightBeamSD);
		nint num37 = (nint)master4;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1526 @ rdx_v101 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		object obj45 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1527 @ r8_v79 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
		nint num38 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1526 @ rdx_v101 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		if (num38 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1527 @ r8_v79 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
			object obj46 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1582 @ rax_v203+FFFFFFF8+v1528 @ rax_v199*8]");
			bool flag16 = 0 == (nint)typeof(VolumetricLightBeamSD);
			obj8 = (UnityEngine.Object)1;
			if (flag16)
			{
				goto IL_16b1;
			}
		}
		obj8 = null;
		goto IL_16b1;
		IL_0a78:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1805E11B0");
		master2 = m_Master;
		if ((object)m_Master == null)
		{
			obj10 = null;
			goto IL_0b49;
		}
		nint num39 = (nint)typeof(VolumetricLightBeamSD);
		nint num40 = (nint)master2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1849 @ rdx_v96 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		object obj47 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1850 @ r8_v76 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
		nint num41 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1849 @ rdx_v96 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		UnityEngine.Object obj49;
		if (num41 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1850 @ r8_v76 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
			object obj48 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1925 @ rax_v179+FFFFFFF8+v1851 @ rax_v175*8]");
			bool flag17 = 0 == (nint)typeof(VolumetricLightBeamSD);
			obj49 = (UnityEngine.Object)1;
			if (flag17)
			{
				goto IL_17cf;
			}
		}
		obj49 = null;
		goto IL_17cf;
		IL_1a78:
		UnityEngine.Object obj50;
		bool flag18 = (object)obj50 == null;
		obj7 = null;
		if (!flag18)
		{
			obj7 = m_Master;
		}
		goto IL_122f;
		IL_145c:
		bool flag19 = obj16;
		bool flag20 = !flag19;
		obj20 = null;
		if (!flag20)
		{
			nint num42 = (nint)obj16;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v3247 @ rdx_v76 (Il2CppClass<UnityEngine.Object>)+208] (should have been resolved before IL gen)");
			UnityEngine.Object obj51 = default(UnityEngine.Object);
			obj20 = obj51;
		}
		goto IL_14bb;
		IL_17cf:
		bool flag21 = (object)obj49 == null;
		obj10 = null;
		if (!flag21)
		{
			obj10 = m_Master;
		}
		goto IL_0b49;
		IL_1284:
		m_Renderer.sortingLayerID = sortingLayerID;
		master3 = m_Master;
		if ((object)m_Master == null)
		{
			num9 = unchecked((nint)null);
			obj15 = null;
			goto IL_1367;
		}
		nint num43 = (nint)typeof(VolumetricLightBeamSD);
		num14 = (nint)master3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3015 @ rdx_v79 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		object obj52 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3016 @ r8_v64 (Il2CppClass<UnityEngine.Object>)+130]");
		nint num44 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3015 @ rdx_v79 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		if (num44 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3016 @ r8_v64 (Il2CppClass<UnityEngine.Object>)+C8]");
			object obj53 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3071 @ rax_v125+FFFFFFF8+v3017 @ rax_v121*8]");
			bool flag22 = 0 == (nint)typeof(VolumetricLightBeamSD);
			obj27 = (UnityEngine.Object)1;
			if (flag22)
			{
				goto IL_1ac1;
			}
		}
		obj27 = null;
		goto IL_1ac1;
		IL_0c5e:
		ParticleSystem.MainModule mainModule6 = (ParticleSystem.MainModule)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 240));
		((ParticleSystem.MainModule*)mainModule6)->startColor = (ParticleSystem.MinMaxGradient)(&vector);
		ParticleSystem.MainModule mainModule7 = (ParticleSystem.MainModule)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 240));
		ParticleSystem.MinMaxCurve startSpeed = ((ParticleSystem.MainModule*)mainModule7)->startSpeed;
		_ = startSpeed.m_Mode;
		obj = startSpeed.m_CurveMax;
		if (direction == ParticlesDirection.Random)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.VolumetricDustParticles)+34]");
			nint num45 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
			float num46 = num45 & 0;
		}
		else
		{
			float num46 = 0f;
		}
		object obj54 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 16));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18191C030");
		ParticleSystem.MainModule mainModule8 = (ParticleSystem.MainModule)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 240));
		((ParticleSystem.MainModule*)mainModule8)->startSpeed = (ParticleSystem.MinMaxCurve)(&num22);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18049AB00");
		bool flag23 = direction < ParticlesDirection.Random;
		bool flag24 = direction == ParticlesDirection.Random;
		ParticleSystem.VelocityOverLifetimeModule velocityOverLifetimeModule = (ParticleSystem.VelocityOverLifetimeModule)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 264));
		bool flag25 = !flag23;
		bool flag26 = !flag24;
		bool flag27 = flag26 & flag25;
		((ParticleSystem.VelocityOverLifetimeModule*)velocityOverLifetimeModule)->enabled = flag27;
		object obj55 = direction - 1;
		bool flag28 = obj55 == null;
		ParticleSystem.VelocityOverLifetimeModule velocityOverLifetimeModule2 = (ParticleSystem.VelocityOverLifetimeModule)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 264));
		bool space = !flag28;
		((ParticleSystem.VelocityOverLifetimeModule*)velocityOverLifetimeModule2)->space = (space ? ParticleSystemSimulationSpace.World : ParticleSystemSimulationSpace.Local);
		ParticleSystem.VelocityOverLifetimeModule velocityOverLifetimeModule3 = (ParticleSystem.VelocityOverLifetimeModule)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 264));
		((ParticleSystem.VelocityOverLifetimeModule*)velocityOverLifetimeModule3)->xMultiplier = (float)velocity;
		ParticleSystem.VelocityOverLifetimeModule velocityOverLifetimeModule4 = (ParticleSystem.VelocityOverLifetimeModule)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 264));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.VolumetricDustParticles)+30]");
		((ParticleSystem.VelocityOverLifetimeModule*)velocityOverLifetimeModule4)->yMultiplier = 0f;
		ParticleSystem.VelocityOverLifetimeModule velocityOverLifetimeModule5 = (ParticleSystem.VelocityOverLifetimeModule)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 264));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.VolumetricDustParticles)+34]");
		((ParticleSystem.VelocityOverLifetimeModule*)velocityOverLifetimeModule5)->zMultiplier = 0f;
		ParticleSystem.MainModule mainModule9 = (ParticleSystem.MainModule)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 240));
		((ParticleSystem.MainModule*)mainModule9)->maxParticles = (int)transform;
		float thickness = UtilsBeamProps.GetThickness(m_Master);
		float fallOffEnd2 = UtilsBeamProps.GetFallOffEnd(m_Master);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18049AB00");
		ParticleSystem.ShapeModule shapeModule = (ParticleSystem.ShapeModule)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 256));
		((ParticleSystem.ShapeModule*)shapeModule)->shapeType = ParticleSystemShapeType.ConeVolume;
		float coneAngle = UtilsBeamProps.GetConeAngle(m_Master);
		float num47 = ((0f > thickness) ? 0f : ((thickness > 1f) ? 1f : thickness));
		float num48 = num47 * 0.3f;
		ParticleSystem.ShapeModule shapeModule2 = (ParticleSystem.ShapeModule)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 256));
		float num49 = num48 + 0.7f;
		float num50 = num49 * coneAngle;
		float angle = num50 * 0.5f;
		((ParticleSystem.ShapeModule*)shapeModule2)->angle = angle;
		float coneRadiusStart = UtilsBeamProps.GetConeRadiusStart(m_Master);
		float num51;
		if (!(0f > thickness))
		{
			bool flag29 = !(thickness > 1f);
			num51 = thickness;
			if (!flag29)
			{
				num51 = 1f;
			}
		}
		else
		{
			num51 = 0f;
		}
		float num52 = num51 * 0.7f;
		float num53 = num50 * ((float)Math.PI / 180f);
		float num54 = num52 + 0.3f;
		float num55 = num53 * 0.5f;
		float num56 = num54 * coneRadiusStart;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033EBD0");
		float num57 = (float)spawnDistanceRange;
		float num58 = num55 * fallOffEnd2;
		if (0 <= (nint)spawnDistanceRange)
		{
			if (num57 > 1f)
			{
				num57 = 1f;
			}
		}
		else
		{
			num57 = 0f;
		}
		float num59 = num58 - num56;
		ParticleSystem.ShapeModule shapeModule3 = (ParticleSystem.ShapeModule)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 256));
		float num60 = num59 * num57;
		float radius = num60 + num56;
		((ParticleSystem.ShapeModule*)shapeModule3)->radius = radius;
		ParticleSystem.ShapeModule shapeModule4 = (ParticleSystem.ShapeModule)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 256));
		((ParticleSystem.ShapeModule*)shapeModule4)->length = num32;
		ParticleSystem.ShapeModule shapeModule5 = (ParticleSystem.ShapeModule)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 256));
		((ParticleSystem.ShapeModule*)shapeModule5)->position = (Vector3)(&vector3);
		ParticleSystem.ShapeModule shapeModule6 = (ParticleSystem.ShapeModule)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 256));
		((ParticleSystem.ShapeModule*)shapeModule6)->arc = 360f;
		bool flag30 = direction == ParticlesDirection.Random;
		float randomDirectionAmount = 1f;
		if (!flag30)
		{
			randomDirectionAmount = 0f;
		}
		ParticleSystem.ShapeModule shapeModule7 = (ParticleSystem.ShapeModule)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 256));
		((ParticleSystem.ShapeModule*)shapeModule7)->randomDirectionAmount = randomDirectionAmount;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18049AB00");
		ParticleSystem.EmissionModule emissionModule = default(ParticleSystem.EmissionModule);
		ParticleSystem.MinMaxCurve rateOverTime = emissionModule.rateOverTime;
		object obj56 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 16));
		_ = rateOverTime.m_Mode;
		_ = rateOverTime.m_CurveMax;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18191C030");
		emissionModule.rateOverTime = (ParticleSystem.MinMaxCurve)(&num22);
		if (!m_Renderer)
		{
			return;
		}
		UnityEngine.Object master6 = m_Master;
		UnityEngine.Object obj57;
		nint num61;
		if ((object)m_Master == null)
		{
			num61 = unchecked((nint)null);
			obj57 = null;
			goto IL_113a;
		}
		nint num62 = (nint)typeof(VolumetricLightBeamSD);
		nint num63 = (nint)master6;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2745 @ rdx_v85 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		object obj58 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2746 @ r8_v67 (Il2CppClass<UnityEngine.Object>)+130]");
		nint num64 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2745 @ rdx_v85 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		UnityEngine.Object obj60;
		if (num64 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2746 @ r8_v67 (Il2CppClass<UnityEngine.Object>)+C8]");
			object obj59 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2801 @ rax_v149+FFFFFFF8+v2747 @ rax_v145*8]");
			bool flag31 = 0 == (nint)typeof(VolumetricLightBeamSD);
			obj60 = (UnityEngine.Object)1;
			if (flag31)
			{
				goto IL_1a49;
			}
		}
		obj60 = null;
		goto IL_1a49;
		IL_1a49:
		bool flag32 = (object)obj60 == null;
		num61 = num63;
		obj57 = null;
		if (!flag32)
		{
			num61 = num63;
			obj57 = m_Master;
		}
		goto IL_113a;
		IL_16fa:
		if (colorMode == ColorMode.Flat)
		{
			goto IL_0a78;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1805E11B0");
		master5 = m_Master;
		if ((object)m_Master == null)
		{
			obj26 = null;
			goto IL_08ac;
		}
		nint num65 = (nint)typeof(VolumetricLightBeamSD);
		nint num66 = (nint)master5;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1953 @ rdx_v44 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		object obj61 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1954 @ r8_v34 (Il2CppClass<UnityEngine.Object>)+130]");
		nint num67 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1953 @ rdx_v44 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		if (num67 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1954 @ r8_v34 (Il2CppClass<UnityEngine.Object>)+C8]");
			object obj62 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2026 @ rax_v66+FFFFFFF8+v1955 @ rax_v62*8]");
			bool flag33 = 0 == (nint)typeof(VolumetricLightBeamSD);
			obj25 = (UnityEngine.Object)1;
			if (flag33)
			{
				goto IL_173e;
			}
		}
		obj25 = null;
		goto IL_173e;
		IL_09b3:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v351 @ r14_v12 (UnityEngine.Object)+50]");
		GradientColorKey[] colorKeys = ((Gradient)0).colorKeys;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v351 @ r14_v12 (UnityEngine.Object)+50]");
		GradientAlphaKey[] alphaKeys = ((Gradient)0).alphaKeys;
		object obj63 = alphaKeys + 32;
		UnityEngine.Object obj64 = null;
		UnityEngine.Object obj65 = null;
		while ((nint)obj64 < alphaKeys.Length)
		{
			obj65 = (UnityEngine.Object)(obj65 + 1);
			float num68 = alpha * (float)obj63;
			obj63 = num68;
			obj63 += 8;
			obj64 = obj65;
		}
		m_GradientCached.SetKeys(colorKeys, alphaKeys);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803DFA40");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-30]");
		vector = (Vector3)0;
		goto IL_0c5e;
		IL_113a:
		if (!obj57)
		{
			if ((object)m_Master == null)
			{
				obj7 = null;
				goto IL_122f;
			}
			nint num69 = (nint)typeof(VolumetricLightBeamHD);
			num61 = (nint)master6;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2860 @ rdx_v84 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
			object obj66 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v335 @ r8_v65 (Il2CppClass<UnityEngine.Object>)+130]");
			nint num70 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2860 @ rdx_v84 (Il2CppClass<VLB.VolumetricLightBeamHD>)+130]");
			if (num70 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v335 @ r8_v65 (Il2CppClass<UnityEngine.Object>)+C8]");
				object obj67 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2917 @ rax_v142+FFFFFFF8+v2862 @ rax_v138*8]");
				bool flag34 = 0 == (nint)typeof(VolumetricLightBeamHD);
				obj50 = (UnityEngine.Object)1;
				if (flag34)
				{
					goto IL_1a78;
				}
			}
			obj50 = null;
			goto IL_1a78;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v356 @ r14_v23 (UnityEngine.Object)+124]");
		sortingLayerID = 0;
		goto IL_1284;
		IL_1af0:
		bool flag35 = (object)obj19 == null;
		obj16 = null;
		if (!flag35)
		{
			obj16 = m_Master;
		}
		goto IL_145c;
	}

	private void HandleBackwardCompatibility(int serializedVersion, int newVersion)
	{
		//IL_00bf: Expected O, but got I4
		if (serializedVersion != -1 && serializedVersion != newVersion)
		{
			if (serializedVersion < 1880)
			{
				bool flag = direction == ParticlesDirection.Random;
				Vector3 vector = default(Vector3);
				velocity = vector;
				direction = (flag ? ParticlesDirection.LocalSpace : ParticlesDirection.Random);
				_ = speed;
			}
			else if (serializedVersion >= 1940)
			{
				return;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18037FDF0");
			spawnDistanceRange = (MinMaxRangeFloat)0;
		}
	}

	private unsafe void UpdateCulling()
	{
		//IL_0054: Expected I, but got O
		//IL_005c: Expected I, but got O
		//IL_006c: Expected O, but got I
		//IL_0904: Expected O, but got I
		//IL_00a8: Expected O, but got I
		//IL_00cd: Expected O, but got I4
		//IL_00ed: Expected O, but got I
		//IL_0155: Expected F4, but got I4
		//IL_012f: Expected F4, but got I4
		//IL_0180: Expected F4, but got I
		//IL_01dd: Expected F4, but got I
		//IL_082b: Expected O, but got I4
		//IL_031a: Expected O, but got I4
		//IL_0a50: Expected I, but got O
		//IL_0345: Expected O, but got I4
		//IL_05b4: Expected F4, but got I4
		//IL_04ef: Expected I, but got O
		//IL_0507: Expected O, but got I
		//IL_0a98: Expected I, but got O
		//IL_064f: Expected I, but got O
		//IL_065f: Expected O, but got I
		//IL_056c: Expected O, but got I4
		//IL_0b04: Expected F4, but got O
		//IL_0b11: Expected O, but got I
		//IL_0382: Expected I, but got O
		//IL_0392: Expected O, but got I
		//IL_06a5: Expected O, but got I
		//IL_05fc: Expected F4, but got I
		//IL_040c: Expected I, but got O
		//IL_041c: Expected O, but got I
		//IL_06ea: Expected O, but got I
		//IL_06ff: Expected F4, but got I
		//IL_078e: Expected O, but got Ref
		if (!m_Particles)
		{
			return;
		}
		VolumetricLightBeamAbstractBase master = m_Master;
		UnityEngine.Object obj;
		if ((object)m_Master == null)
		{
			obj = null;
			goto IL_00fc;
		}
		nint num = (nint)typeof(VolumetricLightBeamSD);
		nint num2 = (nint)master;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v296 @ rdx_v50 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v297 @ r8_v19 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v296 @ rdx_v50 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
		UnityEngine.Object obj4;
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v297 @ r8_v19 (Il2CppClass<VLB.VolumetricLightBeamAbstractBase>)+C8]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rax_v90+FFFFFFF8+v298 @ rax_v86*8]");
			bool flag = 0 == (nint)typeof(VolumetricLightBeamSD);
			obj4 = (UnityEngine.Object)1;
			if (flag)
			{
				goto IL_08ed;
			}
		}
		obj4 = null;
		goto IL_08ed;
		IL_0933:
		bool flag2 = (object)m_Particles == null;
		UnityEngine.Object particles = m_Particles;
		bool flag5;
		object[] array;
		Renderer renderer;
		if (!flag2)
		{
			GameObject gameObject = m_Particles.gameObject;
			bool flag3 = (object)gameObject == null;
			renderer = null;
			particles = m_Particles;
			if (!flag3)
			{
				bool activeSelf = gameObject.activeSelf;
				bool flag4 = activeSelf == flag5;
				renderer = null;
				if (!flag4)
				{
					SetActive(flag5);
					bool flag6 = (byte)((flag5 ? 1u : 0u) ^ 1u) != 0;
					_003CisCulled_003Ek__BackingField = flag6;
					array = null;
					renderer = (Renderer)flag5;
				}
				if (!flag5)
				{
					return;
				}
				bool flag7 = (object)m_Particles == null;
				particles = m_Particles;
				if (!flag7)
				{
					if (m_Particles.isPlaying)
					{
						return;
					}
					bool flag8 = (object)m_Particles == null;
					renderer = null;
					particles = m_Particles;
					if (!flag8)
					{
						m_Particles.Play();
						return;
					}
				}
			}
		}
		goto IL_08b4;
		IL_099e:
		UnityEngine.Object master2 = m_Master;
		float num5;
		float num4 = num5 * num5;
		Vector3 point = default(Vector3);
		if ((object)m_Master != null)
		{
			nint num6 = (nint)master2;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1120 @ rdx_v22 (Il2CppClass<UnityEngine.Object>)+178] (should have been resolved before IL gen)");
			UnityEngine.Object obj5 = default(UnityEngine.Object);
			if (!(obj5 != null))
			{
				nint num7 = (nint)typeof(Vector3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1287 @ rdx_v30 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num8 = 0;
				Vector3 vector = default(Vector3);
				float num9 = (float)vector * 0.5f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1292 @ rax_v49 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
				float num10 = 0f * 0.5f;
				point = Vector3.zeroVector;
				float num11 = (float)vector;
				array = null;
				renderer = (Renderer)num7;
				goto IL_0a5f;
			}
			nint num12 = (nint)master2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1234 @ rdx_v32 (Il2CppClass<UnityEngine.Object>)+180]");
			renderer = (Renderer)0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1234 @ rdx_v32 (Il2CppClass<UnityEngine.Object>)+178] (should have been resolved before IL gen)");
			object obj6 = default(object);
			bool flag9 = obj6 == null;
			array = null;
			particles = m_Master;
			if (!flag9)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v539 @ rax_v53+20]");
				renderer = (Renderer)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v539 @ rax_v53+20]");
				bool flag10 = (nint)0 == 0;
				array = null;
				particles = m_Master;
				if (!flag10)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v539 @ rax_v53+20]");
					Bounds bounds = ((Renderer)0).bounds;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1284 @ rax_v54 (UnityEngine.Bounds)+10]");
					float num11 = 0f;
					array = null;
					goto IL_0a5f;
				}
			}
		}
		goto IL_08b4;
		IL_08b4:
		throw new NullReferenceException();
		IL_0593:
		UnityEngine.Object obj7;
		bool flag11 = obj7;
		bool flag12 = !flag11;
		float num13 = 0f;
		if (!flag12)
		{
			bool flag13 = (object)obj7 == null;
			renderer = null;
			particles = obj7;
			if (flag13)
			{
				goto IL_08b4;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v495 @ rdi_v13 (UnityEngine.Object)+130]");
			num13 = 0f;
		}
		bool flag14 = !(num5 > num13);
		renderer = null;
		particles = obj7;
		if (!flag14)
		{
			num5 = num13;
			renderer = null;
			particles = obj7;
		}
		goto IL_099e;
		IL_08ed:
		bool flag15 = (object)obj4 == null;
		array = (object[])num2;
		obj = null;
		if (!flag15)
		{
			array = (object[])num2;
			obj = m_Master;
		}
		goto IL_00fc;
		IL_0a5f:
		Config instance = Config.Instance;
		bool flag16 = (object)instance == null;
		particles = null;
		if (!flag16)
		{
			Transform fadeOutCameraTransform = instance.fadeOutCameraTransform;
			bool flag17 = (object)fadeOutCameraTransform == null;
			renderer = null;
			particles = instance;
			if (!flag17)
			{
				Vector3 position = fadeOutCameraTransform.position;
				Bounds bounds2 = default(Bounds);
				float num11 = bounds2.Internal_SqrDistance(ref point);
				bool flag18 = num4 < num11;
				flag5 = !flag18;
				array = null;
				renderer = (Renderer)(&point);
				goto IL_0933;
			}
		}
		goto IL_08b4;
		IL_09fa:
		UnityEngine.Object obj8;
		bool flag19 = (object)obj8 == null;
		obj7 = null;
		if (!flag19)
		{
			obj7 = m_Master;
		}
		goto IL_0593;
		IL_00fc:
		bool flag20 = obj;
		bool flag21 = !flag20;
		bool flag22;
		if (!flag20)
		{
			float num11 = 0f;
			flag22 = false;
		}
		else
		{
			bool flag23 = (object)obj == null;
			float num11 = 0f;
			renderer = null;
			particles = obj;
			if (flag23)
			{
				goto IL_08b4;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v312 @ rsi_v2 (UnityEngine.Object)+12C]");
			num11 = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v312 @ rsi_v2 (UnityEngine.Object)+12C]");
			flag21 = (nint)0 == 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v312 @ rsi_v2 (UnityEngine.Object)+12C]");
			if ((nint)0 < (nint)0)
			{
				flag22 = false;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v312 @ rsi_v2 (UnityEngine.Object)+130]");
				num11 = 0f;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v312 @ rsi_v2 (UnityEngine.Object)+130]");
				bool flag24 = (nint)0 < (nint)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v312 @ rsi_v2 (UnityEngine.Object)+130]");
				flag21 = (nint)0 == 0;
				flag22 = !flag24;
			}
		}
		flag5 = true;
		renderer = null;
		if (!flag21)
		{
			bool flag25 = (object)m_Master == null;
			renderer = null;
			particles = m_Master;
			if (!flag25)
			{
				bool hasGeometry = m_Master.hasGeometry;
				bool flag26 = !hasGeometry;
				flag5 = true;
				renderer = null;
				if (flag26)
				{
					goto IL_0933;
				}
				Config instance2 = Config.Instance;
				bool flag27 = (object)instance2 == null;
				renderer = null;
				particles = null;
				if (!flag27)
				{
					Transform fadeOutCameraTransform2 = instance2.fadeOutCameraTransform;
					if ((bool)fadeOutCameraTransform2)
					{
						num5 = cullingMaxDistance;
						bool flag28 = !flag22;
						renderer = null;
						particles = fadeOutCameraTransform2;
						if (flag28)
						{
							goto IL_099e;
						}
						VolumetricLightBeamAbstractBase master3 = m_Master;
						bool flag29 = (object)m_Master == null;
						obj7 = null;
						if (!flag29)
						{
							nint num14 = (nint)typeof(VolumetricLightBeamSD);
							array = (object[])(object)master3;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1062 @ rdx_v37 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
							object obj9 = 0;
							object obj10 = array[34];
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1062 @ rdx_v37 (Il2CppClass<VLB.VolumetricLightBeamSD>)+130]");
							if ((nint)obj10 >= 0)
							{
								object obj11 = array[21];
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1137 @ rax_v65 (System.Object)+FFFFFFF8+v1064 @ rax_v62*8]");
								bool flag30 = 0 == (nint)typeof(VolumetricLightBeamSD);
								obj8 = (UnityEngine.Object)1;
								if (flag30)
								{
									goto IL_09fa;
								}
							}
							obj8 = null;
							goto IL_09fa;
						}
						goto IL_0593;
					}
					GameObject context = base.gameObject;
					object[] array2 = new object[2];
					Config instance3 = Config.Instance;
					bool flag31 = (object)instance3 == null;
					renderer = (Renderer)2;
					particles = null;
					if (!flag31)
					{
						bool flag32 = array2 == null;
						renderer = (Renderer)2;
						particles = null;
						if (!flag32)
						{
							if (instance3.fadeOutCameraTag != null)
							{
								nint num15 = (nint)array2;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1208 @ rdx_v47 (Il2CppClass<System.Object[]>)+40]");
								renderer = (Renderer)0;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
								object obj12 = default(object);
								bool flag33 = obj12 == null;
								particles = (UnityEngine.Object)(object)instance3.fadeOutCameraTag;
								if (flag33)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
									object obj13 = default(object);
									throw obj13;
								}
							}
							array2[0] = instance3.fadeOutCameraTag;
							bool flag34 = "VolumetricDustParticles" == null;
							object obj14 = "VolumetricDustParticles";
							if (!flag34)
							{
								nint num16 = (nint)array2;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1350 @ rdx_v45 (Il2CppClass<System.Object[]>)+40]");
								object obj15 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
								object obj16 = default(object);
								bool flag35 = obj16 == null;
								object obj17 = "VolumetricDustParticles";
								if (flag35)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
									object obj18 = default(object);
									throw obj18;
								}
								obj14 = "VolumetricDustParticles";
							}
							array2[1] = obj14;
							Debug.LogErrorFormat(context, "Fail to retrieve the camera with tag '{0}' (specified in VLB Config's 'fadeOutCameraTag') for the {1} Culling feature.", array2);
							array = array2;
							flag5 = true;
							renderer = (Renderer)(object)"Fail to retrieve the camera with tag '{0}' (specified in VLB Config's 'fadeOutCameraTag') for the {1} Culling feature.";
							goto IL_0933;
						}
					}
				}
			}
			goto IL_08b4;
		}
		goto IL_0933;
	}

	public VolumetricDustParticles()
	{
		//IL_003b: Expected I, but got O
		//IL_008c: Expected I, but got O
		alpha = 0.5f;
		size = 0.01f;
		nint num = (nint)typeof(Consts.DustParticles);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ rax_v3 (Il2CppClass<VLB.Consts+DustParticles>)+B8]");
		nint num2 = 0;
		velocity = Consts.DustParticles.VelocityDefault;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rax_v4 (Il2CppStaticFields<VLB.Consts+DustParticles>)+8]");
		_ = 0;
		speed = 0.03f;
		density = 5f;
		nint num3 = (nint)typeof(Consts.DustParticles);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rax_v5 (Il2CppClass<VLB.Consts+DustParticles>)+B8]");
		nint num4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rax_v6 (Il2CppStaticFields<VLB.Consts+DustParticles>)+10]");
		_ = 0;
		spawnDistanceRange = Consts.DustParticles.SpawnDistanceRangeDefault;
		spawnMaxDistance = 0.7f;
		cullingMaxDistance = 10f;
		m_AlphaAdditionalRuntime = 1f;
		Gradient gradientCached = new Gradient();
		m_GradientCached = gradientCached;
		m_RuntimePropertiesDirty = true;
		base._002Ector();
	}
}
