using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class LevelPlaylistSlot : MonoBehaviour
{
	[HideInInspector]
	public string path;

	public bool isCanvas = true;

	public Button canvasMoveUp;

	public Button canvasMoveDown;

	public Button canvasTrash;

	public Text canvasName;

	public UIButton moveUpButton;

	public UIButton moveDownButton;

	public UIButton trashButton;

	public TextMesh displayName;

	public GameObject hoverOverGO;

	public GetPlaylistThumbnail playlistThumbnail;

	protected LevelPlaylistManager manager;

	protected string fileName;

	private GameObject moveUpGO;

	private GameObject moveDownGO;

	private static Regex maxPlayerRegex;

	private static Regex minPlayerRegex;

	public virtual void PreInit()
	{
		if (isCanvas)
		{
			moveUpGO = canvasMoveUp.gameObject;
			moveDownGO = canvasMoveDown.gameObject;
			canvasTrash.onClick.AddListener(OnDelete);
			canvasMoveUp.onClick.AddListener(OnMoveUp);
			canvasMoveDown.onClick.AddListener(OnMoveDown);
		}
		else
		{
			moveUpButton.Click += OnMoveUp;
			moveDownButton.Click += OnMoveDown;
			trashButton.Click += OnDelete;
			moveUpGO = moveUpButton.gameObject;
			moveDownGO = moveDownButton.gameObject;
		}
	}

	protected void OnEnable()
	{
		HoverOut();
	}

	public void HoverOver()
	{
		ToggleHover(true);
	}

	public void HoverOut()
	{
		ToggleHover(false);
	}

	public void ToggleHover(bool toggle)
	{
		if (hoverOverGO.activeSelf != toggle)
		{
			hoverOverGO.SetActive(toggle);
		}
	}

	public virtual void Init(LevelPlaylistManager playlistManager, string levelPath)
	{
		manager = playlistManager;
		path = levelPath;
		fileName = Path.GetFileNameWithoutExtension(levelPath);
		if (isCanvas)
		{
			canvasName.text = fileName;
		}
		else
		{
			displayName.text = fileName;
		}
		string[] array = new string[2] { ".png", ".jpg" };
		string text = Path.Combine(Path.GetDirectoryName(levelPath), Path.Combine("Thumbnails", fileName));
		string thumbnailPath = text + array[0];
		if (!File.Exists(thumbnailPath))
		{
			thumbnailPath = text + array[1];
			if (!File.Exists(thumbnailPath))
			{
				text = Path.Combine(Path.GetDirectoryName(levelPath), fileName);
				thumbnailPath = text + array[0];
				if (!File.Exists(thumbnailPath))
				{
					thumbnailPath = text + array[1];
				}
			}
		}
		playlistThumbnail.Initialize(thumbnailPath, false);
	}

	public void ToggleMoveButton(bool isDown, bool toggle)
	{
		if (isDown)
		{
			moveDownGO.SetActive(toggle);
		}
		else
		{
			moveUpGO.SetActive(toggle);
		}
	}

	private void OnDelete()
	{
		manager.OnDelete(this);
	}

	private void OnMoveUp()
	{
		manager.OnMoveUp(this);
	}

	private void OnMoveDown()
	{
		manager.OnMoveDown(this);
	}

	public virtual void Select(bool toggle)
	{
	}

	protected void ReadPlayerLimitInfo(string levelPath, out int minPlayers, out int maxPlayers)
	{
		minPlayers = (maxPlayers = -1);
		if (!File.Exists(levelPath))
		{
			return;
		}
		if (minPlayerRegex == null)
		{
			minPlayerRegex = new Regex("MinPlayers\\s*=\\s*\"\\s*([0-9-]+)\\s*\"", RegexOptions.Compiled);
			maxPlayerRegex = new Regex("MaxPlayers\\s*=\\s*\"\\s*([0-9-]+)\\s*\"", RegexOptions.Compiled);
		}
		using (StreamReader streamReader = new StreamReader(levelPath))
		{
			string text;
			while ((text = streamReader.ReadLine()) != null && !text.Contains("</LevelSettings>"))
			{
				Match match = minPlayerRegex.Match(text);
				if (match.Success)
				{
					minPlayers = int.Parse(match.Groups[1].Value);
				}
				Match match2 = maxPlayerRegex.Match(text);
				if (match2.Success)
				{
					maxPlayers = int.Parse(match2.Groups[1].Value);
				}
			}
		}
	}
}
