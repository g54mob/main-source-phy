using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuLevelUI : MonoBehaviour
{
	public Text title;

	public Text tokens;

	public Image tokensIcon;

	public RawImage levelImage;

	public RawImage levelImageOverlay;

	public GameObject installing;

	public GameObject notInstalled;

	public Button unsubscribe;

	public Slider installingSlider;

	public Text saveDate;

	public Text playTime;

	public GameObject locked;

	public GameObject finishIcon;

	public GameObject trophyIcon;

	public GameObject customLevelDotIcon;

	public GameObject customLevelNewIcon;

	public GameObject customLevelFinishIcon;

	public GameObject customLevelDownloadedIcon;

	public GameObject downloadLevelButton;

	public GameObject downloadLevelIcon;

	public DateTime dateCreated = DateTime.Now;

	public bool isCustom;

	public ulong collectionId;

	public int orderingNumber;

	public string levelPack;

	public int levelIndex;

	public string savePath;

	public DLC notInstalledDlc;
}
