using UnityEngine;

public class EnemyBeyBladeMovement : BeyBlade
{
    [SerializeField]
    private float minChangeDirectionTime = 0.5f;
    [SerializeField]
    private float maxChangeDirectionTime = 2.5f;

    private Vector3 currentMoveDirection;
    private float timeToNextChange;
    private float timer;

    private void Awake()
    {
        PickNewDirection();
    }

    protected override Vector3 GetInput()
    {
        timer += Time.fixedDeltaTime;

        if (timer >= timeToNextChange)
        {
            PickNewDirection();
        }

        return currentMoveDirection;
    }

    private void PickNewDirection()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);

        currentMoveDirection = new Vector3(randomX, 0, randomZ).normalized;

        timeToNextChange = Random.Range(minChangeDirectionTime, maxChangeDirectionTime);

        timer = 0f;
    }
}
