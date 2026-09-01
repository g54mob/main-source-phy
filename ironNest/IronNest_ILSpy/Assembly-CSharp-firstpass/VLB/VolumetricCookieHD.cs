using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace VLB;

public class VolumetricCookieHD : MonoBehaviour
{
	public const string ClassName = "VolumetricCookieHD";

	private float m_Contribution;

	private Texture m_CookieTexture;

	private CookieChannel m_Channel;

	private bool m_Negative;

	private Vector2 m_Translation;

	private float m_Rotation;

	private Vector2 m_Scale;

	private VolumetricLightBeamHD m_Master;

	public float contribution
	{
		get
		{
			return m_Contribution;
		}
		set
		{
			bool flag = m_Contribution == value;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803866BCh\"");
			if (!flag)
			{
				m_Contribution = value;
				SetDirty();
			}
		}
	}

	public Texture cookieTexture
	{
		get
		{
			return m_CookieTexture;
		}
		set
		{
			if (m_CookieTexture != value)
			{
				m_CookieTexture = value;
				SetDirty();
			}
		}
	}

	public CookieChannel channel
	{
		get
		{
			return m_Channel;
		}
		set
		{
			if (m_Channel != value)
			{
				m_Channel = value;
				SetDirty();
			}
		}
	}

	public bool negative
	{
		get
		{
			return m_Negative;
		}
		set
		{
			if (m_Negative != value)
			{
				m_Negative = value;
				SetDirty();
			}
		}
	}

	public Vector2 translation
	{
		get
		{
			Vector2 result = default(Vector2);
			return result;
		}
		set
		{
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Expected O, but got Unknown
			//IL_0054: Invalid comparison between F4 and O
			//IL_0073: Invalid comparison between F4 and I4
			//IL_009c: Expected O, but got I4
			object obj = m_Translation - value;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.VolumetricCookieHD)+3C]");
			object obj3 = default(object);
			object obj2 = 0 - obj3;
			object obj4 = obj2 * obj2;
			object obj5 = obj * obj;
			object obj6 = obj4 + obj5;
			bool flag = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)9.9999994E-11f) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6);
			float num = 9.9999994E-11f - (float)obj6;
			bool flag2 = num == 0f;
			bool flag3 = !flag;
			bool flag4 = !flag2;
			object obj7 = flag4 & flag3;
			if (obj7 == null)
			{
				m_Translation = value;
				SetDirty();
			}
		}
	}

	public float rotation
	{
		get
		{
			return m_Rotation;
		}
		set
		{
			bool flag = m_Rotation == value;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 000000018038677Ch\"");
			if (!flag)
			{
				m_Rotation = value;
				SetDirty();
			}
		}
	}

	public Vector2 scale
	{
		get
		{
			Vector2 result = default(Vector2);
			return result;
		}
		set
		{
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Expected O, but got Unknown
			//IL_0054: Invalid comparison between F4 and O
			//IL_0073: Invalid comparison between F4 and I4
			//IL_009c: Expected O, but got I4
			object obj = m_Scale - value;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (VLB.VolumetricCookieHD)+48]");
			object obj3 = default(object);
			object obj2 = 0 - obj3;
			object obj4 = obj2 * obj2;
			object obj5 = obj * obj;
			object obj6 = obj4 + obj5;
			bool flag = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)9.9999994E-11f) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6);
			float num = 9.9999994E-11f - (float)obj6;
			bool flag2 = num == 0f;
			bool flag3 = !flag;
			bool flag4 = !flag2;
			object obj7 = flag4 & flag3;
			if (obj7 == null)
			{
				m_Scale = value;
				SetDirty();
			}
		}
	}

	private void SetDirty()
	{
		if ((bool)m_Master)
		{
			m_Master.SetPropertyDirty(DirtyProps.CookieProps);
		}
	}

	public unsafe static void ApplyMaterialProperties(VolumetricCookieHD instance, BeamGeometryHD geom)
	{
		//IL_01b0: Expected O, but got Ref
		//IL_00ac: Expected I, but got O
		//IL_0186: Expected O, but got Ref
		//IL_019d: Expected O, but got Ref
		float num4 = default(float);
		int nameID;
		Vector4 value;
		if ((bool)instance && instance.enabled && instance.m_CookieTexture != null)
		{
			geom.SetMaterialProp(ShaderProperties.HD.CookieTexture, instance.m_CookieTexture);
			nint num = (nint)typeof(ShaderProperties.HD);
			if (!instance.m_Negative)
			{
			}
			float num2 = instance.m_Rotation * ((float)Math.PI / 180f);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033DE70");
			float num3 = instance.m_Rotation * ((float)Math.PI / 180f);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
			geom.SetMaterialProp(ShaderProperties.HD.CookieProperties, (Vector4)(&num4));
			nameID = ShaderProperties.HD.CookiePosAndScale;
			value = (Vector4)(&num4);
		}
		else
		{
			if ((bool)((BeamGeometryAbstractBase)geom).m_CustomMaterial)
			{
				((BeamGeometryAbstractBase)geom).m_CustomMaterial.SetTexture(ShaderProperties.HD.CookieTexture, null);
			}
			value = (Vector4)(&num4);
			nameID = ShaderProperties.HD.CookieProperties;
		}
		geom.SetMaterialProp(nameID, value);
	}

	private void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		VolumetricLightBeamHD master = default(VolumetricLightBeamHD);
		m_Master = master;
	}

	private void OnEnable()
	{
		SetDirty();
	}

	private void OnDisable()
	{
		SetDirty();
	}

	private void OnDidApplyAnimationProperties()
	{
		SetDirty();
	}

	private void Start()
	{
		if (Application.isPlaying)
		{
			SetDirty();
		}
	}

	private void OnDestroy()
	{
		if (Application.isPlaying)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 51 Invalid \"Jump target not found in method: 0x1803864B0\"");
		}
	}

	public VolumetricCookieHD()
	{
		//IL_0029: Expected I, but got O
		//IL_0064: Expected I, but got O
		m_Contribution = 1f;
		m_Channel = CookieChannel.Alpha;
		nint num = (nint)typeof(Consts.Cookie);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rax_v3 (Il2CppClass<VLB.Consts+Cookie>)+B8]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ rax_v4 (Il2CppStaticFields<VLB.Consts+Cookie>)+4]");
		_ = 0;
		m_Translation = Consts.Cookie.TranslationDefault;
		nint num3 = (nint)typeof(Consts.Cookie);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ rax_v6 (Il2CppClass<VLB.Consts+Cookie>)+B8]");
		nint num4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rax_v7 (Il2CppStaticFields<VLB.Consts+Cookie>)+C]");
		_ = 0;
		m_Scale = Consts.Cookie.ScaleDefault;
		base._002Ector();
	}
}
