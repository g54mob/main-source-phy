using TFBGames;
using UnityEngine;

public class ProjectileSound : ProjectileHitEffect, IValidatable
{
	[SerializeField]
	private string soundRef = "";

	[SerializeField]
	private AudioPathData soundPathData;

	private SoundPlayer soundPlayer;

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
		soundPlayer = ServiceLocator.GetService<SoundPlayer>();
	}

	public override bool DoEffect(HitData hit)
	{
		if (soundRef != string.Empty)
		{
			soundPlayer.PlaySoundEffectNonAlloc(soundPathData, 1f, base.transform.position);
		}
		return false;
	}
}
