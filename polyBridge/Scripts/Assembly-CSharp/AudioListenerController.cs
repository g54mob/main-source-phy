using UnityEngine;

public class AudioListenerController : MonoBehaviour
{
	public AnimationCurve AmbienceAttenuationCurve;

	private Vector3 _originPos;

	private void Start()
	{
		_originPos = TerrainIslands.GetAveragePositionOfBookendSpawnPoints();
	}

	private void LateUpdate()
	{
		Camera camera = Cameras.MainCamera();
		Vector3 vector = camera.transform.position - _originPos;
		Vector3 normalized = vector.normalized;
		float magnitude = vector.magnitude;
		Vector3 vector2 = camera.cameraToWorldMatrix.MultiplyPoint(new Vector3(0f, 0f, 0f - magnitude));
		base.transform.position = vector2 + normalized * Cameras.GetOrthographicSize();
		base.transform.rotation = camera.transform.rotation;
		UpdateAmbienceSound();
	}

	private void UpdateAmbienceSound()
	{
		float num = Game.MinOrthographicSize();
		float num2 = Game.MaxOrthographicSize();
		float time = 1f - (Cameras.GetOrthographicSize() - num) / (num2 - num);
		ThemeAudio.UpdateAmbienceVolume(AmbienceAttenuationCurve.Evaluate(time));
	}
}
