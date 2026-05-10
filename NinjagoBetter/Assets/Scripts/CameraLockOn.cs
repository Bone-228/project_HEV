using UnityEngine;

public class CameraLockOn : MonoBehaviour
{
    public Transform player;
    public Transform enemyTarget;

    public float distance = 7f;
    public float height = 4f;
    public float smoothTime = 0.15f;

    private Vector3 currentVelocity = Vector3.zero;

    void Start()
    {
        transform.SetParent(null);
    }

    void FixedUpdate()
    {
        if (player == null || enemyTarget == null) return;

        Vector3 dirFromEnemy = (player.position - enemyTarget.position);
        dirFromEnemy.y = 0;
        dirFromEnemy.Normalize();

        Vector3 targetPos = player.position + (dirFromEnemy * distance) + (Vector3.up * height);

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref currentVelocity, smoothTime);
    }

    void LateUpdate()
    {
        if (player == null || enemyTarget == null) return;

        Vector3 lookAtPoint = (player.position + enemyTarget.position) / 2f;
        transform.LookAt(lookAtPoint + Vector3.up);
    }
}