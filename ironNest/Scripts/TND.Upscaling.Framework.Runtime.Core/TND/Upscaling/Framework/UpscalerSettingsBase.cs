using System;
using UnityEngine;

namespace TND.Upscaling.Framework
{
	[Serializable]
	public abstract class UpscalerSettingsBase : ScriptableObject
	{
		public virtual bool RestartRequired()
		{
			return false;
		}

		public virtual void UpdateCachedValues()
		{
		}

		protected virtual void Awake()
		{
		}
	}
}
