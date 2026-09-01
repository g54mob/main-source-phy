using System.Collections.Generic;
using Landfall.TABS_Input;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace LevelCreator
{
	public class SeedPainterTool : Tool
	{
		[SerializeField]
		private ParticleSystem m_continousEffect;

		private Vector3 m_targetPosition;

		[SerializeField]
		private string m_defaultSeed = string.Empty;

		private static SeedCollectionData[] seeds;

		[SerializeField]
		private LayerMask m_densityLayers;

		private static float radius = 5f;

		private static float spawnRate = 0.045f;

		private static float densityCheckRange = 4f;

		private static bool isVegetationSeed;

		private bool m_paint;

		private bool m_erase;

		private float m_spawnTimer;

		private Brush m_foliageBrush;

		private Collider[] m_dummyCollidersWithSphere;

		[SerializeField]
		private Grid m_gridPrefab;

		private static Grid gridManager;

		private UnityAction<string> m_onItemSelected;

		[SerializeField]
		[BoxGroup("Sound")]
		private string m_objectSpawnSoundRef;

		[SerializeField]
		[BoxGroup("Sound")]
		private ContinousSoundData m_grassPaintSound;

		private PlayContinousSound m_grassPaintSoundObject;

		[SerializeField]
		[BoxGroup("Sound")]
		private ContinousSoundData m_eraseSound;

		private PlayContinousSound m_eraseSoundObject;

		protected override void Start()
		{
			base.Start();
			DMEditor instance = DMEditor.Instance;
			GenerateBrush();
			SetRadius(radius);
			if (seeds == null)
			{
				GetSeedInfo();
			}
			if (gridManager == null)
			{
				gridManager = Object.Instantiate(m_gridPrefab, instance.gridCanvas);
				gridManager.closeAction = PlayerActions.Instance.m_toolSpecial1;
				List<GridItem> gridItems = new List<GridItem>();
				instance.seedTable.ForEachRow(delegate(string key, SeedCollectionRow seedRow)
				{
					gridItems.Add(new GridItem
					{
						Id = key,
						Path = seedRow.Path,
						DisplayName = seedRow.GetLocalizedRowName(),
						Tooltip = "",
						Icon = seedRow.icon,
						Tint = Color.white
					});
				});
				gridManager.SetGridData(gridItems, "LC_ITEMGRID_BRUSHES");
				DMUIManager.Instance.BindPanel(gridManager, DMUIManager.UIPanels.SeedBrowser);
			}
			m_onItemSelected = delegate(string id)
			{
				GetSeedInfo(id);
			};
			gridManager.onItemSelected.AddListener(m_onItemSelected);
			DMUIManager.Instance.PopPanel();
		}

		protected override void AssignInput(PlayerActions actions)
		{
			base.AssignInput(actions);
			m_inputState.AddOnKeyDownListener(actions.m_toolPrimary, delegate
			{
				EnablePaint(enabled: true);
			}, m_contextIcons.m_primaryIcon);
			m_inputState.AddOnKeyUpListener(actions.m_toolPrimary, delegate
			{
				EnablePaint(enabled: false);
			});
			m_inputState.AddOnKeyDownListener(actions.m_toolSecondary, delegate
			{
				EnableErase(enabled: true);
			}, m_contextIcons.m_secondaryIcon);
			m_inputState.AddOnKeyUpListener(actions.m_toolSecondary, delegate
			{
				EnableErase(enabled: false);
			});
			m_inputState.AddOnKeyDownListener(actions.m_toolSpecial1, delegate
			{
				DMUIManager.Instance.OpenPanel(DMUIManager.UIPanels.SeedBrowser);
			}, m_contextIcons.m_special1Icon);
		}

		private void Update()
		{
			DMEditor instance = DMEditor.Instance;
			if (Physics.Raycast(instance.playerCamera.transform.position + instance.playerCamera.transform.forward, instance.playerCamera.transform.forward, out var hitInfo, instance.rayDistance, LayerMask.GetMask("Map")))
			{
				m_targetPosition = hitInfo.point;
			}
			else
			{
				m_targetPosition = instance.playerCamera.transform.position + instance.playerCamera.transform.forward * instance.rayDistance;
			}
			m_spawnTimer += Time.deltaTime;
			if (Physics.Raycast(instance.playerCamera.transform.position + instance.playerCamera.transform.forward, instance.playerCamera.transform.forward, out hitInfo, instance.rayDistance, LayerMask.GetMask("Map")))
			{
				if (m_paint && m_spawnTimer >= spawnRate / radius)
				{
					Paint();
				}
				else if (m_erase)
				{
					EraseSeed();
				}
			}
		}

		private void Paint()
		{
			m_continousEffect.transform.position = m_targetPosition;
			m_continousEffect.transform.rotation = Quaternion.LookRotation(Vector3.up, Vector3.right);
			if (isVegetationSeed)
			{
				DMEditor.Instance.VolumeRootObject.AddFoliage(m_targetPosition, m_foliageBrush, Volume.defaultLerpIntensity);
			}
			if (seeds.Length == 0)
			{
				return;
			}
			Vector2 insideUnitCircle = Random.insideUnitCircle;
			Vector3 bestPosition = m_targetPosition + new Vector3(insideUnitCircle.x, 0f, insideUnitCircle.y) * radius;
			if (m_dummyCollidersWithSphere == null)
			{
				m_dummyCollidersWithSphere = new Collider[3];
			}
			if (Physics.OverlapSphereNonAlloc(bestPosition, densityCheckRange, m_dummyCollidersWithSphere, m_densityLayers) > 1)
			{
				return;
			}
			m_spawnTimer = 0f;
			if (!Utility.FindBestGroundPosition(bestPosition, Vector3.up * 5f, out bestPosition) || DMEditor.Instance.IsPointUnderWater(bestPosition))
			{
				return;
			}
			int num = 0;
			float num2 = 0f;
			for (int i = 0; i < seeds.Length; i++)
			{
				float num3 = Random.Range(0, seeds[i].countMultiplier);
				if (num3 >= num2)
				{
					num2 = num3;
					num = i;
				}
			}
			string editorObjectId = seeds[num].editorObjectId;
			Vector2 scaleMultiplierMinMax = seeds[num].scaleMultiplierMinMax;
			float num4 = Random.Range(scaleMultiplierMinMax.x, scaleMultiplierMinMax.y);
			Vector3 scale = Vector3.one * num4;
			DMEditor.Instance.InstantiateEditorObject(editorObjectId, bestPosition, Quaternion.identity, Quaternion.Euler(0f, Random.Range(0f, 360f), 0f), scale, DMEditor.Instance.LevelRootObject, animatedSpawn: true);
			Utility.PlaySound(m_objectSpawnSoundRef, 1f, bestPosition);
		}

		private void EraseSeed()
		{
			if (isVegetationSeed)
			{
				DMEditor.Instance.VolumeRootObject.SubtractFoliage(m_targetPosition, m_foliageBrush, Volume.fullLerpIntensity);
			}
			foreach (DMEditorComponent item in DMEditor.Instance.GetObjectsInSphere(m_targetPosition, radius))
			{
				for (int i = 0; i < seeds.Length; i++)
				{
					if (seeds[i].editorObjectId == item.ObjectTypeId)
					{
						Object.Destroy(item.gameObject);
					}
				}
			}
		}

		private void EnablePaint(bool enabled)
		{
			m_paint = enabled;
			DMEditor.Instance.EnableSphereEmission(enabled);
			if (!enabled)
			{
				m_continousEffect.Stop();
				DMEditor.Instance.ScheduleTakeLevelSnapshot();
				if (m_grassPaintSoundObject != null)
				{
					m_grassPaintSoundObject.Stop(0f);
				}
			}
			else
			{
				m_continousEffect.Play();
				m_grassPaintSoundObject = Utility.PlayContinousSound(m_grassPaintSound, m_continousEffect.transform);
			}
		}

		private void EnableErase(bool enabled)
		{
			m_erase = enabled;
			DMEditor.Instance.EnableSphereEmission(enabled);
			if (!enabled)
			{
				DMEditor.Instance.ScheduleTakeLevelSnapshot();
				if (m_eraseSoundObject != null)
				{
					m_eraseSoundObject.Stop();
				}
			}
			else
			{
				m_eraseSoundObject = Utility.PlayContinousSound(m_eraseSound, m_continousEffect.transform);
			}
		}

		private void GetSeedInfo(string seedRowId = null)
		{
			SeedCollectionRow rowValue = DMEditor.Instance.seedTable.GetRowValue(string.IsNullOrEmpty(seedRowId) ? m_defaultSeed : seedRowId);
			seeds = rowValue.seeds;
			spawnRate = rowValue.spawnRate;
			densityCheckRange = rowValue.densityRangeCheck;
			isVegetationSeed = rowValue.isVegetationSeed;
		}

		private void GenerateBrush()
		{
			m_foliageBrush = VolumeBrushes.CreateCylinderBrush((int)radius, 2, 0f, -0.3f);
		}

		public void SetRadius(float radius)
		{
			SeedPainterTool.radius = radius;
			DMEditor.Instance.SetVisualObjectSphereRadius(radius);
			GenerateBrush();
			ParticleSystem[] componentsInChildren = m_continousEffect.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				ParticleSystem.ShapeModule shape = componentsInChildren[i].shape;
				shape.radius = radius;
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			gridManager.onItemSelected.RemoveListener(m_onItemSelected);
		}
	}
}
