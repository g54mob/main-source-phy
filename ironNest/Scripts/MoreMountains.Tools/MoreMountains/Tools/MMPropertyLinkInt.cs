using System;

namespace MoreMountains.Tools
{
	public class MMPropertyLinkInt : MMPropertyLink
	{
		public Func<int> GetIntDelegate;

		public Action<int> SetIntDelegate;

		protected int _initialValue;

		protected int _newValue;

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

		protected virtual int GetValueOptimized(MMProperty property)
		{
			return 0;
		}

		protected virtual void SetValueOptimized(MMProperty property, int newValue)
		{
		}
	}
}
