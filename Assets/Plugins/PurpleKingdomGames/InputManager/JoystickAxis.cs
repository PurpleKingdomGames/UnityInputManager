using System;
using UnityEngine;

namespace PurpleKingdomGames.Unity.InputManager
{
    [Serializable]
    public class JoystickAxis : IButton
    {
        [SerializeField]
        public bool Invert { get; set; }

        [SerializeField]
        public string Name { get; protected set; }

        [SerializeField]
        public readonly int JoystickNumber;

        [SerializeField]
        public readonly int AxisNumber;

        [SerializeField]
        private string _virtualKeyName;

        public JoystickAxis(int joystickNumber, int axisNumber)
        {
            JoystickNumber = joystickNumber;
            AxisNumber = axisNumber;

            _virtualKeyName = "Joystick " + JoystickNumber + " Axis " + AxisNumber;

            // TODO: Change the name depending on the type of joystick (i.e. display 'Left thumb stick' for an Xbox controller)
            Name = _virtualKeyName;
        }

        public float GetCurrentValue()
        {
            if (Invert) {
                return -Input.GetAxis(_virtualKeyName);
            } else {
                return Input.GetAxis(_virtualKeyName);
            }
        }

        public float GetCurrentRawValue()
        {
            if (Invert) {
                return -Input.GetAxisRaw(_virtualKeyName);
            } else {
                return Input.GetAxisRaw(_virtualKeyName);
            }
        }

        public bool IsDown()
        {
            return GetCurrentRawValue() == 0;
        }

        public bool IsUp()
        {
            return GetCurrentRawValue() != 0;
        }

        public bool IsHeld()
        {
            return IsDown();
        }
    }
}
