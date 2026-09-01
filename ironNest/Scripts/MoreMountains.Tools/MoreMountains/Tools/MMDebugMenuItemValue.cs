using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.Tools
{
	public class MMDebugMenuItemValue : MonoBehaviour
	{
		[Header("Bindings")]
		public Text LabelText;

		public Text ValueText;

		public MMRadioReceiver RadioReceiver;

		protected float _level;

		public virtual float Level
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}
	}
}
