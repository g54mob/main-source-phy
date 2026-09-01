using System;
using UnityEngine;
using UnityEngine.Events;

namespace Landfall.TABC
{
	public class ShopHandlerUI : MonoBehaviour
	{
		public UnityEvent openShopEvent;

		public UnityEvent closeShopEvent;

		private void Awake()
		{
		}

		private void Start()
		{
			ShopHandler instance = ShopHandler.instance;
			instance.openShopAction = (Action)Delegate.Combine(instance.openShopAction, new Action(OpenShop));
			ShopHandler instance2 = ShopHandler.instance;
			instance2.closeShopAction = (Action)Delegate.Combine(instance2.closeShopAction, new Action(CloseShop));
		}

		private void OpenShop()
		{
			openShopEvent.Invoke();
		}

		private void CloseShop()
		{
			closeShopEvent.Invoke();
		}
	}
}
