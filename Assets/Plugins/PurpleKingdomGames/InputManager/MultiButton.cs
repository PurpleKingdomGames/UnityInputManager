using System;
using System.Collections;
using UnityEngine;

namespace PurpleKingdomGames.Unity.InputManager
{
    [Serializable]
    public class MultiButton : IButton, IEnumerable
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
        public string Name { get; set; }

        public int MaxLength { get { return _buttons.Length; } }
        public int Length { get { return _length; } }

        [SerializeField]
        private IButton[] _buttons = new IButton[10];

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

            throw new UnityException("Cannot add more than " + _buttons.Length + " buttons to a multi button");
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
            for (int i = 0; i < _buttons.Length; i++) {
                if (_buttons[i] == null) {
                    break;
                }

                float value = _buttons[i].GetCurrentValue();
                if (value != 0) {
                    if (Invert) {
                        return -value;
                    } else {
                        return value;
                    }
                }
            }

            return 0;
        }

        public float GetCurrentRawValue()
        {
            for (int i = 0; i < _buttons.Length; i++) {
                if (_buttons[i] == null) {
                    break;
                }

                float value = _buttons[i].GetCurrentRawValue();
                if (value != 0) {
                    if (Invert) {
                        return -value;
                    } else {
                        return value;
                    }
                }
            }

            return 0;
        }

        public bool IsDown()
        {
            for (int i = 0; i < _buttons.Length; i++) {
                if (_buttons[i] == null) {
                    break;
                }

                if (_buttons[i].IsDown()) {
                    return true;
                }
            }

            return false;
        }

        public bool IsUp()
        {
            for (int i = 0; i < _buttons.Length; i++) {
                if (_buttons[i] == null) {
                    break;
                }

                if (_buttons[i].IsUp()) {
                    return true;
                }
            }

            return false;
        }

        public bool IsHeld()
        {
            for (int i = 0; i < _buttons.Length; i++) {
                if (_buttons[i] == null) {
                    break;
                }

                if (_buttons[i].IsHeld()) {
                    return true;
                }
            }

            return false;
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
					Name += " or ";
				}

				Name += _buttons[i].Name;
			}
		}
    }
}