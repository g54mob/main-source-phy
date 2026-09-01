using System;
using UnityEngine;
using UnityEngine.Audio;

public class PlaySoundOnRoll : MonoBehaviour
{
	[Serializable]
	public class AudioSetting
	{
		public AudioSource audio;

		public AnimationCurve curve;

		public float maxVolume = 0.4f;

		[HideInInspector]
		public float fallOff;

		public void SetMixer(AudioMixerGroup mixer)
		{
			audio.outputAudioMixerGroup = mixer;
		}
	}

	public AudioSetting rolling;

	public AudioSetting scraping;

	public AudioSetting scrapedRoll;

	public AudioSetting rollingSand;

	public AudioSetting scrapingSand;

	public AudioSetting scrapedRollSand;

	protected AudioMixerGroup mixer;

	protected AudioMixerGroup underwaterMixer;

	public Collider collider;

	public float angularThreshold = 0.2f;

	public float velocityThreshold = 1f;

	public float angularMaxThreshold = 2f;

	public float velocityMaxThreshold = 20f;

	public BasicInfo info;

	public Rigidbody rB;

	private float lerpInDuration = 0.6f;

	private float lerpOutDuration = 2f;

	public float radius = 9.5f;

	protected bool isUnderwater;

	protected bool isGrounded = true;

	protected RaycastHit hit;

	public float Radius
	{
		get
		{
			return radius;
		}
	}

	protected bool IsGrounded(out RaycastHit hit)
	{
		Vector3 localScale = base.transform.localScale;
		float num = ((!StatMaster.isMP) ? radius : (radius * Mathf.Max(localScale.x, localScale.y, localScale.z)));
		return Physics.Raycast(base.transform.position, -Vector3.up, out hit, num + 0.1f);
	}

	public bool WasGrounded()
	{
		return isGrounded;
	}

	private void Awake()
	{
		mixer = rolling.audio.outputAudioMixerGroup;
		if (mixer == null)
		{
			mixer = ReferenceMaster.GetMixer("Physics");
		}
		if (info == null)
		{
			info = rB.GetComponent<BasicInfo>();
		}
		underwaterMixer = ReferenceMaster.GetWaterMixerFrom(mixer);
		SetMixers((!(info.submergedPercent < 0.5f)) ? underwaterMixer : mixer);
	}

	protected void SetMixers(AudioMixerGroup mixer)
	{
		rolling.SetMixer(mixer);
		scraping.SetMixer(mixer);
		scrapedRoll.SetMixer(mixer);
		scrapingSand.SetMixer(mixer);
		scrapedRollSand.SetMixer(mixer);
	}

	private void Update()
	{
		if (!StatMaster.levelSimulating || (StatMaster.isMP && StatMaster.isClient && !StatMaster.isLocalSim))
		{
			return;
		}
		float magnitude = rB.velocity.magnitude;
		float magnitude2 = rB.angularVelocity.magnitude;
		if (info.submergedPercent > 0.5f)
		{
			if (!isUnderwater)
			{
				isUnderwater = true;
				SetMixers(underwaterMixer);
			}
		}
		else if (isUnderwater)
		{
			isUnderwater = false;
			SetMixers(mixer);
		}
		isGrounded = IsGrounded(out hit);
		if (isGrounded)
		{
			bool flag = hit.collider.gameObject.CompareTag("Sand");
			if (magnitude > velocityThreshold)
			{
				if (magnitude2 > angularThreshold)
				{
					PlayAudioSources(magnitude, magnitude2, flag ? 3 : 0);
				}
				else
				{
					PlayAudioSources(magnitude, magnitude2, (!flag) ? 1 : 4);
				}
				return;
			}
			if (magnitude2 > angularThreshold)
			{
				PlayAudioSources(magnitude, magnitude2, (!flag) ? 2 : 5);
				return;
			}
		}
		PlayAudioSources(magnitude, magnitude2, -1);
	}

	protected void PlayAudioSources(float rbVelocity, float rbAngular, int source)
	{
		PlayAudioSource(rolling, Mathf.Clamp01((rbAngular - angularThreshold) / angularMaxThreshold), source == 0);
		PlayAudioSource(scraping, Mathf.Clamp01((rbVelocity - velocityThreshold) / velocityMaxThreshold), source == 1);
		PlayAudioSource(scrapedRoll, Mathf.Clamp01((rbAngular - angularThreshold) / angularMaxThreshold), source == 2);
		PlayAudioSource(rollingSand, Mathf.Clamp01((rbAngular - angularThreshold) / angularMaxThreshold), source == 3);
		PlayAudioSource(scrapingSand, Mathf.Clamp01((rbVelocity - velocityThreshold) / velocityMaxThreshold), source == 4);
		PlayAudioSource(scrapedRollSand, Mathf.Clamp01((rbAngular - angularThreshold) / angularMaxThreshold), source == 5);
	}

	protected void PlayAudioSource(AudioSetting setting, float lerp, bool play)
	{
		if (!(setting.audio == null))
		{
			if (play && !setting.audio.isPlaying)
			{
				setting.audio.Play();
			}
			float num = ((!play) ? (0f - lerpOutDuration) : lerpInDuration);
			if (!isGrounded)
			{
				num = -0.25f;
			}
			setting.fallOff += Time.deltaTime / num;
			setting.fallOff = Mathf.Clamp01(setting.fallOff);
			float time = Mathf.Lerp(0f, lerp, setting.fallOff);
			setting.audio.volume = setting.maxVolume * setting.curve.Evaluate(time);
		}
	}
}
