using UnityEngine;

public class BeyBladeShield : MonoBehaviour
{
    [SerializeField]
    public float maxEndurance = 100f;

    [SerializeField]
    public float damageMultiplier = 2f;

    [SerializeField]
    public float knockbackForce = 15f;

    private float currentEndurance;

    void Start()
    {
        currentEndurance = maxEndurance;
    }

    void OnCollisionEnter(Collision collision)
    {
        Rigidbody enemyRb = collision.collider.GetComponent<Rigidbody>();

        if (enemyRb != null)
        {
            float impactSpeed = collision.relativeVelocity.magnitude;


            float damage = impactSpeed * damageMultiplier;


            TakeDamage(damage);


            Vector3 knockbackDirection = collision.transform.position - transform.position;
            knockbackDirection.y = 0;

            enemyRb.AddForce(knockbackDirection.normalized * knockbackForce, ForceMode.Impulse);
        }
    }

    private void TakeDamage(float amount)
    {
        currentEndurance -= amount;


        Debug.Log($"Štít dostal ránu o síle {amount:F1}! Zbývá výdrže: {Mathf.Max(currentEndurance, 0):F1}");

        if (currentEndurance <= 0)
        {
            DestroyShield();
        }
    }

    private void DestroyShield()
    {
        Destroy(gameObject);
    }
}

