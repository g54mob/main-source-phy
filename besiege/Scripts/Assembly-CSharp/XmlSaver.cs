using System.IO;
using System.Xml;
using InternalModding.Blocks;
using InternalModding.Loading;
using UnityEngine;
using XMLTypes;

[AddComponentMenu("XML/XML Saver")]
public class XmlSaver
{
	public static event OnSaveHandler OnSave;

	public static void Save(MachineInfo machine, string directory)
	{
		OnSaveHandler onSave = XmlSaver.OnSave;
		if (onSave != null)
		{
			onSave(machine);
		}
		string path = StaticSettings.SanatizeFileName(machine.Name) + ".bsg";
		Directory.CreateDirectory(directory);
		using (FileStream stream = new FileStream(Path.Combine(directory, path), FileMode.Create))
		{
			using (StreamWriter writer = new StreamWriter(stream))
			{
				using (XmlTextWriter xmlTextWriter = new XmlTextWriter(writer))
				{
					xmlTextWriter.Formatting = Formatting.Indented;
					xmlTextWriter.Indentation = 4;
					xmlTextWriter.WriteStartDocument();
					xmlTextWriter.WriteComment("Besiege machine save file.");
					xmlTextWriter.WriteStartElement("Machine");
					xmlTextWriter.WriteAttributeString("version", "1");
					xmlTextWriter.WriteAttributeString("bsgVersion", "1.4");
					xmlTextWriter.WriteAttributeString("name", machine.Name.ToString(StaticSettings.Culture));
					if (!string.IsNullOrEmpty(machine.Author))
					{
						xmlTextWriter.WriteAttributeString("Auth", machine.Author.ToString(StaticSettings.Culture));
					}
					xmlTextWriter.WriteComment("The machine's position and rotation.");
					xmlTextWriter.WriteStartElement("Global");
					xmlTextWriter.WriteStartElement("Position");
					xmlTextWriter.WriteAttributeString("x", machine.Position.x.ToString(StaticSettings.Culture));
					xmlTextWriter.WriteAttributeString("y", machine.Position.y.ToString(StaticSettings.Culture));
					xmlTextWriter.WriteAttributeString("z", machine.Position.z.ToString(StaticSettings.Culture));
					xmlTextWriter.WriteEndElement();
					xmlTextWriter.WriteStartElement("Rotation");
					xmlTextWriter.WriteAttributeString("x", machine.Rotation.x.ToString(StaticSettings.Culture));
					xmlTextWriter.WriteAttributeString("y", machine.Rotation.y.ToString(StaticSettings.Culture));
					xmlTextWriter.WriteAttributeString("z", machine.Rotation.z.ToString(StaticSettings.Culture));
					xmlTextWriter.WriteAttributeString("w", machine.Rotation.w.ToString(StaticSettings.Culture));
					xmlTextWriter.WriteEndElement();
					xmlTextWriter.WriteEndElement();
					xmlTextWriter.WriteComment("The machine's additional data or modded data.");
					xmlTextWriter.WriteStartElement("Data");
					foreach (XData item in machine.MachineData.ReadAll())
					{
						xmlTextWriter.WriteStartElement(item.Type);
						xmlTextWriter.WriteAttributeString("key", item.Key.ToString(StaticSettings.Culture));
						XAttribute[] array = item.Serialize();
						if (array.Length > 1)
						{
							XAttribute[] array2 = array;
							foreach (XAttribute xAttribute in array2)
							{
								xmlTextWriter.WriteElementString(xAttribute.Name.ToString(StaticSettings.Culture), xAttribute.Value.ToString(StaticSettings.Culture));
							}
						}
						else if (array.Length > 0)
						{
							xmlTextWriter.WriteString(array[0].Value.ToString(StaticSettings.Culture));
						}
						xmlTextWriter.WriteEndElement();
					}
					xmlTextWriter.WriteEndElement();
					xmlTextWriter.WriteComment("The machine's blocks.");
					xmlTextWriter.WriteStartElement("Blocks");
					foreach (BlockInfo block in machine.Blocks)
					{
						xmlTextWriter.WriteStartElement("Block");
						int iD = (int)block.ID;
						xmlTextWriter.WriteAttributeString("id", iD.ToString(StaticSettings.Culture));
						xmlTextWriter.WriteAttributeString("guid", block.Guid.ToString());
						if (SingleInstanceFindOnly<BlockLoader>.Instance.IsModBlock(iD))
						{
							ModdedBlock blockByEffectiveId = ModIds.GetBlockByEffectiveId(iD);
							xmlTextWriter.WriteAttributeString("modId", blockByEffectiveId.Info.Mod.Info.Id.ToString());
							xmlTextWriter.WriteAttributeString("localId", blockByEffectiveId.LocalId.ToString(StaticSettings.Culture));
							if (blockByEffectiveId.Fallback != null)
							{
								xmlTextWriter.WriteAttributeString("fallback", ((int)blockByEffectiveId.Fallback.Get()).ToString());
							}
						}
						xmlTextWriter.WriteStartElement("Transform");
						xmlTextWriter.WriteStartElement("Position");
						xmlTextWriter.WriteAttributeString("x", block.Position.x.ToString(StaticSettings.Culture));
						xmlTextWriter.WriteAttributeString("y", block.Position.y.ToString(StaticSettings.Culture));
						xmlTextWriter.WriteAttributeString("z", block.Position.z.ToString(StaticSettings.Culture));
						xmlTextWriter.WriteEndElement();
						xmlTextWriter.WriteStartElement("Rotation");
						xmlTextWriter.WriteAttributeString("x", block.Rotation.x.ToString(StaticSettings.Culture));
						xmlTextWriter.WriteAttributeString("y", block.Rotation.y.ToString(StaticSettings.Culture));
						xmlTextWriter.WriteAttributeString("z", block.Rotation.z.ToString(StaticSettings.Culture));
						xmlTextWriter.WriteAttributeString("w", block.Rotation.w.ToString(StaticSettings.Culture));
						xmlTextWriter.WriteEndElement();
						xmlTextWriter.WriteStartElement("Scale");
						xmlTextWriter.WriteAttributeString("x", block.Scale.x.ToString(StaticSettings.Culture));
						xmlTextWriter.WriteAttributeString("y", block.Scale.y.ToString(StaticSettings.Culture));
						xmlTextWriter.WriteAttributeString("z", block.Scale.z.ToString(StaticSettings.Culture));
						xmlTextWriter.WriteEndElement();
						xmlTextWriter.WriteEndElement();
						if (block.Skin != null && !block.Skin.isDefault)
						{
							xmlTextWriter.WriteStartElement("Settings");
							if (block.Skin != null && block.Skin.pack != null)
							{
								xmlTextWriter.WriteStartElement("Skin");
								xmlTextWriter.WriteAttributeString("name", block.Skin.pack.name.ToString(StaticSettings.Culture));
								xmlTextWriter.WriteAttributeString("id", block.Skin.pack.id.ToString(StaticSettings.Culture));
								xmlTextWriter.WriteEndElement();
							}
							else
							{
								xmlTextWriter.WriteStartElement("Skin");
								xmlTextWriter.WriteAttributeString("name", BlockSkinLoader.defaultPack.name.ToString(StaticSettings.Culture));
								xmlTextWriter.WriteAttributeString("id", BlockSkinLoader.defaultPack.id.ToString(StaticSettings.Culture));
								xmlTextWriter.WriteEndElement();
							}
							xmlTextWriter.WriteEndElement();
						}
						xmlTextWriter.WriteStartElement("Data");
						foreach (XData item2 in block.BlockData.ReadAll())
						{
							XAttribute[] array3 = item2.Serialize();
							if (array3.Length <= 0)
							{
								continue;
							}
							xmlTextWriter.WriteStartElement(item2.Type);
							xmlTextWriter.WriteAttributeString("key", item2.Key.ToString(StaticSettings.Culture));
							if (array3.Length > 1)
							{
								XAttribute[] array4 = array3;
								foreach (XAttribute xAttribute2 in array4)
								{
									xmlTextWriter.WriteElementString(xAttribute2.Name.ToString(StaticSettings.Culture), xAttribute2.Value.ToString(StaticSettings.Culture));
								}
							}
							else
							{
								xmlTextWriter.WriteString(array3[0].Value.ToString(StaticSettings.Culture));
							}
							xmlTextWriter.WriteEndElement();
						}
						xmlTextWriter.WriteEndElement();
						xmlTextWriter.WriteEndElement();
					}
					xmlTextWriter.WriteEndElement();
					xmlTextWriter.WriteEndElement();
					xmlTextWriter.WriteEndDocument();
					xmlTextWriter.Flush();
					xmlTextWriter.Close();
				}
			}
		}
	}

	public static bool IsXmlFormat(string path)
	{
		string text;
		using (StreamReader streamReader = new StreamReader(path))
		{
			text = streamReader.ReadLine() ?? string.Empty;
		}
		return text.ToLower().Contains("xml");
	}

	public static bool IsBsgFormat(string path)
	{
		string text;
		using (StreamReader streamReader = new StreamReader(path))
		{
			text = streamReader.ReadLine() ?? string.Empty;
		}
		return text.ToLower().Contains("prefab ids");
	}
}
