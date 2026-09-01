using DarkTonic.MasterAudio;
using UnityEngine;

public class TransformFollower : MonoBehaviour
{
	[Tooltip("This is for diagnostic purposes only. Do not change or assign this field.")]
	public Transform RuntimeFollowingTransform;

	private GameObject _goToFollow;

	private Transform _trans;

	private GameObject _go;

	private string _soundType;

	private string _variationName;

	private bool _willFollowSource;

	private bool _isInsideTrigger;

	private bool _hasPlayedSound;

	private float _playVolume;

	private bool _positionAtClosestColliderPoint;

	private MasterAudio.AmbientSoundExitMode _exitMode;

	private float _exitFadeTime;

	private MasterAudio.AmbientSoundReEnterMode _reEnterMode;

	private float _reEnterFadeTime;

	private Vector3 _lastListenerPos = new Vector3(float.MinValue, float.MinValue, float.MinValue);

	private PlaySoundResult playingVariation;

	private PlaySoundResult fadingVariation;

	public GameObject GameObj
	{
		get
		{
			if (_go != null)
			{
				return _go;
			}
			_go = base.gameObject;
			return _go;
		}
	}

	public Transform Trans
	{
		get
		{
			if (_trans == null)
			{
				_trans = base.transform;
			}
			return _trans;
		}
	}

	private void Awake()
	{
		if (!(_lastListenerPos == Vector3.zero) && playingVariation == null)
		{
			_ = _positionAtClosestColliderPoint;
		}
	}

	private void OnDisable()
	{
		AmbientUtil.RemoveTransformFollower(this);
		PerformTriggerExit();
	}

	public void StartFollowing(Transform transToFollow, string soundType, string variationName, float volume, float trigRadius, bool willFollowSource, bool positionAtClosestColliderPoint, bool useTopCollider, bool useChildColliders, MasterAudio.AmbientSoundExitMode exitMode, float exitFadeTime, MasterAudio.AmbientSoundReEnterMode reEnterMode, float reEnterFadeTime)
	{
		RuntimeFollowingTransform = transToFollow;
		_goToFollow = transToFollow.gameObject;
		_soundType = soundType;
		_variationName = variationName;
		_playVolume = volume;
		_willFollowSource = willFollowSource;
		_exitMode = exitMode;
		_exitFadeTime = exitFadeTime;
		_reEnterMode = reEnterMode;
		_reEnterFadeTime = reEnterFadeTime;
		if (useTopCollider)
		{
			_ = (Object)null != (Object)null;
		}
		if (useChildColliders && transToFollow != null)
		{
			for (int i = 0; i < transToFollow.childCount; i++)
			{
			}
		}
		_lastListenerPos = MasterAudio.ListenerTrans.position;
		int num = 0;
		if (0 == 0 && num == 0 && positionAtClosestColliderPoint)
		{
			Debug.Log("Can't follow collider of '" + transToFollow.name + "' because it doesn't have any colliders.");
			return;
		}
		_positionAtClosestColliderPoint = positionAtClosestColliderPoint;
		if (_positionAtClosestColliderPoint)
		{
			RecalcClosestColliderPosition(forceRecalc: true);
			MasterAudio.QueueTransformFollowerForColliderPositionRecalc(this);
		}
	}

	private void StopFollowing()
	{
		RuntimeFollowingTransform = null;
		Object.Destroy(GameObj);
	}

	private void PlaySound()
	{
		bool flag = !string.IsNullOrEmpty(_variationName);
		bool flag2 = _positionAtClosestColliderPoint || _exitMode == MasterAudio.AmbientSoundExitMode.FadeSound;
		if (fadingVariation != null && fadingVariation.ActingVariation != null)
		{
			MasterAudio.AmbientSoundReEnterMode ambientSoundReEnterMode = _reEnterMode;
			if (!fadingVariation.ActingVariation.IsPlaying)
			{
				ambientSoundReEnterMode = MasterAudio.AmbientSoundReEnterMode.StopExistingSound;
			}
			switch (ambientSoundReEnterMode)
			{
			case MasterAudio.AmbientSoundReEnterMode.FadeInSameSound:
				fadingVariation.ActingVariation.FadeToVolume(_playVolume, _reEnterFadeTime);
				playingVariation = fadingVariation;
				fadingVariation = null;
				_hasPlayedSound = true;
				return;
			case MasterAudio.AmbientSoundReEnterMode.StopExistingSound:
				fadingVariation.ActingVariation.Stop();
				break;
			}
		}
		if (_willFollowSource)
		{
			if (flag2)
			{
				if (flag)
				{
					playingVariation = MasterAudio.PlaySound3DFollowTransform(_soundType, RuntimeFollowingTransform, _playVolume, 1f, 0f, _variationName);
				}
				else
				{
					playingVariation = MasterAudio.PlaySound3DFollowTransform(_soundType, RuntimeFollowingTransform, _playVolume);
				}
			}
			else if (flag)
			{
				MasterAudio.PlaySound3DFollowTransformAndForget(_soundType, RuntimeFollowingTransform, _playVolume, 1f, 0f, _variationName);
			}
			else
			{
				MasterAudio.PlaySound3DFollowTransformAndForget(_soundType, RuntimeFollowingTransform, _playVolume);
			}
		}
		else if (flag2)
		{
			if (flag)
			{
				playingVariation = MasterAudio.PlaySound3DAtTransform(_soundType, RuntimeFollowingTransform, _playVolume, 1f, 0f, _variationName);
			}
			else
			{
				playingVariation = MasterAudio.PlaySound3DAtTransform(_soundType, RuntimeFollowingTransform, _playVolume);
			}
		}
		else if (flag)
		{
			MasterAudio.PlaySound3DAtTransformAndForget(_soundType, RuntimeFollowingTransform, _playVolume, 1f, 0f, _variationName);
		}
		else
		{
			MasterAudio.PlaySound3DAtTransformAndForget(_soundType, RuntimeFollowingTransform, _playVolume);
		}
		fadingVariation = null;
		_hasPlayedSound = true;
	}

	public void ManualUpdate()
	{
		if (RuntimeFollowingTransform == null || !DTMonoHelper.IsActive(_goToFollow))
		{
			StopFollowing();
			return;
		}
		if (!_positionAtClosestColliderPoint)
		{
			Trans.position = RuntimeFollowingTransform.position;
		}
		if (_isInsideTrigger && !_hasPlayedSound)
		{
			PlaySound();
		}
	}

	public bool RecalcClosestColliderPosition(bool forceRecalc = false)
	{
		Vector3 position = MasterAudio.ListenerTrans.position;
		_ = _lastListenerPos != position;
		Vector3 position2 = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
		bool flag = false;
		int num = 0;
		if (0 <= 0 && num <= 0)
		{
			return false;
		}
		if (!flag)
		{
			return false;
		}
		Trans.position = position2;
		Trans.LookAt(MasterAudio.ListenerTrans);
		if (playingVariation != null && playingVariation.ActingVariation != null)
		{
			playingVariation.ActingVariation.transform.position = position2;
		}
		_lastListenerPos = position;
		return true;
	}

	private void PerformTriggerExit()
	{
		_isInsideTrigger = false;
		_hasPlayedSound = false;
		if (!(MasterAudio.GrabGroup(_soundType, logIfMissing: false) == null))
		{
			switch (_exitMode)
			{
			case MasterAudio.AmbientSoundExitMode.StopSound:
				MasterAudio.StopSoundGroupOfTransform(RuntimeFollowingTransform, _soundType);
				break;
			case MasterAudio.AmbientSoundExitMode.FadeSound:
				MasterAudio.FadeOutSoundGroupOfTransform(RuntimeFollowingTransform, _soundType, _exitFadeTime);
				break;
			}
			fadingVariation = playingVariation;
			playingVariation = null;
		}
	}
}
