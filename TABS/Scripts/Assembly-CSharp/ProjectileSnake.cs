using System;
using UnityEngine;

public class ProjectileSnake : MonoBehaviour, GameObjectPooling.IPoolable
{
	[SerializeField]
	private GameObject[] projectileSnakeObjects;

	public bool IsManagedByPool { get; set; }

	public Action ReleaseSelf { get; set; }

	public void Initialize()
	{
		GameObject[] array = projectileSnakeObjects;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(value: true);
		}
	}

	void GameObjectPooling.IPoolable.Reset()
	{
	}

	void GameObjectPooling.IPoolable.Release()
	{
	}

	public void Hide()
	{
		GameObject[] array = projectileSnakeObjects;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(value: false);
		}
	}
}
