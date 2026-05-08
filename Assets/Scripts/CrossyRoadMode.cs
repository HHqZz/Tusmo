using UnityEngine;

public class CrossyRoadMode : GameModeBase
{
    public float moveSpeed = 5f;

    private Vector3 targetPosition;
    private bool isMoving = false;

    public override void Enter()
    {
        // Configurer caméra isométrique
        Camera.main.orthographic = true;
        Camera.main.orthographicSize = 10;
        Camera.main.transform.position = new Vector3(0, 10, -10);
        Camera.main.transform.rotation = Quaternion.Euler(30, 0, 0);
        cameraTransform = Camera.main.transform;

        // Activer contrôles case par case
        controls.SetMode(PlayerController.Mode.Isometric);

        // Générer environnement isométrique
        GenerateIsometricEnvironment();
    }

    public override void UpdateMode()
    {
        base.UpdateMode();

        if (isMoving)
        {
            player.position = Vector3.MoveTowards(player.position, targetPosition, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(player.position, targetPosition) < 0.1f)
            {
                player.position = targetPosition;
                isMoving = false;
            }
        }

        // Caméra suit
        Camera.main.transform.position = new Vector3(playerPosition.x, 10, playerPosition.z - 5);
    }

    public override void Exit()
    {
        Camera.main.orthographic = false;
        // Nettoyer
    }

    private void GenerateIsometricEnvironment()
    {
        // Créer routes, rivières, trains
    }

    public void MoveTo(Vector3 direction)
    {
        if (!isMoving)
        {
            targetPosition = playerPosition + direction;
            isMoving = true;
        }
    }
}