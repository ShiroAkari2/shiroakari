# ShiroAkari's Project — Playable Prototype Foundation

This is a Unity 6 URP-ready **prototype codebase**, not a finished full open-world game.
It includes third-person movement, follow camera, interaction, collectibles, a simple rune puzzle,
NPC dialogue, a knowledge journal, quest tracking, and local save/load.

## Setup
1. Install Unity Hub and Unity 6 (any recent Unity 6 LTS release).
2. Create a new **3D (URP)** project named `ShiroAkarisProject`.
3. Close Unity, then copy this package's `Assets/` and `Packages/` folders into the project root.
   Merge folders if prompted.
4. Reopen the project and wait for scripts to compile.
5. In Unity, choose **Tools > ShiroAkari > Create Prototype Scene**.
6. Open `Assets/Scenes/WhisperingMeadow.unity` and press Play.

## Controls
- WASD / Arrow keys: move
- Mouse: look around
- Space: jump
- Left Shift: run
- E: interact
- Esc: unlock cursor

The prototype scene builder creates primitive placeholder art so the scene is playable immediately.
Replace primitives with original models, animation, sound, and designed environments during production.

## Important
- Uses Unity's built-in legacy Input Manager for maximum out-of-box compatibility. If the project is
  configured as Input System Package only, set **Active Input Handling** to **Both** or **Input Manager**
  in Project Settings > Player, then restart Unity.
- This is a foundation for a vertical slice, not a claim that the complete game is finished.
- Android support/build modules are installed through Unity Hub when preparing mobile builds.
