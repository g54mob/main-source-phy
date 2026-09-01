using UnityEngine;

public class LocalizeTextPlatform : LocalizeText
{
	[Header("Platform Specific")]
	[SerializeField]
	private string playstationId = "$none";

	[SerializeField]
	private string nintendoId = "$none";

	[SerializeField]
	private string gameCoreId = "$none";

	protected override void Awake()
	{
		base.Awake();
	}
}
