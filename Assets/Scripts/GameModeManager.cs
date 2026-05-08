using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    public enum GameMode { SubwaySurfers, TempleRun, DoodleJump, JetpackJoyride, FlappyBird, AltoAdventure, GeometryDash, CrossyRoad, Vector, SonicDash }

    [SerializeField] private GameMode currentMode;

    [SerializeField] private Camera mainCamera;

    [SerializeField] private PlayerController playerController;

    [SerializeField] private Transform player;

    [SerializeField] private SubwaySurfersMode subwayMode;

    [SerializeField] private TempleRunMode templeMode;

    [SerializeField] private DoodleJumpMode doodleMode;

    [SerializeField] private JetpackJoyrideMode jetpackMode;

    [SerializeField] private FlappyBirdMode flappyMode;

    [SerializeField] private AltoAdventureMode altoMode;

    [SerializeField] private GeometryDashMode geoMode;

    [SerializeField] private CrossyRoadMode crossyMode;

    [SerializeField] private VectorMode vectorMode;

    [SerializeField] private SonicDashMode sonicMode;

    private float transitionDuration = 2f;
    [SerializeField] private float modeDuration = 12f;
    private float modeTimer = 0f;
    private bool isTransitioning = false;

    void Start()
    {
        subwayMode.SetPlayer(player);
        subwayMode.controls = playerController;
        templeMode.SetPlayer(player);
        templeMode.controls = playerController;
        doodleMode.SetPlayer(player);
        doodleMode.controls = playerController;
        jetpackMode.SetPlayer(player);
        jetpackMode.controls = playerController;
        flappyMode.SetPlayer(player);
        flappyMode.controls = playerController;
        altoMode.SetPlayer(player);
        altoMode.controls = playerController;
        geoMode.SetPlayer(player);
        geoMode.controls = playerController;
        crossyMode.SetPlayer(player);
        crossyMode.controls = playerController;
        vectorMode.SetPlayer(player);
        vectorMode.controls = playerController;
        sonicMode.SetPlayer(player);
        sonicMode.controls = playerController;

        TransitionTo(GameMode.SubwaySurfers);
    }

    public void TransitionTo(GameMode newMode)
    {
        if (isTransitioning || currentMode == newMode) return;

        isTransitioning = true;
        SoundManager.Instance?.PlayModeChangeSound();

        // Exit current mode
        if (currentMode == GameMode.SubwaySurfers) subwayMode.Exit();
        else if (currentMode == GameMode.TempleRun) templeMode.Exit();
        else if (currentMode == GameMode.DoodleJump) doodleMode.Exit();
        else if (currentMode == GameMode.JetpackJoyride) jetpackMode.Exit();
        else if (currentMode == GameMode.FlappyBird) flappyMode.Exit();
        else if (currentMode == GameMode.AltoAdventure) altoMode.Exit();
        else if (currentMode == GameMode.GeometryDash) geoMode.Exit();
        else if (currentMode == GameMode.CrossyRoad) crossyMode.Exit();
        else if (currentMode == GameMode.Vector) vectorMode.Exit();
        else if (currentMode == GameMode.SonicDash) sonicMode.Exit();

        currentMode = newMode;
        modeTimer = 0f;

        // Enter new mode
        if (currentMode == GameMode.SubwaySurfers)
        {
            subwayMode.Enter();
            playerController.SetMode(PlayerController.Mode.ThreeLane);
            UIManager.Instance?.UpdateMode("Subway Surfers");
        }
        else if (currentMode == GameMode.TempleRun)
        {
            templeMode.Enter();
            playerController.SetMode(PlayerController.Mode.TurnBased);
            UIManager.Instance?.UpdateMode("Temple Run");
        }
        else if (currentMode == GameMode.DoodleJump)
        {
            doodleMode.Enter();
            playerController.SetMode(PlayerController.Mode.VerticalJump);
            UIManager.Instance?.UpdateMode("Doodle Jump");
        }
        else if (currentMode == GameMode.JetpackJoyride)
        {
            jetpackMode.Enter();
            playerController.SetMode(PlayerController.Mode.Jetpack);
            UIManager.Instance?.UpdateMode("Jetpack Joyride");
        }
        else if (currentMode == GameMode.FlappyBird)
        {
            flappyMode.Enter();
            playerController.SetMode(PlayerController.Mode.Flappy);
            UIManager.Instance?.UpdateMode("Flappy Bird");
        }
        else if (currentMode == GameMode.AltoAdventure)
        {
            altoMode.Enter();
            playerController.SetMode(PlayerController.Mode.Slide);
            UIManager.Instance?.UpdateMode("Alto's Adventure");
        }
        else if (currentMode == GameMode.GeometryDash)
        {
            geoMode.Enter();
            playerController.SetMode(PlayerController.Mode.PreciseJump);
            UIManager.Instance?.UpdateMode("Geometry Dash");
        }
        else if (currentMode == GameMode.CrossyRoad)
        {
            crossyMode.Enter();
            playerController.SetMode(PlayerController.Mode.Isometric);
            UIManager.Instance?.UpdateMode("Crossy Road");
        }
        else if (currentMode == GameMode.Vector)
        {
            vectorMode.Enter();
            playerController.SetMode(PlayerController.Mode.Parkour);
            UIManager.Instance?.UpdateMode("Vector");
        }
        else if (currentMode == GameMode.SonicDash)
        {
            sonicMode.Enter();
            playerController.SetMode(PlayerController.Mode.Dash);
            UIManager.Instance?.UpdateMode("Sonic Dash");
        }

        // Lerp camera
        StartCoroutine(TransitionCoroutine());
    }

    private System.Collections.IEnumerator TransitionCoroutine()
    {
        Transform targetTransform = null;
        if (currentMode == GameMode.SubwaySurfers) targetTransform = subwayMode.cameraTransform;
        else if (currentMode == GameMode.TempleRun) targetTransform = templeMode.cameraTransform;
        else if (currentMode == GameMode.DoodleJump) targetTransform = doodleMode.cameraTransform;
        else if (currentMode == GameMode.JetpackJoyride) targetTransform = jetpackMode.cameraTransform;
        else if (currentMode == GameMode.FlappyBird) targetTransform = flappyMode.cameraTransform;
        else if (currentMode == GameMode.AltoAdventure) targetTransform = altoMode.cameraTransform;
        else if (currentMode == GameMode.GeometryDash) targetTransform = geoMode.cameraTransform;
        else if (currentMode == GameMode.CrossyRoad) targetTransform = crossyMode.cameraTransform;
        else if (currentMode == GameMode.Vector) targetTransform = vectorMode.cameraTransform;
        else if (currentMode == GameMode.SonicDash) targetTransform = sonicMode.cameraTransform;

        Vector3 startPos = mainCamera.transform.position;
        Quaternion startRot = mainCamera.transform.rotation;
        Vector3 endPos = targetTransform.position;
        Quaternion endRot = targetTransform.rotation;

        float elapsed = 0f;

        while (elapsed < transitionDuration)
        {
            mainCamera.transform.position = Vector3.Lerp(startPos, endPos, elapsed / transitionDuration);
            mainCamera.transform.rotation = Quaternion.Lerp(startRot, endRot, elapsed / transitionDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = endPos;
        mainCamera.transform.rotation = endRot;
        isTransitioning = false;
    }

    void Update()
    {
        if (currentMode == GameMode.SubwaySurfers) subwayMode.UpdateMode();
        else if (currentMode == GameMode.TempleRun) templeMode.UpdateMode();
        else if (currentMode == GameMode.DoodleJump) doodleMode.UpdateMode();
        else if (currentMode == GameMode.JetpackJoyride) jetpackMode.UpdateMode();
        else if (currentMode == GameMode.FlappyBird) flappyMode.UpdateMode();
        else if (currentMode == GameMode.AltoAdventure) altoMode.UpdateMode();
        else if (currentMode == GameMode.GeometryDash) geoMode.UpdateMode();
        else if (currentMode == GameMode.CrossyRoad) crossyMode.UpdateMode();
        else if (currentMode == GameMode.Vector) vectorMode.UpdateMode();
        else if (currentMode == GameMode.SonicDash) sonicMode.UpdateMode();

        if (!isTransitioning)
        {
            modeTimer += Time.deltaTime;
            if (modeTimer >= modeDuration)
            {
                GameMode next = (GameMode)((int)(currentMode + 1) % 10);
                TransitionTo(next);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameMode next = (GameMode)((int)(currentMode + 1) % 10);
                TransitionTo(next);
            }
        }
    }
}
