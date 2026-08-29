using System;
using System.Collections.Generic;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public static class PineFmod
{
	public static bool enabled = true;

	public static void disableFmod()
	{
		enabled = false;
		destroyAll<StudioEventEmitter>();
		destroyAll<StudioListener>();
		destroyAll<StudioBankLoader>();
		static void destroyAll<T>() where T : MonoBehaviour
		{
			T[] array = UnityEngine.Object.FindObjectsOfType<T>(includeInactive: true);
			for (int i = 0; i < array.Length; i++)
			{
				UnityEngine.Object.DestroyImmediate(array[i]);
			}
		}
	}

	public static StudioEventEmitter addSoundEmitter(GameObject gameObject, EventReference sound)
	{
		StudioEventEmitter studioEventEmitter = gameObject.AddComponent<StudioEventEmitter>();
		studioEventEmitter.EventReference = sound;
		studioEventEmitter.StopEvent = EmitterGameEvent.ObjectDestroy;
		set3DAttributes(studioEventEmitter.EventInstance, to3DAttributes(gameObject.transform));
		return studioEventEmitter;
	}

	public static StudioEventEmitter addSoundEmitter(GameObject gameObject)
	{
		StudioEventEmitter studioEventEmitter = gameObject.AddComponent<StudioEventEmitter>();
		studioEventEmitter.StopEvent = EmitterGameEvent.ObjectDestroy;
		set3DAttributes(studioEventEmitter.EventInstance, to3DAttributes(gameObject.transform));
		return studioEventEmitter;
	}

	public static void playOneShotSoundAttached(EventReference sound, GameObject go)
	{
		if (!enabled)
		{
			return;
		}
		try
		{
			if (!sound.IsNull && RuntimeManager.StudioSystem.getEventByID(sound.Guid, out var _event) == RESULT.OK)
			{
				_event.isOneshot(out var oneshot);
				if (!oneshot)
				{
					GUID guid = sound.Guid;
					UnityEngine.Debug.LogWarning("Playing LOOP sound as oneshot " + guid.ToString());
				}
				else if (go == null)
				{
					RuntimeManager.PlayOneShot(sound);
				}
				else
				{
					RuntimeManager.PlayOneShotAttached(sound, go);
				}
			}
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
		}
	}

	public static void playOneShotSound(EventReference sound)
	{
		playOneShotSoundAttached(sound, null);
	}

	public static void playOneShotSoundAttached(string sound, GameObject go)
	{
		if (!enabled)
		{
			return;
		}
		try
		{
			if (RuntimeManager.StudioSystem.getEvent(sound, out var _event) == RESULT.OK)
			{
				_event.isOneshot(out var oneshot);
				if (!oneshot)
				{
					UnityEngine.Debug.LogWarning("Playing LOOP sound as oneshot " + sound);
				}
				else if (go == null)
				{
					RuntimeManager.PlayOneShot(sound);
				}
				else
				{
					RuntimeManager.PlayOneShotAttached(sound, go);
				}
			}
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
		}
	}

	public static void playOneShotSound(string sound)
	{
		playOneShotSoundAttached(sound, null);
	}

	public static void playSoundOnEmitter(StudioEventEmitter emitter, EventReference sound)
	{
		if (!enabled)
		{
			return;
		}
		try
		{
			emitter.Stop();
			emitter.EventReference = sound;
			emitter.Lookup();
			emitter.Play();
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
		}
	}

	public static Bus getBus(string bus)
	{
		if (!enabled)
		{
			return default(Bus);
		}
		try
		{
			return RuntimeManager.GetBus(bus);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			return default(Bus);
		}
	}

	public static RESULT stopAllEvents(Bus bus, FMOD.Studio.STOP_MODE mode)
	{
		if (!enabled)
		{
			return RESULT.ERR_PLUGIN;
		}
		try
		{
			return bus.stopAllEvents(mode);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			return RESULT.ERR_PLUGIN;
		}
	}

	public static RESULT getChannelGroup(Bus bus, out ChannelGroup channelGroup)
	{
		if (!enabled)
		{
			channelGroup = default(ChannelGroup);
			return RESULT.ERR_PLUGIN;
		}
		try
		{
			return bus.getChannelGroup(out channelGroup);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			channelGroup = default(ChannelGroup);
			return RESULT.ERR_PLUGIN;
		}
	}

	public static RESULT setMode(FMOD.Sound sound, MODE mode)
	{
		if (!enabled)
		{
			return RESULT.ERR_PLUGIN;
		}
		try
		{
			return sound.setMode(mode);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			return RESULT.ERR_PLUGIN;
		}
	}

	public static RESULT setMode(Channel channel, MODE mode)
	{
		if (!enabled)
		{
			return RESULT.ERR_PLUGIN;
		}
		try
		{
			return channel.setMode(mode);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			return RESULT.ERR_PLUGIN;
		}
	}

	public static RESULT set3DSpread(Channel channel, float angle)
	{
		if (!enabled)
		{
			return RESULT.ERR_PLUGIN;
		}
		try
		{
			return channel.set3DSpread(angle);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			return RESULT.ERR_PLUGIN;
		}
	}

	public static RESULT set3DMinMaxDistance(Channel channel, float mindistance, float maxdistance)
	{
		if (!enabled)
		{
			return RESULT.ERR_PLUGIN;
		}
		try
		{
			return channel.set3DMinMaxDistance(mindistance, maxdistance);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			return RESULT.ERR_PLUGIN;
		}
	}

	public static RESULT setLoopCount(FMOD.Sound sound, int loopcount)
	{
		if (!enabled)
		{
			return RESULT.ERR_PLUGIN;
		}
		try
		{
			return sound.setLoopCount(loopcount);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			return RESULT.ERR_PLUGIN;
		}
	}

	public static RESULT playSound(FMOD.System system, FMOD.Sound sound, ChannelGroup channelgroup, bool paused, out Channel channel)
	{
		if (!enabled)
		{
			channel = default(Channel);
			return RESULT.ERR_PLUGIN;
		}
		try
		{
			return system.playSound(sound, channelgroup, paused, out channel);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			channel = default(Channel);
			return RESULT.ERR_PLUGIN;
		}
	}

	public static RESULT setVolume(Channel channel, float volume)
	{
		if (!enabled)
		{
			return RESULT.ERR_PLUGIN;
		}
		try
		{
			return channel.setVolume(volume);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			return RESULT.ERR_PLUGIN;
		}
	}

	public static RESULT setVolume(EventInstance eventInstance, float volume)
	{
		if (!enabled)
		{
			return RESULT.ERR_PLUGIN;
		}
		try
		{
			return eventInstance.setVolume(volume);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			return RESULT.ERR_PLUGIN;
		}
	}

	public static RESULT setVolume(Bus bus, float volume)
	{
		if (!enabled)
		{
			return RESULT.ERR_PLUGIN;
		}
		try
		{
			return bus.setVolume(volume);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			return RESULT.ERR_PLUGIN;
		}
	}

	public static RESULT lockChannelGroup(Bus bus)
	{
		if (!enabled)
		{
			return RESULT.ERR_PLUGIN;
		}
		try
		{
			return bus.lockChannelGroup();
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			return RESULT.ERR_PLUGIN;
		}
	}

	public static RESULT createInstance(EventDescription eventDescription, out EventInstance instance)
	{
		if (!enabled)
		{
			instance = default(EventInstance);
			return RESULT.ERR_PLUGIN;
		}
		try
		{
			return eventDescription.createInstance(out instance);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			instance = default(EventInstance);
			return RESULT.ERR_PLUGIN;
		}
	}

	public static RESULT getPlaybackState(EventInstance instance, out PLAYBACK_STATE state)
	{
		if (!enabled)
		{
			state = PLAYBACK_STATE.PLAYING;
			return RESULT.ERR_PLUGIN;
		}
		try
		{
			return instance.getPlaybackState(out state);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			state = PLAYBACK_STATE.PLAYING;
			return RESULT.ERR_PLUGIN;
		}
	}

	public static RESULT start(EventInstance instance)
	{
		if (!enabled)
		{
			return RESULT.ERR_PLUGIN;
		}
		try
		{
			return instance.start();
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			return RESULT.ERR_PLUGIN;
		}
	}

	public static RESULT release(EventInstance instance)
	{
		if (!enabled)
		{
			return RESULT.ERR_PLUGIN;
		}
		try
		{
			return instance.release();
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			return RESULT.ERR_PLUGIN;
		}
	}

	public static RESULT release(FMOD.Sound sound)
	{
		if (!enabled)
		{
			return RESULT.ERR_PLUGIN;
		}
		try
		{
			return sound.release();
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			return RESULT.ERR_PLUGIN;
		}
	}

	public static RESULT stop(EventInstance instance, FMOD.Studio.STOP_MODE mode)
	{
		if (!enabled)
		{
			return RESULT.ERR_PLUGIN;
		}
		try
		{
			return instance.stop(mode);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			return RESULT.ERR_PLUGIN;
		}
	}

	public static void clearHandle(EventInstance instance)
	{
		if (!enabled)
		{
			return;
		}
		try
		{
			instance.clearHandle();
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
		}
	}

	public static bool hasHandle(EventInstance instance)
	{
		if (!enabled)
		{
			return false;
		}
		try
		{
			return instance.hasHandle();
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			return false;
		}
	}

	public static bool hasHandle(Channel channel)
	{
		if (!enabled)
		{
			return false;
		}
		try
		{
			return channel.hasHandle();
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			return false;
		}
	}

	public static bool hasHandle(FMOD.Sound sound)
	{
		if (!enabled)
		{
			return false;
		}
		try
		{
			return sound.hasHandle();
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			return false;
		}
	}

	public static bool isValid(EventInstance instance)
	{
		if (!enabled)
		{
			return false;
		}
		try
		{
			return instance.isValid();
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			return false;
		}
	}

	public static RESULT isPlaying(Channel channel, out bool playing)
	{
		if (!enabled)
		{
			playing = false;
			return RESULT.ERR_PLUGIN;
		}
		try
		{
			return channel.isPlaying(out playing);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			playing = false;
			return RESULT.ERR_PLUGIN;
		}
	}

	public static RESULT getLoopCount(Channel channel, out int loopcount)
	{
		if (!enabled)
		{
			loopcount = 0;
			return RESULT.ERR_PLUGIN;
		}
		try
		{
			return channel.getLoopCount(out loopcount);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			loopcount = 0;
			return RESULT.ERR_PLUGIN;
		}
	}

	public static RESULT stop(Channel channel)
	{
		if (!enabled)
		{
			return RESULT.ERR_PLUGIN;
		}
		try
		{
			return channel.stop();
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			return RESULT.ERR_PLUGIN;
		}
	}

	public static RESULT get3DAttributes(Channel channel, out VECTOR pos, out VECTOR vel)
	{
		if (!enabled)
		{
			pos = default(VECTOR);
			vel = default(VECTOR);
			return RESULT.ERR_PLUGIN;
		}
		try
		{
			return channel.get3DAttributes(out pos, out vel);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			pos = default(VECTOR);
			vel = default(VECTOR);
			return RESULT.ERR_PLUGIN;
		}
	}

	public static RESULT set3DAttributes(Channel channel, ref VECTOR pos, ref VECTOR vel)
	{
		if (!enabled)
		{
			return RESULT.ERR_PLUGIN;
		}
		try
		{
			return channel.set3DAttributes(ref pos, ref vel);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			pos = default(VECTOR);
			vel = default(VECTOR);
			return RESULT.ERR_PLUGIN;
		}
	}

	public static EventInstance createInstance(string path)
	{
		if (!enabled)
		{
			return default(EventInstance);
		}
		try
		{
			return RuntimeManager.CreateInstance(path);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			return default(EventInstance);
		}
	}

	public static EventInstance createInstance(GUID guid)
	{
		if (!enabled)
		{
			return default(EventInstance);
		}
		try
		{
			return RuntimeManager.CreateInstance(guid);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			return default(EventInstance);
		}
	}

	public static RESULT set3DAttributes(EventInstance eventInstance, ATTRIBUTES_3D attributes)
	{
		if (!enabled)
		{
			return RESULT.ERR_PLUGIN;
		}
		try
		{
			return eventInstance.set3DAttributes(attributes);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			return RESULT.ERR_PLUGIN;
		}
	}

	public static ATTRIBUTES_3D to3DAttributes(Transform transform)
	{
		if (!enabled)
		{
			return default(ATTRIBUTES_3D);
		}
		try
		{
			return transform.To3DAttributes();
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			return default(ATTRIBUTES_3D);
		}
	}

	public static void attachInstanceToGameObject(EventInstance instance, Transform transform, Rigidbody rigidBody)
	{
		if (!enabled)
		{
			return;
		}
		try
		{
			RuntimeManager.AttachInstanceToGameObject(instance, transform, rigidBody);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
		}
	}

	public static void playOrContinue(StudioEventEmitter emitter)
	{
		if (emitter == null || !enabled)
		{
			return;
		}
		try
		{
			EventInstance eventInstance = emitter.EventInstance;
			if (eventInstance.isValid() && eventInstance.getPaused(out var _) == RESULT.OK)
			{
				eventInstance.setPaused(paused: false);
			}
			else if (!isPlaying(emitter))
			{
				play(emitter);
			}
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
		}
	}

	public static void play(StudioEventEmitter emitter)
	{
		if (emitter == null || !enabled)
		{
			return;
		}
		try
		{
			emitter.Play();
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
		}
	}

	public static void pause(StudioEventEmitter emitter)
	{
		if (emitter == null || !enabled)
		{
			return;
		}
		try
		{
			EventInstance eventInstance = emitter.EventInstance;
			if (eventInstance.isValid())
			{
				eventInstance.setPaused(paused: true);
			}
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
		}
	}

	public static bool isPlaying(StudioEventEmitter emitter)
	{
		if (emitter == null)
		{
			return false;
		}
		if (!enabled)
		{
			return false;
		}
		try
		{
			return emitter.IsPlaying();
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			return false;
		}
	}

	public static void stop(StudioEventEmitter emitter)
	{
		if (emitter == null || !enabled)
		{
			return;
		}
		try
		{
			emitter.Stop();
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
		}
	}

	public static void SetParameter(StudioEventEmitter emitter, string name, float value, bool ignoreseekspeed = false)
	{
		if (!enabled)
		{
			return;
		}
		try
		{
			emitter.SetParameter(name, value, ignoreseekspeed);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
		}
	}

	public static void killSoundsInBus(Bus bus)
	{
		if (!enabled)
		{
			return;
		}
		try
		{
			bus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			if (bus.getChannelGroup(out var group) == RESULT.OK)
			{
				killSoundsInChannelGroup(group);
			}
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
		}
	}

	public static void killSoundsInChannelGroup(ChannelGroup channelGroup)
	{
		if (!enabled)
		{
			return;
		}
		try
		{
			channelGroup.getNumChannels(out var numchannels);
			List<Channel> list = new List<Channel>(numchannels);
			for (int i = 0; i < numchannels; i++)
			{
				if (channelGroup.getChannel(i, out var channel) == RESULT.OK)
				{
					list.Add(channel);
				}
			}
			for (int j = 0; j < list.Count; j++)
			{
				list[j].stop();
			}
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
		}
	}

	public static Bank getAudioPresetsBank()
	{
		if (!enabled)
		{
			return default(Bank);
		}
		try
		{
			RuntimeManager.StudioSystem.getBankList(out var array);
			return Array.Find(array, delegate(Bank x)
			{
				x.getPath(out var path);
				return path.Contains("RoomEditor");
			});
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
			return default(Bank);
		}
	}

	public static void flushCommands()
	{
		if (!enabled)
		{
			return;
		}
		try
		{
			RuntimeManager.StudioSystem.flushCommands();
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
		}
	}

	public static void destroy()
	{
		if (!enabled)
		{
			return;
		}
		try
		{
			RuntimeManager.StudioSystem.release();
			RuntimeManager.StudioSystem.clearHandle();
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log(ex.Message);
			UnityEngine.Debug.Log(ex.StackTrace);
		}
	}
}
