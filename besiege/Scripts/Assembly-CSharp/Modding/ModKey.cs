using System;
using UnityEngine;

namespace Modding
{
	public class ModKey
	{
		internal KeyCode Modifier;

		internal KeyCode Trigger;

		internal KeyCode RealTrigger;

		public bool IsDown
		{
			get
			{
				return CM(Input.GetKey) && CT(Input.GetKey);
			}
		}

		public bool IsPressed
		{
			get
			{
				return CM(Input.GetKey) && CT(Input.GetKeyDown);
			}
		}

		public bool IsReleased
		{
			get
			{
				return (CM(Input.GetKey) && CT(Input.GetKeyUp)) || (CM(Input.GetKeyUp) && CT(Input.GetKey)) || (CM(Input.GetKeyUp) && CT(Input.GetKeyUp));
			}
		}

		private bool CM(Predicate<KeyCode> f)
		{
			return Modifier == KeyCode.None || f(Modifier);
		}

		private bool CT(Predicate<KeyCode> f)
		{
			return Trigger != KeyCode.None && f(Trigger);
		}

		public void Change(KeyCode modifier, KeyCode trigger)
		{
			Modifier = modifier;
			Trigger = trigger;
		}

		public void Change(ControlScheme.ControlOption option)
		{
			KeyCode modifier = KeyCode.None;
			KeyCode trigger = KeyCode.None;
			KeyCode[] keys = option.Keys;
			if (keys.Length <= 0)
			{
				Debug.LogWarning("No keys set for mod key change");
			}
			else if (keys.Length == 1)
			{
				trigger = keys[0];
			}
			else
			{
				modifier = keys[0];
				trigger = keys[1];
			}
			Change(modifier, trigger);
		}
	}
}
