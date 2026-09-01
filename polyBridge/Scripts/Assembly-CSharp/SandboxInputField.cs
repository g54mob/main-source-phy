using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SandboxInputField : MonoBehaviour
{
	public delegate void AddDeltaDelegate(GameObject go, float delta);

	public delegate void SetDelegate(GameObject go, float value);

	public delegate void RestoreDelegate(GameObject go);

	public SandboxInputFieldFormat m_Format;

	public TMP_InputField m_InputField;

	public Button m_GamepadInputFieldButton;

	public SandboxTapeSlider m_LinkedSlider;

	public Button m_ButtonIncrease;

	public Button m_ButtonDecrease;

	[NonSerialized]
	public bool m_ExternalContinuousHoldActive;

	private AddDeltaDelegate m_AddCallback;

	private SetDelegate m_SetCallback;

	private RestoreDelegate m_RestoreCallback;

	private float m_ContinuousHoldTime;

	private float m_NextTickTime;

	private Button m_ContinuousHoldButton;

	private bool m_ContinuousHoldActive;

	private bool m_DoSnapShotWhenContinuousHoldOff;

	private bool m_IgnoreNextIncreaseOrDecrease;

	private static int CHARACTER_LIMIT = 12;

	private static float MAX_TIME_DELAY = 300f;

	private void Start()
	{
		SetCallbacks(m_Format);
		SetInputType(m_Format);
		SandboxInputFields.m_InputFields.Add(this);
		m_InputField.onEndEdit.AddListener(delegate
		{
			OnEndEdit();
		});
		m_ButtonIncrease.onClick.AddListener(delegate
		{
			OnIncrease();
		});
		m_ButtonDecrease.onClick.AddListener(delegate
		{
			OnDecrease();
		});
		m_GamepadInputFieldButton.onClick.AddListener(OnGamepadInputField);
		m_InputField.characterLimit = CHARACTER_LIMIT;
	}

	private void OnDestroy()
	{
		if (SandboxInputFields.m_InputFields.Contains(this))
		{
			SandboxInputFields.m_InputFields.Remove(this);
		}
	}

	private void Update()
	{
		if (GameInput.GetMouseButtonJustReleased(0))
		{
			m_IgnoreNextIncreaseOrDecrease = false;
		}
		UpdateContinuousHold();
	}

	private void OnEnable()
	{
		m_IgnoreNextIncreaseOrDecrease = GameInput.GetMouseButtonIsDown(0);
		UpdateForCurrentDevice();
	}

	private void OnDisable()
	{
		m_ContinuousHoldActive = false;
	}

	public void UpdateForCurrentDevice()
	{
		m_InputField.interactable = !GamepadVirtualKeyboard.IsSupported();
		m_GamepadInputFieldButton.gameObject.SetActive(GamepadVirtualKeyboard.IsSupported());
		m_ContinuousHoldActive = false;
	}

	public void ProcessInputForRotation()
	{
		if (GameInput.JustReleased(BindingType.ROTATE_CLIPBOARD_LEFT))
		{
			OnDecrease();
		}
		if (GameInput.JustReleased(BindingType.ROTATE_CLIPBOARD_RIGHT))
		{
			OnIncrease();
		}
		if (GameInput.IsDown(BindingType.ROTATE_CLIPBOARD_LEFT))
		{
			OnDecreaseContinuous();
			m_ExternalContinuousHoldActive = true;
		}
		else if (GameInput.IsDown(BindingType.ROTATE_CLIPBOARD_RIGHT))
		{
			OnIncreaseContinuous();
			m_ExternalContinuousHoldActive = true;
		}
		else if (m_ExternalContinuousHoldActive)
		{
			ContinuousHoldReset();
			m_ExternalContinuousHoldActive = false;
			SandboxUndo.SnapShot();
		}
	}

	public void ContinuousHoldReset()
	{
		m_ContinuousHoldTime = 0f;
	}

	public void ChangeToFormat(SandboxInputFieldFormat format)
	{
		m_Format = format;
		SetCallbacks(m_Format);
	}

	public void AddHeight(GameObject go, float deltaHeight)
	{
		if ((bool)go)
		{
			switch (go.GetComponent<SandboxItem>().m_Type)
			{
			case SandboxItemType.PLATFORM:
				SetHeight(go, GameGrid.RoundToNearestGridSquare(go.GetComponent<Platform>().m_Height + deltaHeight));
				break;
			case SandboxItemType.RAMP:
				SetHeight(go, GameGrid.RoundToNearestGridSquare(go.GetComponent<Ramp>().m_Height + deltaHeight));
				break;
			case SandboxItemType.VEHICLE_STOP_TRIGGER:
				SetHeight(go, GameGrid.RoundToNearestGridSquare(go.GetComponent<VehicleStopTrigger>().m_Height + deltaHeight));
				break;
			case SandboxItemType.WATER:
				SetHeight(go, go.GetComponent<WaterBlock>().m_Height + deltaHeight);
				break;
			case SandboxItemType.PILLAR:
				SetHeight(go, GameGrid.RoundToNearestGridSquare(go.GetComponent<Pillar>().m_Height + deltaHeight));
				break;
			case SandboxItemType.BUILD_ZONE:
				SetHeight(go, GameGrid.RoundToNearestGridSquare(go.GetComponent<BuildZone>().GetSize().y + deltaHeight));
				break;
			case SandboxItemType.TERRAIN:
				SetHeight(go, GameGrid.RoundToNearestGridSquare(go.GetComponent<TerrainIsland>().GetHeight() + deltaHeight));
				break;
			}
		}
	}

	public void SetHeight(GameObject go, float height)
	{
		if ((bool)go)
		{
			switch (go.GetComponent<SandboxItem>().m_Type)
			{
			case SandboxItemType.PLATFORM:
				SetPlatformHeight(go.GetComponent<Platform>(), height);
				break;
			case SandboxItemType.RAMP:
				SetRampHeight(go.GetComponent<Ramp>(), height);
				break;
			case SandboxItemType.VEHICLE_STOP_TRIGGER:
				SetVehicleStopTriggerHeight(go.GetComponent<VehicleStopTrigger>(), height);
				break;
			case SandboxItemType.WATER:
				SetWaterBlockHeight(go.GetComponent<WaterBlock>(), height);
				break;
			case SandboxItemType.PILLAR:
				SetPillarHeight(go.GetComponent<Pillar>(), height);
				break;
			case SandboxItemType.BUILD_ZONE:
				SetBuildZoneHeight(go.GetComponent<BuildZone>(), height);
				break;
			case SandboxItemType.TERRAIN:
				SetTerrainIslandHeight(go.GetComponent<TerrainIsland>(), height);
				break;
			}
			MarkOutlineDirty(go);
		}
	}

	public void RestoreHeight(GameObject go)
	{
		if ((bool)go)
		{
			switch (go.GetComponent<SandboxItem>().m_Type)
			{
			case SandboxItemType.PLATFORM:
				m_InputField.text = Utils.FormatDistance(go.GetComponent<Platform>().m_Height);
				break;
			case SandboxItemType.RAMP:
				m_InputField.text = Utils.FormatDistance(go.GetComponent<Ramp>().m_Height);
				break;
			case SandboxItemType.VEHICLE_STOP_TRIGGER:
				m_InputField.text = Utils.FormatDistance(go.GetComponent<VehicleStopTrigger>().m_Height);
				break;
			case SandboxItemType.WATER:
				m_InputField.text = Utils.FormatDistance(go.GetComponent<WaterBlock>().m_Height);
				break;
			case SandboxItemType.PILLAR:
				m_InputField.text = Utils.FormatDistance(go.GetComponent<Pillar>().m_Height);
				break;
			case SandboxItemType.BUILD_ZONE:
				m_InputField.text = Utils.FormatDistance(go.GetComponent<BuildZone>().GetSize().y);
				break;
			case SandboxItemType.TERRAIN:
				go.GetComponent<TerrainIsland>().GetHeight();
				m_InputField.text = Utils.FormatDistance(go.GetComponent<TerrainIsland>().GetHeight());
				break;
			}
		}
	}

	public void AddHeightFogStartMin(GameObject go, float deltaHeight)
	{
		SetHeightFogStartMin(go, SandboxSettings.m_FogHeightMinWorldY + deltaHeight);
	}

	public void SetHeightFogStartMin(GameObject go, float height)
	{
		SandboxSettings.m_FogHeightMinWorldY = Mathf.Clamp(height, 0f, SandboxSettings.m_FogHeightMaxWorldY);
		if (m_InputField.gameObject.activeInHierarchy)
		{
			m_InputField.text = Utils.FormatDistance(SandboxSettings.m_FogHeightMinWorldY);
			MaybeSnapshot();
		}
	}

	public void RestoreHeightFogStartMin(GameObject go)
	{
		m_InputField.text = Utils.FormatDistance(SandboxSettings.m_FogHeightMinWorldY);
	}

	public void AddHeightFogStartMax(GameObject go, float deltaHeight)
	{
		SetHeightFogStartMax(go, SandboxSettings.m_FogHeightMaxWorldY + deltaHeight);
	}

	public void SetHeightFogStartMax(GameObject go, float height)
	{
		SandboxSettings.m_FogHeightMaxWorldY = Mathf.Clamp(height, SandboxSettings.m_FogHeightMinWorldY, WaterBlocks.GetHeight());
		if (m_InputField.gameObject.activeInHierarchy)
		{
			m_InputField.text = Utils.FormatDistance(SandboxSettings.m_FogHeightMaxWorldY);
			MaybeSnapshot();
		}
	}

	public void RestoreHeightFogStartMax(GameObject go)
	{
		m_InputField.text = Utils.FormatDistance(SandboxSettings.m_FogHeightMaxWorldY);
	}

	public void AddHeightFogEnd(GameObject go, float deltaHeight)
	{
		SetHeightFogEnd(go, SandboxSettings.m_FogHeightEndRelativeY + deltaHeight);
	}

	public void SetHeightFogEnd(GameObject go, float height)
	{
		SandboxSettings.m_FogHeightEndRelativeY = Mathf.Clamp(height, 0f, HeightFog.MAX_HEIGHT);
		if (m_InputField.gameObject.activeInHierarchy)
		{
			m_InputField.text = Utils.FormatDistance(SandboxSettings.m_FogHeightEndRelativeY);
			MaybeSnapshot();
		}
	}

	public void RestoreHeightFogEnd(GameObject go)
	{
		m_InputField.text = Utils.FormatDistance(SandboxSettings.m_FogHeightEndRelativeY);
	}

	private void SetCallbacks(AddDeltaDelegate addCallback, SetDelegate setCallback, RestoreDelegate restoreCallback)
	{
		m_AddCallback = addCallback;
		m_SetCallback = setCallback;
		m_RestoreCallback = restoreCallback;
	}

	private void UpdateContinuousHold()
	{
		if (GameInput.GetMouseButtonJustReleased(0) && m_ContinuousHoldActive)
		{
			m_ContinuousHoldActive = false;
			if (m_DoSnapShotWhenContinuousHoldOff)
			{
				m_DoSnapShotWhenContinuousHoldOff = false;
				SandboxUndo.SnapShot();
			}
		}
		if (GameInput.GetMouseButtonJustPressed(0) && !m_ContinuousHoldActive)
		{
			if (m_ButtonIncrease.GetComponent<PointerEvents>().m_IsHovering)
			{
				m_ContinuousHoldButton = m_ButtonIncrease;
			}
			else if (m_ButtonDecrease.GetComponent<PointerEvents>().m_IsHovering)
			{
				m_ContinuousHoldButton = m_ButtonDecrease;
			}
			else
			{
				m_ContinuousHoldButton = null;
			}
			m_ContinuousHoldActive = true;
			m_ContinuousHoldTime = 0f;
			m_NextTickTime = 0f;
		}
		if (m_ContinuousHoldActive)
		{
			if (m_ButtonIncrease.GetComponent<PointerEvents>().m_IsHovering && m_ContinuousHoldButton == m_ButtonIncrease)
			{
				OnIncreaseContinuous();
				m_DoSnapShotWhenContinuousHoldOff = true;
			}
			if (m_ButtonDecrease.GetComponent<PointerEvents>().m_IsHovering && m_ContinuousHoldButton == m_ButtonDecrease)
			{
				OnDecreaseContinuous();
				m_DoSnapShotWhenContinuousHoldOff = true;
			}
		}
	}

	public void OnIncrease()
	{
		InterfaceAudio.Play("ui_settings_value_scroll");
		if (!m_IgnoreNextIncreaseOrDecrease)
		{
			GameObject selectedGameObject = GetSelectedGameObject();
			m_AddCallback(selectedGameObject, GetDelta(selectedGameObject));
		}
		m_IgnoreNextIncreaseOrDecrease = false;
	}

	public void OnIncreaseContinuous()
	{
		GameObject selectedGameObject = GetSelectedGameObject();
		AddDeltaContinuous(selectedGameObject, GetDelta(selectedGameObject));
	}

	public void OnDecrease()
	{
		InterfaceAudio.Play("ui_settings_value_scroll");
		if (!m_IgnoreNextIncreaseOrDecrease)
		{
			GameObject selectedGameObject = GetSelectedGameObject();
			m_AddCallback(selectedGameObject, 0f - GetDelta(selectedGameObject));
		}
		m_IgnoreNextIncreaseOrDecrease = false;
	}

	public void OnDecreaseContinuous()
	{
		GameObject selectedGameObject = GetSelectedGameObject();
		AddDeltaContinuous(selectedGameObject, 0f - GetDelta(selectedGameObject));
	}

	private void OnEndEdit()
	{
		string strippedValue = GetStrippedValue(m_InputField.text);
		if (string.IsNullOrEmpty(strippedValue) && m_SetCallback != null)
		{
			m_SetCallback(GetSelectedGameObject(), 0f);
			return;
		}
		GameObject selectedGameObject = GetSelectedGameObject();
		if (float.TryParse(strippedValue, out var result))
		{
			if (float.IsNaN(result) || float.IsInfinity(result))
			{
				if (m_RestoreCallback != null)
				{
					m_RestoreCallback(selectedGameObject);
				}
			}
			else
			{
				if (m_SetCallback == null)
				{
					return;
				}
				if (selectedGameObject != null)
				{
					SandboxItem component = selectedGameObject.GetComponent<SandboxItem>();
					if (component != null && component.m_Type == SandboxItemType.TERRAIN && m_Format == SandboxInputFieldFormat.HEIGHT && GameGrid.IsGridAligned(result, TerrainIslands.GRID_ALIGN_OFFSET))
					{
						result += 0.001f;
					}
				}
				m_SetCallback(selectedGameObject, Mathf.Clamp(result, -100000000f, 100000000f));
				MaybeUpdateForUniformScaleFlag(selectedGameObject, result);
			}
		}
		else if (m_RestoreCallback != null)
		{
			m_RestoreCallback(selectedGameObject);
		}
	}

	public void AddDeltaContinuous(GameObject go, float delta)
	{
		m_ContinuousHoldTime += Time.unscaledDeltaTime;
		m_NextTickTime += Time.unscaledDeltaTime;
		if (m_ContinuousHoldTime > 0.3f && m_NextTickTime > 0.05f)
		{
			m_AddCallback(go, delta);
			m_NextTickTime = Mathf.Min(m_NextTickTime - 0.05f, 0.05f);
		}
	}

	private void AddPosX(GameObject go, float delta)
	{
		if ((bool)go)
		{
			float f = go.transform.position.x + delta;
			SetPosX(go, go.GetComponent<SandboxItem>().RoundToNearestGridSquare(f));
		}
	}

	private void SetPosX(GameObject go, float x)
	{
		if (!go)
		{
			return;
		}
		TerrainIsland component = go.GetComponent<TerrainIsland>();
		if ((bool)component && component.m_TerrainIslandType == TerrainIslandType.Bookend)
		{
			TerrainIsland leftTerrain = TerrainIslands.GetLeftTerrain();
			TerrainIsland rightTerrain = TerrainIslands.GetRightTerrain();
			if (component == leftTerrain && x > rightTerrain.transform.position.x)
			{
				m_InputField.text = Utils.FormatThreeDecimalPlaces(go.transform.position.x);
				return;
			}
			if (component == rightTerrain && x < leftTerrain.transform.position.x)
			{
				m_InputField.text = Utils.FormatThreeDecimalPlaces(go.transform.position.x);
				return;
			}
			if (component == leftTerrain)
			{
				if (rightTerrain.transform.position.x - x > TerrainIslands.MAX_SEPARATION_X)
				{
					x = rightTerrain.transform.position.x - TerrainIslands.MAX_SEPARATION_X;
				}
			}
			else if (x - leftTerrain.transform.position.x > TerrainIslands.MAX_SEPARATION_X)
			{
				x = leftTerrain.transform.position.x + TerrainIslands.MAX_SEPARATION_X;
			}
		}
		go.transform.position = new Vector3(Mathf.Clamp(x, SandboxItems.MIN_X, SandboxItems.MAX_X), go.transform.position.y, go.transform.position.z);
		WorldBounds.Calculate(GameSettings.WorldWidth(), GameSettings.WorldMinY(), GameSettings.WorldMaxY());
		UpdatePolygonShapes(go);
		MarkOutlineDirty(go);
		BridgeJoints.ResolveOverlappingAnchors(Vector3.up);
		m_InputField.text = Utils.FormatThreeDecimalPlaces(go.transform.position.x);
		MaybeSnapshot();
	}

	private void RestorePosX(GameObject go)
	{
		if ((bool)go)
		{
			m_InputField.text = Utils.FormatThreeDecimalPlaces(go.transform.position.x);
		}
	}

	private void AddPosY(GameObject go, float delta)
	{
		if ((bool)go)
		{
			float f = go.transform.position.y + delta;
			float num = go.GetComponent<SandboxItem>().RoundToNearestGridSquare(f);
			Vehicle component = go.GetComponent<Vehicle>();
			VehicleStopTrigger component2 = go.GetComponent<VehicleStopTrigger>();
			if ((bool)component || (bool)component2)
			{
				num += BridgeMaterials.GetRoadCollisionOffset();
			}
			SetPosY(go, num);
		}
	}

	private void SetPosY(GameObject go, float y)
	{
		if ((bool)go)
		{
			y = Mathf.Clamp(y, SandboxItems.MIN_Y, SandboxItems.MAX_Y);
			float y2 = go.transform.position.y;
			go.transform.position = new Vector3(go.transform.position.x, y, go.transform.position.z);
			float num = go.transform.position.y - y2;
			float y3 = go.transform.position.y;
			m_InputField.text = Utils.FormatThreeDecimalPlaces(y3);
			UpdatePolygonShapes(go);
			MarkOutlineDirty(go);
			BridgeJoints.ResolveOverlappingAnchors((num > 0f) ? Vector3.up : Vector3.down);
			MaybeSnapshot();
		}
	}

	private void RestorePosY(GameObject go)
	{
		if ((bool)go)
		{
			m_InputField.text = Utils.FormatThreeDecimalPlaces(go.transform.position.y);
		}
	}

	private void AddPosZ(GameObject go, float delta)
	{
		if ((bool)go)
		{
			float f = go.transform.position.z + delta;
			SetPosZ(go, GameGrid.RoundToNearestGridSquare(f));
		}
	}

	private void SetPosZ(GameObject go, float z)
	{
		if ((bool)go)
		{
			z = Mathf.Clamp(z, CustomShapes.MIN_Z, CustomShapes.MAX_Z);
			go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, z);
			m_InputField.text = Utils.FormatThreeDecimalPlaces(go.transform.position.z);
			MaybeSnapshot();
			UpdatePolygonShapes(go);
		}
	}

	private void RestorePosZ(GameObject go)
	{
		if ((bool)go)
		{
			m_InputField.text = Utils.FormatThreeDecimalPlaces(go.transform.position.z);
		}
	}

	private void AddScale(GameObject go, float delta)
	{
		if ((bool)go)
		{
			float num = Mathf.Abs(go.transform.localScale.x * 100f);
			FlyingObject component = go.GetComponent<FlyingObject>();
			if ((bool)component)
			{
				num = component.GetUniformScaleNormalized() * 100f;
			}
			CustomShape component2 = go.GetComponent<CustomShape>();
			if ((bool)component2)
			{
				num = component2.transform.localScale.x * 100f;
			}
			Vehicle component3 = go.GetComponent<Vehicle>();
			if ((bool)component3)
			{
				num = component3.GetUniformScaleNormalized() * 100f;
			}
			ZedAxisVehicle component4 = go.GetComponent<ZedAxisVehicle>();
			if ((bool)component4)
			{
				num = component4.GetUniformScaleNormalized() * 100f;
			}
			SetScale(go, num + delta);
		}
	}

	private void SetScale(GameObject go, float percentage)
	{
		if (!go)
		{
			return;
		}
		FlyingObject component = go.GetComponent<FlyingObject>();
		if ((bool)component)
		{
			float num = Mathf.Clamp(percentage / 100f, FlyingObjects.MIN_NORMALIZED_SCALE, FlyingObjects.MAX_NORMALIZED_SCALE);
			if (!Mathf.Approximately(num, 0f))
			{
				component.transform.localScale = new Vector3(num, num, num);
				component.UpdatePolygonShapes();
				m_InputField.text = Utils.FormatPercentageToTwoDecimalPlaces(num);
				if (m_LinkedSlider != null)
				{
					GameUI.m_Instance.m_SandboxEditFlyingObject.SkipInputFieldUpdateFromSlider();
					m_LinkedSlider.SetValue(num * 100f);
				}
				MarkOutlineDirty(go);
				MaybeSnapshot();
			}
		}
		CustomShape component2 = go.GetComponent<CustomShape>();
		if ((bool)component2)
		{
			float num2 = Mathf.Clamp(percentage / 100f, CustomShapes.MIN_NORMALIZED_SCALE, CustomShapes.MAX_NORMALIZED_SCALE);
			if (!Mathf.Approximately(num2, 0f))
			{
				component2.transform.localScale = new Vector3((component2.transform.localScale.x < 0f) ? (0f - num2) : num2, num2, component2.IsDynamicProp() ? num2 : 1f);
				component2.UpdatePolygonShapes();
				component2.UpdateVisualScale();
				m_InputField.text = Utils.FormatPercentageToTwoDecimalPlaces(num2);
				if (m_LinkedSlider != null)
				{
					GameUI.m_Instance.m_SandboxEditCustomShape.SkipInputFieldUpdateFromSlider();
					m_LinkedSlider.SetValue(num2 * 100f);
				}
				MarkOutlineDirty(go);
				MaybeSnapshot();
			}
		}
		ZedAxisVehicle component3 = go.GetComponent<ZedAxisVehicle>();
		if ((bool)component3)
		{
			float num3 = Mathf.Clamp(percentage / 100f, ZedAxisVehicles.MIN_NORMALIZED_SCALE, ZedAxisVehicles.MAX_NORMALIZED_SCALE);
			if (!Mathf.Approximately(num3, 0f))
			{
				component3.SetUniformScale(num3);
				m_InputField.text = Utils.FormatPercentageToTwoDecimalPlaces(num3);
				if (m_LinkedSlider != null)
				{
					GameUI.m_Instance.m_SandboxEditZedAxisVehicle.SkipInputFieldUpdateFromSlider();
					m_LinkedSlider.SetValue(num3 * 100f);
				}
				SandboxItems.ResolveOverlappingFloatingText();
				MarkOutlineDirty(go);
				MaybeSnapshot();
			}
		}
		Vehicle component4 = go.GetComponent<Vehicle>();
		if (!component4)
		{
			return;
		}
		float num4 = Mathf.Clamp(percentage / 100f, Vehicles.MIN_NORMALIZED_SCALE, Vehicles.MAX_NORMALIZED_SCALE);
		if (!Mathf.Approximately(num4, 0f))
		{
			component4.SetUniformScale(num4);
			m_InputField.text = Utils.FormatPercentageToTwoDecimalPlaces(num4);
			if (m_LinkedSlider != null)
			{
				GameUI.m_Instance.m_SandboxEditVehicle.SkipInputFieldUpdateFromSlider();
				m_LinkedSlider.SetValue(num4 * 100f);
			}
			SandboxItems.ResolveOverlappingFloatingText();
			MarkOutlineDirty(go);
			MaybeSnapshot();
		}
	}

	private void RestoreScale(GameObject go)
	{
		if ((bool)go)
		{
			int num = Mathf.RoundToInt(go.transform.localScale.x * 100f);
			m_InputField.text = Utils.FormatPercentageToTwoDecimalPlaces(num);
		}
	}

	private void AddCustomShapeMeshScale(GameObject go, float delta)
	{
		CustomShape component = go.GetComponent<CustomShape>();
		if ((bool)component && (bool)component.m_CustomMesh)
		{
			float num = component.m_CustomMesh.transform.localScale.x * 100f;
			SetScale(go, num + delta);
		}
	}

	private void SetCustomShapeMeshScale(GameObject go, float percentage)
	{
		if (!go)
		{
			return;
		}
		CustomShape component = go.GetComponent<CustomShape>();
		if (!component || !component.m_CustomMesh)
		{
			return;
		}
		float num = Mathf.Clamp(percentage / 100f, CustomShapes.MIN_NORMALIZED_SCALE, CustomShapes.MAX_NORMALIZED_SCALE);
		if (!Mathf.Approximately(num, 0f))
		{
			component.m_CustomMesh.transform.localScale = new Vector3((component.m_CustomMesh.transform.localScale.x < 0f) ? (0f - num) : num, num, 1f);
			m_InputField.text = Utils.FormatPercentageToTwoDecimalPlaces(num);
			if (m_LinkedSlider != null)
			{
				GameUI.m_Instance.m_SandboxEditCustomShape.SkipInputFieldUpdateFromSlider();
				m_LinkedSlider.SetValue(num * 100f);
			}
			MaybeSnapshot();
		}
	}

	private void RestoreCustomShapeMeshScale(GameObject go)
	{
		if ((bool)go)
		{
			CustomShape component = go.GetComponent<CustomShape>();
			if ((bool)component && (bool)component.m_CustomMesh)
			{
				int num = Mathf.RoundToInt(component.m_CustomMesh.transform.localScale.x * 100f);
				m_InputField.text = Utils.FormatPercentageToTwoDecimalPlaces(num);
			}
		}
	}

	private void AddScaleX(GameObject go, float delta)
	{
		if ((bool)go)
		{
			SetScaleX(go, go.transform.localScale.x * 100f + delta);
		}
	}

	private void SetScaleX(GameObject go, float x)
	{
		if (!go)
		{
			return;
		}
		Rock component = go.GetComponent<Rock>();
		Decor component2 = go.GetComponent<Decor>();
		if (!component && !component2)
		{
			return;
		}
		float min = (component ? Rocks.MIN_NORMALIZED_SCALE : Decors.MIN_NORMALIZED_SCALE);
		float max = (component ? Rocks.MAX_NORMALIZED_SCALE_X : Decors.MAX_NORMALIZED_SCALE_X);
		float num = Mathf.Clamp(x / 100f, min, max);
		if (Mathf.Approximately(num, 0f))
		{
			return;
		}
		Vector3 localScale = new Vector3(num, go.transform.localScale.y, go.transform.localScale.z);
		go.transform.localScale = localScale;
		UpdatePolygonShapes(go);
		m_InputField.text = Utils.FormatPercentageToTwoDecimalPlaces(num);
		if (m_LinkedSlider != null)
		{
			if ((bool)component)
			{
				GameUI.m_Instance.m_SandboxEditRock.SkipInputFieldUpdateFromSlider();
			}
			else
			{
				GameUI.m_Instance.m_SandboxEditDecor.SkipInputFieldUpdateFromSlider();
			}
			m_LinkedSlider.SetValue(num * 100f);
		}
		MarkOutlineDirty(go);
		MaybeSnapshot();
	}

	private void RestoreScaleX(GameObject go)
	{
		if ((bool)go)
		{
			int num = Mathf.RoundToInt(go.transform.localScale.x * 100f);
			m_InputField.text = Utils.FormatPercentageToTwoDecimalPlaces(num);
		}
	}

	private void AddScaleY(GameObject go, float delta)
	{
		if ((bool)go)
		{
			SetScaleY(go, go.transform.localScale.y * 100f + delta);
		}
	}

	private void SetScaleY(GameObject go, float y)
	{
		if (!go)
		{
			return;
		}
		Rock component = go.GetComponent<Rock>();
		Decor component2 = go.GetComponent<Decor>();
		if (!component && !component2)
		{
			return;
		}
		float min = (component ? Rocks.MIN_NORMALIZED_SCALE : Decors.MIN_NORMALIZED_SCALE);
		float max = (component ? Rocks.MAX_NORMALIZED_SCALE_Y : Decors.MAX_NORMALIZED_SCALE_Y);
		float num = Mathf.Clamp(y / 100f, min, max);
		if (Mathf.Approximately(num, 0f))
		{
			return;
		}
		Vector3 localScale = new Vector3(go.transform.localScale.x, num, go.transform.localScale.z);
		go.transform.localScale = localScale;
		UpdatePolygonShapes(go);
		m_InputField.text = Utils.FormatPercentageToTwoDecimalPlaces(num);
		if (m_LinkedSlider != null)
		{
			if ((bool)component)
			{
				GameUI.m_Instance.m_SandboxEditRock.SkipInputFieldUpdateFromSlider();
			}
			else
			{
				GameUI.m_Instance.m_SandboxEditDecor.SkipInputFieldUpdateFromSlider();
			}
			m_LinkedSlider.SetValue(num * 100f);
		}
		MarkOutlineDirty(go);
		MaybeSnapshot();
	}

	private void RestoreScaleY(GameObject go)
	{
		if ((bool)go)
		{
			int num = Mathf.RoundToInt(go.transform.localScale.y * 100f);
			m_InputField.text = Utils.FormatPercentageToTwoDecimalPlaces(num);
		}
	}

	private void AddScaleZ(GameObject go, float delta)
	{
		if ((bool)go)
		{
			SetScaleZ(go, go.transform.localScale.z * 100f + delta);
		}
	}

	private void SetScaleZ(GameObject go, float z)
	{
		if (!go)
		{
			return;
		}
		Rock component = go.GetComponent<Rock>();
		Decor component2 = go.GetComponent<Decor>();
		if (!component && !component2)
		{
			return;
		}
		float min = (component ? Rocks.MIN_NORMALIZED_SCALE : Decors.MIN_NORMALIZED_SCALE);
		float max = (component ? Rocks.MAX_NORMALIZED_SCALE_Z : Decors.MAX_NORMALIZED_SCALE_Z);
		float num = Mathf.Clamp(z / 100f, min, max);
		if (Mathf.Approximately(num, 0f))
		{
			return;
		}
		Vector3 localScale = new Vector3(go.transform.localScale.x, go.transform.localScale.y, num);
		go.transform.localScale = localScale;
		UpdatePolygonShapes(go);
		m_InputField.text = Utils.FormatPercentageToTwoDecimalPlaces(num);
		if (m_LinkedSlider != null)
		{
			if ((bool)component)
			{
				GameUI.m_Instance.m_SandboxEditRock.SkipInputFieldUpdateFromSlider();
			}
			else
			{
				GameUI.m_Instance.m_SandboxEditDecor.SkipInputFieldUpdateFromSlider();
			}
			m_LinkedSlider.SetValue(num * 100f);
		}
		MarkOutlineDirty(go);
		MaybeSnapshot();
	}

	private void RestoreScaleZ(GameObject go)
	{
		if ((bool)go)
		{
			int num = Mathf.RoundToInt(go.transform.localScale.z * 100f);
			m_InputField.text = Utils.FormatPercentageToTwoDecimalPlaces(num);
		}
	}

	private void AddTiling(GameObject go, float delta)
	{
		if ((bool)go)
		{
			CustomShape component = go.GetComponent<CustomShape>();
			if ((bool)component)
			{
				SetTiling(go, component.m_TextureTiling + delta);
			}
		}
	}

	private void SetTiling(GameObject go, float value)
	{
		if ((bool)go)
		{
			CustomShape component = go.GetComponent<CustomShape>();
			if ((bool)component)
			{
				component.m_TextureTiling = value;
				component.UpdateShaderProperties(component.m_Color, buildMode: false);
				m_InputField.text = Utils.FormatTwoDecimalPlaces(component.m_TextureTiling);
				MaybeSnapshot();
			}
		}
	}

	private void RestoreTiling(GameObject go)
	{
		if ((bool)go)
		{
			CustomShape component = go.GetComponent<CustomShape>();
			if ((bool)component)
			{
				m_InputField.text = Utils.FormatTwoDecimalPlaces(component.m_TextureTiling);
			}
		}
	}

	private void AddNudge(GameObject go, float delta)
	{
		SetNudge(go, SandboxSettings.m_MultiSelectMovementIncrement + delta);
	}

	private void SetNudge(GameObject go, float value)
	{
		float p = (SandboxSettings.m_MultiSelectMovementIncrement = Mathf.Clamp(value, Panel_SandboxNudge.MIN_INCREMENT, Panel_SandboxNudge.MAX_INCREMENT));
		m_InputField.text = Utils.FormatThreeDecimalPlaces(p);
		MaybeSnapshot();
	}

	private void RestoreNudge(GameObject go)
	{
		m_InputField.text = Utils.FormatThreeDecimalPlaces(SandboxSettings.m_MultiSelectMovementIncrement);
	}

	private void AddWidth(GameObject go, float deltaWidth)
	{
		if ((bool)go)
		{
			Platform component = go.GetComponent<Platform>();
			if ((bool)component)
			{
				SetWidth(go, GameGrid.RoundToNearestGridSquare(component.m_Width + deltaWidth));
			}
			BuildZone component2 = go.GetComponent<BuildZone>();
			if ((bool)component2)
			{
				SetWidth(go, GameGrid.RoundToNearestGridSquare(component2.GetSize().x + deltaWidth));
			}
		}
	}

	private void SetWidth(GameObject go, float width)
	{
		if (!go)
		{
			return;
		}
		Platform component = go.GetComponent<Platform>();
		if ((bool)component)
		{
			component.m_Width = Mathf.Clamp(width, Platforms.MIN_WIDTH, Platforms.MAX_WIDTH);
			component.RefreshMesh();
			m_InputField.text = Utils.FormatDistance(component.m_Width);
			if (m_LinkedSlider != null)
			{
				GameUI.m_Instance.m_SandboxEditPlatform.SkipInputFieldUpdateFromSlider();
				m_LinkedSlider.SetValue(component.m_Width);
			}
			MaybeSnapshot();
		}
		BuildZone component2 = go.GetComponent<BuildZone>();
		if ((bool)component2)
		{
			width = Mathf.Clamp(width, BuildZones.MIN_WIDTH, BuildZones.MAX_WIDTH);
			component2.SetBounds(component2.GetPosition(), new Vector2(width, component2.GetSize().y));
			m_InputField.text = Utils.FormatThreeDecimalPlaces(component2.GetSize().x);
			if (m_LinkedSlider != null)
			{
				GameUI.m_Instance.m_SandboxEditBuildZone.SkipInputFieldUpdateFromSlider();
				m_LinkedSlider.SetValue(component2.GetSize().x);
			}
			component2.PositionControlPoints();
			MaybeSnapshot();
		}
	}

	private void RestoreWidth(GameObject go)
	{
		if ((bool)go)
		{
			Platform component = go.GetComponent<Platform>();
			if ((bool)component)
			{
				m_InputField.text = Utils.FormatDistance(component.m_Width);
			}
			BuildZone component2 = go.GetComponent<BuildZone>();
			if ((bool)component2)
			{
				m_InputField.text = Utils.FormatDistance(component2.GetSize().x);
			}
		}
	}

	private void AddThickness(GameObject go, float deltaThickness)
	{
		if ((bool)go)
		{
			CustomShape component = go.GetComponent<CustomShape>();
			if ((bool)component)
			{
				SetThickness(go, component.m_Thickness + deltaThickness);
			}
		}
	}

	private void SetThickness(GameObject go, float Thickness)
	{
		if ((bool)go)
		{
			CustomShape component = go.GetComponent<CustomShape>();
			if ((bool)component)
			{
				component.m_Thickness = Mathf.Clamp(Thickness, CustomShapes.MIN_THICKNESS, CustomShapes.MAX_THICKNESS);
				component.RebuildMesh();
				m_InputField.text = Utils.FormatDistanceOneDecimalPlace(component.m_Thickness);
				MaybeSnapshot();
			}
		}
	}

	private void RestoreThickness(GameObject go)
	{
		if ((bool)go)
		{
			CustomShape component = go.GetComponent<CustomShape>();
			if ((bool)component)
			{
				m_InputField.text = Utils.FormatDistance(component.m_Thickness);
			}
		}
	}

	private void AddRoll(GameObject go, float deltaAngle)
	{
		if ((bool)go)
		{
			Decor component = go.GetComponent<Decor>();
			if ((bool)component)
			{
				SetRoll(go, component.m_RollRotationDegrees + deltaAngle);
			}
		}
	}

	private void AddPitch(GameObject go, float deltaAngle)
	{
		if ((bool)go)
		{
			Decor component = go.GetComponent<Decor>();
			if ((bool)component)
			{
				SetPitch(go, component.m_PitchRotationDegrees + deltaAngle);
			}
		}
	}

	private void SetRoll(GameObject go, float angle)
	{
		if ((bool)go)
		{
			Decor component = go.GetComponent<Decor>();
			if ((bool)component)
			{
				component.m_RollRotationDegrees = angle % 360f;
				SetRotHelper(go, new Vector3(0f - component.m_PitchRotationDegrees, 0f - component.m_HeadingRotationDegrees, 0f - component.m_RollRotationDegrees));
				m_InputField.text = Utils.FormatAngle(component.m_RollRotationDegrees);
			}
		}
	}

	private void SetPitch(GameObject go, float angle)
	{
		if ((bool)go)
		{
			Decor component = go.GetComponent<Decor>();
			if ((bool)component)
			{
				component.m_PitchRotationDegrees = angle % 360f;
				SetRotHelper(go, new Vector3(0f - component.m_PitchRotationDegrees, 0f - component.m_HeadingRotationDegrees, 0f - component.m_RollRotationDegrees));
				m_InputField.text = Utils.FormatAngle(component.m_PitchRotationDegrees);
			}
		}
	}

	private void RestoreRoll(GameObject go)
	{
		if ((bool)go)
		{
			Decor component = go.GetComponent<Decor>();
			if ((bool)component)
			{
				m_InputField.text = Utils.FormatAngle(component.m_RollRotationDegrees);
			}
		}
	}

	private void RestorePitch(GameObject go)
	{
		if ((bool)go)
		{
			Decor component = go.GetComponent<Decor>();
			if ((bool)component)
			{
				m_InputField.text = Utils.FormatAngle(component.m_PitchRotationDegrees);
			}
		}
	}

	private void AddRot(GameObject go, float deltaAngle)
	{
		if ((bool)go)
		{
			Vehicle component = go.GetComponent<Vehicle>();
			if ((bool)component)
			{
				SetRot(go, component.m_RotationDegrees + deltaAngle);
			}
			VehicleStopTrigger component2 = go.GetComponent<VehicleStopTrigger>();
			if ((bool)component2)
			{
				SetRot(go, component2.m_RotationDegrees + deltaAngle);
			}
			CustomShape component3 = go.GetComponent<CustomShape>();
			if ((bool)component3)
			{
				SetRot(go, component3.m_RotationDegrees + deltaAngle);
			}
			Decor component4 = go.GetComponent<Decor>();
			if ((bool)component4)
			{
				SetRot(go, component4.m_HeadingRotationDegrees + deltaAngle);
			}
			ZedAxisVehicle component5 = go.GetComponent<ZedAxisVehicle>();
			if ((bool)component5)
			{
				SetRot(go, component5.m_RotationDegrees + deltaAngle);
			}
			BuildZone component6 = go.GetComponent<BuildZone>();
			if ((bool)component6)
			{
				SetRot(go, component6.m_RotationDegrees + deltaAngle);
			}
		}
	}

	public void SetRot(GameObject go, float angle)
	{
		if (!go)
		{
			return;
		}
		Vehicle component = go.GetComponent<Vehicle>();
		if ((bool)component)
		{
			component.m_RotationDegrees = angle % 360f;
			SetRotHelper(go, new Vector3(0f, 0f, 0f - component.m_RotationDegrees));
			m_InputField.text = Utils.FormatAngle(component.m_RotationDegrees);
			if (m_LinkedSlider != null)
			{
				m_LinkedSlider.SetValue(component.m_RotationDegrees);
			}
			component.UpdatePolygonShapes();
		}
		VehicleStopTrigger component2 = go.GetComponent<VehicleStopTrigger>();
		if ((bool)component2)
		{
			component2.m_RotationDegrees = angle % 360f;
			SetRotHelper(go, new Vector3(0f, 0f, 0f - component2.m_RotationDegrees));
			m_InputField.text = Utils.FormatAngle(component2.m_RotationDegrees);
			if (m_LinkedSlider != null)
			{
				m_LinkedSlider.SetValue(component2.m_RotationDegrees);
			}
		}
		CustomShape component3 = go.GetComponent<CustomShape>();
		if ((bool)component3)
		{
			component3.m_RotationDegrees = angle % 360f;
			component3.UpdateAfterRotation();
			SetRotHelper(go, new Vector3(0f, 0f, 0f - component3.m_RotationDegrees));
			m_InputField.text = Utils.FormatAngle(component3.m_RotationDegrees);
			if (m_LinkedSlider != null)
			{
				m_LinkedSlider.SetValue(component3.m_RotationDegrees);
			}
		}
		ZedAxisVehicle component4 = go.GetComponent<ZedAxisVehicle>();
		if ((bool)component4)
		{
			component4.m_RotationDegrees = angle % 360f;
			SetRotHelper(go, new Vector3(0f, 0f, 0f - component4.m_RotationDegrees));
			m_InputField.text = Utils.FormatAngle(component4.m_RotationDegrees);
			if (m_LinkedSlider != null)
			{
				m_LinkedSlider.SetValue(component4.m_RotationDegrees);
			}
			component4.UpdatePolygonShapes();
		}
		Decor component5 = go.GetComponent<Decor>();
		if ((bool)component5)
		{
			component5.m_HeadingRotationDegrees = angle % 360f;
			SetRotHelper(go, new Vector3(0f - component5.m_PitchRotationDegrees, 0f - component5.m_HeadingRotationDegrees, 0f - component5.m_RollRotationDegrees));
			m_InputField.text = Utils.FormatAngle(component5.m_HeadingRotationDegrees);
			if (m_LinkedSlider != null)
			{
				m_LinkedSlider.SetValue(component5.m_HeadingRotationDegrees);
			}
		}
		BuildZone component6 = go.GetComponent<BuildZone>();
		if ((bool)component6)
		{
			component6.m_RotationDegrees = angle % 360f;
			SetRotHelper(go, new Vector3(0f, 0f, 0f - component6.m_RotationDegrees));
			m_InputField.text = Utils.FormatAngle(component6.m_RotationDegrees);
			if (m_LinkedSlider != null)
			{
				m_LinkedSlider.SetValue(component6.m_RotationDegrees);
			}
		}
		UpdatePolygonShapes(go);
		MarkOutlineDirty(go);
		MaybeSnapshot();
	}

	private void SetRotHelper(GameObject go, Vector3 angles)
	{
		if ((bool)go)
		{
			go.transform.rotation = Quaternion.Euler(angles);
			SandboxItem component = go.GetComponent<SandboxItem>();
			if (component != null)
			{
				component.SetFloatingTextToDefaultPosition();
			}
		}
	}

	private void RestoreRot(GameObject go)
	{
		if ((bool)go)
		{
			Vehicle component = go.GetComponent<Vehicle>();
			if ((bool)component)
			{
				m_InputField.text = Utils.FormatAngle(component.m_RotationDegrees);
			}
			VehicleStopTrigger component2 = go.GetComponent<VehicleStopTrigger>();
			if ((bool)component2)
			{
				m_InputField.text = Utils.FormatAngle(component2.m_RotationDegrees);
			}
			CustomShape component3 = go.GetComponent<CustomShape>();
			if ((bool)component3)
			{
				m_InputField.text = Utils.FormatAngle(component3.m_RotationDegrees);
			}
			Decor component4 = go.GetComponent<Decor>();
			if ((bool)component4)
			{
				m_InputField.text = Utils.FormatAngle(component4.m_HeadingRotationDegrees);
			}
			ZedAxisVehicle component5 = go.GetComponent<ZedAxisVehicle>();
			if ((bool)component5)
			{
				m_InputField.text = Utils.FormatAngle(component5.m_RotationDegrees);
			}
			BuildZone component6 = go.GetComponent<BuildZone>();
			if ((bool)component6)
			{
				m_InputField.text = Utils.FormatAngle(component6.m_RotationDegrees);
			}
		}
	}

	private void AddTimeDelay(GameObject go, float deltaSeconds)
	{
		if ((bool)go)
		{
			ZedAxisVehicle component = go.GetComponent<ZedAxisVehicle>();
			if ((bool)component)
			{
				SetTimeDelay(go, component.m_TimeDelaySeconds + deltaSeconds);
			}
			Vehicle component2 = go.GetComponent<Vehicle>();
			if ((bool)component2)
			{
				SetTimeDelay(go, component2.m_TimeDelaySeconds + deltaSeconds);
			}
			HydraulicsPhase component3 = go.GetComponent<HydraulicsPhase>();
			if ((bool)component3)
			{
				SetTimeDelay(go, component3.m_TimeDelaySeconds + deltaSeconds);
			}
			VehicleRestartPhase component4 = go.GetComponent<VehicleRestartPhase>();
			if ((bool)component4)
			{
				SetTimeDelay(go, component4.m_TimeDelaySeconds + deltaSeconds);
			}
		}
	}

	private void SetTimeDelay(GameObject go, float seconds)
	{
		if ((bool)go)
		{
			ZedAxisVehicle component = go.GetComponent<ZedAxisVehicle>();
			if ((bool)component)
			{
				component.m_TimeDelaySeconds = Mathf.Clamp(seconds, 0f, MAX_TIME_DELAY);
				m_InputField.text = Utils.FormatSeconds(component.m_TimeDelaySeconds);
			}
			Vehicle component2 = go.GetComponent<Vehicle>();
			if ((bool)component2)
			{
				component2.m_TimeDelaySeconds = Mathf.Clamp(seconds, 0f, MAX_TIME_DELAY);
				m_InputField.text = Utils.FormatSeconds(component2.m_TimeDelaySeconds);
			}
			HydraulicsPhase component3 = go.GetComponent<HydraulicsPhase>();
			if ((bool)component3)
			{
				component3.m_TimeDelaySeconds = Mathf.Clamp(seconds, 0f, MAX_TIME_DELAY);
				m_InputField.text = Utils.FormatSeconds(component3.m_TimeDelaySeconds);
			}
			VehicleRestartPhase component4 = go.GetComponent<VehicleRestartPhase>();
			if ((bool)component4)
			{
				component4.m_TimeDelaySeconds = Mathf.Clamp(seconds, 0f, MAX_TIME_DELAY);
				m_InputField.text = Utils.FormatSeconds(component4.m_TimeDelaySeconds);
			}
			MaybeSnapshot();
		}
	}

	private void RestoreTimeDelay(GameObject go)
	{
		if ((bool)go)
		{
			ZedAxisVehicle component = go.GetComponent<ZedAxisVehicle>();
			if ((bool)component)
			{
				m_InputField.text = Utils.FormatSeconds(component.m_TimeDelaySeconds);
			}
			Vehicle component2 = go.GetComponent<Vehicle>();
			if ((bool)component2)
			{
				m_InputField.text = Utils.FormatSeconds(component2.m_TimeDelaySeconds);
			}
			HydraulicsPhase component3 = go.GetComponent<HydraulicsPhase>();
			if ((bool)component3)
			{
				m_InputField.text = Utils.FormatSeconds(component3.m_TimeDelaySeconds);
			}
			VehicleRestartPhase component4 = go.GetComponent<VehicleRestartPhase>();
			if ((bool)component4)
			{
				m_InputField.text = Utils.FormatSeconds(component4.m_TimeDelaySeconds);
			}
		}
	}

	private void AddSpeed(GameObject go, float delta)
	{
		if ((bool)go)
		{
			Vehicle component = go.GetComponent<Vehicle>();
			if ((bool)component)
			{
				SetSpeed(go, component.m_TargetSpeed + delta);
			}
			ZedAxisVehicle component2 = go.GetComponent<ZedAxisVehicle>();
			if ((bool)component2)
			{
				SetSpeed(go, component2.m_Speed + delta);
			}
		}
	}

	private void SetSpeed(GameObject go, float speed)
	{
		if ((bool)go)
		{
			Vehicle component = go.GetComponent<Vehicle>();
			if ((bool)component)
			{
				component.m_TargetSpeed = Mathf.Clamp(speed, Vehicles.MIN_SPEED, Vehicles.MAX_SPEED);
				m_InputField.text = Utils.FormatSpeed(component.m_TargetSpeed);
			}
			ZedAxisVehicle component2 = go.GetComponent<ZedAxisVehicle>();
			if ((bool)component2)
			{
				component2.m_Speed = Mathf.Clamp(speed, ZedAxisVehicles.MIN_SPEED, ZedAxisVehicles.MAX_SPEED);
				m_InputField.text = Utils.FormatSpeed(component2.m_Speed);
			}
			MaybeSnapshot();
		}
	}

	private void RestoreSpeed(GameObject go)
	{
		if ((bool)go)
		{
			Vehicle component = go.GetComponent<Vehicle>();
			if ((bool)component)
			{
				m_InputField.text = Utils.FormatSpeed(component.m_TargetSpeed);
			}
			ZedAxisVehicle component2 = go.GetComponent<ZedAxisVehicle>();
			if ((bool)component2)
			{
				m_InputField.text = Utils.FormatSpeed(component2.m_Speed);
			}
		}
	}

	private void AddMass(GameObject go, float deltaPolyGrams)
	{
		if ((bool)go)
		{
			Vehicle component = go.GetComponent<Vehicle>();
			if ((bool)component)
			{
				SetMass(go, component.m_Mass * BridgePhysics.KgToPg + deltaPolyGrams);
			}
			CustomShape component2 = go.GetComponent<CustomShape>();
			if ((bool)component2)
			{
				SetMass(go, component2.m_Mass * BridgePhysics.KgToPg + deltaPolyGrams);
			}
		}
	}

	private void SetMass(GameObject go, float polyGrams)
	{
		if ((bool)go)
		{
			Vehicle component = go.GetComponent<Vehicle>();
			if ((bool)component)
			{
				component.m_Mass = Mathf.Clamp(polyGrams * BridgePhysics.PgToKg, Vehicles.MIN_MASS, Vehicles.MAX_MASS);
				m_InputField.text = Utils.FormatWeight(component.m_Mass * BridgePhysics.KgToPg);
				MaybeSnapshot();
			}
			CustomShape component2 = go.GetComponent<CustomShape>();
			if ((bool)component2)
			{
				component2.m_Mass = Mathf.Clamp(polyGrams * BridgePhysics.PgToKg, CustomShapes.MIN_MASS, CustomShapes.MAX_MASS);
				m_InputField.text = Utils.FormatWeight(component2.m_Mass * BridgePhysics.KgToPg);
				CustomShapes.UpdateCustomShapeMinimumStrengthHint(component2);
				MaybeSnapshot();
			}
		}
	}

	private void RestoreMass(GameObject go)
	{
		if ((bool)go)
		{
			Vehicle component = go.GetComponent<Vehicle>();
			if ((bool)component)
			{
				m_InputField.text = Utils.FormatWeight(component.m_Mass * BridgePhysics.KgToPg);
			}
			CustomShape component2 = go.GetComponent<CustomShape>();
			if ((bool)component2)
			{
				m_InputField.text = Utils.FormatWeight(component2.m_Mass * BridgePhysics.KgToPg);
			}
		}
	}

	private void AddAcceleration(GameObject go, float delta)
	{
		if ((bool)go)
		{
			Vehicle component = go.GetComponent<Vehicle>();
			if ((bool)component)
			{
				SetAcceleration(go, component.m_Acceleration + delta);
			}
		}
	}

	private void SetAcceleration(GameObject go, float acceleration)
	{
		if ((bool)go)
		{
			Vehicle component = go.GetComponent<Vehicle>();
			if ((bool)component)
			{
				component.m_Acceleration = Mathf.Clamp(acceleration, Vehicles.MIN_HORSEPOWER, Vehicles.MAX_HORSEPOWER);
				m_InputField.text = Utils.FormatAcceleration(component.m_Acceleration);
				MaybeSnapshot();
			}
		}
	}

	private void RestoreAcceleration(GameObject go)
	{
		if ((bool)go)
		{
			Vehicle component = go.GetComponent<Vehicle>();
			if ((bool)component)
			{
				m_InputField.text = Utils.FormatAcceleration(component.m_Acceleration);
			}
		}
	}

	private void AddDesiredAcceleration(GameObject go, float delta)
	{
		if ((bool)go)
		{
			Vehicle component = go.GetComponent<Vehicle>();
			if ((bool)component)
			{
				SetDesiredAcceleration(go, component.m_DesiredAcceleration + delta);
			}
			CustomShape component2 = go.GetComponent<CustomShape>();
			if ((bool)component2)
			{
				SetDesiredAcceleration(go, component2.m_PinTargetAccelerationSeconds + delta);
			}
		}
	}

	private void SetDesiredAcceleration(GameObject go, float desiredAcceleration)
	{
		if ((bool)go)
		{
			Vehicle component = go.GetComponent<Vehicle>();
			if ((bool)component)
			{
				component.m_DesiredAcceleration = Mathf.Clamp(desiredAcceleration, Vehicles.MIN_DESIRED_ACCELERATION, Vehicles.MAX_DESIRED_ACCELERATION);
				m_InputField.text = Utils.FormatAcceleration(component.m_DesiredAcceleration);
				MaybeSnapshot();
			}
			CustomShape component2 = go.GetComponent<CustomShape>();
			if ((bool)component2)
			{
				component2.m_PinTargetAccelerationSeconds = Mathf.Clamp(desiredAcceleration, 0f, CustomShapes.MAX_PIN_TARGET_ACCELERATION);
				m_InputField.text = Utils.FormatSeconds(component2.m_PinTargetAccelerationSeconds);
				CustomShapes.UpdateCustomShapeMinimumStrengthHint(component2);
				MaybeSnapshot();
			}
		}
	}

	private void RestoreDesiredAcceleration(GameObject go)
	{
		if ((bool)go)
		{
			Vehicle component = go.GetComponent<Vehicle>();
			if ((bool)component)
			{
				m_InputField.text = Utils.FormatAcceleration(component.m_DesiredAcceleration);
			}
			CustomShape component2 = go.GetComponent<CustomShape>();
			if ((bool)component2)
			{
				m_InputField.text = Utils.FormatOneDecimalPlace(component2.m_PinTargetAccelerationSeconds);
			}
		}
	}

	private void AddShocksMultiplier(GameObject go, float delta)
	{
		if ((bool)go)
		{
			Vehicle component = go.GetComponent<Vehicle>();
			if ((bool)component)
			{
				SetShocksMultiplier(go, component.m_ShocksMultiplier + delta);
			}
		}
	}

	private void SetShocksMultiplier(GameObject go, float shocksMultiplier)
	{
		if ((bool)go)
		{
			Vehicle component = go.GetComponent<Vehicle>();
			if ((bool)component)
			{
				component.m_ShocksMultiplier = Mathf.Clamp(shocksMultiplier, Vehicles.MIN_SHOCKS_MULTIPLIER, Vehicles.MAX_SHOCKS_MULTIPLIER);
				m_InputField.text = Utils.FormatOneDecimalPlace(component.m_ShocksMultiplier);
				MaybeSnapshot();
			}
		}
	}

	private void RestoreShocksMultiplier(GameObject go)
	{
		if ((bool)go)
		{
			Vehicle component = go.GetComponent<Vehicle>();
			if ((bool)component)
			{
				m_InputField.text = Utils.FormatOneDecimalPlace(component.m_ShocksMultiplier);
			}
		}
	}

	private void AddBrakingForceMultiplier(GameObject go, float delta)
	{
		if ((bool)go)
		{
			Vehicle component = go.GetComponent<Vehicle>();
			if ((bool)component)
			{
				SetBrakingForceMultiplier(component.gameObject, component.m_BrakingForceMultiplier + delta);
			}
		}
	}

	private void SetBrakingForceMultiplier(GameObject go, float mutliplier)
	{
		if ((bool)go)
		{
			Vehicle component = go.GetComponent<Vehicle>();
			if ((bool)component)
			{
				component.m_BrakingForceMultiplier = Mathf.Clamp(mutliplier, Vehicles.MIN_BRAKING_INTENSITY, Vehicles.MAX_BREAKING_INTENSITY);
				m_InputField.text = Utils.FormatOneDecimalPlace(component.m_BrakingForceMultiplier);
				MaybeSnapshot();
			}
		}
	}

	private void RestoreBrakingForceMultiplier(GameObject go)
	{
		if ((bool)go)
		{
			Vehicle component = go.GetComponent<Vehicle>();
			if ((bool)component)
			{
				m_InputField.text = Utils.FormatOneDecimalPlace(component.m_BrakingForceMultiplier);
			}
		}
	}

	private void AddNumSegments(GameObject go, float deltaNumSegments)
	{
		if ((bool)go)
		{
			Ramp component = go.GetComponent<Ramp>();
			if ((bool)component)
			{
				SetNumSegments(go, component.m_NumSegments + Mathf.FloorToInt(deltaNumSegments));
			}
		}
	}

	private void SetNumSegments(GameObject go, float numSegments)
	{
		if ((bool)go)
		{
			Ramp component = go.GetComponent<Ramp>();
			if ((bool)component)
			{
				component.m_NumSegments = Mathf.Clamp(Mathf.FloorToInt(numSegments), Ramps.MIN_NUM_SEGMENTS, int.MaxValue);
				component.RefreshMesh();
				m_InputField.text = component.m_NumSegments.ToString();
				MaybeSnapshot();
			}
		}
	}

	private void RestoreNumSegments(GameObject go)
	{
		if ((bool)go)
		{
			Ramp component = go.GetComponent<Ramp>();
			if ((bool)component)
			{
				m_InputField.text = component.m_NumSegments.ToString();
			}
		}
	}

	private void AddBudget(GameObject go, float deltaBudget)
	{
		Budget.m_CashBudget = Mathf.RoundToInt((float)Budget.m_CashBudget + deltaBudget);
		if (Budget.m_CashBudget < Budget.MIN_CASH_BUDGET)
		{
			Budget.m_CashBudget = Budget.UNLIMITED_CASH_BUDGET;
		}
		if (Budget.m_CashBudget > Budget.UNLIMITED_CASH_BUDGET)
		{
			Budget.m_CashBudget = Mathf.CeilToInt(Budget.m_BridgeCost / 1000f) * 1000;
		}
		SetBudget(null, Budget.m_CashBudget);
	}

	private void SetBudget(GameObject go, float budget)
	{
		Budget.m_CashBudget = Mathf.Clamp(Mathf.RoundToInt(budget), Budget.MIN_CASH_BUDGET, Budget.UNLIMITED_CASH_BUDGET);
		m_InputField.text = Utils.FormatCash(Budget.m_CashBudget);
		MaybeSnapshot();
	}

	private void RestoreBudget(GameObject go)
	{
		m_InputField.text = Utils.FormatCash(Budget.m_CashBudget);
	}

	private void AddRoadBudget(GameObject go, float deltaBudget)
	{
		Budget.m_RoadBudget += Mathf.RoundToInt(deltaBudget);
		if (Budget.m_RoadBudget < Budget.MIN_ROAD_BUDGET)
		{
			Budget.m_RoadBudget = Budget.UNLIMITED_MATERIAL_BUDGET;
		}
		if (Budget.m_RoadBudget > Budget.UNLIMITED_MATERIAL_BUDGET)
		{
			Budget.m_RoadBudget = Budget.MIN_ROAD_BUDGET;
		}
		SetRoadBudget(null, Budget.m_RoadBudget);
	}

	private void SetRoadBudget(GameObject go, float budget)
	{
		if (!(budget < 0f))
		{
			Budget.m_RoadBudget = Mathf.Clamp(Mathf.RoundToInt(budget), Budget.MIN_ROAD_BUDGET, Budget.UNLIMITED_MATERIAL_BUDGET);
			m_InputField.text = Utils.FormatMaterialBudget(Budget.m_RoadBudget);
			MaybeSnapshot();
		}
	}

	private void RestoreRoadBudget(GameObject go)
	{
		m_InputField.text = Utils.FormatMaterialBudget(Budget.m_RoadBudget);
	}

	private void AddWoodBudget(GameObject go, float deltaBudget)
	{
		Budget.m_WoodBudget += Mathf.RoundToInt(deltaBudget);
		if (Budget.m_WoodBudget < 0)
		{
			Budget.m_WoodBudget = Budget.UNLIMITED_MATERIAL_BUDGET;
		}
		if (Budget.m_WoodBudget > Budget.UNLIMITED_MATERIAL_BUDGET)
		{
			Budget.m_WoodBudget = 0;
		}
		SetWoodBudget(null, Budget.m_WoodBudget);
	}

	private void SetWoodBudget(GameObject go, float budget)
	{
		if (!(budget < 0f))
		{
			Budget.m_WoodBudget = Mathf.Clamp(Mathf.RoundToInt(budget), 0, Budget.UNLIMITED_MATERIAL_BUDGET);
			m_InputField.text = Utils.FormatMaterialBudget(Budget.m_WoodBudget);
			MaybeSnapshot();
		}
	}

	private void RestoreWoodBudget(GameObject go)
	{
		m_InputField.text = Utils.FormatMaterialBudget(Budget.m_WoodBudget);
	}

	private void AddSteelBudget(GameObject go, float deltaBudget)
	{
		Budget.m_SteelBudget += Mathf.RoundToInt(deltaBudget);
		if (Budget.m_SteelBudget < 0)
		{
			Budget.m_SteelBudget = Budget.UNLIMITED_MATERIAL_BUDGET;
		}
		if (Budget.m_SteelBudget > Budget.UNLIMITED_MATERIAL_BUDGET)
		{
			Budget.m_SteelBudget = 0;
		}
		SetSteelBudget(null, Budget.m_SteelBudget);
	}

	private void SetSteelBudget(GameObject go, float budget)
	{
		if (!(budget < 0f))
		{
			Budget.m_SteelBudget = Mathf.Clamp(Mathf.RoundToInt(budget), 0, Budget.UNLIMITED_MATERIAL_BUDGET);
			m_InputField.text = Utils.FormatMaterialBudget(Budget.m_SteelBudget);
			MaybeSnapshot();
		}
	}

	private void RestoreSteelBudget(GameObject go)
	{
		m_InputField.text = Utils.FormatMaterialBudget(Budget.m_SteelBudget);
	}

	private void AddHydraulicBudget(GameObject go, float deltaBudget)
	{
		Budget.m_HydraulicBudget += Mathf.RoundToInt(deltaBudget);
		if (Budget.m_HydraulicBudget < 0)
		{
			Budget.m_HydraulicBudget = Budget.UNLIMITED_MATERIAL_BUDGET;
		}
		if (Budget.m_HydraulicBudget > Budget.UNLIMITED_MATERIAL_BUDGET)
		{
			Budget.m_HydraulicBudget = 0;
		}
		SetHydraulicBudget(null, Budget.m_HydraulicBudget);
	}

	private void SetHydraulicBudget(GameObject go, float budget)
	{
		if (!(budget < 0f))
		{
			Budget.m_HydraulicBudget = Mathf.Clamp(Mathf.RoundToInt(budget), 0, Budget.UNLIMITED_MATERIAL_BUDGET);
			m_InputField.text = Utils.FormatMaterialBudget(Budget.m_HydraulicBudget);
			MaybeSnapshot();
		}
	}

	private void RestoreHydraulicBudget(GameObject go)
	{
		m_InputField.text = Utils.FormatMaterialBudget(Budget.m_HydraulicBudget);
	}

	private void AddRopeBudget(GameObject go, float deltaBudget)
	{
		Budget.m_RopeBudget += Mathf.RoundToInt(deltaBudget);
		if (Budget.m_RopeBudget < 0)
		{
			Budget.m_RopeBudget = Budget.UNLIMITED_MATERIAL_BUDGET;
		}
		if (Budget.m_RopeBudget > Budget.UNLIMITED_MATERIAL_BUDGET)
		{
			Budget.m_RopeBudget = 0;
		}
		SetRopeBudget(null, Budget.m_RopeBudget);
	}

	private void SetRopeBudget(GameObject go, float budget)
	{
		if (!(budget < 0f))
		{
			Budget.m_RopeBudget = Mathf.Clamp(Mathf.RoundToInt(budget), 0, Budget.UNLIMITED_MATERIAL_BUDGET);
			m_InputField.text = Utils.FormatMaterialBudget(Budget.m_RopeBudget);
			MaybeSnapshot();
		}
	}

	private void RestoreRopeBudget(GameObject go)
	{
		m_InputField.text = Utils.FormatMaterialBudget(Budget.m_RopeBudget);
	}

	private void AddCableBudget(GameObject go, float deltaBudget)
	{
		Budget.m_CableBudget += Mathf.RoundToInt(deltaBudget);
		if (Budget.m_CableBudget < 0)
		{
			Budget.m_CableBudget = Budget.UNLIMITED_MATERIAL_BUDGET;
		}
		if (Budget.m_CableBudget > Budget.UNLIMITED_MATERIAL_BUDGET)
		{
			Budget.m_CableBudget = 0;
		}
		SetCableBudget(null, Budget.m_CableBudget);
	}

	private void SetCableBudget(GameObject go, float budget)
	{
		if (!(budget < 0f))
		{
			Budget.m_CableBudget = Mathf.Clamp(Mathf.RoundToInt(budget), 0, Budget.UNLIMITED_MATERIAL_BUDGET);
			m_InputField.text = Utils.FormatMaterialBudget(Budget.m_CableBudget);
			MaybeSnapshot();
		}
	}

	private void RestoreCableBudget(GameObject go)
	{
		m_InputField.text = Utils.FormatMaterialBudget(Budget.m_CableBudget);
	}

	private void AddSpringBudget(GameObject go, float deltaBudget)
	{
		Budget.m_SpringBudget += Mathf.RoundToInt(deltaBudget);
		if (Budget.m_SpringBudget < 0)
		{
			Budget.m_SpringBudget = Budget.UNLIMITED_MATERIAL_BUDGET;
		}
		if (Budget.m_SpringBudget > Budget.UNLIMITED_MATERIAL_BUDGET)
		{
			Budget.m_SpringBudget = 0;
		}
		SetSpringBudget(null, Budget.m_SpringBudget);
	}

	private void SetSpringBudget(GameObject go, float budget)
	{
		if (!(budget < 0f))
		{
			Budget.m_SpringBudget = Mathf.Clamp(Mathf.RoundToInt(budget), 0, Budget.UNLIMITED_MATERIAL_BUDGET);
			m_InputField.text = Utils.FormatMaterialBudget(Budget.m_SpringBudget);
			MaybeSnapshot();
		}
	}

	private void RestoreSpringBudget(GameObject go)
	{
		m_InputField.text = Utils.FormatMaterialBudget(Budget.m_SpringBudget);
	}

	private void AddPillarBudget(GameObject go, float deltaBudget)
	{
		Budget.m_PillarBudget += Mathf.RoundToInt(deltaBudget);
		if (Budget.m_PillarBudget < 0)
		{
			Budget.m_PillarBudget = Budget.UNLIMITED_MATERIAL_BUDGET;
		}
		if (Budget.m_PillarBudget > Budget.UNLIMITED_MATERIAL_BUDGET)
		{
			Budget.m_PillarBudget = 0;
		}
		SetPillarBudget(null, Budget.m_PillarBudget);
	}

	private void SetPillarBudget(GameObject go, float budget)
	{
		if (!(budget < 0f))
		{
			Budget.m_PillarBudget = Mathf.Clamp(Mathf.RoundToInt(budget), 0, Budget.UNLIMITED_MATERIAL_BUDGET);
			m_InputField.text = Utils.FormatMaterialBudget(Budget.m_PillarBudget);
			MaybeSnapshot();
		}
	}

	private void RestorePillarBudget(GameObject go)
	{
		m_InputField.text = Utils.FormatMaterialBudget(Budget.m_PillarBudget);
	}

	private void AddBounciness(GameObject go, float delta)
	{
		if ((bool)go)
		{
			CustomShape component = go.GetComponent<CustomShape>();
			if ((bool)component)
			{
				SetBounciness(go, component.m_Bounciness + delta);
			}
		}
	}

	private void SetBounciness(GameObject go, float value)
	{
		if ((bool)go)
		{
			CustomShape component = go.GetComponent<CustomShape>();
			if ((bool)component)
			{
				component.m_Bounciness = Mathf.Clamp01(value);
				m_InputField.text = Utils.FormatTwoDecimalPlaces(component.m_Bounciness);
				MaybeSnapshot();
			}
		}
	}

	private void RestoreBounciness(GameObject go)
	{
		if ((bool)go)
		{
			CustomShape component = go.GetComponent<CustomShape>();
			if ((bool)component)
			{
				m_InputField.text = Utils.FormatTwoDecimalPlaces(component.m_Bounciness);
			}
		}
	}

	private void AddPinMotorStrength(GameObject go, float delta)
	{
		if ((bool)go)
		{
			CustomShape component = go.GetComponent<CustomShape>();
			if ((bool)component)
			{
				SetPinMotorStrength(go, component.m_PinMotorStrength + delta);
			}
		}
	}

	private void SetPinMotorStrength(GameObject go, float value)
	{
		if ((bool)go)
		{
			CustomShape component = go.GetComponent<CustomShape>();
			if ((bool)component)
			{
				component.m_PinMotorStrength = Mathf.Clamp(value, 0f, CustomShapes.MAX_PIN_MOTOR_STRENGTH);
				m_InputField.text = Utils.FormatOneDecimalPlace(component.m_PinMotorStrength);
				CustomShapes.UpdateCustomShapeMinimumStrengthHint(component);
				MaybeSnapshot();
			}
		}
	}

	private void RestorePinMotorStrength(GameObject go)
	{
		if ((bool)go)
		{
			CustomShape component = go.GetComponent<CustomShape>();
			if ((bool)component)
			{
				m_InputField.text = Utils.FormatOneDecimalPlace(component.m_PinMotorStrength);
			}
		}
	}

	private void AddPinTargetVelocity(GameObject go, float delta)
	{
		if ((bool)go)
		{
			CustomShape component = go.GetComponent<CustomShape>();
			if ((bool)component)
			{
				SetPinTargetVelocity(go, component.m_PinTargetVelocity + delta);
			}
		}
	}

	private void SetPinTargetVelocity(GameObject go, float value)
	{
		if ((bool)go)
		{
			CustomShape component = go.GetComponent<CustomShape>();
			if ((bool)component)
			{
				component.m_PinTargetVelocity = Mathf.Clamp(value, 0f - CustomShapes.MAX_PIN_TARGET_VELOCITY, CustomShapes.MAX_PIN_TARGET_VELOCITY);
				m_InputField.text = Utils.FormatOneDecimalPlace(component.m_PinTargetVelocity);
				CustomShapes.UpdateCustomShapeMinimumStrengthHint(component);
				MaybeSnapshot();
			}
		}
	}

	private void RestorePinTargetVelocity(GameObject go)
	{
		if ((bool)go)
		{
			CustomShape component = go.GetComponent<CustomShape>();
			if ((bool)component)
			{
				m_InputField.text = Utils.FormatOneDecimalPlace(component.m_PinTargetVelocity);
			}
		}
	}

	private float GetDelta(GameObject go)
	{
		return GetUnmodifiedDelta(go, m_Format);
	}

	private float GetUnmodifiedDelta(GameObject go, SandboxInputFieldFormat format)
	{
		switch (format)
		{
		case SandboxInputFieldFormat.WIDTH:
			if (!UseHalfMeterWidthDelta(go.GetComponent<SandboxItem>().m_Type))
			{
				return GameGrid.m_Spacing;
			}
			return 0.5f;
		case SandboxInputFieldFormat.HEIGHT:
			if (!UseHalfMeterHeightDelta(go.GetComponent<SandboxItem>().m_Type))
			{
				return GameGrid.m_Spacing;
			}
			return 0.5f;
		case SandboxInputFieldFormat.HEIGHT_FOG_START_MIN:
		case SandboxInputFieldFormat.HEIGHT_FOG_START_MAX:
		case SandboxInputFieldFormat.HEIGHT_FOG_END:
			return 0.1f;
		case SandboxInputFieldFormat.POSX:
		case SandboxInputFieldFormat.POSY:
		case SandboxInputFieldFormat.POSZ:
		case SandboxInputFieldFormat.NUDGE:
			return GameGrid.m_Spacing;
		case SandboxInputFieldFormat.ROT:
		case SandboxInputFieldFormat.NUM_SEGMENTS:
		case SandboxInputFieldFormat.SCALE:
		case SandboxInputFieldFormat.SCALEX:
		case SandboxInputFieldFormat.SCALEY:
		case SandboxInputFieldFormat.SCALEZ:
		case SandboxInputFieldFormat.TILING:
		case SandboxInputFieldFormat.CUSTOMSHAPE_MESH_SCALE:
		case SandboxInputFieldFormat.ROLL:
		case SandboxInputFieldFormat.PITCH:
			return 1f;
		case SandboxInputFieldFormat.SPEED:
		case SandboxInputFieldFormat.WEIGHT:
			return 0.5f;
		case SandboxInputFieldFormat.TIME_DELAY:
		case SandboxInputFieldFormat.BRAKING_FORCE_MULTIPLIER:
		case SandboxInputFieldFormat.ACCELERATION:
		case SandboxInputFieldFormat.DESIRED_ACCELERATION:
		case SandboxInputFieldFormat.SHOCKS_MULTIPLIER:
		case SandboxInputFieldFormat.BOUNCINESS:
		case SandboxInputFieldFormat.THICKNESS:
			return 0.1f;
		case SandboxInputFieldFormat.ROAD_BUDGET:
		case SandboxInputFieldFormat.WOOD_BUDGET:
		case SandboxInputFieldFormat.STEEL_BUDGET:
		case SandboxInputFieldFormat.HYDRAULIC_BUDGET:
		case SandboxInputFieldFormat.ROPE_BUDGET:
		case SandboxInputFieldFormat.CABLE_BUDGET:
		case SandboxInputFieldFormat.BUNGIE_ROPE_BUDGET:
		case SandboxInputFieldFormat.SPRING_BUDGET:
		case SandboxInputFieldFormat.REINFORCED_ROAD_BUDGET:
		case SandboxInputFieldFormat.PILLAR_BUDGET:
			return 1f;
		case SandboxInputFieldFormat.BUDGET:
			return 1000f;
		case SandboxInputFieldFormat.PIN_MOTOR_STRENGTH:
			return 0.5f;
		case SandboxInputFieldFormat.PIN_TARGET_VELOCITY:
			return 0.5f;
		default:
			Debug.LogWarningFormat("Unsupported input field format {0}", m_Format.ToString());
			return 0f;
		}
	}

	private bool UseHalfMeterWidthDelta(SandboxItemType sandboxItemType)
	{
		if (sandboxItemType != SandboxItemType.PLATFORM)
		{
			return sandboxItemType == SandboxItemType.BUILD_ZONE;
		}
		return true;
	}

	private bool UseHalfMeterHeightDelta(SandboxItemType sandboxItemType)
	{
		if (sandboxItemType != SandboxItemType.PLATFORM && sandboxItemType != SandboxItemType.RAMP)
		{
			return sandboxItemType == SandboxItemType.BUILD_ZONE;
		}
		return true;
	}

	private void SetInputType(SandboxInputFieldFormat format)
	{
		switch (format)
		{
		case SandboxInputFieldFormat.POSX:
		case SandboxInputFieldFormat.POSY:
		case SandboxInputFieldFormat.ROT:
		case SandboxInputFieldFormat.WIDTH:
		case SandboxInputFieldFormat.HEIGHT:
		case SandboxInputFieldFormat.TIME_DELAY:
		case SandboxInputFieldFormat.POSZ:
		case SandboxInputFieldFormat.SPEED:
		case SandboxInputFieldFormat.WEIGHT:
		case SandboxInputFieldFormat.BRAKING_FORCE_MULTIPLIER:
		case SandboxInputFieldFormat.ACCELERATION:
		case SandboxInputFieldFormat.DESIRED_ACCELERATION:
		case SandboxInputFieldFormat.SHOCKS_MULTIPLIER:
		case SandboxInputFieldFormat.SCALE:
		case SandboxInputFieldFormat.SCALEX:
		case SandboxInputFieldFormat.SCALEY:
		case SandboxInputFieldFormat.SCALEZ:
		case SandboxInputFieldFormat.BOUNCINESS:
		case SandboxInputFieldFormat.PIN_MOTOR_STRENGTH:
		case SandboxInputFieldFormat.PIN_TARGET_VELOCITY:
		case SandboxInputFieldFormat.TILING:
		case SandboxInputFieldFormat.NUDGE:
		case SandboxInputFieldFormat.CUSTOMSHAPE_MESH_SCALE:
		case SandboxInputFieldFormat.THICKNESS:
		case SandboxInputFieldFormat.ROLL:
		case SandboxInputFieldFormat.PITCH:
		case SandboxInputFieldFormat.HEIGHT_FOG_START_MIN:
		case SandboxInputFieldFormat.HEIGHT_FOG_START_MAX:
		case SandboxInputFieldFormat.HEIGHT_FOG_END:
			m_InputField.contentType = TMP_InputField.ContentType.DecimalNumber;
			break;
		case SandboxInputFieldFormat.NUM_SEGMENTS:
		case SandboxInputFieldFormat.BUDGET:
		case SandboxInputFieldFormat.ROAD_BUDGET:
		case SandboxInputFieldFormat.WOOD_BUDGET:
		case SandboxInputFieldFormat.STEEL_BUDGET:
		case SandboxInputFieldFormat.HYDRAULIC_BUDGET:
		case SandboxInputFieldFormat.ROPE_BUDGET:
		case SandboxInputFieldFormat.CABLE_BUDGET:
		case SandboxInputFieldFormat.SPRING_BUDGET:
		case SandboxInputFieldFormat.REINFORCED_ROAD_BUDGET:
		case SandboxInputFieldFormat.PILLAR_BUDGET:
			m_InputField.contentType = TMP_InputField.ContentType.IntegerNumber;
			break;
		default:
			m_InputField.contentType = TMP_InputField.ContentType.Standard;
			break;
		}
	}

	private void SetCallbacks(SandboxInputFieldFormat format)
	{
		switch (format)
		{
		case SandboxInputFieldFormat.POSX:
			SetCallbacks(AddPosX, SetPosX, RestorePosX);
			break;
		case SandboxInputFieldFormat.POSY:
			SetCallbacks(AddPosY, SetPosY, RestorePosY);
			break;
		case SandboxInputFieldFormat.POSZ:
			SetCallbacks(AddPosZ, SetPosZ, RestorePosZ);
			break;
		case SandboxInputFieldFormat.SCALE:
			SetCallbacks(AddScale, SetScale, RestoreScale);
			break;
		case SandboxInputFieldFormat.CUSTOMSHAPE_MESH_SCALE:
			SetCallbacks(AddCustomShapeMeshScale, SetCustomShapeMeshScale, RestoreCustomShapeMeshScale);
			break;
		case SandboxInputFieldFormat.SCALEX:
			SetCallbacks(AddScaleX, SetScaleX, RestoreScaleX);
			break;
		case SandboxInputFieldFormat.SCALEY:
			SetCallbacks(AddScaleY, SetScaleY, RestoreScaleY);
			break;
		case SandboxInputFieldFormat.SCALEZ:
			SetCallbacks(AddScaleZ, SetScaleZ, RestoreScaleZ);
			break;
		case SandboxInputFieldFormat.TILING:
			SetCallbacks(AddTiling, SetTiling, RestoreTiling);
			break;
		case SandboxInputFieldFormat.NUDGE:
			SetCallbacks(AddNudge, SetNudge, RestoreNudge);
			break;
		case SandboxInputFieldFormat.WIDTH:
			SetCallbacks(AddWidth, SetWidth, RestoreWidth);
			break;
		case SandboxInputFieldFormat.HEIGHT:
			SetCallbacks(AddHeight, SetHeight, RestoreHeight);
			break;
		case SandboxInputFieldFormat.HEIGHT_FOG_START_MIN:
			SetCallbacks(AddHeightFogStartMin, SetHeightFogStartMin, RestoreHeightFogStartMin);
			break;
		case SandboxInputFieldFormat.HEIGHT_FOG_START_MAX:
			SetCallbacks(AddHeightFogStartMax, SetHeightFogStartMax, RestoreHeightFogStartMax);
			break;
		case SandboxInputFieldFormat.HEIGHT_FOG_END:
			SetCallbacks(AddHeightFogEnd, SetHeightFogEnd, RestoreHeightFogEnd);
			break;
		case SandboxInputFieldFormat.ROT:
			SetCallbacks(AddRot, SetRot, RestoreRot);
			break;
		case SandboxInputFieldFormat.ROLL:
			SetCallbacks(AddRoll, SetRoll, RestoreRoll);
			break;
		case SandboxInputFieldFormat.PITCH:
			SetCallbacks(AddPitch, SetPitch, RestorePitch);
			break;
		case SandboxInputFieldFormat.TIME_DELAY:
			SetCallbacks(AddTimeDelay, SetTimeDelay, RestoreTimeDelay);
			break;
		case SandboxInputFieldFormat.SPEED:
			SetCallbacks(AddSpeed, SetSpeed, RestoreSpeed);
			break;
		case SandboxInputFieldFormat.WEIGHT:
			SetCallbacks(AddMass, SetMass, RestoreMass);
			break;
		case SandboxInputFieldFormat.BRAKING_FORCE_MULTIPLIER:
			SetCallbacks(AddBrakingForceMultiplier, SetBrakingForceMultiplier, RestoreBrakingForceMultiplier);
			break;
		case SandboxInputFieldFormat.ACCELERATION:
			SetCallbacks(AddAcceleration, SetAcceleration, RestoreAcceleration);
			break;
		case SandboxInputFieldFormat.DESIRED_ACCELERATION:
			SetCallbacks(AddDesiredAcceleration, SetDesiredAcceleration, RestoreDesiredAcceleration);
			break;
		case SandboxInputFieldFormat.SHOCKS_MULTIPLIER:
			SetCallbacks(AddShocksMultiplier, SetShocksMultiplier, RestoreShocksMultiplier);
			break;
		case SandboxInputFieldFormat.NUM_SEGMENTS:
			SetCallbacks(AddNumSegments, SetNumSegments, RestoreNumSegments);
			break;
		case SandboxInputFieldFormat.BUDGET:
			SetCallbacks(AddBudget, SetBudget, RestoreBudget);
			break;
		case SandboxInputFieldFormat.ROAD_BUDGET:
			SetCallbacks(AddRoadBudget, SetRoadBudget, RestoreRoadBudget);
			break;
		case SandboxInputFieldFormat.WOOD_BUDGET:
			SetCallbacks(AddWoodBudget, SetWoodBudget, RestoreWoodBudget);
			break;
		case SandboxInputFieldFormat.STEEL_BUDGET:
			SetCallbacks(AddSteelBudget, SetSteelBudget, RestoreSteelBudget);
			break;
		case SandboxInputFieldFormat.HYDRAULIC_BUDGET:
			SetCallbacks(AddHydraulicBudget, SetHydraulicBudget, RestoreHydraulicBudget);
			break;
		case SandboxInputFieldFormat.ROPE_BUDGET:
			SetCallbacks(AddRopeBudget, SetRopeBudget, RestoreRopeBudget);
			break;
		case SandboxInputFieldFormat.CABLE_BUDGET:
			SetCallbacks(AddCableBudget, SetCableBudget, RestoreCableBudget);
			break;
		case SandboxInputFieldFormat.SPRING_BUDGET:
			SetCallbacks(AddSpringBudget, SetSpringBudget, RestoreSpringBudget);
			break;
		case SandboxInputFieldFormat.PILLAR_BUDGET:
			SetCallbacks(AddPillarBudget, SetPillarBudget, RestorePillarBudget);
			break;
		case SandboxInputFieldFormat.REINFORCED_ROAD_BUDGET:
			SetCallbacks(null, null, null);
			break;
		case SandboxInputFieldFormat.BOUNCINESS:
			SetCallbacks(AddBounciness, SetBounciness, RestoreBounciness);
			break;
		case SandboxInputFieldFormat.PIN_MOTOR_STRENGTH:
			SetCallbacks(AddPinMotorStrength, SetPinMotorStrength, RestorePinMotorStrength);
			break;
		case SandboxInputFieldFormat.PIN_TARGET_VELOCITY:
			SetCallbacks(AddPinTargetVelocity, SetPinTargetVelocity, RestorePinTargetVelocity);
			break;
		case SandboxInputFieldFormat.THICKNESS:
			SetCallbacks(AddThickness, SetThickness, RestoreThickness);
			break;
		case SandboxInputFieldFormat.BUNGIE_ROPE_BUDGET:
			break;
		}
	}

	private void UpdatePolygonShapes(GameObject go)
	{
		SandboxItem component = go.GetComponent<SandboxItem>();
		if (component != null)
		{
			component.UpdatePolygonShapes();
		}
	}

	private void MaybeSnapshot()
	{
		if (!m_ExternalContinuousHoldActive && !m_ContinuousHoldActive)
		{
			SandboxUndo.SnapShot();
		}
	}

	private void MarkOutlineDirty(GameObject go)
	{
		SandboxItem component = go.GetComponent<SandboxItem>();
		if (component != null)
		{
			component.SetOutlineDirty(dirty: true);
		}
	}

	private GameObject GetSelectedGameObject()
	{
		GameObject gameObject = ((SandboxSelectionSet.m_Items.Count > 0) ? SandboxSelectionSet.m_Items[0].gameObject : null);
		if (gameObject != null && gameObject.GetComponent<Vehicle>() != null)
		{
			SandboxTapeCheckpoint componentInParent = base.gameObject.GetComponentInParent<SandboxTapeCheckpoint>();
			if (componentInParent != null)
			{
				gameObject = componentInParent.m_Checkpoint.gameObject;
			}
		}
		return gameObject;
	}

	private void SetPlatformHeight(Platform platform, float height)
	{
		platform.SetHeight(Mathf.Clamp(height, Platforms.MIN_HEIGHT, Platforms.MAX_HEIGHT));
		platform.RefreshMesh();
		if (m_InputField.gameObject.activeInHierarchy)
		{
			m_InputField.text = Utils.FormatDistance(platform.m_Height);
			if (m_LinkedSlider != null)
			{
				GameUI.m_Instance.m_SandboxEditPlatform.SkipInputFieldUpdateFromSlider();
				m_LinkedSlider.SetValue(platform.m_Height);
			}
			MaybeSnapshot();
		}
	}

	private void SetRampHeight(Ramp ramp, float height)
	{
		ramp.m_Height = Mathf.Clamp(height, Ramps.MIN_HEIGHT, Ramps.MAX_HEIGHT);
		ramp.RefreshMesh();
		if (m_InputField.gameObject.activeInHierarchy)
		{
			m_InputField.text = Utils.FormatDistance(ramp.m_Height);
			if (m_LinkedSlider != null)
			{
				GameUI.m_Instance.m_SandboxEditRamp.SkipInputFieldUpdateFromSlider();
				m_LinkedSlider.SetValue(ramp.m_Height);
			}
			MaybeSnapshot();
		}
	}

	private void SetVehicleStopTriggerHeight(VehicleStopTrigger trigger, float height)
	{
		trigger.m_Height = Mathf.Clamp(height, VehicleStopTriggers.MIN_HEIGHT, VehicleStopTriggers.MAX_HEIGHT);
		trigger.SetPoleScaleForHeight(trigger.m_Height);
		if (m_InputField.gameObject.activeInHierarchy)
		{
			m_InputField.text = Utils.FormatDistance(trigger.m_Height);
			if (m_LinkedSlider != null)
			{
				GameUI.m_Instance.m_SandboxEditVehicleStopTrigger.SkipInputFieldUpdateFromSlider();
				m_LinkedSlider.SetValue(trigger.m_Height);
			}
			MaybeSnapshot();
			SandboxItems.ResolveOverlappingFloatingText();
		}
	}

	private void SetWaterBlockHeight(WaterBlock waterBlock, float height)
	{
		waterBlock.m_Height = Mathf.Clamp(height, WaterBlocks.MIN_HEIGHT, waterBlock.GetMaxHeight());
		if ((bool)waterBlock.m_LeftTerrain)
		{
			waterBlock.m_LeftTerrain.m_RightEdgeWaterHeight = waterBlock.m_Height;
		}
		waterBlock.RefreshPosition();
		if (m_InputField.gameObject.activeInHierarchy)
		{
			m_InputField.text = Utils.FormatDistance(waterBlock.m_Height);
			if (m_LinkedSlider != null)
			{
				GameUI.m_Instance.m_SandboxEditWater.SkipInputFieldUpdateFromSlider();
				m_LinkedSlider.SetValue(waterBlock.m_Height);
			}
		}
	}

	private void SetPillarHeight(Pillar pillar, float height)
	{
		pillar.SetHeight(Mathf.Clamp(height, Pillars.MIN_HEIGHT, Pillars.MAX_HEIGHT));
		if (m_InputField.gameObject.activeInHierarchy)
		{
			m_InputField.text = Utils.FormatDistance(pillar.m_Height);
			if (m_LinkedSlider != null)
			{
				GameUI.m_Instance.m_SandboxEditPillar.SkipInputFieldUpdateFromSlider();
				m_LinkedSlider.SetValue(pillar.m_Height);
			}
			MaybeSnapshot();
		}
	}

	private void SetBuildZoneHeight(BuildZone buildZone, float height)
	{
		height = Mathf.Clamp(height, BuildZones.MIN_HEIGHT, float.MaxValue);
		buildZone.SetBounds(buildZone.GetPosition(), new Vector2(buildZone.GetSize().x, height));
		if (m_InputField.gameObject.activeInHierarchy)
		{
			m_InputField.text = Utils.FormatDistance(buildZone.GetSize().y);
			if (m_LinkedSlider != null)
			{
				GameUI.m_Instance.m_SandboxEditBuildZone.SkipInputFieldUpdateFromSlider();
				m_LinkedSlider.SetValue(buildZone.GetSize().y);
			}
			buildZone.PositionControlPoints();
			MaybeSnapshot();
		}
	}

	private void SetTerrainIslandHeight(TerrainIsland terrainIsland, float height)
	{
		height = Mathf.Clamp(height, TerrainIslands.MIN_HEIGHT, TerrainIslands.MAX_HEIGHT);
		if (GameGrid.IsGridAligned(height))
		{
			height += TerrainIslands.GRID_ALIGN_OFFSET;
		}
		terrainIsland.SetHeight(height);
		if (m_InputField.gameObject.activeInHierarchy)
		{
			m_InputField.text = terrainIsland.FormatHeight();
			if (m_LinkedSlider != null)
			{
				GameUI.m_Instance.m_SandboxEditTerrain.SkipInputFieldUpdateFromSlider();
				m_LinkedSlider.SetValue(terrainIsland.GetHeight());
			}
			MaybeSnapshot();
		}
	}

	private void MaybeUpdateForUniformScaleFlag(GameObject selectedGameObject, float value)
	{
		if (!(selectedGameObject != null))
		{
			return;
		}
		Decor component = selectedGameObject.GetComponent<Decor>();
		if ((bool)component && component.m_UniformScale)
		{
			if (m_Format == SandboxInputFieldFormat.SCALEX)
			{
				GameUI.m_Instance.m_SandboxEditDecor.m_SliderScaleY.m_SandboxInputField.SetScaleY(selectedGameObject, value);
				GameUI.m_Instance.m_SandboxEditDecor.m_SliderScaleZ.m_SandboxInputField.SetScaleZ(selectedGameObject, value);
			}
			else if (m_Format == SandboxInputFieldFormat.SCALEY)
			{
				GameUI.m_Instance.m_SandboxEditDecor.m_SliderScaleX.m_SandboxInputField.SetScaleX(selectedGameObject, value);
				GameUI.m_Instance.m_SandboxEditDecor.m_SliderScaleZ.m_SandboxInputField.SetScaleZ(selectedGameObject, value);
			}
			else if (m_Format == SandboxInputFieldFormat.SCALEZ)
			{
				GameUI.m_Instance.m_SandboxEditDecor.m_SliderScaleX.m_SandboxInputField.SetScaleX(selectedGameObject, value);
				GameUI.m_Instance.m_SandboxEditDecor.m_SliderScaleY.m_SandboxInputField.SetScaleY(selectedGameObject, value);
			}
			if ((m_Format == SandboxInputFieldFormat.SCALEX || m_Format == SandboxInputFieldFormat.SCALEY || m_Format == SandboxInputFieldFormat.SCALEZ) && SandboxUndo.m_States.Count > 2)
			{
				SandboxUndo.m_States.RemoveAt(SandboxUndo.m_States.Count - 2);
				SandboxUndo.m_States.RemoveAt(SandboxUndo.m_States.Count - 2);
			}
		}
		Rock component2 = selectedGameObject.GetComponent<Rock>();
		if ((bool)component2 && component2.m_UniformScale)
		{
			if (m_Format == SandboxInputFieldFormat.SCALEX)
			{
				GameUI.m_Instance.m_SandboxEditRock.m_SliderScaleY.m_SandboxInputField.SetScaleY(selectedGameObject, value);
				GameUI.m_Instance.m_SandboxEditRock.m_SliderScaleZ.m_SandboxInputField.SetScaleZ(selectedGameObject, value);
			}
			else if (m_Format == SandboxInputFieldFormat.SCALEY)
			{
				GameUI.m_Instance.m_SandboxEditRock.m_SliderScaleX.m_SandboxInputField.SetScaleX(selectedGameObject, value);
				GameUI.m_Instance.m_SandboxEditRock.m_SliderScaleZ.m_SandboxInputField.SetScaleZ(selectedGameObject, value);
			}
			else if (m_Format == SandboxInputFieldFormat.SCALEZ)
			{
				GameUI.m_Instance.m_SandboxEditRock.m_SliderScaleX.m_SandboxInputField.SetScaleX(selectedGameObject, value);
				GameUI.m_Instance.m_SandboxEditRock.m_SliderScaleY.m_SandboxInputField.SetScaleY(selectedGameObject, value);
			}
			if ((m_Format == SandboxInputFieldFormat.SCALEX || m_Format == SandboxInputFieldFormat.SCALEY || m_Format == SandboxInputFieldFormat.SCALEZ) && SandboxUndo.m_States.Count > 2)
			{
				SandboxUndo.m_States.RemoveAt(SandboxUndo.m_States.Count - 2);
				SandboxUndo.m_States.RemoveAt(SandboxUndo.m_States.Count - 2);
			}
		}
	}

	private void OnGamepadInputField()
	{
		GamepadVirtualKeyboard.MaybeOpenVirtualKeyboard(GetStrippedValue(m_InputField.text), m_InputField.characterLimit, string.Empty, multiline: false, OnInputFieldEntered);
	}

	private void OnInputFieldEntered(string text)
	{
		if (!string.IsNullOrEmpty(text))
		{
			m_InputField.text = text;
			OnEndEdit();
		}
	}

	private string GetStrippedValue(string text)
	{
		string text2 = text.Trim(' ', '\t', '°', 'º', '²', '$', 's', '%', 'm', '/', 'P', 'g');
		if (m_Format != SandboxInputFieldFormat.BUDGET)
		{
			text2 = text2.Replace(',', '.');
		}
		int num = text2.IndexOf('.');
		if (num != -1)
		{
			int num2 = text2.IndexOf('.', num + 1);
			if (num2 != -1)
			{
				text2 = text2.Remove(num2, 1);
			}
		}
		return text2;
	}
}
