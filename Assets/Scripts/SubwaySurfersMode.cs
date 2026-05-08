using UnityEngine;

public class SubwaySurfersMode : GameModeBase
{
    public float speed = 10f;

    private int currentLane = 1; // 0: left, 1: middle, 2: right
    private float laneWidth = 3f;
    private float obstacleSpawnZ = 50f;

    public override void Enter()
    {
        // Configurer caméra 3D urbaine
        Camera.main.transform.position = new Vector3(0, 5, -10);
        Camera.main.transform.rotation = Quaternion.Euler(15, 0, 0);
        cameraTransform = Camera.main.transform;

        // Activer contrôles 3 voies
        controls.SetMode(PlayerController.Mode.ThreeLane);

        // Générer environnement urbain de base
        GenerateUrbanEnvironment();
    }

    public override void UpdateMode()
    {
        base.UpdateMode();

        // Mouvement automatique vers l'avant
        playerPosition.z += speed * Time.deltaTime;
        player.position = playerPosition;

        // Générer obstacles
        if (Random.value < 0.01f) // Probabilité de spawn
        {
            SpawnObstacle();
        }

        // Caméra suit le joueur
        Camera.main.transform.position = new Vector3(playerPosition.x, 5, playerPosition.z - 10);
    }

    public override void Exit()
    {
        // Nettoyer obstacles
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
                obstaclePool.ReturnObject(child.gameObject);
        }
    }

    private void GenerateUrbanEnvironment()
    {
        // Créer des bâtiments, routes, etc. (simplifié)
        // Ici, instancier prefabs pour environnement urbain
    }

    private void SpawnObstacle()
    {
        GameObject obs = obstaclePool.GetObject();
        obs.transform.position = new Vector3((currentLane - 1) * laneWidth, 0, playerPosition.z + obstacleSpawnZ);
        obs.transform.parent = transform;
    }

    public void ChangeLane(int direction) // -1 left, 1 right
    {
        currentLane = Mathf.Clamp(currentLane + direction, 0, 2);
        playerPosition.x = (currentLane - 1) * laneWidth;
    }
}