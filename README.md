# Card Match Game

## Description
A simple card-matching game prototype built using Unity. The game involves flipping cards, finding matching pairs, and progressing through various levels. It includes features like a combo system, save/load functionality, level selection, and sound effects.

---

## Features

- **Card Matching Gameplay**: Flip cards to find matching pairs.
- **Combo System**: Rewards players for consecutive matches.
- **Save/Load System**: Saves the game state and reloads it upon returning.
- **Level Selection**: Players can choose different levels, each with unique grid sizes and sprites.
- **Responsive UI**: Dynamic UI that adjusts to game states, including in-game and level selection menus.
- **Sound Effects**: Includes various sound effects for interactions like flipping cards, matches, mismatches, and game over.
- **Scriptable Objects**: Stores level data (rows, columns, and sprites) for easy customization and scalability.

---

## Installation

1. Clone or download this repository.
2. Open the project in Unity 2021 LTS or higher.
3. Import the required dependencies:
    - [DOTween](http://dotween.demigiant.com/) (for animations)
---

## How to Play

1. Launch the game.
2. Select a level from the level selection menu.
3. Flip cards to find matching pairs.
4. Complete the grid to win the game.

---

## Assets Used

- **UI Assets**: [Basic GUI Bundle by Penzilla](https://penzilla.itch.io/basic-gui-bundle?download).
- **Sound Effects**: [Freesound](https://freesound.org/).
    - Flip sound: "flip_sound" 
    - Match sound: "match_sound" 
    - Mismatch sound: "mismatch_sound" 
    - Game over sound: "gameover_sound"
- **Card Sprites**: Custom card designs stored in the `Sprites` folder.

---

## Scripts Overview

- **GameManager**: Handles grid generation, level setup, and game logic.
- **UIManager**: Manages all UI elements, including turns, matches, combo display, and game over screen.
- **CardController**: Manages card interactions, including flips, matches, and mismatches.
- **SaveLoadManager**: Handles saving and loading of game state using PlayerPrefs.
- **AudioManager**: Manages sound effects for different game interactions.
- **LevelData**: Scriptable Object to store level configurations (rows, columns, and card sprites).

---

## How to Add Levels

1. Create a new `LevelData` Scriptable Object (Right-click in the Project window > Create > Level Data).
2. Configure the grid size (rows, columns) and assign sprites.
3. Add the new `LevelData` to the Level Selection menu in the `UIManager`.
