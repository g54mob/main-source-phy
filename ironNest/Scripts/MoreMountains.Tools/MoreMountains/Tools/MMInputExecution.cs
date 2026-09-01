using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMInputExecution : MonoBehaviour
	{
		[Header("Bindings")]
		public List<MMInputExecutionBinding> Bindings;

		protected virtual void Update()
		{
		}

		protected virtual void HandleInput()
		{
		}
	}
}
