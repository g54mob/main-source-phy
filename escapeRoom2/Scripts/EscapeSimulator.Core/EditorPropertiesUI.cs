using UnityEngine;
using UnityEngine.UI;

public class EditorPropertiesUI : PineUIComponent
{
	public Canvas root;

	public Image Properties;

	public Image Background;

	public RectTransform Content;

	public HeaderPropertyUI HeaderProperty;

	public IntPropertyUI IntProperty;

	public FloatPropertyUI FloatProperty;

	public Vector2PropertyUI Vector2Property;

	public Vector3PropertyUI Vector3Property;

	public StringPropertyUI StringProperty;

	public TogglePropertyUI BoolProperty;

	public TexturePropertyUI TextureProperty;

	public ColorPropertyUI ColorProperty;

	public EnumPropertyUI EnumProperty;

	public PropertiesSeparatorUI SeparatorProperty;

	public FoldoutPropertyUI FoldoutProperty;

	public ButtonPropertyUI ButtonProperty;

	public HintPropertyUI HintProperty;

	public TargetPropertyUI TargetProperty;

	public TexArrayPropertyUI TexArrayProperty;

	public PasswordPropertyUI PasswordProperty;

	public PasswordHintPropertyUI PasswordHintProperty;

	public TargetArrayPropertyUI TargetArrayProperty;

	public PaintPropertyUI PaintProperty;

	public InteractionsPropertyUI InteractionsProperty;

	public SoundPropertyUI SoundProperty;

	public FloatRangePropertyUI FloatRangeProperty;

	public SkyTexturePropertyUI SkyTextureProperty;

	public StringButtonPropertyUI StringButtonProperty;

	public EnumButtonPropertyUI EnumButtonProperty;

	public MultiEnumPropertyUI MultiEnumProperty;

	public Image Scrollbar;

	public Image Header;

	public Text Header_InstanceID;

	public Text Header_Text;

	public Button Header_PropLink;

	public Image PropertiesHide;

	public object data;

	private bool isInitialized;

	protected override void Awake()
	{
		init();
	}

	public void init()
	{
		if (!isInitialized)
		{
			isInitialized = true;
			PineUI.addButtonListeners(Header_PropLink);
		}
	}
}
