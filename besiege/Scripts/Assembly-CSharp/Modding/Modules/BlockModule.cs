using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Modding.Serialization;

namespace Modding.Modules
{
	public abstract class BlockModule : Element, IReloadable
	{
		[XmlIgnore]
		internal string Guid;

		[DefaultValue(null)]
		[XmlAttribute("modid")]
		public Guid ModId;

		public virtual void OnReload(IReloadable newModule)
		{
		}

		public virtual void PreprocessForReloading()
		{
		}
	}
}
