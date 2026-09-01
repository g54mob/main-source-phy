using System.Collections.Generic;
using UnityEngine;

public class CatMovementManager : MonoBehaviour
{
	private static CatMovementManager _instance;

	[SerializeField]
	private Transform _playerTransform;

	[SerializeField]
	private List<CatFloor> _floors;

	[SerializeField]
	private List<CatActivity> _activities;

	public bool EnabledCatFollow;

	public static CatMovementManager Instance => null;

	public Transform PlayerTransform => null;

	public List<CatFloor> Floors => null;

	public List<CatActivity> Activities => null;

	private void Awake()
	{
	}
}
