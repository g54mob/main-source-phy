using System;

namespace MoreMountains.Tools
{
	public class MMPropertyLinkString : MMPropertyLink
	{
		public Func<string> GetStringDelegate;

		public Action<string> SetStringDelegate;

		protected string _initialValue;

		protected string _newValue;

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

		public override void SetLevel(MMPropertyReceiver receiver, MMProperty property, float level)
		{
		}

		protected virtual string GetValueOptimized(MMProperty property)
		{
			return null;
		}

		protected virtual void SetValueOptimized(MMProperty property, string newValue)
		{
		}
	}
}
