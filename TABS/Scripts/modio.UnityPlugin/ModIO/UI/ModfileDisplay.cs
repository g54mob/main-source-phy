using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	[Obsolete("Use ModfileFieldDisplay components instead.")]
	public class ModfileDisplay : ModfileDisplayComponent
	{
		private delegate string GetDisplayString(ModfileDisplayData data);

		[Header("UI Components")]
		public Text modfileIdDisplay;

		public Text modIdDisplay;

		public Text dateAddedDisplay;

		public Text fileNameDisplay;

		public Text fileSizeDisplay;

		public Text MD5Display;

		public Text versionDisplay;

		public Text changelogDisplay;

		public Text metadataBlobDisplay;

		public Text virusScanDateDisplay;

		public Text virusScanStatusDisplay;

		public Text virusScanResultDisplay;

		public Text virusScanHashDisplay;

		[Header("Display Data")]
		[SerializeField]
		private ModfileDisplayData m_data;

		private List<TextLoadingOverlay> m_loadingOverlays;

		private Dictionary<Text, GetDisplayString> m_displayMapping;

		public override ModfileDisplayData data
		{
			get
			{
				return m_data;
			}
			set
			{
				m_data = value;
				PresentData(value);
			}
		}

		public override event Action<ModfileDisplayComponent> onClick;

		private void PresentData(ModfileDisplayData displayData)
		{
			if (m_displayMapping == null)
			{
				Initialize();
			}
			foreach (KeyValuePair<Text, GetDisplayString> item in m_displayMapping)
			{
				item.Key.text = item.Value(displayData);
			}
			foreach (TextLoadingOverlay loadingOverlay in m_loadingOverlays)
			{
				loadingOverlay.gameObject.SetActive(value: false);
			}
		}

		public override void Initialize()
		{
			if (m_displayMapping == null)
			{
				BuildDisplayMap();
				CollectLoadingOverlays();
			}
		}

		private void BuildDisplayMap()
		{
			string dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
			m_displayMapping = new Dictionary<Text, GetDisplayString>();
			if (modfileIdDisplay != null)
			{
				m_displayMapping.Add(modfileIdDisplay, (ModfileDisplayData d) => d.modfileId.ToString());
			}
			if (modIdDisplay != null)
			{
				m_displayMapping.Add(modIdDisplay, (ModfileDisplayData d) => d.modId.ToString());
			}
			if (dateAddedDisplay != null)
			{
				m_displayMapping.Add(dateAddedDisplay, (ModfileDisplayData d) => ServerTimeStamp.ToLocalDateTime(d.dateAdded).ToString(dateTimeFormat));
			}
			if (fileNameDisplay != null)
			{
				m_displayMapping.Add(fileNameDisplay, (ModfileDisplayData d) => d.fileName);
			}
			if (fileSizeDisplay != null)
			{
				m_displayMapping.Add(fileSizeDisplay, (ModfileDisplayData d) => ValueFormatting.ByteCount(d.fileSize, "0.0"));
			}
			if (MD5Display != null)
			{
				m_displayMapping.Add(MD5Display, (ModfileDisplayData d) => d.MD5);
			}
			if (versionDisplay != null)
			{
				m_displayMapping.Add(versionDisplay, (ModfileDisplayData d) => d.version);
			}
			if (changelogDisplay != null)
			{
				m_displayMapping.Add(changelogDisplay, (ModfileDisplayData d) => d.changelog);
			}
			if (metadataBlobDisplay != null)
			{
				m_displayMapping.Add(metadataBlobDisplay, (ModfileDisplayData d) => d.metadataBlob);
			}
			if (virusScanDateDisplay != null)
			{
				m_displayMapping.Add(virusScanDateDisplay, (ModfileDisplayData d) => ServerTimeStamp.ToLocalDateTime(d.virusScanDate).ToString(dateTimeFormat));
			}
			if (virusScanStatusDisplay != null)
			{
				m_displayMapping.Add(virusScanStatusDisplay, (ModfileDisplayData d) => d.virusScanStatus.ToString());
			}
			if (virusScanResultDisplay != null)
			{
				m_displayMapping.Add(virusScanResultDisplay, (ModfileDisplayData d) => d.virusScanResult.ToString());
			}
			if (virusScanHashDisplay != null)
			{
				m_displayMapping.Add(virusScanHashDisplay, (ModfileDisplayData d) => d.virusScanHash);
			}
		}

		private void CollectLoadingOverlays()
		{
			TextLoadingOverlay[] componentsInChildren = base.gameObject.GetComponentsInChildren<TextLoadingOverlay>(includeInactive: true);
			List<Text> list = new List<Text>(m_displayMapping.Keys);
			m_loadingOverlays = new List<TextLoadingOverlay>();
			TextLoadingOverlay[] array = componentsInChildren;
			foreach (TextLoadingOverlay textLoadingOverlay in array)
			{
				if (list.Contains(textLoadingOverlay.textDisplayComponent))
				{
					m_loadingOverlays.Add(textLoadingOverlay);
				}
			}
		}

		public override void DisplayModfile(Modfile modfile)
		{
			PresentData(m_data = new ModfileDisplayData
			{
				modfileId = modfile.id,
				modId = modfile.modId,
				dateAdded = modfile.dateAdded,
				fileName = modfile.fileName,
				fileSize = modfile.fileSize,
				MD5 = ((modfile.fileHash == null) ? string.Empty : modfile.fileHash.md5),
				version = modfile.version,
				changelog = modfile.changelog,
				metadataBlob = modfile.metadataBlob,
				virusScanDate = modfile.dateScanned,
				virusScanStatus = modfile.virusScanStatus,
				virusScanResult = modfile.virusScanResult,
				virusScanHash = modfile.virusScanHash
			});
		}

		public override void DisplayLoading()
		{
			foreach (TextLoadingOverlay loadingOverlay in m_loadingOverlays)
			{
				loadingOverlay.gameObject.SetActive(value: true);
			}
			foreach (Text key in m_displayMapping.Keys)
			{
				key.text = string.Empty;
			}
		}

		public void NotifyClicked()
		{
			if (onClick != null)
			{
				onClick(this);
			}
		}
	}
}
