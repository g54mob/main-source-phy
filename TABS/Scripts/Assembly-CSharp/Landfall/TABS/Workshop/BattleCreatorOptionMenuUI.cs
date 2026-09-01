using System;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABS.Workshop
{
	public class BattleCreatorOptionMenuUI : MonoBehaviour
	{
		[SerializeField]
		private Button m_EditButton;

		[SerializeField]
		private Button m_UploadButton;

		[SerializeField]
		private Button m_RenameButton;

		[SerializeField]
		private Button m_RemoveButton;

		private BattleCreatorAssetUICellBase m_CurrentCell;

		private BattleCreatorState m_CurrentState;

		public void Open(BattleCreatorAssetUICellBase cell, BattleCreatorState state, Action onEdit = null, Action onUpload = null, Action onRename = null, Action onRemove = null)
		{
			if (cell == m_CurrentCell)
			{
				Close();
				return;
			}
			m_CurrentCell = cell;
			m_CurrentState = state;
			Vector3 position = cell.transform.position;
			Vector3 position2 = base.transform.position;
			position2.y = position.y;
			base.transform.position = position2;
			Debug.Log("Opening Edit Menu for cell: " + cell.ContentName);
			ActivateButtons();
			AssignListeners(onEdit, onUpload, onRename, onRemove);
			base.gameObject.SetActive(value: true);
		}

		private void ActivateButtons()
		{
			m_EditButton.gameObject.SetActive(value: true);
			m_UploadButton.gameObject.SetActive(value: true);
			m_RenameButton.gameObject.SetActive(value: true);
			m_RemoveButton.gameObject.SetActive(value: true);
		}

		private void RemoveListeners()
		{
			m_EditButton.onClick.RemoveAllListeners();
			m_UploadButton.onClick.RemoveAllListeners();
			m_RenameButton.onClick.RemoveAllListeners();
			m_RemoveButton.onClick.RemoveAllListeners();
		}

		private void AssignListeners(Action onEdit = null, Action onUpload = null, Action onRename = null, Action onRemove = null)
		{
			RemoveListeners();
			switch (m_CurrentState)
			{
			case BattleCreatorState.Load:
				m_EditButton.gameObject.SetActive(value: false);
				break;
			case BattleCreatorState.Upload:
				m_UploadButton.gameObject.SetActive(value: false);
				break;
			}
			switch (m_CurrentCell.ContentType)
			{
			case ContentTypeFilter.Battles:
				if (m_CurrentCell.LevelAsset.IsModIOLevel)
				{
					m_RenameButton.gameObject.SetActive(value: false);
				}
				m_EditButton.onClick.AddListener(delegate
				{
					BattleCreatorSharedCommands.LoadContent(m_CurrentCell, onEdit);
				});
				break;
			case ContentTypeFilter.Campaigns:
				if (m_CurrentCell.CampaignAsset.IsModCampaign)
				{
					m_RenameButton.gameObject.SetActive(value: false);
				}
				m_EditButton.onClick.AddListener(delegate
				{
					BattleCreatorSharedCommands.LoadContent(m_CurrentCell, onEdit);
				});
				break;
			}
			m_UploadButton.onClick.AddListener(delegate
			{
				BattleCreatorSharedCommands.OpenUploadScreen(m_CurrentCell);
			});
			m_RemoveButton.onClick.AddListener(delegate
			{
				BattleCreatorSharedCommands.DeleteContent(m_CurrentCell, onRemove);
			});
			if (onRename != null)
			{
				m_EditButton.onClick.AddListener(delegate
				{
					onRename();
				});
			}
			m_EditButton.onClick.AddListener(Close);
			m_UploadButton.onClick.AddListener(Close);
			m_RenameButton.onClick.AddListener(Close);
			m_RemoveButton.onClick.AddListener(Close);
		}

		public void Close()
		{
			base.gameObject.SetActive(value: false);
			m_CurrentCell = null;
		}
	}
}
