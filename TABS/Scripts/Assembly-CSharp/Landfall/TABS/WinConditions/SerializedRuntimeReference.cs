using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Landfall.TABS.WinConditions
{
	[Serializable]
	public struct SerializedRuntimeReference
	{
		[SerializeField]
		[JsonProperty]
		private string m_guid;

		[SerializeField]
		[JsonProperty]
		private string m_referenceType;

		[SerializeField]
		[JsonProperty]
		private bool m_isRequest;

		[JsonIgnore]
		public string Guid => m_guid;

		[JsonIgnore]
		public string ReferenceType => m_referenceType;

		[JsonIgnore]
		public bool IsRequest => m_isRequest;

		public SerializedRuntimeReference(RuntimeReference reference)
		{
			m_guid = reference.Guid.ToString();
			m_referenceType = reference.ReferenceType.FullName;
			m_isRequest = reference.IsRequest;
		}
	}
}
