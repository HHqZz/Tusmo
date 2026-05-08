# Tusmo - Endless Runner Game

A Unity-based endless runner featuring 10 different game modes with smooth transitions.

## Supported Modes

1. **Subway Surfers** - 3-lane side-scroller
2. **Temple Run** - Turn-based maze runner
3. **Doodle Jump** - Vertical platformer
4. **Jetpack Joyride** - Horizontal flight
5. **Flappy Bird** - Pipe navigation
6. **Alto's Adventure** - Slope skiing
7. **Geometry Dash** - Precision platformer
8. **Crossy Road** - Isometric movement
9. **Vector** - Parkour runner
10. **Sonic Dash** - High-speed loops

## Quick Start

### Prerequisites
- Unity 2021.3 LTS or later
- TextMeshPro (comes with Unity)

### Setup Instructions

1. **Clone or open this project in Unity Editor**
   ```bash
   # Clone if not already done
   git clone https://github.com/HHqZz/Tusmo.git
   cd Tusmo
   
   # Open in Unity
   ```

2. **Run setup script (optional)**
   ```bash
   chmod +x setup.sh
   ./setup.sh
   ```

3. **Create Game Scene**
   - File → New Scene (name: GameScene)
   - Save to `Assets/Scenes/GameScene.unity`

4. **Set up GameObjects**
   - Create empty GameObject "GameManager"
   - Create Cube "Player" and tag it as "Player"
   - Main Camera (position: 0, 5, -10)
   - Canvas for UI

5. **Attach Scripts**
   - `GameModeManager.cs` → GameManager
   - `PlayerController.cs` → GameManager  
   - `UIManager.cs` → Canvas
   - `SoundManager.cs` → new "AudioManager" GameObject
   - Each mode script → corresponding GameObject

6. **Configure References**
   - In GameModeManager inspector: assign all mode GameObjects
   - In UIManager: assign Canvas Text elements
   - In SoundManager: assign AudioSources and clips

7. **Play!**
   - Press Play button
   - Game auto-cycles through modes every 12 seconds
   - Score increments by +1 per second

## Controls

| Mode | Control |
|------|---------|
| Subway Surfers | ← → (Lane change) |
| Temple Run | ← → (Turn) |
| Doodle Jump | SPACE (Jump) |
| Jetpack Joyride | ↑ ↓ (Hover) |
| Flappy Bird | SPACE (Flap) |
| Alto's Adventure | SPACE (Jump) |
| Geometry Dash | SPACE (Jump) |
| Crossy Road | ← → ↑ ↓ (Move) |
| Vector | ← → ↑ ↓ (Move) |
| Sonic Dash | ↑ ↓ (Control) |

## Features

✅ 10 unique game modes
✅ Smooth camera transitions  
✅ Automatic mode cycling (12s per mode)
✅ Score system (+1 per second)
✅ UI displays current mode and score
✅ Sound effects and background music
✅ Object pooling for performance
✅ Mobile-optimized (Android target)

## Project Structure

```
Tusmo/
├── Assets/
│   ├── Scripts/
│   │   ├── GameModeBase.cs          (Abstract base for modes)
│   │   ├── GameModeManager.cs       (Mode transitions & cycling)
│   │   ├── PlayerController.cs      (Input handling)
│   │   ├── UIManager.cs             (Score & mode display)
│   │   ├── SoundManager.cs          (Audio management)
│   │   ├── PerformanceManager.cs    (FPS monitoring)
│   │   ├── ObjectPool.cs            (Obstacle pooling)
│   │   └── *Mode.cs                 (10 mode implementations)
│   ├── Scenes/
│   └── Audio/
├── ProjectSettings/                 (Unity project config)
├── TESTING.md                       (Testing guide)
├── README.md                        (This file)
└── setup.sh                         (Setup helper)
```

## Testing

See [TESTING.md](TESTING.md) for comprehensive testing guide including:
- Editor testing setup
- Android build instructions
- Performance monitoring
- Debugging tips
- Test scenarios

## Mobile Build

```
1. File → Build Settings
2. Select Android platform
3. Configure Player Settings
4. Build and Run
```

## Architecture

- **GameModeBase**: Abstract base class for all modes
- **GameModeManager**: Handles transitions and automatic cycling
- **PlayerController**: Dispatches input to active mode
- **UIManager**: Updates score and mode display
- **SoundManager**: Plays effects and background music
- **ObjectPool**: Reuses obstacle GameObjects for performance

## Performance

- **Desktop**: Target 60 FPS
- **Mobile**: Target 30+ FPS
- Uses object pooling to prevent memory spikes
- Dynamic quality adjustment based on FPS
- Optimized for Android

## Known Considerations

- Audio clips must be assigned in inspector
- UI Canvas must use TextMeshPro
- Player GameObject must have "Player" tag
- Obstacles must have Collider set as trigger
- Each mode needs a Camera Transform reference

## Troubleshooting

**No audio?**
- Verify SoundManager has AudioSource components
- Check audio clips are assigned
- Volume may be muted

**Modes not transitioning?**
- Check modeTimer in GameModeManager.Update()
- Verify modeDuration is set (default 12s)
- Look for isTransitioning flag stuck as true

**Score not updating?**
- Ensure all modes call `base.UpdateMode()` in their UpdateMode()
- Check UIManager instance is created
- Verify scoreInterval is 1.0f

## License

See LICENSE file for details

## Next Steps

After running the game:
1. Customize mode durations in GameModeManager
2. Add sound effects and music to SoundManager
3. Design custom visuals for each mode
4. Add power-ups or scoring multipliers
5. Build and deploy to Android

---

For detailed setup and testing instructions, see **TESTING.md**
