using UnityEngine;

public class TwitchVoteCubeSpawn : TwitchAction
{
	public TwitchCube Cube;

	private int noCubes = 10;

	public override void RunAction(string name, string text)
	{
		GameObject gameObject = GameObject.FindGameObjectsWithTag("Respawn")[0];
		for (int i = 0; i < noCubes; i++)
		{
			Vector3 position = gameObject.transform.position;
			position.x += Random.Range(-10f, 10f);
			position.z += Random.Range(-10f, 10f);
			Object.Instantiate(Cube, position, Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))));
		}
	}
}
