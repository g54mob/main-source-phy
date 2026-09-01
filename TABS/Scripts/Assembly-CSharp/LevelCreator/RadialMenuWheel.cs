using System;
using System.Collections.Generic;
using System.Linq;
using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.UI;

namespace LevelCreator
{
	public class RadialMenuWheel : MonoBehaviour
	{
		public OnRadialWheelItemSelected onItemSelected = new OnRadialWheelItemSelected();

		public float selectionDeltaRequirement = 50f;

		public GameObject radialButtonPrefabInitial;

		private GameObject radialButtonPrefab;

		public GameObject radialCenterPrefabInitial;

		private GameObject radialCenterPrefab;

		public GameObject subselectionDotPrefabInitial;

		private GameObject subselectionDotPrefab;

		private List<RadialMenuSlot> radialMenuSlots = new List<RadialMenuSlot>();

		private Image centerImageComponent;

		private Text centerTextComponent;

		private int numberOfSegments;

		private int hoveredSlotIndex;

		private int lastHoveredSlotIndex;

		private List<RadialWheelSlotData> indexedSlotData = new List<RadialWheelSlotData>();

		public bool inFocus { get; set; }

		private void AssertionCheck()
		{
			radialButtonPrefab = radialButtonPrefabInitial;
			radialCenterPrefab = radialCenterPrefabInitial;
			subselectionDotPrefab = subselectionDotPrefabInitial;
		}

		private void Update()
		{
			if (inFocus && base.transform.childCount > 0)
			{
				UpdateButtonSelection();
				if (Input.mouseScrollDelta.y > 0f)
				{
					UpdateSubSelection(1);
				}
				if (Input.mouseScrollDelta.y < 0f)
				{
					UpdateSubSelection(-1);
				}
			}
		}

		private void OnDisable()
		{
			foreach (RadialWheelSlotData indexedSlotDatum in indexedSlotData)
			{
				Animator component = indexedSlotDatum.Button.GetComponent<Animator>();
				if (component != null && component.isActiveAndEnabled)
				{
					indexedSlotDatum.Button.GetComponent<Animator>().SetBool("Highlighted", value: false);
				}
			}
		}

		public void SetRadialWheelButtons(List<RadialMenuSlot> radialMenuSlots)
		{
			this.radialMenuSlots = radialMenuSlots;
			RebuildUI();
		}

		private RadialMenuItem CurrentRadialWheelItem()
		{
			return indexedSlotData[hoveredSlotIndex].RadialMenuSlot.Items[indexedSlotData[hoveredSlotIndex].SubSelectionIndex];
		}

		private RadialWheelSlotData CurrentRadialWheelSlot()
		{
			return indexedSlotData[hoveredSlotIndex];
		}

		public void InvokeButton()
		{
			if (inFocus)
			{
				onItemSelected.Invoke(CurrentRadialWheelItem().Id);
			}
		}

		public bool CheckSubselectionButtons()
		{
			if (indexedSlotData[hoveredSlotIndex].NrOfSubButtons < 2)
			{
				return false;
			}
			Button button = indexedSlotData[hoveredSlotIndex].Button;
			Button[] array = (from x in button.transform.GetComponentsInChildren<Button>()
				where x.gameObject != button.gameObject
				select x).ToArray();
			if (array[0].gameObject.activeSelf && Vector2.Distance(Input.mousePosition, array[0].transform.position) < 50f)
			{
				UpdateSubSelection(-1);
				return true;
			}
			if (array[1].gameObject.activeSelf && Vector2.Distance(Input.mousePosition, array[1].transform.position) < 50f)
			{
				UpdateSubSelection(1);
				return true;
			}
			return false;
		}

		private void RebuildUI()
		{
			AssertionCheck();
			DestroyUI();
			numberOfSegments = radialMenuSlots.Count;
			float num = 360f / (float)numberOfSegments * ((float)Math.PI / 180f);
			float num2 = num * 57.29578f / 360f;
			for (int i = 0; i < radialMenuSlots.Count; i++)
			{
				RadialMenuSlot radialMenuSlot = radialMenuSlots[i];
				Button button = UnityEngine.Object.Instantiate(radialButtonPrefab, base.transform).GetComponent<Button>();
				indexedSlotData.Insert(i, new RadialWheelSlotData
				{
					Button = button,
					RadialMenuSlot = radialMenuSlot,
					SubSelectionIndex = 0,
					NrOfSubButtons = radialMenuSlot.Items.Count
				});
				Image image = (from x in button.GetComponentsInChildren<Image>()
					where x.name == "Image"
					select x).First();
				image.color = radialMenuSlot.Items[0].Tint;
				image.sprite = radialMenuSlot.Items[0].Icon;
				if (image.sprite != null)
				{
					float num3 = num2 * 360f;
					Vector2 vector = Quaternion.AngleAxis(num3 * 0.5f - num3, Vector3.forward) * button.transform.right;
					image.transform.localScale *= num2 * 0.82f;
					image.transform.localPosition += new Vector3(vector.x, vector.y, 0f).normalized * 160f;
				}
				else
				{
					image.color = Color.clear;
				}
				Vector2 vector2 = new Vector2(Mathf.Cos(num * (float)i), Mathf.Sin(num * (float)i));
				button.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(vector2.y, vector2.x) * 57.29578f, Vector3.forward);
				button.GetComponent<Image>().fillAmount = num2 + 0.0005f;
				image.transform.Rotate(-button.transform.rotation.eulerAngles);
				Button[] array = (from x in button.transform.GetComponentsInChildren<Button>()
					where x.gameObject != button.gameObject
					select x).ToArray();
				if (radialMenuSlot.Items.Count < 2)
				{
					Array.ForEach(array, delegate(Button x)
					{
						x.gameObject.SetActive(value: false);
					});
				}
				AddSubselectionDots(button.gameObject, radialMenuSlot);
				UpdateIconHighlight();
			}
			GameObject gameObject = UnityEngine.Object.Instantiate(radialCenterPrefab, base.transform);
			gameObject.transform.localScale *= 1f - num2;
			centerImageComponent = (from x in gameObject.GetComponentsInChildren<Image>()
				where x.name == "CenterImage"
				select x).First();
			centerTextComponent = gameObject.GetComponentInChildren<Text>();
			UpdateCenterImage(isSubselectionUpdate: false, imageEnabled: false);
		}

		private void DestroyUI()
		{
			foreach (RadialWheelSlotData indexedSlotDatum in indexedSlotData)
			{
				UnityEngine.Object.DestroyImmediate(indexedSlotDatum.Button.gameObject);
			}
			indexedSlotData.Clear();
		}

		private void AddSubselectionDots(GameObject button, RadialMenuSlot slot)
		{
			int count = slot.Items.Count;
			if (count >= 2)
			{
				for (int i = 0; i < count; i++)
				{
					UnityEngine.Object.Instantiate(subselectionDotPrefab, button.transform.GetChild(0).Find("Dots")).GetComponent<Image>().color = Color.white;
				}
			}
		}

		private void UpdateButtonSelection()
		{
			Vector2 vector = default(Vector2);
			if (PlayerActions.Instance.InputType == InputType.Controller)
			{
				vector = new Vector2(PlayerActions.Instance.m_aim.X, PlayerActions.Instance.m_aim.Y);
				if (vector.magnitude / Time.deltaTime < selectionDeltaRequirement)
				{
					return;
				}
			}
			else
			{
				vector = new Vector2(Input.mousePosition.x - base.transform.position.x, Input.mousePosition.y - base.transform.position.y);
			}
			float num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
			float num2 = 360f / (float)numberOfSegments;
			num += num2;
			if (num < 0f)
			{
				num += 360f;
			}
			indexedSlotData[hoveredSlotIndex].Button.GetComponent<Animator>().SetBool("Highlighted", value: false);
			hoveredSlotIndex = Mathf.Max(Mathf.FloorToInt(num / num2), 0);
			hoveredSlotIndex = Mathf.Min(indexedSlotData.Count - 1, hoveredSlotIndex);
			RadialWheelSlotData radialWheelSlotData = indexedSlotData[hoveredSlotIndex];
			radialWheelSlotData.Button.Select();
			radialWheelSlotData.Button.GetComponent<Animator>().SetBool("Highlighted", value: true);
			UpdateCenterImage(isSubselectionUpdate: false);
			UpdateIconHighlight();
			lastHoveredSlotIndex = hoveredSlotIndex;
		}

		public void UpdateSubSelection(int subSelectionAmount)
		{
			RadialWheelSlotData radialWheelSlotData = CurrentRadialWheelSlot();
			if (radialWheelSlotData.NrOfSubButtons != 0)
			{
				radialWheelSlotData.SubSelectionIndex = Utility.PositiveModulo(radialWheelSlotData.SubSelectionIndex + subSelectionAmount, radialWheelSlotData.NrOfSubButtons);
				RadialMenuItem radialMenuItem = CurrentRadialWheelItem();
				(from x in radialWheelSlotData.Button.GetComponentsInChildren<Image>()
					where x.name == "Image"
					select x).First().sprite = radialMenuItem.Icon;
				UpdateCenterImage(isSubselectionUpdate: true);
				UpdateIconHighlight();
			}
		}

		private void UpdateCenterImage(bool isSubselectionUpdate, bool imageEnabled = true)
		{
			RadialMenuItem radialMenuItem = CurrentRadialWheelItem();
			Color tint = radialMenuItem.Tint;
			Sprite icon = radialMenuItem.Icon;
			centerImageComponent.enabled = imageEnabled;
			centerImageComponent.color = tint;
			centerImageComponent.sprite = icon;
			centerTextComponent.text = radialMenuItem.DisplayName;
			if (lastHoveredSlotIndex != hoveredSlotIndex || isSubselectionUpdate)
			{
				centerImageComponent.GetComponent<Animator>().SetTrigger("Pop");
			}
		}

		private void UpdateIconHighlight()
		{
			RadialWheelSlotData radialWheelSlotData = CurrentRadialWheelSlot();
			Transform transform = radialWheelSlotData.Button.transform.GetChild(0).Find("Dots");
			if (!(transform == null) && radialWheelSlotData.NrOfSubButtons >= 2)
			{
				for (int i = 0; i < transform.childCount; i++)
				{
					transform.GetChild(i).GetComponent<Image>().color = Color.grey;
				}
				transform.GetChild(radialWheelSlotData.SubSelectionIndex).GetComponent<Image>().color = Color.white;
			}
		}
	}
}
