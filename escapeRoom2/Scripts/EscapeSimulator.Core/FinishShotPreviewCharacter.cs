using System;
using UnityEngine;

[ExecuteAlways]
public class FinishShotPreviewCharacter : MonoBehaviour
{
	[NonSerialized]
	public FinishLevelShot parentShot;

	private void Update()
	{
		if (parentShot == null)
		{
			UnityEngine.Object.DestroyImmediate(base.gameObject);
		}
	}
}
