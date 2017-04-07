# Unity Input Manager

The Unity Input Manager facilitates runtime configuration of input for Unity.
As Unity is unable to edit which keys map to what input at runtime a developer can't
ordinarily create a custom page from within their game or application that allows a player or user to
change how their input is recieved.

This Asset aims to change that by providing a proxy through to Unity's own input API in a way that should not
hinder the developer.

## Installation

Installing the Input manager is a simple two step process:

* Copy the files found in Assets to your Unity Project (or import the package)
* Go to Tools > Purple Kingdom Games > Create Input File

Your input settings should now have about 310 entries for Mouse Axis and all Joystick Axis, and your old input file
will be backed up. If this isn't the case, restart Unity and you should see the change.

## Removal

If you need to remove the plugin for whatever reason, simply do the following:

* Go to Tools > Purple Kingdom Games > Restore Input File (this will only be available if there is a backup file)
* Remove all files under Assets/Plugins/PurpleKingdomGames/InputManager

## Setting Up

Before any queries can be asked of the Input Manager, it first needs to be given a config. As the Input Manager is static
you will only need to do this once each time your game is started.

It's worth noting that all of the provided buttons are serializable, and therefore can be saved to a file for later 
retrieval if a developer wished to.

The below example creates a Keyboard layout where the left and the right arrows act as horizontal axis, the up and down 
arrows act as vertical axis, and space bar jumps:

```c#
// using PurpleKingdomGames.Unity.InputManager
InputManager.SetMap("Horizontal", new MultiButton() {
    new KeyboardButton(KeyCode.LeftArrow) { Invert = true },
    new KeyboardButton(KeyCode.RightArrow)
});

InputManager.SetMap("Vertical", new MultiButton() {
    new KeyboardButton(KeyCode.DownArrow) { Invert = true },
    new KeyboardButton(KeyCode.UpArrow)
});

InputManager.SetMap("Jump", new KeyboardButton(KeyCode.Space));
```

The following shows a configuration whereby the horizontal and vertical are controlled by Joystick 1,
and Jump is the left mouse click:

```c#
// using PurpleKingdomGames.Unity.InputManager
InputManager.SetMap("Horizontal", new JoystickAxis(1, 0));
InputManager.SetMap("Vertical", new JoystickAxis(1, 1));
InputManager.SetMap("Jump", new MouseButton(0));
```

You can also mix and match different inputs when combined with the [`MultiButton`](#multibutton) object. The below 
configuration combines the two previous ones to allow the player to move left or right using the Joystick or the Left and
Right arrows. They may also move up or down with the joystick or the arrows, and can jump using either space bar
or a left mouse click:

```c#
// using PurpleKingdomGames.Unity.InputManager
InputManager.SetMap("Horizontal", new MultiButton() {
    new JoystickAxis(1, 0),
    new KeyboardButton(KeyCode.LeftArrow) { Invert = true },
    new KeyboardButton(KeyCode.RightArrow)
});

InputManager.SetMap("Vertical", new MultiButton() {
    new JoystickAxis(1, 1)
    new KeyboardButton(KeyCode.DownArrow) { Invert = true },
    new KeyboardButton(KeyCode.UpArrow)
});

InputManager.SetMap("Jump", new MultiButton() {
    new MouseButton(0),
    new KeyboardButton(KeyCode.Space)
});
```

## Usage

Once the Input Manager is configured you can then ask it whether or not a key is pressed and the value of that key
in much the same way you would if you were using Unity's inbuilt API.

For example, the following detects whether a player should move left or right:

```c#
// using PurpleKingdomGames.Unity.InputManager
float horizontal = InputManager.GetCurrentValue("Horizontal");
if (horizontal > 0) {
    // Move right
} else if (horizontal < 0) {
    // Move left
}
```

And this script will detect whether a player should jump:

```c#
// using PurpleKingdomGames.Unity.InputManager
if (InputManager.IsDown("Jump")) {
    // Jump
}
```

## Integration with the UI

Because we've now added a layer to the input system the inbuilt Unity Standalone Input Module
for UI menus will no longer operate as expected. We've included a new Input Module for this purpose, which acts
in the same way, but uses the Purple Kingdom Input Manager.

To create a new Event system that has this Module attached, either right click on the heirarchy window or click 'GameObject'
in the top menu. From there select `UI > Purple Kingdom Event System`. This will create an new Game Object with an event 
system and a Purple Kingdom Input Module.

## Types

The Purple Kingdom Input Manager comes with a number of types that can be exteded as required

### IButton

This is the interface that defines which methods a button must implement.
All buttons must implement this interface, which has the following properties and methods:

#### `string Invert {get; set; }`

This property defines whether or not the input value will be inverted before being returned. For example, if we use a
left arrow to denote left movement, we may wish to invert it so that the value is always -1 when pressed (and
therefore can easily be used in position calculations)

#### `string Name { get; }`

The name of the button. Used for display purposes so that a player, user, or developer can know what a specific button is

#### `float GetCurrentValue()`

Returns the frame rate independant current value of the button. Mouse buttons and Keyboard buttons will always return a
value between 0 and 1 (or, if inverted, 0 and -1). Joysticks and mouse axis will return a value between -1 and 1.

If `Inverted` is set to `true` then this value will be the opposite to what the underlying API reports
(i.e. 1 becomes -1, -1 becomes 1)

#### `float GetCurrentRawValue()`

Returns the raw current value of the button. Mouse buttons and Keyboard buttons will always return either 0 or 1
(or, if inverted, 0 or -1). Joysticks and mouse axis will return a value between -1 and 1.

If `Inverted` is set to `true` then this value will be the opposite to what the underlying API reports
(i.e. 1 becomes -1, -1 becomes 1)

#### `bool IsDown()`

Returns `true` if a button was pressed down within the current update loop. In the case of Axis, this will be `true`
if the axis does not read 0

#### `bool IsUp()`

Returns `true` if a button was released within the current update loop. In the case of Axis, this will be `true`
if the axis reads 0

#### `bool IsHeld()`

Returns `true` if a button is pressed down. In the case of Axis, this will be `true` if the axis does not read 0

### KeyboardButton

A keyboard button represents any key on the keyboard that has a 
[KeyCode](http://docs.unity3d.com/ScriptReference/KeyCode.html). The contrustor takes a single `KeyCode` argument.

### MouseButton

A mouse button represents a button press on a mouse. The constructor takes a single `int` value, which can be
0 (for left button), 1 (for right button), 2 (for the middle button).

### MouseAxis

A mouse axis is the movement of the mouse. You can detect 2 directions of movement, X or Y, which is passed into the
constructor as a `PurpleKingdomGames.Unity.InputManager.MouseAxis.DirectionType`

As well as the usual interface methods and properties, this object also exposes the following:

#### `readonly DirectionType Direction`

This represents the direction that the button is currently assigned to

### JoystickAxis

A joystick axis is any movement on a specified Joystick axis (as defined in the custom [input config](#installation)).
The constructor takes 2 arguments, an `int` that represents the joystick number (between 1 and 11) and another
`int` representing the axis number (between 1 and 28)

As well as the usual interface methods and properties, this object also exposes the following:

#### `readonly int JoystickNumber`

This is the number of the joystick that the button is assigned to

#### `readonly int AxisNumber`

This is the number of the axis that the button is assigned to

### MultiButton

A multi-button can be used to group inputs together. For example, if you want your horizontal axis to take values from
the Joystick or Keyboard then you can [use a multi-button to achieve that] (#usage)

As well as the usual interface methods and properties, this object also exposes the following:

#### `int MaxLength { get; }`

The maximum number of buttons that can be assigned to the multi-button

#### `int Length { get; }`

The current number of buttons assigned to the multi-button

#### `void Add(IButton button)`

Adds a new button to the multi-button

#### `void Remove(IButton button)`

Removes a button from the multi-button

### CombiButton

A combi-button can be used to ensure that two or more buttons are receiving a value before reporting what that value is.
This allows you to do things like requiring that Shift *and* Space are held down to do a super jump, for example.

The nature of this button means that the current values will always be 0 until all buttons have been held, at which point
the maximum value will be returned (this will be inverted if `Inverted` is true).

#### `int MaxLength { get; }`

The maximum number of buttons that can be assigned to the combi-button

#### `int Length { get; }`

The current number of buttons assigned to the combi-button

#### `void Add(IButton button)`

Adds a new button to the combi-button

#### `void Remove(IButton button)`

Removes a button from the combi-button
