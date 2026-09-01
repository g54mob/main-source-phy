using System.Collections.Generic;
using DM;
using TFBGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS.Workshop
{
	public class FactionCreatorUI : MonoBehaviour, ICampaignMenu
	{
		private const int MAX_UNITS_PER_FACTION = 7;

		private int m_CurrentNumberOfUnitsInFaction;

		public float m_Spring = 10f;

		public float m_Dampner = 5f;

		private float m_velocity;

		private float m_targerScale = 1f;

		private State m_state = State.Closing;

		[SerializeField]
		private GameObject m_CreateFactionObject;

		[SerializeField]
		private GameObject m_EditFactionObject;

		[SerializeField]
		private Transform m_LibraryGrid;

		[SerializeField]
		private Transform m_SelectedUnitsGrid;

		[SerializeField]
		private GameObject m_UnitCell;

		[SerializeField]
		private GameObject m_SelectedUnitCell;

		[SerializeField]
		private Button m_SaveButton;

		[SerializeField]
		private Button m_ToSelectedButton;

		[SerializeField]
		private Button m_ToLibraryButton;

		[SerializeField]
		private Button m_CloseButton;

		[SerializeField]
		private Button m_EditButton;

		[SerializeField]
		private Button m_CreateButton;

		[SerializeField]
		private TMP_InputField m_FactionNameInput;

		private FactionUnitCellUI m_SelectedUnitUI;

		private List<UnitBlueprint> m_SelectedUnitsForFaction;

		private Dictionary<DatabaseID, FactionUnitCellUI> m_UnitsInSelection;

		private DatabaseID[] m_UnitsPositionArray;

		private bool m_Editing;

		private SteamManager m_steamManager;

		private const int START_X = -7;

		private const int START_Y = 5;

		private int m_x;

		private int m_y;

		public static FactionCreatorUI Instance { get; private set; }

		private void Awake()
		{
			if (Instance != null)
			{
				Object.Destroy(base.gameObject);
				return;
			}
			Instance = this;
			m_steamManager = (SteamManager)ServiceLocator.GetService<IPlatformManager>();
			InitReferences();
			InitListeners();
		}

		private void InitReferences()
		{
			m_SelectedUnitsForFaction = new List<UnitBlueprint>();
			m_UnitsInSelection = new Dictionary<DatabaseID, FactionUnitCellUI>();
			m_UnitsPositionArray = new DatabaseID[7];
			NextUnitPosition();
		}

		private void InitListeners()
		{
			m_SaveButton.onClick.AddListener(SaveFaction);
			m_CloseButton.onClick.AddListener(delegate
			{
				UnitCreatorUIHandler.Instance.Toggle(UnitCreatorScreen.Faction);
			});
			m_EditButton.onClick.AddListener(OnEditFactionClicked);
			m_CreateButton.onClick.AddListener(OnEditFactionClicked);
		}

		private void OnEnable()
		{
			UIScreenInputBlocker.ScreenOpen(this);
			Populate();
		}

		private void OnDisable()
		{
			UIScreenInputBlocker.ScreenClose(this);
		}

		private void OnEditFactionClicked()
		{
			m_Editing = !m_Editing;
			m_CreateFactionObject.SetActive(!m_Editing);
			m_EditFactionObject.SetActive(m_Editing);
		}

		private void SaveFaction()
		{
		}

		private void MakeFactionSequenceList()
		{
			m_SelectedUnitsForFaction = new List<UnitBlueprint>();
			foreach (KeyValuePair<DatabaseID, FactionUnitCellUI> item in m_UnitsInSelection)
			{
				m_SelectedUnitsForFaction.Add(item.Value.UnitBlueprint);
			}
		}

		private bool ValidateText()
		{
			return !string.IsNullOrWhiteSpace(m_FactionNameInput.text);
		}

		private void Populate()
		{
			ClearAll();
		}

		public void SelectUnit(DatabaseID unitID)
		{
			UnitBlueprint unitBlueprint = ContentDatabase.Instance().GetUnitBlueprint(unitID);
			if (m_UnitsInSelection.ContainsKey(unitID))
			{
				m_SelectedUnitUI = m_UnitsInSelection[unitID];
				RemoveUnit();
			}
			else if (m_CurrentNumberOfUnitsInFaction < 7)
			{
				FactionUnitCellUI factionUnitCellUI = new FactionUnitCellUI();
				factionUnitCellUI.Init(unitBlueprint, null);
				m_UnitsInSelection.Add(unitID, factionUnitCellUI);
				m_SelectedUnitUI = factionUnitCellUI;
				SpawnVisualUnit();
			}
		}

		private void NextUnitPosition()
		{
			int emptyIndex = GetEmptyIndex();
			m_x = -7 + 2 * (emptyIndex + 1);
			m_y = 5;
		}

		private GameObject SpawnVisualUnit()
		{
			GameObject gameObject = new GameObject("Unit: " + m_SelectedUnitUI.UnitName);
			Vector3 vector = new Vector3(m_x, 0f, m_y);
			gameObject.transform.position = vector;
			GameObject[] array = m_SelectedUnitUI.UnitBlueprint.Spawn(vector, Quaternion.Euler(0f, 180f, 0f), Team.Red);
			for (int i = 0; i < array.Length; i++)
			{
				array[i].transform.SetParent(gameObject.transform);
			}
			int emptyIndex = GetEmptyIndex();
			m_UnitsPositionArray[emptyIndex] = m_SelectedUnitUI.UnitBlueprint.Entity.GUID;
			NextUnitPosition();
			m_SelectedUnitUI.AssignVisualUnit(gameObject);
			m_CurrentNumberOfUnitsInFaction++;
			return gameObject;
		}

		private int GetEmptyIndex()
		{
			int num = 7;
			for (int i = 0; i < num; i++)
			{
				if (m_UnitsPositionArray[i] == default(DatabaseID))
				{
					return i;
				}
			}
			return -1;
		}

		private void RemoveUnit()
		{
			DatabaseID gUID = m_SelectedUnitUI.UnitBlueprint.Entity.GUID;
			m_UnitsInSelection.Remove(gUID);
			for (int num = 6; num >= 0; num--)
			{
				if (m_UnitsPositionArray[num] == gUID)
				{
					m_UnitsPositionArray[num] = default(DatabaseID);
				}
			}
			Object.Destroy(m_SelectedUnitUI.VisualUnit);
			NextUnitPosition();
			m_CurrentNumberOfUnitsInFaction--;
		}

		private void ClearAll()
		{
			ClearLibrary();
			ClearSelected();
		}

		private void ClearLibrary()
		{
			for (int num = m_LibraryGrid.childCount - 1; num >= 0; num--)
			{
				Object.Destroy(m_LibraryGrid.GetChild(num).gameObject);
			}
		}

		private void ClearSelected()
		{
			for (int num = m_SelectedUnitsGrid.childCount - 1; num >= 0; num--)
			{
				Object.Destroy(m_SelectedUnitsGrid.GetChild(num).gameObject);
			}
		}

		private void Update()
		{
			m_velocity += (m_targerScale - base.transform.localScale.x) * m_Spring * Time.deltaTime;
			m_velocity -= m_velocity * Time.deltaTime * m_Dampner;
			base.transform.localScale = base.transform.localScale + Vector3.one * m_velocity * Time.deltaTime;
			if (m_state == State.Closing && base.transform.localScale.x <= 0f)
			{
				base.gameObject.SetActive(value: false);
			}
		}

		public void Toggle()
		{
			if (m_state == State.Closing)
			{
				Open();
			}
			else
			{
				Close();
			}
		}

		public void Open()
		{
			base.transform.localScale = Vector3.zero;
			m_state = State.Opening;
			m_targerScale = 1f;
			base.gameObject.SetActive(value: true);
		}

		public void Close()
		{
			m_state = State.Closing;
			m_targerScale = 0f;
		}

		public bool IsOpen()
		{
			return m_state == State.Opening;
		}
	}
}
