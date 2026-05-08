using UnityEngine;

public class TempleRunMode : GameModeBase
{
    public float speed = 10f;

    private Vector3 direction = Vector3.forward;
    private float turnTimer = 0f;
    private bool isTurning = false;

    public override void Enter()
    {
        // Configurer caméra pour jungle avec virages
        Camera.main.transform.position = new Vector3(0, 10, 0);
        Camera.main.transform.rotation = Quaternion.Euler(45, 0, 0);
        cameraTransform = Camera.main.transform;

        // Activer contrôles virages
        controls.SetMode(PlayerController.Mode.TurnBased);

        // Générer environnement jungle
        GenerateJungleEnvironment();
    }

    public override void UpdateMode()
    {
        base.UpdateMode();

        // Mouvement selon direction
        playerPosition += direction * speed * Time.deltaTime;
        player.position = playerPosition;

        // Gestion des virages
        if (isTurning)
        {
            turnTimer += Time.deltaTime;
            if (turnTimer >= 1f) // Durée du virage
            {
                isTurning = false;
                turnTimer = 0f;
            }
        }

        // Caméra suit
        Camera.main.transform.position = new Vector3(playerPosition.x, 10, playerPosition.z);
    }

    public override void Exit()
    {
        // Nettoyer
    }

    private void GenerateJungleEnvironment()
    {
        // Instancier arbres, temples, chemins
    }

    public void Turn(int angle) // 90 ou -90
    {
        if (!isTurning)
        {
            isTurning = true;
            direction = Quaternion.Euler(0, angle, 0) * direction;
        }
    }
}