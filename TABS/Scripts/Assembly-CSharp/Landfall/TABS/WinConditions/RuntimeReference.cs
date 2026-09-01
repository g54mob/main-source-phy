using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Landfall.TABS.WinConditions
{
	[JsonConverter(typeof(ReferenceConverter))]
	public class RuntimeReference
	{
		private Guid m_guid = Guid.Empty;

		private Type m_referenceType;

		private bool m_isRequest;

		public Guid Guid
		{
			get
			{
				return m_guid;
			}
			set
			{
				m_guid = value;
			}
		}

		public Type ReferenceType
		{
			get
			{
				return m_referenceType;
			}
			set
			{
				m_referenceType = value;
			}
		}

		public bool IsRequest
		{
			get
			{
				return m_isRequest;
			}
			set
			{
				m_isRequest = value;
			}
		}

		public RuntimeReference(string guid, bool isRequest)
		{
			m_guid = Guid.Parse(guid);
			m_isRequest = isRequest;
		}

		public static ReferenceType<T> ToReferenceType<T>(SerializedRuntimeReference serializedRef) where T : class
		{
			if (serializedRef.Guid == string.Empty)
			{
				Debug.LogError("No guid present!");
				return null;
			}
			return new ReferenceType<T>(serializedRef.Guid);
		}

		public override bool Equals(object obj)
		{
			return ((RuntimeReference)obj).Guid == Guid;
		}
	}
}
