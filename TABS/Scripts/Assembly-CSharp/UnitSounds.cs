using TFBGames;
using UnityEngine;
using UnityEngine.Serialization;

public class UnitSounds : MonoBehaviour
{
	[FormerlySerializedAs("pitch")]
	public float Pitch;

	public AudioPathData VocalPathData;

	public AudioPathData FootPathData;

	public AudioPathData DeathPathData;

	private SoundPlayer m_soundPlayer;

	private void Awake()
	{
		m_soundPlayer = ServiceLocator.GetService<SoundPlayer>();
	}

	public void PlayFootSound(Vector3 collisionPoint, SoundEffectVariations.MaterialType materialType = SoundEffectVariations.MaterialType.Default)
	{
		if (!(FootPathData.Effect == string.Empty))
		{
			m_soundPlayer.PlaySoundEffectNonAlloc(FootPathData, 1f, collisionPoint, materialType);
		}
	}

	public void PlayVocalSound(int vocalType, float str, Vector3 pos)
	{
		if (VocalPathData != null && !(VocalPathData.Effect == string.Empty) && !(Random.value > str))
		{
			m_soundPlayer.PlaySoundEffectNonAlloc(VocalPathData, 1f, pos, SoundEffectVariations.MaterialType.Default, null, Pitch);
		}
	}

	public void PlayDeathSound(float str, Vector3 pos)
	{
		if (DeathPathData != null && !(DeathPathData.Effect == string.Empty) && !(Random.value > str))
		{
			m_soundPlayer.PlaySoundEffectNonAlloc(DeathPathData, 1f, pos, SoundEffectVariations.MaterialType.Default, null, Pitch);
		}
	}
}
