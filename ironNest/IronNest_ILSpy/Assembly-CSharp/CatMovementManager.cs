using System.Collections.Generic;
using UnityEngine;

public class CatMovementManager : MonoBehaviour
{
	private static CatMovementManager _instance;

	private Transform _playerTransform;

	private List<CatFloor> _floors;

	private List<CatActivity> _activities;

	public bool EnabledCatFollow;

	public static CatMovementManager Instance
	{
		get
		{
			if (_instance == null)
			{
				CatMovementManager instance = Object.FindObjectOfType<CatMovementManager>();
				_instance = instance;
			}
			return _instance;
		}
	}

	public Transform PlayerTransform => _playerTransform;

	public List<CatFloor> Floors => _floors;

	public List<CatActivity> Activities => _activities;

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			GameObject obj = base.gameObject;
			Object.Destroy(obj);
		}
		else
		{
			_instance = this;
		}
	}

	public CatMovementManager()
	{
		List<CatFloor> floors = new List<CatFloor>();
		_floors = floors;
		_activities = new List<CatActivity>();
		base._002Ector();
	}
}
