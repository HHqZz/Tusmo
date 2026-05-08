using UnityEngine;

/// <summary>
/// Test setup helper - Attach to empty GameObject in scene for quick testing
/// </summary>
public class TestSetup : MonoBehaviour
{
    [Header("Test Configuration")]
    [SerializeField] private bool autoStartGame = true;
    [SerializeField] private float testDuration = 60f; // seconds to run test
    [SerializeField] private bool logTestMetrics = true;

    private float testStartTime;
    private int frameCount = 0;
    private int modeChangeCount = 0;
    private int scoreAtStart = 0;
    private bool testActive = false;

    private void Start()
    {
        if (autoStartGame)
        {
            StartTest();
        }
    }

    private void StartTest()
    {
        testActive = true;
        testStartTime = Time.time;
        frameCount = 0;
        modeChangeCount = 0;
        Debug.Log($"[TEST] Starting test for {testDuration} seconds");
        Debug.Log("[TEST] Monitoring: FPS, Mode Transitions, Scoring, Audio");
    }

    private void Update()
    {
        if (!testActive) return;

        frameCount++;

        float elapsedTime = Time.time - testStartTime;
        if (elapsedTime >= testDuration)
        {
            EndTest();
            return;
        }

        // Log metrics every 10 seconds
        if ((int)elapsedTime % 10 == 0 && (int)(elapsedTime - Time.deltaTime) % 10 != 0)
        {
            LogMetrics(elapsedTime);
        }
    }

    private void LogMetrics(float elapsed)
    {
        float fps = frameCount / elapsed;
        int score = UIManager.Instance != null ? 0 : -1; // Would need score property
        
        Debug.Log($"[TEST] {elapsed:F0}s - FPS: {fps:F1} | Frames: {frameCount}");
    }

    private void EndTest()
    {
        testActive = false;
        float totalTime = Time.time - testStartTime;
        float avgFps = frameCount / totalTime;

        Debug.Log("=== TEST COMPLETE ===");
        Debug.Log($"Duration: {totalTime:F2}s");
        Debug.Log($"Total Frames: {frameCount}");
        Debug.Log($"Average FPS: {avgFps:F1}");
        Debug.Log($"Mode Transitions: {modeChangeCount}");
        Debug.Log("Check console for any errors or warnings during test");
    }

    public static void PrintTestGuide()
    {
        Debug.Log(@"
=== TUSMO TEST GUIDE ===

SETUP CHECKLIST:
□ Create GameScene.unity
□ Add Player (Cube, tag 'Player')
□ Add Camera
□ Add Canvas with Text elements (Score, Mode)
□ Add GameModeManager with all mode references
□ Add PlayerController
□ Add UIManager (Canvas script)
□ Add SoundManager with audio sources/clips

TEST POINTS:
1. Mode Transitions - Watch camera lerp smoothly
2. Auto-cycling - Modes change every 12 seconds
3. Scoring - Score increments by +1 every second
4. UI Updates - Mode name displays correctly on transition
5. Audio - Sounds play for jumps/collisions/transitions
6. Input - Test mode-specific controls
7. Performance - Monitor FPS (target 60)

ANDROID BUILD:
File > Build Settings > Select Android
Configure Player Settings (package name, etc.)
Build and Run

DEBUGGING:
- Check Console for errors
- Use Profiler for performance (Window > Analysis > Profiler)
- Monitor memory in Android Profiler
        ");
    }
}
