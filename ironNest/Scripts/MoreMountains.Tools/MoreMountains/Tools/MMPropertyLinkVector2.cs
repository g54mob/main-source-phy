using System;
using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMPropertyLinkVector2 : MMPropertyLink
	{
		public Func<Vector2> GetVector2Delegate;

		public Action<Vector2> SetVector2Delegate;

		protected Vector2 _initialValue;

		protected Vector2 _newValue;

		protected Vector2 _vector2;

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

		protected virtual Vector2 GetValueOptimized(MMProperty property)
		{
			return default(Vector2);
		}

		protected virtual void SetValueOptimized(MMProperty property, Vector2 newValue)
		{
		}
	}
}
