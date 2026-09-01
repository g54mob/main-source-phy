using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Sony.NP
{
	internal class MemoryBuffer
	{
		public enum BufferIntegrityChecks
		{
			BufferBegin = 0,
			BufferEnd = 1,
			OnlineUserBegin = 2,
			OnlineUserEnd = 3,
			NpOnlineIdBegin = 4,
			NpOnlineIdEnd = 5,
			SceNpIdBegin = 6,
			SceNpIdEnd = 7,
			NpCountryCodeBegin = 8,
			NpCountryCodeEnd = 9,
			NpTitleIdBegin = 10,
			NpTitleIdEnd = 11,
			NpLanguageCodeBegin = 12,
			NpLanguageCodeEnd = 13,
			PNGBegin = 14,
			PNGEnd = 15,
			FriendsBegin = 16,
			FriendsEnd = 17,
			FriendBegin = 18,
			FriendEnd = 19,
			FriendsOfFriendsBegin = 20,
			FriendsOfFriendsEnd = 21,
			BlockedUsersBegin = 22,
			BlockedUsersEnd = 23,
			ProfileBegin = 24,
			ProfileEnd = 25,
			RealNameBegin = 26,
			RealNameEnd = 27,
			PresenceBegin = 28,
			PresenceEnd = 29,
			PlatformPresenceBegin = 30,
			PlatformPresenceEnd = 31,
			NpProfilesBegin = 32,
			NpProfilesEnd = 33,
			BandwidthInfoBegin = 34,
			BandwidthInfoEnd = 35,
			NetStateBasicBegin = 36,
			NetStateBasicEnd = 37,
			NetStateDetailedBegin = 38,
			NetStateDetailedEnd = 39,
			UnlockedTrophiesBegin = 40,
			UnlockedTrophiesEnd = 41,
			TrophyPackSummaryBegin = 42,
			TrophyPackSummaryEnd = 43,
			TrophyPackGroupBegin = 44,
			TrophyPackGroupEnd = 45,
			TrophyPackTrophyBegin = 46,
			TrophyPackTrophyEnd = 47,
			TempRankBegin = 48,
			TempRankEnd = 49,
			RangeOfRanksBegin = 50,
			RangeOfRanksEnd = 51,
			FriendsRanksBegin = 52,
			FriendsRanksEnd = 53,
			UsersRanksBegin = 54,
			UsersRanksEnd = 55,
			SetGameDataBegin = 56,
			SetGameDataEnd = 57,
			GetGameDataBegin = 58,
			GetGameDataEnd = 59,
			WorldsBegin = 60,
			WorldsEnd = 61,
			CreateRoomBegin = 62,
			CreateRoomEnd = 63,
			RoomBegin = 64,
			RoomEnd = 65,
			RoomsBegin = 66,
			RoomsEnd = 67,
			RoomPingTimeBegin = 68,
			RoomPingTimeEnd = 69,
			GetDataBegin = 70,
			GetDataEnd = 71,
			TssDataBegin = 72,
			TssDataEnd = 73,
			TusVariablesBegin = 74,
			TusVariablesEnd = 75,
			TusAtomicAddToAndGetVariableBegin = 76,
			TusAtomicAddToAndGetVariableEnd = 77,
			TusDataBegin = 78,
			TusDataEnd = 79,
			TusFriendsVariablesBegin = 80,
			TusFriendsVariablesEnd = 81,
			TusDataStatusesBegin = 82,
			TusDataStatusesEnd = 83,
			TusFriendsDataStatusesBegin = 84,
			TusFriendsDataStatusesEnd = 85,
			GameDataMessagesBegin = 86,
			GameDataMessagesEnd = 87,
			GameDataMessageThumbnailBegin = 88,
			GameDataMessageThumbnailEnd = 89,
			GameDataMessageAttachmentBegin = 90,
			GameDataMessageAttachmentEnd = 91,
			GameDataMessageBegin = 92,
			GameDataMessageEnd = 93,
			GameDataMessageDetailsBegin = 94,
			GameDataMessageDetailsEnd = 95,
			CategoriesBegin = 96,
			CategoriesEnd = 97,
			CategoryBegin = 98,
			CategoryEnd = 99,
			SubCategoryBegin = 100,
			SubCategoryEnd = 101,
			ProductsBegin = 102,
			ProductsEnd = 103,
			ProductBegin = 104,
			ProductEnd = 105,
			ProductDetailsBegin = 106,
			ProductDetailsEnd = 107,
			SkuInfoBegin = 108,
			SkuInfoEnd = 109,
			ServiceEntitlementsBegin = 110,
			ServiceEntitlementsEnd = 111,
			ServiceEntitlementBegin = 112,
			ServiceEntitlementEnd = 113,
			AuthCodeBegin = 114,
			AuthCodeEnd = 115,
			IdTokenBegin = 116,
			IdTokenEnd = 117,
			WordFilterBegin = 118,
			WordFilterEnd = 119,
			FriendListUpdateBegin = 120,
			FriendListUpdateEnd = 121,
			BlocklistUpdateBegin = 122,
			BlocklistUpdateEnd = 123,
			PresenceUpdateBegin = 124,
			PresenceUpdateEnd = 125,
			UserStateChangeBegin = 126,
			UserStateChangeEnd = 127,
			NetStateChangeBegin = 128,
			NetStateChangeEnd = 129,
			RefreshRoomBegin = 130,
			RefreshRoomEnd = 131,
			InvitationReceivedBegin = 132,
			InvitationReceivedEnd = 133,
			NewRoomMessageBegin = 134,
			NewRoomMessageEnd = 135,
			NewInGameMessageBegin = 136,
			NewInGameMessageEnd = 137,
			NewGameDataMessageBegin = 138,
			NewGameDataMessageEnd = 139,
			SessionInvitationEventBegin = 140,
			SessionInvitationEventEnd = 141,
			PlayTogetherHostEventBegin = 142,
			PlayTogetherHostEventEnd = 143,
			GameCustomDataEventBegin = 144,
			GameCustomDataEventEnd = 145,
			LaunchAppEventBegin = 146,
			LaunchAppEventEnd = 147,
			CheckPlusBegin = 148,
			CheckPlusEnd = 149,
			GetParentalControlInfoBegin = 150,
			GetParentalControlInfoEnd = 151,
			GetFeedBegin = 152,
			GetFeedEnd = 153,
			StoryBegin = 154,
			StoryEnd = 155,
			StoryUserBegin = 156,
			StoryUserEnd = 157,
			UsersWhoLikedBegin = 158,
			UsersWhoLikedEnd = 159,
			PlayedWithFeedBegin = 160,
			PlayedWithFeedEnd = 161,
			SharedVideosBegin = 162,
			SharedVideosEnd = 163,
			SharedVideoBegin = 164,
			SharedVideoEnd = 165
		}

		private NpMemoryBuffer rawBuffer;

		private IntPtr pos;

		public MemoryBuffer(NpMemoryBuffer pointer)
		{
			rawBuffer.data = pointer.data;
			rawBuffer.size = pointer.size;
			pos = rawBuffer.data;
		}

		public void CheckStartMarker()
		{
			CheckMarker(BufferIntegrityChecks.BufferBegin);
		}

		public void CheckEndMarker()
		{
			CheckMarker(BufferIntegrityChecks.BufferEnd);
		}

		public void CheckMarker(BufferIntegrityChecks value)
		{
			byte b = Marshal.ReadByte(pos);
			pos = new IntPtr(pos.ToInt64() + 1);
			byte b2 = Marshal.ReadByte(pos);
			pos = new IntPtr(pos.ToInt64() + 1);
			byte b3 = Marshal.ReadByte(pos);
			pos = new IntPtr(pos.ToInt64() + 1);
			byte b4 = Marshal.ReadByte(pos);
			pos = new IntPtr(pos.ToInt64() + 1);
			if (b == byte.MaxValue && b2 == 254 && b3 == 253 && (BufferIntegrityChecks)b4 == value)
			{
				return;
			}
			throw new NpToolkitException("MemoryBuffer - CheckMarker error - Expecting " + value);
		}

		public void CheckBufferOverflow(string method)
		{
			long num = pos.ToInt64() - rawBuffer.data.ToInt64();
			if ((uint)num > rawBuffer.size)
			{
				throw new NpToolkitException("MemoryBuffer - Overflow error detected. (" + method + ") (" + num + "," + rawBuffer.size + ")");
			}
		}

		public bool ReadBool()
		{
			CheckBufferOverflow("ReadBool");
			byte b = Marshal.ReadByte(pos);
			pos = new IntPtr(pos.ToInt64() + 1);
			if (b == 0)
			{
				return false;
			}
			return true;
		}

		public sbyte ReadInt8()
		{
			CheckBufferOverflow("ReadInt8");
			sbyte result = (sbyte)Marshal.ReadByte(pos);
			pos = new IntPtr(pos.ToInt64() + 1);
			return result;
		}

		public byte ReadUInt8()
		{
			CheckBufferOverflow("ReadUInt8");
			byte result = Marshal.ReadByte(pos);
			pos = new IntPtr(pos.ToInt64() + 1);
			return result;
		}

		public short ReadInt16()
		{
			CheckBufferOverflow("ReadInt16");
			short result = Marshal.ReadInt16(pos);
			pos = new IntPtr(pos.ToInt64() + 2);
			return result;
		}

		public ushort ReadUInt16()
		{
			CheckBufferOverflow("ReadUInt16");
			ushort result = (ushort)Marshal.ReadInt16(pos);
			pos = new IntPtr(pos.ToInt64() + 2);
			return result;
		}

		public int ReadInt32()
		{
			CheckBufferOverflow("ReadInt32");
			int result = Marshal.ReadInt32(pos);
			pos = new IntPtr(pos.ToInt64() + 4);
			return result;
		}

		public uint ReadUInt32()
		{
			CheckBufferOverflow("ReadUInt32");
			uint result = (uint)Marshal.ReadInt32(pos);
			pos = new IntPtr(pos.ToInt64() + 4);
			return result;
		}

		public long ReadInt64()
		{
			CheckBufferOverflow("ReadInt64");
			long result = Marshal.ReadInt64(pos);
			pos = new IntPtr(pos.ToInt64() + 8);
			return result;
		}

		public ulong ReadUInt64()
		{
			CheckBufferOverflow("ReadUInt64");
			ulong result = (ulong)Marshal.ReadInt64(pos);
			pos = new IntPtr(pos.ToInt64() + 8);
			return result;
		}

		public IntPtr ReadPtr()
		{
			CheckBufferOverflow("ReadPtr");
			long value = Marshal.ReadInt64(pos);
			pos = new IntPtr(pos.ToInt64() + 8);
			return new IntPtr(value);
		}

		public double ReadDouble()
		{
			CheckBufferOverflow("ReadDouble");
			double[] array = new double[1];
			Marshal.Copy(pos, array, 0, 1);
			pos = new IntPtr(pos.ToInt64() + 8);
			return array[0];
		}

		public uint ReadData(ref byte[] data)
		{
			CheckBufferOverflow("ReadData");
			uint num = ReadUInt32();
			if (num == 0)
			{
				return 0u;
			}
			if (data == null || data.Length != num)
			{
				data = new byte[num];
			}
			Marshal.Copy(pos, data, 0, (int)num);
			pos = new IntPtr(pos.ToInt64() + num);
			return num;
		}

		public uint ReadData(ref byte[] data, uint startIndex)
		{
			CheckBufferOverflow("ReadData");
			uint num = ReadUInt32();
			if (num == 0)
			{
				return 0u;
			}
			if (data == null || startIndex + num > data.Length)
			{
				byte[] array = new byte[num];
				if (data != null)
				{
					Array.Copy(data, array, startIndex);
				}
				data = array;
			}
			Marshal.Copy(pos, data, (int)startIndex, (int)num);
			pos = new IntPtr(pos.ToInt64() + num);
			return num;
		}

		public void ReadString(ref string str)
		{
			CheckBufferOverflow("ReadString");
			byte[] data = null;
			if (ReadData(ref data) == 0)
			{
				str = "";
			}
			else
			{
				str = Encoding.UTF8.GetString(data, 0, data.Length);
			}
		}

		public override string ToString()
		{
			long num = pos.ToInt64() - rawBuffer.data.ToInt64();
			long num2 = rawBuffer.data.ToInt64();
			return "Memorry buffer : Data = (" + num2.ToString("X") + ") Size = (" + rawBuffer.size + ") Read = (" + num + ")";
		}
	}
}
