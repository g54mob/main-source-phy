using System;
using UnityEngine;

[AddComponentMenu("Water/Controllers/Water Vis Controller")]
public class WaterLod : MonoBehaviour
{
	[Serializable]
	public class WaterLOD
	{
		public MeshRenderer[] LODMeshrenderer;
	}

	public WaterLOD[] waterLODs;

	public MeshRenderer dofPlane;

	public Collider collisionPlane;

	public MeshRenderer[] fogPlanes;

	private Material currentMat;

	private Material currentMat2;

	private MaterialPropertyBlock prop;

	private MaterialPropertyBlock prop2;

	protected void Awake()
	{
		prop = new MaterialPropertyBlock();
		prop2 = new MaterialPropertyBlock();
		ReferenceMaster.onDOFChanged = (Action)Delegate.Combine(ReferenceMaster.onDOFChanged, new Action(DOF));
		DOF();
		SetRenderBounds();
		currentMat = waterLODs[0].LODMeshrenderer[0].sharedMaterial;
	}

	private void SetRenderBounds()
	{
		Mesh mesh = waterLODs[0].LODMeshrenderer[0].GetComponent<MeshFilter>().mesh;
		mesh.bounds = new Bounds(Vector3.zero, new Vector3(9f, 2f, 9f));
	}

	public void DOF()
	{
		if (dofPlane != null)
		{
			dofPlane.enabled = OptionsMaster.BesiegeConfig.DepthOfField;
		}
	}

	protected void OnDestroy()
	{
		ReferenceMaster.onDOFChanged = (Action)Delegate.Remove(ReferenceMaster.onDOFChanged, new Action(DOF));
	}

	public void SetMaterial(Material m, Material f)
	{
		currentMat = m;
		currentMat2 = f;
		for (int i = 0; i < waterLODs.Length; i++)
		{
			for (int j = 0; j < waterLODs[i].LODMeshrenderer.Length; j++)
			{
				waterLODs[i].LODMeshrenderer[j].sharedMaterial = m;
			}
		}
		for (int k = 0; k < fogPlanes.Length; k++)
		{
			fogPlanes[k].sharedMaterial = f;
		}
	}

	public void SetFogMap(Cubemap map, Color fogColor, bool setProperties = true)
	{
		SetTexture("_FogCube", map);
		SetColor("_DistanceFog", fogColor);
		if (setProperties)
		{
			SetPropertyBlock();
		}
	}

	public void SetReflectionMap(Cubemap map, float intensity, float falloff, bool setProperties = true)
	{
		SetTexture("_Cube", map);
		SetFloat("_ReflectionPower", intensity);
		SetFloat("_ReflectionFalloff", falloff);
		if (setProperties)
		{
			SetPropertyBlock();
		}
	}

	public void SetColor(Color color, Color scatter, Color foam, bool setProperties = true)
	{
		SetColor("_BaseColor", color);
		SetColor("_SSS", scatter);
		SetColor("_FoamAmbient", foam);
		if (setProperties)
		{
			SetPropertyBlock();
		}
	}

	public void SetRenderDistances(float distance, bool setProperties = true)
	{
		SetFloat("_DistanceFogRange", distance);
		SetFloat("_DistanceFadeEnd", distance * 2f);
		if (setProperties)
		{
			SetPropertyBlock();
		}
	}

	public void ClearCustom()
	{
		prop.Clear();
		prop2.Clear();
		SetPropertyBlock();
	}

	private void SetTexture(string key, Texture t)
	{
		prop.SetTexture(key, t);
		prop2.SetTexture(key, t);
	}

	private void SetColor(string key, Color c)
	{
		prop.SetColor(key, c);
		prop2.SetColor(key, c);
	}

	private void SetFloat(string key, float f)
	{
		prop.SetFloat(key, f);
		prop2.SetFloat(key, f);
	}

	private void SetPropertyBlock()
	{
		for (int i = 0; i < waterLODs.Length; i++)
		{
			for (int j = 0; j < waterLODs[i].LODMeshrenderer.Length; j++)
			{
				waterLODs[i].LODMeshrenderer[j].SetPropertyBlock(prop);
			}
		}
		if (!prop2.isEmpty)
		{
			prop2.SetFloat("_SkipDisplacement", 1f);
		}
		for (int k = 0; k < fogPlanes.Length; k++)
		{
			if (prop2.isEmpty)
			{
				fogPlanes[k].sharedMaterial = currentMat2;
			}
			else
			{
				fogPlanes[k].sharedMaterial = currentMat;
			}
			fogPlanes[k].SetPropertyBlock(prop2);
		}
	}
}
