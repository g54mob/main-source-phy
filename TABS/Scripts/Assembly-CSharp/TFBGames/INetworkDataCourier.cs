using UnityEngine;

namespace TFBGames
{
	public interface INetworkDataCourier : IService
	{
		event TextureReceivedEventHandler TextureReceived;

		void SendTexture(NetworkTextureType textureType, Texture2D texture);
	}
}
