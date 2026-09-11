using System.Collections.Specialized;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace NewBlood
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct LegacyInputState : IInputStateTypeInfo
	{
		private static readonly KeyCode[] s_BitKeys = new KeyCode[323]
		{
			KeyCode.Backspace,
			KeyCode.Tab,
			KeyCode.Clear,
			KeyCode.Return,
			KeyCode.Pause,
			KeyCode.Escape,
			KeyCode.Space,
			KeyCode.Exclaim,
			KeyCode.DoubleQuote,
			KeyCode.Hash,
			KeyCode.Dollar,
			KeyCode.Percent,
			KeyCode.Ampersand,
			KeyCode.Quote,
			KeyCode.LeftParen,
			KeyCode.RightParen,
			KeyCode.Asterisk,
			KeyCode.Plus,
			KeyCode.Comma,
			KeyCode.Minus,
			KeyCode.Period,
			KeyCode.Slash,
			KeyCode.Alpha0,
			KeyCode.Alpha1,
			KeyCode.Alpha2,
			KeyCode.Alpha3,
			KeyCode.Alpha4,
			KeyCode.Alpha5,
			KeyCode.Alpha6,
			KeyCode.Alpha7,
			KeyCode.Alpha8,
			KeyCode.Alpha9,
			KeyCode.Colon,
			KeyCode.Semicolon,
			KeyCode.Less,
			KeyCode.Equals,
			KeyCode.Greater,
			KeyCode.Question,
			KeyCode.At,
			KeyCode.LeftBracket,
			KeyCode.Backslash,
			KeyCode.RightBracket,
			KeyCode.Caret,
			KeyCode.Underscore,
			KeyCode.BackQuote,
			KeyCode.A,
			KeyCode.B,
			KeyCode.C,
			KeyCode.D,
			KeyCode.E,
			KeyCode.F,
			KeyCode.G,
			KeyCode.H,
			KeyCode.I,
			KeyCode.J,
			KeyCode.K,
			KeyCode.L,
			KeyCode.M,
			KeyCode.N,
			KeyCode.O,
			KeyCode.P,
			KeyCode.Q,
			KeyCode.R,
			KeyCode.S,
			KeyCode.T,
			KeyCode.U,
			KeyCode.V,
			KeyCode.W,
			KeyCode.X,
			KeyCode.Y,
			KeyCode.Z,
			KeyCode.LeftCurlyBracket,
			KeyCode.Pipe,
			KeyCode.RightCurlyBracket,
			KeyCode.Tilde,
			KeyCode.Delete,
			KeyCode.Keypad0,
			KeyCode.Keypad1,
			KeyCode.Keypad2,
			KeyCode.Keypad3,
			KeyCode.Keypad4,
			KeyCode.Keypad5,
			KeyCode.Keypad6,
			KeyCode.Keypad7,
			KeyCode.Keypad8,
			KeyCode.Keypad9,
			KeyCode.KeypadPeriod,
			KeyCode.KeypadDivide,
			KeyCode.KeypadMultiply,
			KeyCode.KeypadMinus,
			KeyCode.KeypadPlus,
			KeyCode.KeypadEnter,
			KeyCode.KeypadEquals,
			KeyCode.UpArrow,
			KeyCode.DownArrow,
			KeyCode.RightArrow,
			KeyCode.LeftArrow,
			KeyCode.Insert,
			KeyCode.Home,
			KeyCode.End,
			KeyCode.PageUp,
			KeyCode.PageDown,
			KeyCode.F1,
			KeyCode.F2,
			KeyCode.F3,
			KeyCode.F4,
			KeyCode.F5,
			KeyCode.F6,
			KeyCode.F7,
			KeyCode.F8,
			KeyCode.F9,
			KeyCode.F10,
			KeyCode.F11,
			KeyCode.F12,
			KeyCode.F13,
			KeyCode.F14,
			KeyCode.F15,
			KeyCode.Numlock,
			KeyCode.CapsLock,
			KeyCode.ScrollLock,
			KeyCode.RightShift,
			KeyCode.LeftShift,
			KeyCode.RightControl,
			KeyCode.LeftControl,
			KeyCode.RightAlt,
			KeyCode.LeftAlt,
			KeyCode.RightMeta,
			KeyCode.LeftMeta,
			KeyCode.LeftWindows,
			KeyCode.RightWindows,
			KeyCode.AltGr,
			KeyCode.Help,
			KeyCode.Print,
			KeyCode.SysReq,
			KeyCode.Break,
			KeyCode.Menu,
			KeyCode.Mouse0,
			KeyCode.Mouse1,
			KeyCode.Mouse2,
			KeyCode.Mouse3,
			KeyCode.Mouse4,
			KeyCode.Mouse5,
			KeyCode.Mouse6,
			KeyCode.JoystickButton0,
			KeyCode.JoystickButton1,
			KeyCode.JoystickButton2,
			KeyCode.JoystickButton3,
			KeyCode.JoystickButton4,
			KeyCode.JoystickButton5,
			KeyCode.JoystickButton6,
			KeyCode.JoystickButton7,
			KeyCode.JoystickButton8,
			KeyCode.JoystickButton9,
			KeyCode.JoystickButton10,
			KeyCode.JoystickButton11,
			KeyCode.JoystickButton12,
			KeyCode.JoystickButton13,
			KeyCode.JoystickButton14,
			KeyCode.JoystickButton15,
			KeyCode.JoystickButton16,
			KeyCode.JoystickButton17,
			KeyCode.JoystickButton18,
			KeyCode.JoystickButton19,
			KeyCode.Joystick1Button0,
			KeyCode.Joystick1Button1,
			KeyCode.Joystick1Button2,
			KeyCode.Joystick1Button3,
			KeyCode.Joystick1Button4,
			KeyCode.Joystick1Button5,
			KeyCode.Joystick1Button6,
			KeyCode.Joystick1Button7,
			KeyCode.Joystick1Button8,
			KeyCode.Joystick1Button9,
			KeyCode.Joystick1Button10,
			KeyCode.Joystick1Button11,
			KeyCode.Joystick1Button12,
			KeyCode.Joystick1Button13,
			KeyCode.Joystick1Button14,
			KeyCode.Joystick1Button15,
			KeyCode.Joystick1Button16,
			KeyCode.Joystick1Button17,
			KeyCode.Joystick1Button18,
			KeyCode.Joystick1Button19,
			KeyCode.Joystick2Button0,
			KeyCode.Joystick2Button1,
			KeyCode.Joystick2Button2,
			KeyCode.Joystick2Button3,
			KeyCode.Joystick2Button4,
			KeyCode.Joystick2Button5,
			KeyCode.Joystick2Button6,
			KeyCode.Joystick2Button7,
			KeyCode.Joystick2Button8,
			KeyCode.Joystick2Button9,
			KeyCode.Joystick2Button10,
			KeyCode.Joystick2Button11,
			KeyCode.Joystick2Button12,
			KeyCode.Joystick2Button13,
			KeyCode.Joystick2Button14,
			KeyCode.Joystick2Button15,
			KeyCode.Joystick2Button16,
			KeyCode.Joystick2Button17,
			KeyCode.Joystick2Button18,
			KeyCode.Joystick2Button19,
			KeyCode.Joystick3Button0,
			KeyCode.Joystick3Button1,
			KeyCode.Joystick3Button2,
			KeyCode.Joystick3Button3,
			KeyCode.Joystick3Button4,
			KeyCode.Joystick3Button5,
			KeyCode.Joystick3Button6,
			KeyCode.Joystick3Button7,
			KeyCode.Joystick3Button8,
			KeyCode.Joystick3Button9,
			KeyCode.Joystick3Button10,
			KeyCode.Joystick3Button11,
			KeyCode.Joystick3Button12,
			KeyCode.Joystick3Button13,
			KeyCode.Joystick3Button14,
			KeyCode.Joystick3Button15,
			KeyCode.Joystick3Button16,
			KeyCode.Joystick3Button17,
			KeyCode.Joystick3Button18,
			KeyCode.Joystick3Button19,
			KeyCode.Joystick4Button0,
			KeyCode.Joystick4Button1,
			KeyCode.Joystick4Button2,
			KeyCode.Joystick4Button3,
			KeyCode.Joystick4Button4,
			KeyCode.Joystick4Button5,
			KeyCode.Joystick4Button6,
			KeyCode.Joystick4Button7,
			KeyCode.Joystick4Button8,
			KeyCode.Joystick4Button9,
			KeyCode.Joystick4Button10,
			KeyCode.Joystick4Button11,
			KeyCode.Joystick4Button12,
			KeyCode.Joystick4Button13,
			KeyCode.Joystick4Button14,
			KeyCode.Joystick4Button15,
			KeyCode.Joystick4Button16,
			KeyCode.Joystick4Button17,
			KeyCode.Joystick4Button18,
			KeyCode.Joystick4Button19,
			KeyCode.Joystick5Button0,
			KeyCode.Joystick5Button1,
			KeyCode.Joystick5Button2,
			KeyCode.Joystick5Button3,
			KeyCode.Joystick5Button4,
			KeyCode.Joystick5Button5,
			KeyCode.Joystick5Button6,
			KeyCode.Joystick5Button7,
			KeyCode.Joystick5Button8,
			KeyCode.Joystick5Button9,
			KeyCode.Joystick5Button10,
			KeyCode.Joystick5Button11,
			KeyCode.Joystick5Button12,
			KeyCode.Joystick5Button13,
			KeyCode.Joystick5Button14,
			KeyCode.Joystick5Button15,
			KeyCode.Joystick5Button16,
			KeyCode.Joystick5Button17,
			KeyCode.Joystick5Button18,
			KeyCode.Joystick5Button19,
			KeyCode.Joystick6Button0,
			KeyCode.Joystick6Button1,
			KeyCode.Joystick6Button2,
			KeyCode.Joystick6Button3,
			KeyCode.Joystick6Button4,
			KeyCode.Joystick6Button5,
			KeyCode.Joystick6Button6,
			KeyCode.Joystick6Button7,
			KeyCode.Joystick6Button8,
			KeyCode.Joystick6Button9,
			KeyCode.Joystick6Button10,
			KeyCode.Joystick6Button11,
			KeyCode.Joystick6Button12,
			KeyCode.Joystick6Button13,
			KeyCode.Joystick6Button14,
			KeyCode.Joystick6Button15,
			KeyCode.Joystick6Button16,
			KeyCode.Joystick6Button17,
			KeyCode.Joystick6Button18,
			KeyCode.Joystick6Button19,
			KeyCode.Joystick7Button0,
			KeyCode.Joystick7Button1,
			KeyCode.Joystick7Button2,
			KeyCode.Joystick7Button3,
			KeyCode.Joystick7Button4,
			KeyCode.Joystick7Button5,
			KeyCode.Joystick7Button6,
			KeyCode.Joystick7Button7,
			KeyCode.Joystick7Button8,
			KeyCode.Joystick7Button9,
			KeyCode.Joystick7Button10,
			KeyCode.Joystick7Button11,
			KeyCode.Joystick7Button12,
			KeyCode.Joystick7Button13,
			KeyCode.Joystick7Button14,
			KeyCode.Joystick7Button15,
			KeyCode.Joystick7Button16,
			KeyCode.Joystick7Button17,
			KeyCode.Joystick7Button18,
			KeyCode.Joystick7Button19,
			KeyCode.Joystick8Button0,
			KeyCode.Joystick8Button1,
			KeyCode.Joystick8Button2,
			KeyCode.Joystick8Button3,
			KeyCode.Joystick8Button4,
			KeyCode.Joystick8Button5,
			KeyCode.Joystick8Button6,
			KeyCode.Joystick8Button7,
			KeyCode.Joystick8Button8,
			KeyCode.Joystick8Button9,
			KeyCode.Joystick8Button10,
			KeyCode.Joystick8Button11,
			KeyCode.Joystick8Button12,
			KeyCode.Joystick8Button13,
			KeyCode.Joystick8Button14,
			KeyCode.Joystick8Button15,
			KeyCode.Joystick8Button16,
			KeyCode.Joystick8Button17,
			KeyCode.Joystick8Button18,
			KeyCode.Joystick8Button19
		};

		[InputControl(name = "backspace", layout = "Button", bit = 0u)]
		[InputControl(name = "tab", layout = "Button", bit = 1u)]
		[InputControl(name = "clear", layout = "Button", bit = 2u)]
		[InputControl(name = "return", layout = "Button", bit = 3u)]
		[InputControl(name = "pause", layout = "Button", bit = 4u)]
		[InputControl(name = "escape", layout = "Button", bit = 5u)]
		[InputControl(name = "space", layout = "Button", bit = 6u)]
		[InputControl(name = "exclaim", layout = "Button", bit = 7u)]
		[InputControl(name = "doubleQuote", layout = "Button", bit = 8u)]
		[InputControl(name = "hash", layout = "Button", bit = 9u)]
		[InputControl(name = "dollar", layout = "Button", bit = 10u)]
		[InputControl(name = "percent", layout = "Button", bit = 11u)]
		[InputControl(name = "ampersand", layout = "Button", bit = 12u)]
		[InputControl(name = "quote", layout = "Button", bit = 13u)]
		[InputControl(name = "leftParen", layout = "Button", bit = 14u)]
		[InputControl(name = "rightParen", layout = "Button", bit = 15u)]
		[InputControl(name = "asterisk", layout = "Button", bit = 16u)]
		[InputControl(name = "plus", layout = "Button", bit = 17u)]
		[InputControl(name = "comma", layout = "Button", bit = 18u)]
		[InputControl(name = "minus", layout = "Button", bit = 19u)]
		[InputControl(name = "period", layout = "Button", bit = 20u)]
		[InputControl(name = "slash", layout = "Button", bit = 21u)]
		[InputControl(name = "alpha0", layout = "Button", bit = 22u)]
		[InputControl(name = "alpha1", layout = "Button", bit = 23u)]
		[InputControl(name = "alpha2", layout = "Button", bit = 24u)]
		[InputControl(name = "alpha3", layout = "Button", bit = 25u)]
		[InputControl(name = "alpha4", layout = "Button", bit = 26u)]
		[InputControl(name = "alpha5", layout = "Button", bit = 27u)]
		[InputControl(name = "alpha6", layout = "Button", bit = 28u)]
		[InputControl(name = "alpha7", layout = "Button", bit = 29u)]
		[InputControl(name = "alpha8", layout = "Button", bit = 30u)]
		[InputControl(name = "alpha9", layout = "Button", bit = 31u)]
		public int bits0;

		[InputControl(name = "colon", layout = "Button", bit = 0u)]
		[InputControl(name = "semicolon", layout = "Button", bit = 1u)]
		[InputControl(name = "less", layout = "Button", bit = 2u)]
		[InputControl(name = "equals", layout = "Button", bit = 3u)]
		[InputControl(name = "greater", layout = "Button", bit = 4u)]
		[InputControl(name = "question", layout = "Button", bit = 5u)]
		[InputControl(name = "at", layout = "Button", bit = 6u)]
		[InputControl(name = "leftBracket", layout = "Button", bit = 7u)]
		[InputControl(name = "backslash", layout = "Button", bit = 8u)]
		[InputControl(name = "rightBracket", layout = "Button", bit = 9u)]
		[InputControl(name = "caret", layout = "Button", bit = 10u)]
		[InputControl(name = "underscore", layout = "Button", bit = 11u)]
		[InputControl(name = "backQuote", layout = "Button", bit = 12u)]
		[InputControl(name = "a", layout = "Button", bit = 13u)]
		[InputControl(name = "b", layout = "Button", bit = 14u)]
		[InputControl(name = "c", layout = "Button", bit = 15u)]
		[InputControl(name = "d", layout = "Button", bit = 16u)]
		[InputControl(name = "e", layout = "Button", bit = 17u)]
		[InputControl(name = "f", layout = "Button", bit = 18u)]
		[InputControl(name = "g", layout = "Button", bit = 19u)]
		[InputControl(name = "h", layout = "Button", bit = 20u)]
		[InputControl(name = "i", layout = "Button", bit = 21u)]
		[InputControl(name = "j", layout = "Button", bit = 22u)]
		[InputControl(name = "k", layout = "Button", bit = 23u)]
		[InputControl(name = "l", layout = "Button", bit = 24u)]
		[InputControl(name = "m", layout = "Button", bit = 25u)]
		[InputControl(name = "n", layout = "Button", bit = 26u)]
		[InputControl(name = "o", layout = "Button", bit = 27u)]
		[InputControl(name = "p", layout = "Button", bit = 28u)]
		[InputControl(name = "q", layout = "Button", bit = 29u)]
		[InputControl(name = "r", layout = "Button", bit = 30u)]
		[InputControl(name = "s", layout = "Button", bit = 31u)]
		public int bits1;

		[InputControl(name = "t", layout = "Button", bit = 0u)]
		[InputControl(name = "u", layout = "Button", bit = 1u)]
		[InputControl(name = "v", layout = "Button", bit = 2u)]
		[InputControl(name = "w", layout = "Button", bit = 3u)]
		[InputControl(name = "x", layout = "Button", bit = 4u)]
		[InputControl(name = "y", layout = "Button", bit = 5u)]
		[InputControl(name = "z", layout = "Button", bit = 6u)]
		[InputControl(name = "leftCurlyBracket", layout = "Button", bit = 7u)]
		[InputControl(name = "pipe", layout = "Button", bit = 8u)]
		[InputControl(name = "rightCurlyBracket", layout = "Button", bit = 9u)]
		[InputControl(name = "tilde", layout = "Button", bit = 10u)]
		[InputControl(name = "delete", layout = "Button", bit = 11u)]
		[InputControl(name = "keypad0", layout = "Button", bit = 12u)]
		[InputControl(name = "keypad1", layout = "Button", bit = 13u)]
		[InputControl(name = "keypad2", layout = "Button", bit = 14u)]
		[InputControl(name = "keypad3", layout = "Button", bit = 15u)]
		[InputControl(name = "keypad4", layout = "Button", bit = 16u)]
		[InputControl(name = "keypad5", layout = "Button", bit = 17u)]
		[InputControl(name = "keypad6", layout = "Button", bit = 18u)]
		[InputControl(name = "keypad7", layout = "Button", bit = 19u)]
		[InputControl(name = "keypad8", layout = "Button", bit = 20u)]
		[InputControl(name = "keypad9", layout = "Button", bit = 21u)]
		[InputControl(name = "keypadPeriod", layout = "Button", bit = 22u)]
		[InputControl(name = "keypadDivide", layout = "Button", bit = 23u)]
		[InputControl(name = "keypadMultiply", layout = "Button", bit = 24u)]
		[InputControl(name = "keypadMinus", layout = "Button", bit = 25u)]
		[InputControl(name = "keypadPlus", layout = "Button", bit = 26u)]
		[InputControl(name = "keypadEnter", layout = "Button", bit = 27u)]
		[InputControl(name = "keypadEquals", layout = "Button", bit = 28u)]
		[InputControl(name = "upArrow", layout = "Button", bit = 29u)]
		[InputControl(name = "downArrow", layout = "Button", bit = 30u)]
		[InputControl(name = "rightArrow", layout = "Button", bit = 31u)]
		public int bits2;

		[InputControl(name = "leftArrow", layout = "Button", bit = 0u)]
		[InputControl(name = "insert", layout = "Button", bit = 1u)]
		[InputControl(name = "home", layout = "Button", bit = 2u)]
		[InputControl(name = "end", layout = "Button", bit = 3u)]
		[InputControl(name = "pageUp", layout = "Button", bit = 4u)]
		[InputControl(name = "pageDown", layout = "Button", bit = 5u)]
		[InputControl(name = "f1", layout = "Button", bit = 6u)]
		[InputControl(name = "f2", layout = "Button", bit = 7u)]
		[InputControl(name = "f3", layout = "Button", bit = 8u)]
		[InputControl(name = "f4", layout = "Button", bit = 9u)]
		[InputControl(name = "f5", layout = "Button", bit = 10u)]
		[InputControl(name = "f6", layout = "Button", bit = 11u)]
		[InputControl(name = "f7", layout = "Button", bit = 12u)]
		[InputControl(name = "f8", layout = "Button", bit = 13u)]
		[InputControl(name = "f9", layout = "Button", bit = 14u)]
		[InputControl(name = "f10", layout = "Button", bit = 15u)]
		[InputControl(name = "f11", layout = "Button", bit = 16u)]
		[InputControl(name = "f12", layout = "Button", bit = 17u)]
		[InputControl(name = "f13", layout = "Button", bit = 18u)]
		[InputControl(name = "f14", layout = "Button", bit = 19u)]
		[InputControl(name = "f15", layout = "Button", bit = 20u)]
		[InputControl(name = "numlock", layout = "Button", bit = 21u)]
		[InputControl(name = "capsLock", layout = "Button", bit = 22u)]
		[InputControl(name = "scrollLock", layout = "Button", bit = 23u)]
		[InputControl(name = "rightShift", layout = "Button", bit = 24u)]
		[InputControl(name = "leftShift", layout = "Button", bit = 25u)]
		[InputControl(name = "rightControl", layout = "Button", bit = 26u)]
		[InputControl(name = "leftControl", layout = "Button", bit = 27u)]
		[InputControl(name = "rightAlt", layout = "Button", bit = 28u)]
		[InputControl(name = "leftAlt", layout = "Button", bit = 29u)]
		[InputControl(name = "rightCommand", layout = "Button", bit = 30u)]
		[InputControl(name = "leftCommand", layout = "Button", bit = 31u)]
		public int bits3;

		[InputControl(name = "leftWindows", layout = "Button", bit = 0u)]
		[InputControl(name = "rightWindows", layout = "Button", bit = 1u)]
		[InputControl(name = "altGr", layout = "Button", bit = 2u)]
		[InputControl(name = "help", layout = "Button", bit = 3u)]
		[InputControl(name = "print", layout = "Button", bit = 4u)]
		[InputControl(name = "sysReq", layout = "Button", bit = 5u)]
		[InputControl(name = "break", layout = "Button", bit = 6u)]
		[InputControl(name = "menu", layout = "Button", bit = 7u)]
		[InputControl(name = "mouse0", layout = "Button", bit = 8u)]
		[InputControl(name = "mouse1", layout = "Button", bit = 9u)]
		[InputControl(name = "mouse2", layout = "Button", bit = 10u)]
		[InputControl(name = "mouse3", layout = "Button", bit = 11u)]
		[InputControl(name = "mouse4", layout = "Button", bit = 12u)]
		[InputControl(name = "mouse5", layout = "Button", bit = 13u)]
		[InputControl(name = "mouse6", layout = "Button", bit = 14u)]
		[InputControl(name = "joystickButton0", layout = "Button", bit = 15u)]
		[InputControl(name = "joystickButton1", layout = "Button", bit = 16u)]
		[InputControl(name = "joystickButton2", layout = "Button", bit = 17u)]
		[InputControl(name = "joystickButton3", layout = "Button", bit = 18u)]
		[InputControl(name = "joystickButton4", layout = "Button", bit = 19u)]
		[InputControl(name = "joystickButton5", layout = "Button", bit = 20u)]
		[InputControl(name = "joystickButton6", layout = "Button", bit = 21u)]
		[InputControl(name = "joystickButton7", layout = "Button", bit = 22u)]
		[InputControl(name = "joystickButton8", layout = "Button", bit = 23u)]
		[InputControl(name = "joystickButton9", layout = "Button", bit = 24u)]
		[InputControl(name = "joystickButton10", layout = "Button", bit = 25u)]
		[InputControl(name = "joystickButton11", layout = "Button", bit = 26u)]
		[InputControl(name = "joystickButton12", layout = "Button", bit = 27u)]
		[InputControl(name = "joystickButton13", layout = "Button", bit = 28u)]
		[InputControl(name = "joystickButton14", layout = "Button", bit = 29u)]
		[InputControl(name = "joystickButton15", layout = "Button", bit = 30u)]
		[InputControl(name = "joystickButton16", layout = "Button", bit = 31u)]
		public int bits4;

		[InputControl(name = "joystickButton17", layout = "Button", bit = 0u)]
		[InputControl(name = "joystickButton18", layout = "Button", bit = 1u)]
		[InputControl(name = "joystickButton19", layout = "Button", bit = 2u)]
		[InputControl(name = "joystick1Button0", layout = "Button", bit = 3u)]
		[InputControl(name = "joystick1Button1", layout = "Button", bit = 4u)]
		[InputControl(name = "joystick1Button2", layout = "Button", bit = 5u)]
		[InputControl(name = "joystick1Button3", layout = "Button", bit = 6u)]
		[InputControl(name = "joystick1Button4", layout = "Button", bit = 7u)]
		[InputControl(name = "joystick1Button5", layout = "Button", bit = 8u)]
		[InputControl(name = "joystick1Button6", layout = "Button", bit = 9u)]
		[InputControl(name = "joystick1Button7", layout = "Button", bit = 10u)]
		[InputControl(name = "joystick1Button8", layout = "Button", bit = 11u)]
		[InputControl(name = "joystick1Button9", layout = "Button", bit = 12u)]
		[InputControl(name = "joystick1Button10", layout = "Button", bit = 13u)]
		[InputControl(name = "joystick1Button11", layout = "Button", bit = 14u)]
		[InputControl(name = "joystick1Button12", layout = "Button", bit = 15u)]
		[InputControl(name = "joystick1Button13", layout = "Button", bit = 16u)]
		[InputControl(name = "joystick1Button14", layout = "Button", bit = 17u)]
		[InputControl(name = "joystick1Button15", layout = "Button", bit = 18u)]
		[InputControl(name = "joystick1Button16", layout = "Button", bit = 19u)]
		[InputControl(name = "joystick1Button17", layout = "Button", bit = 20u)]
		[InputControl(name = "joystick1Button18", layout = "Button", bit = 21u)]
		[InputControl(name = "joystick1Button19", layout = "Button", bit = 22u)]
		[InputControl(name = "joystick2Button0", layout = "Button", bit = 23u)]
		[InputControl(name = "joystick2Button1", layout = "Button", bit = 24u)]
		[InputControl(name = "joystick2Button2", layout = "Button", bit = 25u)]
		[InputControl(name = "joystick2Button3", layout = "Button", bit = 26u)]
		[InputControl(name = "joystick2Button4", layout = "Button", bit = 27u)]
		[InputControl(name = "joystick2Button5", layout = "Button", bit = 28u)]
		[InputControl(name = "joystick2Button6", layout = "Button", bit = 29u)]
		[InputControl(name = "joystick2Button7", layout = "Button", bit = 30u)]
		[InputControl(name = "joystick2Button8", layout = "Button", bit = 31u)]
		public int bits5;

		[InputControl(name = "joystick2Button9", layout = "Button", bit = 0u)]
		[InputControl(name = "joystick2Button10", layout = "Button", bit = 1u)]
		[InputControl(name = "joystick2Button11", layout = "Button", bit = 2u)]
		[InputControl(name = "joystick2Button12", layout = "Button", bit = 3u)]
		[InputControl(name = "joystick2Button13", layout = "Button", bit = 4u)]
		[InputControl(name = "joystick2Button14", layout = "Button", bit = 5u)]
		[InputControl(name = "joystick2Button15", layout = "Button", bit = 6u)]
		[InputControl(name = "joystick2Button16", layout = "Button", bit = 7u)]
		[InputControl(name = "joystick2Button17", layout = "Button", bit = 8u)]
		[InputControl(name = "joystick2Button18", layout = "Button", bit = 9u)]
		[InputControl(name = "joystick2Button19", layout = "Button", bit = 10u)]
		[InputControl(name = "joystick3Button0", layout = "Button", bit = 11u)]
		[InputControl(name = "joystick3Button1", layout = "Button", bit = 12u)]
		[InputControl(name = "joystick3Button2", layout = "Button", bit = 13u)]
		[InputControl(name = "joystick3Button3", layout = "Button", bit = 14u)]
		[InputControl(name = "joystick3Button4", layout = "Button", bit = 15u)]
		[InputControl(name = "joystick3Button5", layout = "Button", bit = 16u)]
		[InputControl(name = "joystick3Button6", layout = "Button", bit = 17u)]
		[InputControl(name = "joystick3Button7", layout = "Button", bit = 18u)]
		[InputControl(name = "joystick3Button8", layout = "Button", bit = 19u)]
		[InputControl(name = "joystick3Button9", layout = "Button", bit = 20u)]
		[InputControl(name = "joystick3Button10", layout = "Button", bit = 21u)]
		[InputControl(name = "joystick3Button11", layout = "Button", bit = 22u)]
		[InputControl(name = "joystick3Button12", layout = "Button", bit = 23u)]
		[InputControl(name = "joystick3Button13", layout = "Button", bit = 24u)]
		[InputControl(name = "joystick3Button14", layout = "Button", bit = 25u)]
		[InputControl(name = "joystick3Button15", layout = "Button", bit = 26u)]
		[InputControl(name = "joystick3Button16", layout = "Button", bit = 27u)]
		[InputControl(name = "joystick3Button17", layout = "Button", bit = 28u)]
		[InputControl(name = "joystick3Button18", layout = "Button", bit = 29u)]
		[InputControl(name = "joystick3Button19", layout = "Button", bit = 30u)]
		[InputControl(name = "joystick4Button0", layout = "Button", bit = 31u)]
		public int bits6;

		[InputControl(name = "joystick4Button1", layout = "Button", bit = 0u)]
		[InputControl(name = "joystick4Button2", layout = "Button", bit = 1u)]
		[InputControl(name = "joystick4Button3", layout = "Button", bit = 2u)]
		[InputControl(name = "joystick4Button4", layout = "Button", bit = 3u)]
		[InputControl(name = "joystick4Button5", layout = "Button", bit = 4u)]
		[InputControl(name = "joystick4Button6", layout = "Button", bit = 5u)]
		[InputControl(name = "joystick4Button7", layout = "Button", bit = 6u)]
		[InputControl(name = "joystick4Button8", layout = "Button", bit = 7u)]
		[InputControl(name = "joystick4Button9", layout = "Button", bit = 8u)]
		[InputControl(name = "joystick4Button10", layout = "Button", bit = 9u)]
		[InputControl(name = "joystick4Button11", layout = "Button", bit = 10u)]
		[InputControl(name = "joystick4Button12", layout = "Button", bit = 11u)]
		[InputControl(name = "joystick4Button13", layout = "Button", bit = 12u)]
		[InputControl(name = "joystick4Button14", layout = "Button", bit = 13u)]
		[InputControl(name = "joystick4Button15", layout = "Button", bit = 14u)]
		[InputControl(name = "joystick4Button16", layout = "Button", bit = 15u)]
		[InputControl(name = "joystick4Button17", layout = "Button", bit = 16u)]
		[InputControl(name = "joystick4Button18", layout = "Button", bit = 17u)]
		[InputControl(name = "joystick4Button19", layout = "Button", bit = 18u)]
		[InputControl(name = "joystick5Button0", layout = "Button", bit = 19u)]
		[InputControl(name = "joystick5Button1", layout = "Button", bit = 20u)]
		[InputControl(name = "joystick5Button2", layout = "Button", bit = 21u)]
		[InputControl(name = "joystick5Button3", layout = "Button", bit = 22u)]
		[InputControl(name = "joystick5Button4", layout = "Button", bit = 23u)]
		[InputControl(name = "joystick5Button5", layout = "Button", bit = 24u)]
		[InputControl(name = "joystick5Button6", layout = "Button", bit = 25u)]
		[InputControl(name = "joystick5Button7", layout = "Button", bit = 26u)]
		[InputControl(name = "joystick5Button8", layout = "Button", bit = 27u)]
		[InputControl(name = "joystick5Button9", layout = "Button", bit = 28u)]
		[InputControl(name = "joystick5Button10", layout = "Button", bit = 29u)]
		[InputControl(name = "joystick5Button11", layout = "Button", bit = 30u)]
		[InputControl(name = "joystick5Button12", layout = "Button", bit = 31u)]
		public int bits7;

		[InputControl(name = "joystick5Button13", layout = "Button", bit = 0u)]
		[InputControl(name = "joystick5Button14", layout = "Button", bit = 1u)]
		[InputControl(name = "joystick5Button15", layout = "Button", bit = 2u)]
		[InputControl(name = "joystick5Button16", layout = "Button", bit = 3u)]
		[InputControl(name = "joystick5Button17", layout = "Button", bit = 4u)]
		[InputControl(name = "joystick5Button18", layout = "Button", bit = 5u)]
		[InputControl(name = "joystick5Button19", layout = "Button", bit = 6u)]
		[InputControl(name = "joystick6Button0", layout = "Button", bit = 7u)]
		[InputControl(name = "joystick6Button1", layout = "Button", bit = 8u)]
		[InputControl(name = "joystick6Button2", layout = "Button", bit = 9u)]
		[InputControl(name = "joystick6Button3", layout = "Button", bit = 10u)]
		[InputControl(name = "joystick6Button4", layout = "Button", bit = 11u)]
		[InputControl(name = "joystick6Button5", layout = "Button", bit = 12u)]
		[InputControl(name = "joystick6Button6", layout = "Button", bit = 13u)]
		[InputControl(name = "joystick6Button7", layout = "Button", bit = 14u)]
		[InputControl(name = "joystick6Button8", layout = "Button", bit = 15u)]
		[InputControl(name = "joystick6Button9", layout = "Button", bit = 16u)]
		[InputControl(name = "joystick6Button10", layout = "Button", bit = 17u)]
		[InputControl(name = "joystick6Button11", layout = "Button", bit = 18u)]
		[InputControl(name = "joystick6Button12", layout = "Button", bit = 19u)]
		[InputControl(name = "joystick6Button13", layout = "Button", bit = 20u)]
		[InputControl(name = "joystick6Button14", layout = "Button", bit = 21u)]
		[InputControl(name = "joystick6Button15", layout = "Button", bit = 22u)]
		[InputControl(name = "joystick6Button16", layout = "Button", bit = 23u)]
		[InputControl(name = "joystick6Button17", layout = "Button", bit = 24u)]
		[InputControl(name = "joystick6Button18", layout = "Button", bit = 25u)]
		[InputControl(name = "joystick6Button19", layout = "Button", bit = 26u)]
		[InputControl(name = "joystick7Button0", layout = "Button", bit = 27u)]
		[InputControl(name = "joystick7Button1", layout = "Button", bit = 28u)]
		[InputControl(name = "joystick7Button2", layout = "Button", bit = 29u)]
		[InputControl(name = "joystick7Button3", layout = "Button", bit = 30u)]
		[InputControl(name = "joystick7Button4", layout = "Button", bit = 31u)]
		public int bits8;

		[InputControl(name = "joystick7Button5", layout = "Button", bit = 0u)]
		[InputControl(name = "joystick7Button6", layout = "Button", bit = 1u)]
		[InputControl(name = "joystick7Button7", layout = "Button", bit = 2u)]
		[InputControl(name = "joystick7Button8", layout = "Button", bit = 3u)]
		[InputControl(name = "joystick7Button9", layout = "Button", bit = 4u)]
		[InputControl(name = "joystick7Button10", layout = "Button", bit = 5u)]
		[InputControl(name = "joystick7Button11", layout = "Button", bit = 6u)]
		[InputControl(name = "joystick7Button12", layout = "Button", bit = 7u)]
		[InputControl(name = "joystick7Button13", layout = "Button", bit = 8u)]
		[InputControl(name = "joystick7Button14", layout = "Button", bit = 9u)]
		[InputControl(name = "joystick7Button15", layout = "Button", bit = 10u)]
		[InputControl(name = "joystick7Button16", layout = "Button", bit = 11u)]
		[InputControl(name = "joystick7Button17", layout = "Button", bit = 12u)]
		[InputControl(name = "joystick7Button18", layout = "Button", bit = 13u)]
		[InputControl(name = "joystick7Button19", layout = "Button", bit = 14u)]
		[InputControl(name = "joystick8Button0", layout = "Button", bit = 15u)]
		[InputControl(name = "joystick8Button1", layout = "Button", bit = 16u)]
		[InputControl(name = "joystick8Button2", layout = "Button", bit = 17u)]
		[InputControl(name = "joystick8Button3", layout = "Button", bit = 18u)]
		[InputControl(name = "joystick8Button4", layout = "Button", bit = 19u)]
		[InputControl(name = "joystick8Button5", layout = "Button", bit = 20u)]
		[InputControl(name = "joystick8Button6", layout = "Button", bit = 21u)]
		[InputControl(name = "joystick8Button7", layout = "Button", bit = 22u)]
		[InputControl(name = "joystick8Button8", layout = "Button", bit = 23u)]
		[InputControl(name = "joystick8Button9", layout = "Button", bit = 24u)]
		[InputControl(name = "joystick8Button10", layout = "Button", bit = 25u)]
		[InputControl(name = "joystick8Button11", layout = "Button", bit = 26u)]
		[InputControl(name = "joystick8Button12", layout = "Button", bit = 27u)]
		[InputControl(name = "joystick8Button13", layout = "Button", bit = 28u)]
		[InputControl(name = "joystick8Button14", layout = "Button", bit = 29u)]
		[InputControl(name = "joystick8Button15", layout = "Button", bit = 30u)]
		[InputControl(name = "joystick8Button16", layout = "Button", bit = 31u)]
		public int bits9;

		[InputControl(name = "joystick8Button17", layout = "Button", bit = 0u)]
		[InputControl(name = "joystick8Button18", layout = "Button", bit = 1u)]
		[InputControl(name = "joystick8Button19", layout = "Button", bit = 2u)]
		public int bits10;

		public FourCC format => new FourCC('U', 'E', 'L', 'I');

		public unsafe void Update()
		{
			fixed (int* ptr = &bits0)
			{
				for (int i = 0; i < s_BitKeys.Length; i++)
				{
					BitVector32 bitVector = new BitVector32(ptr[i / 32]);
					int bit = 1 << i % 32;
					bitVector[bit] = Input.GetKey(s_BitKeys[i]);
					ptr[i / 32] = bitVector.Data;
				}
			}
		}
	}
}
