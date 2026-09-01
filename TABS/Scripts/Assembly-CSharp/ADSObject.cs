using System.Collections.Generic;
using UnityEngine;

[HelpURL("https://docs.google.com/document/d/13vul0zDF478he8hhteKjnxoLYgfW47G0Z9TSox21_J0/edit#heading=h.rp8ji698m9wz")]
[DisallowMultipleComponent]
[ExecuteInEditMode]
public class ADSObject : MonoBehaviour
{
	private Mesh sharedMesh;

	private void Awake()
	{
		if (base.gameObject.GetComponent<MeshFilter>() != null && base.gameObject.GetComponent<MeshFilter>().sharedMesh != null && Object.FindObjectOfType<ADSGlobals>() != null)
		{
			ADSGlobals component = Object.FindObjectOfType<ADSGlobals>().GetComponent<ADSGlobals>();
			sharedMesh = base.gameObject.GetComponent<MeshFilter>().sharedMesh;
			if (!component.ADSObjects.Contains(sharedMesh))
			{
				component.ADSObjects.Add(sharedMesh);
				UpdateUV3();
			}
		}
	}

	private void UpdateUV3()
	{
		List<Vector3> list = new List<Vector3>();
		for (int i = 0; i < sharedMesh.vertices.Length; i++)
		{
			list.Add(new Vector4(sharedMesh.vertices[i].x, sharedMesh.vertices[i].y, sharedMesh.vertices[i].z));
		}
		sharedMesh.SetUVs(3, list);
		list = new List<Vector3>();
	}
}
