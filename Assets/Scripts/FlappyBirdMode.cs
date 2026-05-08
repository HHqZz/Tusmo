using UnityEngine;

public class FlappyBirdMode : GameModeBase
{
    public float flapForce = 5f;
    public float gravity = -9.8f;
    public float forwardSpeed = 3f;

    private Vector3 velocity;

    public override void Enter()
    {
        // Configurer caméra 2D side-scrolling
        Camera.main.orthographic = true;
        Camera.main.orthographicSize = 5;
        Camera.main.transform.position = new Vector3(0, 0, -10);
        Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
        cameraTransform = Camera.main.transform;

        // Activer contrôles flap
        controls.SetMode(PlayerController.Mode.Flappy);

        // Générer environnement pipes
        GenerateFlappyEnvironment();
    }

    public override void UpdateMode()
    {
        base.UpdateMode();

        // Mouvement horizontal
        velocity.x = forwardSpeed;
        velocity.y += gravity * Time.deltaTime;

        playerPosition += velocity * Time.deltaTime;
        player.position = playerPosition;

        // Caméra suit
        Camera.main.transform.position = new Vector3(playerPosition.x, 0, -10);

        // Générer pipes
        if (Random.value < 0.005f)
        {
            SpawnPipe();
        }
    }

    public override void Exit()
    {
        Camera.main.orthographic = false;
        // Nettoyer
    }

    private void GenerateFlappyEnvironment()
    {
        // Créer fond, etc.
    }

    private void SpawnPipe()
    {
        // Spawn upper and lower pipes
        GameObject pipeUp = obstaclePool.GetObject();
        GameObject pipeDown = obstaclePool.GetObject();
        float gapY = Random.Range(-2f, 2f);
        pipeUp.transform.position = new Vector3(playerPosition.x + 15, gapY + 3, 0);
        pipeDown.transform.position = new Vector3(playerPosition.x + 15, gapY - 3, 0);
        pipeUp.transform.parent = transform;
        pipeDown.transform.parent = transform;
    }

    public void Flap()
    {
        velocity.y = flapForce;
        SoundManager.Instance?.PlayJumpSound();
    }
}