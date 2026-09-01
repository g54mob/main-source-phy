using UnityEngine;
using UnityEngine.UI;

public class GetPlaylistThumbnail : ThumbnailComponent
{
	public RawImage thumb;

	public bool isCanvas = true;

	protected override Texture GetCurrentTexture()
	{
		return (!isCanvas) ? base.GetCurrentTexture() : thumb.texture;
	}

	protected override void ApplyTexture(Texture tex)
	{
		if (isCanvas)
		{
			thumb.texture = tex;
		}
		else
		{
			base.ApplyTexture(tex);
		}
	}

	public override void Initialize(string thumbnailPath, bool isFolderThumbnail)
	{
		base.Initialize(thumbnailPath, isFolderThumbnail);
		ResolveThumbnail(thumbnailPath);
	}

	public override void SetInvisible()
	{
	}

	public override void SetVisible()
	{
	}
}
