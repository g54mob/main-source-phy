using UnityEngine;

namespace LevelCreator
{
	public class RainHazard : LandscapeHazard
	{
		[SerializeField]
		private string m_blurBrushKey = string.Empty;

		private Brush m_blurBrush;

		[SerializeField]
		private float m_startDelay;

		[SerializeField]
		private float m_destroyDelay = 4.5f;

		private float startTimer;

		private float blurTimer;

		private void Start()
		{
			m_blurBrush = brushTable.GetBrush(m_blurBrushKey);
			Object.Destroy(base.gameObject, m_destroyDelay);
		}

		private void Update()
		{
			if (startTimer >= m_startDelay)
			{
				if (blurTimer > 0.25f)
				{
					DMEditor.Instance.VolumeRootObject.BlurVolume(base.transform.position, m_blurBrush);
					blurTimer = 0f;
				}
				else
				{
					blurTimer += Time.deltaTime;
				}
			}
			else
			{
				startTimer += Time.deltaTime;
			}
		}

		private new void OnDestroy()
		{
			if (!(DMEditor.Instance == null) && !(DMEditor.Instance.VolumeRootObject == null))
			{
				DMEditor.Instance.MarkObjectsForSnapping(DMEditor.Instance.VolumeRootObject.GetBounds(base.transform.position, m_blurBrush));
				if (TakeSnapshot)
				{
					DMEditor.Instance.ScheduleTakeLevelSnapshot();
				}
			}
		}
	}
}
