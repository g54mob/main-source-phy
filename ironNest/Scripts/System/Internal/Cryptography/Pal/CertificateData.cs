using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Internal.Cryptography.Pal
{
	internal struct CertificateData
	{
		internal struct AlgorithmIdentifier
		{
			internal string AlgorithmId;

			internal byte[] Parameters;
		}

		[CompilerGenerated]
		private sealed class _003CReadReverseRdns_003Ed__21 : IEnumerable<KeyValuePair<string, string>>, IEnumerable, IEnumerator<KeyValuePair<string, string>>, IDisposable, IEnumerator
		{
			private int _003C_003E1__state;

			private KeyValuePair<string, string> _003C_003E2__current;

			private int _003C_003El__initialThreadId;

			private X500DistinguishedName name;

			public X500DistinguishedName _003C_003E3__name;

			private Stack<DerSequenceReader> _003CrdnReaders_003E5__2;

			private DerSequenceReader _003CrdnReader_003E5__3;

			KeyValuePair<string, string> IEnumerator<KeyValuePair<string, string>>.Current
			{
				[DebuggerHidden]
				get
				{
					return default(KeyValuePair<string, string>);
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CReadReverseRdns_003Ed__21(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}

			[DebuggerHidden]
			IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator()
			{
				return null;
			}

			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return null;
			}
		}

		internal byte[] RawData;

		internal byte[] SubjectPublicKeyInfo;

		internal int Version;

		internal byte[] SerialNumber;

		internal AlgorithmIdentifier TbsSignature;

		internal X500DistinguishedName Issuer;

		internal DateTime NotBefore;

		internal DateTime NotAfter;

		internal X500DistinguishedName Subject;

		internal AlgorithmIdentifier PublicKeyAlgorithm;

		internal byte[] PublicKey;

		internal byte[] IssuerUniqueId;

		internal byte[] SubjectUniqueId;

		internal List<X509Extension> Extensions;

		internal AlgorithmIdentifier SignatureAlgorithm;

		internal byte[] SignatureValue;

		internal CertificateData(byte[] rawData)
		{
			RawData = null;
			SubjectPublicKeyInfo = null;
			Version = 0;
			SerialNumber = null;
			TbsSignature = default(AlgorithmIdentifier);
			Issuer = null;
			NotBefore = default(DateTime);
			NotAfter = default(DateTime);
			Subject = null;
			PublicKeyAlgorithm = default(AlgorithmIdentifier);
			PublicKey = null;
			IssuerUniqueId = null;
			SubjectUniqueId = null;
			Extensions = null;
			SignatureAlgorithm = default(AlgorithmIdentifier);
			SignatureValue = null;
		}

		public string GetNameInfo(X509NameType nameType, bool forIssuer)
		{
			return null;
		}

		private static string GetSimpleNameInfo(X500DistinguishedName name)
		{
			return null;
		}

		private static string FindAltNameMatch(byte[] extensionBytes, GeneralNameType matchType, string otherOid)
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CReadReverseRdns_003Ed__21))]
		private static IEnumerable<KeyValuePair<string, string>> ReadReverseRdns(X500DistinguishedName name)
		{
			return null;
		}
	}
}
