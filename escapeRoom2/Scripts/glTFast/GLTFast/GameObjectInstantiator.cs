using System;
using System.Collections.Generic;
using GLTFast.Logging;
using GLTFast.Schema;
using Unity.Collections;
using UnityEngine;

namespace GLTFast
{
	public class GameObjectInstantiator : IInstantiator
	{
		public delegate void NodeCreatedDelegate(uint nodeIndex, GameObject gameObject);

		public delegate void MeshAddedDelegate(GameObject gameObject, uint nodeIndex, string meshName, MeshResult meshResult, uint[] joints = null, uint? rootJoint = null, float[] morphTargetWeights = null, int meshNumeration = 0);

		protected InstantiationSettings m_Settings;

		protected ICodeLogger m_Logger;

		protected IGltfReadable m_Gltf;

		protected Transform m_Parent;

		protected Dictionary<uint, GameObject> m_Nodes;

		private List<IMaterialsVariantsSlotInstance> m_InstanceSlots;

		public Transform SceneTransform { get; protected set; }

		public GameObjectSceneInstance SceneInstance { get; protected set; }

		internal ImportSettings ImportSettings { get; set; }

		public event NodeCreatedDelegate NodeCreated;

		public event MeshAddedDelegate MeshAdded;

		public event Action EndSceneCompleted;

		public GameObjectInstantiator(IGltfReadable gltf, Transform parent, ICodeLogger logger = null, InstantiationSettings settings = null)
		{
			m_Gltf = gltf;
			m_Parent = parent;
			m_Logger = logger;
			m_Settings = settings ?? new InstantiationSettings();
		}

		public virtual void BeginScene(string name, uint[] rootNodeIndices)
		{
			m_Nodes = new Dictionary<uint, GameObject>();
			SceneInstance = new GameObjectSceneInstance();
			GameObject gameObject;
			if (m_Settings.SceneObjectCreation == SceneObjectCreation.Never || (m_Settings.SceneObjectCreation == SceneObjectCreation.WhenMultipleRootNodes && rootNodeIndices.Length == 1))
			{
				gameObject = m_Parent.gameObject;
			}
			else
			{
				gameObject = new GameObject(name ?? "Scene");
				gameObject.transform.SetParent(m_Parent, worldPositionStays: false);
				gameObject.layer = m_Settings.Layer;
			}
			SceneTransform = gameObject.transform;
		}

		public virtual void AddAnimation(AnimationClip[] animationClips)
		{
			if ((m_Settings.Mask & ComponentType.Animation) == 0 || animationClips == null)
			{
				return;
			}
			if (animationClips.Length != 0 && animationClips[0].legacy)
			{
				UnityEngine.Animation animation = SceneTransform.gameObject.AddComponent<UnityEngine.Animation>();
				for (int i = 0; i < animationClips.Length; i++)
				{
					AnimationClip animationClip = animationClips[i];
					animation.AddClip(animationClip, animationClip.name);
					if (i < 1)
					{
						animation.clip = animationClip;
					}
				}
				SceneInstance.SetLegacyAnimation(animation);
				return;
			}
			SceneTransform.gameObject.AddComponent<Animator>();
			if (ImportSettings.AnimationMethod == AnimationMethod.Playables)
			{
				AnimationPlayableComponent animationPlayableComponent = SceneTransform.gameObject.AddComponent<AnimationPlayableComponent>();
				animationPlayableComponent.Init(animationClips, autoSequence: false);
				if (animationPlayableComponent.Playable.HasValue)
				{
					SceneInstance.Playable = animationPlayableComponent.Playable.Value;
				}
			}
		}

		public void CreateNode(uint nodeIndex, uint? parentIndex, Vector3 position, Quaternion rotation, Vector3 scale)
		{
			GameObject gameObject = new GameObject();
			gameObject.SetActive(parentIndex.HasValue);
			gameObject.transform.localScale = scale;
			gameObject.transform.localPosition = position;
			gameObject.transform.localRotation = rotation;
			gameObject.layer = m_Settings.Layer;
			m_Nodes[nodeIndex] = gameObject;
			gameObject.transform.SetParent(parentIndex.HasValue ? m_Nodes[parentIndex.Value].transform : SceneTransform, worldPositionStays: false);
			this.NodeCreated?.Invoke(nodeIndex, gameObject);
		}

		public virtual void SetNodeName(uint nodeIndex, string name)
		{
			m_Nodes[nodeIndex].name = name ?? $"Node-{nodeIndex}";
		}

		public virtual void AddPrimitive(uint nodeIndex, string meshName, MeshResult meshResult, uint[] joints = null, uint? rootJoint = null, float[] morphTargetWeights = null, int meshNumeration = 0)
		{
			if ((m_Settings.Mask & ComponentType.Mesh) == 0)
			{
				return;
			}
			GameObject gameObject;
			if (meshNumeration == 0)
			{
				gameObject = m_Nodes[nodeIndex];
			}
			else
			{
				gameObject = new GameObject(meshName);
				gameObject.transform.SetParent(m_Nodes[nodeIndex].transform, worldPositionStays: false);
				gameObject.layer = m_Settings.Layer;
			}
			bool flag = meshResult.mesh.blendShapeCount > 0;
			Renderer renderer;
			if (joints == null && !flag)
			{
				gameObject.AddComponent<MeshFilter>().mesh = meshResult.mesh;
				renderer = gameObject.AddComponent<MeshRenderer>();
			}
			else
			{
				SkinnedMeshRenderer skinnedMeshRenderer = gameObject.AddComponent<SkinnedMeshRenderer>();
				skinnedMeshRenderer.updateWhenOffscreen = m_Settings.SkinUpdateWhenOffscreen;
				if (joints != null)
				{
					Transform[] array = new Transform[joints.Length];
					for (int i = 0; i < array.Length; i++)
					{
						uint key = joints[i];
						array[i] = m_Nodes[key].transform;
					}
					skinnedMeshRenderer.bones = array;
					if (rootJoint.HasValue)
					{
						skinnedMeshRenderer.rootBone = m_Nodes[rootJoint.Value].transform;
					}
				}
				skinnedMeshRenderer.sharedMesh = meshResult.mesh;
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
			UnityEngine.Material[] array2 = new UnityEngine.Material[meshResult.materialIndices.Length];
			for (int k = 0; k < array2.Length; k++)
			{
				UnityEngine.Material material = m_Gltf.GetMaterial(meshResult.materialIndices[k]) ?? m_Gltf.GetDefaultMaterial();
				array2[k] = material;
			}
			renderer.sharedMaterials = array2;
			IMaterialsVariantsSlot[] materialsVariantsSlots = m_Gltf.GetMaterialsVariantsSlots(meshResult.meshIndex, meshNumeration);
			if (materialsVariantsSlots != null && materialsVariantsSlots.Length != 0)
			{
				if (m_InstanceSlots == null)
				{
					m_InstanceSlots = new List<IMaterialsVariantsSlotInstance>();
				}
				MaterialsVariantsSlotInstances materialsVariantsSlotInstances = new MaterialsVariantsSlotInstances(renderer, materialsVariantsSlots);
				m_InstanceSlots.Add(materialsVariantsSlotInstances);
			}
			this.MeshAdded?.Invoke(gameObject, nodeIndex, meshName, meshResult, joints, rootJoint, morphTargetWeights, meshNumeration);
		}

		public virtual void AddPrimitiveInstanced(uint nodeIndex, string meshName, MeshResult meshResult, uint instanceCount, NativeArray<Vector3>? positions, NativeArray<Quaternion>? rotations, NativeArray<Vector3>? scales, int meshNumeration = 0)
		{
			if ((m_Settings.Mask & ComponentType.Mesh) == 0)
			{
				return;
			}
			UnityEngine.Material[] array = new UnityEngine.Material[meshResult.materialIndices.Length];
			for (int i = 0; i < array.Length; i++)
			{
				UnityEngine.Material material = m_Gltf.GetMaterial(meshResult.materialIndices[i]) ?? m_Gltf.GetDefaultMaterial();
				material.enableInstancing = true;
				array[i] = material;
			}
			IMaterialsVariantsSlot[] materialsVariantsSlots = m_Gltf.GetMaterialsVariantsSlots(meshResult.meshIndex, meshNumeration);
			bool flag = materialsVariantsSlots != null && materialsVariantsSlots.Length != 0;
			Renderer[] array2 = (flag ? new Renderer[instanceCount] : null);
			for (int j = 0; j < instanceCount; j++)
			{
				GameObject obj = new GameObject($"{meshName}_i{j}")
				{
					layer = m_Settings.Layer
				};
				Transform transform = obj.transform;
				transform.SetParent(m_Nodes[nodeIndex].transform, worldPositionStays: false);
				transform.localPosition = positions?[j] ?? Vector3.zero;
				transform.localRotation = rotations?[j] ?? Quaternion.identity;
				transform.localScale = scales?[j] ?? Vector3.one;
				obj.AddComponent<MeshFilter>().mesh = meshResult.mesh;
				Renderer renderer = obj.AddComponent<MeshRenderer>();
				renderer.sharedMaterials = array;
				if (flag)
				{
					array2[j] = renderer;
				}
			}
			if (flag)
			{
				if (m_InstanceSlots == null)
				{
					m_InstanceSlots = new List<IMaterialsVariantsSlotInstance>();
				}
				MultiMaterialsVariantsSlotInstances multiMaterialsVariantsSlotInstances = new MultiMaterialsVariantsSlotInstances(array2, materialsVariantsSlots);
				m_InstanceSlots.Add(multiMaterialsVariantsSlotInstances);
			}
		}

		public virtual void AddCamera(uint nodeIndex, uint cameraIndex)
		{
			if ((m_Settings.Mask & ComponentType.Camera) != ComponentType.None)
			{
				CameraBase sourceCamera = m_Gltf.GetSourceCamera(cameraIndex);
				switch (sourceCamera.GetCameraType())
				{
				case CameraBase.Type.Orthographic:
				{
					CameraOrthographic orthographic = sourceCamera.Orthographic;
					AddCameraOrthographic(nodeIndex, orthographic.znear, (orthographic.zfar >= 0f) ? new float?(orthographic.zfar) : ((float?)null), orthographic.xmag, orthographic.ymag, sourceCamera.name);
					break;
				}
				case CameraBase.Type.Perspective:
				{
					CameraPerspective perspective = sourceCamera.Perspective;
					AddCameraPerspective(nodeIndex, perspective.yfov, perspective.znear, perspective.zfar, (perspective.aspectRatio > 0f) ? new float?(perspective.aspectRatio) : ((float?)null), sourceCamera.name);
					break;
				}
				}
			}
		}

		private void AddCameraPerspective(uint nodeIndex, float verticalFieldOfView, float nearClipPlane, float farClipPlane, float? aspectRatio, string cameraName)
		{
			float localScale;
			UnityEngine.Camera camera = CreateCamera(nodeIndex, cameraName, out localScale);
			camera.orthographic = false;
			camera.fieldOfView = verticalFieldOfView * 57.29578f;
			camera.nearClipPlane = nearClipPlane * localScale;
			camera.farClipPlane = farClipPlane * localScale;
		}

		private void AddCameraOrthographic(uint nodeIndex, float nearClipPlane, float? farClipPlane, float horizontal, float vertical, string cameraName)
		{
			float localScale;
			UnityEngine.Camera camera = CreateCamera(nodeIndex, cameraName, out localScale);
			float num = farClipPlane ?? float.MaxValue;
			camera.orthographic = true;
			camera.nearClipPlane = nearClipPlane * localScale;
			camera.farClipPlane = num * localScale;
			camera.orthographicSize = vertical;
			camera.projectionMatrix = Matrix4x4.Ortho(0f - horizontal, horizontal, 0f - vertical, vertical, nearClipPlane, num);
		}

		private UnityEngine.Camera CreateCamera(uint nodeIndex, string cameraName, out float localScale)
		{
			GameObject gameObject = m_Nodes[nodeIndex];
			GameObject obj = new GameObject(string.IsNullOrEmpty(cameraName) ? $"Camera-{nodeIndex}" : (gameObject.name + "-Camera"))
			{
				layer = m_Settings.Layer
			};
			Transform transform = obj.transform;
			Transform transform2 = gameObject.transform;
			transform.SetParent(transform2, worldPositionStays: false);
			Quaternion localRotation = Quaternion.Euler(0f, 180f, 0f);
			transform.localRotation = localRotation;
			UnityEngine.Camera camera = obj.AddComponent<UnityEngine.Camera>();
			camera.enabled = false;
			SceneInstance.AddCamera(camera);
			Vector3 lossyScale = transform2.localToWorldMatrix.lossyScale;
			localScale = (lossyScale.x + lossyScale.y + lossyScale.y) / 3f;
			return camera;
		}

		public void AddLightPunctual(uint nodeIndex, uint lightIndex)
		{
			if ((m_Settings.Mask & ComponentType.Light) != ComponentType.None)
			{
				GameObject gameObject = m_Nodes[nodeIndex];
				LightPunctual sourceLightPunctual = m_Gltf.GetSourceLightPunctual(lightIndex);
				if (sourceLightPunctual.GetLightType() != LightPunctual.Type.Point)
				{
					GameObject gameObject2 = new GameObject(gameObject.name + "_Orientation");
					gameObject2.transform.SetParent(gameObject.transform, worldPositionStays: false);
					gameObject2.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
					gameObject = gameObject2;
				}
				Light light = gameObject.AddComponent<Light>();
				sourceLightPunctual.ToUnityLight(light, m_Settings.LightIntensityFactor);
				SceneInstance.AddLight(light);
			}
		}

		public virtual void EndScene(uint[] rootNodeIndices)
		{
			if (m_InstanceSlots != null)
			{
				MaterialsVariantsControl materialsVariantsControl = new MaterialsVariantsControl(m_Gltf, m_InstanceSlots);
				SceneInstance.SetMaterialsVariantsControl(materialsVariantsControl);
			}
			if (rootNodeIndices != null)
			{
				foreach (uint key in rootNodeIndices)
				{
					m_Nodes[key].SetActive(value: true);
				}
			}
			this.EndSceneCompleted?.Invoke();
		}
	}
}
