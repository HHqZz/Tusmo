using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum Mode { ThreeLane, TurnBased, VerticalJump, Jetpack, Flappy, Slide, PreciseJump, Isometric, Parkour, Dash }

    private Mode currentMode;
    private SubwaySurfersMode subwayMode;
    private TempleRunMode templeMode;
    private DoodleJumpMode doodleMode;
    private JetpackJoyrideMode jetpackMode;
    private FlappyBirdMode flappyMode;
    private AltoAdventureMode altoMode;
    private GeometryDashMode geoMode;
    private CrossyRoadMode crossyMode;
    private VectorMode vectorMode;
    private SonicDashMode sonicMode;

    void Start()
    {
        // Assigner les modes via Find ou inspector
        subwayMode = FindObjectOfType<SubwaySurfersMode>();
        templeMode = FindObjectOfType<TempleRunMode>();
        doodleMode = FindObjectOfType<DoodleJumpMode>();
        jetpackMode = FindObjectOfType<JetpackJoyrideMode>();
        flappyMode = FindObjectOfType<FlappyBirdMode>();
        altoMode = FindObjectOfType<AltoAdventureMode>();
        geoMode = FindObjectOfType<GeometryDashMode>();
        crossyMode = FindObjectOfType<CrossyRoadMode>();
        vectorMode = FindObjectOfType<VectorMode>();
        sonicMode = FindObjectOfType<SonicDashMode>();
    }

    public void SetMode(Mode mode)
    {
        currentMode = mode;
    }

    void Update()
    {
        switch (currentMode)
        {
            case Mode.ThreeLane:
                // Swipe ou touches pour changer voie
                if (Input.GetKeyDown(KeyCode.LeftArrow) || SwipeLeft())
                {
                    subwayMode.ChangeLane(-1);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow) || SwipeRight())
                {
                    subwayMode.ChangeLane(1);
                }
                break;
            case Mode.TurnBased:
                // Contrôles pour tourner
                if (Input.GetKeyDown(KeyCode.LeftArrow) || SwipeLeft())
                {
                    templeMode.Turn(-90);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow) || SwipeRight())
                {
                    templeMode.Turn(90);
                }
                break;
            case Mode.VerticalJump:
                // Contrôles pour saut vertical
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetMouseButtonDown(0))
                {
                    doodleMode.Jump();
                }
                break;
            case Mode.Jetpack:
                // Contrôles jetpack
                if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.UpArrow))
                {
                    jetpackMode.ActivateJetpack(true);
                }
                else
                {
                    jetpackMode.ActivateJetpack(false);
                }
                break;
            case Mode.Flappy:
                // Contrôles flap
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    flappyMode.Flap();
                }
                break;
            case Mode.Slide:
                // Contrôles glisse
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    altoMode.Jump();
                }
                break;
            case Mode.PreciseJump:
                // Contrôles sauts précis
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    geoMode.Jump();
                }
                break;
            case Mode.Isometric:
                // Contrôles isométriques
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    crossyMode.MoveTo(Vector3.forward);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    crossyMode.MoveTo(Vector3.back);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    crossyMode.MoveTo(Vector3.left);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    crossyMode.MoveTo(Vector3.right);
                }
                break;
            case Mode.Parkour:
                // Contrôles parkour
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    vectorMode.Jump();
                }
                break;
            case Mode.Dash:
                // Contrôles dash
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    sonicMode.ChangeDirection(Vector3.left);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    sonicMode.ChangeDirection(Vector3.right);
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    sonicMode.ChangeDirection(Vector3.forward);
                }
                break;
        }
    }

    private bool SwipeLeft()
    {
        // Implémentation swipe simplifiée (pour mobile, utiliser Input.touches)
        return Input.GetMouseButtonDown(0) && Input.mousePosition.x < Screen.width / 2;
    }

    private bool SwipeRight()
    {
        return Input.GetMouseButtonDown(0) && Input.mousePosition.x > Screen.width / 2;
    }
}
