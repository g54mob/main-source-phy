using UnityEngine;

public class PreviewCacheItem
{
	public Texture2D m_Texture2D;

	public float m_CreateTime;

	public PreviewCacheItem(Texture2D texture2D, float createTime)
	{
		m_Texture2D = texture2D;
		m_CreateTime = createTime;
	}
}
