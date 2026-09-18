using UnityEngine;

namespace Synty.Interface.FantasyMenus.Samples
{
	public class SampleButtonDescriptions : MonoBehaviour
	{
		public void SetActive(GameObject description)
		{
			for (int i = 0; i < base.transform.childCount; i++)
			{
				Transform child = base.transform.GetChild(i);
				child.gameObject.SetActive(child.gameObject == description);
			}
		}
	}
}
