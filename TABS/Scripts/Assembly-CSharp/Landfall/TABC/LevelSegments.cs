using System;
using UnityEngine;

namespace Landfall.TABC
{
	public class LevelSegments : MonoBehaviour
	{
		private void Start()
		{
			XPHandlerClient instance = XPHandlerClient.instance;
			instance.VisualLevelUpAction = (Action<int>)Delegate.Combine(instance.VisualLevelUpAction, new Action<int>(SetSegments));
		}

		public void SetSegments(int targetXPThisLevel)
		{
			for (int i = 0; i < base.transform.childCount; i++)
			{
				if (targetXPThisLevel.ToString() == base.transform.GetChild(i).name)
				{
					base.transform.GetChild(i).gameObject.SetActive(value: true);
				}
				else if (base.transform.GetChild(i).gameObject.activeSelf)
				{
					base.transform.GetChild(i).GetComponent<CodeAnimation>()?.PlayOut();
				}
			}
		}
	}
}
