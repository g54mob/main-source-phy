using InGameTextEditor;
using UnityEngine;
using UnityEngine.UI;

public class TextEditorUI : PineUIComponent
{
	public Canvas root;

	public InGameTextEditor.TextEditor Editor;

	public Image Editor_MainPanel;

	public Image Editor_MainPanel_Content_Selection_Caret;

	public Text Editor_MainPanel_Content_Text_MainText;

	public Image Editor_LineNumberPanel;

	public Text Editor_LineNumberPanel_Content_LineNumberText;

	public Image Editor_LineLabelIconsPanel;

	public Image Editor_VerticalScrollbar;

	public Image Editor_VerticalScrollbar_SlidingArea_Handle;

	public Image Editor_HorizontalScrollbar;

	public Image Editor_HorizontalScrollbar_SlidingArea_Handle;

	public Image Editor_Tooltip;

	public Text Editor_Tooltip_Text;

	public Image Editor_LockMask;

	public LuaFilesUI LuaFiles;

	public Image Buttons;

	public Button Buttons_Copy;

	public Text Buttons_Copy_Text;

	public Button Buttons_Paste;

	public Text Buttons_Paste_Text;

	public Button Buttons_Cut;

	public Text Buttons_Cut_Text;

	public Button Buttons_SelectAll;

	public Text Buttons_SelectAll_Text;

	public Button Buttons_Undo;

	public Text Buttons_Undo_Text;

	public Button Buttons_Redo;

	public Text Buttons_Redo_Text;

	public Button Buttons_Close;

	public Text Buttons_Close_Text;

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
			PineUI.addButtonListeners(Buttons_Copy);
			PineUI.addButtonListeners(Buttons_Paste);
			PineUI.addButtonListeners(Buttons_Cut);
			PineUI.addButtonListeners(Buttons_SelectAll);
			PineUI.addButtonListeners(Buttons_Undo);
			PineUI.addButtonListeners(Buttons_Redo);
			PineUI.addButtonListeners(Buttons_Close);
		}
	}
}
