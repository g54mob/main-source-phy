using UnityEngine;

public class BoatController : MonoBehaviour
{
	public GameObject[] BoatInstance;

	public Transform[] spawnPos;

	public Transform boatTarget;

	private string animationToPlay;

	public AnimationClip[] animations;

	public float pos2Rotation;

	public float pos3Rotation;

	public float pos4Rotation;

	private int typeOfBoat;

	private int selectedSpawnPoint;

	private int animationSelect;

	private Animation boatMove;

	private GameObject clone;

	public float timer = 5f;

	private float countdown;

	private void Start()
	{
		countdown = timer;
		SelectBoatAndLocation();
	}

	private void Update()
	{
		countdown -= Time.deltaTime;
		if (!(countdown > 0f))
		{
			SelectBoatAndLocation();
		}
	}

	private void SpawnBoat(float rotation, string animationName)
	{
		clone = Object.Instantiate(BoatInstance[typeOfBoat], spawnPos[selectedSpawnPoint].position, Quaternion.Euler(0f, rotation, 0f)) as GameObject;
		boatMove = clone.GetComponent<Animation>();
		countdown = timer;
		boatMove.Play(animationName);
	}

	private void SelectBoatAndLocation()
	{
		typeOfBoat = Random.Range(0, BoatInstance.Length);
		selectedSpawnPoint = Random.Range(0, spawnPos.Length);
		animationSelect = Random.Range(0, animations.Length);
		switch (selectedSpawnPoint)
		{
		case 0:
			switch (animationSelect)
			{
			case 0:
				SpawnBoat(0f, animations[0].name);
				break;
			case 1:
				SpawnBoat(0f, animations[1].name);
				break;
			}
			break;
		case 1:
			SpawnBoat(pos2Rotation, animations[1].name);
			break;
		case 2:
			switch (animationSelect)
			{
			case 0:
				SpawnBoat(pos3Rotation, animations[0].name);
				break;
			case 1:
				SpawnBoat(pos3Rotation + 10f, animations[1].name);
				break;
			}
			break;
		case 3:
			switch (animationSelect)
			{
			case 0:
				SpawnBoat(pos4Rotation, animations[0].name);
				break;
			case 1:
				SpawnBoat(pos4Rotation + 10f, animations[1].name);
				break;
			}
			break;
		}
	}
}
