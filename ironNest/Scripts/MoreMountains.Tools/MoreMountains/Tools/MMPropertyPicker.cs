using System;
using UnityEngine;

namespace MoreMountains.Tools
{
	[Serializable]
	public class MMPropertyPicker
	{
		public UnityEngine.Object TargetObject;

		public Component TargetComponent;

		public ScriptableObject TargetScriptableObject;

		public string TargetPropertyName;

		protected MMProperty _targetMMProperty;

		protected bool _initialized;

		protected MMPropertyLink _propertySetter;

		public virtual bool PropertyFound { get; protected set; }

		public virtual void Initialization(GameObject source)
		{
		}

		public virtual object GetRawValue()
		{
			return null;
		}
	}
}
