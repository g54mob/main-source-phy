using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Landfall.TABS;
using Landfall.TABS_Input;
using Pathfinding;
using TFBGames;
using UIStateManager;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

namespace LevelCreator
{
	public class SpawnLevel : MonoBehaviour
	{
		public enum SpawnLevelState
		{
			NotStarted = 0,
			BuildLevelData = 1,
			InstantiateObjects = 2,
			PathfindingScan = 3,
			Done = 4
		}

		public struct FoliageData
		{
			public Vector2 ScaleMultiplierMinMax;

			public Mesh sharedMesh;

			public Material sharedMaterial;
		}

		private class Foliage
		{
			public GameObject gameObject;

			public List<CombineInstance> combineInstances = new List<CombineInstance>();
		}

		private const float BuildLevelWait = 0.3f;

		private const float BuildLevelFirstWait = 1f;

		[Space]
		[SerializeField]
		private DMEditorObjectTable editorObjectTable;

		[SerializeField]
		private DMEditorObjectTable editorObjectTableTriggerables;

		[SerializeField]
		private DMEditorObjectTable editorObjectTableEffects;

		[SerializeField]
		private GameObject volumeChunkPrefab;

		[SerializeField]
		private GameObject foliagePrefab;

		[SerializeField]
		private Material volumeMaterial;

		[SerializeField]
		private GameObject water;

		[SerializeField]
		private SeedCollectionTable seedCollectionTable;

		[SerializeField]
		private PostProcessVolume postProcess;

		[SerializeField]
		private Transform sun;

		[SerializeField]
		private Light directionalLight;

		[SerializeField]
		private Transform weatherParent;

		[SerializeField]
		private AstarPath pathFinding;

		private static InterfaceStateManager m_interfaceStateManager;

		private MeshData meshData = new MeshData();

		private VertexArrays vertexArrays = new VertexArrays();

		private static SpawnLevelState spawnLevelState;

		private Level loadedLevel;

		public static bool finishedPathfindingScan;

		public static string levelToSpawn;

		public static bool IsCustomLevelTestRun;

		private List<FoliageData> foliageItems;

		private Dictionary<Level.Entity, GameObject> m_instantiatedEntities = new Dictionary<Level.Entity, GameObject>();

		public static CustomMap CustomMap { get; private set; }

		public static bool IsCustomLevelScene => SceneManager.GetSceneByName("LevelScene").IsValid();

		public static SpawnLevelState GetSpawnLevelState => spawnLevelState;

		private void Awake()
		{
			DMEditorObjectTable mergedTable = ScriptableObject.CreateInstance<DMEditorObjectTable>();
			editorObjectTable.ForEachRow(delegate(string key, DMEditorObjectRow row)
			{
				mergedTable.AddRow(key, row);
			});
			editorObjectTableTriggerables.ForEachRow(delegate(string key, DMEditorObjectRow row)
			{
				mergedTable.AddRow(key, row);
			});
			editorObjectTableEffects.ForEachRow(delegate(string key, DMEditorObjectRow row)
			{
				mergedTable.AddRow(key, row);
			});
			editorObjectTable = mergedTable;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			if (levelToSpawn == Paths.TestMapPath && PlayerActions.Instance.InputType == InputType.Keyboard)
			{
				TutorialPopUps.ReturnToEditorPopUp(this);
			}
			if (levelToSpawn.Equals(string.Empty))
			{
				SetCustomMapToLoad(CampaignPlayerDataHolder.StoredCustomMap);
			}
			finishedPathfindingScan = false;
			LoadLevel(levelToSpawn);
		}

		private void Start()
		{
			m_interfaceStateManager = UnityEngine.Object.FindObjectOfType<InterfaceStateManager>();
		}

		private void OnDestroy()
		{
			levelToSpawn = string.Empty;
			IsCustomLevelTestRun = false;
			CustomMap = null;
		}

		public static void ReturnToEditor()
		{
			if (!(m_interfaceStateManager == null) && m_interfaceStateManager.IsDefaultState && IsCustomLevelTestRun)
			{
				if (levelToSpawn == Paths.TestMapPath)
				{
					TABSSceneManager.LoadLevelCreator(DMEditor.StartState.Edit, Paths.TestMapPath);
				}
				else
				{
					TABSSceneManager.LoadLevelCreator(DMEditor.StartState.Edit, levelToSpawn);
				}
			}
		}

		private void Update()
		{
			if (PlayerActions.Instance.m_playmode.WasPressed && finishedPathfindingScan)
			{
				ReturnToEditor();
			}
			switch (spawnLevelState)
			{
			case SpawnLevelState.BuildLevelData:
				StartCoroutine(BuildLevelData(base.transform, loadedLevel.scene, loadedLevel.volume));
				break;
			case SpawnLevelState.PathfindingScan:
				StartCoroutine(PathFindingScan());
				break;
			}
		}

		public static void SetCustomMapToLoad(CustomMap customMap)
		{
			if (customMap == null)
			{
				Debug.LogError("Custom map is null");
				return;
			}
			CampaignPlayerDataHolder.StoredCustomMap = customMap;
			CustomMap = customMap;
			if (customMap != null)
			{
				levelToSpawn = customMap.LevelPath;
			}
		}

		public void LoadLevel(string filePath)
		{
			try
			{
				spawnLevelState = SpawnLevelState.NotStarted;
				Utility.DestroyChildren(base.transform);
				if (string.IsNullOrEmpty(filePath))
				{
					Debug.LogError("Level file path is empty");
					PopupLoadFailed();
					return;
				}
				DMIOWrapper.File.Exists(filePath, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(bool exists)
				{
					if (!exists)
					{
						Debug.LogError("Level file path could not be found: " + filePath);
						PopupLoadFailed();
					}
					else
					{
						LoadLevelData(filePath);
					}
				});
			}
			catch (Exception ex)
			{
				Debug.LogError("Something went wrong assessing the file: " + ex.Message);
				PopupLoadFailed();
			}
		}

		private void LoadLevelData(string filePath)
		{
			try
			{
				CustomMap customMapFromLevelPath = LevelUtility.GetCustomMapFromLevelPath(filePath);
				if (customMapFromLevelPath != null)
				{
					CustomMap = customMapFromLevelPath;
				}
				else
				{
					Debug.LogError("Custom map is null: " + filePath);
				}
				Debug.Log("Loading " + filePath);
				DMIOWrapper.File.ReadAllBytes(filePath, FileHandlingFileType.CustomContentOrLocalStorageFile, delegate(byte[] bytes, Exception exception)
				{
					if (exception == null)
					{
						loadedLevel = LevelSerializer.Deserialize(Utility.Unzip(bytes));
						BuildLevelSettings(loadedLevel.settings);
						spawnLevelState = SpawnLevelState.BuildLevelData;
						MusicHandler service = ServiceLocator.GetService<MusicHandler>();
						int musicIndex = loadedLevel.settings.musicIndex;
						int num = ((AudioManager.MusicClips != null) ? AudioManager.MusicClips.Length : (-1));
						if (service != null && musicIndex >= 0 && musicIndex < num)
						{
							service.OverrideSongCategory = AudioManager.MusicClips[musicIndex];
							ServiceLocator.GetService<MusicHandler>().PlaySongPlacement(TABSSceneManager.CurrentLoadedMap);
						}
					}
					else
					{
						Debug.LogError("Loading map data failed to read all bytes: " + exception.Message);
						PopupLoadFailed();
					}
				});
			}
			catch (Exception ex)
			{
				Debug.LogError("Loading map data failed with exception: " + ex.Message);
				PopupLoadFailed();
			}
		}

		private static void PopupLoadFailed()
		{
			LeanTween.delayedCall(0.5f, (System.Action)delegate
			{
				ServiceLocator.GetService<ModalPanel>().PopUp("LC_LOAD_FAILED_POPUP", delegate
				{
					TABSSceneManager.LoadMainMenu();
				});
			});
		}

		private void BuildLevelSettings(Level.Settings settings)
		{
			UpdateLevelLighting(settings);
			for (int i = 0; i < weatherParent.childCount; i++)
			{
				weatherParent.GetChild(i).gameObject.SetActive(i == settings.weatherIndex);
			}
			LevelPresetData levelPresetData = (from x in LevelPresetData.GetAllPresets()
				where x.name == settings.presetName
				select x).FirstOrDefault();
			if (!(levelPresetData == null))
			{
				postProcess.profile = levelPresetData.PostProcessProfile;
				water.SetActive(settings.showWater);
				water.transform.position = Vector3.zero;
				water.transform.position = new Vector3(water.transform.position.x, settings.waterLevel, water.transform.position.z) + base.transform.position;
				UpdateLevelMaterials(levelPresetData);
			}
		}

		private void UpdateLevelMaterials(LevelPresetData levelPreset)
		{
			volumeMaterial.SetColor("_TopColor", levelPreset.TopColor);
			volumeMaterial.SetColor("_DirtColor", levelPreset.DirtColor);
			volumeMaterial.SetColor("_RockColor", levelPreset.RockColor);
			volumeMaterial.SetFloat("_BaseMetal", levelPreset.BaseMetallic);
			volumeMaterial.SetFloat("_BaseSmooth", levelPreset.BaseSmoothness);
			volumeMaterial.SetColor("_SecondCol", levelPreset.SecondColor);
			volumeMaterial.SetFloat("_SecondMetal", levelPreset.SecondMetallic);
			volumeMaterial.SetFloat("_SecondSmooth", levelPreset.SecondSmoothness);
			volumeMaterial.SetColor("_ThirdCol", levelPreset.ThirdColor);
			volumeMaterial.SetFloat("_ThirdMetal", levelPreset.ThirdMetallic);
			volumeMaterial.SetFloat("_ThirdSmooth", levelPreset.ThirdSmoothness);
			water.GetComponentInChildren<MeshRenderer>().material = levelPreset.WaterMaterial;
		}

		private void UpdateLevelLighting(Level.Settings settings)
		{
			sun.transform.localRotation = settings.timeOfDay;
			MeshRenderer[] componentsInChildren = sun.GetComponentsInChildren<MeshRenderer>();
			MeshRenderer meshRenderer = componentsInChildren[0];
			_ = componentsInChildren[1];
			float num = Mathf.Clamp01(Vector3.Dot(Vector3.up, meshRenderer.transform.position.normalized));
			bool flag = num <= 0f;
			float a = Mathf.Lerp(0f, 1f, Mathf.Pow(num, 0.25f));
			float a2 = Mathf.Lerp(1f, 0f, Mathf.Pow(num, 0.025f));
			componentsInChildren[0].material.SetColor("_Color", new Color(1f, 1f, 1f, a));
			componentsInChildren[1].material.SetColor("_Color", new Color(1f, 1f, 1f, a2));
			Quaternion localRotation = (flag ? Quaternion.Euler(0f, 180f, 0f) : Quaternion.identity);
			directionalLight.transform.localRotation = localRotation;
			Color color = (flag ? new Color(0.4f, 0.4f, 0.5f) : Color.white);
			float num2 = (flag ? 1f : 1f);
			directionalLight.color = settings.sunColor * color;
			directionalLight.intensity = settings.sunIntensity * num2;
			RenderSettings.skybox.SetFloat("_Blend1_2", settings.skyboxDayBlend);
			RenderSettings.skybox.SetFloat("_Blend1_3", settings.skyboxNightBlend);
			RenderSettings.ambientGroundColor = settings.ambientGroundColor;
			RenderSettings.ambientEquatorColor = settings.ambientEquatorColor;
			RenderSettings.ambientSkyColor = settings.ambientSkyColor;
		}

		private IEnumerator BuildLevelData(Transform parent, Level.Scene levelScene, Level.Volume levelVolume)
		{
			spawnLevelState = SpawnLevelState.InstantiateObjects;
			SeedCollectionData[] seedCollection = seedCollectionTable.GetRowValue("a8bf746f-eb45-4945-8446-e64113168ac2").seeds;
			yield return new WaitForSeconds(1f);
			foreach (KeyValuePair<Vector3Int, Level.VolumeChunk> volumeChunk in levelVolume.volumeChunks)
			{
				InstantiateVoxelChunkObject(volumeChunk.Key, volumeChunk.Value.voxelChunk, volumeChunk.Value.materialChunk, volumeChunk.Value.foliageChunk, seedCollection);
			}
			yield return new WaitForSeconds(0.3f);
			InstantiateGameObjects(LevelUtil.BuildEntityTrees(levelScene.flatEntities), parent);
			yield return new WaitForSeconds(0.3f);
			InitiateEditorObjects(m_instantiatedEntities, levelScene.flatEntities);
			m_instantiatedEntities.Clear();
			yield return new WaitForSeconds(0.3f);
			StaticBatchingUtility.Combine(base.gameObject);
			spawnLevelState = SpawnLevelState.PathfindingScan;
		}

		private GameObject InstantiateVoxelChunkObject(Vector3Int chunkPosition, Level.VoxelChunk voxelChunk, Level.MaterialChunk materialChunk, Level.FoliageChunk foliageChunk, SeedCollectionData[] seedCollection)
		{
			MeshBuilder.BuildMeshData(meshData, voxelChunk.densities, materialChunk.densities, chunkPosition);
			Mesh mesh = new Mesh();
			mesh.Clear();
			if (meshData.indices.Count == 0)
			{
				return null;
			}
			vertexArrays.CopyFrom(meshData);
			mesh.vertices = vertexArrays.positions;
			mesh.normals = vertexArrays.normals;
			mesh.uv = vertexArrays.materials;
			mesh.triangles = meshData.indices.ToArray();
			GameObject gameObject = UnityEngine.Object.Instantiate(volumeChunkPrefab, base.transform);
			gameObject.transform.localPosition = chunkPosition;
			gameObject.GetComponent<MeshFilter>().mesh = mesh;
			MeshCollider component = gameObject.GetComponent<MeshCollider>();
			if (component != null)
			{
				component.sharedMesh = null;
				component.sharedMesh = mesh;
			}
			BuildFoliageMesh(foliagePrefab, gameObject.transform, foliageChunk, chunkPosition, meshData, seedCollection);
			return gameObject;
		}

		public void BuildFoliageMesh(GameObject foliagePrefab, Transform parent, Level.FoliageChunk foliageChunk, Vector3Int chunkPosition, MeshData meshData, SeedCollectionData[] seedCollection)
		{
			if (foliageItems == null)
			{
				foliageItems = new List<FoliageData>();
				int i = 0;
				for (int num = seedCollection.Length; i < num; i++)
				{
					SeedCollectionData seedCollectionData = seedCollection[i];
					DMEditorObjectRow rowValue = editorObjectTable.GetRowValue(seedCollectionData.editorObjectId);
					if (rowValue == null || rowValue.EditorObject == null)
					{
						throw new Exception("Missing SpawnLevel object: " + seedCollectionData.editorObjectId);
					}
					GameObject editorObject = rowValue.EditorObject;
					foliageItems.Add(new FoliageData
					{
						ScaleMultiplierMinMax = seedCollectionData.scaleMultiplierMinMax,
						sharedMesh = editorObject.GetComponentInChildren<MeshFilter>().sharedMesh,
						sharedMaterial = editorObject.GetComponentInChildren<MeshRenderer>().sharedMaterial
					});
				}
			}
			Dictionary<Material, Foliage> foliages = new Dictionary<Material, Foliage>();
			FoliageBuilder.ForeachPlant(foliageChunk, chunkPosition, meshData, delegate(Vector3 bladePosition)
			{
				int plantSeedIndex = FoliageBuilder.GetPlantSeedIndex(bladePosition, foliageItems.Count);
				FoliageData foliageData = foliageItems[plantSeedIndex];
				Vector3 s = Vector3.one * FoliageBuilder.GetPlantScale(bladePosition, foliageData.ScaleMultiplierMinMax);
				Quaternion plantRotation = FoliageBuilder.GetPlantRotation(bladePosition);
				if (!foliages.TryGetValue(foliageData.sharedMaterial, out var value))
				{
					value = new Foliage
					{
						gameObject = UnityEngine.Object.Instantiate(foliagePrefab, parent)
					};
					foliages.Add(foliageData.sharedMaterial, value);
				}
				value.combineInstances.Add(new CombineInstance
				{
					mesh = foliageData.sharedMesh,
					transform = Matrix4x4.TRS(bladePosition, plantRotation, s)
				});
			});
			foreach (KeyValuePair<Material, Foliage> item in foliages)
			{
				if (item.Value.combineInstances.Count > 0)
				{
					item.Value.gameObject.GetComponent<MeshFilter>().mesh.CombineMeshes(item.Value.combineInstances.ToArray(), mergeSubMeshes: true);
					item.Value.gameObject.GetComponent<MeshRenderer>().material = item.Key;
					item.Value.gameObject.GetComponent<MeshRenderer>().enabled = true;
				}
			}
		}

		private GameObject InstantiateGameObject(string id, Vector3 position, float heightOffset, Quaternion slope, Quaternion additionalRotation, Vector3 scale, Transform parent, bool animatedSpawn)
		{
			DMEditorObjectRow rowValue = editorObjectTable.GetRowValue(id);
			if (rowValue != null && rowValue.EditorObject != null)
			{
				Quaternion localRotation = Quaternion.Lerp(Quaternion.identity, slope, rowValue.defaultSlopeAngle) * additionalRotation;
				GameObject gameObject = UnityEngine.Object.Instantiate((rowValue.GameObject != null) ? rowValue.GameObject : rowValue.EditorObject, parent);
				gameObject.transform.localPosition = position + rowValue.PivotOffset + Vector3.up * heightOffset;
				gameObject.transform.localRotation = localRotation;
				gameObject.transform.localScale = scale;
				Utility.SetLayerRecursively(gameObject, LayerMask.NameToLayer("Map"));
				RecastMeshObj componentInChildren = gameObject.GetComponentInChildren<RecastMeshObj>();
				if ((bool)componentInChildren)
				{
					UnityEngine.Object.DestroyImmediate(componentInChildren);
				}
				if (rowValue.IsEffect)
				{
					ParticleSystem[] componentsInChildren = gameObject.GetComponentsInChildren<ParticleSystem>();
					ParticleSystem[] array = componentsInChildren;
					foreach (ParticleSystem obj in array)
					{
						ParticleSystem.MainModule main = obj.main;
						main.playOnAwake = false;
						main.stopAction = ParticleSystemStopAction.None;
						obj.Stop(withChildren: true, ParticleSystemStopBehavior.StopEmittingAndClear);
					}
					Component[] componentsInChildren2 = gameObject.GetComponentsInChildren<Component>();
					foreach (Component component in componentsInChildren2)
					{
						if (component is MonoBehaviour monoBehaviour)
						{
							monoBehaviour.StopAllCoroutines();
						}
						if (!(component is Light) && !(component is Transform) && !(component is MeshFilter) && !(component is DelayEvent) && !(component is MeshRenderer) && !(component is TriggerEffect) && !(component is SphereCollider) && !(component is ParticleSystem) && !(component is PlaySoundEffect) && !(component is DMEditorComponent) && !(component is SphereRadiusChange) && !(component is ParticleSystemRenderer) && !(component is ParticleSystemForceField))
						{
							UnityEngine.Object.DestroyImmediate(component);
						}
					}
					if (componentsInChildren != null && componentsInChildren.Length != 0)
					{
						gameObject.gameObject.AddComponent<TriggerEffect>();
					}
				}
				return gameObject;
			}
			return null;
		}

		private void InstantiateGameObjects(List<EntityTreeNode> entityTrees, Transform parent)
		{
			int i = 0;
			for (int count = entityTrees.Count; i < count; i++)
			{
				EntityTreeNode entityTreeNode = entityTrees[i];
				GameObject gameObject = InstantiateGameObject(entityTreeNode.entity.objectTypeId, entityTreeNode.entity.position, entityTreeNode.entity.heightOffset, entityTreeNode.entity.slope, entityTreeNode.entity.rotation, entityTreeNode.entity.scale, parent, animatedSpawn: false);
				if (entityTreeNode.childs != null)
				{
					InstantiateGameObjects(entityTreeNode.childs, gameObject.transform);
				}
				m_instantiatedEntities.Add(entityTreeNode.entity, gameObject);
			}
		}

		public void InitiateEditorObjects(Dictionary<Level.Entity, GameObject> instantiatedEntities, List<Level.FlatEntity> entities)
		{
			List<Component> list = new List<Component>();
			foreach (KeyValuePair<Level.Entity, GameObject> instantiatedEntity in instantiatedEntities)
			{
				GameObject value = instantiatedEntity.Value;
				Level.Entity key = instantiatedEntity.Key;
				if (value == null)
				{
					continue;
				}
				list.Clear();
				value.GetComponents(list);
				foreach (Component item in list)
				{
					if (!(item is TriggerBox triggerBox))
					{
						continue;
					}
					triggerBox.m_playConnections.Clear();
					if (key.customData == null || !key.customData.TryGetValue("triggerBox", out var value2))
					{
						continue;
					}
					foreach (Guid item2 in value2.Split(',').Select(Guid.Parse).ToList())
					{
						bool flag = false;
						foreach (Level.FlatEntity entity in entities)
						{
							if (instantiatedEntities.TryGetValue(entity.entity, out var value3) && value3 != null && entity.entity.guid == item2)
							{
								flag = true;
								value3.GetComponent<ITriggerConnected>()?.OnTriggerConnected(triggerBox);
								triggerBox.m_playConnections.Add(value3);
							}
						}
						if (!flag)
						{
							Debug.LogError("Could not find id!");
						}
					}
				}
			}
		}

		private IEnumerator PathFindingScan()
		{
			spawnLevelState = SpawnLevelState.Done;
			int i = 0;
			for (int len = pathFinding.graphs.Length; i < len; i++)
			{
				IEnumerable<Progress> enumerable = pathFinding.ScanAsync(pathFinding.graphs[i]);
				foreach (Progress item in enumerable)
				{
					Debug.Log("Scan progress: " + item.progress);
					yield return null;
				}
				finishedPathfindingScan = true;
				yield return new WaitForEndOfFrame();
			}
		}
	}
}
