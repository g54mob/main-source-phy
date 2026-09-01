using System;
using TFBGames;
using UnityEngine;

public class PlaySoundEffect : MonoBehaviour, IValidatable, GameObjectPooling.IPoolable
{
	[SerializeField]
	private string soundRef;

	[SerializeField]
	private AudioPathData soundPathData;

	public bool PlayOnAwake = true;

	private SoundPlayer soundPlayer;

	private bool init;

	public bool IsManagedByPool { get; set; }

	public Action ReleaseSelf { get; set; }

	public string SoundRef
	{
		get
		{
			return soundRef;
		}
		set
		{
			soundRef = value;
			AudioPathData.ValidateAndAssignPathData(soundRef, ref soundPathData, base.gameObject);
		}
	}

	public bool Validate()
	{
		return AudioPathData.ValidateAndAssignPathData(soundRef, ref soundPathData, base.gameObject);
	}

	private void Start()
	{
		if (!IsManagedByPool)
		{
			InitializeOnSpawn();
		}
	}

	public void Go()
	{
		Init();
		if (soundRef != string.Empty)
		{
			soundPlayer.PlaySoundEffectNonAlloc(soundPathData, 1f, base.transform.position);
		}
	}

	public void Initialize()
	{
		InitializeOnSpawn();
	}

	public void Reset()
	{
	}

	public void Release()
	{
	}

	private void InitializeOnSpawn()
	{
		Init();
		if (PlayOnAwake)
		{
			Go();
		}
	}

	private void Init()
	{
		if (!init)
		{
			soundPlayer = ServiceLocator.GetService<SoundPlayer>();
			init = true;
		}
	}
}
