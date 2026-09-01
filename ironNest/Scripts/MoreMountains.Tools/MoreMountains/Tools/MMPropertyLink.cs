namespace MoreMountains.Tools
{
	public abstract class MMPropertyLink
	{
		protected bool _getterSetterInitialized;

		public virtual void Initialization(MMProperty property)
		{
		}

		public virtual void CreateGettersAndSetters(MMProperty property)
		{
		}

		public virtual float GetLevel(MMPropertyEmitter emitter, MMProperty property)
		{
			return 0f;
		}

		public virtual void SetLevel(MMPropertyReceiver receiver, MMProperty property, float level)
		{
		}

		public virtual object GetValue(MMPropertyEmitter emitter, MMProperty property)
		{
			return null;
		}

		public virtual void SetValue(MMPropertyReceiver receiver, MMProperty property, object newValue)
		{
		}

		public virtual object GetPropertyValue(MMProperty property)
		{
			return null;
		}

		protected virtual void SetPropertyValue(MMProperty property, object newValue)
		{
		}
	}
}
