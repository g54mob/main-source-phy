using System;
using System.Collections.Generic;
using UnityEngine;

namespace Landfall.TABC
{
	public class PlayerPortraitHandler : MonoBehaviour
	{
		public Populate populate;

		public Transform portraitParent;

		private PlayePortrait[] playerPortraits;

		private bool inited;

		private void Start()
		{
			PlayerList instance = PlayerList.instance;
			instance.playerListUpdatedAction = (Action)Delegate.Combine(instance.playerListUpdatedAction, new Action(PlayerListWasUpdated));
		}

		public void Init(List<Client> players)
		{
			populate.times = players.Count - 1;
			populate.DoPopulate();
			playerPortraits = portraitParent.transform.parent.GetComponentsInChildren<PlayePortrait>();
		}

		public void PlayerListWasUpdated()
		{
			if (!inited)
			{
				Init(PlayerList.instance.playerList);
			}
			for (int i = 0; i < playerPortraits.Length; i++)
			{
				playerPortraits[i].PlayerInfoWasUpdated(PlayerList.instance.playerList[i].money, PlayerList.instance.playerList[i].health);
			}
		}
	}
}
