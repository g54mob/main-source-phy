using System;
using System.Collections.Generic;
using System.Globalization;
using CloudinaryDotNet.Core;

namespace CloudinaryDotNet
{
	public abstract class BaseLayer : CloudinaryDotNet.Core.ICloneable
	{
		public abstract object Clone();
	}
	public abstract class BaseLayer<T> : BaseLayer where T : BaseLayer<T>
	{
		protected string m_resourceType;

		protected string m_type;

		protected string m_publicId;

		protected string m_format;

		public T ResourceType(string resourceType)
		{
			m_resourceType = resourceType;
			return (T)this;
		}

		public T Type(string type)
		{
			m_type = type;
			return (T)this;
		}

		public T PublicId(string publicId)
		{
			m_publicId = publicId.Replace('/', ':');
			return (T)this;
		}

		public T Format(string format)
		{
			m_format = format;
			return (T)this;
		}

		public virtual string AdditionalParams()
		{
			return string.Empty;
		}

		public override string ToString()
		{
			List<string> list = new List<string>();
			if (!string.IsNullOrEmpty(m_resourceType) && !m_resourceType.Equals("image", StringComparison.Ordinal))
			{
				list.Add(m_resourceType);
			}
			if (!string.IsNullOrEmpty(m_type) && !m_type.Equals("upload", StringComparison.Ordinal))
			{
				list.Add(m_type);
			}
			string text = AdditionalParams();
			if (!string.IsNullOrEmpty(text))
			{
				list.Add(text);
			}
			if (!string.IsNullOrEmpty(m_publicId))
			{
				list.Add(FormattedPublicId());
			}
			return string.Join(":", list.ToArray());
		}

		public override object Clone()
		{
			return MemberwiseClone();
		}

		private string FormattedPublicId()
		{
			string text = m_publicId;
			if (!string.IsNullOrEmpty(m_format))
			{
				text = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", text, m_format);
			}
			return text;
		}
	}
}
