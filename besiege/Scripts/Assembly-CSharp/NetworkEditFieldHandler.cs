using System;
using System.Collections.Generic;
using Mono.CSharp;
using UnityEngine;

public class NetworkEditFieldHandler : EditFieldHandler
{
	private NetworkAuxAddPiece auxAddPiece;

	protected void Start()
	{
		EditFieldHandler.Instance = this;
		auxAddPiece = NetworkAuxAddPiece.Instance;
	}

	public override void OnCloseMapper()
	{
		if (!StatMaster.isClient)
		{
			return;
		}
		foreach (MapperType mapperType in BlockMapper.CurrentInstance.Current.MapperTypes)
		{
			mapperType.ResetValue();
		}
	}

	public override void OnCloseOverviewMapper()
	{
		if (!StatMaster.isClient)
		{
			return;
		}
		List<InputGroup> inputGroups = OverviewBlockMapper.CurrentInstance.inputGroups;
		for (int i = 0; i < inputGroups.Count; i++)
		{
			InputGroup inputGroup = inputGroups[i];
			for (int j = 0; j < inputGroup.blockList.Count; j++)
			{
				InputGroup.BlockEntry blockEntry = inputGroup.blockList[j];
				if (blockEntry == null)
				{
					continue;
				}
				BlockBehaviour block = blockEntry.block;
				if (!(block != null))
				{
					continue;
				}
				foreach (MapperType mapperType in block.MapperTypes)
				{
					mapperType.ResetValue();
				}
			}
		}
	}

	public override void OnReset()
	{
		BlockMapper currentInstance = BlockMapper.CurrentInstance;
		if (currentInstance.IsBlock)
		{
			List<BlockBehaviour> machineSelection = AdvancedBlockEditor.Instance.selectionController.MachineSelection;
			if (!machineSelection.Contains(currentInstance.Block))
			{
				machineSelection.Add(currentInstance.Block);
			}
			int num = NetworkCompression.PackedUIntLength(machineSelection.Count, false);
			byte[] array = new byte[1 + num + machineSelection.Count * 4];
			int num2 = 0;
			array[num2++] = (byte)((OptionsMaster.skinsEnabled && !StatMaster.collapseSkinMapper) ? 1u : 0u);
			NetworkCompression.PackUInt(machineSelection.Count, array, num2, false, num);
			num2 += num;
			for (int i = 0; i < machineSelection.Count; i++)
			{
				NetworkCompression.WriteUInt((ushort)machineSelection[i].BuildIndex, false, array, num2);
				num2 += 4;
			}
			auxAddPiece.SendFragmentedServerMessage(RPCMessageType.MapperResetBlock, CLZF2.Compress(array));
		}
		else if (currentInstance.IsEntity)
		{
			GenericEntity entity = currentInstance.Entity;
			auxAddPiece.SendServerMessage(RPCMessageType.MapperResetEntity, entity.GetIdentifierBytes());
		}
		else
		{
			XDataHolder xDataHolder = currentInstance.Current.InitialState.Clone();
			xDataHolder.EraseCustomBlockData();
			currentInstance.Current.OnLoad(currentInstance.Current.InitialState);
			currentInstance.Current.OnReset();
			currentInstance.Refresh();
		}
	}

	public override void OnPaste(BlockBehaviour block, CopyMode mode)
	{
		List<BlockBehaviour> machineSelection = AdvancedBlockEditor.Instance.selectionController.MachineSelection;
		if (!machineSelection.Contains(BlockMapper.CurrentInstance.Block))
		{
			machineSelection.Add(BlockMapper.CurrentInstance.Block);
		}
		int num = NetworkCompression.PackedUIntLength(machineSelection.Count, false);
		byte[] array = new byte[6 + num + machineSelection.Count * 4];
		int num2 = 0;
		array[num2++] = (byte)mode;
		array[num2++] = (byte)((OptionsMaster.skinsEnabled && !StatMaster.collapseSkinMapper) ? 1u : 0u);
		NetworkCompression.WriteUInt((ushort)block.BuildIndex, false, array, num2);
		num2 += 4;
		NetworkCompression.PackUInt(machineSelection.Count, array, num2, false, num);
		num2 += num;
		for (int i = 0; i < machineSelection.Count; i++)
		{
			NetworkCompression.WriteUInt((ushort)machineSelection[i].BuildIndex, false, array, num2);
			num2 += 4;
		}
		auxAddPiece.SendFragmentedServerMessage(RPCMessageType.MapperPasteBlock, CLZF2.Compress(array));
	}

	public override void OnPaste(GenericEntity sourceEntity, CopyMode mode)
	{
		GenericEntity entity = BlockMapper.CurrentInstance.Entity;
		byte[] array = new byte[LevelEntity.ID_LENGTH * 2 + 1];
		int num = 0;
		int iD_LENGTH = LevelEntity.ID_LENGTH;
		Buffer.BlockCopy(entity.GetIdentifierBytes(), 0, array, num, iD_LENGTH);
		num += LevelEntity.ID_LENGTH;
		Buffer.BlockCopy(sourceEntity.GetIdentifierBytes(), 0, array, num, iD_LENGTH);
		num += LevelEntity.ID_LENGTH;
		array[num] = (byte)mode;
		auxAddPiece.SendServerMessage(RPCMessageType.MapperPasteEntity, array);
	}

	public void OnEditEntityState(GenericEntity genEntity, bool isUndo, XDataHolder data, CopyMode mode)
	{
		byte[] identifierBytes = genEntity.GetIdentifierBytes();
		byte[] outData;
		bool flag = data.Encode(out outData);
		byte[] array = new byte[identifierBytes.Length + 2 + (flag ? outData.Length : 0)];
		int num = 0;
		Buffer.BlockCopy(identifierBytes, 0, array, num, LevelEntity.ID_LENGTH);
		num += identifierBytes.Length;
		array[num] = (byte)((isUndo ? 1 : 0) | (flag ? 2 : 0));
		num++;
		array[num] = (byte)mode;
		num++;
		if (flag)
		{
			Buffer.BlockCopy(outData, 0, array, num, outData.Length);
		}
		auxAddPiece.SendServerMessage(RPCMessageType.UpdateEntityState, array);
	}

	public void OnEditBlockState(BlockBehaviour block, bool isUndo, XDataHolder data, BlockSkinLoader.SkinPack.Skin skin)
	{
		byte[] outData;
		bool flag = data.Encode(out outData);
		byte[] array = null;
		bool flag2 = skin != null && !skin.isDefault;
		if (flag2)
		{
			array = skin.pack.Encode();
			flag2 = true;
		}
		int num = NetworkCompression.PackedUIntLength(block.BuildIndex, true);
		byte[] array2 = new byte[num + 1 + (flag ? outData.Length : 0) + (flag2 ? array.Length : 0)];
		int num2 = 0;
		NetworkCompression.PackUInt(block.BuildIndex, array2, num2, true, num);
		num2 += num;
		array2[num2] = (byte)((isUndo ? 1 : 0) | (flag ? 2 : 0) | (flag2 ? 4 : 0));
		num2++;
		if (flag)
		{
			Buffer.BlockCopy(outData, 0, array2, num2, outData.Length);
			num2 += outData.Length;
		}
		if (flag2)
		{
			Buffer.BlockCopy(array, 0, array2, num2, array.Length);
		}
		auxAddPiece.SendServerMessage(RPCMessageType.UpdateBlockState, array2);
	}

	public void OnEditEntityField(GenericEntity genEntity, XData field, bool isUndo)
	{
		byte[] array = XDataHolder.EncodeXData(field);
		int num = 0;
		byte[] array2 = new byte[1 + LevelEntity.ID_LENGTH + array.Length];
		Buffer.BlockCopy(genEntity.GetIdentifierBytes(), 0, array2, 0, LevelEntity.ID_LENGTH);
		num += LevelEntity.ID_LENGTH;
		array2[num] = (byte)(isUndo ? 1u : 0u);
		num++;
		Buffer.BlockCopy(array, 0, array2, num, array.Length);
		auxAddPiece.SendServerMessage(RPCMessageType.EditEntity, array2);
	}

	public void OnEditBlockField(List<Tuple<BlockBehaviour, XData>> blockData, bool sameData, bool isUndo)
	{
		List<byte[]> list = new List<byte[]>();
		int num = 0;
		for (int i = 0; i < (sameData ? 1 : blockData.Count); i++)
		{
			XData xData = blockData[i].Item2;
			if (xData == null)
			{
				XString xString = new XString(string.Empty);
				xString.Value = string.Empty;
				xData = xString;
			}
			byte[] array = XDataHolder.EncodeXData(xData);
			list.Add(array);
			num += array.Length;
		}
		int num2 = NetworkCompression.PackedUIntLength(blockData.Count, false);
		byte[] array2 = new byte[1 + num2 + blockData.Count * 4 + num];
		int num3 = 0;
		array2[num3++] = (byte)((sameData ? 1 : 0) | (isUndo ? 2 : 0));
		NetworkCompression.PackUInt(blockData.Count, array2, num3, false, num2);
		num3 += num2;
		for (int i = 0; i < blockData.Count; i++)
		{
			NetworkCompression.WriteUInt((uint)blockData[i].Item1.BuildIndex, false, array2, num3);
			num3 += 4;
			if (!sameData)
			{
				byte[] array3 = list[i];
				Buffer.BlockCopy(array3, 0, array2, num3, array3.Length);
				num3 += array3.Length;
			}
		}
		if (sameData)
		{
			Buffer.BlockCopy(list[0], 0, array2, num3, num);
		}
		byte[] messageData = CLZF2.Compress(array2);
		auxAddPiece.SendFragmentedServerMessage(RPCMessageType.EditBlock, messageData);
	}

	public override void OnEditField(SaveableDataHolder dataHolder, MapperType mapperType)
	{
		XData xData = mapperType.Serialize();
		if (dataHolder is BlockBehaviour)
		{
			List<BlockBehaviour> machineSelection = AdvancedBlockEditor.Instance.selectionController.MachineSelection;
			if (!machineSelection.Contains(dataHolder as BlockBehaviour))
			{
				machineSelection.Add(dataHolder as BlockBehaviour);
			}
			List<Tuple<BlockBehaviour, XData>> editData = new List<Tuple<BlockBehaviour, XData>>();
			machineSelection.ForEach(delegate(BlockBehaviour x)
			{
				editData.Add(new Tuple<BlockBehaviour, XData>(x, xData));
			});
			OnEditBlockField(editData, true, false);
		}
		else if (dataHolder is GenericEntity)
		{
			OnEditEntityField(dataHolder as GenericEntity, xData, false);
		}
		else if (dataHolder is GenericDataHolder)
		{
			mapperType.ApplyValue();
		}
		else
		{
			Debug.LogError(string.Concat("Unknown target: ", dataHolder, "!"));
		}
	}
}
