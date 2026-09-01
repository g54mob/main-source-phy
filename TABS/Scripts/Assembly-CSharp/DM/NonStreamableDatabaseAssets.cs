using System.Collections.Generic;
using Landfall.TABS;
using UnityEngine;

namespace DM
{
	public struct NonStreamableDatabaseAssets
	{
		public string BuildDateTimeString;

		public List<Object> Objects;

		public SerializedHelpingData HelpingData;

		public CustomFactionColorDatabase CustomFactionColorDatabase;

		public List<string> MainMenuScenes;

		public UnitEditorColorPalette UnitEditorColorPalette;

		public string Version;

		public UpgradeDataAsset UpgradeDataAsset;

		public UnitBlueprint UnitEditorBlueprint;
	}
}
