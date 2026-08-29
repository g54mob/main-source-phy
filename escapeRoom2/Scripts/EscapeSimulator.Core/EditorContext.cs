using System.Collections.Generic;
using UnityEngine;

public class EditorContext
{
	public LevelContainerEditor container;

	public Dictionary<InstanceID, PropInstance> propInstances;

	public string roomDirPath;

	public List<LoadTextureOp> loadTexturesOps;

	public List<StaggeredTextureLoadInfo> staggeredTextureLoad;

	public UnpackedCustomRoom customRoom;

	public Dictionary<string, long> soundPointersCache;

	public List<AssetBundle> assetsBundles;

	public AssetBundle assetsIconsBundle;

	public List<AssetRequest> assetsRequests;

	public List<CustomModelObject> customModels;

	public List<CustomModelRequest> loadingCustomModels;

	public Texture materialMissingTexture;

	public EditorVolumeRefs editorVolumeRefs;

	public bool useNonLegacyLights;

	public bool hidePlayerNameplates;

	public bool hideItemNameplates;

	public bool useProximityChat;

	public bool useSkyboxPreview;

	public bool usePostProcessingPreview;

	public bool useWaterPreview;

	public RefreshFlags refresh;
}
