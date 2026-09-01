using System;
using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMPropertyLinkColor : MMPropertyLink
	{
		public Func<Color> GetColorDelegate;

		public Action<Color> SetColorDelegate;

		protected Color _initialValue;

		protected Color _newValue;

		protected Color _color;

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

		protected virtual Color GetValueOptimized(MMProperty property)
		{
			return default(Color);
		}

		protected virtual void SetValueOptimized(MMProperty property, Color newValue)
		{
		}
	}
}
