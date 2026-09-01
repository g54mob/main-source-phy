using UnityEngine;

public class SetMachineCenterForGridShader : MonoBehaviour
{
	public MeshRenderer[] renderers;

	private Vector3 lastVec;

	private Vector3 defaultCenter;

	private MaterialPropertyBlock prop;

	private void Start()
	{
		defaultCenter = Vector3.up * 5.072f;
		prop = new MaterialPropertyBlock();
	}

	private void Update()
	{
		AddPiece instance = SingleInstanceFindOnly<AddPiece>.Instance;
		Vector3 position = defaultCenter;
		if ((!StatMaster.isMP || (PlayerData.hasLocalPlayer && !PlayerData.localPlayer.isSpectator)) && instance != null)
		{
			position = instance.middleOfObject.position;
		}
		if (position != lastVec)
		{
			prop.SetVector("_MachinePos", position);
			for (int i = 0; i < renderers.Length; i++)
			{
				prop.SetVector("_MainTex_ST", renderers[i].material.GetVector("_MainTex_ST"));
				renderers[i].SetPropertyBlock(prop);
			}
			lastVec = position;
		}
	}
}
