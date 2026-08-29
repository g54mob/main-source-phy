using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using GLTFast.Logging;
using Unity.Mathematics;
using UnityEngine;

namespace GLTFast.Export
{
	public class GameObjectExport
	{
		private GltfWriter m_Writer;

		private IMaterialExport m_MaterialExport;

		private GameObjectExportSettings m_Settings;

		public GameObjectExport(ExportSettings exportSettings = null, GameObjectExportSettings gameObjectExportSettings = null, IMaterialExport materialExport = null, IDeferAgent deferAgent = null, ICodeLogger logger = null)
		{
			m_Settings = gameObjectExportSettings ?? new GameObjectExportSettings();
			m_Writer = new GltfWriter(exportSettings, deferAgent, logger);
			m_MaterialExport = materialExport ?? MaterialExport.GetDefaultMaterialExport();
		}

		public bool AddScene(GameObject[] gameObjects, string name = null)
		{
			return AddScene(gameObjects, float4x4.identity, name);
		}

		public bool AddScene(ICollection<GameObject> gameObjects, float4x4 origin, string name)
		{
			CertifyNotDisposed();
			List<uint> list = new List<uint>(gameObjects.Count);
			List<Material> tempMaterials = new List<Material>();
			bool flag = true;
			Queue<Transform> queue = new Queue<Transform>();
			Dictionary<Transform, uint> transformNodeId = new Dictionary<Transform, uint>();
			foreach (GameObject gameObject in gameObjects)
			{
				flag &= AddGameObject(gameObject, origin, queue, transformNodeId, out var nodeId);
				if (nodeId >= 0)
				{
					list.Add((uint)nodeId);
				}
			}
			while (queue.Count > 0)
			{
				Transform transform = queue.Dequeue();
				AddNodeComponents(transform, transformNodeId, tempMaterials);
			}
			if (list.Count > 0)
			{
				m_Writer.AddScene(list.ToArray(), name);
			}
			return flag;
		}

		public async Task<bool> SaveToFileAndDispose(string path, CancellationToken cancellationToken = default(CancellationToken))
		{
			CertifyNotDisposed();
			bool result = await m_Writer.SaveToFileAndDispose(path);
			m_Writer = null;
			return result;
		}

		public async Task<bool> SaveToStreamAndDispose(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			CertifyNotDisposed();
			bool result = await m_Writer.SaveToStreamAndDispose(stream);
			m_Writer = null;
			return result;
		}

		private void CertifyNotDisposed()
		{
			if (m_Writer == null)
			{
				throw new InvalidOperationException("GameObjectExport was already disposed");
			}
		}

		private bool AddGameObject(GameObject gameObject, float4x4? sceneOrigin, Queue<Transform> nodesQueue, Dictionary<Transform, uint> transformNodeId, out int nodeId)
		{
			if ((m_Settings.OnlyActiveInHierarchy && !gameObject.activeInHierarchy) || gameObject.CompareTag("EditorOnly"))
			{
				nodeId = -1;
				return true;
			}
			bool flag = true;
			int childCount = gameObject.transform.childCount;
			uint[] array = null;
			if (childCount > 0)
			{
				List<uint> list = new List<uint>(gameObject.transform.childCount);
				for (int i = 0; i < childCount; i++)
				{
					Transform child = gameObject.transform.GetChild(i);
					flag &= AddGameObject(child.gameObject, null, nodesQueue, transformNodeId, out var nodeId2);
					if (nodeId2 >= 0)
					{
						list.Add((uint)nodeId2);
					}
				}
				if (list.Count > 0)
				{
					array = list.ToArray();
				}
			}
			Transform transform = gameObject.transform;
			bool flag2 = ((1 << gameObject.layer) & (int)m_Settings.LayerMask) != 0;
			if (flag2 || array != null)
			{
				float3 translation;
				quaternion rotation;
				float3 scale;
				if (sceneOrigin.HasValue)
				{
					math.mul(sceneOrigin.Value, transform.localToWorldMatrix).Decompose(out translation, out rotation, out scale);
				}
				else
				{
					translation = transform.localPosition;
					rotation = transform.localRotation;
					scale = transform.localScale;
				}
				uint num = m_Writer.AddNode(translation, rotation, scale, array, gameObject.name);
				if (flag2)
				{
					nodesQueue.Enqueue(transform);
				}
				transformNodeId[transform] = num;
				nodeId = (int)num;
			}
			else
			{
				nodeId = -1;
			}
			return flag;
		}

		private void AddNodeComponents(Transform transform, Dictionary<Transform, uint> transformNodeId, List<Material> tempMaterials)
		{
			GameObject gameObject = transform.gameObject;
			uint nodeId = transformNodeId[transform];
			tempMaterials.Clear();
			Mesh mesh = null;
			Transform[] array = null;
			SkinnedMeshRenderer component3;
			if (gameObject.TryGetComponent<MeshFilter>(out var component))
			{
				if (gameObject.TryGetComponent<Renderer>(out var component2) && (component2.enabled || m_Settings.DisabledComponents))
				{
					mesh = component.sharedMesh;
					component2.GetSharedMaterials(tempMaterials);
				}
			}
			else if (gameObject.TryGetComponent<SkinnedMeshRenderer>(out component3) && (component3.enabled || m_Settings.DisabledComponents))
			{
				mesh = component3.sharedMesh;
				array = component3.bones;
				component3.GetSharedMaterials(tempMaterials);
			}
			int[] array2 = new int[tempMaterials.Count];
			for (int i = 0; i < tempMaterials.Count; i++)
			{
				Material material = tempMaterials[i];
				if (material != null && m_Writer.AddMaterial(material, out var materialId, m_MaterialExport))
				{
					array2[i] = materialId;
				}
				else
				{
					array2[i] = -1;
				}
			}
			if (mesh != null)
			{
				uint[] array3 = null;
				if (array != null)
				{
					array3 = new uint[array.Length];
					for (int j = 0; j < array.Length; j++)
					{
						Transform key = array[j];
						transformNodeId.TryGetValue(key, out array3[j]);
					}
				}
				m_Writer.AddMeshToNode((int)nodeId, mesh, array2, array3);
			}
			if (gameObject.TryGetComponent<Camera>(out var component4) && (component4.enabled || m_Settings.DisabledComponents) && m_Writer.AddCamera(component4, out var cameraId))
			{
				m_Writer.AddCameraToNode((int)nodeId, cameraId);
			}
			if (gameObject.TryGetComponent<Light>(out var component5) && (component5.enabled || m_Settings.DisabledComponents) && m_Writer.AddLight(component5, out var lightId))
			{
				m_Writer.AddLightToNode((int)nodeId, lightId);
			}
		}
	}
}
