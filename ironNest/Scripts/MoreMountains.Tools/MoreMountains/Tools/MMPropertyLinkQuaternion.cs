using System;
using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMPropertyLinkQuaternion : MMPropertyLink
	{
		public Func<Quaternion> GetQuaternionDelegate;

		public Action<Quaternion> SetQuaternionDelegate;

		protected Quaternion _initialValue;

		protected Quaternion _newValue;

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

		protected virtual Quaternion GetValueOptimized(MMProperty property)
		{
			return default(Quaternion);
		}

		protected virtual void SetValueOptimized(MMProperty property, Quaternion newValue)
		{
		}
	}
}
