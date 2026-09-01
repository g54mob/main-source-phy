using System;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace MoreMountains.Tools
{
	[Serializable]
	public class MMInputExecutionBinding
	{
		public Key TargetInputKey;

		public UnityEvent OnKeyDown;

		public UnityEvent OnKey;

		public UnityEvent OnKeyUp;

		public virtual void ProcessInput()
		{
		}
	}
}
