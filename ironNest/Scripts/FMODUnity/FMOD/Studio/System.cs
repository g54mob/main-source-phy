using System;
using System.Runtime.InteropServices;

namespace FMOD.Studio
{
	public struct System
	{
		public IntPtr handle;

		public static RESULT create(out System system)
		{
			system = default(System);
			return default(RESULT);
		}

		public RESULT setAdvancedSettings(ADVANCEDSETTINGS settings)
		{
			return default(RESULT);
		}

		public RESULT setAdvancedSettings(ADVANCEDSETTINGS settings, string encryptionKey)
		{
			return default(RESULT);
		}

		public RESULT getAdvancedSettings(out ADVANCEDSETTINGS settings)
		{
			settings = default(ADVANCEDSETTINGS);
			return default(RESULT);
		}

		public RESULT initialize(int maxchannels, INITFLAGS studioflags, FMOD.INITFLAGS flags, IntPtr extradriverdata)
		{
			return default(RESULT);
		}

		public RESULT release()
		{
			return default(RESULT);
		}

		public RESULT update()
		{
			return default(RESULT);
		}

		public RESULT getCoreSystem(out FMOD.System coresystem)
		{
			coresystem = default(FMOD.System);
			return default(RESULT);
		}

		public RESULT getEvent(string path, out EventDescription _event)
		{
			_event = default(EventDescription);
			return default(RESULT);
		}

		public RESULT getBus(string path, out Bus bus)
		{
			bus = default(Bus);
			return default(RESULT);
		}

		public RESULT getVCA(string path, out VCA vca)
		{
			vca = default(VCA);
			return default(RESULT);
		}

		public RESULT getBank(string path, out Bank bank)
		{
			bank = default(Bank);
			return default(RESULT);
		}

		public RESULT getEventByID(GUID id, out EventDescription _event)
		{
			_event = default(EventDescription);
			return default(RESULT);
		}

		public RESULT getBusByID(GUID id, out Bus bus)
		{
			bus = default(Bus);
			return default(RESULT);
		}

		public RESULT getVCAByID(GUID id, out VCA vca)
		{
			vca = default(VCA);
			return default(RESULT);
		}

		public RESULT getBankByID(GUID id, out Bank bank)
		{
			bank = default(Bank);
			return default(RESULT);
		}

		public RESULT getSoundInfo(string key, out SOUND_INFO info)
		{
			info = default(SOUND_INFO);
			return default(RESULT);
		}

		public RESULT getParameterDescriptionByName(string name, out PARAMETER_DESCRIPTION parameter)
		{
			parameter = default(PARAMETER_DESCRIPTION);
			return default(RESULT);
		}

		public RESULT getParameterDescriptionByID(PARAMETER_ID id, out PARAMETER_DESCRIPTION parameter)
		{
			parameter = default(PARAMETER_DESCRIPTION);
			return default(RESULT);
		}

		public RESULT getParameterLabelByName(string name, int labelindex, out string label)
		{
			label = null;
			return default(RESULT);
		}

		public RESULT getParameterLabelByID(PARAMETER_ID id, int labelindex, out string label)
		{
			label = null;
			return default(RESULT);
		}

		public RESULT getParameterByID(PARAMETER_ID id, out float value)
		{
			value = default(float);
			return default(RESULT);
		}

		public RESULT getParameterByID(PARAMETER_ID id, out float value, out float finalvalue)
		{
			value = default(float);
			finalvalue = default(float);
			return default(RESULT);
		}

		public RESULT setParameterByID(PARAMETER_ID id, float value, bool ignoreseekspeed = false)
		{
			return default(RESULT);
		}

		public RESULT setParameterByIDWithLabel(PARAMETER_ID id, string label, bool ignoreseekspeed = false)
		{
			return default(RESULT);
		}

		public RESULT setParametersByIDs(PARAMETER_ID[] ids, float[] values, int count, bool ignoreseekspeed = false)
		{
			return default(RESULT);
		}

		public RESULT getParameterByName(string name, out float value)
		{
			value = default(float);
			return default(RESULT);
		}

		public RESULT getParameterByName(string name, out float value, out float finalvalue)
		{
			value = default(float);
			finalvalue = default(float);
			return default(RESULT);
		}

		public RESULT setParameterByName(string name, float value, bool ignoreseekspeed = false)
		{
			return default(RESULT);
		}

		public RESULT setParameterByNameWithLabel(string name, string label, bool ignoreseekspeed = false)
		{
			return default(RESULT);
		}

		public RESULT lookupID(string path, out GUID id)
		{
			id = default(GUID);
			return default(RESULT);
		}

		public RESULT lookupPath(GUID id, out string path)
		{
			path = null;
			return default(RESULT);
		}

		public RESULT getNumListeners(out int numlisteners)
		{
			numlisteners = default(int);
			return default(RESULT);
		}

		public RESULT setNumListeners(int numlisteners)
		{
			return default(RESULT);
		}

		public RESULT getListenerAttributes(int listener, out ATTRIBUTES_3D attributes)
		{
			attributes = default(ATTRIBUTES_3D);
			return default(RESULT);
		}

		public RESULT getListenerAttributes(int listener, out ATTRIBUTES_3D attributes, out VECTOR attenuationposition)
		{
			attributes = default(ATTRIBUTES_3D);
			attenuationposition = default(VECTOR);
			return default(RESULT);
		}

		public RESULT setListenerAttributes(int listener, ATTRIBUTES_3D attributes)
		{
			return default(RESULT);
		}

		public RESULT setListenerAttributes(int listener, ATTRIBUTES_3D attributes, VECTOR attenuationposition)
		{
			return default(RESULT);
		}

		public RESULT getListenerWeight(int listener, out float weight)
		{
			weight = default(float);
			return default(RESULT);
		}

		public RESULT setListenerWeight(int listener, float weight)
		{
			return default(RESULT);
		}

		public RESULT loadBankFile(string filename, LOAD_BANK_FLAGS flags, out Bank bank)
		{
			bank = default(Bank);
			return default(RESULT);
		}

		public RESULT loadBankMemory(byte[] buffer, LOAD_BANK_FLAGS flags, out Bank bank)
		{
			bank = default(Bank);
			return default(RESULT);
		}

		public RESULT loadBankMemory(IntPtr buffer, int length, LOAD_BANK_FLAGS flags, out Bank bank)
		{
			bank = default(Bank);
			return default(RESULT);
		}

		public RESULT loadBankCustom(BANK_INFO info, LOAD_BANK_FLAGS flags, out Bank bank)
		{
			bank = default(Bank);
			return default(RESULT);
		}

		public RESULT unloadAll()
		{
			return default(RESULT);
		}

		public RESULT flushCommands()
		{
			return default(RESULT);
		}

		public RESULT flushSampleLoading()
		{
			return default(RESULT);
		}

		public RESULT startCommandCapture(string filename, COMMANDCAPTURE_FLAGS flags)
		{
			return default(RESULT);
		}

		public RESULT stopCommandCapture()
		{
			return default(RESULT);
		}

		public RESULT loadCommandReplay(string filename, COMMANDREPLAY_FLAGS flags, out CommandReplay replay)
		{
			replay = default(CommandReplay);
			return default(RESULT);
		}

		public RESULT getBankCount(out int count)
		{
			count = default(int);
			return default(RESULT);
		}

		public RESULT getBankList(out Bank[] array)
		{
			array = null;
			return default(RESULT);
		}

		public RESULT getParameterDescriptionCount(out int count)
		{
			count = default(int);
			return default(RESULT);
		}

		public RESULT getParameterDescriptionList(out PARAMETER_DESCRIPTION[] array)
		{
			array = null;
			return default(RESULT);
		}

		public RESULT getCPUUsage(out CPU_USAGE usage, out FMOD.CPU_USAGE usage_core)
		{
			usage = default(CPU_USAGE);
			usage_core = default(FMOD.CPU_USAGE);
			return default(RESULT);
		}

		public RESULT getBufferUsage(out BUFFER_USAGE usage)
		{
			usage = default(BUFFER_USAGE);
			return default(RESULT);
		}

		public RESULT resetBufferUsage()
		{
			return default(RESULT);
		}

		public RESULT setCallback(SYSTEM_CALLBACK callback, SYSTEM_CALLBACK_TYPE callbackmask = SYSTEM_CALLBACK_TYPE.ALL)
		{
			return default(RESULT);
		}

		public RESULT getUserData(out IntPtr userdata)
		{
			userdata = default(IntPtr);
			return default(RESULT);
		}

		public RESULT setUserData(IntPtr userdata)
		{
			return default(RESULT);
		}

		public RESULT getMemoryUsage(out MEMORY_USAGE memoryusage)
		{
			memoryusage = default(MEMORY_USAGE);
			return default(RESULT);
		}

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_Create(out IntPtr system, uint headerversion);

		[PreserveSig]
		private static extern bool FMOD_Studio_System_IsValid(IntPtr system);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_SetAdvancedSettings(IntPtr system, ref ADVANCEDSETTINGS settings);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_GetAdvancedSettings(IntPtr system, out ADVANCEDSETTINGS settings);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_Initialize(IntPtr system, int maxchannels, INITFLAGS studioflags, FMOD.INITFLAGS flags, IntPtr extradriverdata);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_Release(IntPtr system);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_Update(IntPtr system);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_GetCoreSystem(IntPtr system, out IntPtr coresystem);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_GetEvent(IntPtr system, byte[] path, out IntPtr _event);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_GetBus(IntPtr system, byte[] path, out IntPtr bus);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_GetVCA(IntPtr system, byte[] path, out IntPtr vca);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_GetBank(IntPtr system, byte[] path, out IntPtr bank);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_GetEventByID(IntPtr system, ref GUID id, out IntPtr _event);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_GetBusByID(IntPtr system, ref GUID id, out IntPtr bus);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_GetVCAByID(IntPtr system, ref GUID id, out IntPtr vca);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_GetBankByID(IntPtr system, ref GUID id, out IntPtr bank);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_GetSoundInfo(IntPtr system, byte[] key, out SOUND_INFO info);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_GetParameterDescriptionByName(IntPtr system, byte[] name, out PARAMETER_DESCRIPTION parameter);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_GetParameterDescriptionByID(IntPtr system, PARAMETER_ID id, out PARAMETER_DESCRIPTION parameter);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_GetParameterLabelByName(IntPtr system, byte[] name, int labelindex, IntPtr label, int size, out int retrieved);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_GetParameterLabelByID(IntPtr system, PARAMETER_ID id, int labelindex, IntPtr label, int size, out int retrieved);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_GetParameterByID(IntPtr system, PARAMETER_ID id, out float value, out float finalvalue);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_SetParameterByID(IntPtr system, PARAMETER_ID id, float value, bool ignoreseekspeed);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_SetParameterByIDWithLabel(IntPtr system, PARAMETER_ID id, byte[] label, bool ignoreseekspeed);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_SetParametersByIDs(IntPtr system, PARAMETER_ID[] ids, float[] values, int count, bool ignoreseekspeed);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_GetParameterByName(IntPtr system, byte[] name, out float value, out float finalvalue);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_SetParameterByName(IntPtr system, byte[] name, float value, bool ignoreseekspeed);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_SetParameterByNameWithLabel(IntPtr system, byte[] name, byte[] label, bool ignoreseekspeed);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_LookupID(IntPtr system, byte[] path, out GUID id);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_LookupPath(IntPtr system, ref GUID id, IntPtr path, int size, out int retrieved);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_GetNumListeners(IntPtr system, out int numlisteners);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_SetNumListeners(IntPtr system, int numlisteners);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_GetListenerAttributes(IntPtr system, int listener, out ATTRIBUTES_3D attributes, IntPtr zero);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_GetListenerAttributes(IntPtr system, int listener, out ATTRIBUTES_3D attributes, out VECTOR attenuationposition);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_SetListenerAttributes(IntPtr system, int listener, ref ATTRIBUTES_3D attributes, IntPtr zero);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_SetListenerAttributes(IntPtr system, int listener, ref ATTRIBUTES_3D attributes, ref VECTOR attenuationposition);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_GetListenerWeight(IntPtr system, int listener, out float weight);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_SetListenerWeight(IntPtr system, int listener, float weight);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_LoadBankFile(IntPtr system, byte[] filename, LOAD_BANK_FLAGS flags, out IntPtr bank);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_LoadBankMemory(IntPtr system, IntPtr buffer, int length, LOAD_MEMORY_MODE mode, LOAD_BANK_FLAGS flags, out IntPtr bank);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_LoadBankCustom(IntPtr system, ref BANK_INFO info, LOAD_BANK_FLAGS flags, out IntPtr bank);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_UnloadAll(IntPtr system);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_FlushCommands(IntPtr system);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_FlushSampleLoading(IntPtr system);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_StartCommandCapture(IntPtr system, byte[] filename, COMMANDCAPTURE_FLAGS flags);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_StopCommandCapture(IntPtr system);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_LoadCommandReplay(IntPtr system, byte[] filename, COMMANDREPLAY_FLAGS flags, out IntPtr replay);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_GetBankCount(IntPtr system, out int count);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_GetBankList(IntPtr system, IntPtr[] array, int capacity, out int count);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_GetParameterDescriptionCount(IntPtr system, out int count);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_GetParameterDescriptionList(IntPtr system, [Out] PARAMETER_DESCRIPTION[] array, int capacity, out int count);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_GetCPUUsage(IntPtr system, out CPU_USAGE usage, out FMOD.CPU_USAGE usage_core);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_GetBufferUsage(IntPtr system, out BUFFER_USAGE usage);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_ResetBufferUsage(IntPtr system);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_SetCallback(IntPtr system, SYSTEM_CALLBACK callback, SYSTEM_CALLBACK_TYPE callbackmask);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_GetUserData(IntPtr system, out IntPtr userdata);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_SetUserData(IntPtr system, IntPtr userdata);

		[PreserveSig]
		private static extern RESULT FMOD_Studio_System_GetMemoryUsage(IntPtr system, out MEMORY_USAGE memoryusage);

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

		public bool isValid()
		{
			return false;
		}
	}
}
