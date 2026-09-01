using System;
using JetBrains.Annotations;
using UnityEngine.Scripting;

namespace BitCode.Attributes
{
	[MeansImplicitUse]
	[AttributeUsage(AttributeTargets.Method)]
	public class DebugCommandAttribute : PreserveAttribute
	{
		public string Name;

		public string Description;
	}
}
