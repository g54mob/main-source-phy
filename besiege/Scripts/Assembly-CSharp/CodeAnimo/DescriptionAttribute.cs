using System;
using UnityEngine;

namespace CodeAnimo
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true)]
	public class DescriptionAttribute : PropertyAttribute
	{
		public float height = 250f;
	}
}
