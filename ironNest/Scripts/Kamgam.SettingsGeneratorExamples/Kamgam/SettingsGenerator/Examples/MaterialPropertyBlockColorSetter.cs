using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kamgam.SettingsGenerator.Examples
{
	public class MaterialPropertyBlockColorSetter : MonoBehaviour
	{
		public Renderer Renderer;

		public int MaterialIndex;

		[NonSerialized]
		protected Dictionary<string, Color> _scheduledColors;

		[NonSerialized]
		protected MaterialPropertyBlock _propertyBlock;

		private readonly string[] _colorPropertyNames;

		public void Init()
		{
		}

		public Material GetSharedMaterial()
		{
			return null;
		}

		public bool HasScheduledChanges()
		{
			return false;
		}

		public bool HasScheduledProperty(string propertyName)
		{
			return false;
		}

		protected void schedule<T>(ref Dictionary<string, T> dict, string propertyName, T value)
		{
		}

		protected void addOrUpdateScheduled<T>(Dictionary<string, T> source, string propertyName, T value)
		{
		}

		protected T getScheduled<T>(Dictionary<string, T> dict, string propertyName, T defaultValue)
		{
			return default(T);
		}

		protected Color getProperty(string propertyName, Color defaultValue = default(Color))
		{
			return default(Color);
		}

		protected bool hasColorProperty(string propertyName)
		{
			return false;
		}

		protected T get<T>(Dictionary<string, T> dict, string propertyName, Func<string, T, T> propertyGetter, T defaultValue = default(T))
		{
			return default(T);
		}

		public void ScheduleColor(string propertyName, Color color)
		{
		}

		public Color GetScheduledColor(string propertyName, Color defaultValue = default(Color))
		{
			return default(Color);
		}

		public Color GetPropertyColor(string propertyName, Color defaultValue = default(Color))
		{
			return default(Color);
		}

		public Color GetColor(string propertyName, Color defaultValue = default(Color))
		{
			return default(Color);
		}

		protected void applyList<T>(Dictionary<string, T> dict, Action<string, T> setter)
		{
		}

		public void Apply()
		{
		}

		public void ClearScheduled()
		{
		}

		public void ResetProperties()
		{
		}

		public void SetMainColor(Color color)
		{
		}

		public string GetMainColorPropertyName()
		{
			return null;
		}
	}
}
