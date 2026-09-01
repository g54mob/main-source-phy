using System.Collections.Generic;
using UnityEngine;

public class CatCustomization : MonoBehaviour
{
	[SerializeField]
	private SkinnedMeshRenderer eyesMesh;

	[SerializeField]
	private SkinnedMeshRenderer bodyMesh;

	[SerializeField]
	private SkinnedMeshRenderer furMesh;

	[SerializeField]
	private SkinnedMeshRenderer whiskersMesh;

	[SerializeField]
	private List<GameObject> hatObjects;

	public void SetBlendShapeValue(BlendShapeKey key)
	{
	}

	public void ChangeBodyColor(Material bodyMat, Material furMat)
	{
	}

	public void ChangeEyesColor(Material eyesMat)
	{
	}

	public void SetHatType(int type)
	{
	}
}
