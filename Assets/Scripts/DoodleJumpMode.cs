using UnityEngine;

public class DoodleJumpMode : GameModeBase
{
    public float jumpForce = 10f;
    public float gravity = -20f;

    private Vector3 velocity;
    private bool isGrounded = false;

    public override void Enter()
    {
        // Configurer caméra 2D verticale
        Camera.main.orthographic = true;
        Camera.main.orthographicSize = 5;
        Camera.main.transform.position = new Vector3(0, 0, -10);
        Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
        cameraTransform = Camera.main.transform;

        // Activer contrôles saut
        controls.SetMode(PlayerController.Mode.VerticalJump);

        // Générer environnement vertical
        GenerateVerticalEnvironment();
    }

    public override void UpdateMode()
    {
        base.UpdateMode();

        // Physique verticale
        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        playerPosition += velocity * Time.deltaTime;
        player.position = playerPosition;

        // Collision sol
        if (playerPosition.y <= 0)
        {
            playerPosition.y = 0;
            velocity.y = 0;
            isGrounded = true;
        }

        // Caméra suit verticalement
        Camera.main.transform.position = new Vector3(0, Mathf.Max(playerPosition.y - 2, 0), -10);

        // Générer plateformes
        if (Random.value < 0.005f)
        {
            SpawnPlatform();
        }
    }

    public override void Exit()
    {
        Camera.main.orthographic = false;
        // Nettoyer
    }

    private void GenerateVerticalEnvironment()
    {
        // Créer plateformes, ennemis
    }

    private void SpawnPlatform()
    {
        GameObject plat = obstaclePool.GetObject();
        plat.transform.position = new Vector3(Random.Range(-2f, 2f), playerPosition.y + 10, 0);
        plat.transform.parent = transform;
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