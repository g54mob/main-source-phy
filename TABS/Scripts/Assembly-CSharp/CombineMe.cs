using System.Collections.Generic;
using UnityEngine;

public class CombineMe : MonoBehaviour
{
	public bool m_combineOnStart;

	private void Start()
	{
		if (m_combineOnStart)
		{
			Combine();
		}
	}

	public void Combine()
	{
		MeshFilter component = GetComponent<MeshFilter>();
		List<CombineInstance> list = new List<CombineInstance>();
		for (int i = 0; i < component.sharedMesh.subMeshCount; i++)
		{
			list.Add(new CombineInstance
			{
				mesh = component.sharedMesh,
				subMeshIndex = i,
				transform = Matrix4x4.identity
			});
		}
		Mesh mesh = new Mesh();
		mesh.CombineMeshes(list.ToArray());
		component.sharedMesh = mesh;
	}

	public void CombineSkinned()
	{
		SkinnedMeshRenderer component = GetComponent<SkinnedMeshRenderer>();
		List<CombineInstance> list = new List<CombineInstance>();
		for (int i = 0; i < component.sharedMesh.subMeshCount; i++)
		{
			list.Add(new CombineInstance
			{
				mesh = component.sharedMesh,
				subMeshIndex = i,
				transform = component.transform.localToWorldMatrix
			});
		}
		Mesh mesh = new Mesh();
		mesh.CombineMeshes(list.ToArray());
		component.sharedMesh = mesh;
	}
}
