using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[AddComponentMenu("UI/UI Button Sound")]
public class UIButtonSound : UIButtonComponent
{
	private AudioSource audioSource;

	protected override void Awake()
	{
		base.Awake();
		audioSource = GetComponent<AudioSource>();
	}

	protected override void OnButtonClicked()
	{
		audioSource.Play();
	}

	protected override void OnButtonMouseEnter()
	{
	}

	protected override void OnButtonMouseExit()
	{
	}
}
