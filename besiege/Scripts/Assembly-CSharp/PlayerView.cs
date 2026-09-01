using UnityEngine;

public class PlayerView : MonoBehaviour
{
	public Texture readyIcon;

	public Texture notReadyIcon;

	public Texture spectatorIcon;

	public Renderer stateIcon;

	private Vector3 startpos;

	public PlayerData player;

	public DynamicText playerName;

	private Transform playerNameTransform;

	private float maxNameSize = 0.95f;

	protected bool inited;

	protected int lastIndex;

	public virtual bool Init()
	{
		if (inited)
		{
			return true;
		}
		inited = true;
		playerNameTransform = playerName.transform;
		startpos = base.transform.localPosition;
		return false;
	}

	public virtual void UpdateView(int index, PlayerData playerData)
	{
		Init();
		player = playerData;
		lastIndex = index;
		playerNameTransform.localScale = Vector3.one;
		ReferenceMaster.SetDynamicText(playerName, player.name);
		float x = playerName.bounds.extents.x;
		if (x > maxNameSize)
		{
			float num = maxNameSize / x;
			playerNameTransform.localScale = new Vector3(num, num, num);
		}
		stateIcon.material.mainTexture = (playerData.isSpectator ? spectatorIcon : ((!StatMaster.levelSimulating || StatMaster.isLocalSim) ? readyIcon : notReadyIcon));
		base.transform.localPosition = startpos + Vector3.up * ((PlayerViewer.voteIndex == -1 || PlayerViewer.voteIndex >= index || !PlayerData.hasLocalPlayer || PlayerData.localPlayer.isSpectator) ? 0f : (-0.35f));
	}
}
