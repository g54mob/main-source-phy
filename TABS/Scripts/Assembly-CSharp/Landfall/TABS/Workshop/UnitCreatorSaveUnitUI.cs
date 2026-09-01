using System;
using TFBGames;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Landfall.TABS.Workshop
{
	public class UnitCreatorSaveUnitUI : MonoBehaviour, ICampaignMenu
	{
		[SerializeField]
		private TMP_InputField m_UnitNameInput;

		[SerializeField]
		private TextMeshProUGUI m_CostText;

		[SerializeField]
		private Button m_SaveButton;

		[SerializeField]
		private Button m_UploadButton;

		[SerializeField]
		private Button m_UpdateButton;

		[SerializeField]
		private Button m_CloseButton;

		private int m_DecidedCost = -1;

		public float m_Spring = 10f;

		public float m_Dampner = 5f;

		private string m_UnitName;

		private string m_LastUnitName;

		private float m_velocity;

		private float m_targerScale = 1f;

		private State m_state = State.Closing;

		private UnitEditorHandler m_UnitEditorHandler;

		public event Action Opened;

		public event Action Closed;

		private void Awake()
		{
			InitListeners();
			m_UnitEditorHandler = UnityEngine.Object.FindObjectOfType<UnitEditorHandler>();
		}

		private void InitListeners()
		{
			m_UnitNameInput.onValueChanged.AddListener(OnUnitNameChange);
			m_SaveButton.onClick.AddListener(delegate
			{
				if (ValidateUnit())
				{
					SaveUnitLocal();
				}
			});
			m_UploadButton.onClick.AddListener(delegate
			{
				if (ValidateUnit())
				{
					UploadUnit();
				}
			});
			m_UpdateButton.onClick.AddListener(delegate
			{
				if (ValidateUnit())
				{
					UpdateUnit();
				}
			});
			m_CloseButton.onClick.AddListener(delegate
			{
				UnitCreatorUIHandler.Instance.Toggle(UnitCreatorScreen.Save);
			});
		}

		private void OnEnable()
		{
			m_LastUnitName = UnitEditorHandler.Instance.LastLoadedUnitName;
			m_UnitName = m_LastUnitName;
			GetComponentInChildren<TMP_InputField>().text = m_UnitName;
			bool initialized = ((SteamManager)ServiceLocator.GetService<IPlatformManager>()).Initialized;
			m_UploadButton.gameObject.SetActive(initialized);
			m_UpdateButton.gameObject.SetActive(!(m_UnitEditorHandler.CurrentLoadedCustomUnit == null) && m_UnitEditorHandler.CurrentLoadedCustomUnit.IsCustomUnit);
		}

		private void OnUnitNameChange(string name)
		{
			m_UnitName = name;
		}

		private bool ValidateUnit()
		{
			if (!ValidateÜnitName())
			{
				ShowErrorPopup();
				return false;
			}
			return true;
		}

		private void UploadUnit()
		{
			CustomContentFilePaths.GetTempFolderPath();
			SaveUnitLocal(temp: true);
			((UnityAction)delegate
			{
			})();
		}

		private void UpdateUnit()
		{
			CustomContentFilePaths.GetTempFolderPath();
			DatabaseID gUID = m_UnitEditorHandler.CurrentLoadedCustomUnit.Entity.GUID;
			SaveUnitLocal(temp: true, gUID);
			((UnityAction)delegate
			{
			})();
		}

		private void SaveUnitLocal(bool temp = false, DatabaseID useThis = default(DatabaseID))
		{
			m_UnitEditorHandler.SaveUnit(m_UnitName, temp, useThis, delegate
			{
				UnitCreatorUIHandler.Instance.Toggle(UnitCreatorScreen.Save);
			});
		}

		private bool ValidateÜnitName()
		{
			if (string.IsNullOrWhiteSpace(m_UnitName))
			{
				return false;
			}
			return true;
		}

		private void ShowErrorPopup()
		{
			ServiceLocator.GetService<ModalPanel>().PopUp("POPUP_EMPTYUNITNAME");
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
