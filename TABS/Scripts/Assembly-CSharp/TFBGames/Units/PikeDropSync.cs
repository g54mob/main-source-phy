using Photon.Bolt;
using UnityEngine;

namespace TFBGames.Units
{
	public class PikeDropSync : MonoBehaviour
	{
		public delegate void dropEventHandler();

		private DelayEvent delayEvent;

		public event dropEventHandler dropEvent;

		private void Start()
		{
			delayEvent = base.gameObject.GetComponent<DelayEvent>();
		}

		public void Drop()
		{
			if (delayEvent == null)
			{
				Debug.LogError("Delay Event object was not found while PikeDropSync is trying to access it.");
			}
			else
			{
				delayEvent.Go();
			}
		}

		public void OnDealDamageEvent()
		{
			if (CampaignPlayerDataHolder.CurrentGameModeState == GameModeState.OnlineMultiplayer)
			{
				if (BoltNetwork.IsServer)
				{
					delayEvent.Go();
					this.dropEvent?.Invoke();
				}
			}
			else
			{
				delayEvent.Go();
			}
		}
	}
}
