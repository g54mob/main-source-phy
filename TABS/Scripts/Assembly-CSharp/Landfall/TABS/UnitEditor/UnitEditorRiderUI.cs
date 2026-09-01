using Landfall.TABS_Input;
using TFBGames;
using UIStateManager;
using UnityEngine;

namespace Landfall.TABS.UnitEditor
{
	public class UnitEditorRiderUI : MonoBehaviour
	{
		public UnitButtonBase m_UnitButton;

		public LocalizeText m_unitName;

		public GameObject[] m_ShowIfHasRider = new GameObject[0];

		private UnitEditorUIManager unitEditorUIManager;

		private InputService inputService;

		public bool HasRider { get; private set; }

		private void Awake()
		{
			inputService = ServiceLocator.GetService<InputService>();
		}

		public void UpdateUI(UnitBlueprint unit)
		{
			HasRider = unit != null;
			if (unit != null)
			{
				string text = (string.IsNullOrEmpty(unit.Entity.Name) ? unit.name : unit.Entity.Name);
				m_unitName.LocaleID = string.Empty;
				m_unitName.Text.text = text;
				m_UnitButton.Setup(unit);
			}
			else
			{
				m_unitName.LocaleID = "LABEL_NONE";
				m_UnitButton.Setup(null);
			}
			bool active = HasRider && inputService.CurrentInputType != InputType.Controller;
			for (int i = 0; i < m_ShowIfHasRider.Length; i++)
			{
				m_ShowIfHasRider[i].SetActive(active);
			}
		}

		public void SetInterfaceStateManager(InterfaceStateManager interfaceStateManager)
		{
			if (interfaceStateManager is UnitEditorUIManager unitEditorUIManager)
			{
				this.unitEditorUIManager = unitEditorUIManager;
			}
		}

		public void EditUnit()
		{
			if (unitEditorUIManager != null)
			{
				unitEditorUIManager.unitEditorManager.LoadRiderSubUnit();
			}
		}

		public void RemoveUnit()
		{
			HasRider = false;
			if (unitEditorUIManager != null)
			{
				unitEditorUIManager.unitEditorManager.SetRider(null);
			}
		}
	}
}
