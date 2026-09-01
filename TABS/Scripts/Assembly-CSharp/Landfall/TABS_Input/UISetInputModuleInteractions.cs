using InControl;
using UnityEngine;

namespace Landfall.TABS_Input
{
	public class UISetInputModuleInteractions : MonoBehaviour
	{
		private void Awake()
		{
			_ = Object.FindObjectOfType<InControlInputModule>() != null;
			Object.Destroy(base.gameObject);
		}
	}
}
