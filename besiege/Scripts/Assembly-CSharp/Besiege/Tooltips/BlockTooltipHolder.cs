using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BesiegeDlc;
using Localisation;
using SRF;
using Selectors;
using UnityEngine;

namespace Besiege.Tooltips
{
	public class BlockTooltipHolder : MonoBehaviour
	{
		private const float LOW_FRICTION = 0.25f;

		private const float HIGH_FRICTION = 10f;

		private const float DEFAULT_FRICTION = 0.6f;

		private const int TITLE_WIDTH = 10;

		private const int FLIP_WIDTH_FIRST = 20;

		private const int FLIP_WIDTH_OTHERS = 25;

		[NonSerialized]
		public BlockPrefab blockPrefab;

		[NonSerialized]
		public Tooltip tooltipCode;

		[Header("")]
		public DynamicText titleSmall;

		public DynamicText titleLarge;

		[Header("")]
		public DynamicText upperDesc;

		[Header("")]
		public Transform keyHolder;

		public LocalisationChild keyUpperText;

		public KeySelectorExtender keyPrefab1;

		public KeySelectorExtender keyPrefab2;

		public LocalisationChild keyLabel1;

		public LocalisationChild keyLabel2;

		[Header("")]
		public DynamicText lowerDesc;

		[Header("")]
		public GameObject warningHolder;

		public LocalisationChild warningText;

		[Header("")]
		public AlignUI flipHolder;

		public KeySelectorExtender keyPrefabFlip;

		public DynamicText flipText;

		public DynamicText flipText2;

		[Header("")]
		public Transform statsHolder;

		public AlignUI statsSpacer;

		public Transform blockPointsIcon;

		public DynamicText blockPointsText;

		public Transform massIcon;

		public DynamicText massText;

		public SpriteRenderer buoyancyIcon;

		public GameObject buoyancyNeutral;

		public GameObject buoyancyPower;

		public GameObject buoyancyPower2;

		[Header("")]
		public Transform frictionHolder;

		public SpriteRenderer frictionIcon;

		public LocalisationChild frictionText;

		[Header("")]
		[Tooltip("Add first AlignUI in child chains that need to be updated before the rest of the tooltip. Adding anything other than the first will cause things to break.")]
		public List<AlignUI> nestedAlignUIs;

		private AlignUI firstAlignUI;

		private AlignUI lastAlignUI;

		private int[] titleLocalisations;

		public void Setup(BlockPrefab prefab)
		{
			blockPrefab = prefab;
			tooltipCode = GetComponent<Tooltip>();
			ReferenceMaster.onControlsChanged = (Action)Delegate.Combine(ReferenceMaster.onControlsChanged, new Action(OnControlsChanged));
			OnControlsChanged();
			LocalisationManager.LanguageChanged += OnLocalisationChange;
			ReferenceMaster.onAdvancedBuildingToggled = (Action)Delegate.Combine(ReferenceMaster.onAdvancedBuildingToggled, new Action(OnAdvancedBuildingToggled));
			if (blockPrefab.nameLocalisations.Length == 0)
			{
				if (blockPrefab.locID >= 0)
				{
					titleLocalisations = new int[1] { blockPrefab.locID };
				}
			}
			else if (blockPrefab.nameLocalisations.Length > 0)
			{
				titleLocalisations = blockPrefab.nameLocalisations;
			}
			if (blockPrefab.blockBehaviour is GenericDraggedBlock)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(SingleInstance<BlockTooltipController>.Instance.draggedBlockPrefab, base.transform, false) as GameObject;
				gameObject.transform.SetSiblingIndex(upperDesc.transform.GetSiblingIndex() + 1);
			}
			if (blockPrefab.keyGroup != ControlScheme.BlockControls.None)
			{
				SetOrDestroy(keyUpperText, blockPrefab.keyUpperLocalisation, keyUpperText.gameObject);
				SetOrDestroy(keyLabel1, blockPrefab.firstKeyLocalisation, keyLabel1.gameObject);
				SetOrDestroy(keyLabel2, blockPrefab.secondKeyLocalisation, keyLabel2.gameObject);
			}
			else
			{
				keyHolder.gameObject.SetActive(false);
				keyUpperText.transform.parent.gameObject.SetActive(false);
			}
			int siblingIndex = titleLarge.transform.GetSiblingIndex() + 1;
			int siblingIndex2 = statsHolder.transform.GetSiblingIndex();
			if (prefab.Type == BlockType.BuildSurface)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate(SingleInstance<BlockTooltipController>.Instance.buildSurfacePrefab, base.transform, false) as GameObject;
				gameObject2.transform.SetSiblingIndex(siblingIndex);
			}
			if (blockPrefab.EmulatesAnyKeys)
			{
				GameObject gameObject3 = UnityEngine.Object.Instantiate(SingleInstance<BlockTooltipController>.Instance.emulatePrefab, base.transform, false) as GameObject;
				gameObject3.transform.SetSiblingIndex(siblingIndex2);
			}
			SetOrDestroy(warningText, blockPrefab.warningLocalisation, warningHolder);
			bool flag = false;
			bool flag2 = false;
			bool useLeaderboards = OptionsMaster.BesiegeConfig.UseLeaderboards;
			int blockScore = WinScreen.GetBlockScore(blockPrefab.blockBehaviour);
			if (useLeaderboards && blockScore > 0)
			{
				ReferenceMaster.SetDynamicText(blockPointsText, string.Format("x{0}", blockScore));
				flag = true;
			}
			else
			{
				blockPointsIcon.gameObject.SetActive(false);
				blockPointsText.gameObject.SetActive(false);
				statsSpacer.target = massIcon.GetComponent<AlignUI>();
			}
			if (!blockPrefab.isVirtualBlock && !string.IsNullOrEmpty(blockPrefab.massOverride))
			{
				ReferenceMaster.SetDynamicText(massText, blockPrefab.massOverride);
				flag = true;
			}
			else if (!blockPrefab.isVirtualBlock && (bool)blockPrefab.blockBehaviour.Rigidbody && blockPrefab.blockBehaviour.Rigidbody.mass > 0f)
			{
				ReferenceMaster.SetDynamicText(massText, string.Format("{0:0.00}", blockPrefab.blockBehaviour.Rigidbody.mass));
				flag = true;
			}
			else
			{
				massIcon.gameObject.SetActive(false);
				massText.gameObject.SetActive(false);
			}
			bool flag3 = !blockPrefab.isVirtualBlock && DlcManager.Instance.GetDlcStatus(DlcManager.DlcType.Water) == DlcManager.DlcStatusType.Allowed;
			buoyancyIcon.gameObject.SetActive(flag3);
			buoyancyNeutral.SetActive(false);
			buoyancyPower.SetActive(false);
			buoyancyPower2.SetActive(false);
			if (flag3)
			{
				float num;
				if (blockPrefab.buoyancyOverride > 0f)
				{
					num = blockPrefab.buoyancyOverride;
				}
				else if (Machine.IsDraggedBlock(blockPrefab.Type))
				{
					num = 0f;
				}
				else
				{
					float num2 = 2.2846699f;
					if (blockPrefab.blockBehaviour.density == 0f)
					{
						blockPrefab.blockBehaviour.CalculateDensity();
					}
					num = num2 / blockPrefab.blockBehaviour.density;
				}
				if (num >= 1.05f)
				{
					buoyancyIcon.sprite = SingleInstance<BlockTooltipController>.Instance.floatSprite;
					buoyancyPower.SetActive(true);
					buoyancyPower2.SetActive(num >= 2f);
				}
				else if (num <= 0.95f)
				{
					buoyancyIcon.sprite = SingleInstance<BlockTooltipController>.Instance.sinkSprite;
					Transform obj = buoyancyPower.transform;
					Vector3 localEulerAngles = Vector3.zero.WithZ(180f);
					buoyancyPower2.transform.localEulerAngles = localEulerAngles;
					obj.localEulerAngles = localEulerAngles;
					buoyancyPower.SetActive(true);
					buoyancyPower2.SetActive(num <= 0.5f);
				}
				else
				{
					buoyancyNeutral.SetActive(true);
				}
			}
			if (!blockPrefab.isVirtualBlock)
			{
				Collider[] componentsInChildren = blockPrefab.blockBehaviour.GetComponentsInChildren<Collider>();
				foreach (Collider collider in componentsInChildren)
				{
					if ((bool)collider && (bool)collider.sharedMaterial)
					{
						PhysicMaterial sharedMaterial = collider.sharedMaterial;
						if (((sharedMaterial.frictionCombine == PhysicMaterialCombine.Average || sharedMaterial.frictionCombine == PhysicMaterialCombine.Minimum) && sharedMaterial.dynamicFriction <= 0.25f) || (sharedMaterial.frictionCombine == PhysicMaterialCombine.Multiply && sharedMaterial.dynamicFriction * 0.6f <= 0.25f))
						{
							frictionIcon.sprite = SingleInstance<BlockTooltipController>.Instance.lowFrictionSprite;
							frictionText.translationID = 777;
							flag2 = true;
							break;
						}
						if (((sharedMaterial.frictionCombine == PhysicMaterialCombine.Average || sharedMaterial.frictionCombine == PhysicMaterialCombine.Maximum) && sharedMaterial.dynamicFriction >= 10f) || (sharedMaterial.frictionCombine == PhysicMaterialCombine.Multiply && sharedMaterial.dynamicFriction * 0.6f >= 10f))
						{
							frictionIcon.sprite = SingleInstance<BlockTooltipController>.Instance.highFrictionSprite;
							frictionText.translationID = 583;
							flag2 = true;
							break;
						}
					}
				}
			}
			if (!flag)
			{
				UnityEngine.Object.DestroyImmediate(statsHolder.gameObject);
			}
			if (!flag2)
			{
				UnityEngine.Object.DestroyImmediate(frictionHolder.gameObject);
			}
			UpdateMultiLocalisations();
		}

		private void OnEnable()
		{
			StartCoroutine(IEOnEnable());
		}

		private IEnumerator IEOnEnable()
		{
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
			OnAdvancedBuildingToggled();
		}

		private void OnDestroy()
		{
			ReferenceMaster.onControlsChanged = (Action)Delegate.Remove(ReferenceMaster.onControlsChanged, new Action(OnControlsChanged));
		}

		[ContextMenu("Construct Layout")]
		public void ConstructLayout()
		{
			foreach (Transform item in base.transform.GetChildren().Reverse())
			{
				if (!item || !item.gameObject.activeSelf)
				{
					continue;
				}
				AlignUI component = item.GetComponent<AlignUI>();
				if (!component)
				{
					continue;
				}
				if (!lastAlignUI)
				{
					firstAlignUI = (lastAlignUI = component);
					continue;
				}
				component.ChangeTarget(lastAlignUI);
				component.boundingContent = component.boundingContent.Where((Transform x) => (bool)x && x.gameObject.activeSelf).ToArray();
				lastAlignUI = component;
			}
			UpdateLayout();
			base.gameObject.SetActive(true);
			tooltipCode.enabled = true;
		}

		public void UpdateLayout()
		{
			foreach (AlignUI nestedAlignUI in nestedAlignUIs)
			{
				if ((bool)nestedAlignUI)
				{
					nestedAlignUI.Align();
				}
			}
			firstAlignUI.Align();
		}

		private void OnAdvancedBuildingToggled()
		{
			if ((bool)statsHolder)
			{
				statsHolder.gameObject.SetActive(OptionsMaster.BesiegeConfig.AdvancedBuilding);
			}
			if ((bool)frictionHolder)
			{
				frictionHolder.gameObject.SetActive(OptionsMaster.BesiegeConfig.AdvancedBuilding);
			}
			ConstructLayout();
		}

		private void UpdateMultiLocalisations()
		{
			if ((bool)upperDesc)
			{
				SetOrDestroy(upperDesc, blockPrefab.upperDescLocalisations, upperDesc.gameObject);
			}
			if ((bool)lowerDesc)
			{
				SetOrDestroy(lowerDesc, blockPrefab.lowerDescLocalisations, lowerDesc.gameObject);
			}
			UpdateTitle();
			UpdateFlip();
		}

		private void OnControlsChanged()
		{
			if (blockPrefab.keyGroup != ControlScheme.BlockControls.None)
			{
				ControlScheme.ControlOption[] options = InputManager.Scheme.Blocks[(int)blockPrefab.keyGroup].Options;
				KeyCode key = options[0].Keys[0];
				keyPrefab1.SetUp(null, null, 0, key);
				if (options.Length > 1)
				{
					KeyCode key2 = options[1].Keys[0];
					keyPrefab2.SetUp(null, null, 0, key2);
				}
				else
				{
					keyPrefab2.gameObject.SetActive(false);
				}
			}
			if (blockPrefab.flipLocalisations.Length > 0)
			{
				ControlScheme.ControlOption[] options2 = InputManager.Scheme.Building[1].Options;
				keyPrefabFlip.SetUp(null, null, 0, options2[0].Keys[0]);
			}
		}

		private void OnLocalisationChange()
		{
			LocalisationChild[] componentsInChildren = GetComponentsInChildren<LocalisationChild>();
			foreach (LocalisationChild localisationChild in componentsInChildren)
			{
				localisationChild.Recaption();
				DynamicText component = localisationChild.GetComponent<DynamicText>();
				if ((bool)component)
				{
					component.GenerateMesh();
				}
			}
			UpdateMultiLocalisations();
			if (base.gameObject.activeSelf)
			{
				UpdateLayout();
			}
		}

		private void SetOrDestroy(LocalisationChild locChild, int localisation, GameObject toDestroy)
		{
			if (localisation <= 0)
			{
				UnityEngine.Object.DestroyImmediate(toDestroy);
				return;
			}
			locChild.translationID = localisation;
			locChild.Recaption();
			DynamicText component = locChild.GetComponent<DynamicText>();
			if ((bool)component)
			{
				component.GenerateMesh();
			}
		}

		private void SetOrDestroy(DynamicText locChild, int[] localisations, GameObject toDestroy)
		{
			if (localisations.Length == 0)
			{
				UnityEngine.Object.DestroyImmediate(toDestroy);
				return;
			}
			List<string> localisations2 = LocalisationManager.GetLocalisations(localisations);
			ReferenceMaster.SetDynamicText(locChild, string.Join("\n", localisations2.ToArray()));
			locChild.GenerateMesh();
		}

		private void UpdateTitle()
		{
			if (titleLocalisations == null || titleLocalisations.Length == 0)
			{
				titleLarge.gameObject.SetActive(false);
				titleSmall.gameObject.SetActive(false);
				return;
			}
			List<string> list;
			if (titleLocalisations.Length == 1)
			{
				string text = LocalisationManager.GetTranslation(titleLocalisations[0]).ToUpper();
				list = ((!text.Contains('\n')) ? text.WordWrap(' ', 10).ToList() : text.Split('\n').ToList());
			}
			else
			{
				list = LocalisationManager.GetLocalisations(titleLocalisations);
			}
			ReferenceMaster.SetDynamicText(titleLarge, list.Last());
			titleLarge.GenerateMesh();
			if (list.Count > 1)
			{
				list.PopLast();
				ReferenceMaster.SetDynamicText(titleSmall, string.Join("\n", list.ToArray()));
			}
			else
			{
				ReferenceMaster.SetDynamicText(titleSmall, string.Empty);
			}
			titleSmall.GenerateMesh();
		}

		private void UpdateFlip()
		{
			if (blockPrefab.flipLocalisations.Length == 0)
			{
				if ((bool)flipHolder)
				{
					UnityEngine.Object.DestroyImmediate(flipHolder.gameObject);
				}
				return;
			}
			List<string> localisations = LocalisationManager.GetLocalisations(blockPrefab.flipLocalisations);
			string[] array = ((!localisations.Any((string x) => x.Contains(" "))) ? localisations.ToArray() : string.Join(" ", localisations.Select((string x) => x.Trim()).ToArray()).WordWrap(' ', 20).ToArray());
			ReferenceMaster.SetDynamicText(flipText, array.First());
			flipText.GenerateMesh();
			if (array.Length > 1)
			{
				array = string.Join(" ", array.Skip(1).ToArray()).WordWrap(' ', 25).ToArray();
				ReferenceMaster.SetDynamicText(flipText2, string.Join("\n", array));
				if (!flipHolder.boundingContent.Contains(flipText2.transform))
				{
					List<Transform> list = flipHolder.boundingContent.ToList();
					list.Add(flipText2.transform);
					flipHolder.boundingContent = list.ToArray();
				}
			}
			else
			{
				ReferenceMaster.SetDynamicText(flipText2, string.Empty);
				if (flipHolder.boundingContent.Contains(flipText2.transform))
				{
					List<Transform> list2 = flipHolder.boundingContent.ToList();
					list2.Remove(flipText2.transform);
					flipHolder.boundingContent = list2.ToArray();
				}
			}
			flipText2.GenerateMesh();
		}
	}
}
