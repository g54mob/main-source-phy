using System;
using BesiegeDlc;
using UnityEngine;

[AddComponentMenu("UI/Multiplayer/Player Label")]
public class PlayerLabel : MonoBehaviour
{
	[HideInInspector]
	public PlayerData player;

	[SerializeField]
	private DynamicText nameLabel;

	[SerializeField]
	private GameObject copyButtonObject;

	[SerializeField]
	private Renderer teamIcon;

	[SerializeField]
	private Renderer copyIcon;

	[SerializeField]
	private Renderer blockedIcon;

	[SerializeField]
	private GameObject healthBar;

	[SerializeField]
	private Renderer healthBarRenderer;

	[SerializeField]
	private Renderer healthBarBackgroundRenderer;

	[SerializeField]
	private GameObject background;

	[SerializeField]
	private Transform content;

	[SerializeField]
	private Vector3 defaultPos;

	[SerializeField]
	private Vector3 healthbarPos;

	[SerializeField]
	private float backgroundXMargin = 0.4f;

	private Transform healthBarTransform;

	private NetworkAuxAddPiece networkAuxAddPiece;

	private ServerMachine machine;

	private MouseOrbit mouseOrbit;

	private float lastHealth;

	private string nameValue;

	private Transform camTransform;

	private AlignGUIObject[] alignGUIObjects;

	private bool initialized;

	private MeshRenderer textRenderer;

	private MeshRenderer backgroundRenderer;

	internal bool blockedByDLC;

	private void Initialize()
	{
		mouseOrbit = SingleInstanceFindOnly<MouseOrbit>.Instance;
		networkAuxAddPiece = NetworkAuxAddPiece.Instance;
		alignGUIObjects = GetComponentsInChildren<AlignGUIObject>();
		healthBarTransform = healthBar.transform;
		LabelCopyButton component = copyButtonObject.GetComponent<LabelCopyButton>();
		component.ButtonClicked = OnCopy;
		textRenderer = nameLabel.GetComponent<MeshRenderer>();
		backgroundRenderer = background.GetComponent<MeshRenderer>();
		initialized = true;
	}

	[ContextMenu("Test player names")]
	private void NameTest()
	{
		InvokeRepeating("TestNames", 1f, 1f);
	}

	private void TestNames()
	{
		string randomName = GetRandomName();
		UpdateName(randomName);
	}

	private string GetRandomName()
	{
		int num = UnityEngine.Random.Range(0, ScoreboardTester.RandomWords.Length);
		int num2 = UnityEngine.Random.Range(0, ScoreboardTester.RandomWords.Length);
		int num3 = UnityEngine.Random.Range(0, ScoreboardTester.RandomWords.Length);
		string text = ((num3 <= ScoreboardTester.RandomWords.Length / 2) ? string.Empty : ScoreboardTester.RandomWords[num3]);
		return ScoreboardTester.RandomWords[num] + " " + ScoreboardTester.RandomWords[num2] + " " + text;
	}

	public void Set(PlayerData playerData)
	{
		player = playerData;
		UpdateTeam();
		UpdateName(player.name);
	}

	private void UpdateName(string newName)
	{
		nameValue = newName.ToUpper();
		WorkshopManager.VerifyString(nameValue, delegate(WorkshopManager.VerifyStringResult res, string str)
		{
			if (nameLabel != null)
			{
				ReferenceMaster.SetDynamicText(nameLabel, str);
				if (initialized)
				{
					ResizeBackground();
					RealignObjects();
				}
			}
		});
	}

	private void ResizeBackground()
	{
		Bounds bounds = backgroundRenderer.bounds;
		Bounds bounds2 = textRenderer.bounds;
		float x = bounds.size.x;
		float x2 = bounds2.size.x;
		float num = ((!(x > 0f) || !(x2 > 0f)) ? 0f : (x2 / x));
		Vector3 localScale = backgroundRenderer.transform.localScale;
		localScale.x *= num;
		localScale.x += backgroundXMargin;
		backgroundRenderer.transform.localScale = localScale;
	}

	private void RealignObjects()
	{
		for (int i = 0; i < alignGUIObjects.Length; i++)
		{
			alignGUIObjects[i].RealignObject();
		}
	}

	public void UpdateTeam()
	{
		MPTeam mPTeam = ((!player.isSpectator) ? player.team : MPTeam.None);
		if (mPTeam == MPTeam.None)
		{
			teamIcon.enabled = false;
			return;
		}
		teamIcon.enabled = true;
		teamIcon.material.SetColor("_TintColor", ReferenceMaster.Instance.zoneColors[(int)mPTeam]);
	}

	protected bool ShowCopy()
	{
		if (player.isLocalPlayer)
		{
			return false;
		}
		if (!StatMaster.levelSimulating && (player.useCustomPos || player.hasSelection))
		{
			return true;
		}
		if (StatMaster.Mode.levelEdit || LevelEditor.Instance.Settings.AllowCopyMachine)
		{
			ServerMachine serverMachine = Machine.Active() as ServerMachine;
			return serverMachine != null && !serverMachine.curtainMode && !serverMachine.isSimulating && serverMachine.CanModify && serverMachine.ReadyForSim;
		}
		return false;
	}

	public void OnEnable()
	{
		if (!initialized)
		{
			Initialize();
		}
		if (player != null)
		{
			bool active = ShowCopy();
			copyButtonObject.SetActive(active);
			if (!string.IsNullOrEmpty(nameValue))
			{
				UpdateName(nameValue);
			}
			UpdateLabel();
			ReferenceMaster.onMachineDLCStateChanged = (Action)Delegate.Combine(ReferenceMaster.onMachineDLCStateChanged, new Action(DlcUpdate));
			DlcUpdate();
		}
	}

	public void OnCopy()
	{
		if (player.useCustomPos)
		{
			CopyPrefab();
		}
		else
		{
			CopyMachine();
		}
	}

	private void CopyPrefab()
	{
		LevelPrefab activePrefab = player.activePrefab;
		if (!StatMaster.levelSimulating && activePrefab != null && StatMaster.SelectedLevelPrefab != activePrefab)
		{
			LevelEditor.Instance.SetPrefab(activePrefab);
		}
	}

	private void CopyMachine()
	{
		ServerMachine serverMachine = Machine.Active() as ServerMachine;
		if (player != null && (bool)player.machine && !blockedByDLC && (bool)serverMachine && !serverMachine.isSimulating && serverMachine.CanModify && serverMachine.ReadyForSim)
		{
			if (!player.buildZone)
			{
				Debug.LogError("Machine doesn't have a build zone!");
				return;
			}
			byte[] array = new byte[2];
			NetworkCompression.WriteUInt16(player.networkId, array, 0);
			networkAuxAddPiece.SendServerMessage(RPCMessageType.Clone, array);
		}
	}

	public void UpdateHealth(float health, bool forced)
	{
		if (!initialized)
		{
			Initialize();
		}
		if (forced || health != lastHealth)
		{
			healthBarRenderer.material.SetColor("_Color", Color.Lerp(ReferenceMaster.Instance.badColor, ReferenceMaster.Instance.goodColor, health));
			healthBarTransform.localScale = new Vector3(health, healthBarTransform.localScale.y, healthBarTransform.localScale.z);
			lastHealth = health;
		}
	}

	private void UpdateLabel()
	{
		bool flag = false;
		if (!player.isSpectator)
		{
			ServerMachine serverMachine = player.machine;
			if ((bool)serverMachine && serverMachine.registerDamage)
			{
				flag = true;
			}
		}
		if (healthBar.activeSelf != flag)
		{
			healthBar.SetActive(flag);
			healthBarBackgroundRenderer.enabled = flag;
			backgroundRenderer.enabled = !flag;
		}
		content.localPosition = ((!flag) ? defaultPos : healthbarPos);
		base.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
		RealignObjects();
		ResizeBackground();
		Vector3 worldPosition = ProjectPointOnPlane(mouseOrbit.camForward, mouseOrbit.camPos, base.transform.position);
		base.transform.LookAt(worldPosition, mouseOrbit.camUp);
		if (ShowCopy())
		{
			if (!copyButtonObject.activeSelf)
			{
				copyButtonObject.SetActive(true);
			}
		}
		else if (copyButtonObject.activeSelf)
		{
			copyButtonObject.SetActive(false);
		}
	}

	public void LateUpdate()
	{
		UpdateLabel();
	}

	private Vector3 ProjectPointOnPlane(Vector3 planeNormal, Vector3 planePoint, Vector3 point)
	{
		float num = 0f - Vector3.Dot(planeNormal, point - planePoint);
		return point + planeNormal * num;
	}

	internal void DlcUpdate()
	{
		blockedByDLC = false;
		if (player != null && player.machine != null && player.machine.containsDLCs != null)
		{
			for (int i = 0; i < player.machine.containsDLCs.Count; i++)
			{
				if (!DlcManager.Instance.HasPurchasedDlc(player.machine.containsDLCs[i]))
				{
					blockedByDLC = true;
				}
			}
		}
		copyIcon.gameObject.SetActive(!blockedByDLC);
		blockedIcon.gameObject.SetActive(blockedByDLC);
	}

	public void OnDisable()
	{
		ReferenceMaster.onMachineDLCStateChanged = (Action)Delegate.Remove(ReferenceMaster.onMachineDLCStateChanged, new Action(DlcUpdate));
	}

	public void OnDestroy()
	{
		ReferenceMaster.onMachineDLCStateChanged = (Action)Delegate.Remove(ReferenceMaster.onMachineDLCStateChanged, new Action(DlcUpdate));
	}
}
