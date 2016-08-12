using System;
using System.Collections;
using UnityEngine;

namespace PurpleKingdomGames.Unity.InputManager
{
    [Serializable]
    public class CombiButton : IButton, IEnumerable
    {
        public IButton this[int index]
        {
            get
            {
                return _buttons[index];
            }
        }

        [SerializeField]
        public bool Invert { get; set; }

        [SerializeField]
        public string Name { get; protected set; }

        public int MaxLength { get { return _buttons.Length; } }
        public int Length { get { return _length; } }

        [SerializeField]
        private IButton[] _buttons = new IButton[5];

        [SerializeField]
        private int _length = 0;

        public void Add(IButton button)
        {
            if (getIndex(button) == -1) {
                for (int i = 0; i < _buttons.Length; i++) {
                    if (_buttons[i] == null) {
                        _buttons[i] = button;
                        generateName();
                        _length++;

                        return;
                    }
                }
            }

            throw new UnityException("Cannot add more than " + _buttons.Length + " buttons to a combi button");
        }

        public void Remove(IButton button)
        {
            int i = getIndex(button);
            if (i == -1) {
                return;
            }

            _buttons[i] = null;
            _length--;
            generateName();
            for (; i < _buttons.Length; i++) {
                if (_buttons[i] == null && i < _buttons.Length - 2) {
                    int nextIndex = i + 1;
                    if (_buttons[nextIndex] == null) {
                        break;
                    }

                    _buttons[i] = _buttons[nextIndex];
                    _buttons[nextIndex] = null;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _buttons.GetEnumerator();
        }

        public float GetCurrentValue()
        {
            float max = 0;
            IButton maxBtn = null;

            for (int i = 0; i < _buttons.Length; i++) {
                if (_buttons[i] == null) {
                    break;
                }

                if (!_buttons[i].IsHeld()) {
                    return 0;
                }

                float value = Mathf.Abs(_buttons[i].GetCurrentValue());
                if (value > max) {
                    max = value;
                    maxBtn = _buttons[i];
                }
            }

            if (max != 0) {
                max = maxBtn.GetCurrentValue();
                return Invert ? -max : max;
            }

            return 0;
        }

        public float GetCurrentRawValue()
        {
            float max = 0;
            IButton maxBtn = null;

            for (int i = 0; i < _buttons.Length; i++) {
                if (_buttons[i] == null) {
                    break;
                }

                if (!_buttons[i].IsHeld()) {
                    return 0;
                }

                float value = Mathf.Abs(_buttons[i].GetCurrentRawValue());
                if (value > max) {
                    max = value;
                    maxBtn = _buttons[i];
                }
            }

            if (max != 0) {
                max = maxBtn.GetCurrentRawValue();
                return Invert ? -max : max;
            }

            return 0;
        }

        public bool IsDown()
        {
            for (int i = 0; i < _buttons.Length; i++) {
                if (_buttons[i] == null) {
                    break;
                }

                if (!_buttons[i].IsDown()) {
                    return false;
                }
            }

            return true;
        }

        public bool IsUp()
        {
            for (int i = 0; i < _buttons.Length; i++) {
                if (_buttons[i] == null) {
                    break;
                }

                if (!_buttons[i].IsUp()) {
                    return false;
                }
            }

            return true;
        }

        public bool IsHeld()
        {
            for (int i = 0; i < _buttons.Length; i++) {
                if (_buttons[i] == null) {
                    break;
                }

                if (!_buttons[i].IsHeld()) {
                    return false;
                }
            }

            return true;
        }

        private int getIndex(IButton button)
        {
            for (int i = 0; i < _buttons.Length; i++) {
                if (_buttons[i] == null) {
                    break;
                }

                if (_buttons[i] == button) {
                    return i;
                }
            }

            return -1;
        }

        private void generateName()
        {
            Name = "";
            for (int i = 0; i < _buttons.Length; i++) {
                if (_buttons[i] == null) {
                    return;
                }

                if (i != 0) {
                    Name += " + ";
                }

                Name += _buttons[i].Name;
            }
        }
    }
}