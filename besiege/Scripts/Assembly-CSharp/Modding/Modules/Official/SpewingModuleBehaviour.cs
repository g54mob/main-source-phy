using System;
using InternalModding.Misc;
using UnityEngine;

namespace Modding.Modules.Official
{
	public class SpewingModuleBehaviour : BlockModuleBehaviour<SpewingModule>
	{
		private ParticleHelper.ParticleSystemsInformation ParticleInfo;

		private bool isActive;

		private float baseAmmo;

		private float time;

		private bool timeOut;

		private float timeSinceToggle;

		private bool keyHeld;

		private MKey igniteKey;

		private MToggle holdToFireToggle;

		private MSlider rangeSlider;

		private bool igniteHeld;

		private bool emuIgniteHeld;

		public override void SafeAwake()
		{
			try
			{
				igniteKey = GetKey(base.Module.TriggerKey);
				holdToFireToggle = GetToggle(base.Module.HoldToFireToggle);
				rangeSlider = GetSlider(base.Module.RangeSlider);
			}
			catch (Exception ex)
			{
				MLog.Error("Could not get all mapper types for Spewing Module! Module will be disabled.");
				MLog.Error(ex.ToString());
				UnityEngine.Object.Destroy(this);
				return;
			}
			baseAmmo = base.Module.BaseAmmo;
			if (base.IsSimulating)
			{
				ParticleInfo = ParticleHelper.CreateParticleSystems(base.transform, this, base.Module.ParticleSystems);
				time += baseAmmo;
			}
			rangeSlider.ValueChanged += delegate(float value)
			{
				if (base.IsSimulating)
				{
					ParticleHelper.SetParticleRange(ParticleInfo, this, value);
				}
			};
		}

		public override void OnReload()
		{
			if (base.IsSimulating)
			{
				baseAmmo = base.Module.BaseAmmo;
				if (time < baseAmmo)
				{
					timeOut = false;
				}
				ParticleInfo = ParticleHelper.OnReloadModule(base.transform, this, base.Module.ParticleSystems, ParticleInfo);
				ParticleHelper.SetParticleRange(ParticleInfo, this, rangeSlider.Value);
				if (isActive)
				{
					ParticleHelper.ParticlesOn(ParticleInfo, base.SimPhysics);
				}
			}
		}

		public override void OnReloadAmmo(ref int units, ReloadAmmoType type, bool setAmmo, bool eachBlock)
		{
			if (type != ReloadAmmoType.All && (!base.Module.AcceptFireAmmo || type != ReloadAmmoType.Fire))
			{
				return;
			}
			if (setAmmo)
			{
				if (eachBlock || (float)units < baseAmmo)
				{
					time = (float)units * 0.25f;
					units = 0;
				}
				else
				{
					units -= (int)baseAmmo;
					time = baseAmmo;
				}
			}
			else if (eachBlock || (float)units <= baseAmmo - time)
			{
				time += (float)units * 0.25f;
				units = 0;
			}
			else
			{
				float num = (baseAmmo - time) * 4f;
				units -= (int)num;
				time = baseAmmo;
			}
			timeOut = time <= 0f;
		}

		public override void KeyEmulationUpdate()
		{
			emuIgniteHeld = igniteKey.EmulationHeld(true);
			HandleIgniteKey(igniteKey.EmulationPressed(), emuIgniteHeld || igniteHeld);
		}

		public override void SimulateUpdateAlways()
		{
			if (!holdToFireToggle.IsActive && base.Module.ToggleTimeLimitSpecified)
			{
				if (timeSinceToggle >= base.Module.ToggleTimeLimit)
				{
					ToggleParticles();
					timeSinceToggle = 0f;
				}
				if (isActive)
				{
					timeSinceToggle += Time.deltaTime;
				}
			}
			igniteHeld = igniteKey.IsHeld;
			HandleIgniteKey(igniteKey.IsPressed, igniteHeld || emuIgniteHeld);
			if (!timeOut)
			{
				if (isActive)
				{
					time -= Time.deltaTime;
				}
				if (time <= 0f)
				{
					TimeOut();
				}
			}
		}

		private void HandleIgniteKey(bool pressed, bool held)
		{
			if (!holdToFireToggle.IsActive)
			{
				if (pressed)
				{
					ToggleParticles();
					timeSinceToggle = 0f;
				}
			}
			else if (held)
			{
				if (!keyHeld)
				{
					keyHeld = true;
					ParticlesOn();
				}
			}
			else if (keyHeld)
			{
				keyHeld = false;
				ParticlesOff();
			}
		}

		private void ToggleParticles()
		{
			if (isActive)
			{
				ParticlesOff();
			}
			else
			{
				ParticlesOn();
			}
		}

		private void ParticlesOn()
		{
			if (base.Machine.InfiniteAmmo)
			{
				StatMaster.GodTools.HasBeenUsed = true;
			}
			else if (timeOut)
			{
				return;
			}
			isActive = true;
			ParticleHelper.ParticlesOn(ParticleInfo, base.SimPhysics);
		}

		private void ParticlesOff()
		{
			isActive = false;
			ParticleHelper.ParticlesOff(ParticleInfo, base.SimPhysics);
		}

		private void TimeOut()
		{
			if (!timeOut)
			{
				time = 0f;
				timeOut = true;
				if (!base.Machine.InfiniteAmmo)
				{
					ParticlesOff();
				}
			}
		}
	}
}
