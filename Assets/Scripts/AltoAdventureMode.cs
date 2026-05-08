using UnityEngine;

public class AltoAdventureMode : GameModeBase
{
    public float slideSpeed = 10f;
    public float jumpForce = 8f;
    public float gravity = -18f;

    private Vector3 velocity;
    private bool isGrounded = true;

    public override void Enter()
    {
        // Configurer caméra pour glisse
        Camera.main.orthographic = false;
        Camera.main.transform.position = new Vector3(0, 5, -10);
        Camera.main.transform.rotation = Quaternion.Euler(15, 0, 0);
        cameraTransform = Camera.main.transform;

        // Activer contrôles glisse
        controls.SetMode(PlayerController.Mode.Slide);

        // Générer environnement montagneux
        GenerateMountainEnvironment();
    }

    public override void UpdateMode()
    {
        base.UpdateMode();

        // Mouvement automatique glisse
        velocity.z = slideSpeed;
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
        Camera.main.transform.position = new Vector3(playerPosition.x, 5, playerPosition.z - 5);

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

    private void GenerateMountainEnvironment()
    {
        // Créer montagnes, neige
    }

    private void SpawnObstacle()
    {
        GameObject obs = obstaclePool.GetObject();
        obs.transform.position = new Vector3(Random.Range(-2f, 2f), 0, playerPosition.z + 20);
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