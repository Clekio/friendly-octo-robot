using System;
using System.Collections;
using UnityEngine;
using InControl;


namespace CustomProfile
{
	// This custom profile is enabled by adding it to the Custom Profiles list
	// on the InControlManager component, or you can attach it yourself like so:
	// InputManager.AttachDevice( new UnityInputDevice( "KeyboardAndMouseProfile" ) );
	// 
	public class KeyboardAndMouseProfile : UnityInputDeviceProfile
	{
		public KeyboardAndMouseProfile()
		{
			Name = "Keyboard/Mouse";
			Meta = "A keyboard and mouse combination profile appropriate for FPS.";

			// This profile only works on desktops.
			SupportedPlatforms = new[]
			{
				"Windows",
				"Mac",
				"Linux"
			};

			Sensitivity = 1.0f;
			LowerDeadZone = 0.0f;
			UpperDeadZone = 1.0f;

			ButtonMappings = new[]
			{
				new InputControlMapping
				{
					Handle = "Jump",
					Target = InputControlType.Action1,
					Source = KeyCodeButton( KeyCode.Space )
                },
				//new InputControlMapping
				//{
				//	Handle = "Magia",
				//	Target = InputControlType.LeftBumper,
				//	// KeyCodeButton fires when any of the provided KeyCode params are down.
				//	Source = KeyCodeButton( KeyCode.L, KeyCode.Return )
				//},
				new InputControlMapping
				{
					Handle = "Golpe Purificante",
					Target = InputControlType.Action2,
					Source = MouseButton0
				},
                new InputControlMapping
                {
                    Handle = "Accion",
                    Target = InputControlType.RightBumper,
                    Source = KeyCodeButton( KeyCode.LeftControl, KeyCode.RightControl)
                },
            };

			AnalogMappings = new[]
			{
				new InputControlMapping
				{
					Handle = "Move X",
					Target = InputControlType.LeftStickX,
					// KeyCodeAxis splits the two KeyCodes over an axis. The first is negative, the second positive.
					Source = KeyCodeAxis( KeyCode.A, KeyCode.D )
				},
				new InputControlMapping
				{
					Handle = "Move Y",
					Target = InputControlType.LeftStickY,
					// Notes that up is positive in Unity, therefore the order of KeyCodes is down, up.
					Source = KeyCodeAxis( KeyCode.S, KeyCode.W )
				},
				new InputControlMapping {
					Handle = "Move X Alternate",
					Target = InputControlType.LeftStickX,
					Source = KeyCodeAxis( KeyCode.LeftArrow, KeyCode.RightArrow )
				},
				new InputControlMapping {
					Handle = "Move Y Alternate",
					Target = InputControlType.LeftStickY,
					Source = KeyCodeAxis( KeyCode.DownArrow, KeyCode.UpArrow )
				}
			};
		}
	}
}

