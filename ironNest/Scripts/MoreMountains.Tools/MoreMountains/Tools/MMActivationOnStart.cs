using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Activation/MMActivationOnStart")]
	public class MMActivationOnStart : MonoBehaviour
	{
		public enum Modes
		{
			Awake = 0,
			Start = 1
		}

		public Modes Mode;

		public bool StateOnStart;

		public List<GameObject> TargetObjects;

		protected virtual void Awake()
		{
		}

		protected virtual void Start()
		{
		}

		protected virtual void SetState()
		{
		}
	}
}
