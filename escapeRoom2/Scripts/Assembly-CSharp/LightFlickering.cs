using System.Collections;
using LightFlickeringSpace;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[AddComponentMenu("Light Flickering/Light Flickering")]
public class LightFlickering : MonoBehaviour
{
	[Tooltip("If enabled, the Flickering will start automatically on start.")]
	public bool onStart = true;

	[Tooltip("The target light source you want to flicker.")]
	public Light lightSource;

	[Tooltip("Use the fade effect where the light won't suddenly turn off but rather get dimmed gradually until the Alpha is 0.")]
	public bool fadeEffect;

	[Tooltip("The time of the fade effect.")]
	[Range(0f, 1f)]
	public float fadeTime = 0.2f;

	[Tooltip("the value to fade the light to. The lower the number, the weaker the light will be.")]
	[Range(0f, 1f)]
	public float fadeTo = 0.5f;

	[Tooltip("If enabled, Flickering timings will be randomized, easing the job on you. The randomization will be based on minimum and maximum time values set below.")]
	public bool randomizeFlickerings = true;

	[Min(0f)]
	public float minRandomizeTime = 0.08f;

	[Min(0f)]
	public float maxRandomizeTime = 0.3f;

	[Tooltip("Set the Flickering amount and timings manually.")]
	public float[] flickerings;

	[Tooltip("If true, the Flickering will loop to the beginning of the list when finished. If false, when the Flickering finishes the entire list. No more Flickering will occur.")]
	public bool loop = true;

	[Tooltip("Set the light color of the flickers.")]
	public Lights[] lightings;

	[Tooltip("Play a buzz sound or whatever you like when the the light is on.")]
	public bool playAudio;

	public AudioClip buzzAudio;

	[Tooltip("Randomize the buzz audio pitch on each flicker.")]
	public bool randomizeAudioPitch;

	[Min(0f)]
	[Tooltip("The audio pitch will randomize between the two values set here. For a constant pitch, set the two values to the same number.")]
	public Vector2 pitchRandomizer = new Vector2(0.9f, 1f);

	[Tooltip("Do you want to change the material of the game object emitting the light during flicker? If so, then enable this.")]
	public bool changeMaterial;

	[Tooltip("The Mesh Renderer that of the game object that you want to change it's material. For example: the bulb glass.")]
	public MeshRenderer bulbObject;

	[Tooltip("The material you want to change to. For example: a darker/dimmed glass material for teh bulb.")]
	public Material newMaterial;

	private int index;

	private int arrayLength;

	private int randomLightingsIndex;

	private int nextIndex;

	private bool triggered;

	private bool fadeOutColor;

	private bool fadeInColor;

	private bool addedCustomMat;

	private float setTime;

	private float t;

	private Material defaultMaterial;

	private AudioSource usedAudioSource;

	private Color defaultColor;

	private Color lastUsedColor;

	private void Start()
	{
		if (lightSource == null)
		{
			Debug.LogWarning("You need to set the Light Source in the inspector");
			return;
		}
		usedAudioSource = GetComponent<AudioSource>();
		usedAudioSource.loop = true;
		if (onStart)
		{
			Flicker();
		}
	}

	private void Reset()
	{
		usedAudioSource = GetComponent<AudioSource>();
		usedAudioSource.spatialBlend = 1f;
	}

	private void OnDisable()
	{
		StopFlickering();
	}

	private void OnEnable()
	{
		if (onStart)
		{
			Flicker();
		}
	}

	private void Update()
	{
		if (!triggered)
		{
			return;
		}
		if (fadeInColor)
		{
			Color nextLightingColor = GetNextLightingColor();
			t += Time.deltaTime / fadeTime;
			lightSource.color = Color.Lerp(lastUsedColor, nextLightingColor, t);
			if (lightSource.color.Equals(nextLightingColor))
			{
				fadeInColor = false;
				t = 0f;
				defaultColor = lightSource.color;
				OpenLightProperties();
			}
		}
		if (fadeOutColor)
		{
			Color color = new Color(fadeTo, fadeTo, fadeTo, 1f) * lastUsedColor;
			t += Time.deltaTime / fadeTime;
			lightSource.color = Color.Lerp(lastUsedColor, color, t);
			if (lightSource.color.Equals(color))
			{
				fadeOutColor = false;
				t = 0f;
				CloseLightProperties();
			}
		}
	}

	public void Flicker()
	{
		if (!randomizeFlickerings && flickerings.Length == 0)
		{
			Debug.LogWarning("No flickering added in the array.");
		}
		else
		{
			if (triggered)
			{
				return;
			}
			triggered = true;
			index = 0;
			arrayLength = flickerings.Length;
			if (!fadeInColor && !fadeOutColor)
			{
				if (changeMaterial && bulbObject != null && lightSource.enabled)
				{
					defaultMaterial = bulbObject.material;
				}
				defaultColor = lightSource.color;
			}
			StartCoroutine(OpenLight());
		}
	}

	public void StopFlickering()
	{
		StopAllCoroutines();
		index = 0;
		triggered = false;
	}

	private IEnumerator OpenLight()
	{
		addedCustomMat = false;
		float seconds;
		if (randomizeFlickerings)
		{
			seconds = (setTime = Random.Range(minRandomizeTime, maxRandomizeTime));
			loop = true;
			if (lightings.Length != 0)
			{
				if (lightings.Length < index + 1)
				{
					index = 0;
				}
				if (!fadeEffect)
				{
					lightSource.color = lightings[index].lightColor;
				}
				defaultColor = lightings[index].lightColor;
				if (bulbObject != null && lightings[index].bulbMaterial != null)
				{
					addedCustomMat = true;
				}
			}
		}
		else
		{
			seconds = flickerings[index];
			if (lightings.Length >= index + 1)
			{
				if (!fadeEffect)
				{
					lightSource.color = lightings[index].lightColor;
				}
				defaultColor = lightings[index].lightColor;
				if (bulbObject != null && lightings[index].bulbMaterial != null)
				{
					addedCustomMat = true;
				}
			}
			setTime = seconds;
		}
		yield return new WaitForSeconds(seconds);
		if (fadeEffect)
		{
			lastUsedColor = lightSource.color;
			fadeInColor = true;
			OpenLightProperties(dontCall: true);
		}
		else
		{
			lightSource.enabled = true;
			OpenLightProperties();
		}
	}

	private IEnumerator FlickerTimer()
	{
		yield return new WaitForSeconds(setTime);
		if (!fadeEffect)
		{
			lightSource.enabled = false;
			CloseLightProperties();
		}
		else
		{
			lastUsedColor = lightSource.color;
			fadeOutColor = true;
		}
	}

	private void OpenLightProperties(bool dontCall = false)
	{
		if (changeMaterial && bulbObject != null && !addedCustomMat)
		{
			bulbObject.material = defaultMaterial;
		}
		if (addedCustomMat)
		{
			if (fadeEffect)
			{
				if (index + 1 <= lightings.Length - 1)
				{
					bulbObject.material = lightings[index + 1].bulbMaterial;
				}
				else
				{
					bulbObject.material = lightings[0].bulbMaterial;
				}
			}
			else
			{
				bulbObject.material = lightings[index].bulbMaterial;
			}
		}
		PlayAudio();
		lightSource.enabled = true;
		if (!dontCall)
		{
			StartCoroutine(FlickerTimer());
		}
	}

	private void CloseLightProperties()
	{
		StopAudio();
		if (changeMaterial && bulbObject != null && newMaterial != null)
		{
			bulbObject.material = newMaterial;
		}
		if (index + 1 < arrayLength)
		{
			index++;
			StartCoroutine(OpenLight());
		}
		else if (randomizeFlickerings)
		{
			index++;
			StartCoroutine(OpenLight());
		}
		else if (loop)
		{
			index = 0;
			StartCoroutine(OpenLight());
		}
	}

	private void PlayAudio()
	{
		if (!playAudio)
		{
			return;
		}
		if (buzzAudio == null)
		{
			Debug.LogWarning("There is no AudioClip added in the Buzz Audio property.");
		}
		else if (!(usedAudioSource == null))
		{
			usedAudioSource.Stop();
			if (randomizeAudioPitch)
			{
				usedAudioSource.pitch = Random.Range(pitchRandomizer.x, pitchRandomizer.y);
			}
			usedAudioSource.clip = buzzAudio;
			usedAudioSource.Play();
		}
	}

	private void StopAudio()
	{
		if (playAudio && buzzAudio == null)
		{
			Debug.LogWarning("There is no AudioClip added in the Buzz Audio property.");
		}
		else if (!(usedAudioSource == null))
		{
			usedAudioSource.Stop();
		}
	}

	private Color GetNextLightingColor()
	{
		if (lightings.Length != 0)
		{
			if (index + 1 <= lightings.Length - 1)
			{
				return lightings[index + 1].lightColor;
			}
			return lightings[0].lightColor;
		}
		return defaultColor;
	}
}
