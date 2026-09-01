using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Events.Edir.EventData;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap.Events.Edir
{
	public class EdirEventIntermediateResponse : LdapIntermediateResponse
	{
		protected EdirEventType event_type;

		protected EdirEventResultType event_result_type;

		protected BaseEdirEventData event_response_data;

		public EdirEventType EventType => event_type;

		public EdirEventResultType EventResultType => event_result_type;

		public BaseEdirEventData EventResponseDataObject => event_response_data;

		public EdirEventIntermediateResponse(RfcLdapMessage message)
			: base(message)
		{
			ProcessMessage(getValue());
		}

		public EdirEventIntermediateResponse(byte[] message)
			: base(new RfcLdapMessage(new Asn1Sequence()))
		{
			ProcessMessage(SupportClass.ToSByteArray(message));
		}

		[CLSCompliant(false)]
		protected void ProcessMessage(sbyte[] returnedValue)
		{
			Asn1Sequence asn1Sequence = (Asn1Sequence)new LBERDecoder().decode(returnedValue);
			event_type = (EdirEventType)((Asn1Integer)asn1Sequence.get_Renamed(0)).intValue();
			event_result_type = (EdirEventResultType)((Asn1Integer)asn1Sequence.get_Renamed(1)).intValue();
			if (asn1Sequence.size() > 2)
			{
				Asn1Tagged asn1Tagged = (Asn1Tagged)asn1Sequence.get_Renamed(2);
				switch ((EdirEventDataType)asn1Tagged.getIdentifier().Tag)
				{
				case EdirEventDataType.EDIR_TAG_ENTRY_EVENT_DATA:
					event_response_data = new EntryEventData(EdirEventDataType.EDIR_TAG_ENTRY_EVENT_DATA, asn1Tagged.taggedValue());
					break;
				case EdirEventDataType.EDIR_TAG_VALUE_EVENT_DATA:
					event_response_data = new ValueEventData(EdirEventDataType.EDIR_TAG_VALUE_EVENT_DATA, asn1Tagged.taggedValue());
					break;
				case EdirEventDataType.EDIR_TAG_DEBUG_EVENT_DATA:
					event_response_data = new DebugEventData(EdirEventDataType.EDIR_TAG_DEBUG_EVENT_DATA, asn1Tagged.taggedValue());
					break;
				case EdirEventDataType.EDIR_TAG_GENERAL_EVENT_DATA:
					event_response_data = new GeneralDSEventData(EdirEventDataType.EDIR_TAG_GENERAL_EVENT_DATA, asn1Tagged.taggedValue());
					break;
				case EdirEventDataType.EDIR_TAG_SKULK_DATA:
					event_response_data = null;
					break;
				case EdirEventDataType.EDIR_TAG_BINDERY_EVENT_DATA:
					event_response_data = new BinderyObjectEventData(EdirEventDataType.EDIR_TAG_BINDERY_EVENT_DATA, asn1Tagged.taggedValue());
					break;
				case EdirEventDataType.EDIR_TAG_DSESEV_INFO:
					event_response_data = new SecurityEquivalenceEventData(EdirEventDataType.EDIR_TAG_DSESEV_INFO, asn1Tagged.taggedValue());
					break;
				case EdirEventDataType.EDIR_TAG_MODULE_STATE_DATA:
					event_response_data = new ModuleStateEventData(EdirEventDataType.EDIR_TAG_MODULE_STATE_DATA, asn1Tagged.taggedValue());
					break;
				case EdirEventDataType.EDIR_TAG_NETWORK_ADDRESS:
					event_response_data = new NetworkAddressEventData(EdirEventDataType.EDIR_TAG_NETWORK_ADDRESS, asn1Tagged.taggedValue());
					break;
				case EdirEventDataType.EDIR_TAG_CONNECTION_STATE:
					event_response_data = new ConnectionStateEventData(EdirEventDataType.EDIR_TAG_CONNECTION_STATE, asn1Tagged.taggedValue());
					break;
				case EdirEventDataType.EDIR_TAG_CHANGE_SERVER_ADDRESS:
					event_response_data = new ChangeAddressEventData(EdirEventDataType.EDIR_TAG_CHANGE_SERVER_ADDRESS, asn1Tagged.taggedValue());
					break;
				case EdirEventDataType.EDIR_TAG_NO_DATA:
					event_response_data = null;
					break;
				default:
					throw new IOException();
				}
			}
			else
			{
				event_response_data = null;
			}
		}
	}
}
