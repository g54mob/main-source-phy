using System;
using System.Runtime.InteropServices;

namespace FMOD
{
	public struct System
	{
		public IntPtr handle;

		public RESULT release()
		{
			return default(RESULT);
		}

		public RESULT setOutput(OUTPUTTYPE output)
		{
			return default(RESULT);
		}

		public RESULT getOutput(out OUTPUTTYPE output)
		{
			output = default(OUTPUTTYPE);
			return default(RESULT);
		}

		public RESULT getNumDrivers(out int numdrivers)
		{
			numdrivers = default(int);
			return default(RESULT);
		}

		public RESULT getDriverInfo(int id, out string name, int namelen, out Guid guid, out int systemrate, out SPEAKERMODE speakermode, out int speakermodechannels)
		{
			name = null;
			guid = default(Guid);
			systemrate = default(int);
			speakermode = default(SPEAKERMODE);
			speakermodechannels = default(int);
			return default(RESULT);
		}

		public RESULT getDriverInfo(int id, out Guid guid, out int systemrate, out SPEAKERMODE speakermode, out int speakermodechannels)
		{
			guid = default(Guid);
			systemrate = default(int);
			speakermode = default(SPEAKERMODE);
			speakermodechannels = default(int);
			return default(RESULT);
		}

		public RESULT setDriver(int driver)
		{
			return default(RESULT);
		}

		public RESULT getDriver(out int driver)
		{
			driver = default(int);
			return default(RESULT);
		}

		public RESULT setSoftwareChannels(int numsoftwarechannels)
		{
			return default(RESULT);
		}

		public RESULT getSoftwareChannels(out int numsoftwarechannels)
		{
			numsoftwarechannels = default(int);
			return default(RESULT);
		}

		public RESULT setSoftwareFormat(int samplerate, SPEAKERMODE speakermode, int numrawspeakers)
		{
			return default(RESULT);
		}

		public RESULT getSoftwareFormat(out int samplerate, out SPEAKERMODE speakermode, out int numrawspeakers)
		{
			samplerate = default(int);
			speakermode = default(SPEAKERMODE);
			numrawspeakers = default(int);
			return default(RESULT);
		}

		public RESULT setDSPBufferSize(uint bufferlength, int numbuffers)
		{
			return default(RESULT);
		}

		public RESULT getDSPBufferSize(out uint bufferlength, out int numbuffers)
		{
			bufferlength = default(uint);
			numbuffers = default(int);
			return default(RESULT);
		}

		public RESULT setFileSystem(FILE_OPEN_CALLBACK useropen, FILE_CLOSE_CALLBACK userclose, FILE_READ_CALLBACK userread, FILE_SEEK_CALLBACK userseek, FILE_ASYNCREAD_CALLBACK userasyncread, FILE_ASYNCCANCEL_CALLBACK userasynccancel, int blockalign)
		{
			return default(RESULT);
		}

		public RESULT attachFileSystem(FILE_OPEN_CALLBACK useropen, FILE_CLOSE_CALLBACK userclose, FILE_READ_CALLBACK userread, FILE_SEEK_CALLBACK userseek)
		{
			return default(RESULT);
		}

		public RESULT setAdvancedSettings(ref ADVANCEDSETTINGS settings)
		{
			return default(RESULT);
		}

		public RESULT getAdvancedSettings(ref ADVANCEDSETTINGS settings)
		{
			return default(RESULT);
		}

		public RESULT setCallback(SYSTEM_CALLBACK callback, SYSTEM_CALLBACK_TYPE callbackmask = SYSTEM_CALLBACK_TYPE.ALL)
		{
			return default(RESULT);
		}

		public RESULT setPluginPath(string path)
		{
			return default(RESULT);
		}

		public RESULT loadPlugin(string filename, out uint handle, uint priority = 0u)
		{
			handle = default(uint);
			return default(RESULT);
		}

		public RESULT unloadPlugin(uint handle)
		{
			return default(RESULT);
		}

		public RESULT getNumNestedPlugins(uint handle, out int count)
		{
			count = default(int);
			return default(RESULT);
		}

		public RESULT getNestedPlugin(uint handle, int index, out uint nestedhandle)
		{
			nestedhandle = default(uint);
			return default(RESULT);
		}

		public RESULT getNumPlugins(PLUGINTYPE plugintype, out int numplugins)
		{
			numplugins = default(int);
			return default(RESULT);
		}

		public RESULT getPluginHandle(PLUGINTYPE plugintype, int index, out uint handle)
		{
			handle = default(uint);
			return default(RESULT);
		}

		public RESULT getPluginInfo(uint handle, out PLUGINTYPE plugintype, out string name, int namelen, out uint version)
		{
			plugintype = default(PLUGINTYPE);
			name = null;
			version = default(uint);
			return default(RESULT);
		}

		public RESULT getPluginInfo(uint handle, out PLUGINTYPE plugintype, out uint version)
		{
			plugintype = default(PLUGINTYPE);
			version = default(uint);
			return default(RESULT);
		}

		public RESULT setOutputByPlugin(uint handle)
		{
			return default(RESULT);
		}

		public RESULT getOutputByPlugin(out uint handle)
		{
			handle = default(uint);
			return default(RESULT);
		}

		public RESULT createDSPByPlugin(uint handle, out DSP dsp)
		{
			dsp = default(DSP);
			return default(RESULT);
		}

		public RESULT getDSPInfoByPlugin(uint handle, out IntPtr description)
		{
			description = default(IntPtr);
			return default(RESULT);
		}

		public RESULT registerDSP(ref DSP_DESCRIPTION description, out uint handle)
		{
			handle = default(uint);
			return default(RESULT);
		}

		public RESULT init(int maxchannels, INITFLAGS flags, IntPtr extradriverdata)
		{
			return default(RESULT);
		}

		public RESULT close()
		{
			return default(RESULT);
		}

		public RESULT update()
		{
			return default(RESULT);
		}

		public RESULT setSpeakerPosition(SPEAKER speaker, float x, float y, bool active)
		{
			return default(RESULT);
		}

		public RESULT getSpeakerPosition(SPEAKER speaker, out float x, out float y, out bool active)
		{
			x = default(float);
			y = default(float);
			active = default(bool);
			return default(RESULT);
		}

		public RESULT setStreamBufferSize(uint filebuffersize, TIMEUNIT filebuffersizetype)
		{
			return default(RESULT);
		}

		public RESULT getStreamBufferSize(out uint filebuffersize, out TIMEUNIT filebuffersizetype)
		{
			filebuffersize = default(uint);
			filebuffersizetype = default(TIMEUNIT);
			return default(RESULT);
		}

		public RESULT set3DSettings(float dopplerscale, float distancefactor, float rolloffscale)
		{
			return default(RESULT);
		}

		public RESULT get3DSettings(out float dopplerscale, out float distancefactor, out float rolloffscale)
		{
			dopplerscale = default(float);
			distancefactor = default(float);
			rolloffscale = default(float);
			return default(RESULT);
		}

		public RESULT set3DNumListeners(int numlisteners)
		{
			return default(RESULT);
		}

		public RESULT get3DNumListeners(out int numlisteners)
		{
			numlisteners = default(int);
			return default(RESULT);
		}

		public RESULT set3DListenerAttributes(int listener, ref VECTOR pos, ref VECTOR vel, ref VECTOR forward, ref VECTOR up)
		{
			return default(RESULT);
		}

		public RESULT get3DListenerAttributes(int listener, out VECTOR pos, out VECTOR vel, out VECTOR forward, out VECTOR up)
		{
			pos = default(VECTOR);
			vel = default(VECTOR);
			forward = default(VECTOR);
			up = default(VECTOR);
			return default(RESULT);
		}

		public RESULT set3DRolloffCallback(CB_3D_ROLLOFF_CALLBACK callback)
		{
			return default(RESULT);
		}

		public RESULT mixerSuspend()
		{
			return default(RESULT);
		}

		public RESULT mixerResume()
		{
			return default(RESULT);
		}

		public RESULT getDefaultMixMatrix(SPEAKERMODE sourcespeakermode, SPEAKERMODE targetspeakermode, float[] matrix, int matrixhop)
		{
			return default(RESULT);
		}

		public RESULT getSpeakerModeChannels(SPEAKERMODE mode, out int channels)
		{
			channels = default(int);
			return default(RESULT);
		}

		public RESULT getVersion(out uint version)
		{
			version = default(uint);
			return default(RESULT);
		}

		public RESULT getVersion(out uint version, out uint buildnumber)
		{
			version = default(uint);
			buildnumber = default(uint);
			return default(RESULT);
		}

		public RESULT getOutputHandle(out IntPtr handle)
		{
			handle = default(IntPtr);
			return default(RESULT);
		}

		public RESULT getChannelsPlaying(out int channels)
		{
			channels = default(int);
			return default(RESULT);
		}

		public RESULT getChannelsPlaying(out int channels, out int realchannels)
		{
			channels = default(int);
			realchannels = default(int);
			return default(RESULT);
		}

		public RESULT getCPUUsage(out CPU_USAGE usage)
		{
			usage = default(CPU_USAGE);
			return default(RESULT);
		}

		public RESULT getFileUsage(out long sampleBytesRead, out long streamBytesRead, out long otherBytesRead)
		{
			sampleBytesRead = default(long);
			streamBytesRead = default(long);
			otherBytesRead = default(long);
			return default(RESULT);
		}

		public RESULT createSound(string name, MODE mode, ref CREATESOUNDEXINFO exinfo, out Sound sound)
		{
			sound = default(Sound);
			return default(RESULT);
		}

		public RESULT createSound(byte[] data, MODE mode, ref CREATESOUNDEXINFO exinfo, out Sound sound)
		{
			sound = default(Sound);
			return default(RESULT);
		}

		public RESULT createSound(IntPtr name_or_data, MODE mode, ref CREATESOUNDEXINFO exinfo, out Sound sound)
		{
			sound = default(Sound);
			return default(RESULT);
		}

		public RESULT createSound(string name, MODE mode, out Sound sound)
		{
			sound = default(Sound);
			return default(RESULT);
		}

		public RESULT createStream(string name, MODE mode, ref CREATESOUNDEXINFO exinfo, out Sound sound)
		{
			sound = default(Sound);
			return default(RESULT);
		}

		public RESULT createStream(byte[] data, MODE mode, ref CREATESOUNDEXINFO exinfo, out Sound sound)
		{
			sound = default(Sound);
			return default(RESULT);
		}

		public RESULT createStream(IntPtr name_or_data, MODE mode, ref CREATESOUNDEXINFO exinfo, out Sound sound)
		{
			sound = default(Sound);
			return default(RESULT);
		}

		public RESULT createStream(string name, MODE mode, out Sound sound)
		{
			sound = default(Sound);
			return default(RESULT);
		}

		public RESULT createDSP(ref DSP_DESCRIPTION description, out DSP dsp)
		{
			dsp = default(DSP);
			return default(RESULT);
		}

		public RESULT createDSPByType(DSP_TYPE type, out DSP dsp)
		{
			dsp = default(DSP);
			return default(RESULT);
		}

		public RESULT createDSPConnection(DSPCONNECTION_TYPE type, out DSPConnection connection)
		{
			connection = default(DSPConnection);
			return default(RESULT);
		}

		public RESULT createChannelGroup(string name, out ChannelGroup channelgroup)
		{
			channelgroup = default(ChannelGroup);
			return default(RESULT);
		}

		public RESULT createSoundGroup(string name, out SoundGroup soundgroup)
		{
			soundgroup = default(SoundGroup);
			return default(RESULT);
		}

		public RESULT createReverb3D(out Reverb3D reverb)
		{
			reverb = default(Reverb3D);
			return default(RESULT);
		}

		public RESULT playSound(Sound sound, ChannelGroup channelgroup, bool paused, out Channel channel)
		{
			channel = default(Channel);
			return default(RESULT);
		}

		public RESULT playDSP(DSP dsp, ChannelGroup channelgroup, bool paused, out Channel channel)
		{
			channel = default(Channel);
			return default(RESULT);
		}

		public RESULT getChannel(int channelid, out Channel channel)
		{
			channel = default(Channel);
			return default(RESULT);
		}

		public RESULT getDSPInfoByType(DSP_TYPE type, out IntPtr description)
		{
			description = default(IntPtr);
			return default(RESULT);
		}

		public RESULT getMasterChannelGroup(out ChannelGroup channelgroup)
		{
			channelgroup = default(ChannelGroup);
			return default(RESULT);
		}

		public RESULT getMasterSoundGroup(out SoundGroup soundgroup)
		{
			soundgroup = default(SoundGroup);
			return default(RESULT);
		}

		public RESULT attachChannelGroupToPort(PORT_TYPE portType, ulong portIndex, ChannelGroup channelgroup, bool passThru = false)
		{
			return default(RESULT);
		}

		public RESULT detachChannelGroupFromPort(ChannelGroup channelgroup)
		{
			return default(RESULT);
		}

		public RESULT setReverbProperties(int instance, ref REVERB_PROPERTIES prop)
		{
			return default(RESULT);
		}

		public RESULT getReverbProperties(int instance, out REVERB_PROPERTIES prop)
		{
			prop = default(REVERB_PROPERTIES);
			return default(RESULT);
		}

		public RESULT lockDSP()
		{
			return default(RESULT);
		}

		public RESULT unlockDSP()
		{
			return default(RESULT);
		}

		public RESULT getRecordNumDrivers(out int numdrivers, out int numconnected)
		{
			numdrivers = default(int);
			numconnected = default(int);
			return default(RESULT);
		}

		public RESULT getRecordDriverInfo(int id, out string name, int namelen, out Guid guid, out int systemrate, out SPEAKERMODE speakermode, out int speakermodechannels, out DRIVER_STATE state)
		{
			name = null;
			guid = default(Guid);
			systemrate = default(int);
			speakermode = default(SPEAKERMODE);
			speakermodechannels = default(int);
			state = default(DRIVER_STATE);
			return default(RESULT);
		}

		public RESULT getRecordDriverInfo(int id, out Guid guid, out int systemrate, out SPEAKERMODE speakermode, out int speakermodechannels, out DRIVER_STATE state)
		{
			guid = default(Guid);
			systemrate = default(int);
			speakermode = default(SPEAKERMODE);
			speakermodechannels = default(int);
			state = default(DRIVER_STATE);
			return default(RESULT);
		}

		public RESULT getRecordPosition(int id, out uint position)
		{
			position = default(uint);
			return default(RESULT);
		}

		public RESULT recordStart(int id, Sound sound, bool loop)
		{
			return default(RESULT);
		}

		public RESULT recordStop(int id)
		{
			return default(RESULT);
		}

		public RESULT isRecording(int id, out bool recording)
		{
			recording = default(bool);
			return default(RESULT);
		}

		public RESULT createGeometry(int maxpolygons, int maxvertices, out Geometry geometry)
		{
			geometry = default(Geometry);
			return default(RESULT);
		}

		public RESULT setGeometrySettings(float maxworldsize)
		{
			return default(RESULT);
		}

		public RESULT getGeometrySettings(out float maxworldsize)
		{
			maxworldsize = default(float);
			return default(RESULT);
		}

		public RESULT loadGeometry(IntPtr data, int datasize, out Geometry geometry)
		{
			geometry = default(Geometry);
			return default(RESULT);
		}

		public RESULT getGeometryOcclusion(ref VECTOR listener, ref VECTOR source, out float direct, out float reverb)
		{
			direct = default(float);
			reverb = default(float);
			return default(RESULT);
		}

		public RESULT setNetworkProxy(string proxy)
		{
			return default(RESULT);
		}

		public RESULT getNetworkProxy(out string proxy, int proxylen)
		{
			proxy = null;
			return default(RESULT);
		}

		public RESULT setNetworkTimeout(int timeout)
		{
			return default(RESULT);
		}

		public RESULT getNetworkTimeout(out int timeout)
		{
			timeout = default(int);
			return default(RESULT);
		}

		public RESULT setUserData(IntPtr userdata)
		{
			return default(RESULT);
		}

		public RESULT getUserData(out IntPtr userdata)
		{
			userdata = default(IntPtr);
			return default(RESULT);
		}

		[PreserveSig]
		private static extern RESULT FMOD5_System_Release(IntPtr system);

		[PreserveSig]
		private static extern RESULT FMOD5_System_SetOutput(IntPtr system, OUTPUTTYPE output);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetOutput(IntPtr system, out OUTPUTTYPE output);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetNumDrivers(IntPtr system, out int numdrivers);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetDriverInfo(IntPtr system, int id, IntPtr name, int namelen, out Guid guid, out int systemrate, out SPEAKERMODE speakermode, out int speakermodechannels);

		[PreserveSig]
		private static extern RESULT FMOD5_System_SetDriver(IntPtr system, int driver);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetDriver(IntPtr system, out int driver);

		[PreserveSig]
		private static extern RESULT FMOD5_System_SetSoftwareChannels(IntPtr system, int numsoftwarechannels);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetSoftwareChannels(IntPtr system, out int numsoftwarechannels);

		[PreserveSig]
		private static extern RESULT FMOD5_System_SetSoftwareFormat(IntPtr system, int samplerate, SPEAKERMODE speakermode, int numrawspeakers);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetSoftwareFormat(IntPtr system, out int samplerate, out SPEAKERMODE speakermode, out int numrawspeakers);

		[PreserveSig]
		private static extern RESULT FMOD5_System_SetDSPBufferSize(IntPtr system, uint bufferlength, int numbuffers);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetDSPBufferSize(IntPtr system, out uint bufferlength, out int numbuffers);

		[PreserveSig]
		private static extern RESULT FMOD5_System_SetFileSystem(IntPtr system, FILE_OPEN_CALLBACK useropen, FILE_CLOSE_CALLBACK userclose, FILE_READ_CALLBACK userread, FILE_SEEK_CALLBACK userseek, FILE_ASYNCREAD_CALLBACK userasyncread, FILE_ASYNCCANCEL_CALLBACK userasynccancel, int blockalign);

		[PreserveSig]
		private static extern RESULT FMOD5_System_AttachFileSystem(IntPtr system, FILE_OPEN_CALLBACK useropen, FILE_CLOSE_CALLBACK userclose, FILE_READ_CALLBACK userread, FILE_SEEK_CALLBACK userseek);

		[PreserveSig]
		private static extern RESULT FMOD5_System_SetAdvancedSettings(IntPtr system, ref ADVANCEDSETTINGS settings);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetAdvancedSettings(IntPtr system, ref ADVANCEDSETTINGS settings);

		[PreserveSig]
		private static extern RESULT FMOD5_System_SetCallback(IntPtr system, SYSTEM_CALLBACK callback, SYSTEM_CALLBACK_TYPE callbackmask);

		[PreserveSig]
		private static extern RESULT FMOD5_System_SetPluginPath(IntPtr system, byte[] path);

		[PreserveSig]
		private static extern RESULT FMOD5_System_LoadPlugin(IntPtr system, byte[] filename, out uint handle, uint priority);

		[PreserveSig]
		private static extern RESULT FMOD5_System_UnloadPlugin(IntPtr system, uint handle);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetNumNestedPlugins(IntPtr system, uint handle, out int count);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetNestedPlugin(IntPtr system, uint handle, int index, out uint nestedhandle);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetNumPlugins(IntPtr system, PLUGINTYPE plugintype, out int numplugins);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetPluginHandle(IntPtr system, PLUGINTYPE plugintype, int index, out uint handle);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetPluginInfo(IntPtr system, uint handle, out PLUGINTYPE plugintype, IntPtr name, int namelen, out uint version);

		[PreserveSig]
		private static extern RESULT FMOD5_System_SetOutputByPlugin(IntPtr system, uint handle);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetOutputByPlugin(IntPtr system, out uint handle);

		[PreserveSig]
		private static extern RESULT FMOD5_System_CreateDSPByPlugin(IntPtr system, uint handle, out IntPtr dsp);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetDSPInfoByPlugin(IntPtr system, uint handle, out IntPtr description);

		[PreserveSig]
		private static extern RESULT FMOD5_System_RegisterDSP(IntPtr system, ref DSP_DESCRIPTION description, out uint handle);

		[PreserveSig]
		private static extern RESULT FMOD5_System_Init(IntPtr system, int maxchannels, INITFLAGS flags, IntPtr extradriverdata);

		[PreserveSig]
		private static extern RESULT FMOD5_System_Close(IntPtr system);

		[PreserveSig]
		private static extern RESULT FMOD5_System_Update(IntPtr system);

		[PreserveSig]
		private static extern RESULT FMOD5_System_SetSpeakerPosition(IntPtr system, SPEAKER speaker, float x, float y, bool active);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetSpeakerPosition(IntPtr system, SPEAKER speaker, out float x, out float y, out bool active);

		[PreserveSig]
		private static extern RESULT FMOD5_System_SetStreamBufferSize(IntPtr system, uint filebuffersize, TIMEUNIT filebuffersizetype);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetStreamBufferSize(IntPtr system, out uint filebuffersize, out TIMEUNIT filebuffersizetype);

		[PreserveSig]
		private static extern RESULT FMOD5_System_Set3DSettings(IntPtr system, float dopplerscale, float distancefactor, float rolloffscale);

		[PreserveSig]
		private static extern RESULT FMOD5_System_Get3DSettings(IntPtr system, out float dopplerscale, out float distancefactor, out float rolloffscale);

		[PreserveSig]
		private static extern RESULT FMOD5_System_Set3DNumListeners(IntPtr system, int numlisteners);

		[PreserveSig]
		private static extern RESULT FMOD5_System_Get3DNumListeners(IntPtr system, out int numlisteners);

		[PreserveSig]
		private static extern RESULT FMOD5_System_Set3DListenerAttributes(IntPtr system, int listener, ref VECTOR pos, ref VECTOR vel, ref VECTOR forward, ref VECTOR up);

		[PreserveSig]
		private static extern RESULT FMOD5_System_Get3DListenerAttributes(IntPtr system, int listener, out VECTOR pos, out VECTOR vel, out VECTOR forward, out VECTOR up);

		[PreserveSig]
		private static extern RESULT FMOD5_System_Set3DRolloffCallback(IntPtr system, CB_3D_ROLLOFF_CALLBACK callback);

		[PreserveSig]
		private static extern RESULT FMOD5_System_MixerSuspend(IntPtr system);

		[PreserveSig]
		private static extern RESULT FMOD5_System_MixerResume(IntPtr system);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetDefaultMixMatrix(IntPtr system, SPEAKERMODE sourcespeakermode, SPEAKERMODE targetspeakermode, float[] matrix, int matrixhop);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetSpeakerModeChannels(IntPtr system, SPEAKERMODE mode, out int channels);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetVersion(IntPtr system, out uint version, out uint buildnumber);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetOutputHandle(IntPtr system, out IntPtr handle);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetChannelsPlaying(IntPtr system, out int channels, IntPtr zero);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetChannelsPlaying(IntPtr system, out int channels, out int realchannels);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetCPUUsage(IntPtr system, out CPU_USAGE usage);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetFileUsage(IntPtr system, out long sampleBytesRead, out long streamBytesRead, out long otherBytesRead);

		[PreserveSig]
		private static extern RESULT FMOD5_System_CreateSound(IntPtr system, byte[] name_or_data, MODE mode, ref CREATESOUNDEXINFO exinfo, out IntPtr sound);

		[PreserveSig]
		private static extern RESULT FMOD5_System_CreateSound(IntPtr system, IntPtr name_or_data, MODE mode, ref CREATESOUNDEXINFO exinfo, out IntPtr sound);

		[PreserveSig]
		private static extern RESULT FMOD5_System_CreateStream(IntPtr system, byte[] name_or_data, MODE mode, ref CREATESOUNDEXINFO exinfo, out IntPtr sound);

		[PreserveSig]
		private static extern RESULT FMOD5_System_CreateStream(IntPtr system, IntPtr name_or_data, MODE mode, ref CREATESOUNDEXINFO exinfo, out IntPtr sound);

		[PreserveSig]
		private static extern RESULT FMOD5_System_CreateDSP(IntPtr system, ref DSP_DESCRIPTION description, out IntPtr dsp);

		[PreserveSig]
		private static extern RESULT FMOD5_System_CreateDSPByType(IntPtr system, DSP_TYPE type, out IntPtr dsp);

		[PreserveSig]
		private static extern RESULT FMOD5_System_CreateDSPConnection(IntPtr system, DSPCONNECTION_TYPE type, out IntPtr connection);

		[PreserveSig]
		private static extern RESULT FMOD5_System_CreateChannelGroup(IntPtr system, byte[] name, out IntPtr channelgroup);

		[PreserveSig]
		private static extern RESULT FMOD5_System_CreateSoundGroup(IntPtr system, byte[] name, out IntPtr soundgroup);

		[PreserveSig]
		private static extern RESULT FMOD5_System_CreateReverb3D(IntPtr system, out IntPtr reverb);

		[PreserveSig]
		private static extern RESULT FMOD5_System_PlaySound(IntPtr system, IntPtr sound, IntPtr channelgroup, bool paused, out IntPtr channel);

		[PreserveSig]
		private static extern RESULT FMOD5_System_PlayDSP(IntPtr system, IntPtr dsp, IntPtr channelgroup, bool paused, out IntPtr channel);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetChannel(IntPtr system, int channelid, out IntPtr channel);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetDSPInfoByType(IntPtr system, DSP_TYPE type, out IntPtr description);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetMasterChannelGroup(IntPtr system, out IntPtr channelgroup);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetMasterSoundGroup(IntPtr system, out IntPtr soundgroup);

		[PreserveSig]
		private static extern RESULT FMOD5_System_AttachChannelGroupToPort(IntPtr system, PORT_TYPE portType, ulong portIndex, IntPtr channelgroup, bool passThru);

		[PreserveSig]
		private static extern RESULT FMOD5_System_DetachChannelGroupFromPort(IntPtr system, IntPtr channelgroup);

		[PreserveSig]
		private static extern RESULT FMOD5_System_SetReverbProperties(IntPtr system, int instance, ref REVERB_PROPERTIES prop);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetReverbProperties(IntPtr system, int instance, out REVERB_PROPERTIES prop);

		[PreserveSig]
		private static extern RESULT FMOD5_System_LockDSP(IntPtr system);

		[PreserveSig]
		private static extern RESULT FMOD5_System_UnlockDSP(IntPtr system);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetRecordNumDrivers(IntPtr system, out int numdrivers, out int numconnected);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetRecordDriverInfo(IntPtr system, int id, IntPtr name, int namelen, out Guid guid, out int systemrate, out SPEAKERMODE speakermode, out int speakermodechannels, out DRIVER_STATE state);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetRecordPosition(IntPtr system, int id, out uint position);

		[PreserveSig]
		private static extern RESULT FMOD5_System_RecordStart(IntPtr system, int id, IntPtr sound, bool loop);

		[PreserveSig]
		private static extern RESULT FMOD5_System_RecordStop(IntPtr system, int id);

		[PreserveSig]
		private static extern RESULT FMOD5_System_IsRecording(IntPtr system, int id, out bool recording);

		[PreserveSig]
		private static extern RESULT FMOD5_System_CreateGeometry(IntPtr system, int maxpolygons, int maxvertices, out IntPtr geometry);

		[PreserveSig]
		private static extern RESULT FMOD5_System_SetGeometrySettings(IntPtr system, float maxworldsize);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetGeometrySettings(IntPtr system, out float maxworldsize);

		[PreserveSig]
		private static extern RESULT FMOD5_System_LoadGeometry(IntPtr system, IntPtr data, int datasize, out IntPtr geometry);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetGeometryOcclusion(IntPtr system, ref VECTOR listener, ref VECTOR source, out float direct, out float reverb);

		[PreserveSig]
		private static extern RESULT FMOD5_System_SetNetworkProxy(IntPtr system, byte[] proxy);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetNetworkProxy(IntPtr system, IntPtr proxy, int proxylen);

		[PreserveSig]
		private static extern RESULT FMOD5_System_SetNetworkTimeout(IntPtr system, int timeout);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetNetworkTimeout(IntPtr system, out int timeout);

		[PreserveSig]
		private static extern RESULT FMOD5_System_SetUserData(IntPtr system, IntPtr userdata);

		[PreserveSig]
		private static extern RESULT FMOD5_System_GetUserData(IntPtr system, out IntPtr userdata);

		public System(IntPtr ptr)
		{
			handle = (IntPtr)0;
		}

		public bool hasHandle()
		{
			return false;
		}

		public void clearHandle()
		{
		}
	}
}
