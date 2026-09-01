using Localisation;
using UnityEngine;

public class LevelEditorPlaylistSlot : LevelPlaylistSlot
{
	public TextMesh playerInfo;

	public UIButton btn;

	public GameObject selectGraphic;

	private Renderer thumbRenderer;

	protected static Camera hudCam;

	private bool mouseOver;

	public override void PreInit()
	{
		if (hudCam == null)
		{
			hudCam = GameObject.Find("HUD Cam").GetComponent<Camera>();
		}
		base.PreInit();
		btn.Click += OnSelect;
		thumbRenderer = playlistThumbnail.GetComponent<MeshRenderer>();
	}

	private void OnSelect()
	{
		manager.OnSelect(this);
	}

	public override void Select(bool toggle)
	{
		selectGraphic.SetActive(toggle);
	}

	protected void Update()
	{
		bool flag = false;
		if (!LevelPlaylistEditor.editorMode)
		{
			Vector2 vector = InputManager.CursorPosition();
			Vector3 vector2 = hudCam.ScreenToWorldPoint(new Vector3(vector.x, vector.y, 10f));
			Bounds bounds = thumbRenderer.bounds;
			flag = vector2.x > bounds.min.x && vector2.x < bounds.max.x && vector2.y > bounds.min.y && vector2.y < bounds.max.y;
		}
		if (mouseOver != flag)
		{
			ToggleHover(flag);
			mouseOver = flag;
		}
	}

	public override void Init(LevelPlaylistManager playlistManager, string levelPath)
	{
		base.Init(playlistManager, levelPath);
		int minPlayers;
		int maxPlayers;
		ReadPlayerLimitInfo(levelPath, out minPlayers, out maxPlayers);
		string text = ((maxPlayers == -1) ? LocalisationManager.GetTranslation(3360) : ((minPlayers == -1) ? string.Format(LocalisationManager.GetTranslation(3358), maxPlayers) : string.Format(LocalisationManager.GetTranslation(3359), minPlayers, maxPlayers)));
		playerInfo.text = text;
	}
}
