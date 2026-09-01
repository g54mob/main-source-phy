using System;
using Kamgam.UGUIComponentsForSettings;
using UnityEngine;
using UnityEngine.Serialization;

namespace Kamgam.SettingsGenerator
{
	[AddComponentMenu("UI/Settings/InputBindingUGUIResolver")]
	[RequireComponent(typeof(InputBindingUGUI))]
	public class InputBindingUGUIResolver : SettingResolver, ISettingResolver
	{
		public delegate bool ResolveBindingConflictDelegate(string previousBindingPath, string newBindingPath, InputBindingConnection currentConnection, InputBindingConnection conflictingConnection);

		protected InputBindingUGUI inputBindingUGUI;

		public static ResolveBindingConflictDelegate ResolveBindingConflictFunc;

		[FormerlySerializedAs("BlockBindingConflicts")]
		[Tooltip("If true then duplicate rebinding will be aborted and reverted to the previous value, unless OnBindingConflict is defined.")]
		public bool BlockOnBindingConflict;

		[NonSerialized]
		protected SettingData.DataType[] supportedDataTypes;

		[Header("Debug")]
		public bool LogLocalizedBindingPath;

		protected bool stopPropagation;

		public InputBindingUGUI InputBindingUGUI => null;

		public override SettingData.DataType[] GetSupportedDataTypes()
		{
			return null;
		}

		public override void Start()
		{
		}

		protected bool checkBindingForDuplicates(string previousPath, string path)
		{
			return false;
		}

		public override void OnDestroy()
		{
		}

		protected void onLanguageChanged(string language)
		{
		}

		protected string localizeKeyCode(string bindingPath)
		{
			return null;
		}

		protected void onChanged(string bindingPath)
		{
		}

		public override void Refresh()
		{
		}

		protected string bindingPathToDisplayName(string bindingPath)
		{
			return null;
		}
	}
}
