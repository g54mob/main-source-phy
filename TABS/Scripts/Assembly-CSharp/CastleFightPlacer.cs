using System.Collections.Generic;
using Landfall.TABS;
using TMPro;
using UnityEngine;

public class CastleFightPlacer : MonoBehaviour
{
	public class HouseData
	{
		public UnitBlueprint unit;

		public Vector2Int pos;

		public GameObject house;

		public CastleFightHouse houseData;

		public HouseData(UnitBlueprint unit, Vector2Int pos, GameObject house, CastleFightHouse housedata)
		{
			this.unit = unit;
			this.pos = pos;
			this.house = house;
			houseData = housedata;
		}
	}

	public enum MenuState
	{
		Placement = 0,
		BrowseUnits = 1,
		UpgradeOrSell = 2
	}

	public Team team;

	public Vector3 min;

	public Vector3 max;

	private Vector3 startPos;

	public KeyCode left;

	public KeyCode right;

	public KeyCode up;

	public KeyCode down;

	public KeyCode click;

	public Transform placementPointer;

	private Vector3 pointerPos;

	private Vector3 pointerVelocity;

	private float holdCounter;

	private float holdTickCounter;

	public UnitBlueprint[] units;

	public MenuState state;

	public GameObject houseBase;

	private CodeAnimation m_animation;

	public List<HouseData> houses = new List<HouseData>();

	private float cappedDeltaTime;

	private int currentButtonID;

	public Transform unitMenu;

	public Transform upgradeMenu;

	public TextMeshProUGUI nameText;

	public TextMeshProUGUI descriptionText;

	private CastleFightButton[] unitMenuButtons;

	private CastleFightButton[] upgradeMenuButtons;

	private CastleFightButton[] currentMenuButtons;

	private Vector3 holdMove;

	private Vector3 pressMove;

	private bool clickedThisFrame;

	private void Start()
	{
		pointerPos = placementPointer.position;
		startPos = pointerPos;
		m_animation = GetComponentInChildren<CodeAnimation>();
		unitMenuButtons = unitMenu.GetComponentsInChildren<CastleFightButton>();
		upgradeMenuButtons = upgradeMenu.GetComponentsInChildren<CastleFightButton>();
		for (int i = 1; i < unitMenuButtons.Length; i++)
		{
			unitMenuButtons[i].unit = units[i - 1];
		}
	}

	private void Update()
	{
		cappedDeltaTime = Mathf.Clamp(Time.deltaTime, 0f, 0.05f);
		AssignInput();
		Navigate();
		if (state == MenuState.Placement)
		{
			MovePlacer();
		}
	}

	private void Navigate()
	{
		if (state != MenuState.Placement)
		{
			currentButtonID += (int)pressMove.x;
			currentButtonID = Mathf.Clamp(currentButtonID, 0, currentMenuButtons.Length - 1);
			if (currentMenuButtons[currentButtonID].buttonType == CastleFightButton.CastleFightButtonType.Cancel)
			{
				HouseData houseData = HouseAt(pointerPos);
				nameText.text = "Cancel";
				if (houseData != null)
				{
					descriptionText.text = houseData.unit.name;
				}
				else
				{
					descriptionText.text = "";
				}
			}
			else if (currentMenuButtons[currentButtonID].buttonType == CastleFightButton.CastleFightButtonType.Level)
			{
				HouseData houseData2 = HouseAt(pointerPos);
				if (houseData2.houseData.level < 2)
				{
					nameText.text = "Upgrade to level " + (houseData2.houseData.level + 1);
					descriptionText.text = houseData2.unit.name + " for " + (100f + houseData2.houseData.totalSpentOnBuilding * 5f);
				}
				else
				{
					nameText.text = "Unit is max level";
					descriptionText.text = "You cannot upgrade any more";
				}
			}
			else if (currentMenuButtons[currentButtonID].buttonType == CastleFightButton.CastleFightButtonType.Sell)
			{
				HouseData houseData3 = HouseAt(pointerPos);
				nameText.text = "Sell";
				descriptionText.text = houseData3.unit.name + " for " + houseData3.houseData.totalSpentOnBuilding / 2f;
			}
			else
			{
				descriptionText.text = currentMenuButtons[currentButtonID].unit.GetUnitCost().ToString();
				nameText.text = currentMenuButtons[currentButtonID].unit.name;
			}
		}
		if (clickedThisFrame)
		{
			if (state == MenuState.Placement)
			{
				if (HouseAt(pointerPos) == null)
				{
					state = MenuState.BrowseUnits;
					currentMenuButtons = unitMenuButtons;
				}
				else
				{
					state = MenuState.UpgradeOrSell;
					currentMenuButtons = upgradeMenuButtons;
				}
			}
			else if (state == MenuState.BrowseUnits)
			{
				if (currentMenuButtons[currentButtonID].buttonType == CastleFightButton.CastleFightButtonType.Cancel)
				{
					state = MenuState.Placement;
				}
				else
				{
					if (HouseAt(pointerPos) != null || !((float)currentMenuButtons[currentButtonID].unit.GetUnitCost() <= CastleFightGamemode.GetMoney(team)) || !CastleFightGamemode.BuyHouse(currentMenuButtons[currentButtonID].unit.GetUnitCost(), team))
					{
						return;
					}
					PurchaseUnit();
					state = MenuState.Placement;
				}
			}
			else if (state == MenuState.UpgradeOrSell)
			{
				if (currentMenuButtons[currentButtonID].buttonType == CastleFightButton.CastleFightButtonType.Cancel)
				{
					HouseAt(pointerPos);
					state = MenuState.Placement;
				}
				else if (currentMenuButtons[currentButtonID].buttonType == CastleFightButton.CastleFightButtonType.Level)
				{
					HouseData houseData4 = HouseAt(pointerPos);
					if (!(100f + houseData4.houseData.totalSpentOnBuilding * 5f <= CastleFightGamemode.GetMoney(team)) || houseData4.houseData.level >= 2)
					{
						return;
					}
					CastleFightGamemode.AddIncome(100f + houseData4.houseData.totalSpentOnBuilding * 5f, team);
					houseData4.house.GetComponent<CastleFightHouse>().level++;
					CastleFightGamemode.RemoveMoney(100f + houseData4.houseData.totalSpentOnBuilding * 5f, team);
					houseData4.houseData.totalSpentOnBuilding += 100f + houseData4.houseData.totalSpentOnBuilding * 5f;
					state = MenuState.Placement;
				}
				else
				{
					if (currentMenuButtons[currentButtonID].buttonType != CastleFightButton.CastleFightButtonType.Sell)
					{
						return;
					}
					state = MenuState.Placement;
					HouseData houseData5 = HouseAt(pointerPos);
					CastleFightGamemode.SellHouse(houseData5.unit, team, houseData5.houseData.totalSpentOnBuilding);
					CastleFightGamemode.AddIncome(houseData5.houseData.totalSpentOnBuilding * 0.75f, team, negative: true);
					Object.Destroy(houseData5.house);
					houses.Remove(houseData5);
				}
			}
			StateUpdated();
		}
		if (state != MenuState.Placement && (bool)currentMenuButtons[currentButtonID] && currentButtonID < currentMenuButtons.Length && currentButtonID > -1)
		{
			currentMenuButtons[currentButtonID].GetComponent<ScaleJiggle>().velocity += cappedDeltaTime * 300f;
		}
	}

	private void StateUpdated()
	{
		if (state == MenuState.BrowseUnits)
		{
			m_animation.Animate(CodeAnimationInstance.AnimationUse.In);
			unitMenu.gameObject.SetActive(value: true);
			upgradeMenu.gameObject.SetActive(value: false);
		}
		if (state == MenuState.UpgradeOrSell)
		{
			m_animation.Animate(CodeAnimationInstance.AnimationUse.In);
			upgradeMenu.gameObject.SetActive(value: true);
			unitMenu.gameObject.SetActive(value: false);
		}
		if (state == MenuState.Placement)
		{
			m_animation.Animate(CodeAnimationInstance.AnimationUse.Out);
		}
	}

	private void PurchaseUnit()
	{
		CastleFightGamemode.RemoveMoney(currentMenuButtons[currentButtonID].unit.GetUnitCost(), team);
		GameObject gameObject = Object.Instantiate(houseBase, pointerPos, placementPointer.transform.rotation);
		CastleFightHouse component = gameObject.GetComponent<CastleFightHouse>();
		houses.Add(new HouseData(currentMenuButtons[currentButtonID].unit, new Vector2Int((int)(2f * pointerPos.x), (int)(2f * pointerPos.z)), gameObject, component));
		component.Setup(team, currentMenuButtons[currentButtonID].unit);
		component.totalSpentOnBuilding += currentMenuButtons[currentButtonID].unit.GetUnitCost();
	}

	public HouseData HouseAt(Vector3 pos)
	{
		Vector2Int vector2Int = new Vector2Int((int)(2f * pos.x), (int)(2f * pos.z));
		for (int i = 0; i < houses.Count; i++)
		{
			if (houses[i].pos == vector2Int)
			{
				return houses[i];
			}
		}
		return null;
	}

	private void MovePlacer()
	{
		pointerPos += pressMove * 0.5f;
		pointerPos = new Vector3(Mathf.Clamp(pointerPos.x, startPos.x - min.x, startPos.x + max.x), pointerPos.y, Mathf.Clamp(pointerPos.z, startPos.z - min.z, startPos.z + max.z));
		pointerVelocity += (pointerPos - placementPointer.position) * cappedDeltaTime * 1000f;
		pointerVelocity -= pointerVelocity * cappedDeltaTime * 20f;
		placementPointer.position += pointerVelocity * cappedDeltaTime;
	}

	private void AssignInput()
	{
		holdMove = Vector3.zero;
		pressMove = Vector3.zero;
		clickedThisFrame = Input.GetKeyDown(click);
		if (Input.GetKey(left))
		{
			holdMove += Vector3.left;
		}
		if (Input.GetKey(right))
		{
			holdMove += Vector3.right;
		}
		if (Input.GetKey(up))
		{
			holdMove += Vector3.forward;
		}
		if (Input.GetKey(down))
		{
			holdMove += Vector3.back;
		}
		if (Input.GetKeyDown(left))
		{
			pressMove += Vector3.left;
		}
		if (Input.GetKeyDown(right))
		{
			pressMove += Vector3.right;
		}
		if (Input.GetKeyDown(up))
		{
			pressMove += Vector3.forward;
		}
		if (Input.GetKeyDown(down))
		{
			pressMove += Vector3.back;
		}
		if (Input.GetKeyDown(click))
		{
			clickedThisFrame = true;
		}
		if (holdMove != Vector3.zero)
		{
			holdCounter += cappedDeltaTime;
		}
		else
		{
			holdCounter = 0f;
			holdTickCounter = 0f;
		}
		if (holdCounter > 0.2f)
		{
			holdTickCounter += cappedDeltaTime;
			if (holdTickCounter > 0.05f + ((holdMove.magnitude > 1.1f) ? 0.03f : 0f))
			{
				pressMove += holdMove * 1f;
				holdTickCounter = 0f;
			}
		}
	}
}
