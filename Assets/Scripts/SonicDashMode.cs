using UnityEngine;

public class SonicDashMode : GameModeBase
{
    public float dashSpeed = 20f;

    private Vector3 direction = Vector3.forward;

    public override void Enter()
    {
        // Configurer caméra pour grande vitesse
        Camera.main.orthographic = false;
        Camera.main.transform.position = new Vector3(0, 5, -10);
        Camera.main.transform.rotation = Quaternion.Euler(15, 0, 0);
        cameraTransform = Camera.main.transform;

        // Activer contrôles dash
        controls.SetMode(PlayerController.Mode.Dash);

        // Générer environnement loops
        GenerateLoopEnvironment();
    }

    public override void UpdateMode()
    {
        base.UpdateMode();

        // Mouvement grande vitesse
        playerPosition += direction * dashSpeed * Time.deltaTime;
        player.position = playerPosition;

        // Caméra suit
        Camera.main.transform.position = new Vector3(playerPosition.x, 5, playerPosition.z - 10);

        // Générer loops/obstacles
        if (Random.value < 0.005f)
        {
            SpawnLoop();
        }
    }

    public override void Exit()
    {
        // Nettoyer
    }

    private void GenerateLoopEnvironment()
    {
        // Créer pistes, loops
    }

    private void SpawnLoop()
    {
        GameObject loop = obstaclePool.GetObject();
        loop.transform.position = new Vector3(playerPosition.x + 30, 0, playerPosition.z);
        loop.transform.parent = transform;
    }

    public void ChangeDirection(Vector3 newDir)
    {
        direction = newDir;
    }
}