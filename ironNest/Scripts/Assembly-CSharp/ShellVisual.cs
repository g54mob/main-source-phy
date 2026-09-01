using UnityEngine;

public class ShellVisual : MonoBehaviour
{
	private RectTransform rectTransform;

	private RectTransform boardRect;

	private ShellVisualBoundaryConfig config;

	private Vector2 startLocalPos;

	private Vector2 targetLocalPos;

	private float travelTime;

	private double startedAt;

	private double endsAt;

	private ShellDefinition impactShell;

	private Vector2 previousPos;

	private bool hasExitedMap;

	private float totalPathDistance;

	public void Initialize(Vector2 startPos, Vector2 targetPos, float travelDuration, ShellDefinition shell)
	{
	}

	private void Update()
	{
	}

	private bool IsInsideBoard(Vector2 localPos)
	{
		return false;
	}

	private Vector2 ClampToBorder(Vector2 pos)
	{
		return default(Vector2);
	}

	private MapBorderSide DetermineBorderSide(Vector2 outsidePos)
	{
		return default(MapBorderSide);
	}

	private void HandleBoundaryExit(Vector2 lastInsidePos, MapBorderSide borderSide)
	{
	}

	private void SpawnOutOfBoundsEffectAt(Vector2 localPos, float exitAngleDeg, float remainingDistance, MapBorderSide borderSide)
	{
	}

	private void SpawnImpactEffectAt(Vector2 localPos)
	{
	}
}
