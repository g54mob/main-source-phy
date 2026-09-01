using System.Collections.Generic;
using System.Linq;
using Landfall.TABS;
using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LevelCreator
{
	public class RadialMenu : MonoBehaviour
	{
		public enum RadialThemes
		{
			Default = 0,
			Hidden = 1,
			Experimental_Tools = 2,
			Tools = 3,
			Props = 4,
			Foliage = 5,
			Landscape = 6,
			Architecture = 7,
			Buildings = 8,
			Visual_Effects = 9
		}

		public OnRadialMenuItemSelected onItemSelected = new OnRadialMenuItemSelected();

		public GameObject radialWheelMenuPrefabInitial;

		private GameObject radialWheelMenuPrefab;

		public GameObject centerArrowsInitial;

		private GameObject centerArrows;

		public Transform radialMenuIconContentPrefabInitial;

		private Transform radialMenuIconContentPrefab;

		public Transform radialMenuIconContentInitial;

		private Transform radialMenuIconContent;

		public GameObject radialMenuIconPrefabInitial;

		private GameObject radialMenuIconPrefab;

		public GameObject leftThemeArrowInitial;

		private GameObject leftThemeArrow;

		public GameObject rightThemeArrowInitial;

		private GameObject rightThemeArrow;

		public Transform radialMenuThemeContentInitial;

		private Transform radialMenuThemeContent;

		public GameObject radialMenuThemePrefabInitial;

		private GameObject radialMenuThemePrefab;

		public float lerpSpeed = 1.5f;

		public float radialMenuDistance = 60f;

		public float radialMenuScale = 0.7f;

		[Range(0f, 1f)]
		public float radialMenuMinSizePrct = 0.6f;

		private int themeIndex;

		private bool themeCycleLock;

		private float lerpTimer;

		private Dictionary<string, RadialMenuTheme> radialMenuThemes = new Dictionary<string, RadialMenuTheme>();

		private List<RadialMenuThemeData> radialMenuThemesData = new List<RadialMenuThemeData>();

		private InputState currentInputState;

		private void AssertionCheck()
		{
			radialWheelMenuPrefab = radialWheelMenuPrefabInitial;
			centerArrows = centerArrowsInitial;
			radialMenuIconContentPrefab = radialMenuIconContentPrefabInitial;
			radialMenuIconContent = radialMenuIconContentInitial;
			radialMenuIconPrefab = radialMenuIconPrefabInitial;
			leftThemeArrow = leftThemeArrowInitial;
			rightThemeArrow = rightThemeArrowInitial;
			radialMenuThemeContent = radialMenuThemeContentInitial;
			radialMenuThemePrefab = radialMenuThemePrefabInitial;
		}

		private void Update()
		{
			LerpRadialMenues();
		}

		public void EnableRadialMenu(InputState inputState = null)
		{
			base.gameObject.SetActive(value: true);
			SetInputState(inputState);
		}

		public void DisableRadialMenu()
		{
			base.gameObject.SetActive(value: false);
			SetInputState(null);
		}

		public void SetInputState(InputState inputState)
		{
			if (currentInputState != null)
			{
				InputManager.RemoveState(currentInputState);
			}
			currentInputState = inputState;
			if (currentInputState == null)
			{
				return;
			}
			UnityAction action = delegate
			{
				if (!CheckCyclingArrows() && !CheckThemeButtons() && !CurrentRadialMenuWheel().CheckSubselectionButtons())
				{
					InvokeButton();
					DisableRadialMenu();
				}
			};
			PlayerActions instance = PlayerActions.Instance;
			inputState.RemoveOnKeyDownListener(instance.m_menuSelect, action);
			inputState.AddOnKeyDownListener(instance.m_menuSelect, action);
			AddCycleActionsToInputState(currentInputState, instance);
		}

		public void SetThemeCyclingEnabled(bool enabled)
		{
			themeCycleLock = !enabled;
		}

		private void AddCycleActionsToInputState(InputState inputState, PlayerActions actions)
		{
			UnityAction action = delegate
			{
				CycleRadialMenu(right: false);
			};
			UnityAction action2 = delegate
			{
				CycleRadialMenu(right: true);
			};
			UnityAction action3 = delegate
			{
				CycleTheme(right: false);
			};
			UnityAction action4 = delegate
			{
				CycleTheme(right: true);
			};
			UnityAction action5 = delegate
			{
				CycleSubSelection(right: false);
			};
			UnityAction action6 = delegate
			{
				CycleSubSelection(right: true);
			};
			inputState.RemoveOnKeyDownListener(actions.m_radialCycleLeft, action);
			inputState.AddOnKeyDownListener(actions.m_radialCycleLeft, action);
			inputState.RemoveOnKeyDownListener(actions.m_radialCycleRight, action2);
			inputState.AddOnKeyDownListener(actions.m_radialCycleRight, action2);
			inputState.RemoveOnKeyDownListener(actions.m_radialCycleThemeLeft, action3);
			inputState.AddOnKeyDownListener(actions.m_radialCycleThemeLeft, action3);
			inputState.RemoveOnKeyDownListener(actions.m_radialCycleThemeRight, action4);
			inputState.AddOnKeyDownListener(actions.m_radialCycleThemeRight, action4);
			inputState.RemoveOnKeyDownListener(actions.m_radialCycleSubSelectionLeft, action5);
			inputState.AddOnKeyDownListener(actions.m_radialCycleSubSelectionLeft, action5);
			inputState.RemoveOnKeyDownListener(actions.m_radialCycleSubSelectionRight, action6);
			inputState.AddOnKeyDownListener(actions.m_radialCycleSubSelectionRight, action6);
		}

		private void InvokeButton()
		{
			CurrentRadialMenuWheel().InvokeButton();
		}

		private bool CheckCyclingArrows()
		{
			if (themeCycleLock)
			{
				return false;
			}
			if (Vector2.Distance(radialMenuThemeContent.transform.GetChild(0).transform.position, Input.mousePosition) < 40f)
			{
				CycleTheme(right: false);
				return true;
			}
			if (Vector2.Distance(radialMenuThemeContent.transform.GetChild(radialMenuThemeContent.transform.childCount - 1).transform.position, Input.mousePosition) < 40f)
			{
				CycleTheme(right: true);
				return true;
			}
			if (Input.mousePosition.x < centerArrows.transform.GetChild(0).transform.position.x + 30f && Input.mousePosition.y < 1300f)
			{
				CycleRadialMenu(right: false);
				return true;
			}
			if (Input.mousePosition.x > centerArrows.transform.GetChild(1).transform.position.x - 30f && Input.mousePosition.y < 1300f)
			{
				CycleRadialMenu(right: true);
				return true;
			}
			return false;
		}

		private bool CheckThemeButtons()
		{
			if (Input.mousePosition.y > radialMenuThemeContent.transform.position.y - 30f)
			{
				return true;
			}
			return false;
		}

		private void CycleTheme(bool right)
		{
			if (!themeCycleLock)
			{
				themeIndex += (right ? 1 : (-1));
				themeIndex = Utility.PositiveModulo(themeIndex, radialMenuThemesData.Count);
				RefreshUI();
			}
		}

		private void SetTheme(int index)
		{
			if (!themeCycleLock)
			{
				themeIndex = index;
				themeIndex = Mathf.Clamp(themeIndex - 1, 0, radialMenuThemesData.Count);
				RefreshUI();
			}
		}

		private RadialMenuThemeData CurrentRadialMenuThemeData()
		{
			return radialMenuThemesData[themeIndex];
		}

		private RadialMenuWheel CurrentRadialMenuWheel()
		{
			return radialMenuThemesData[themeIndex].RadialMenuWheels[radialMenuThemesData[themeIndex].SelectedRadialWheelIndex];
		}

		public void SetRadialMenuData(List<RadialMenuItem> radialMenuItems)
		{
			radialMenuThemes = GenerateRadialThemes(radialMenuItems);
			RebuildUI();
		}

		private void RebuildUI()
		{
			AssertionCheck();
			DestroyUI();
			int num = 0;
			foreach (KeyValuePair<string, RadialMenuTheme> radialMenuTheme in radialMenuThemes)
			{
				RadialMenuThemeData radialMenuThemeData = new RadialMenuThemeData
				{
					Index = num,
					SelectedRadialWheelIndex = 0,
					Theme = radialMenuTheme.Value,
					RadialMenuWheels = new List<RadialMenuWheel>(),
					iconTransform = Object.Instantiate(radialMenuIconContentPrefab, radialMenuIconContent)
				};
				GameObject themeObject = Object.Instantiate(radialMenuThemePrefab, radialMenuThemeContent);
				themeObject.GetComponentInChildren<Text>().text = radialMenuThemeData.Theme.ThemeName;
				themeObject.GetComponent<Button>().onClick.AddListener(delegate
				{
					SetTheme(themeObject.transform.GetSiblingIndex());
				});
				foreach (KeyValuePair<string, RadialMenuCategory> category in radialMenuTheme.Value.Categories)
				{
					RadialMenuWheel component = Object.Instantiate(radialWheelMenuPrefab, base.transform).GetComponent<RadialMenuWheel>();
					component.SetRadialWheelButtons(category.Value.Slots.Values.SelectMany((List<RadialMenuSlot> x) => x).ToList());
					radialMenuThemeData.RadialMenuWheels.Add(component);
					component.onItemSelected.AddListener(delegate(string id)
					{
						onItemSelected.Invoke(id);
					});
					if (radialMenuTheme.Value.Categories.Count > 1)
					{
						Object.Instantiate(radialMenuThemePrefab, radialMenuThemeData.iconTransform).GetComponentInChildren<Text>().text = category.Value.CategoryName;
					}
				}
				radialMenuThemesData.Add(radialMenuThemeData);
				num++;
			}
			if (radialMenuThemes.Count > 1)
			{
				Object.Instantiate(leftThemeArrow, radialMenuThemeContent).transform.SetSiblingIndex(0);
				Object.Instantiate(rightThemeArrow, radialMenuThemeContent).transform.SetSiblingIndex(radialMenuThemes.Count + 1);
			}
			RefreshUI();
		}

		private void RefreshUI()
		{
			UpdateRadialWheelsVisibility();
			UpdateIconHighlight();
			CurrentRadialMenuWheel().inFocus = true;
			TranslateRadialMenues(instant: true);
			if (CurrentRadialMenuThemeData().RadialMenuWheels.Count > 1)
			{
				centerArrows.gameObject.SetActive(value: true);
			}
			else
			{
				centerArrows.gameObject.SetActive(value: false);
			}
			UpdateThemeHighlight();
		}

		private void DestroyUI()
		{
			foreach (RadialMenuThemeData radialMenuThemesDatum in radialMenuThemesData)
			{
				Object.DestroyImmediate(radialMenuThemeContent.GetChild(0).gameObject);
				foreach (RadialMenuWheel radialMenuWheel in radialMenuThemesDatum.RadialMenuWheels)
				{
					if (radialMenuIconContent.childCount > 0)
					{
						Object.DestroyImmediate(radialMenuIconContent.GetChild(0).gameObject);
					}
					Object.Destroy(radialMenuWheel.gameObject);
				}
			}
			radialMenuThemesData.Clear();
		}

		private void UpdateRadialWheelsVisibility()
		{
			foreach (RadialMenuThemeData radialMenuThemesDatum in radialMenuThemesData)
			{
				radialMenuThemesDatum.iconTransform.gameObject.SetActive(radialMenuThemesDatum.Index == themeIndex);
				foreach (RadialMenuWheel radialMenuWheel in radialMenuThemesDatum.RadialMenuWheels)
				{
					radialMenuWheel.gameObject.SetActive(radialMenuThemesDatum.Index == themeIndex);
				}
			}
		}

		private void CycleRadialMenu(bool right)
		{
			CurrentRadialMenuWheel().inFocus = false;
			CurrentRadialMenuThemeData().SelectedRadialWheelIndex += (right ? 1 : (-1));
			CurrentRadialMenuThemeData().SelectedRadialWheelIndex = Utility.PositiveModulo(CurrentRadialMenuThemeData().SelectedRadialWheelIndex, CurrentRadialMenuThemeData().RadialMenuWheels.Count);
			CurrentRadialMenuWheel().inFocus = true;
			lerpTimer = 0f;
			UpdateIconHighlight();
		}

		private void LerpRadialMenues()
		{
			lerpTimer += Time.deltaTime * lerpSpeed;
			lerpTimer = Mathf.Clamp01(lerpTimer);
			foreach (RadialMenuWheel radialMenuWheel in CurrentRadialMenuThemeData().RadialMenuWheels)
			{
				radialMenuWheel.transform.localScale = Vector3.Lerp(Vector3.one * radialMenuScale, Vector3.one * radialMenuScale * radialMenuMinSizePrct, Mathf.Abs(radialMenuWheel.transform.localPosition.x) / radialMenuDistance);
				float alpha = Mathf.Max(1f - Mathf.Clamp01(Mathf.Abs(radialMenuWheel.transform.localPosition.x) / radialMenuDistance * 1.5f), 0.5f);
				radialMenuWheel.GetComponent<CanvasGroup>().alpha = alpha;
			}
			TranslateRadialMenues(instant: false);
		}

		private void TranslateRadialMenues(bool instant)
		{
			if (instant)
			{
				lerpTimer = 1f;
			}
			int num = 0;
			foreach (RadialMenuWheel radialMenuWheel in CurrentRadialMenuThemeData().RadialMenuWheels)
			{
				float x = radialMenuDistance * (float)(num - CurrentRadialMenuThemeData().SelectedRadialWheelIndex);
				radialMenuWheel.transform.localPosition = Vector3.Lerp(radialMenuWheel.transform.localPosition, new Vector3(x, radialMenuWheel.transform.localPosition.y), lerpTimer);
				num++;
			}
		}

		private void UpdateIconHighlight()
		{
			if (radialMenuIconContent.childCount != 0)
			{
				Transform child = radialMenuIconContent.GetChild(themeIndex);
				for (int i = 0; i < child.childCount; i++)
				{
					child.GetChild(i).GetComponentInChildren<Image>().color = UIStyleManager.GetStyle().m_BackgroundColor - Color.white * 0.3f;
				}
				if (child.childCount > 0)
				{
					child.GetChild(CurrentRadialMenuThemeData().SelectedRadialWheelIndex).GetComponentInChildren<Image>().color = UIStyleManager.GetStyle().m_HighlightedColor;
				}
			}
		}

		private void UpdateThemeHighlight()
		{
			for (int i = 0; i < radialMenuThemeContent.childCount; i++)
			{
				radialMenuThemeContent.GetChild(i).GetComponent<Image>().color = UIStyleManager.GetStyle().m_BackgroundColor - Color.white * 0.3f;
			}
			int index = Mathf.Clamp(themeIndex + 1, 0, radialMenuThemeContent.childCount - 1);
			Image component = radialMenuThemeContent.GetChild(index).GetComponent<Image>();
			if (component != null)
			{
				component.color = UIStyleManager.GetStyle().m_HighlightedColor;
			}
		}

		private void CycleSubSelection(bool right)
		{
			CurrentRadialMenuWheel().UpdateSubSelection(right ? 1 : (-1));
		}

		private Dictionary<string, RadialMenuTheme> GenerateRadialThemes(List<RadialMenuItem> radialMenuItems)
		{
			Dictionary<string, RadialMenuTheme> dictionary = new Dictionary<string, RadialMenuTheme>();
			foreach (RadialMenuItem radialMenuItem in radialMenuItems)
			{
				string[] array = radialMenuItem.Path.TrimStart('/').TrimEnd('/').Replace("_", " ")
					.Split('/');
				if (array[0] == RadialThemes.Hidden.ToString())
				{
					continue;
				}
				if (array.Length != 3)
				{
					Debug.LogError("Invalid radial path: " + radialMenuItem.Path + "in object: " + radialMenuItem.Id);
					continue;
				}
				string text = array[0];
				dictionary.TryGetValue(text, out var value);
				if (value == null)
				{
					value = new RadialMenuTheme
					{
						ThemeName = text
					};
					dictionary.Add(text, value);
				}
				string text2 = array[1];
				value.Categories.TryGetValue(text2, out var value2);
				if (value2 == null)
				{
					value2 = new RadialMenuCategory
					{
						CategoryName = text2
					};
					value.Categories.Add(text2, value2);
				}
				string radialSlotKey = array[2];
				value2.Slots.TryGetValue(radialSlotKey, out var value3);
				if (value3 == null)
				{
					value3 = new List<RadialMenuSlot>();
					value2.Slots.Add(radialSlotKey, value3);
				}
				RadialMenuSlot radialMenuSlot = value3.Find((RadialMenuSlot x) => x.SlotName == radialSlotKey);
				if (radialMenuSlot != null)
				{
					radialMenuSlot.Items.Add(radialMenuItem);
					continue;
				}
				value3.Add(new RadialMenuSlot
				{
					SlotName = radialSlotKey,
					Items = new List<RadialMenuItem> { radialMenuItem }
				});
			}
			return dictionary;
		}
	}
}
