using UnityEngine;

public class GodTornadoController : MonoBehaviour
{
	public Transform tornadoPrefab;

	public bool tornadoActive;

	public RaycastHit hit;

	public AddPiece addPieceCode;

	public float lerpSpeed;

	private Ray ray;

	private Transform activeTornado;

	private void Start()
	{
		addPieceCode = SingleInstanceFindOnly<AddPiece>.Instance;
	}

	private void Update()
	{
		if (Machine.Active().isSimulating && InputManager.LeftMouseButtonHeld() && !addPieceCode.hudOccluding)
		{
			ray = Camera.main.ScreenPointToRay(InputManager.CursorPosition());
			if (Physics.Raycast(ray, out hit, 1000f))
			{
				SetTornado();
			}
		}
		else
		{
			tornadoActive = false;
		}
	}

	private void SetTornado()
	{
		if (!tornadoActive)
		{
			tornadoActive = true;
			if (activeTornado != null)
			{
				Object.Destroy(activeTornado.gameObject);
			}
			activeTornado = (Object.Instantiate(tornadoPrefab, hit.point, Quaternion.identity) as GameObject).transform;
		}
		if (activeTornado != null)
		{
			activeTornado.position = Vector3.Lerp(activeTornado.position, hit.point, Time.deltaTime * lerpSpeed);
		}
	}
}
