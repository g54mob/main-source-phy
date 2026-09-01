using System;

namespace Modding.Mapper
{
	public abstract class MCustom<T> : MapperType
	{
		protected T value;

		protected T loadValue;

		protected T defaultValue;

		public virtual T Value
		{
			get
			{
				return value;
			}
			set
			{
				this.value = value;
				InvokeChanged(value);
			}
		}

		protected string SerializationKey
		{
			get
			{
				return "bmt-" + base.Key;
			}
		}

		public override bool isDefaultValue
		{
			get
			{
				return value.Equals(defaultValue);
			}
		}

		public event Action<T> Changed;

		protected MCustom(string displayName, string key, T defaultValue)
			: base(displayName, key)
		{
			value = (loadValue = (this.defaultValue = defaultValue));
			InvokeChanged(value);
			base.defaultData = SerializeValue(defaultValue);
		}

		public abstract XData SerializeValue(T value);

		public abstract T DeSerializeValue(XData data);

		public override void ResetDefaults()
		{
			value = defaultValue;
		}

		public override void ResetValue()
		{
			value = loadValue;
		}

		public override void ApplyValue()
		{
			loadValue = value;
			InvokeChanged(value);
		}

		public override XData Serialize()
		{
			return SerializeValue(value);
		}

		public override XData SerializeDefault()
		{
			return SerializeValue(defaultValue);
		}

		public override XData SerializeLoadValue()
		{
			return SerializeValue(loadValue);
		}

		public override void DeSerialize(XData raw)
		{
			value = (loadValue = DeSerializeValue(raw));
			InvokeChanged(value);
		}

		public virtual bool ValueEquals(MCustom<T> other)
		{
			return other.Serialize().RawValue == Serialize().RawValue;
		}

		public sealed override bool CompareValue(MapperType other)
		{
			if (other.Key != base.Key)
			{
				return false;
			}
			MCustom<T> mCustom = other as MCustom<T>;
			return mCustom != null && ValueEquals(mCustom);
		}

		public virtual void InvokeChanged(T value)
		{
			Action<T> changed = this.Changed;
			if (changed != null)
			{
				changed(value);
			}
		}
	}
}
