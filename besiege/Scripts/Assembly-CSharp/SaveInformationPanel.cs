using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using SFB;
using UnityEngine;

public class SaveInformationPanel : MonoBehaviour
{
	public Action<string> OnThumbnailChanged;

	[SerializeField]
	private ClickBehaviour openPhotoButton;

	public Transform Thumbnail;

	public TextMesh MachineNameMesh;

	public TextMesh SuffixMesh;

	public TextMesh BlockCountMesh;

	public Texture2D noThumbTex;

	private Texture2D currentTex;

	private Texture remoteTexture;

	private string fileName;

	private string thumbnailPath;

	private Material thumbnailMaterial;

	private bool inited;

	public static string WorkshopThumbnailPath
	{
		get
		{
			return StaticSettings.DataPath + "/WorkshopThumbnails/";
		}
	}

	private void Awake()
	{
		thumbnailMaterial = Thumbnail.GetComponent<Renderer>().material;
	}

	public void Initialize(string fileName, string thumbnailPath, Texture cachedTexture = null)
	{
		Initialize(fileName, thumbnailPath, cachedTexture, UploadDialog.UploadDialogMode.NewUpload);
	}

	public void Initialize(string fileName, string thumbnailPath, Texture cachedTexture, UploadDialog.UploadDialogMode mode)
	{
		this.fileName = fileName;
		this.thumbnailPath = thumbnailPath;
		remoteTexture = cachedTexture;
		if (cachedTexture != null)
		{
			thumbnailMaterial.mainTexture = remoteTexture;
		}
		if (mode == UploadDialog.UploadDialogMode.NewUpload)
		{
			inited = true;
		}
		GetThumbnailFolderPath();
		inited = true;
	}

	private void Start()
	{
		StopAllCoroutines();
		StartCoroutine(LoadImage(thumbnailPath));
		if (MachineNameMesh != null)
		{
			MachineNameMesh.text = fileName;
			if (SuffixMesh != null)
			{
				SuffixMesh.transform.position = new Vector3(MachineNameMesh.transform.GetComponent<Renderer>().bounds.max.x, SuffixMesh.transform.position.y, SuffixMesh.transform.position.z);
			}
		}
	}

	private bool isErrorImage(Texture tex)
	{
		return tex != null && tex.name == string.Empty && tex.height == 8 && tex.width == 8 && tex.filterMode == FilterMode.Bilinear && tex.anisoLevel == 1 && tex.wrapMode == TextureWrapMode.Repeat && tex.mipMapBias == 0f;
	}

	private IEnumerator LoadImage(string thumbnailPath, bool checkValidity = false)
	{
		if (thumbnailPath == null)
		{
			yield break;
		}
		yield return null;
		if (ReferenceMaster.UIActive != ReferenceMaster.WorkshopItemType.Machine && ReferenceMaster.UIActive != ReferenceMaster.WorkshopItemType.Levels && ReferenceMaster.UIActive != ReferenceMaster.WorkshopItemType.Skins)
		{
			UnityEngine.Debug.Log("Wrong UI mode for thumbnail selection");
			yield break;
		}
		string thumbnailFilePath = thumbnailPath.Replace("\\", "/");
		if (!File.Exists(thumbnailFilePath))
		{
			UnityEngine.Debug.Log("Thumbnail file not found");
			thumbnailMaterial.mainTexture = noThumbTex;
			yield break;
		}
		WWW www = new WWW("file:///" + thumbnailFilePath);
		yield return www;
		if (!string.IsNullOrEmpty(www.error))
		{
			UnityEngine.Debug.LogFormat("Could not load image at location '{0}', error: {1}", www.url, www.error);
			yield break;
		}
		try
		{
			Texture2D tex = www.texture;
			tex.name = thumbnailFilePath;
			if (checkValidity && tex.width != tex.height)
			{
				UnityEngine.Debug.LogWarning("Only square thumbnails are supported");
				UnityEngine.Object.DestroyImmediate(tex);
				yield break;
			}
			if (isErrorImage(tex))
			{
				UnityEngine.Debug.Log("Error loading thumbnail");
				thumbnailMaterial.mainTexture = noThumbTex;
				yield break;
			}
			if (currentTex != null)
			{
				UnityEngine.Object.DestroyImmediate(currentTex);
			}
			this.thumbnailPath = thumbnailPath;
			currentTex = tex;
			thumbnailMaterial.mainTexture = currentTex;
		}
		catch (Exception message)
		{
			UnityEngine.Debug.LogError(message);
			thumbnailMaterial.mainTexture = noThumbTex;
		}
	}

	private void OnApplicationFocus(bool focusStatus)
	{
		if (focusStatus)
		{
			StopAllCoroutines();
			StartCoroutine(LoadImage(thumbnailPath));
		}
	}

	private void OnMouseEnter()
	{
		StopAllCoroutines();
		StartCoroutine(LoadImage(thumbnailPath));
	}

	private void OnDisable()
	{
		if (currentTex != null)
		{
			UnityEngine.Object.DestroyImmediate(currentTex);
		}
		if (openPhotoButton != null)
		{
			ClickBehaviour clickBehaviour = openPhotoButton;
			clickBehaviour.OnActivation = (Action)Delegate.Remove(clickBehaviour.OnActivation, new Action(OnOpenPhotoClicked));
		}
	}

	private void OnEnable()
	{
		if (openPhotoButton != null)
		{
			ClickBehaviour clickBehaviour = openPhotoButton;
			clickBehaviour.OnActivation = (Action)Delegate.Combine(clickBehaviour.OnActivation, new Action(OnOpenPhotoClicked));
		}
	}

	private void UpdateThumbnailPath(string path)
	{
		thumbnailPath = path;
		StopAllCoroutines();
		StartCoroutine(LoadImage(path, true));
		if (inited && OnThumbnailChanged != null)
		{
			UnityEngine.Debug.Log("changed thumbnail: " + path);
			OnThumbnailChanged(path);
		}
	}

	private void OnOpenPhotoClicked()
	{
		string thumbnailFolderPath = GetThumbnailFolderPath();
		if (OnThumbnailChanged != null)
		{
			UnityEngine.Debug.Log("changed thumbnail: " + thumbnailPath);
			OnThumbnailChanged(thumbnailPath);
		}
		if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
		{
			string[] array = StandaloneFileBrowser.OpenFilePanel("Select Thumbnail", thumbnailFolderPath, "png", false);
			if (array == null || array.Length <= 0)
			{
				return;
			}
			thumbnailFolderPath = array[0];
			if (!string.IsNullOrEmpty(thumbnailFolderPath))
			{
				FileInfo fileInfo = new FileInfo(thumbnailFolderPath);
				if (fileInfo.Length < 1048576)
				{
					UnityEngine.Debug.Log("changed thumbnail to: " + thumbnailFolderPath);
					UpdateThumbnailPath(thumbnailFolderPath);
				}
				else
				{
					UnityEngine.Debug.Log("Thumbnails need to be less than 1MB");
				}
			}
		}
		else if (FileBrowserView.saveMenuUpload)
		{
			Process.Start(WorkshopThumbnailPath);
		}
		else if (ReferenceMaster.UIActive == ReferenceMaster.WorkshopItemType.Skins)
		{
			Process.Start(thumbnailPath);
		}
		else
		{
			Process.Start(StaticSettings.DataPath + "/SavedMachines/Thumbnails/");
		}
	}

	private void CreateDefaultTexture()
	{
		if (!File.Exists(thumbnailPath))
		{
			Texture2D texture2D = remoteTexture as Texture2D;
			if (texture2D == null)
			{
				texture2D = noThumbTex;
			}
			byte[] bytes = texture2D.EncodeToPNG();
			File.WriteAllBytes(thumbnailPath, bytes);
			UpdateThumbnailPath(thumbnailPath);
		}
	}

	private string GetThumbnailFolderPath()
	{
		string text;
		if (!File.Exists(thumbnailPath))
		{
			if (FileBrowserView.saveMenuUpload)
			{
				text = WorkshopThumbnailPath;
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				thumbnailPath = text.Replace("/", "\\") + fileName + ".png";
				CreateDefaultTexture();
			}
			else
			{
				if (Directory.Exists(thumbnailPath))
				{
					text = thumbnailPath;
					thumbnailPath = Path.Combine(text, "thumbnail.png");
				}
				else
				{
					text = Path.GetDirectoryName(thumbnailPath);
					thumbnailPath = Path.Combine(text.Replace("/", "\\"), fileName + ".png");
				}
				CreateDefaultTexture();
			}
		}
		else
		{
			thumbnailPath = thumbnailPath.Replace("/", "\\");
			text = Path.GetDirectoryName(thumbnailPath);
		}
		return text;
	}
}
