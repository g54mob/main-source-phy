using System;
using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMPropertyLinkVector4 : MMPropertyLink
	{
		public Func<Vector4> GetVector4Delegate;

		public Action<Vector4> SetVector4Delegate;

		protected Vector4 _initialValue;

		protected Vector4 _newValue;

		protected Vector4 _vector4;

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

		protected virtual Vector4 GetValueOptimized(MMProperty property)
		{
			return default(Vector4);
		}

		protected virtual void SetValueOptimized(MMProperty property, Vector4 newValue)
		{
		}
	}
}
