using System;
using System.Collections.Generic;
using DM;
using Landfall.TABS;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UnitEditorContextMenu : MonoBehaviour
{
	[SerializeField]
	private Button m_removeButton;

	[SerializeField]
	private Button m_equipButton;

	[SerializeField]
	private Button m_discardButton;

	[SerializeField]
	private Button m_closeButton;

	[SerializeField]
	private TextMeshProUGUI m_textTitle;

	public GameObject gridObject;

	[FormerlySerializedAs("contextMenuTransform")]
	[SerializeField]
	private CodeAnimation m_contextMenuAnim;

	public GameObject spacing;

	public GameObject settingsButton;

	public GameObject m_colorButton;

	public GameObject titleObject;

	public GameObject equipButton;

	[SerializeField]
	private UIColorPicker m_colorPicker;

	public ContextMenu debugContextMenu;

	private List<EditorSettingsButton> m_spawnedContextObjects = new List<EditorSettingsButton>();

	private List<ColorSettingsButton> m_colorSettingsObjects = new List<ColorSettingsButton>();

	private EquipSettingsButton m_equipSettingsButton;

	private bool m_isOpen;

	private CharacterItem m_currentProp;

	private PropItemData m_discardPropItemData;

	public bool IsOpen => m_isOpen;

	public CharacterItem CurrentProp => m_currentProp;

	private void Awake()
	{
		m_removeButton.onClick.AddListener(RemoveProp);
		m_discardButton.onClick.AddListener(DiscardSettings);
		m_closeButton.onClick.AddListener(delegate
		{
			CloseContextMenu();
		});
	}

	public void PlayAnimation(bool open)
	{
		if (open)
		{
			m_contextMenuAnim.PlayIn();
			m_contextMenuAnim.transform.SetAsLastSibling();
		}
		else
		{
			m_contextMenuAnim.PlayOut();
		}
		UnitEditorHandler.Instance.EquippedList.ShowEquipped(!open);
	}

	public void OpenContextMenu(CharacterItem prop = null)
	{
		m_isOpen = true;
		UnitEditorHandler.Instance.SetCameraPosition(prop.GearT);
		UnitEditorHandler.Instance.RemoveTemporary();
		SetCurrentHighlight(isOn: false);
		if (m_currentProp != null && !m_currentProp.IsReallyEquipped)
		{
			UnitEditorItemButton.TurnOffCurrentPropButton();
			UnitEditorHandler.Instance.RemoveEquipedProp(m_currentProp);
		}
		m_currentProp = prop;
		if (m_currentProp != null)
		{
			m_textTitle.text = m_currentProp.PropName;
			m_discardPropItemData = new PropItemData(prop.PropData);
			SetCurrentHighlight(isOn: true);
		}
		else
		{
			m_textTitle.text = "No PROp ITem";
		}
		SpawnContextMenu(debugContextMenu);
		if (m_contextMenuAnim.currentState == CodeAnimationInstance.AnimationUse.Out || m_contextMenuAnim.currentState == CodeAnimationInstance.AnimationUse.None)
		{
			PlayAnimation(open: true);
		}
		else
		{
			m_contextMenuAnim.PlayBoop();
		}
	}

	public void CloseContextMenu(bool removeCurrent = false)
	{
		m_isOpen = false;
		SetCurrentHighlight(isOn: false);
		if (m_contextMenuAnim.currentState != CodeAnimationInstance.AnimationUse.Out)
		{
			PlayAnimation(open: false);
		}
		m_currentProp = null;
		m_colorPicker.CloseColorPicker();
		UnitEditorHandler.Instance.EquippedList.UpdateVisibility();
		UnitEditorHandler.Instance.ResetCameraPosition(forceCategory: true);
	}

	private void SetCurrentHighlight(bool isOn)
	{
		if (m_currentProp != null)
		{
			m_currentProp.SetConstantHighlight(isOn);
		}
	}

	private void ClearMenu()
	{
		if (m_spawnedContextObjects.Count != 0)
		{
			for (int i = 0; i < m_spawnedContextObjects.Count; i++)
			{
				UnityEngine.Object.Destroy(m_spawnedContextObjects[i].gameObject);
			}
		}
		m_spawnedContextObjects.Clear();
		m_colorSettingsObjects.Clear();
	}

	private void SpawnContextMenu(ContextMenu context)
	{
		ClearMenu();
		if (m_currentProp.NumObjects > 1)
		{
			EquipSettingsButton component = UnityEngine.Object.Instantiate(equipButton, gridObject.transform).GetComponent<EquipSettingsButton>();
			component.SetToggles(m_currentProp.PropData.m_equip);
			component.RegisterToggleCallback(delegate(UnitRig.EquipType equipType)
			{
				m_currentProp.SetEquipType(equipType);
			});
			m_spawnedContextObjects.Add(component);
			m_equipSettingsButton = component;
		}
		CharacterItem.RendererMaterialWrapper[] sharedMaterials = m_currentProp.SharedMaterials;
		for (int num = 0; num < sharedMaterials.Length; num++)
		{
			if (sharedMaterials[num].m_material == null)
			{
				continue;
			}
			GameObject gameObject = UnityEngine.Object.Instantiate(m_colorButton, gridObject.transform);
			ColorSettingsButton colorButton = gameObject.GetComponent<ColorSettingsButton>();
			if ((bool)colorButton)
			{
				CharacterItem.RendererMaterialWrapper renderWrapper = sharedMaterials[num];
				colorButton.SetText("Color #" + (num + 1));
				colorButton.SetColor(renderWrapper.m_material.color);
				int matIndex = num;
				TeamColorPaletteData[] teamColors = ContentDatabase.Instance().GetUnitEditorColorPalette().TeamColors;
				colorButton.SetTeamColorCallback(delegate(Team team)
				{
					if (renderWrapper.m_hasTeamColor)
					{
						colorButton.SetColor(teamColors[renderWrapper.m_paletteIndex].GetColor(team));
					}
				});
				CharacterItem currentProp = m_currentProp;
				Action callback = delegate
				{
					Action<Color> newColorCallback = delegate(Color newColor)
					{
						colorButton.SetColor(newColor);
						m_colorPicker.CloseColorPicker();
					};
					m_colorPicker.OpenColorPicker(m_currentProp, matIndex, newColorCallback);
				};
				colorButton.AddButtonCallback(callback);
				colorButton.AddHoverCallback(delegate(bool enter)
				{
					if (enter)
					{
						currentProp.ResetTemporaryMaterial(matIndex);
						Material highlightMaterial = ContentDatabase.Instance().GetUnitEditorColorPalette().HighlightMaterial;
						currentProp.SetTemporaryMaterial(highlightMaterial, matIndex);
					}
					else
					{
						currentProp.ResetTemporaryMaterial(matIndex);
					}
				});
			}
			m_spawnedContextObjects.Add(colorButton);
			m_colorSettingsObjects.Add(colorButton);
		}
		MeleeWeapon component2 = m_currentProp.GetComponent<MeleeWeapon>();
		RangeWeapon component3 = m_currentProp.GetComponent<RangeWeapon>();
		if (component2 != null)
		{
			Debug.Log("Melee weapon");
			GameObject obj = UnityEngine.Object.Instantiate(settingsButton, gridObject.transform);
			EditorSettingsButton component4 = obj.GetComponent<EditorSettingsButton>();
			if ((bool)component4)
			{
				component4.SetText("Curve Force");
			}
			obj.transform.localScale = Vector3.one;
			m_spawnedContextObjects.Add(component4);
		}
		if (component3 != null)
		{
			Debug.Log("Range weapon");
			GameObject obj2 = UnityEngine.Object.Instantiate(settingsButton, gridObject.transform);
			EditorSettingsButton component5 = obj2.GetComponent<EditorSettingsButton>();
			if ((bool)component5)
			{
				component5.SetText("Cooldown");
			}
			obj2.transform.localScale = Vector3.one;
			m_spawnedContextObjects.Add(component5);
		}
		if (m_currentProp.GetComponent<CollisionWeapon>() != null)
		{
			Debug.Log("Collision weapon");
			GameObject obj3 = UnityEngine.Object.Instantiate(settingsButton, gridObject.transform);
			EditorSettingsButton component6 = obj3.GetComponent<EditorSettingsButton>();
			if ((bool)component6)
			{
				component6.SetText("Damage");
			}
			obj3.transform.localScale = Vector3.one;
			m_spawnedContextObjects.Add(component6);
		}
	}

	public void DiscardSettings()
	{
		UnitEditorColorPalette unitEditorColorPalette = ContentDatabase.Instance().GetUnitEditorColorPalette();
		m_currentProp.SetPropData(m_discardPropItemData, UnitEditorTeamButtons._CurrentTeam);
		for (int i = 0; i < m_colorSettingsObjects.Count; i++)
		{
			int num = m_discardPropItemData.m_colors[i];
			bool flag = m_discardPropItemData.m_isTeamColor[i];
			if (num < 0)
			{
				m_colorSettingsObjects[i].SetColor(m_currentProp.DefaultColors[i].m_material.color);
			}
			else if (!flag)
			{
				m_colorSettingsObjects[i].SetColor(unitEditorColorPalette.Colors[num].m_color);
			}
			else
			{
				m_colorSettingsObjects[i].SetColor(unitEditorColorPalette.TeamColors[num].GetColor(UnitEditorTeamButtons._CurrentTeam));
			}
		}
		if (m_equipSettingsButton != null)
		{
			m_equipSettingsButton.SetToggles(m_discardPropItemData.m_equip);
		}
	}

	public void RemoveProp()
	{
		UnitEditorHandler.Instance.RemoveEquipedProp(m_currentProp);
		CloseContextMenu();
	}
}
