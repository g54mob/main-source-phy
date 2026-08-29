using System;
using System.Runtime.InteropServices;
using FMOD;
using FMOD.Studio;
using Photon.Voice;
using UnityEngine;

public class PineFmodAudioOut<T> : AudioOutDelayControl<T>
{
	protected readonly int sizeofT = Marshal.SizeOf(default(T));

	public static MODE DEFAULT_MODE = MODE.LOOP_NORMAL | MODE.OPENUSER;

	private FMOD.System coreSystem;

	private FMOD.Sound sound;

	private Channel channel;

	private SOUND_FORMAT soundFormat;

	private bool isSteamVoice;

	private bool isVoiceChannelInVoiceBus;

	public FMOD.Sound Sound => sound;

	public Channel Channel => channel;

	public override int OutPos
	{
		get
		{
			channel.getPosition(out var position, TIMEUNIT.PCMBYTES);
			return (int)(position / channels / sizeofT);
		}
	}

	public string Error { get; private set; }

	public PineFmodAudioOut(FMOD.System coreSystem, PlayDelayConfig playDelayConfig, Photon.Voice.ILogger logger, string logPrefix, bool debugInfo, bool isSteamVoice)
		: base(false, playDelayConfig, logger, "[PV] [FMOD] PineFmodAudioOut" + ((logPrefix == "") ? "" : " ") + logPrefix + " ", debugInfo)
	{
		this.isSteamVoice = isSteamVoice;
		if (isSteamVoice)
		{
			soundFormat = SOUND_FORMAT.PCM16;
		}
		else if (sizeofT == 2)
		{
			soundFormat = SOUND_FORMAT.PCM16;
		}
		else
		{
			if (sizeofT != 4)
			{
				Error = "only float and short buffers are supported: " + typeof(T);
				logger.LogError(logPrefix + Error);
				return;
			}
			soundFormat = SOUND_FORMAT.PCMFLOAT;
		}
		this.coreSystem = coreSystem;
	}

	public override void OutCreate(int samplingRate, int channels, int bufferSamples)
	{
		CREATESOUNDEXINFO exinfo = default(CREATESOUNDEXINFO);
		exinfo.cbsize = Marshal.SizeOf(exinfo);
		exinfo.numchannels = channels;
		exinfo.format = soundFormat;
		exinfo.defaultfrequency = samplingRate;
		exinfo.length = (uint)(bufferSamples * channels * sizeofT);
		RESULT rESULT = coreSystem.createSound("Photon AudioOut", DEFAULT_MODE, ref exinfo, out sound);
		if (rESULT != RESULT.OK)
		{
			Error = "failed to createSound: " + rESULT;
			logger.LogError(logPrefix + Error);
		}
		else
		{
			logger.LogInfo(logPrefix + "Sound Created" + sound.handle);
		}
	}

	public override void OutStart()
	{
		PineFmod.getChannelGroup(PineFmod.getBus("bus:/Voice"), out var channelGroup);
		RESULT rESULT = coreSystem.playSound(sound, channelGroup, paused: false, out channel);
		if (rESULT != RESULT.OK)
		{
			Error = "failed to playSound: " + rESULT;
			logger.LogError(logPrefix + Error);
		}
	}

	public override void OutWrite(T[] frame, int offsetSamples)
	{
		if (sound.handle == IntPtr.Zero)
		{
			UnityEngine.Debug.Log("Cannot OutWrite for sound with IntPtr.Zero handle [TEST]");
		}
		else
		{
			if (Error != null)
			{
				return;
			}
			if (!isVoiceChannelInVoiceBus)
			{
				Bus bus = PineFmod.getBus("bus:/Voice");
				PineFmod.lockChannelGroup(bus);
				PineFmod.flushCommands();
				PineFmod.getChannelGroup(bus, out var channelGroup);
				if (channelGroup.handle != IntPtr.Zero)
				{
					channel.setChannelGroup(channelGroup);
					PineFmod.flushCommands();
					isVoiceChannelInVoiceBus = true;
					channel.getChannelGroup(out var channelgroup);
					UnityEngine.Debug.LogError($"[PineFmodAudioOut] Channel group of voice bus exists ({channelGroup.handle}) and set to channel {channel.handle}; setChannelGroup: {channelgroup.handle}");
				}
				else
				{
					UnityEngine.Debug.LogError("[PineFmodAudioOut] Channel group of voice bus is IntPtr.Zero");
				}
			}
			RESULT rESULT = sound.@lock((uint)(offsetSamples * sizeofT * channels), (uint)(frame.Length * sizeofT), out var ptr, out var ptr2, out var len, out var len2);
			if (rESULT != RESULT.OK)
			{
				Error = "failed to lock sound buffer: " + rESULT;
				logger.LogError(logPrefix + Error);
				return;
			}
			int num = (int)len / sizeofT;
			int length = (int)len2 / sizeofT;
			if (isSteamVoice)
			{
				Marshal.Copy(frame as byte[], 0, ptr, num);
				if (ptr2 != IntPtr.Zero)
				{
					Marshal.Copy(frame as byte[], num, ptr2, length);
				}
			}
			else if (soundFormat == SOUND_FORMAT.PCM16)
			{
				Marshal.Copy(frame as short[], 0, ptr, num);
				if (ptr2 != IntPtr.Zero)
				{
					Marshal.Copy(frame as short[], num, ptr2, length);
				}
			}
			else if (soundFormat == SOUND_FORMAT.PCMFLOAT)
			{
				Marshal.Copy(frame as float[], 0, ptr, num);
				if (ptr2 != IntPtr.Zero)
				{
					Marshal.Copy(frame as float[], num, ptr2, length);
				}
			}
			rESULT = sound.unlock(ptr, ptr2, len, len2);
			if (rESULT != RESULT.OK)
			{
				Error = "failed to unlock sound buffer: " + rESULT;
				logger.LogError(logPrefix + Error);
			}
		}
	}

	public override void Stop()
	{
		lock (frameQueue)
		{
			if (sound.handle == IntPtr.Zero)
			{
				UnityEngine.Debug.Log("Trying to stop sound with IntPtr.Zero handle");
				return;
			}
			base.Stop();
			sound.release();
			Error = null;
			sound.handle = IntPtr.Zero;
		}
	}
}
