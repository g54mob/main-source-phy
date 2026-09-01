using System;

namespace MoreMountains.Tools
{
	public class MMPropertyLinkFloat : MMPropertyLink
	{
		public Func<float> GetFloatDelegate;

		public Action<float> SetFloatDelegate;

		protected float _initialValue;

		protected float _newValue;

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

		protected virtual float GetValueOptimized(MMProperty property)
		{
			return 0f;
		}

		protected virtual void SetValueOptimized(MMProperty property, float newValue)
		{
		}
	}
}
