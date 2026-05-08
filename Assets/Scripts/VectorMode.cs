using UnityEngine;

public class VectorMode : GameModeBase
{
    public float runSpeed = 10f;
    public float jumpForce = 10f;
    public float gravity = -20f;

    private Vector3 velocity;
    private bool isGrounded = true;

    public override void Enter()
    {
        // Configurer caméra pour parkour
        Camera.main.orthographic = false;
        Camera.main.transform.position = new Vector3(0, 5, -10);
        Camera.main.transform.rotation = Quaternion.Euler(15, 0, 0);
        cameraTransform = Camera.main.transform;

        // Activer contrôles parkour
        controls.SetMode(PlayerController.Mode.Parkour);

        // Générer environnement parkour
        GenerateParkourEnvironment();
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
        Camera.main.transform.position = new Vector3(playerPosition.x + 5, 5, -10);

        // Générer obstacles
        if (Random.value < 0.01f)
        {
            SpawnObstacle();
        }
    }

    public override void Exit()
    {
        // Nettoyer
    }

    private void GenerateParkourEnvironment()
    {
        // Créer murs, sauts
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