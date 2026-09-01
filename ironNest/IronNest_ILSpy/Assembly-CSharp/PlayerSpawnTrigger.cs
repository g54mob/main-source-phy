using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSpawnTrigger : MonoBehaviour
{
	public PlayerSpawnPoint target;

	public string spawnPointTag;

	public bool triggerOnEnable;

	public UnityEvent onTriggered;

	private void OnEnable()
	{
		if (triggerOnEnable)
		{
			Trigger();
		}
	}

	public void Trigger()
	{
		PlayerSpawnPoint playerSpawnPoint = ResolveTarget();
		if (playerSpawnPoint != null)
		{
			playerSpawnPoint.TeleportPlayer();
			onTriggered.Invoke();
			GameObject gameObject = base.gameObject;
			string text = gameObject.name;
			GameObject gameObject2 = playerSpawnPoint.gameObject;
			string text2 = gameObject2.name;
			string message = "[PlayerSpawnTrigger] '" + text + "' triggered spawn point '" + text2 + "'.";
			Debug.Log(message, this);
		}
		else
		{
			Debug.LogWarning("[PlayerSpawnTrigger] Could not resolve a PlayerSpawnPoint. Assign Target directly or set a valid Spawn Point Tag.", this);
		}
	}

	private PlayerSpawnPoint ResolveTarget()
	{
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Expected O, but got Unknown
		//IL_0083: Expected O, but got I4
		//IL_008c: Expected O, but got I4
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Expected O, but got Unknown
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Expected O, but got Unknown
		if (target == null)
		{
			if (!string.IsNullOrEmpty(spawnPointTag))
			{
				GameObject[] array = GameObject.FindGameObjectsWithTag(spawnPointTag);
				if (array != null)
				{
					object obj = array + 32;
					object obj2 = 0;
					object obj3 = 0;
					UnityEngine.Object obj4 = default(UnityEngine.Object);
					while (true)
					{
						if ((nint)obj3 < array.Length)
						{
							if (obj == null)
							{
								break;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
							if (obj4 == null)
							{
								obj2++;
								obj += 8;
								obj3 = obj2;
								continue;
							}
							return (PlayerSpawnPoint)obj4;
						}
						string message = "[PlayerSpawnTrigger] No PlayerSpawnPoint component found on any GameObject tagged '" + spawnPointTag + "'. Check the tag assignment.";
						Debug.LogWarning(message, this);
						return null;
					}
				}
				return (PlayerSpawnPoint)(object)new NullReferenceException();
			}
			return null;
		}
		return target;
	}

	public PlayerSpawnTrigger()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A8D1]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		spawnPointTag = "BedSpawnPoint";
		triggerOnEnable = true;
		base._002Ector();
	}
}
