using Cpp2ILInjected;
using UnityEngine;

namespace VLB;

public abstract class BeamGeometryAbstractBase : MonoBehaviour
{
	private MeshRenderer _003CmeshRenderer_003Ek__BackingField;

	private MeshFilter _003CmeshFilter_003Ek__BackingField;

	private Mesh _003CconeMesh_003Ek__BackingField;

	protected Matrix4x4 m_ColorGradientMatrix;

	protected Material m_CustomMaterial;

	public MeshRenderer meshRenderer
	{
		get
		{
			return _003CmeshRenderer_003Ek__BackingField;
		}
		protected set
		{
			_003CmeshRenderer_003Ek__BackingField = value;
		}
	}

	public MeshFilter meshFilter
	{
		get
		{
			return _003CmeshFilter_003Ek__BackingField;
		}
		protected set
		{
			_003CmeshFilter_003Ek__BackingField = value;
		}
	}

	public Mesh coneMesh
	{
		get
		{
			return _003CconeMesh_003Ek__BackingField;
		}
		protected set
		{
			_003CconeMesh_003Ek__BackingField = value;
		}
	}

	protected abstract VolumetricLightBeamAbstractBase GetMaster();

	private void Start()
	{
		//IL_0033: Expected I, but got O
		VolumetricLightBeamAbstractBase master = GetMaster();
		if ((bool)master)
		{
			nint num = (nint)master;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v115 @ rdx_v9 (Il2CppClass<UnityEngine.Object>)+178] (should have been resolved before IL gen)");
			Object obj = default(Object);
			if (!(obj != this))
			{
				return;
			}
		}
		if ((bool)this)
		{
			GameObject obj2 = base.gameObject;
			Object.DestroyImmediate(obj2);
		}
	}

	private void OnDestroy()
	{
		if ((bool)m_CustomMaterial)
		{
			Object.DestroyImmediate(m_CustomMaterial);
			m_CustomMaterial = null;
		}
	}

	private void DestroyOrphanBeamGeom()
	{
		//IL_0033: Expected I, but got O
		VolumetricLightBeamAbstractBase master = GetMaster();
		if ((bool)master)
		{
			nint num = (nint)master;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v115 @ rdx_v9 (Il2CppClass<UnityEngine.Object>)+178] (should have been resolved before IL gen)");
			Object obj = default(Object);
			if (!(obj != this))
			{
				return;
			}
		}
		if ((bool)this)
		{
			GameObject obj2 = base.gameObject;
			Object.DestroyImmediate(obj2);
		}
	}

	public static void DestroyBeamGeometryGameObject(BeamGeometryAbstractBase beamGeom)
	{
		if ((bool)beamGeom)
		{
			GameObject obj = beamGeom.gameObject;
			Object.DestroyImmediate(obj);
		}
	}
}
