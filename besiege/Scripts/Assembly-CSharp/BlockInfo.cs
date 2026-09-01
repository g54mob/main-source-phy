using System;
using InternalModding.Blocks;
using InternalModding.Loading;
using UnityEngine;

public class BlockInfo
{
	private static Vector3 posHolder = default(Vector3);

	private static Quaternion rotHolder = default(Quaternion);

	private static Vector3 scaleHolder = default(Vector3);

	public Guid Guid { get; set; }

	public BlockType ID { get; set; }

	public Vector3 Position { get; set; }

	public Quaternion Rotation { get; set; }

	public Vector3 Scale { get; set; }

	public XDataHolder BlockData { get; set; }

	public BlockSkinLoader.SkinPack.Skin Skin { get; set; }

	public bool Flipped { get; set; }

	public int EncodedSize { get; set; }

	public bool HasSimData { get; private set; }

	public BlockInfo(BlockInfo info)
	{
		Guid = info.Guid;
		ID = info.ID;
		Position = info.Position;
		Rotation = info.Rotation;
		Scale = info.Scale;
		BlockData = info.BlockData.Clone();
		Skin = info.Skin;
		Flipped = info.Flipped;
	}

	public BlockInfo()
	{
		Guid = Guid.NewGuid();
		ID = BlockType.StartingBlock;
		Position = Vector3.zero;
		Rotation = Quaternion.identity;
		Scale = Vector3.one;
		Skin = BlockSkinLoader.SkinPack.Skin.GetEmpty();
		BlockData = new XDataHolder();
		Flipped = false;
	}

	public BlockInfo(Guid guid, BlockType id, Vector3 pos, Quaternion rot, Vector3 scale, BlockSkinLoader.SkinPack.Skin skin, bool flipped, XDataHolder data)
	{
		Guid = guid;
		ID = id;
		Position = pos;
		Rotation = rot;
		Scale = scale;
		BlockData = data;
		Skin = skin;
		Flipped = flipped;
	}

	public static BlockInfo FromBlockBehaviour(BlockBehaviour block)
	{
		BlockInfo blockInfo = new BlockInfo();
		blockInfo.Guid = block.Guid;
		blockInfo.ID = block.Prefab.Type;
		blockInfo.Flipped = block.Flipped;
		BlockInfo blockInfo2 = blockInfo;
		Vector3 originalPos;
		Quaternion originalRot;
		Vector3 originalScale;
		if (StatMaster.advancedBuilding && StatMaster.Mode.currentBlockTool != null && StatMaster.Mode.currentBlockTool.GetBlockInfo(block, out originalPos, out originalRot, out originalScale))
		{
			blockInfo2.Position = originalPos;
			blockInfo2.Rotation = originalRot;
			blockInfo2.Scale = originalScale;
		}
		else
		{
			blockInfo2.Position = block.Position;
			blockInfo2.Rotation = block.Rotation;
			blockInfo2.Scale = block.Scale;
		}
		XDataHolder xDataHolder;
		if (block.hasLastState)
		{
			xDataHolder = block.LastState.Clone();
		}
		else
		{
			xDataHolder = new XDataHolder();
			block.OnSave(xDataHolder);
		}
		blockInfo2.BlockData = xDataHolder;
		blockInfo2.Skin = block.VisualController.selectedSkin ?? block.Prefab.DefaultSkin;
		return blockInfo2;
	}

	public static byte[] Encode(BlockBehaviour block, bool includeSimState)
	{
		Transform transform = block.transform;
		XDataHolder xDataHolder;
		if (!block.hasLastState)
		{
			xDataHolder = new XDataHolder();
			block.OnSave(xDataHolder);
		}
		else
		{
			xDataHolder = block.LastState;
		}
		return Encode(block.Prefab.ID, block.Guid, transform.localPosition, transform.localRotation, transform.localScale, block.VisualController.selectedSkin, block.Flipped, xDataHolder, includeSimState, block, block.SimBlock);
	}

	public byte[] Encode()
	{
		return Encode((int)ID, Guid, Position, Rotation, Scale, Skin, Flipped, BlockData, false, null, null);
	}

	public static byte[] Encode(int id, Guid guid, Vector3 pos, Quaternion rot, Vector3 scale, BlockSkinLoader.SkinPack.Skin skin, bool flipped, XDataHolder saveData, bool includeSim, BlockBehaviour block, BlockBehaviour simBlock)
	{
		int num = 0;
		ModdedBlock blockByEffectiveId = ModIds.GetBlockByEffectiveId(id);
		if (blockByEffectiveId != null)
		{
			saveData.Write("modding-internal-guid", blockByEffectiveId.Info.Mod.Info.Id.ToString());
			saveData.Write("modding-internal-id", blockByEffectiveId.LocalId);
		}
		byte[] outData;
		bool flag = saveData.Encode(out outData);
		NetworkBlock networkBlock = null;
		int num2 = 0;
		byte[] array = guid.ToByteArray();
		byte[] array2 = null;
		bool flag2 = skin != null && !skin.isDefault && skin.pack != null;
		bool flag3 = includeSim && block.hasSimBlock;
		if (flag2)
		{
			array2 = skin.pack.Encode();
		}
		if (flag3)
		{
			networkBlock = simBlock.NetBlock;
			num2 = networkBlock.GetDataSize();
		}
		int num3 = NetworkCompression.PackedUIntLength(id, true);
		byte[] array3 = new byte[num3 + 1 + array.Length + 24 + 16 + (flag ? outData.Length : 0) + (flag2 ? array2.Length : 0) + (flag3 ? num2 : 0)];
		NetworkCompression.PackUInt(id, array3, num, true, num3);
		num += num3;
		array3[num] = (byte)((flag2 ? 1 : 0) | (flipped ? 2 : 0) | (flag ? 4 : 0) | (flag3 ? 8 : 0));
		num++;
		Buffer.BlockCopy(array, 0, array3, num, array.Length);
		num += array.Length;
		NetworkCompression.PackVector(pos, array3, num);
		num += 12;
		NetworkCompression.PackQuaternion(rot, array3, num);
		num += 16;
		NetworkCompression.PackVector(scale, array3, num);
		num += 12;
		if (flag)
		{
			Buffer.BlockCopy(outData, 0, array3, num, outData.Length);
			num += outData.Length;
		}
		if (flag2)
		{
			Buffer.BlockCopy(array2, 0, array3, num, array2.Length);
			num += array2.Length;
		}
		if (flag3)
		{
			networkBlock.EncodeState(array3, num);
			num += num2;
		}
		return array3;
	}

	public static BlockInfo Decode(ushort index, byte[] data, int offset)
	{
		int num = offset;
		int count;
		int num2 = NetworkCompression.UnpackUInt(data, offset, true, out count);
		BlockType blockType = (BlockType)count;
		offset += num2;
		int num3 = data[offset++];
		byte[] array = new byte[16];
		Buffer.BlockCopy(data, offset, array, 0, array.Length);
		Guid guid = new Guid(array);
		offset += array.Length;
		NetworkCompression.UnpackVector(data, offset, out posHolder);
		offset += 12;
		NetworkCompression.UnpackQuaternion(data, offset, out rotHolder);
		offset += 16;
		NetworkCompression.UnpackVector(data, offset, out scaleHolder);
		offset += 12;
		bool flag = (num3 & 1) != 0;
		bool flipped = (num3 & 2) != 0;
		bool flag2 = (num3 & 4) != 0;
		bool hasSimData = (num3 & 8) != 0;
		XDataHolder xDataHolder = new XDataHolder();
		if (flag2)
		{
			int num4 = xDataHolder.Decode(data, offset);
			offset += num4;
		}
		if (xDataHolder.HasKey("modding-internal-guid"))
		{
			Guid modId = new Guid(xDataHolder.ReadString("modding-internal-guid"));
			int localId = xDataHolder.ReadInt("modding-internal-id");
			blockType = (BlockType)ModIds.GetEffectiveBlockId(modId, localId);
		}
		BlockSkinLoader.SkinPack.Skin skin = null;
		if (flag)
		{
			offset += BlockSkinLoader.SkinPack.Skin.Decode(data, offset, out skin);
		}
		else
		{
			int num5 = (int)blockType;
			BlockPrefab value;
			if (PrefabMaster.BlockPrefabs.TryGetValue(num5, out value))
			{
				skin = value.DefaultSkin;
			}
			else
			{
				Debug.LogError("Couldn't fetch skin for unknown block type " + num5 + "!");
			}
		}
		BlockInfo blockInfo = new BlockInfo(guid, blockType, posHolder, rotHolder, scaleHolder, skin, flipped, xDataHolder);
		blockInfo.HasSimData = hasSimData;
		blockInfo.EncodedSize = offset - num;
		return blockInfo;
	}
}
