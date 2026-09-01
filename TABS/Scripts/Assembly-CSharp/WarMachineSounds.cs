using UnityEngine;

public class WarMachineSounds : MonoBehaviour
{
	public float velocityFactor = 0.1f;

	public float velocityPitchFactor = 0.1f;

	private DataHandler data;

	private AudioSource audio;

	private float startVolume;

	private void Start()
	{
		data = base.transform.root.GetComponentInChildren<DataHandler>();
		audio = GetComponent<AudioSource>();
		startVolume = audio.volume;
	}

	private void Update()
	{
		if (CampaignPlayerDataHolder.CurrentGameModeState == GameModeState.Menu)
		{
			audio.volume = 0f;
			return;
		}
		base.transform.position = data.mainRig.position;
		audio.volume = Mathf.Lerp(audio.volume, data.mainRig.velocity.magnitude * velocityFactor * ((data.isGrounded && !data.Dead) ? 1f : 0f), Time.deltaTime * 5f);
		audio.pitch = Mathf.Clamp(0.5f + data.mainRig.velocity.magnitude * velocityPitchFactor, 0f, 2f);
	}
}
