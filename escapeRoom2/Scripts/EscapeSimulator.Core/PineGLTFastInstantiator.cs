using System.Collections.Generic;
using GLTFast;
using GLTFast.Logging;
using Unity.Collections;
using UnityEngine;

public class PineGLTFastInstantiator : IInstantiator
{
	public class SceneInstance
	{
		public List<Camera> cameras { get; private set; }

		public void AddCamera(Camera camera)
		{
			if (cameras == null)
			{
				cameras = new List<Camera>();
			}
			cameras.Add(camera);
		}
	}

	protected ICodeLogger logger;

	protected IGltfReadable gltf;

	protected Transform parent;

	private GameObject root;

	protected Dictionary<uint, GameObject> nodes = new Dictionary<uint, GameObject>();

	public SceneInstance sceneInstance { get; protected set; } = new SceneInstance();

	public PineGLTFastInstantiator(IGltfReadable gltf, Transform parent, ICodeLogger logger = null)
	{
		this.gltf = gltf;
		this.parent = parent;
		this.logger = logger;
	}

	public void CreateNode(uint nodeIndex, uint? parentIndex, Vector3 position, Quaternion rotation, Vector3 scale)
	{
		GameObject gameObject = new GameObject();
		gameObject.transform.localScale = scale;
		gameObject.transform.localPosition = position;
		gameObject.transform.localRotation = rotation;
		nodes[nodeIndex] = gameObject;
		if (parentIndex.HasValue)
		{
			if (nodes[nodeIndex] == null || nodes[parentIndex.Value] == null)
			{
				logger?.Error(LogCode.HierarchyInvalid);
			}
			else
			{
				nodes[nodeIndex].transform.SetParent(nodes[parentIndex.Value].transform, worldPositionStays: false);
			}
		}
	}

	public virtual void SetNodeName(uint nodeIndex, string name)
	{
		nodes[nodeIndex].name = name ?? $"Node-{nodeIndex}";
	}

	public void AddPrimitive(uint nodeIndex, string meshName, MeshResult mesh, uint[] joints, uint? rootJoint, float[] morphTargetWeights, int primitiveNumeration)
	{
		GameObject gameObject;
		if (primitiveNumeration == 0)
		{
			gameObject = nodes[nodeIndex];
		}
		else
		{
			gameObject = new GameObject(meshName);
			gameObject.transform.SetParent(nodes[nodeIndex].transform, worldPositionStays: false);
		}
		bool flag = mesh.mesh.blendShapeCount > 0;
		Renderer renderer;
		if (joints == null && !flag)
		{
			gameObject.AddComponent<MeshFilter>().mesh = mesh.mesh;
			MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
			renderer = meshRenderer;
			if (meshName.Split('.')[0].ToLower().EndsWith("#boxcollider"))
			{
				gameObject.AddComponent<BoxCollider>();
				meshRenderer.enabled = false;
			}
		}
		else
		{
			SkinnedMeshRenderer skinnedMeshRenderer = gameObject.AddComponent<SkinnedMeshRenderer>();
			if (joints != null)
			{
				Transform[] array = new Transform[joints.Length];
				for (int i = 0; i < array.Length; i++)
				{
					uint key = joints[i];
					array[i] = nodes[key].transform;
				}
				skinnedMeshRenderer.bones = array;
				if (rootJoint.HasValue)
				{
					skinnedMeshRenderer.rootBone = nodes[rootJoint.Value].transform;
				}
			}
			skinnedMeshRenderer.sharedMesh = mesh.mesh;
			if (morphTargetWeights != null)
			{
				for (int j = 0; j < morphTargetWeights.Length; j++)
				{
					float value = morphTargetWeights[j];
					skinnedMeshRenderer.SetBlendShapeWeight(j, value);
				}
			}
			renderer = skinnedMeshRenderer;
		}
		int[] materialIndices = mesh.materialIndices;
		Material[] array2 = new Material[materialIndices.Length];
		for (int k = 0; k < array2.Length; k++)
		{
			Material material = gltf.GetMaterial(materialIndices[k]) ?? gltf.GetDefaultMaterial();
			array2[k] = material;
		}
		renderer.sharedMaterials = array2;
	}

	public void AddPrimitiveInstanced(uint nodeIndex, string meshName, MeshResult mesh, uint instanceCount, NativeArray<Vector3>? positions, NativeArray<Quaternion>? rotations, NativeArray<Vector3>? scales, int primitiveNumeration)
	{
		int[] materialIndices = mesh.materialIndices;
		Material[] array = new Material[materialIndices.Length];
		for (int i = 0; i < array.Length; i++)
		{
			Material material = gltf.GetMaterial(materialIndices[i]) ?? gltf.GetDefaultMaterial();
			material.enableInstancing = true;
			array[i] = material;
		}
		for (int j = 0; j < instanceCount; j++)
		{
			GameObject gameObject = new GameObject($"{meshName}_i{j}");
			Transform transform = gameObject.transform;
			transform.SetParent(nodes[nodeIndex].transform, worldPositionStays: false);
			transform.localPosition = positions?[j] ?? Vector3.zero;
			transform.localRotation = rotations?[j] ?? Quaternion.identity;
			transform.localScale = scales?[j] ?? Vector3.one;
			gameObject.AddComponent<MeshFilter>().mesh = mesh.mesh;
			gameObject.AddComponent<MeshRenderer>().sharedMaterials = array;
		}
	}

	public void AddCamera(uint nodeIndex, uint cameraIndex)
	{
	}

	public void AddLightPunctual(uint nodeIndex, uint lightIndex)
	{
	}

	public void EndScene(uint[] rootNodeIndices)
	{
		if (rootNodeIndices == null)
		{
			return;
		}
		foreach (uint key in rootNodeIndices)
		{
			if (nodes[key] != null)
			{
				nodes[key].transform.SetParent(root.transform, worldPositionStays: false);
			}
		}
	}

	public void AddAnimation(AnimationClip[] animationClips)
	{
		if (animationClips == null || animationClips.Length == 0 || !animationClips[0].legacy)
		{
			return;
		}
		Animation animation = root.AddComponent<Animation>();
		for (int i = 0; i < animationClips.Length; i++)
		{
			AnimationClip animationClip = animationClips[i];
			animation.AddClip(animationClip, animationClip.name);
			if (i < 1)
			{
				animation.clip = animationClip;
			}
		}
		animation.Play();
	}

	public void BeginScene(string name, uint[] nodeIndices)
	{
		GameObject gameObject = new GameObject(name ?? "Scene");
		gameObject.transform.SetParent(parent, worldPositionStays: false);
		root = gameObject;
	}
}
