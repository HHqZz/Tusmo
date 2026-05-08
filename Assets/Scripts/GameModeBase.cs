using UnityEngine;

public abstract class GameModeBase : MonoBehaviour
{
    public Transform cameraTransform;
    public PlayerController controls;
    public ObjectPool obstaclePool;
    public float speed = 10f;

    protected Transform player;
    protected Vector3 playerPosition;
    private float scoreTimer = 0f;
    private const float scoreInterval = 1f; // score every second

    public virtual void SetPlayer(Transform p)
    {
        player = p;
    }

    public abstract void Enter();

    public virtual void UpdateMode()
    {
        scoreTimer += Time.deltaTime;
        if (scoreTimer >= scoreInterval)
        {
            UIManager.Instance?.AddScore(1);
            scoreTimer = 0f;
        }
    }

    public abstract void Exit();
}
