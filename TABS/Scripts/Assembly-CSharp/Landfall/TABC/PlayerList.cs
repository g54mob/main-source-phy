using System;
using System.Collections.Generic;
using UnityEngine;

namespace Landfall.TABC
{
	public class PlayerList : MonoBehaviour
	{
		public static PlayerList instance;

		public List<Client> playerList = new List<Client>();

		public Action playerListUpdatedAction;

		private void Awake()
		{
			instance = this;
		}

		public void UpdatePlayerList(List<Client> newClientList)
		{
			playerList = newClientList;
			playerListUpdatedAction?.Invoke();
		}
	}
}
