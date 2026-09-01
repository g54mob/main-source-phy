using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using InternalModding;
using InternalModding.Assemblies;
using InternalModding.Mods;
using UnityEngine;
using XMLTypes;

namespace Modding
{
	public class Configuration
	{
		private static readonly IDictionary<ModContainer, XDataHolder> dataHolders = new Dictionary<ModContainer, XDataHolder>();

		private static XDataHolder modLoaderConfiguration;

		public static XDataHolder GetData()
		{
			Assembly callingAssembly = Assembly.GetCallingAssembly();
			if (callingAssembly == Assembly.GetExecutingAssembly())
			{
				return modLoaderConfiguration;
			}
			ModContainer modByAssembly = AssemblyLoader.GetModByAssembly(callingAssembly);
			return GetData(modByAssembly);
		}

		internal static XDataHolder GetData(ModContainer mod)
		{
			if (mod == null)
			{
				throw new InvalidOperationException("Configuration.GetData called from an assembly not listed in the manifest.");
			}
			if (!dataHolders.ContainsKey(mod))
			{
				dataHolders.Add(mod, new XDataHolder());
			}
			return dataHolders[mod];
		}

		public static void Save()
		{
			Assembly callingAssembly = Assembly.GetCallingAssembly();
			if (callingAssembly == Assembly.GetExecutingAssembly())
			{
				Save(null);
				return;
			}
			ModContainer modByAssembly = AssemblyLoader.GetModByAssembly(callingAssembly);
			if (modByAssembly == null)
			{
				throw new InvalidOperationException("Configuration.Save called from an assembly not listed in the manifest.");
			}
			Save(modByAssembly);
		}

		internal static void LoadModLoader()
		{
			Load(null);
		}

		internal static void SaveAll()
		{
			foreach (ModContainer mod in ModManager.Mods)
			{
				Save(mod);
			}
			Save(null);
		}

		private static void Save(ModContainer mod)
		{
			bool flag = mod == null;
			if (!flag && !dataHolders.ContainsKey(mod))
			{
				return;
			}
			XDataHolder xDataHolder = ((!flag) ? dataHolders[mod] : modLoaderConfiguration);
			if (!xDataHolder.HasData)
			{
				return;
			}
			string path = GetPath(mod);
			try
			{
				using (FileStream stream = new FileStream(path, FileMode.Create))
				{
					using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stream, Encoding.UTF8))
					{
						xmlTextWriter.Formatting = Formatting.Indented;
						xmlTextWriter.Indentation = 4;
						xmlTextWriter.WriteStartDocument();
						xmlTextWriter.WriteStartElement("Configuration");
						xmlTextWriter.WriteAttributeString("version", "1");
						foreach (XData item in xDataHolder.ReadAll())
						{
							if (item.Key == null || item.Serialize().Length == 0)
							{
								continue;
							}
							xmlTextWriter.WriteStartElement(item.Type);
							xmlTextWriter.WriteAttributeString("key", item.Key.ToString(StaticSettings.Culture));
							XMLTypes.XAttribute[] array = item.Serialize();
							if (array.Length > 1)
							{
								XMLTypes.XAttribute[] array2 = array;
								foreach (XMLTypes.XAttribute xAttribute in array2)
								{
									xmlTextWriter.WriteElementString(xAttribute.Name.ToString(StaticSettings.Culture), xAttribute.Value.ToString(StaticSettings.Culture));
								}
							}
							else
							{
								xmlTextWriter.WriteString(array[0].Value.ToString(StaticSettings.Culture));
							}
							xmlTextWriter.WriteEndElement();
						}
						xmlTextWriter.WriteEndElement();
						xmlTextWriter.WriteEndDocument();
						xmlTextWriter.Flush();
					}
				}
			}
			catch (Exception exception)
			{
				Debug.LogError("Failed to save configuration for " + ((!flag) ? mod.Info.Name : "the mod loader"));
				Debug.LogException(exception);
			}
		}

		internal static void Load(ModContainer mod)
		{
			bool flag = mod == null;
			string path = GetPath(mod);
			try
			{
				XDataHolder xDataHolder = new XDataHolder();
				XDocument xDocument = XDocument.Load(path);
				foreach (XElement item in xDocument.Element("Configuration").Elements())
				{
					List<XElement> list = item.Elements().ToList();
					XMLTypes.XAttribute[] array = new XMLTypes.XAttribute[list.Count];
					if (list.Count > 0)
					{
						for (int i = 0; i < list.Count; i++)
						{
							XElement xElement = list[i];
							array[i] = new XMLTypes.XAttribute(xElement.Name.LocalName.ToString(StaticSettings.Culture), xElement.Value.ToString(StaticSettings.Culture));
						}
					}
					else
					{
						array = XMLTypes.XAttribute.Single(item.Value);
					}
					xDataHolder.Write(XDataUtil.CreateXData(item.Name.LocalName, (string)item.Attribute("key"), array));
				}
				if (flag)
				{
					modLoaderConfiguration = xDataHolder;
				}
				else
				{
					dataHolders.Add(mod, xDataHolder);
				}
			}
			catch (FileNotFoundException)
			{
			}
			catch (DirectoryNotFoundException)
			{
			}
			catch (Exception exception)
			{
				Debug.LogError("Failed to load configuration for " + ((!flag) ? mod.Info.Name : "the mod loader"));
				Debug.LogException(exception);
			}
			if (flag && modLoaderConfiguration == null)
			{
				modLoaderConfiguration = new XDataHolder();
			}
		}

		internal static string GetPath(ModContainer mod)
		{
			if (mod == null)
			{
				return GetPath(null, null);
			}
			return GetPath(mod.Info.Name, mod.Info.Id.ToString());
		}

		internal static string GetPath(string name, string id)
		{
			string text = Path.Combine(StaticSettings.DataPath, "Mods/Config/");
			if (!Directory.Exists(text))
			{
				try
				{
					Directory.CreateDirectory(text);
				}
				catch
				{
					Debug.LogError("Missing Read/Write permission");
				}
			}
			if (name == null)
			{
				return Path.Combine(text, "Modding.xml");
			}
			return Path.Combine(text, name.Replace(" ", string.Empty).Replace("_", string.Empty) + "_" + id + ".xml");
		}
	}
}
