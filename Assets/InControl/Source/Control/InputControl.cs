using System;
using UnityEngine;


namespace InControl
{
	public class InputControl
	{
		public static readonly InputControl Null = new InputControl( "NullInputControl" );

		public string Handle { get; protected set; }
		public InputControlType Target { get; protected set; }

		public ulong UpdateTick { get; protected set; }

		public float Sensitivity = 1.0f;
		public float LowerDeadZone = 0.0f;
		public float UpperDeadZone = 1.0f;

		public bool IsButton { get; protected set; }

		InputControlState thisState;
		InputControlState lastState;
		InputControlState tempState;

		ulong zeroTick;


		private InputControl( string handle )
		{
			Handle = handle;
		}


		public InputControl( string handle, InputControlType target )
		{
			Handle = handle;
			Target = target;

			IsButton = (target >= InputControlType.Action1 && target <= InputControlType.Action4) ||
					   (target >= InputControlType.Button0 && target <= InputControlType.Button19);
		}


		public void UpdateWithState( bool state, ulong updateTick )
		{
			if (IsNull)
			{
				throw new InvalidOperationException( "A null control cannot be updated." );
			}

			if (UpdateTick > updateTick)
			{
				throw new InvalidOperationException( "A control cannot be updated with an earlier tick." );
			}

			tempState.Set( state || tempState.State );
		}


		public void UpdateWithValue( float value, ulong updateTick )
		{
			if (IsNull)
			{
				throw new InvalidOperationException( "A null control cannot be updated." );
			}

			if (UpdateTick > updateTick)
			{
				throw new InvalidOperationException( "A control cannot be updated with an earlier tick." );
			}

			if (Mathf.Abs( value ) > Mathf.Abs( tempState.Value ))
			{
				tempState.Set( value );
			}
		}


		internal void PreUpdate( ulong updateTick )
		{
			RawValue = null;
			PreValue = null;

			lastState = thisState;
			tempState.Reset();
		}


		internal void PostUpdate( ulong updateTick )
		{
			thisState = tempState;
			if (thisState != lastState)
			{
				UpdateTick = updateTick;
			}
		}


		internal void SetZeroTick() 
		{ 
			zeroTick = UpdateTick; 
		}


		internal bool IsOnZeroTick
		{
			get { return UpdateTick == zeroTick; }
		}


		public bool State
		{
			get { return thisState.State; }
		}


		public bool LastState
		{
			get { return lastState.State; }
		}

        /// <summary>
        /// Returns a float.(Read Only)
        /// <para>In range -1..1 for axes, 0..1 for buttons / triggers.</para>
        /// </summary>
		public float Value
		{
			get { return thisState.Value; }
		}

        /// <summary>
        /// Returns a float, previous tick value.(Read Only)
        /// <para>In range -1..1 for axes, 0..1 for buttons / triggers.</para>
        /// </summary>
		public float LastValue
		{
			get { return lastState.Value; }
		}

        /// <summary>
        /// Returns a bool.(Read Only)
        /// <para>Has changed state since previous tick.</para>
        /// </summary>
		public bool HasChanged
		{
			get { return thisState != lastState; }
		}

        /// <summary>
        /// Returns a bool (Read Only)
        /// <para>Is currently press.</para>
        /// </summary>
        public bool IsPressed
		{
			get { return thisState.State; }
		}

        /// <summary>
        /// Pressed since the previous tick(Read Only).
        /// </summary>
        /// <returns>Returns a bulean base on the button press previous frame status.</returns>
        public bool WasPressed
		{
			get { return thisState && !lastState; }
		}

        /// <summary>
        /// Released since the previous tick (Read Only).
        /// </summary>
        /// <returns>Returns a bulean base on the button release previous frame status.</returns>
        public bool WasReleased
		{
			get { return !thisState && lastState; }
		}


		public bool IsNull
		{
			get { return this == Null; }
		}


		public bool IsNotNull
		{
			get { return this != Null; }
		}

        /// <summary>
        /// Returns a string representig the button status.
        /// </summary>
        /// <returns>Returns a format string (InputControl: Handle, Value) representig the button.</returns>
        public override string ToString()
		{
			return string.Format( "[InputControl: Handle={0}, Value={1}]", Handle, Value );
		}

        /// <summary>
        /// Compares.
        /// </summary>
        /// <param name="control">Parameter value to pass.</param>
        /// <returns>Returns an Bolean based on the passed value.</returns>
        public static implicit operator bool( InputControl control )
		{
			return control.State;
		}


		public static implicit operator float( InputControl control )
		{
			return control.Value;
		}


		public InputControlType? Obverse
		{
			get
			{
				switch (Target)
				{
				case InputControlType.LeftStickX:
					return InputControlType.LeftStickY;
				case InputControlType.LeftStickY:
					return InputControlType.LeftStickX;
				case InputControlType.RightStickX:
					return InputControlType.RightStickY;
				case InputControlType.RightStickY:
					return InputControlType.RightStickX;
				default:
					return null;
				}
			}
		}


		// This is for internal use only and is not always set.
		internal float? RawValue;
		internal float? PreValue;
	}
}