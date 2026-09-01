using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;

[AddComponentMenu("XML/Skin XML Loader")]
public static class SkinXmlLoader
{
	public static BlockSkinLoader.SkinPack.Config Load(string path)
	{
		string path2 = Path.Combine(path, "Config.xml");
		if (File.Exists(path2))
		{
			return LoadFromFullPath(path2);
		}
		return new BlockSkinLoader.SkinPack.Config();
	}

	public static BlockSkinLoader.SkinPack.Config LoadFromXmlDocument(XDocument xml)
	{
		try
		{
			BlockSkinLoader.SkinPack.Config config = new BlockSkinLoader.SkinPack.Config();
			if (xml.Elements("Config").Any())
			{
				XElement xElement = xml.Element("Config");
				if (xElement.Attributes("version").Any())
				{
					config.version = (int)xElement.Attribute("version");
				}
				if (xElement.Elements("UseSingleTexture").Any())
				{
					config.useSingleTexture = (bool)xElement.Element("UseSingleTexture");
				}
				if (xElement.Elements("AllowTiling").Any())
				{
					config.allowTiling = (bool)xElement.Element("AllowTiling");
				}
				if (xElement.Elements("DisplayBlock").Any())
				{
					config.displayBlock = (int)xElement.Element("DisplayBlock");
				}
			}
			return config;
		}
		catch (XmlException exception)
		{
			Debug.LogException(exception);
			throw new FileLoadException("SkinPack Config's layout is invalid. Likely corrupted or manipulated.");
		}
		catch (NullReferenceException exception2)
		{
			Debug.LogException(exception2);
			throw new FileLoadException("SkinPack Config does not contain all required elements. Likely corrupted or manipulated.");
		}
		catch (ArgumentNullException exception3)
		{
			Debug.LogException(exception3);
			throw new FileLoadException("SkinPack Config does not contain all required attributes. Likely corrupted or manipulated.");
		}
	}

	public static BlockSkinLoader.SkinPack.Config LoadFromFullPath(string path)
	{
		try
		{
			XDocument xml = XDocument.Load(path);
			return LoadFromXmlDocument(xml);
		}
		catch (XmlException exception)
		{
			Debug.LogException(exception);
			throw new FileLoadException("SkinPack Config's layout is invalid. Likely corrupted or manipulated.");
		}
		catch (NullReferenceException exception2)
		{
			Debug.LogException(exception2);
			throw new FileLoadException("SkinPack Config does not contain all required elements. Likely corrupted or manipulated.");
		}
		catch (ArgumentNullException exception3)
		{
			Debug.LogException(exception3);
			throw new FileLoadException("SkinPack Config does not contain all required attributes. Likely corrupted or manipulated.");
		}
		catch (Exception exception4)
		{
			Debug.LogException(exception4);
			throw new FileLoadException("Uncaught exception while loading the machine file.");
		}
	}
}
