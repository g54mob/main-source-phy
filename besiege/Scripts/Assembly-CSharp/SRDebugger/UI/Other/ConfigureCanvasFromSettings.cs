using SRDebugger.Internal;
using SRF;
using UnityEngine;

namespace SRDebugger.UI.Other
{
	[RequireComponent(typeof(Canvas))]
	public class ConfigureCanvasFromSettings : SRMonoBehaviour
	{
		private void Start()
		{
			Canvas component = GetComponent<Canvas>();
			SRDebuggerUtil.ConfigureCanvas(component);
			Object.Destroy(this);
		}
	}
}
