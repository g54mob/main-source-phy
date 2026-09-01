using System;
using TMPro;
using UnityEngine;

namespace Landfall.TABC
{
	public class MoneyCoin : MonoBehaviour
	{
		private TextMeshProUGUI text;

		private void Awake()
		{
			text = GetComponentInChildren<TextMeshProUGUI>();
		}

		private void Start()
		{
			WalletHandlerClient instance = WalletHandlerClient.instance;
			instance.moneyWasUpdatedAction = (Action<bool>)Delegate.Combine(instance.moneyWasUpdatedAction, new Action<bool>(UpdateMoney));
			UpdateMoney();
		}

		public void UpdateMoney(bool positive = true)
		{
			GetComponent<ScaleShake>().AddForce(positive ? 0.15f : (-0.15f));
			text.text = WalletHandlerClient.instance.money.ToString();
		}
	}
}
