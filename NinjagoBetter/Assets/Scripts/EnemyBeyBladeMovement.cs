using UnityEngine;

public class EnemyBeyBladeMovement : BeyBlade
{
    [SerializeField]
    public Transform playerTarget;

    [Range(0f, 1f)]
    public float inaccuracy = 0.3f;

    [SerializeField]
    public float enemySpeedMultiplier = 2.5f;


    [SerializeField] private float minChangeDirectionTime = 0.2f;
    [SerializeField] private float maxChangeDirectionTime = 0.8f;

    private Vector3 currentMoveDirection;
    private float timeToNextChange;
    private float timer;
    private bool initialized = false;

    private void InitializeEnemy()
    {
        if (playerTarget == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("BeyBlade");
            if (player != null) playerTarget = player.transform;
        }

        PickNewDirection();
        initialized = true;
    }

    protected override Vector3 GetInput()
    {
        if (!initialized) InitializeEnemy();
        if (playerTarget == null) return Vector3.zero;

        timer += Time.fixedDeltaTime;

        if (timer >= timeToNextChange)
        {
            PickNewDirection();
        }

        return currentMoveDirection * enemySpeedMultiplier;
    }

    private void PickNewDirection()
    {
        if (playerTarget == null) return;


        Vector3 directionToPlayer = (playerTarget.position - transform.position).normalized;


        Vector3 randomOffset = new Vector3(
            Random.Range(-inaccuracy, inaccuracy),
            0f,
            Random.Range(-inaccuracy, inaccuracy)
        );


        currentMoveDirection = (directionToPlayer + randomOffset).normalized;
        currentMoveDirection.y = 0;


        timeToNextChange = Random.Range(minChangeDirectionTime, maxChangeDirectionTime);
        timer = 0f;
    }
}



