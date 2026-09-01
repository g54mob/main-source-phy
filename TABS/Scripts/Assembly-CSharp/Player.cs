using UnityEngine;

public class Player : MonoBehaviour
{
	public static Player localPlayer;

	public CharacterData data;

	private void Awake()
	{
		localPlayer = this;
		data = GetComponent<CharacterData>();
	}

	public RaycastHit LooksAt()
	{
		RaycastHit hitInfo = default(RaycastHit);
		Physics.Raycast(new Ray(data.camera.position, data.camera.forward), out hitInfo, 5f);
		return hitInfo;
	}
}
