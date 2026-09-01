using System;
using Modding.Serialization;
using UnityEngine;

namespace Modding.Modules
{
	public abstract class BlockModuleBehaviour<TModule> : ModBlockBehaviour, IModuleBehaviour where TModule : BlockModule
	{
		[SerializeField]
		private string moduleGuid;

		public object RawModule { get; set; }

		public string ModuleGuid
		{
			get
			{
				return moduleGuid;
			}
			set
			{
				moduleGuid = value;
			}
		}

		public TModule Module
		{
			get
			{
				return (TModule)RawModule;
			}
		}

		public MKey GetKey(MKeyReference key)
		{
			MKey mKey = handler.GetMapperReference(key) as MKey;
			if (mKey == null)
			{
				throw new ArgumentException("Mapper Type with key " + key.Key + " is not an MKey!");
			}
			return mKey;
		}

		public MSlider GetSlider(MSliderReference slider)
		{
			MSlider mSlider = handler.GetMapperReference(slider) as MSlider;
			if (mSlider == null)
			{
				throw new ArgumentException("Mapper Type with key " + slider.Key + " is not an MSlider!");
			}
			return mSlider;
		}

		public MToggle GetToggle(MToggleReference toggle)
		{
			MToggle mToggle = handler.GetMapperReference(toggle) as MToggle;
			if (mToggle == null)
			{
				throw new ArgumentException("Mapper Type with key " + toggle.Key + " is not an MToggle!");
			}
			return mToggle;
		}

		public MValue GetValue(MValueReference value)
		{
			MValue mValue = handler.GetMapperReference(value) as MValue;
			if (mValue == null)
			{
				throw new ArgumentException("Mapper Type with key " + value.Key + " is not an MValue!");
			}
			return mValue;
		}

		public MColourSlider GetColourSlider(MColourSliderReference slider)
		{
			MColourSlider mColourSlider = handler.GetMapperReference(slider) as MColourSlider;
			if (mColourSlider == null)
			{
				throw new ArgumentException("Mapper Type with key " + slider.Key + " is not an MColourSlider!");
			}
			return mColourSlider;
		}

		public ModResource GetResource(ResourceReference reference)
		{
			return handler.GetResource(reference);
		}

		public virtual void OnReload()
		{
		}
	}
}
