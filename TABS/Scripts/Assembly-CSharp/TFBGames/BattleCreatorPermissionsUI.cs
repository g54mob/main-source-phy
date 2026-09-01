using Landfall.TABS;
using Landfall.TABS.Workshop;
using Landfall.TABS_Input;
using TMPro;
using UnityEngine;

namespace TFBGames
{
	public class BattleCreatorPermissionsUI : MonoBehaviour, IBattleCreatorMenu
	{
		[SerializeField]
		private TextMeshProUGUI message;

		private InputService inputService;

		private GameObject parent;

		public bool AllowPageChange => true;

		public void Open(BattleCreatorState state, object data)
		{
			inputService.OnUIOpen();
			parent.SetActive(value: true);
		}

		public void Close()
		{
			inputService.OnUIClose();
			parent.SetActive(value: false);
		}

		public bool IsOpen()
		{
			return base.gameObject.activeInHierarchy;
		}

		public void Init(BattleCreatorTabsUIHandler tabsHandler)
		{
			inputService = ServiceLocator.GetService<InputService>();
			parent = base.transform.parent.gameObject;
			parent.SetActive(value: false);
		}

		public void Init(CustomContentOverlaysManager overlay)
		{
		}

		public bool NavigateUIWithController(PlayerActions playerActions)
		{
			return false;
		}

		public void SetMessage(string messageText)
		{
			message.text = messageText;
		}
	}
}
