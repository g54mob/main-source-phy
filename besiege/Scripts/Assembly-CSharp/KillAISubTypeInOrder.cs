using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAISubTypeInOrder : MonoBehaviour
{
	[Serializable]
	public class DisplayList
	{
		public List<EnemyAISimple> group = new List<EnemyAISimple>();
	}

	public int[] order = new int[0];

	public List<DisplayList> ais = new List<DisplayList>();

	public float minDeathInterval = 0.2f;

	public float maxDeathInterval = 2f;

	public float stingerInterval = 0.35f;

	public AudioSource sfx;

	public AudioClip[] stingers;

	[SerializeField]
	internal SpawnAchievementTrophy spawner;

	private float lastDeath;

	private int progress;

	private bool run = true;

	private void Start()
	{
		if (!StatMaster.levelSimulating)
		{
			return;
		}
		foreach (DisplayList ai in ais)
		{
			foreach (EnemyAISimple item in ai.group)
			{
				item.OnDeath = (Action<EnemyAISimple>)Delegate.Combine(item.OnDeath, new Action<EnemyAISimple>(Death));
			}
		}
	}

	private void Death(EnemyAISimple ai)
	{
		if (!run)
		{
			return;
		}
		if (Time.time - lastDeath < maxDeathInterval || progress == 0)
		{
			if (ais[order[progress]].group.Contains(ai) && Time.time - lastDeath > minDeathInterval)
			{
				progress++;
			}
			else
			{
				int num = Mathf.Max(progress - 1, 0);
				if (!ais[order[num]].group.Contains(ai))
				{
					progress = 0;
				}
			}
		}
		else
		{
			progress = 0;
		}
		lastDeath = Time.time;
		if (progress == ais.Count)
		{
			StartCoroutine(Success());
		}
	}

	private IEnumerator Success()
	{
		run = false;
		yield return new WaitForSeconds(stingerInterval);
		for (int i = 0; i < stingers.Length; i++)
		{
			sfx.PlayOneShot(stingers[i]);
			yield return new WaitForSeconds(stingerInterval);
		}
		yield return new WaitForSeconds(1f);
		spawner.SpawnTrophy(base.transform.position);
	}
}
