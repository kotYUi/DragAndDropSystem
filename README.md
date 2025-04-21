Unity Drag and Drop Card System
![Demo Gif](https://media.giphy.com/media/v1.Y2lkPTc5MGI3NjExcW5xZ2V6d2V5eGx6dWl5ZzB0b2VqZzJ6eGZ1Z2JqZzB0b2VqZzJ6eGZ1Z2JqZzB0b2VqZzJ6eGZ1Z2JqZzB0b2VqZw/giphy.gif)

Project Description
A simple and convenient Drag and Drop mechanism for maps (or other objects) in Unity. The system allows you to:

Dragging objects with a smooth return to their original position

Determine the zone where the card can be dropped

Add tolerance for a more convenient drop

Great for card games, inventory games, or any other systems where intuitive drag and drop is needed.

How it works
OnMouseDown – picks up the object and remembers the initial position.

OnMouseDrag – moves the object behind the cursor

onMouseUp – checks whether the object is in the drop zone, and either fixes it there or returns it back.

Smooth return – if the object is not in the zone, it smoothly returns to its place.

⚙️ Settings
In the inspector, you can configure:

returnDuration  – the time to return to the starting position.

dragHeight  – the height of the lift when dragging

dropTolerance  – tolerance for the drop zone (reduces the rigidity of the check)

Drag Zone – an object where you can drop the map.

Possible improvements
Add support for UI Canvas (via RectTransform)

Implement events (OnDragStart, OnDropSuccess, OnReturn)

Optimize for multi-touch control
