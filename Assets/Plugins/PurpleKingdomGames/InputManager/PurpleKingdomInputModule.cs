using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public sealed class PurpleKingdomInputModule : StandaloneInputModule
{
    public bool HandleMouseEvents = true;
    public string SubmitKey = "Submit";

    private Vector3 _prevMousePosition;
    private float _nextDownTick;
    private bool _isDragging;

    public override void Process()
    {
        if (eventSystem.currentSelectedGameObject == null && eventSystem.firstSelectedGameObject != null) {
            eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);
        }

        handleInputNavigation();

        if (HandleMouseEvents && Input.mousePresent && Cursor.lockState != CursorLockMode.Locked) {
            ProcessMouseEvent();
        }
    }

    public override bool ShouldActivateModule()
    {
        if (forceModuleActive) {
            return true;
        }

        return !Application.isMobilePlatform;
    }

    private void handleInputNavigation()
    {
        if (eventSystem.currentSelectedGameObject == null) {
            return;
        }

        Selectable currentItem = eventSystem.currentSelectedGameObject.GetComponent<Selectable>();
        if (currentItem == null) {
            return;
        }

        Selectable nextItem = null;
        Slider currentSlider = currentItem.GetComponent<Slider>();

        if (InputManager.GetCurrentValue(verticalAxis) < 0) {
            if (InputManager.IsDown(verticalAxis) || (InputManager.IsHeld(verticalAxis) && _nextDownTick <= Time.time)) {
                nextItem = currentItem.FindSelectableOnDown();
                _nextDownTick = Time.time + repeatDelay;
            }
        }

        if (InputManager.GetCurrentValue(verticalAxis) > 0) {
            if (InputManager.IsDown(verticalAxis) || (InputManager.IsHeld(verticalAxis) && _nextDownTick <= Time.time)) {
                nextItem = currentItem.FindSelectableOnUp();
                _nextDownTick = Time.time + repeatDelay;
            }
        }

        if (InputManager.GetCurrentValue(horizontalAxis) < 0) {
            if (InputManager.IsDown(horizontalAxis) || (InputManager.IsHeld(horizontalAxis) && _nextDownTick <= Time.time)) {
                if (currentSlider != null) {
                    currentSlider.value--;
                } else {
                    nextItem = currentItem.FindSelectableOnLeft();
                }

                _nextDownTick = Time.time + repeatDelay;
            }
        }

        if (InputManager.GetCurrentValue(horizontalAxis) > 0) {
            if (InputManager.IsDown(horizontalAxis) || (InputManager.IsHeld(horizontalAxis) && _nextDownTick <= Time.time)) {
                if (currentSlider != null) {
                    currentSlider.value++;
                } else {
                    nextItem = currentItem.FindSelectableOnRight();
                }

                _nextDownTick = Time.time + repeatDelay;
            }
        }

        if (nextItem != null) {
            eventSystem.SetSelectedGameObject(nextItem.gameObject);
        }

        if (!string.IsNullOrEmpty(SubmitKey) && InputManager.IsDown(SubmitKey)) {
            ExecuteEvents.ExecuteHierarchy(
                eventSystem.currentSelectedGameObject,
                GetBaseEventData(),
                ExecuteEvents.submitHandler
            );
        }
    }
}