using System;
using System.Runtime.InteropServices;

namespace FMOD
{
	public struct DSP
	{
		public IntPtr handle;

		public RESULT release()
		{
			return default(RESULT);
		}

		public RESULT getSystemObject(out System system)
		{
			system = default(System);
			return default(RESULT);
		}

		public RESULT addInput(DSP input)
		{
			return default(RESULT);
		}

		public RESULT addInput(DSP input, out DSPConnection connection, DSPCONNECTION_TYPE type = DSPCONNECTION_TYPE.STANDARD)
		{
			connection = default(DSPConnection);
			return default(RESULT);
		}

		public RESULT addInputPreallocated(DSP input, DSPConnection connection)
		{
			return default(RESULT);
		}

		public RESULT disconnectFrom(DSP target, DSPConnection connection)
		{
			return default(RESULT);
		}

		public RESULT disconnectAll(bool inputs, bool outputs)
		{
			return default(RESULT);
		}

		public RESULT getNumInputs(out int numinputs)
		{
			numinputs = default(int);
			return default(RESULT);
		}

		public RESULT getNumOutputs(out int numoutputs)
		{
			numoutputs = default(int);
			return default(RESULT);
		}

		public RESULT getInput(int index, out DSP input, out DSPConnection inputconnection)
		{
			input = default(DSP);
			inputconnection = default(DSPConnection);
			return default(RESULT);
		}

		public RESULT getOutput(int index, out DSP output, out DSPConnection outputconnection)
		{
			output = default(DSP);
			outputconnection = default(DSPConnection);
			return default(RESULT);
		}

		public RESULT setActive(bool active)
		{
			return default(RESULT);
		}

		public RESULT getActive(out bool active)
		{
			active = default(bool);
			return default(RESULT);
		}

		public RESULT setBypass(bool bypass)
		{
			return default(RESULT);
		}

		public RESULT getBypass(out bool bypass)
		{
			bypass = default(bool);
			return default(RESULT);
		}

		public RESULT setWetDryMix(float prewet, float postwet, float dry)
		{
			return default(RESULT);
		}

		public RESULT getWetDryMix(out float prewet, out float postwet, out float dry)
		{
			prewet = default(float);
			postwet = default(float);
			dry = default(float);
			return default(RESULT);
		}

		public RESULT setChannelFormat(CHANNELMASK channelmask, int numchannels, SPEAKERMODE source_speakermode)
		{
			return default(RESULT);
		}

		public RESULT getChannelFormat(out CHANNELMASK channelmask, out int numchannels, out SPEAKERMODE source_speakermode)
		{
			channelmask = default(CHANNELMASK);
			numchannels = default(int);
			source_speakermode = default(SPEAKERMODE);
			return default(RESULT);
		}

		public RESULT getOutputChannelFormat(CHANNELMASK inmask, int inchannels, SPEAKERMODE inspeakermode, out CHANNELMASK outmask, out int outchannels, out SPEAKERMODE outspeakermode)
		{
			outmask = default(CHANNELMASK);
			outchannels = default(int);
			outspeakermode = default(SPEAKERMODE);
			return default(RESULT);
		}

		public RESULT reset()
		{
			return default(RESULT);
		}

		public RESULT setCallback(DSP_CALLBACK callback)
		{
			return default(RESULT);
		}

		public RESULT setParameterFloat(int index, float value)
		{
			return default(RESULT);
		}

		public RESULT setParameterInt(int index, int value)
		{
			return default(RESULT);
		}

		public RESULT setParameterBool(int index, bool value)
		{
			return default(RESULT);
		}

		public RESULT setParameterData(int index, byte[] data)
		{
			return default(RESULT);
		}

		public RESULT getParameterFloat(int index, out float value)
		{
			value = default(float);
			return default(RESULT);
		}

		public RESULT getParameterInt(int index, out int value)
		{
			value = default(int);
			return default(RESULT);
		}

		public RESULT getParameterBool(int index, out bool value)
		{
			value = default(bool);
			return default(RESULT);
		}

		public RESULT getParameterData(int index, out IntPtr data, out uint length)
		{
			data = default(IntPtr);
			length = default(uint);
			return default(RESULT);
		}

		public RESULT getNumParameters(out int numparams)
		{
			numparams = default(int);
			return default(RESULT);
		}

		public RESULT getParameterInfo(int index, out DSP_PARAMETER_DESC desc)
		{
			desc = default(DSP_PARAMETER_DESC);
			return default(RESULT);
		}

		public RESULT getDataParameterIndex(int datatype, out int index)
		{
			index = default(int);
			return default(RESULT);
		}

		public RESULT showConfigDialog(IntPtr hwnd, bool show)
		{
			return default(RESULT);
		}

		public RESULT getInfo(out string name, out uint version, out int channels, out int configwidth, out int configheight)
		{
			name = null;
			version = default(uint);
			channels = default(int);
			configwidth = default(int);
			configheight = default(int);
			return default(RESULT);
		}

		public RESULT getInfo(out uint version, out int channels, out int configwidth, out int configheight)
		{
			version = default(uint);
			channels = default(int);
			configwidth = default(int);
			configheight = default(int);
			return default(RESULT);
		}

		public RESULT getType(out DSP_TYPE type)
		{
			type = default(DSP_TYPE);
			return default(RESULT);
		}

		public RESULT getIdle(out bool idle)
		{
			idle = default(bool);
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

		public RESULT setMeteringEnabled(bool inputEnabled, bool outputEnabled)
		{
			return default(RESULT);
		}

		public RESULT getMeteringEnabled(out bool inputEnabled, out bool outputEnabled)
		{
			inputEnabled = default(bool);
			outputEnabled = default(bool);
			return default(RESULT);
		}

		public RESULT getMeteringInfo(IntPtr zero, out DSP_METERING_INFO outputInfo)
		{
			outputInfo = default(DSP_METERING_INFO);
			return default(RESULT);
		}

		public RESULT getMeteringInfo(out DSP_METERING_INFO inputInfo, IntPtr zero)
		{
			inputInfo = default(DSP_METERING_INFO);
			return default(RESULT);
		}

		public RESULT getMeteringInfo(out DSP_METERING_INFO inputInfo, out DSP_METERING_INFO outputInfo)
		{
			inputInfo = default(DSP_METERING_INFO);
			outputInfo = default(DSP_METERING_INFO);
			return default(RESULT);
		}

		public RESULT getCPUUsage(out uint exclusive, out uint inclusive)
		{
			exclusive = default(uint);
			inclusive = default(uint);
			return default(RESULT);
		}

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_Release(IntPtr dsp);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_GetSystemObject(IntPtr dsp, out IntPtr system);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_AddInput(IntPtr dsp, IntPtr input, IntPtr zero, DSPCONNECTION_TYPE type);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_AddInput(IntPtr dsp, IntPtr input, out IntPtr connection, DSPCONNECTION_TYPE type);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_AddInputPreallocated(IntPtr dsp, IntPtr input, out IntPtr connection);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_DisconnectFrom(IntPtr dsp, IntPtr target, IntPtr connection);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_DisconnectAll(IntPtr dsp, bool inputs, bool outputs);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_GetNumInputs(IntPtr dsp, out int numinputs);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_GetNumOutputs(IntPtr dsp, out int numoutputs);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_GetInput(IntPtr dsp, int index, out IntPtr input, out IntPtr inputconnection);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_GetOutput(IntPtr dsp, int index, out IntPtr output, out IntPtr outputconnection);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_SetActive(IntPtr dsp, bool active);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_GetActive(IntPtr dsp, out bool active);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_SetBypass(IntPtr dsp, bool bypass);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_GetBypass(IntPtr dsp, out bool bypass);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_SetWetDryMix(IntPtr dsp, float prewet, float postwet, float dry);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_GetWetDryMix(IntPtr dsp, out float prewet, out float postwet, out float dry);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_SetChannelFormat(IntPtr dsp, CHANNELMASK channelmask, int numchannels, SPEAKERMODE source_speakermode);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_GetChannelFormat(IntPtr dsp, out CHANNELMASK channelmask, out int numchannels, out SPEAKERMODE source_speakermode);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_GetOutputChannelFormat(IntPtr dsp, CHANNELMASK inmask, int inchannels, SPEAKERMODE inspeakermode, out CHANNELMASK outmask, out int outchannels, out SPEAKERMODE outspeakermode);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_Reset(IntPtr dsp);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_SetCallback(IntPtr dsp, DSP_CALLBACK callback);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_SetParameterFloat(IntPtr dsp, int index, float value);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_SetParameterInt(IntPtr dsp, int index, int value);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_SetParameterBool(IntPtr dsp, int index, bool value);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_SetParameterData(IntPtr dsp, int index, byte[] data, uint length);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_GetParameterFloat(IntPtr dsp, int index, out float value, IntPtr valuestr, int valuestrlen);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_GetParameterInt(IntPtr dsp, int index, out int value, IntPtr valuestr, int valuestrlen);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_GetParameterBool(IntPtr dsp, int index, out bool value, IntPtr valuestr, int valuestrlen);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_GetParameterData(IntPtr dsp, int index, out IntPtr data, out uint length, IntPtr valuestr, int valuestrlen);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_GetNumParameters(IntPtr dsp, out int numparams);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_GetParameterInfo(IntPtr dsp, int index, out IntPtr desc);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_GetDataParameterIndex(IntPtr dsp, int datatype, out int index);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_ShowConfigDialog(IntPtr dsp, IntPtr hwnd, bool show);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_GetInfo(IntPtr dsp, IntPtr name, out uint version, out int channels, out int configwidth, out int configheight);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_GetType(IntPtr dsp, out DSP_TYPE type);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_GetIdle(IntPtr dsp, out bool idle);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_SetUserData(IntPtr dsp, IntPtr userdata);

		[PreserveSig]
		private static extern RESULT FMOD5_DSP_GetUserData(IntPtr dsp, out IntPtr userdata);

		[PreserveSig]
		public static extern RESULT FMOD5_DSP_SetMeteringEnabled(IntPtr dsp, bool inputEnabled, bool outputEnabled);

		[PreserveSig]
		public static extern RESULT FMOD5_DSP_GetMeteringEnabled(IntPtr dsp, out bool inputEnabled, out bool outputEnabled);

		[PreserveSig]
		public static extern RESULT FMOD5_DSP_GetMeteringInfo(IntPtr dsp, IntPtr zero, out DSP_METERING_INFO outputInfo);

		[PreserveSig]
		public static extern RESULT FMOD5_DSP_GetMeteringInfo(IntPtr dsp, out DSP_METERING_INFO inputInfo, IntPtr zero);

		[PreserveSig]
		public static extern RESULT FMOD5_DSP_GetMeteringInfo(IntPtr dsp, out DSP_METERING_INFO inputInfo, out DSP_METERING_INFO outputInfo);

		[PreserveSig]
		public static extern RESULT FMOD5_DSP_GetCPUUsage(IntPtr dsp, out uint exclusive, out uint inclusive);

		public DSP(IntPtr ptr)
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
