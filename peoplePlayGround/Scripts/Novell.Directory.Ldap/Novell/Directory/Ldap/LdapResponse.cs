using System;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	public class LdapResponse : LdapMessage
	{
		private InterThreadException exception;

		private ReferralInfo activeReferral;

		public virtual string ErrorMessage
		{
			get
			{
				if (exception != null)
				{
					return exception.LdapErrorMessage;
				}
				return ((RfcResponse)message.Response).getErrorMessage().stringValue();
			}
		}

		public virtual string MatchedDN
		{
			get
			{
				if (exception != null)
				{
					return exception.MatchedDN;
				}
				return ((RfcResponse)message.Response).getMatchedDN().stringValue();
			}
		}

		public virtual string[] Referrals
		{
			get
			{
				string[] array = null;
				RfcReferral referral = ((RfcResponse)message.Response).getReferral();
				if (referral == null)
				{
					array = new string[0];
				}
				else
				{
					int num = referral.size();
					array = new string[num];
					for (int i = 0; i < num; i++)
					{
						string text = ((Asn1OctetString)referral.get_Renamed(i)).stringValue();
						try
						{
							LdapUrl ldapUrl = new LdapUrl(text);
							string requestDN;
							if (ldapUrl.getDN() == null && (requestDN = base.Asn1Object.RequestingMessage.Asn1Object.RequestDN) != null)
							{
								ldapUrl.setDN(requestDN);
								text = ldapUrl.ToString();
							}
						}
						catch (UriFormatException)
						{
						}
						finally
						{
							array[i] = text;
						}
					}
				}
				return array;
			}
		}

		public virtual int ResultCode
		{
			get
			{
				if (exception != null)
				{
					return exception.ResultCode;
				}
				if (((RfcResponse)message.Response) is RfcIntermediateResponse)
				{
					return 0;
				}
				return ((RfcResponse)message.Response).getResultCode().intValue();
			}
		}

		internal virtual LdapException ResultException
		{
			get
			{
				LdapException ex = null;
				switch (ResultCode)
				{
				case 10:
				{
					string[] referrals = Referrals;
					ex = new LdapReferralException("Automatic referral following not enabled", 10, ErrorMessage);
					((LdapReferralException)ex).setReferrals(referrals);
					break;
				}
				default:
					ex = new LdapException(LdapException.resultCodeToString(ResultCode), ResultCode, ErrorMessage, MatchedDN);
					break;
				case 0:
				case 5:
				case 6:
					break;
				}
				return ex;
			}
		}

		public override LdapControl[] Controls
		{
			get
			{
				if (exception != null)
				{
					return null;
				}
				return base.Controls;
			}
		}

		public override int MessageID
		{
			get
			{
				if (exception != null)
				{
					return exception.MessageID;
				}
				return base.MessageID;
			}
		}

		public override int Type
		{
			get
			{
				if (exception != null)
				{
					return exception.ReplyType;
				}
				return base.Type;
			}
		}

		internal virtual LdapException Exception => exception;

		internal virtual ReferralInfo ActiveReferral => activeReferral;

		public LdapResponse(InterThreadException ex, ReferralInfo activeReferral)
		{
			exception = ex;
			this.activeReferral = activeReferral;
		}

		internal LdapResponse(RfcLdapMessage message)
			: base(message)
		{
		}

		public LdapResponse(int type)
			: this(type, 0, null, null, null, null)
		{
		}

		public LdapResponse(int type, int resultCode, string matchedDN, string serverMessage, string[] referrals, LdapControl[] controls)
			: base(new RfcLdapMessage(RfcResultFactory(type, resultCode, matchedDN, serverMessage, referrals)))
		{
		}

		private static Asn1Sequence RfcResultFactory(int type, int resultCode, string matchedDN, string serverMessage, string[] referrals)
		{
			if (matchedDN == null)
			{
				matchedDN = "";
			}
			if (serverMessage == null)
			{
				serverMessage = "";
			}
			return type switch
			{
				5 => new RfcSearchResultDone(new Asn1Enumerated(resultCode), new RfcLdapDN(matchedDN), new RfcLdapString(serverMessage), null), 
				1 => null, 
				4 => null, 
				7 => new RfcModifyResponse(new Asn1Enumerated(resultCode), new RfcLdapDN(matchedDN), new RfcLdapString(serverMessage), null), 
				9 => new RfcAddResponse(new Asn1Enumerated(resultCode), new RfcLdapDN(matchedDN), new RfcLdapString(serverMessage), null), 
				11 => new RfcDelResponse(new Asn1Enumerated(resultCode), new RfcLdapDN(matchedDN), new RfcLdapString(serverMessage), null), 
				13 => new RfcModifyDNResponse(new Asn1Enumerated(resultCode), new RfcLdapDN(matchedDN), new RfcLdapString(serverMessage), null), 
				15 => new RfcCompareResponse(new Asn1Enumerated(resultCode), new RfcLdapDN(matchedDN), new RfcLdapString(serverMessage), null), 
				19 => null, 
				24 => null, 
				_ => throw new SystemException("Type " + type + " Not Supported"), 
			};
		}

		internal virtual void chkResultCode()
		{
			if (exception != null)
			{
				throw exception;
			}
			LdapException resultException = ResultException;
			if (resultException != null)
			{
				throw resultException;
			}
		}

		internal virtual bool hasException()
		{
			return exception != null;
		}
	}
}
