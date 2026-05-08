using UnityEngine;

public class JetpackJoyrideMode : GameModeBase
{
    public float jetpackForce = 15f;
    public float gravity = -20f;
    public float forwardSpeed = 8f;

    private Vector3 velocity;
    private bool jetpackActive = false;

    public override void Enter()
    {
        // Configurer caméra horizontale
        Camera.main.orthographic = false;
        Camera.main.transform.position = new Vector3(0, 0, -10);
        Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
        cameraTransform = Camera.main.transform;

        // Activer contrôles jetpack
        controls.SetMode(PlayerController.Mode.Jetpack);

        // Générer environnement horizontal
        GenerateHorizontalEnvironment();
    }

    public override void UpdateMode()
    {
        base.UpdateMode();

        // Mouvement horizontal automatique
        velocity.x = forwardSpeed;

        // Jetpack ou gravité
        if (jetpackActive)
        {
            velocity.y = jetpackForce;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        playerPosition += velocity * Time.deltaTime;
        player.position = playerPosition;

        // Limites verticales
        playerPosition.y = Mathf.Clamp(playerPosition.y, -5, 10);

        // Caméra suit horizontalement
        Camera.main.transform.position = new Vector3(playerPosition.x + 5, 0, -10);

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

    private void GenerateHorizontalEnvironment()
    {
        // Créer bâtiments, lasers, etc.
    }

    private void SpawnObstacle()
    {
        GameObject obs = obstaclePool.GetObject();
        obs.transform.position = new Vector3(playerPosition.x + 20, Random.Range(-3f, 3f), 0);
        obs.transform.parent = transform;
    }

    public void ActivateJetpack(bool active)
    {
        jetpackActive = active;
    }
}