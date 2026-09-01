using System;
using System.Collections.Generic;
using TFBGames;
using UnityEngine;

namespace Landfall.TABS.Workshop
{
	public class BattleCreatorTabsUIHandler : MonoBehaviour
	{
		public enum State
		{
			Closed = 0,
			Open = 1
		}

		[SerializeField]
		private GameObject m_SaveObject;

		[SerializeField]
		private GameObject m_AssetObject;

		[SerializeField]
		private GameObject m_CampaignCreator;

		[SerializeField]
		private GameObject m_UploadObject;

		[SerializeField]
		[Tooltip("UI to show permissions messages (e.g. when user doesn't have permissions to upload content.")]
		private GameObject m_PermissionsObject;

		[SerializeField]
		private GameObject m_StartScreenObject;

		private float m_TargetAlpha;

		private bool m_DoneExpanding;

		private Coroutine m_openNewScreenCoroutine;

		public Dictionary<BattleCreatorScreenState, IBattleCreatorMenu> m_CampaignMenus;

		private IAccountPermissions m_AccountPermissions;

		private BattleCreatorPermissionsUI m_PermissionsUI;

		private int onToggleCount;

		private BattleCreatorState m_LastState = BattleCreatorState.None;

		private BattleCreatorScreenState m_LastScreenState = BattleCreatorScreenState.None;

		public State m_State { get; private set; }

		public event Action<BattleCreatorScreenState> PageOpened;

		public event Action<BattleCreatorState> UIClosed;

		private void Awake()
		{
			if (CampaignPlayerDataHolder.CurrentGameModeState != GameModeState.Sandbox)
			{
				UnityEngine.Object.DestroyImmediate(base.gameObject);
				return;
			}
			InitReferences();
			InitListeners();
		}

		private void Start()
		{
			m_AccountPermissions = ServiceLocator.GetService<IAccountPermissions>();
			m_PermissionsUI = m_PermissionsObject.GetComponent<BattleCreatorPermissionsUI>();
		}

		private void InitReferences()
		{
			m_CampaignMenus = new Dictionary<BattleCreatorScreenState, IBattleCreatorMenu>();
			m_CampaignMenus.Add(BattleCreatorScreenState.Save, m_SaveObject.GetComponent<IBattleCreatorMenu>());
			m_CampaignMenus.Add(BattleCreatorScreenState.AssetMenu, m_AssetObject.GetComponent<IBattleCreatorMenu>());
			m_CampaignMenus.Add(BattleCreatorScreenState.TwoList, m_CampaignCreator.GetComponent<IBattleCreatorMenu>());
			m_CampaignMenus.Add(BattleCreatorScreenState.Upload, m_UploadObject.GetComponent<IBattleCreatorMenu>());
			m_CampaignMenus.Add(BattleCreatorScreenState.Permissions, m_PermissionsObject.GetComponent<IBattleCreatorMenu>());
			m_CampaignMenus.Add(BattleCreatorScreenState.StartScreen, m_StartScreenObject.GetComponent<IBattleCreatorMenu>());
			BattleCreatorSharedCommands.AssignUI(this);
		}

		private void InitListeners()
		{
			foreach (KeyValuePair<BattleCreatorScreenState, IBattleCreatorMenu> campaignMenu in m_CampaignMenus)
			{
				campaignMenu.Value.Init(this);
			}
		}

		public void Close()
		{
			UIScreenInputBlocker.AnimatedMenuTransitionEnd();
			OnToggle(BattleCreatorScreenState.None, BattleCreatorState.None);
		}

		public void OpenNewScreen(BattleCreatorScreenState screenState, BattleCreatorState newState, object data = null, bool closeIfAlreadyOpen = true)
		{
			OnToggle(screenState, newState, data, closeIfAlreadyOpen);
		}

		public void OpenUploadScreen(bool closeIfAlreadyOpen = true)
		{
			int tempCount = onToggleCount;
			m_AccountPermissions.CanUploadUgcAsync(showPopup: false, null, delegate(bool permitted)
			{
				if (tempCount == onToggleCount)
				{
					if (permitted)
					{
						OnToggle(BattleCreatorScreenState.AssetMenu, BattleCreatorState.Upload, closeIfAlreadyOpen);
					}
					else
					{
						m_PermissionsUI.SetMessage("POPUP_NOT_ALLOWED_TO_UPLOAD_UGC");
						OnToggle(BattleCreatorScreenState.Permissions, BattleCreatorState.Permissions, closeIfAlreadyOpen);
					}
				}
			});
		}

		private bool IsWindowOpenOrOpening(BattleCreatorState state)
		{
			if (state != m_LastState)
			{
				return false;
			}
			return m_State == State.Open;
		}

		private void OnToggle(BattleCreatorScreenState screenState, BattleCreatorState newState, object data = null, bool closeIfAlreadyOpen = true)
		{
			onToggleCount++;
			if (!closeIfAlreadyOpen && IsWindowOpenOrOpening(newState))
			{
				return;
			}
			foreach (KeyValuePair<BattleCreatorScreenState, IBattleCreatorMenu> campaignMenu in m_CampaignMenus)
			{
				campaignMenu.Value.Close();
				if (screenState == campaignMenu.Key)
				{
					campaignMenu.Value.Open(newState, data);
					this.PageOpened?.Invoke(screenState);
				}
			}
			this.UIClosed?.Invoke(newState);
		}
	}
}
