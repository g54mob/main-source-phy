using System;
using TFBGames;
using UnityEngine;

public class CollisionSound : MonoBehaviour, IValidatable
{
	private bool hasPlayedSoundThisSwing;

	[SerializeField]
	private string soundEffectRef = "";

	[SerializeField]
	private AudioPathData soundEffectPathData;

	public float multiplier = 1f;

	public bool onlySoundOnRig;

	private MeleeWeapon meleeWeapon;

	private Rigidbody rig;

	private SoundPlayer m_soundPlayer;

	public string SoundEffectRef
	{
		get
		{
			return soundEffectRef;
		}
		set
		{
			soundEffectRef = value;
			AudioPathData.ValidateAndAssignPathData(soundEffectRef, ref soundEffectPathData, base.gameObject);
		}
	}

	public bool Validate()
	{
		return AudioPathData.ValidateAndAssignPathData(soundEffectRef, ref soundEffectPathData, base.gameObject);
	}

	private void Awake()
	{
		m_soundPlayer = ServiceLocator.GetService<SoundPlayer>();
	}

	private void Start()
	{
		rig = GetComponent<Rigidbody>();
		meleeWeapon = GetComponent<MeleeWeapon>();
		if ((bool)meleeWeapon)
		{
			MeleeWeapon obj = meleeWeapon;
			obj.swingAction = (Action)Delegate.Combine(obj.swingAction, new Action(Swing));
		}
	}

	public void Swing()
	{
		hasPlayedSoundThisSwing = false;
	}

	public void DoEffect(Transform hitTransform, Collision collision, float impact)
	{
		float num = impact;
		num *= 0.5f;
		if (!(num < 0.1f) && (!onlySoundOnRig || (bool)collision.rigidbody) && (!meleeWeapon || !hasPlayedSoundThisSwing))
		{
			m_soundPlayer.PlaySoundEffectNonAlloc(soundEffectPathData, num, base.transform.position, SoundEffectVariations.GetMaterialType(collision.gameObject, collision.rigidbody));
		}
	}
}
