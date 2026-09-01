using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LineRendererBaker : MonoBehaviour
{
	[Tooltip("Include inactive LineRenderers when searching children.")]
	public bool includeInactive;

	[Tooltip("Name for the generated baked child GameObject.")]
	public string bakedChildName;

	[Tooltip("If true, logs per-line and per-submesh processing details.")]
	public bool verboseLogging;

	[Tooltip("Copy normals from baked line meshes (usually unnecessary).")]
	public bool copyNormals;

	[Tooltip("Copy tangents from baked line meshes (usually unnecessary).")]
	public bool copyTangents;

	[Tooltip("Force every baked line to use a single material & single submesh (reduces draw calls further). First valid sharedMaterial found becomes the master material.")]
	public bool forceSingleMaterial;

	[Tooltip("Optional explicit override for the material when forceSingleMaterial is true. If left null, first encountered line's sharedMaterial is used.")]
	public Material overrideSingleMaterial;

	[SerializeField]
	[Tooltip("True if a bake currently exists.")]
	private bool baked;

	[SerializeField]
	private GameObject bakedChild;

	[SerializeField]
	private List<GameObject> disabledOriginals;

	public bool IsBaked => false;

	public void Bake()
	{
	}

	public void Unbake()
	{
	}

	private void DestroyTempMesh(Mesh m)
	{
	}
}
