using System;
using System.Collections.Generic;
using UnityEngine;

namespace TFBGames.UnitCreatorBakeReport
{
	[Serializable]
	public class BakeReportItem
	{
		[SerializeField]
		public string assetName;

		[SerializeField]
		public string assetFilePath;

		[SerializeField]
		public List<BakeReportLog> Errors;

		[SerializeField]
		public List<BakeReportLog> Warnings;

		[SerializeField]
		public bool ManuallyFixed;

		public string AssetName => assetName;
	}
}
