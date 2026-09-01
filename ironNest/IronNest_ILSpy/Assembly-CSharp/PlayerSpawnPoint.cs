using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSpawnPoint : MonoBehaviour
{
	public enum SpawnTriggerMode
	{
		OnEnable,
		Manual
	}

	public SpawnTriggerMode triggerMode;

	public string playerTag;

	public bool applyYawRotation;

	public UnityEvent onSpawned;

	private void OnEnable()
	{
		if (triggerMode == SpawnTriggerMode.OnEnable)
		{
			TeleportPlayer();
		}
	}

	public void Trigger()
	{
		TeleportPlayer();
	}

	public void Respawn()
	{
		TeleportPlayer();
	}

	private unsafe void TeleportPlayer()
	{
		//IL_009a: Expected O, but got Ref
		//IL_0153: Expected O, but got Ref
		//IL_020d: Expected O, but got F4
		GameObject gameObject = GameObject.FindWithTag(playerTag);
		if (gameObject != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
			Object obj = default(Object);
			float x = default(float);
			if (!(obj == null))
			{
				((Collider)obj).enabled = false;
				Transform transform = gameObject.transform;
				Transform transform2 = base.transform;
				Vector3 position = transform2.position;
				transform.position = (Vector3)(&x);
				Transform playerTransform = gameObject.transform;
				ApplyYaw(playerTransform);
				((Collider)obj).enabled = true;
				x = position.x;
			}
			else
			{
				string text = gameObject.name;
				string message = "[PlayerSpawnPoint] '" + text + "' has no CharacterController. Falling back to raw transform move.";
				Debug.LogWarning(message, this);
				Transform transform3 = gameObject.transform;
				Transform transform4 = base.transform;
				Vector3 position2 = transform4.position;
				transform3.position = (Vector3)(&x);
				Transform playerTransform2 = gameObject.transform;
				ApplyYaw(playerTransform2);
				x = position2.x;
			}
			Transform transform5 = gameObject.transform;
			if (transform5.TryGetComponent<FirstPersonController>(out var component))
			{
				Transform transform6 = base.transform;
				Vector3 eulerAngles = transform6.eulerAngles;
				component.pitch = eulerAngles.x;
			}
			string arg = gameObject.name;
			Transform transform7 = base.transform;
			Vector3 position3 = transform7.position;
			object arg2 = (Vector3)x;
			string text2 = $"[PlayerSpawnPoint] '{arg}' teleported to {arg2} ";
			GameObject gameObject2 = base.gameObject;
			string text3 = gameObject2.name;
			string message2 = text2 + "by '" + text3 + "'.";
			Debug.Log(message2, this);
			onSpawned.Invoke();
		}
		else
		{
			string message3 = "[PlayerSpawnPoint] No GameObject found with tag '" + playerTag + "'. Make sure the player is loaded and the tag matches.";
			Debug.LogWarning(message3, this);
		}
	}

	private unsafe void ApplyYaw(Transform playerTransform)
	{
		//IL_0056: Expected O, but got Ref
		if (applyYawRotation)
		{
			Vector3 eulerAngles = playerTransform.eulerAngles;
			Transform transform = base.transform;
			Vector3 eulerAngles2 = transform.eulerAngles;
			float num = default(float);
			playerTransform.eulerAngles = (Vector3)(&num);
		}
	}

	private void ApplyPitch(Transform playerTransform)
	{
		if (playerTransform.TryGetComponent<FirstPersonController>(out var component))
		{
			Transform transform = base.transform;
			Vector3 eulerAngles = transform.eulerAngles;
			component.pitch = eulerAngles.x;
		}
	}

	public PlayerSpawnPoint()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A8CE]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		playerTag = "Player";
		applyYawRotation = true;
		base._002Ector();
	}
}
