using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Landfall.TABS;
using Landfall.TABS.UnitEditor;
using Landfall.TABS_Input;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UnitEditorColorWheel : MonoBehaviour
{
	private class SpawnedWrapper
	{
		public UnitEditorEquipedColorCell cell;

		public float leftAngle;

		public float midAngle;

		public float angle;

		public SpawnedWrapper(UnitEditorEquipedColorCell obj, float leftAngle, float midAngle, float angle)
		{
			cell = obj;
			this.leftAngle = leftAngle;
			this.midAngle = midAngle;
			this.angle = angle;
		}
	}

	public float clickForce = 5f;

	public string enterRef;

	public string clickRef;

	public Camera cam;

	public GameObject shard;

	[FormerlySerializedAs("catagoryShard")]
	public GameObject categoryShard;

	[FormerlySerializedAs("catagoryShardInverted")]
	public GameObject categoryShardInverted;

	public Transform middlePiece;

	public UnitEditorColorWheelFlash colorWheelFlash;

	public Image colorPreviewImage;

	public float cursorMagnitude;

	public UnitEditorColorPalette ColorPalette;

	public UnitEditorColorWheelCursor cursor;

	private UnitEditorEquipedClothing equippedClothing;

	private List<SpawnedWrapper> SpawnedObjects = new List<SpawnedWrapper>();

	private SpawnedWrapper lastWrapper;

	private SoundPlayer m_soundPlayer;

	private UnitEditorManager.EquipedWrapper equippedWrapper;

	private int subMeshIndex;

	private bool lastFrameExit;

	private ColorWheelMode colorWheelMode;

	private float colorPaletteAnglePerStep;

	private float colorPaletteStepCount;

	private const float colorRadius = 7.2f;

	private GameObject[] colorParentCategoriesSpawnedShards;

	private GameObject[] colorParentCategorySpawnedShards;

	private GameObject[] colorCategorySpawnedShard;

	private UnitEditorColorPalette.ParentCatagories currentParentCategories;

	private UnitEditorColorPalette.ColorPaletteCatagory currentCategory;

	private bool showingTeamColors;

	private Team showingTeamColorsForTeam;

	private PlayerActions playerActions;

	private int lastSelectedCategory = -1;

	public int LastSelectedCategory => lastSelectedCategory;

	public ColorWheelMode WheelMode => colorWheelMode;

	public bool LastFrameExit
	{
		get
		{
			return lastFrameExit;
		}
		set
		{
			lastFrameExit = value;
		}
	}

	public event Action<ColorWheelMode> ColorWheelStateChanged;

	private void Awake()
	{
		m_soundPlayer = ServiceLocator.GetService<SoundPlayer>();
		playerActions = PlayerActions.Instance;
	}

	private void ShowColorCategoryState(UnitEditorColorPalette.ColorPaletteCatagory category)
	{
		UpdateStateObjects(ColorWheelMode.ColorCategory);
		currentCategory = category;
		showingTeamColors = false;
		int num = 0;
		if (category.Colors != null)
		{
			num = category.Colors.Length;
			if (num == 0)
			{
				showingTeamColors = true;
				num = category.TeamColors.Length;
			}
		}
		colorCategorySpawnedShard = ClearSpawnedArray(colorCategorySpawnedShard, num);
		float num2 = 360f / (float)num;
		UnitEditorManager unitEditorManager = UnityEngine.Object.FindObjectOfType<UnitEditorManager>();
		showingTeamColorsForTeam = unitEditorManager.currentTeam;
		for (int i = 0; i < num; i++)
		{
			float z = (float)i * num2;
			GameObject gameObject = UnityEngine.Object.Instantiate(shard, base.transform);
			gameObject.SetActive(value: true);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, z);
			gameObject.transform.localScale = Vector3.zero;
			Image[] componentsInChildren = gameObject.GetComponentsInChildren<Image>();
			if (!showingTeamColors)
			{
				if (category.Colors != null)
				{
					componentsInChildren[0].color = category.Colors[i].m_color;
				}
			}
			else
			{
				componentsInChildren[0].color = category.TeamColors[i].GetColor(showingTeamColorsForTeam);
			}
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				componentsInChildren[j].fillAmount = num2 / 360f;
			}
			colorCategorySpawnedShard[i] = gameObject;
		}
		StateChange();
	}

	private void StateChange()
	{
		lastSelectedCategory = -1;
		colorWheelFlash.SetLastSibling();
		middlePiece.SetAsLastSibling();
	}

	private void UpdateStateObjects(ColorWheelMode newState)
	{
		colorWheelMode = newState;
		if (newState != ColorWheelMode.EquipmentColors)
		{
			for (int i = 0; i < SpawnedObjects.Count; i++)
			{
				SpawnedObjects[i].cell.gameObject.SetActive(value: false);
			}
		}
		if (newState != ColorWheelMode.ColorParentCategories && colorParentCategoriesSpawnedShards != null)
		{
			for (int j = 0; j < colorParentCategoriesSpawnedShards.Length; j++)
			{
				colorParentCategoriesSpawnedShards[j].GetComponent<ScaleJiggle>().targetScale = 0f;
			}
		}
		if (newState != ColorWheelMode.ColorParentCategory && colorParentCategorySpawnedShards != null)
		{
			for (int k = 0; k < colorParentCategorySpawnedShards.Length; k++)
			{
				colorParentCategorySpawnedShards[k].GetComponent<ScaleJiggle>().targetScale = 0f;
			}
		}
		if (newState != ColorWheelMode.ColorCategory && colorCategorySpawnedShard != null)
		{
			for (int l = 0; l < colorCategorySpawnedShard.Length; l++)
			{
				colorCategorySpawnedShard[l].GetComponent<ScaleJiggle>().targetScale = 0f;
			}
		}
		this.ColorWheelStateChanged?.Invoke(newState);
	}

	private GameObject[] ClearSpawnedArray(GameObject[] spawnedArray, int newSize)
	{
		if (spawnedArray != null)
		{
			for (int i = 0; i < spawnedArray.Length; i++)
			{
				UnityEngine.Object.Destroy(spawnedArray[i]);
			}
		}
		spawnedArray = new GameObject[newSize];
		return spawnedArray;
	}

	private void ShowColorParentCategoryState(UnitEditorColorPalette.ParentCatagories categories)
	{
		if (categories.colorPaletteCatagories == null)
		{
			ShowColorParentCategoriesState();
			return;
		}
		int num = categories.colorPaletteCatagories.Length;
		if (num == 1)
		{
			ShowColorCategoryState(categories.colorPaletteCatagories[0]);
			return;
		}
		UpdateStateObjects(ColorWheelMode.ColorParentCategory);
		currentParentCategories = categories;
		colorParentCategorySpawnedShards = ClearSpawnedArray(colorParentCategorySpawnedShards, num);
		float num2 = 360f / (float)num;
		for (int i = 0; i < num; i++)
		{
			GameObject gameObject = SpawnCategoryShard(i, num2, categories.colorPaletteCatagories[i].name, categories.colorPaletteCatagories[i].shardImage);
			UnitEditorColorCatagoryCell component = gameObject.GetComponent<UnitEditorColorCatagoryCell>();
			if (component != null)
			{
				component.Setup(categories.colorPaletteCatagories[i], num2);
				colorParentCategorySpawnedShards[i] = gameObject;
			}
		}
		StateChange();
	}

	private void ShowColorParentCategoriesState()
	{
		int num = ColorPalette.ColorPaletteParentCatagories.Length;
		UpdateStateObjects(ColorWheelMode.ColorParentCategories);
		float num2 = 360f / (float)num;
		colorParentCategoriesSpawnedShards = ClearSpawnedArray(colorParentCategoriesSpawnedShards, num);
		for (int i = 0; i < num; i++)
		{
			GameObject gameObject = SpawnCategoryShard(i, num2, ColorPalette.ColorPaletteParentCatagories[i].name, ColorPalette.ColorPaletteParentCatagories[i].colorWheelSprite);
			UnitEditorColorCatagoryCell component = gameObject.GetComponent<UnitEditorColorCatagoryCell>();
			if (component != null)
			{
				component.Setup(ColorPalette.ColorPaletteParentCatagories[i], num2);
				colorParentCategoriesSpawnedShards[i] = gameObject;
			}
		}
		StateChange();
	}

	private IEnumerator DelayedInvertFix(TextMeshProUGUI text)
	{
		yield return null;
		text.isRightToLeftText = false;
		text.enabled = false;
	}

	public void ShowEquipmentState()
	{
		for (int i = 0; i < SpawnedObjects.Count; i++)
		{
			SpawnedObjects[i].cell.gameObject.SetActive(value: true);
		}
		UpdateStateObjects(ColorWheelMode.EquipmentColors);
		StateChange();
	}

	public void GoBackState()
	{
		if (colorWheelMode == ColorWheelMode.ColorCategory)
		{
			ShowColorParentCategoryState(currentParentCategories);
		}
		else if (colorWheelMode == ColorWheelMode.ColorParentCategory)
		{
			ShowColorParentCategoriesState();
		}
		else if (colorWheelMode == ColorWheelMode.ColorParentCategories)
		{
			ShowEquipmentState();
		}
	}

	public void Setup(UnitEditorManager.EquipedWrapper equiped, UnitEditorEquipedClothing equipedClothing)
	{
		equippedClothing = equipedClothing;
		equippedWrapper = equiped;
		Color[] array = new Color[equiped.propData.m_colors.Length];
		CharacterItem prop = equiped.prop;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine($"Submeshes: {prop.SubmeshArea.Length}");
		float num = 0f;
		for (int i = 0; i < prop.SubmeshArea.Length; i++)
		{
			num += prop.SubmeshArea[i];
			stringBuilder.AppendLine($"Submesh {i}:{prop.SubmeshArea[i]}");
		}
		stringBuilder.AppendLine();
		float[] array2 = new float[array.Length];
		for (int j = 0; j < array.Length; j++)
		{
			if (j < array2.Length && j < prop.SubmeshArea.Length)
			{
				array2[j] = prop.SubmeshArea[j] / num;
				stringBuilder.AppendLine($"Submesh {j}%: {array2[j]}");
			}
		}
		for (int k = 0; k < array2.Length; k++)
		{
			if (!(array2[k] < 0.1f))
			{
				continue;
			}
			array2[k] += 0.1f;
			for (int l = 0; l < array2.Length; l++)
			{
				if (l != k)
				{
					array2[l] -= 0.1f / (float)(array2.Length - 1);
				}
			}
		}
		bool flag = true;
		for (int m = 0; m < array2.Length; m++)
		{
			if (array2[m] > 0f)
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			float num2 = 1f / (float)array2.Length;
			for (int n = 0; n < array2.Length; n++)
			{
				array2[n] = num2;
			}
		}
		for (int num3 = 0; num3 < SpawnedObjects.Count; num3++)
		{
			UnityEngine.Object.Destroy(SpawnedObjects[num3].cell.gameObject);
		}
		SpawnedObjects.Clear();
		float num4 = 100f;
		for (int num5 = 0; num5 < array.Length; num5++)
		{
			float midDegree = num4 - array2[num5] * 180f;
			SpawnShard(array2[num5], num4, midDegree, array2[num5] * 360f, equiped, num5);
			num4 -= array2[num5] * 360f;
		}
		ShowEquipmentState();
	}

	public void UpdateColorWheelInput(bool usingGamepad)
	{
		if (playerActions != null)
		{
			float num = 0f;
			float dst = 0f;
			num = ((!usingGamepad) ? GetCursorAngle(out dst, out var dir) : GetControllerCursorAngle(playerActions, out dst, out dir));
			if (!base.gameObject.activeInHierarchy)
			{
				num = 0f;
				dst = 0f;
			}
			switch (colorWheelMode)
			{
			case ColorWheelMode.EquipmentColors:
				EquippedColorUpdate(num, dst, dir);
				break;
			case ColorWheelMode.ColorParentCategories:
				ParentCategoriesUpdate(num, dst, dir);
				break;
			case ColorWheelMode.ColorParentCategory:
				ParentCategoryUpdate(num, dst, dir);
				break;
			case ColorWheelMode.ColorCategory:
				ColorCategoryUpdate(num, dst, dir);
				break;
			}
		}
	}

	private float GetCursorAngle(out float dst, out Vector3 dir)
	{
		Vector3 position = base.transform.position;
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = 100f;
		mousePosition = cam.ScreenToWorldPoint(mousePosition);
		dir = mousePosition - position;
		dst = Vector2.Distance(mousePosition, position) / 7.2f;
		return NormalizeAngle(180f - Angle(dir));
	}

	private float GetControllerCursorAngle(PlayerActions actions, out float dst, out Vector3 dir)
	{
		dst = 0f;
		dir = Vector3.zero;
		if (actions == null)
		{
			return 0f;
		}
		Vector2 value = actions.m_move.Value;
		dir = value;
		dst = value.magnitude;
		return NormalizeAngle(180f - Angle(dir));
	}

	private void ColorCategoryUpdate(float angle, float dst, Vector3 dir)
	{
		int num = -1;
		int num2 = currentCategory.Colors.Length;
		if (showingTeamColors)
		{
			num2 = currentCategory.TeamColors.Length;
		}
		float num3 = 360f / (float)num2;
		bool onExitButton = false;
		bool onWheel = false;
		if (playerActions.m_back.WasPressed)
		{
			GoBackState();
		}
		if (dst > 1f || dst < 0.27f)
		{
			if (dst < 0.27f)
			{
				onExitButton = true;
				if (Input.GetMouseButtonDown(0))
				{
					GoBackState();
				}
			}
		}
		else
		{
			onWheel = true;
			for (int i = 0; i < num2; i++)
			{
				float y = (float)i * num3;
				if (Quaternion.Angle(Quaternion.Euler(0f, y, 0f), Quaternion.Euler(0f, angle + num3 / 2f, 0f)) < num3 / 2f)
				{
					num = i;
					break;
				}
			}
		}
		cursor.SetCursorAngle(angle, onExitButton, onWheel);
		if (lastSelectedCategory != num && lastSelectedCategory != -1)
		{
			OnColorPreviewExit();
			colorCategorySpawnedShard[lastSelectedCategory].GetComponent<ScaleJiggle>().targetScale = 1f;
		}
		if (num != -1 && lastSelectedCategory != num)
		{
			if (!showingTeamColors)
			{
				OnColorPreviewEnter(currentCategory.Colors[num]);
			}
			else
			{
				OnTeamColorPreviewEnter(currentCategory.TeamColors[num], showingTeamColorsForTeam);
			}
			colorCategorySpawnedShard[num].GetComponent<ScaleJiggle>().targetScale = 1.1f;
		}
		if (num != -1 && (Input.GetMouseButtonDown(0) || playerActions.m_accept.WasPressed))
		{
			if (!showingTeamColors)
			{
				Color(currentCategory.Colors[num]);
			}
			else
			{
				TeamColor(currentCategory.TeamColors[num], showingTeamColorsForTeam);
			}
			OnColorPickingDone(subMeshIndex);
			ShowEquipmentState();
		}
		lastSelectedCategory = num;
	}

	private void ParentCategoryUpdate(float angle, float dst, Vector3 dir)
	{
		int num = -1;
		int num2 = currentParentCategories.colorPaletteCatagories.Length;
		float num3 = 360f / (float)num2;
		bool onExitButton = false;
		bool onWheel = false;
		if (playerActions.m_back.WasPressed)
		{
			GoBackState();
		}
		if (dst > 1f || dst < 0.27f)
		{
			if (dst < 0.27f)
			{
				onExitButton = true;
				if (Input.GetMouseButtonDown(0))
				{
					GoBackState();
				}
			}
		}
		else
		{
			onWheel = true;
			for (int i = 0; i < num2; i++)
			{
				float y = (float)i * num3;
				if (Quaternion.Angle(Quaternion.Euler(0f, y, 0f), Quaternion.Euler(0f, angle + num3 / 2f, 0f)) < num3 / 2f)
				{
					num = i;
					break;
				}
			}
		}
		cursor.SetCursorAngle(angle, onExitButton, onWheel);
		if (colorParentCategorySpawnedShards != null && lastSelectedCategory != num && lastSelectedCategory >= 0 && lastSelectedCategory < colorParentCategorySpawnedShards.Length)
		{
			GameObject gameObject = colorParentCategorySpawnedShards[lastSelectedCategory];
			if (gameObject != null)
			{
				ScaleJiggle component = gameObject.GetComponent<ScaleJiggle>();
				if (component != null)
				{
					component.targetScale = 1f;
				}
			}
			UnitEditorColorCatagoryCell component2 = gameObject.GetComponent<UnitEditorColorCatagoryCell>();
			if (component2 != null)
			{
				component2.OnExit();
			}
		}
		if (colorParentCategorySpawnedShards != null && num >= 0 && lastSelectedCategory != num && num < colorParentCategorySpawnedShards.Length)
		{
			GameObject gameObject2 = colorParentCategorySpawnedShards[num];
			if (gameObject2 != null)
			{
				ScaleJiggle component3 = gameObject2.GetComponent<ScaleJiggle>();
				if (component3 != null)
				{
					component3.targetScale = 1.1f;
				}
			}
			UnitEditorColorCatagoryCell component4 = gameObject2.GetComponent<UnitEditorColorCatagoryCell>();
			if (component4 != null)
			{
				component4.OnEnter();
			}
		}
		lastSelectedCategory = num;
		bool flag = Input.GetMouseButtonDown(0) || playerActions.m_accept.WasPressed;
		if (currentParentCategories.colorPaletteCatagories != null && flag && num >= 0 && num < currentParentCategories.colorPaletteCatagories.Length)
		{
			UnitEditorColorPalette.ColorPaletteCatagory category = currentParentCategories.colorPaletteCatagories[num];
			ShowColorCategoryState(category);
		}
	}

	private void ParentCategoriesUpdate(float angle, float dst, Vector3 dir)
	{
		int num = -1;
		int num2 = ColorPalette.ColorPaletteParentCatagories.Length;
		float num3 = 360f / (float)num2;
		bool onExitButton = false;
		bool onWheel = false;
		if (playerActions.m_back.WasPressed)
		{
			GoBackState();
		}
		if (dst > 1f || dst < 0.27f)
		{
			if (dst < 0.27f)
			{
				onExitButton = true;
				if (Input.GetMouseButtonDown(0))
				{
					GoBackState();
				}
			}
		}
		else
		{
			onWheel = true;
			for (int i = 0; i < num2; i++)
			{
				float y = (float)i * num3;
				if (Quaternion.Angle(Quaternion.Euler(0f, y, 0f), Quaternion.Euler(0f, angle + num3 / 2f, 0f)) < num3 / 2f)
				{
					num = i;
					break;
				}
			}
		}
		cursor.SetCursorAngle(angle, onExitButton, onWheel);
		if (lastSelectedCategory != num && lastSelectedCategory != -1 && lastSelectedCategory < colorParentCategoriesSpawnedShards.Length)
		{
			GameObject obj = colorParentCategoriesSpawnedShards[lastSelectedCategory];
			obj.GetComponent<ScaleJiggle>().targetScale = 1f;
			obj.GetComponent<UnitEditorColorCatagoryCell>().OnExit();
		}
		if (num != -1 && lastSelectedCategory != num)
		{
			colorParentCategoriesSpawnedShards[num].GetComponent<ScaleJiggle>().targetScale = 1.1f;
			colorParentCategoriesSpawnedShards[num].GetComponent<UnitEditorColorCatagoryCell>().OnEnter();
		}
		lastSelectedCategory = num;
		if (num != -1 && (Input.GetMouseButtonDown(0) || playerActions.m_accept.WasPressed))
		{
			ShowColorParentCategoryState(ColorPalette.ColorPaletteParentCatagories[num]);
		}
	}

	private void Color(ColorPaletteData colorData)
	{
		UnityEngine.Object.FindObjectOfType<UnitEditorManager>().ColorProp(equippedWrapper, subMeshIndex, colorData);
	}

	private void TeamColor(TeamColorPaletteData colorData, Team team)
	{
		UnityEngine.Object.FindObjectOfType<UnitEditorManager>().TeamColorProp(equippedWrapper, subMeshIndex, colorData, team);
	}

	private void OnColorPreviewEnter(ColorPaletteData colorData)
	{
		UnityEngine.Object.FindObjectOfType<UnitEditorHighlightingManager>().BlinkClothes(equippedWrapper, subMeshIndex, colorData.m_color, flash: false);
	}

	private void OnTeamColorPreviewEnter(TeamColorPaletteData colorData, Team team)
	{
		UnityEngine.Object.FindObjectOfType<UnitEditorHighlightingManager>().BlinkClothes(equippedWrapper, subMeshIndex, colorData.GetColor(team), flash: false);
	}

	public void OnColorPreviewExit()
	{
		lastWrapper = null;
		UnityEngine.Object.FindObjectOfType<UnitEditorHighlightingManager>().StopBlinking(equippedWrapper, subMeshIndex);
	}

	private void EquippedColorUpdate(float angle, float dst, Vector3 dir)
	{
		int index;
		SpawnedWrapper spawnedWrapper = GetWrapper(angle, out index);
		bool onWheel = false;
		if (dst > 1.15f || dst < 0.27f)
		{
			spawnedWrapper = null;
		}
		else
		{
			onWheel = true;
		}
		cursor.SetCursorAngle(angle, OnExitButton: false, onWheel);
		if (lastWrapper != null)
		{
			UnitEditorEquipedColorCell cell = lastWrapper.cell;
			if (cell == null)
			{
				return;
			}
			if (spawnedWrapper != lastWrapper)
			{
				cell.Exit();
			}
			cell.GetComponent<ScaleJiggle>().targetScale = 1f;
		}
		if (spawnedWrapper != null && !lastFrameExit)
		{
			spawnedWrapper.cell.GetComponent<ScaleJiggle>().targetScale = 1.05f;
			if (spawnedWrapper != lastWrapper)
			{
				subMeshIndex = index;
				m_soundPlayer.PlaySoundEffect(enterRef, 1f, base.transform.position);
				spawnedWrapper.cell.Enter();
			}
			if (Input.GetMouseButtonDown(0) || playerActions.m_accept.WasPressed)
			{
				subMeshIndex = index;
				m_soundPlayer.PlaySoundEffect(clickRef, 1f, base.transform.position);
				spawnedWrapper.cell.GetComponent<ScaleJiggle>().AddForce(0f - clickForce);
				spawnedWrapper.cell.Click();
			}
			lastWrapper = spawnedWrapper;
		}
		else
		{
			lastWrapper = null;
		}
	}

	private SpawnedWrapper GetWrapper(float angle, out int index)
	{
		for (int i = 0; i < SpawnedObjects.Count; i++)
		{
			_ = SpawnedObjects[i].leftAngle;
			float midAngle = SpawnedObjects[i].midAngle;
			if (CompareAngles(angle, midAngle) < SpawnedObjects[i].angle * 0.5f)
			{
				index = i;
				return SpawnedObjects[i];
			}
		}
		index = -1;
		return null;
	}

	private float CompareAngles(float angle1, float angle2)
	{
		Quaternion a = Quaternion.Euler(0f, 0f, angle1);
		Quaternion b = Quaternion.Euler(0f, 0f, angle2);
		return Mathf.Abs(Quaternion.Angle(a, b));
	}

	private static float Angle(Vector2 p_vector2)
	{
		if (p_vector2.x < 0f)
		{
			return 360f - Mathf.Atan2(p_vector2.x, p_vector2.y) * 57.29578f * -1f;
		}
		return Mathf.Atan2(p_vector2.x, p_vector2.y) * 57.29578f;
	}

	private static float NormalizeAngle(float eulerAngles)
	{
		float num = eulerAngles - (float)Mathf.CeilToInt(eulerAngles / 360f) * 360f;
		if (num < 0f)
		{
			num += 360f;
		}
		return num;
	}

	private GameObject SpawnCategoryShard(int index, float anglePerShard, string categoryText, Sprite customSprite = null)
	{
		float num = (float)index * anglePerShard;
		GameObject gameObject = null;
		gameObject = ((!(num <= 180f) || num == 0f) ? UnityEngine.Object.Instantiate(categoryShard, base.transform) : UnityEngine.Object.Instantiate(categoryShardInverted, base.transform));
		gameObject.SetActive(value: true);
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, num);
		gameObject.transform.localScale = Vector3.zero;
		Image[] componentsInChildren = gameObject.GetComponentsInChildren<Image>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].fillAmount = anglePerShard / 360f;
		}
		if (customSprite != null)
		{
			componentsInChildren[0].sprite = customSprite;
			componentsInChildren[0].color = UnityEngine.Color.white;
		}
		LocalizeText componentInChildren = gameObject.GetComponentInChildren<LocalizeText>();
		componentInChildren.LocaleID = categoryText;
		componentInChildren.Text.isRightToLeftText = true;
		StartCoroutine(DelayedInvertFix(componentInChildren.Text));
		gameObject.GetComponent<ScaleJiggle>().targetScale = 1f;
		return gameObject;
	}

	private void SpawnShard(float fillAmount, float rotation, float midDegree, float angle, UnitEditorManager.EquipedWrapper equiped, int index)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(shard, base.transform);
		object data;
		switch (equiped.GetClothingColor(index, ColorPalette, out data))
		{
		case UnitEditorManager.EquipedWrapper.ColorClothingDataType.ColorData:
			gameObject.GetComponent<UnitEditorEquipedColorCell>().Setup((ColorPaletteData)data, index, equiped, this);
			break;
		case UnitEditorManager.EquipedWrapper.ColorClothingDataType.TeamColorData:
			gameObject.GetComponent<UnitEditorEquipedColorCell>().Setup((TeamColorPaletteData)data, index, equiped, this);
			break;
		default:
			gameObject.GetComponent<UnitEditorEquipedColorCell>().Setup((CharacterItem.RendererMaterialWrapper)data, index, equiped, this);
			break;
		}
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localScale = Vector3.one;
		gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, rotation);
		gameObject.SetActive(value: true);
		Image[] componentsInChildren = gameObject.GetComponentsInChildren<Image>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].fillAmount = fillAmount;
		}
		UnitEditorEquipedColorCell component = gameObject.GetComponent<UnitEditorEquipedColorCell>();
		if (!(component == null))
		{
			SpawnedWrapper item = new SpawnedWrapper(component, rotation, midDegree, angle);
			SpawnedObjects.Add(item);
		}
	}

	public void StartColorPicking(int submeshIndex)
	{
		ShowColorParentCategoriesState();
	}

	public void OnColorPickingDone(int submeshIndex)
	{
		object data;
		switch (equippedWrapper.GetClothingColor(submeshIndex, ColorPalette, out data))
		{
		case UnitEditorManager.EquipedWrapper.ColorClothingDataType.ColorData:
			SpawnedObjects[submeshIndex].cell.Setup((ColorPaletteData)data, submeshIndex, equippedWrapper, this);
			break;
		case UnitEditorManager.EquipedWrapper.ColorClothingDataType.TeamColorData:
			SpawnedObjects[submeshIndex].cell.Setup((TeamColorPaletteData)data, submeshIndex, equippedWrapper, this);
			break;
		default:
			SpawnedObjects[submeshIndex].cell.Setup((CharacterItem.RendererMaterialWrapper)data, submeshIndex, equippedWrapper, this);
			break;
		}
	}

	public void RemoveClothes()
	{
		UnitEditorManager unitEditorManager = UnityEngine.Object.FindObjectOfType<UnitEditorManager>();
		if (unitEditorManager != null)
		{
			if (equippedWrapper.GetType() == typeof(UnitEditorManager.EquipedWeaponWrapper))
			{
				UnitEditorManager.EquipedWeaponWrapper equipedWeaponWrapper = (UnitEditorManager.EquipedWeaponWrapper)equippedWrapper;
				unitEditorManager.RemoveWeapon(equipedWeaponWrapper.isRightHanded);
			}
			else
			{
				unitEditorManager.RemoveProp(equippedWrapper);
			}
			unitEditorManager.UIManager.NavigateToPage("UNIT");
		}
	}
}
