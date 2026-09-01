using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace LevelCreator
{
	[RequireComponent(typeof(MeshFilter))]
	public class VolumeMeshChunk : MonoBehaviour
	{
		private struct Plant
		{
			public Vector3 position;

			public float lifeTime;
		}

		private enum FoliageMeshStatus
		{
			Built = 0,
			Building = 1,
			Incorrect = 2
		}

		private class Foliage
		{
			public GameObject gameObject;

			public List<CombineInstance> combineInstances = new List<CombineInstance>();
		}

		public Vector3Int chunkPosition;

		public float[,,] oldVoxelDensities;

		public Level.VoxelChunk voxelChunk;

		public float[,,] oldMaterialDensities;

		public Level.MaterialChunk materialChunk;

		public float[,,] oldFoliageDensities;

		public Level.FoliageChunk foliageChunk;

		public GameObject FoliagePrefab;

		private MeshData m_meshData = new MeshData();

		private Mesh m_volumeMesh;

		private List<Plant> m_plants = new List<Plant>();

		private Dictionary<Vector3, float> m_plantLifeTimes = new Dictionary<Vector3, float>();

		private FoliageMeshStatus m_foliageMeshStatus;

		private Dictionary<Material, Foliage> m_foliages = new Dictionary<Material, Foliage>();

		public void Init(Vector3Int chunkPosition, Level.VolumeChunk volumeChunk)
		{
			this.chunkPosition = chunkPosition;
			SetVolumeChunk(volumeChunk);
		}

		public void CommitChunkUpdates()
		{
			oldVoxelDensities = null;
			oldMaterialDensities = null;
			oldFoliageDensities = null;
		}

		public void Invalidate()
		{
			CommitChunkUpdates();
			voxelChunk.version = -1;
			materialChunk.version = -1;
			foliageChunk.version = -1;
		}

		public void SetVolumeChunk(Level.VolumeChunk volumeChunk)
		{
			CommitChunkUpdates();
			if (volumeChunk.voxelChunk == null)
			{
				throw new Exception("SetVolumeChunk: volumeChunk.voxelChunk == null");
			}
			if (volumeChunk.materialChunk == null)
			{
				throw new Exception("SetVolumeChunk: volumeChunk.materialChunk == null");
			}
			if (volumeChunk.foliageChunk == null)
			{
				throw new Exception("SetVolumeChunk: volumeChunk.foliageChunk == null");
			}
			voxelChunk = volumeChunk.voxelChunk;
			materialChunk = volumeChunk.materialChunk;
			foliageChunk = volumeChunk.foliageChunk;
		}

		public Level.VolumeChunk CloneVolumeChunk()
		{
			CommitChunkUpdates();
			return new Level.VolumeChunk
			{
				voxelChunk = new Level.VoxelChunk
				{
					densities = voxelChunk.densities,
					version = voxelChunk.version
				},
				materialChunk = new Level.MaterialChunk
				{
					densities = materialChunk.densities,
					version = materialChunk.version
				},
				foliageChunk = new Level.FoliageChunk
				{
					densities = foliageChunk.densities,
					version = foliageChunk.version
				}
			};
		}

		public int VoxelVersion()
		{
			return voxelChunk.version;
		}

		public int MaterialVersion()
		{
			return materialChunk.version;
		}

		public int FoliageVersion()
		{
			return foliageChunk.version;
		}

		public Level.VoxelChunk GetReadableVoxelChunk()
		{
			return voxelChunk;
		}

		public Level.VoxelChunk GetModifiableVoxelChunk()
		{
			if (oldVoxelDensities == null)
			{
				oldVoxelDensities = voxelChunk.densities;
				voxelChunk = new Level.VoxelChunk
				{
					densities = (float[,,])voxelChunk.densities.Clone(),
					version = voxelChunk.version + 1
				};
			}
			return voxelChunk;
		}

		public Level.MaterialChunk GetModifiableMaterialChunk()
		{
			if (oldMaterialDensities == null)
			{
				oldMaterialDensities = materialChunk.densities;
				materialChunk = new Level.MaterialChunk
				{
					densities = (float[,,])materialChunk.densities.Clone(),
					version = materialChunk.version + 1
				};
			}
			return materialChunk;
		}

		public Level.FoliageChunk GetModifiableFoliageChunk()
		{
			if (oldFoliageDensities == null)
			{
				oldFoliageDensities = foliageChunk.densities;
				foliageChunk = new Level.FoliageChunk
				{
					densities = (float[,,])foliageChunk.densities.Clone(),
					version = foliageChunk.version + 1
				};
			}
			return foliageChunk;
		}

		public float[,,] GetOldVoxelDensities()
		{
			return oldVoxelDensities;
		}

		public float[,,] GetOldMaterialDensities()
		{
			return oldMaterialDensities;
		}

		public float[,,] GetOldFoliageDensities()
		{
			return oldFoliageDensities;
		}

		private float GetDensity(Vector3Int pos)
		{
			return voxelChunk.densities[pos.z, pos.y, pos.x];
		}

		public float BuildMeshData()
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			MeshBuilder.BuildMeshData(m_meshData, voxelChunk.densities, materialChunk.densities, chunkPosition);
			stopwatch.Stop();
			return (float)(stopwatch.Elapsed.TotalMilliseconds / 1000.0);
		}

		public void BuildVolumeMesh(VertexArrays vertexArraysBuffer)
		{
			m_volumeMesh = (m_volumeMesh ? m_volumeMesh : new Mesh());
			m_volumeMesh.Clear();
			if (m_meshData.indices.Count > 0)
			{
				vertexArraysBuffer.CopyFrom(m_meshData);
				m_volumeMesh.vertices = vertexArraysBuffer.positions;
				m_volumeMesh.normals = vertexArraysBuffer.normals;
				m_volumeMesh.uv = vertexArraysBuffer.materials;
				m_volumeMesh.triangles = m_meshData.indices.ToArray();
			}
			GetComponent<MeshFilter>().mesh = m_volumeMesh;
			MeshCollider component = GetComponent<MeshCollider>();
			if (component != null)
			{
				component.sharedMesh = null;
				component.sharedMesh = m_volumeMesh;
			}
		}

		public void BuildFoliage(bool instantGrow, Vector3Int chunkPosition)
		{
			m_plantLifeTimes.Clear();
			foreach (Plant plant in m_plants)
			{
				m_plantLifeTimes.Add(plant.position, plant.lifeTime);
			}
			m_plants.Clear();
			FoliageBuilder.ForeachPlant(foliageChunk, chunkPosition, m_meshData, delegate(Vector3 bladePosition)
			{
				m_plants.Add(new Plant
				{
					position = bladePosition,
					lifeTime = (instantGrow ? 1f : (m_plantLifeTimes.TryGetValue(bladePosition, out var value) ? value : 0f))
				});
			});
			if (m_plants.Count > 0)
			{
				m_foliageMeshStatus = FoliageMeshStatus.Building;
			}
			if (m_plants.Count == 0 && m_plantLifeTimes.Count != 0)
			{
				m_foliageMeshStatus = FoliageMeshStatus.Building;
			}
			if (instantGrow && m_foliageMeshStatus == FoliageMeshStatus.Building)
			{
				m_foliageMeshStatus = FoliageMeshStatus.Incorrect;
			}
		}

		public void UpdateFoliageIfDirty(float deltaSeconds)
		{
			if (m_foliageMeshStatus == FoliageMeshStatus.Built)
			{
				return;
			}
			for (int i = 0; i < m_plants.Count; i++)
			{
				Plant value = m_plants[i];
				if (value.lifeTime < 1f)
				{
					value.lifeTime = ((m_foliageMeshStatus == FoliageMeshStatus.Building) ? Mathf.Clamp01(value.lifeTime + deltaSeconds * 3f) : 1f);
					m_plants[i] = value;
				}
			}
		}

		public void DirtyFlagFoliageMesh()
		{
			if (m_foliageMeshStatus == FoliageMeshStatus.Built)
			{
				m_foliageMeshStatus = FoliageMeshStatus.Building;
			}
		}

		public bool BuildFoliageMeshIfDirty(List<FoliageData> foliageItems)
		{
			if (m_foliageMeshStatus == FoliageMeshStatus.Built)
			{
				return false;
			}
			if (foliageItems == null)
			{
				UnityEngine.Debug.LogWarning("foliageItems is null");
				foreach (KeyValuePair<Material, Foliage> foliage in m_foliages)
				{
					if (foliage.Value != null && foliage.Value.gameObject != null)
					{
						UnityEngine.Object.Destroy(foliage.Value.gameObject);
					}
				}
				m_foliages.Clear();
				m_foliageMeshStatus = FoliageMeshStatus.Built;
				return false;
			}
			foreach (KeyValuePair<Material, Foliage> foliage2 in m_foliages)
			{
				foliage2.Value.combineInstances.Clear();
			}
			bool flag = true;
			if (m_plants.Count > 0)
			{
				int count = m_plants.Count;
				for (int i = 0; i < count; i++)
				{
					Plant plant = m_plants[i];
					if (plant.lifeTime < 1f)
					{
						flag = false;
					}
					int plantSeedIndex = FoliageBuilder.GetPlantSeedIndex(plant.position, foliageItems.Count);
					FoliageData foliageData = foliageItems[plantSeedIndex];
					Vector3 s = Vector3.one * (FoliageBuilder.GetPlantScale(plant.position, foliageData.ScaleMultiplierMinMax) * ((plant.lifeTime >= 1f) ? 1f : Mathf.Sin(Mathf.Clamp01(plant.lifeTime) * (float)Math.PI * 0.5f)));
					Quaternion plantRotation = FoliageBuilder.GetPlantRotation(plant.position);
					if (!m_foliages.TryGetValue(foliageData.sharedMaterial, out var value))
					{
						value = new Foliage
						{
							gameObject = UnityEngine.Object.Instantiate(FoliagePrefab, base.transform)
						};
						m_foliages.Add(foliageData.sharedMaterial, value);
					}
					value.combineInstances.Add(new CombineInstance
					{
						mesh = foliageData.sharedMesh,
						transform = Matrix4x4.TRS(plant.position, plantRotation, s)
					});
				}
				List<Material> list = new List<Material>();
				foreach (KeyValuePair<Material, Foliage> foliage3 in m_foliages)
				{
					if (foliage3.Value.combineInstances.Count > 0)
					{
						foliage3.Value.gameObject.GetComponent<MeshFilter>().mesh.CombineMeshes(foliage3.Value.combineInstances.ToArray(), mergeSubMeshes: true);
						foliage3.Value.gameObject.GetComponent<MeshRenderer>().material = foliage3.Key;
						foliage3.Value.gameObject.GetComponent<MeshRenderer>().enabled = true;
					}
					else
					{
						list.Add(foliage3.Key);
					}
				}
				foreach (Material item in list)
				{
					if (m_foliages.TryGetValue(item, out var value2))
					{
						UnityEngine.Object.Destroy(value2.gameObject);
					}
					m_foliages.Remove(item);
				}
			}
			else
			{
				foreach (KeyValuePair<Material, Foliage> foliage4 in m_foliages)
				{
					UnityEngine.Object.Destroy(foliage4.Value.gameObject);
				}
				m_foliages.Clear();
			}
			if (flag)
			{
				m_foliageMeshStatus = FoliageMeshStatus.Built;
			}
			return true;
		}

		public float BuildMesh(VertexArrays vertexArraysBuffer, List<FoliageData> foliageItems, Vector3Int chunkPosition)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			BuildVolumeMesh(vertexArraysBuffer);
			BuildFoliage(instantGrow: true, chunkPosition);
			m_foliageMeshStatus = FoliageMeshStatus.Incorrect;
			UpdateFoliageIfDirty(0f);
			BuildFoliageMeshIfDirty(foliageItems);
			stopwatch.Stop();
			return (float)(stopwatch.Elapsed.TotalMilliseconds / 1000.0);
		}

		public Vector3? LineCast(Vector3 start, Vector3 end)
		{
			MeshCollider component = GetComponent<MeshCollider>();
			Ray ray = new Ray(start, end - start);
			if (component.Raycast(ray, out var hitInfo, (end - start).magnitude))
			{
				return hitInfo.point;
			}
			return null;
		}

		public void Update()
		{
			UpdateFoliageIfDirty(Time.deltaTime);
		}
	}
}
