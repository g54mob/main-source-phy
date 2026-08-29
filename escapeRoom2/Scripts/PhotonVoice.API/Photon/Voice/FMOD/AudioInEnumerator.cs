using System.Collections.Generic;
using FMOD;
using FMODUnity;

namespace Photon.Voice.FMOD
{
	public class AudioInEnumerator : DeviceEnumeratorBase
	{
		private const int NAME_MAX_LENGTH = 1000;

		private const string LOG_PREFIX = "[PV] [FMOD] AudioInEnumerator: ";

		public AudioInEnumerator(ILogger logger)
			: base(logger)
		{
			Refresh();
		}

		public override void Refresh()
		{
			RESULT recordNumDrivers = RuntimeManager.CoreSystem.getRecordNumDrivers(out var numdrivers, out var _);
			if (recordNumDrivers != RESULT.OK)
			{
				Error = "failed to getRecordNumDrivers: " + recordNumDrivers;
				logger.LogError("[PV] [FMOD] AudioInEnumerator: " + Error);
				return;
			}
			devices = new List<DeviceInfo>();
			for (int i = 0; i < numdrivers; i++)
			{
				recordNumDrivers = RuntimeManager.CoreSystem.getRecordDriverInfo(i, out var name, 1000, out var _, out var _, out var _, out var _, out var _);
				if (recordNumDrivers != RESULT.OK)
				{
					Error = "failed to getRecordDriverInfo: " + recordNumDrivers;
					logger.LogError("[PV] [FMOD] AudioInEnumerator: " + Error);
					break;
				}
				devices.Add(new DeviceInfo(i, name));
			}
		}

		public override void Dispose()
		{
		}
	}
}
