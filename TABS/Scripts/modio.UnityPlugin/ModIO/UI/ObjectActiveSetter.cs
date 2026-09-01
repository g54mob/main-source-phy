using UnityEngine;

namespace ModIO.UI
{
	public class ObjectActiveSetter : MonoBehaviour
	{
		public void ActiveOnlyIfNull(object o)
		{
			base.gameObject.SetActive(o == null);
		}

		public void ActiveOnlyIfNotNull(object o)
		{
			base.gameObject.SetActive(o != null);
		}
	}
}
