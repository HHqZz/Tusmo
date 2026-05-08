using UnityEngine;

public class GeometryDashMode : GameModeBase
{
    public float runSpeed = 8f;
    public float jumpForce = 12f;
    public float gravity = -25f;

    private Vector3 velocity;
    private bool isGrounded = true;

    public override void Enter()
    {
        // Configurer caméra side-scrolling
        Camera.main.orthographic = true;
        Camera.main.orthographicSize = 5;
        Camera.main.transform.position = new Vector3(0, 0, -10);
        Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
        cameraTransform = Camera.main.transform;

        // Activer contrôles sauts précis
        controls.SetMode(PlayerController.Mode.PreciseJump);

        // Générer environnement spikes
        GenerateSpikeEnvironment();
    }

    public override void UpdateMode()
    {
        base.UpdateMode();

        // Mouvement horizontal
        velocity.x = runSpeed;
        velocity.y += gravity * Time.deltaTime;

        playerPosition += velocity * Time.deltaTime;
        player.position = playerPosition;

        // Collision sol
        if (playerPosition.y <= 0)
        {
            playerPosition.y = 0;
            velocity.y = 0;
            isGrounded = true;
        }

        // Caméra suit
        Camera.main.transform.position = new Vector3(playerPosition.x + 5, 0, -10);

        // Générer obstacles
        if (Random.value < 0.01f)
        {
            SpawnObstacle();
        }
    }

    public override void Exit()
    {
        Camera.main.orthographic = false;
        // Nettoyer
    }

    private void GenerateSpikeEnvironment()
    {
        // Créer blocs, spikes
    }

    private void SpawnObstacle()
    {
        GameObject obs = obstaclePool.GetObject();
        obs.transform.position = new Vector3(playerPosition.x + 15, Random.Range(0f, 3f), 0);
        obs.transform.parent = transform;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            velocity.y = jumpForce;
            isGrounded = false;
            SoundManager.Instance?.PlayJumpSound();
        }
    }
}