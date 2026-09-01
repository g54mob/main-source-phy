using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class OnInspectorGUIAttribute : ShowInInspectorAttribute
	{
		public string PrependMethodName;

		public string AppendMethodName;

		public OnInspectorGUIAttribute()
		{
		}

		public OnInspectorGUIAttribute(string methodName, bool append = true)
		{
			if (append)
			{
				AppendMethodName = methodName;
			}
			else
			{
				PrependMethodName = methodName;
			}
		}

		public OnInspectorGUIAttribute(string prependMethodName, string appendMethodName)
		{
			PrependMethodName = prependMethodName;
			AppendMethodName = appendMethodName;
		}
	}
}
