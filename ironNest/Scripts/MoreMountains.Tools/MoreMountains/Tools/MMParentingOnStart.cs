using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMParentingOnStart : MonoBehaviour
	{
		public enum Modes
		{
			Awake = 0,
			Start = 1,
			Script = 2
		}

		public Modes Mode;

		public Transform TargetParent;

		protected virtual void Awake()
		{
		}

		protected virtual void Start()
		{
		}

		public virtual void Parent()
		{
		}
	}
}
