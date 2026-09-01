using System;
using Poly.Solver;
using UnityEngine;
using UnityEngine.UI;

namespace Poly.UI
{
	public class SolverSettingsFallbackUI : MonoBehaviour
	{
		[Serializable]
		public struct SettingsItem
		{
			public SolverSettings settings;

			public string shortMessage;

			[Multiline]
			public string message;

			public float messageDuration;
		}

		public SettingsItem[] settingsToCycle;

		public Text messageLabel;

		public SimulationOptionsUI simulationOptionsToLinkUp;

		private int currentItemIdx;

		private float timeLeftTillHideText;
	}
}
