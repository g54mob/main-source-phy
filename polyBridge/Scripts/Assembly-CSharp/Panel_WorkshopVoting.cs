using Steamworks.Ugc;
using UnityEngine;
using UnityEngine.UI;

public class Panel_WorkshopVoting : MonoBehaviour
{
	[Header("ThumbsUp")]
	public Button m_ThumbsUpButton;

	public Image m_ThumbsUpIcon;

	public Color m_ThumbsUpIconColor;

	[Header("ThumbsDown")]
	public Button m_ThumbsDownButton;

	public Image m_ThumbsDownIcon;

	public Color m_ThumbsDownIconColor;

	[Header("Favoriting")]
	public Button m_FavoriteButton;

	public Button m_FavoritedButton;

	private WorkshopItemVoteType m_UserItemVote;

	private bool m_UserItemFavorited;

	private UserItemVote? m_SteamUserItemVote;

	private bool m_SteamUserItemFavorited;

	private void Start()
	{
		m_ThumbsUpButton.onClick.AddListener(OnThumbsUp);
		m_ThumbsDownButton.onClick.AddListener(OnThumbsDown);
		m_FavoriteButton.onClick.AddListener(OnFavorite);
		m_FavoritedButton.onClick.AddListener(OnFavorited);
	}

	public void UpdateFavoriteButtons(WorkshopItem item)
	{
		bool flag = WorkshopItemFavorites.m_Favorites.Contains(item.GetIdAsUlong());
		EnableFavoriteButtons(flag);
		m_UserItemFavorited = flag;
		m_SteamUserItemFavorited = flag;
	}

	public async void UpdateVoteIcons(WorkshopItem item)
	{
		string id = item.GetId();
		if (WorkshopItemVotes.m_Votes.ContainsKey(id))
		{
			SetVoteIcons(WorkshopItemVotes.m_Votes[id]);
		}
		else
		{
			SetVoteIcons(WorkshopItemVoteType.NONE);
		}
		m_SteamUserItemVote = await item.m_SteamItem.GetUserVote();
		m_UserItemVote = GetVoteTypeFromSteamVote(m_SteamUserItemVote);
		SetVoteIcons(m_UserItemVote);
	}

	public void MaybeWriteWorkshopItemVotes(WorkshopItem item)
	{
		string id = item.GetId();
		if (WorkshopItemVotes.m_Votes.ContainsKey(id))
		{
			if (WorkshopItemVotes.m_Votes[id] != m_UserItemVote)
			{
				WorkshopItemVotes.m_Votes[id] = m_UserItemVote;
				WorkshopItemVotes.Save();
			}
		}
		else
		{
			WorkshopItemVotes.m_Votes.Add(id, m_UserItemVote);
			WorkshopItemVotes.Save();
		}
	}

	public async void MaybeSyncVoteToSteam(WorkshopItem item)
	{
		if (m_SteamUserItemVote.HasValue && GetVoteTypeFromSteamVote(m_SteamUserItemVote.Value) != m_UserItemVote && m_UserItemVote != WorkshopItemVoteType.NONE)
		{
			await item.m_SteamItem.Vote(m_UserItemVote == WorkshopItemVoteType.UP);
		}
	}

	public void MaybeWriteWorkshopItemFavorite(WorkshopItem item)
	{
		ulong idAsUlong = item.GetIdAsUlong();
		if (WorkshopItemFavorites.m_Favorites.Contains(idAsUlong) && !m_UserItemFavorited)
		{
			WorkshopItemFavorites.m_Favorites.Remove(idAsUlong);
			WorkshopItemFavorites.Save();
		}
		if (!WorkshopItemFavorites.m_Favorites.Contains(idAsUlong) && m_UserItemFavorited)
		{
			WorkshopItemFavorites.m_Favorites.Add(idAsUlong);
			WorkshopItemFavorites.Save();
		}
	}

	public async void MaybeSyncFavoriteToSteam(WorkshopItem item)
	{
		if (m_SteamUserItemFavorited != m_UserItemFavorited)
		{
			if (m_UserItemFavorited)
			{
				await item.m_SteamItem.AddFavorite();
			}
			else
			{
				await item.m_SteamItem.RemoveFavorite();
			}
		}
	}

	private void OnThumbsUp()
	{
		InterfaceAudio.Play("ui_menu_select");
		m_UserItemVote = WorkshopItemVoteType.UP;
		SetVoteIcons(WorkshopItemVoteType.UP);
	}

	private void OnThumbsDown()
	{
		InterfaceAudio.Play("ui_menu_select");
		m_UserItemVote = WorkshopItemVoteType.DOWN;
		SetVoteIcons(WorkshopItemVoteType.DOWN);
	}

	private void OnFavorite()
	{
		InterfaceAudio.Play("ui_menu_select");
		m_UserItemFavorited = true;
		EnableFavoriteButtons(m_UserItemFavorited);
	}

	private void OnFavorited()
	{
		InterfaceAudio.Play("ui_menu_select");
		m_UserItemFavorited = false;
		EnableFavoriteButtons(m_UserItemFavorited);
	}

	private void SetVoteIcons(WorkshopItemVoteType vote)
	{
		m_ThumbsUpIcon.color = ((vote == WorkshopItemVoteType.UP) ? m_ThumbsUpIconColor : Color.white);
		m_ThumbsDownIcon.color = ((vote == WorkshopItemVoteType.DOWN) ? m_ThumbsDownIconColor : Color.white);
	}

	private void EnableFavoriteButtons(bool favorited)
	{
		m_FavoritedButton.gameObject.SetActive(favorited);
		m_FavoriteButton.gameObject.SetActive(!favorited);
	}

	private WorkshopItemVoteType GetVoteTypeFromSteamVote(UserItemVote? userItemVote)
	{
		if (userItemVote.HasValue && userItemVote.Value.VotedUp)
		{
			return WorkshopItemVoteType.UP;
		}
		if (userItemVote.HasValue && userItemVote.Value.VotedDown)
		{
			return WorkshopItemVoteType.DOWN;
		}
		return WorkshopItemVoteType.NONE;
	}
}
