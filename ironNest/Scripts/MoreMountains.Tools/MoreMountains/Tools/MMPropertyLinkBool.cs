using System;

namespace MoreMountains.Tools
{
	public class MMPropertyLinkBool : MMPropertyLink
	{
		public Func<bool> GetBoolDelegate;

		public Action<bool> SetBoolDelegate;

		protected bool _initialValue;

		protected bool _newValue;

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

		protected virtual bool GetValueOptimized(MMProperty property)
		{
			return false;
		}

		protected virtual void SetValueOptimized(MMProperty property, bool newValue)
		{
		}
	}
}
