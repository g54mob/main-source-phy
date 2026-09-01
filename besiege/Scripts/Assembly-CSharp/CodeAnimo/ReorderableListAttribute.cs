using System;
using UnityEngine;

namespace CodeAnimo
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true)]
	public class ReorderableListAttribute : PropertyAttribute
	{
	}
}
