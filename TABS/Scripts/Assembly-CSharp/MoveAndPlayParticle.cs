using UnityEngine;

public class MoveAndPlayParticle : MonoBehaviour
{
	public ParticleSystem part;

	public void Go()
	{
		part.transform.position = base.transform.position;
		part.Emit(10);
	}
}
