using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.Feedbacks
{
	[ExecuteAlways]
	[AddComponentMenu(null)]
	public class DemoButton : MonoBehaviour
	{
		[Header("Behaviour")]
		public bool NotSupportedInWebGL;

		[Header("Bindings")]
		public Button TargetButton;

		public Text ButtonText;

		public Text WebGL;

		public MMF_Player TargetMMF_Player;

		protected Color _disabledColor;

		protected virtual void OnEnable()
		{
		}

		protected void OnDisable()
		{
		}

		public void OnClickEvent()
		{
		}

		protected virtual void HandleWebGL()
		{
		}
	}
}
