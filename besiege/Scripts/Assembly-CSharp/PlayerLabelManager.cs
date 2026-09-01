using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("UI/Multiplayer/Player Label Manager")]
public class PlayerLabelManager : MonoBehaviour
{
	private class LabelEntry
	{
		public Transform transform;

		public GameObject go;

		public PlayerLabel label;

		public LabelEntry(Transform labelTransform, PlayerLabel playerLabel)
		{
			transform = labelTransform;
			go = transform.gameObject;
			label = playerLabel;
		}
	}

	public GameObject template;

	public float yOffset = 1.5f;

	public Camera hudCamera;

	private static int POOL_COUNT = 50;

	private static int VIS_THRESHOLD;

	private List<Transform> labelPool;

	private List<LabelEntry> labels;

	private Camera mainCamera;

	private MouseOrbit mouseOrbit;

	protected void Awake()
	{
		VIS_THRESHOLD = (int)((float)Mathf.Max(Screen.width, Screen.height) * 0.1f);
		template.SetActive(false);
		labelPool = new List<Transform>();
		Transform parent = template.transform.parent;
		for (int i = 0; i < POOL_COUNT; i++)
		{
			GameObject gameObject = Object.Instantiate(template);
			Transform transform = gameObject.transform;
			transform.SetParent(parent, false);
			transform.localScale = Vector3.one;
			labelPool.Add(transform);
		}
		labels = new List<LabelEntry>();
	}

	public void SetOwner(ushort owner)
	{
		mainCamera = Camera.main;
		mouseOrbit = SingleInstanceFindOnly<MouseOrbit>.Instance;
	}

	public void Clear()
	{
		if (labels.Count == 0)
		{
			return;
		}
		while (labels.Count > 0)
		{
			LabelEntry labelEntry = labels[0];
			labels.RemoveAt(0);
			if ((bool)labelEntry.transform && (bool)labelEntry.transform.gameObject)
			{
				PlayerLabel label = labelEntry.label;
				label.gameObject.SetActive(false);
				labelPool.Add(label.transform);
			}
		}
	}

	public void UpdateLabel(int index, PlayerData player)
	{
		PlayerLabel label;
		if (Get(player, out label))
		{
			label.Set(player);
			if (!player.isSpectator)
			{
				player.machine.SetLabel(label);
			}
		}
	}

	public bool Get(PlayerData player, out PlayerLabel label)
	{
		for (int i = 0; i < labels.Count; i++)
		{
			LabelEntry labelEntry = labels[i];
			if (labelEntry.label.player == player)
			{
				label = labelEntry.label;
				return true;
			}
		}
		if (labelPool.Count == 0)
		{
			label = null;
			return false;
		}
		Transform transform = labelPool[0];
		labelPool.RemoveAt(0);
		label = transform.GetComponent<PlayerLabel>();
		label.Set(player);
		labels.Add(new LabelEntry(transform, label));
		return true;
	}

	public void LateUpdate()
	{
		if (labels.Count == 0 || StatMaster.isHeadless)
		{
			return;
		}
		Vector3 camPos = mouseOrbit.camPos;
		Vector3 camForward = mouseOrbit.camForward;
		for (int i = 0; i < labels.Count; i++)
		{
			LabelEntry labelEntry = labels[i];
			Transform transform = labelEntry.transform;
			PlayerLabel label = labelEntry.label;
			PlayerData player = label.player;
			ServerMachine machine = player.machine;
			Vector3 vector = Vector3.zero;
			bool flag = true;
			if (player.isLocalPlayer && !player.isSpectator)
			{
				if (!machine.isSimulating)
				{
					flag = false;
				}
				else
				{
					vector = machine.MachineCenterPos + machine.LabelOffset;
				}
			}
			else if (StatMaster.Mode.hideLabels)
			{
				flag = false;
			}
			else if (player.useCustomPos)
			{
				vector = player.customPos;
			}
			else if (player.hasSelection)
			{
				vector = player.selectedEntity.GetCenter();
			}
			else if (!player.isSpectator)
			{
				if (machine.curtainMode && !machine.isSimulating)
				{
					vector = machine.Position;
					player.buildZone.UpdateCurtainIcon();
				}
				else
				{
					vector = machine.MachineCenterPos + machine.LabelOffset;
				}
			}
			else
			{
				flag = false;
			}
			bool flag2 = false;
			if (flag)
			{
				Vector3 vector2 = vector + Vector3.up * yOffset;
				float num = Vector3.Dot(camForward, vector2 - camPos);
				if (num > 0f)
				{
					Vector2 vector3 = mainCamera.WorldToScreenPoint(vector2);
					flag2 = vector3.x > (float)(-VIS_THRESHOLD) && vector3.x < (float)(Screen.width + VIS_THRESHOLD) && vector3.y > (float)(-VIS_THRESHOLD) && vector3.y < (float)(Screen.height + VIS_THRESHOLD);
					if (flag2)
					{
						transform.position = vector2;
					}
				}
			}
			if (labelEntry.go.activeInHierarchy != flag2)
			{
				player.isVisible = flag2;
				labelEntry.go.SetActive(flag2);
			}
		}
	}
}
