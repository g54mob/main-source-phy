using System.Runtime.Serialization;

namespace CloudinaryDotNet
{
	public enum AudioFrequency
	{
		[EnumMember(Value = "8000")]
		AF8000 = 0,
		[EnumMember(Value = "11025")]
		AF11025 = 1,
		[EnumMember(Value = "16000")]
		AF16000 = 2,
		[EnumMember(Value = "22050")]
		AF22050 = 3,
		[EnumMember(Value = "32000")]
		AF32000 = 4,
		[EnumMember(Value = "37800")]
		AF37800 = 5,
		[EnumMember(Value = "44056")]
		AF44056 = 6,
		[EnumMember(Value = "44100")]
		AF44100 = 7,
		[EnumMember(Value = "47250")]
		AF47250 = 8,
		[EnumMember(Value = "48000")]
		AF48000 = 9,
		[EnumMember(Value = "88200")]
		AF88200 = 10,
		[EnumMember(Value = "96000")]
		AF96000 = 11,
		[EnumMember(Value = "176400")]
		AF176400 = 12,
		[EnumMember(Value = "192000")]
		AF192000 = 13
	}
}
