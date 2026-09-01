using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Kamgam.SettingsGenerator
{
	public class SettingsCheckForUnapplied : MonoBehaviour
	{
		[NonSerialized]
		private static List<SettingsCheckForUnapplied> _registry;

		[NonSerialized]
		private static int _lastCheckFrame;

		[Tooltip("Turns of the check. If disabled then this component will do nothing.")]
		[FormerlySerializedAs("Enabled")]
		public bool CheckOnDisable;

		[Tooltip("(Optional) Usually it's fine to leave this empty.\nIf set the this settings provider will be used. Otherwise the last used provider (or the configured provider, depending on the flag below) will be used instead.")]
		public SettingsProvider Provider;

		[Tooltip("If enabled then the configured global provider will be used if the Provider on this component is NULL, otherwise the last used provider will be used as fallback.")]
		public bool FallBackOnConfiguredProvider;

		public UnityEvent<List<ISetting>> OnUnappliedSettingsDetected;

		[Tooltip("Useful for showing modal confirm dialogs after settings UI has been disabled.")]
		public List<GameObject> ObjectsToShowOnUnapplied;

		[NonSerialized]
		public List<ISetting> _unappliedSettings;

		public static void TriggerCheck()
		{
		}

		protected SettingsProvider getProvider()
		{
			return null;
		}

		public void OnEnable()
		{
		}

		public void OnDisable()
		{
		}

		public void Check()
		{
		}

		public void LogSettings(List<ISetting> settings)
		{
		}
	}
}
