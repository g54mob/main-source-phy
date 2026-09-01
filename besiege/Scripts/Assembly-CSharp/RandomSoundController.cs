using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class RandomSoundController : MonoBehaviour
{
	public AudioClip[] audioclips;

	public bool randomPitch;

	public AudioClip[] audioclips2;

	public bool randomPitch2 = true;

	public AudioClip[] audioclips3;

	public AudioSource audioSource;

	private int lastPLayed1;

	private int lastPLayed2;

	[Header("General Settings")]
	public bool usePlayOneShot = true;

	public float pitchRange = 0.1f;

	public bool randomVolume;

	public float minVol = 0.1f;

	public float maxVol = 1f;

	public bool playOnStart;

	public bool forcePlay;

	[Header("Timing")]
	public float delay;

	public bool useTimedLooping;

	public Vector2 loopInterval;

	private float loopTime;

	private float startPitch;

	private bool networkSounds;

	private NetworkBlock netBlock;

	private bool started;

	protected AudioMixerGroup mixer;

	protected AudioMixerGroup underwaterMixer;

	private bool assignedMixers;

	private bool SourceActive
	{
		get
		{
			return audioSource.gameObject != null && audioSource != null && audioSource.gameObject.activeInHierarchy;
		}
	}

	public void AssignMixers()
	{
		if (!assignedMixers)
		{
			mixer = audioSource.outputAudioMixerGroup;
			if (mixer == null)
			{
				AudioMixerGroup outputAudioMixerGroup = ReferenceMaster.GetMixer("Physics");
				audioSource.outputAudioMixerGroup = outputAudioMixerGroup;
				mixer = outputAudioMixerGroup;
			}
			underwaterMixer = ReferenceMaster.GetWaterMixerFrom(mixer);
			assignedMixers = true;
		}
	}

	public void SetMixer(bool underwater)
	{
		if (underwater)
		{
			audioSource.outputAudioMixerGroup = underwaterMixer;
		}
		else
		{
			audioSource.outputAudioMixerGroup = mixer;
		}
	}

	protected void Start()
	{
		if (started)
		{
			return;
		}
		started = true;
		if (audioSource == null)
		{
			audioSource = GetComponent<AudioSource>();
		}
		AssignMixers();
		startPitch = audioSource.pitch;
		if (StatMaster.isMP)
		{
			netBlock = GetComponentInParent<NetworkBlock>();
			if (netBlock != null)
			{
				if (netBlock.isBlock)
				{
					BlockBehaviour blockBehaviour = netBlock.blockBehaviour;
					networkSounds = blockBehaviour.SimPhysics && blockBehaviour.isSimulating;
				}
				else
				{
					networkSounds = StatMaster.isHosting && StatMaster.InGlobalPlayMode;
				}
			}
		}
		if (useTimedLooping)
		{
			loopTime = Random.Range(loopInterval.x, loopInterval.y);
		}
		if (playOnStart)
		{
			if (delay != 0f)
			{
				StartCoroutine(PlayAudio(delay));
			}
			else
			{
				Play();
			}
		}
	}

	private void Update()
	{
		if (useTimedLooping)
		{
			loopTime -= Time.deltaTime;
			if (loopTime <= 0f)
			{
				loopTime = Random.Range(loopInterval.x, loopInterval.y);
				Play();
			}
		}
	}

	public IEnumerator PlayAudio(float delay)
	{
		float cTime = 0f;
		while (cTime < delay)
		{
			cTime += Time.deltaTime;
			yield return null;
		}
		Stop();
		Play();
	}

	protected void SetPitch(float value)
	{
		audioSource.pitch = value;
	}

	public void Play(float vol, float submergedPerc)
	{
		if (audioclips.Length != 0 && SourceActive)
		{
			SetMixer(WaterController.Exist && submergedPerc > 0.9f);
			float num = (maxVol - minVol) * vol + minVol;
			if (!audioSource.isPlaying || !(audioSource.volume > num))
			{
				audioSource.volume = num;
				Play(false);
			}
		}
	}

	public void Play(bool setMixer = true)
	{
		if (!started)
		{
			Start();
		}
		if (audioclips.Length == 0 || !SourceActive)
		{
			return;
		}
		if (networkSounds)
		{
			netBlock.Event(NetworkEntity.EntityEvent.RSCPlay);
			if (!SourceActive)
			{
				return;
			}
		}
		if (setMixer)
		{
			SetMixer(WaterController.Exist && base.transform.position.y < WaterController.waterTransformHeight);
		}
		if (audioclips.Length < 2)
		{
			if (randomPitch)
			{
				SetPitch(startPitch + Random.Range(0f - pitchRange, pitchRange));
			}
			if (randomVolume)
			{
				audioSource.volume = Random.Range(minVol, maxVol);
			}
			if (!audioSource.isPlaying || forcePlay)
			{
				if (usePlayOneShot && audioclips.Length > 0)
				{
					audioSource.PlayOneShot(audioclips[0]);
				}
				else
				{
					audioSource.Play();
				}
			}
			return;
		}
		int num;
		for (num = Random.Range(0, audioclips.Length); num == lastPLayed1; num = Random.Range(0, audioclips.Length))
		{
		}
		if (randomPitch)
		{
			SetPitch(startPitch + Random.Range(0f - pitchRange, pitchRange));
		}
		if (randomVolume)
		{
			audioSource.volume = Random.Range(minVol, maxVol);
		}
		lastPLayed1 = num;
		if (!audioSource.isPlaying || forcePlay)
		{
			if (usePlayOneShot)
			{
				audioSource.PlayOneShot(audioclips[lastPLayed1]);
				return;
			}
			audioSource.clip = audioclips[lastPLayed1];
			audioSource.Play();
		}
	}

	public void Stop()
	{
		if (SourceActive && audioSource.isPlaying)
		{
			if (networkSounds)
			{
				netBlock.Event(NetworkEntity.EntityEvent.RSCStop);
			}
			audioSource.Stop();
		}
	}

	public void Play2(float volume, bool setMixer = true)
	{
		if (audioclips2.Length == 0 || !SourceActive)
		{
			return;
		}
		if (networkSounds)
		{
			netBlock.Event(NetworkEntity.EntityEvent.RSCPlay2, (byte)(volume * 255f));
			if (!SourceActive)
			{
				return;
			}
		}
		audioSource.volume = volume;
		if (setMixer)
		{
			SetMixer(WaterController.Exist && base.transform.position.y < WaterController.waterTransformHeight);
		}
		if (randomPitch2)
		{
			SetPitch(startPitch + Random.Range(0f - pitchRange, pitchRange));
		}
		if (audioclips2.Length == 1)
		{
			audioSource.PlayOneShot(audioclips2[0]);
			return;
		}
		int num = Random.Range(0, audioclips2.Length);
		for (int i = 0; i < audioclips2.Length; i++)
		{
			if (num != lastPLayed2)
			{
				break;
			}
			num = Random.Range(0, audioclips2.Length);
		}
		lastPLayed2 = num;
		audioSource.PlayOneShot(audioclips2[lastPLayed2]);
	}

	public void Play3(bool setMixer = true)
	{
		if (audioclips3.Length == 0 || !SourceActive)
		{
			return;
		}
		if (networkSounds)
		{
			netBlock.Event(NetworkEntity.EntityEvent.RSCPlay3);
			if (!SourceActive)
			{
				return;
			}
		}
		if (setMixer)
		{
			SetMixer(WaterController.Exist && base.transform.position.y < WaterController.waterTransformHeight);
		}
		audioSource.PlayOneShot(audioclips3[Random.Range(0, audioclips3.Length)]);
	}

	public void Play3(float volume, bool setMixer = true)
	{
		if (audioclips3.Length == 0 || !SourceActive)
		{
			return;
		}
		if (networkSounds)
		{
			netBlock.Event(NetworkEntity.EntityEvent.RSCPlay3);
			if (!SourceActive)
			{
				return;
			}
		}
		if (setMixer)
		{
			SetMixer(WaterController.Exist && base.transform.position.y < WaterController.waterTransformHeight);
		}
		audioSource.volume = volume;
		if (audioclips3.Length == 1)
		{
			audioSource.PlayOneShot(audioclips3[0]);
		}
		else
		{
			audioSource.PlayOneShot(audioclips3[Random.Range(0, audioclips3.Length)]);
		}
	}
}
