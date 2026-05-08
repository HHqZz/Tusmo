# TUSMO - Testing Guide

## Quick Start Testing

### 1. Editor Testing (Fastest)
```
1. Open Unity project
2. Create new scene: Assets > Create > Scene (name: GameScene)
3. Add to scene:
   - Empty GameObject "GameManager"
   - Empty GameObject "Player" (add Cube, tag as "Player")
   - Main Camera (position: 0, 5, -10)
   - Canvas (for UI)
4. Attach scripts:
   - GameModeManager → GameManager
   - PlayerController → GameManager
   - UIManager → Canvas
   - SoundManager → new GameObject "AudioManager"
5. Add all 10 mode GameObjects and assign in GameModeManager inspector
6. Create UI elements in Canvas:
   - Text: "Score: 0" (assign to UIManager.scoreText)
   - Text: "Mode: Starting..." (assign to UIManager.modeText)
   - Panel: "GameOver" (assign to UIManager.gameOverPanel)
7. Press Play and watch the console
```

### 2. What to Observe While Testing

**Mode Transitions:**
- Every 12 seconds, game switches to next mode
- Camera should smoothly lerp to new position
- Mode name updates in UI

**Scoring:**
- Score increments +1 every second
- Should increase steadily (1, 2, 3, 4...)
- Resets when collision occurs

**Audio Feedback:**
- Jump sound plays on character action
- Collision sound plays on impact
- Mode change sound plays on transition
- Background music loops continuously

**Input Testing (per mode):**
- SubwaySurfers: Arrow keys for lane switching (left/right)
- TempleRun: Arrow keys for turns (left/right)
- DoodleJump: Arrow keys or Space for jump
- JetpackJoyride: Up/Down for jetpack control
- FlappyBird: Space for flap
- AltoAdventure: Space for jump
- GeometryDash: Space for jump
- CrossyRoad: Arrow keys for movement
- Vector: Arrow keys for movement
- SonicDash: Automatic with up/down control

### 3. Android Testing

```bash
# In Unity Editor:
1. File > Build Settings
2. Select "Android" platform
3. Player Settings:
   - Company Name: Your Company
   - Product Name: Tusmo
   - Package Name: com.yourcompany.tusmo
   - Target API Level: 30+
4. Build and Run to device

# On device:
- Verify all 10 modes load
- Test touch/swipe input
- Monitor performance with Android Profiler
- Check for memory leaks
```

### 4. Performance Testing

**Monitor These Metrics:**
- FPS (target 60 on desktop, 30+ on mobile)
- Memory usage (track with Profiler)
- Frame time (should be < 16.67ms for 60 FPS)
- Object pooling (check active obstacle count)

**In Unity Editor:**
- Window > Analysis > Profiler
- Monitor CPU, Memory, and GPU usage
- Watch for frame spikes during transitions

### 5. Test Scenarios

| Scenario | Expected Result | Status |
|----------|-----------------|--------|
| Game starts | SubwaySurfers mode loads, score = 0 | ☐ |
| Wait 12s | Auto-transitions to TempleRun | ☐ |
| Score increments | +1 per second | ☐ |
| Collision occurs | GameOver panel shows, sound plays | ☐ |
| Mode name displays | Updates on each transition | ☐ |
| Input works | Mode-specific controls respond | ☐ |
| All 10 modes cycle | Complete cycle without crashing | ☐ |
| Camera transitions | Smooth lerp between views | ☐ |
| Audio plays | No missing or distorted sounds | ☐ |
| Mobile build | Runs on Android device | ☐ |

### 6. Debugging Common Issues

**Issue: Modes not transitioning**
- Check GameModeManager.cs Update() method
- Verify modeTimer increments
- Ensure isTransitioning flag resets

**Issue: Score not increasing**
- Verify UIManager is instantiated
- Check GameModeBase.UpdateMode() calls base.UpdateMode()
- Ensure scoreInterval is 1.0f

**Issue: No audio**
- Verify SoundManager instance exists
- Check AudioSource components have clips assigned
- Ensure volume levels aren't muted
- Test in standalone build (web audio may have restrictions)

**Issue: Performance drops**
- Reduce obstacle spawn rate (check mode Update methods)
- Verify object pooling is working (check active count)
- Profile to identify bottlenecks
- Lower quality settings for mobile

**Issue: Collision not detected**
- Verify Player has "Player" tag
- Check Obstacle has OnTriggerEnter
- Ensure colliders are set as triggers
- Verify Rigidbody is attached

### 7. Build Checklist

- [ ] All 10 modes implemented
- [ ] UIManager configured and visible
- [ ] SoundManager with audio clips
- [ ] Player controller responds to input
- [ ] Score system working
- [ ] Mode transitions smooth
- [ ] No console errors
- [ ] Performance acceptable (30+ FPS on target)
- [ ] Android build tested on device
- [ ] Touch input mapped correctly for mobile

### 8. Console Commands for Testing

Add to a debug script:
```csharp
// Force next mode
GameModeManager.Instance.TransitionTo(GameModeManager.GameMode.TempleRun);

// Set score
UIManager.Instance.UpdateScore(100);

// Show game over
UIManager.Instance.ShowGameOver();

// Test sound
SoundManager.Instance.PlayJumpSound();
```

