using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace Novell.Directory.Ldap.Asn1
{
	public class LBERDecoder : Asn1Decoder, ISerializable
	{
		private Asn1Identifier asn1ID;

		private Asn1Length asn1Len;

		public LBERDecoder()
		{
			InitBlock();
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
		}

		private void InitBlock()
		{
			asn1ID = new Asn1Identifier();
			asn1Len = new Asn1Length();
		}

		[CLSCompliant(false)]
		public virtual Asn1Object decode(sbyte[] value_Renamed)
		{
			Asn1Object result = null;
			MemoryStream in_Renamed = new MemoryStream(SupportClass.ToByteArray(value_Renamed));
			try
			{
				result = decode(in_Renamed);
			}
			catch (IOException)
			{
			}
			return result;
		}

		public virtual Asn1Object decode(Stream in_Renamed)
		{
			int[] len = new int[1];
			return decode(in_Renamed, len);
		}

		public virtual Asn1Object decode(Stream in_Renamed, int[] len)
		{
			asn1ID.reset(in_Renamed);
			asn1Len.reset(in_Renamed);
			int length = asn1Len.Length;
			len[0] = asn1ID.EncodedLength + asn1Len.EncodedLength + length;
			if (asn1ID.Universal)
			{
				return asn1ID.Tag switch
				{
					16 => new Asn1Sequence(this, in_Renamed, length), 
					17 => new Asn1Set(this, in_Renamed, length), 
					1 => new Asn1Boolean(this, in_Renamed, length), 
					2 => new Asn1Integer(this, in_Renamed, length), 
					4 => new Asn1OctetString(this, in_Renamed, length), 
					10 => new Asn1Enumerated(this, in_Renamed, length), 
					5 => new Asn1Null(), 
					_ => throw new EndOfStreamException("Unknown tag"), 
				};
			}
			return new Asn1Tagged(this, in_Renamed, length, (Asn1Identifier)asn1ID.Clone());
		}

		public object decodeBoolean(Stream in_Renamed, int len)
		{
			sbyte[] target = new sbyte[len];
			if (SupportClass.ReadInput(in_Renamed, ref target, 0, target.Length) != len)
			{
				throw new EndOfStreamException("LBER: BOOLEAN: decode error: EOF");
			}
			return (target[0] != 0) ? true : false;
		}

		public object decodeNumeric(Stream in_Renamed, int len)
		{
			long num = 0L;
			int num2 = in_Renamed.ReadByte();
			if (num2 < 0)
			{
				throw new EndOfStreamException("LBER: NUMERIC: decode error: EOF");
			}
			if ((num2 & 0x80) != 0)
			{
				num = -1L;
			}
			num = (num << 8) | num2;
			for (int i = 1; i < len; i++)
			{
				num2 = in_Renamed.ReadByte();
				if (num2 < 0)
				{
					throw new EndOfStreamException("LBER: NUMERIC: decode error: EOF");
				}
				num = (num << 8) | num2;
			}
			return num;
		}

		public object decodeOctetString(Stream in_Renamed, int len)
		{
			sbyte[] target = new sbyte[len];
			int num;
			for (int i = 0; i < len; i += num)
			{
				num = SupportClass.ReadInput(in_Renamed, ref target, i, len - i);
			}
			return target;
		}

		public object decodeCharacterString(Stream in_Renamed, int len)
		{
			sbyte[] array = new sbyte[len];
			for (int i = 0; i < len; i++)
			{
				int num = in_Renamed.ReadByte();
				if (num == -1)
				{
					throw new EndOfStreamException("LBER: CHARACTER STRING: decode error: EOF");
				}
				array[i] = (sbyte)num;
			}
			return new string(Encoding.GetEncoding("utf-8").GetChars(SupportClass.ToByteArray(array)));
		}
	}
}
