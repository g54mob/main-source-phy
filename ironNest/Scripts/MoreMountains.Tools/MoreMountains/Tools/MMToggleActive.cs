using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMToggleActive : MonoBehaviour
	{
		[Header("Target - leave empty for self")]
		public GameObject TargetGameObject;

		[MMInspectorButton("ToggleActive")]
		public bool ToggleActiveButton;

		protected virtual void Awake()
		{
		}

		public virtual void ToggleActive()
		{
		}
	}
}
