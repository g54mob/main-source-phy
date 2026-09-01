using System;
using System.Linq;
using DM;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS.Workshop
{
	public class UnitCreatorLoadUnitUI : MonoBehaviour, ICampaignMenu
	{
		private enum UnitTypeSearch : byte
		{
			Local = 0,
			Workshop = 1
		}

		public float m_Spring = 10f;

		public float m_Dampner = 5f;

		private string m_levelName;

		private float m_velocity;

		private float m_targerScale = 1f;

		private State m_state = State.Closing;

		private UnitTypeSearch m_CurrentUnitTypeSearch;

		[SerializeField]
		private Transform m_Grid;

		[SerializeField]
		private GameObject m_LevelCell;

		[SerializeField]
		private Button m_LoadButton;

		[SerializeField]
		private Button m_CloseButton;

		[SerializeField]
		private Button m_LocalCategoryButton;

		[SerializeField]
		private Button m_WorkshopCategoryButton;

		private UnitCellUI m_LastClickedUnit;

		public event Action Opened;

		public event Action Closed;

		private void Awake()
		{
			m_CurrentUnitTypeSearch = UnitTypeSearch.Local;
			InitListeners();
		}

		private void InitListeners()
		{
			m_LoadButton.onClick.AddListener(Load);
			m_CloseButton.onClick.AddListener(delegate
			{
				UnitCreatorUIHandler.Instance.Toggle(UnitCreatorScreen.Load);
			});
			m_LocalCategoryButton.onClick.AddListener(OnLocalCategoryClicked);
			m_WorkshopCategoryButton.onClick.AddListener(OnWorkshopCategoryClicked);
		}

		private void OnWorkshopCategoryClicked()
		{
			m_CurrentUnitTypeSearch = UnitTypeSearch.Workshop;
			Populate();
		}

		private void OnLocalCategoryClicked()
		{
			m_CurrentUnitTypeSearch = UnitTypeSearch.Local;
			Populate();
		}

		private void OnEnable()
		{
			ServiceLocator.GetService<CustomContentLoaderModIO>().QuickRefresh(WorkshopContentType.Unit, delegate
			{
				Populate();
			});
		}

		private void OnDisable()
		{
		}

		private void Populate()
		{
			Clear();
			UnitBlueprint[] array = new UnitBlueprint[0];
			array = ContentDatabase.Instance().GetUserUnitBlueprints().ToArray();
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				UnitBlueprint unitBlueprint = array[i];
				if (m_CurrentUnitTypeSearch == UnitTypeSearch.Local)
				{
					if (!unitBlueprint.IsCustomUnit)
					{
						SpawnUnitCell(unitBlueprint);
					}
				}
				else if (m_CurrentUnitTypeSearch == UnitTypeSearch.Workshop && unitBlueprint.IsCustomUnit)
				{
					SpawnUnitCell(unitBlueprint);
				}
			}
		}

		private void Clear()
		{
			for (int num = m_Grid.childCount - 1; num >= 0; num--)
			{
				UnityEngine.Object.Destroy(m_Grid.GetChild(num).gameObject);
			}
			m_LastClickedUnit = null;
			m_LoadButton.interactable = false;
		}

		private void SpawnUnitCell(UnitBlueprint wrapper)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(m_LevelCell);
			UnitCellUI cellUI = gameObject.FetchComponent<UnitCellUI>();
			cellUI.Init(wrapper, delegate
			{
				OnUnitClicked(cellUI);
			});
			gameObject.transform.SetParent(m_Grid, worldPositionStays: false);
			gameObject.transform.localScale = Vector3.one;
			gameObject.SetActive(value: true);
		}

		private void OnUnitClicked(UnitCellUI levelCell)
		{
			Debug.Log("Clicked: " + levelCell.UnitName, levelCell);
			if (m_LastClickedUnit != null && m_LastClickedUnit == levelCell)
			{
				Load();
				return;
			}
			m_LastClickedUnit = levelCell;
			m_LoadButton.interactable = true;
		}

		private void Load()
		{
			if (m_LastClickedUnit == null)
			{
				Debug.LogError("Clicked load level but clicked level is NULL!?");
				return;
			}
			UnityEngine.Object.FindObjectOfType<UnitEditorHandler>().LoadUnit(m_LastClickedUnit.UnitWrapper, delegate
			{
				UnityEngine.Object.FindObjectOfType<UnitCreatorUIHandler>().Toggle(UnitCreatorScreen.Load);
			});
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
			this.Opened?.Invoke();
		}

		public void Close()
		{
			m_state = State.Closing;
			m_targerScale = 0f;
			this.Closed?.Invoke();
		}

		public bool IsOpen()
		{
			return m_state == State.Opening;
		}
	}
}
