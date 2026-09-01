using System;
using UnityEngine;

namespace Landfall.TABC
{
	public class ShopHandler : MonoBehaviour
	{
		public bool shopIsOpen;

		public Action openShopAction;

		public Action closeShopAction;

		public static ShopHandler instance;

		public UnitDatabase database;

		private UnitButton[] buttons;

		public Populate populate;

		public Action ShopRefreshAction;

		private void Awake()
		{
			instance = this;
			buttons = populate.DoPopulate<UnitButton>().ToArray();
		}

		private void Start()
		{
		}

		public void FillBar()
		{
			for (int i = 0; i < buttons.Length; i++)
			{
				buttons[i].SetUnitBlueprint(database.GetRandomUnit(), isOWned: false);
			}
		}

		public void Clear()
		{
			for (int i = 0; i < buttons.Length; i++)
			{
				buttons[i].Clear();
			}
		}

		public void ReRollButton()
		{
			if (WalletHandlerClient.instance.Spend(2))
			{
				Refresh();
			}
		}

		public void LevelUpButton()
		{
		}

		public void Refresh()
		{
			if (ShopRefreshAction != null)
			{
				ShopRefreshAction();
			}
			Clear();
			FillBar();
			OpenShop();
		}

		private void OpenShop()
		{
			if (!shopIsOpen)
			{
				openShopAction?.Invoke();
				shopIsOpen = true;
			}
		}

		private void CloseShop()
		{
			if (shopIsOpen)
			{
				closeShopAction?.Invoke();
				shopIsOpen = false;
			}
		}

		public void ToggleShop()
		{
			if (shopIsOpen)
			{
				CloseShop();
			}
			else
			{
				OpenShop();
			}
		}
	}
}
