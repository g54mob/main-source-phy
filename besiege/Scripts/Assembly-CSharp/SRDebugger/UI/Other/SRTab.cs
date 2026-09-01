using System;
using SRDebugger.UI.Controls;
using SRF;
using UnityEngine;
using UnityEngine.Serialization;

namespace SRDebugger.UI.Other
{
	public class SRTab : SRMonoBehaviourEx
	{
		public RectTransform HeaderExtraContent;

		[HideInInspector]
		[Obsolete]
		public Sprite Icon;

		public RectTransform IconExtraContent;

		public string IconStyleKey = "Icon_Stompy";

		public int SortIndex;

		[HideInInspector]
		public SRTabButton TabButton;

		[FormerlySerializedAs("Title")]
		[SerializeField]
		private string _title;

		[SerializeField]
		private string _longTitle;

		[SerializeField]
		private string _key;

		public string Title
		{
			get
			{
				return _title;
			}
		}

		public string LongTitle
		{
			get
			{
				return string.IsNullOrEmpty(_longTitle) ? _title : _longTitle;
			}
		}

		public string Key
		{
			get
			{
				return _key;
			}
		}
	}
}
