using System;

namespace CloudinaryDotNet
{
	public class VideoLayer : BaseLayer<VideoLayer>
	{
		public VideoLayer()
		{
			m_resourceType = "video";
		}

		public VideoLayer(string publicId)
			: this()
		{
			PublicId(publicId);
		}

		public new VideoLayer ResourceType(string resourceType)
		{
			throw new InvalidOperationException("Cannot modify resourceType " + resourceType + " for video layers");
		}

		public new VideoLayer Type(string type)
		{
			throw new InvalidOperationException("Cannot modify type " + type + " for video layers");
		}

		public new VideoLayer Format(string format)
		{
			throw new InvalidOperationException("Cannot modify format " + format + " for video layers");
		}

		public override string AdditionalParams()
		{
			if (string.IsNullOrEmpty(m_publicId))
			{
				throw new ArgumentException("Must supply publicId.");
			}
			return base.AdditionalParams();
		}
	}
}
