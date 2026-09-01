using UnityEngine;

public class SetPoseOnDisrens : MonoBehaviour
{
	public MeshFilter MeshFiltery;

	public EnemyAISimple aiCode;

	public Mesh[] Pos2;

	public float speedToRun = 6f;

	private Vector3 direction;

	private void Start()
	{
		if (aiCode.runAway)
		{
			aiCode.runAwaySpeed = speedToRun;
		}
	}

	private void Update()
	{
		if (!StatMaster.levelSimulating || aiCode.isDead || StatMaster.GodTools.GravityDisabled)
		{
			return;
		}
		Machine machine = Machine.Active();
		if (!(machine == null))
		{
			direction = machine.MiddlePosition - base.transform.position;
			float num = Random.Range(8f, 12f);
			if (num * num > direction.sqrMagnitude)
			{
				aiCode.runAwaySpeed = 3f;
				MeshFiltery.mesh = Pos2[Random.Range(0, Pos2.Length)];
				Object.Destroy(this);
			}
		}
	}
}
