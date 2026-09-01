using System;
using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMPropertyLinkVector3 : MMPropertyLink
	{
		public Func<Vector3> GetVector3Delegate;

		public Action<Vector3> SetVector3Delegate;

		protected Vector3 _initialValue;

		protected Vector3 _newValue;

		protected Vector3 _vector3;

		public override void Initialization(MMProperty property)
		{
		}

		public override void CreateGettersAndSetters(MMProperty property)
		{
		}

		public override object GetValue(MMPropertyEmitter emitter, MMProperty property)
		{
			return null;
		}

		public override void SetValue(MMPropertyReceiver receiver, MMProperty property, object newValue)
		{
		}

		public override float GetLevel(MMPropertyEmitter emitter, MMProperty property)
		{
			return 0f;
		}

		public override void SetLevel(MMPropertyReceiver receiver, MMProperty property, float level)
		{
		}

		protected virtual Vector3 GetValueOptimized(MMProperty property)
		{
			return default(Vector3);
		}

		protected virtual void SetValueOptimized(MMProperty property, Vector3 newValue)
		{
		}
	}
}
