using System;
using UnityEngine;
using UnityEngine.Events;

namespace Kamgam.SettingsGenerator
{
	public abstract class SettingEvent : SettingResolver
	{
		public bool TriggerOnStart;

		public bool TriggerOnEnable;

		[Tooltip("If set to false then this event will not trigger if the gameobject or component is disabled.\n\nNOTICE: The event registers itself in OnEnable() so if the object starts disabled then it will NEVER be triggered not matter what this was set to.")]
		public bool TriggerIfDisabled;

		[NonSerialized]
		protected SettingData.DataType[] _supportedDataTypes;

		public abstract override SettingData.DataType[] GetSupportedDataTypes();

		public ISetting GetSetting()
		{
			return null;
		}

		public override void Start()
		{
		}

		public override void OnEnable()
		{
		}

		public override void OnDisable()
		{
		}

		public void Register()
		{
		}

		public void UnRegister()
		{
		}

		protected virtual void onChanged(ISetting setting)
		{
		}

		public override void Refresh()
		{
		}

		public virtual bool shoudTrigger()
		{
			return false;
		}

		public abstract void TriggerEvent();
	}
	public abstract class SettingEvent<T> : SettingEvent
	{
		[Space(10f)]
		public UnityEvent<T> OnValueChanged;

		public void Log(T value)
		{
		}
	}
}
