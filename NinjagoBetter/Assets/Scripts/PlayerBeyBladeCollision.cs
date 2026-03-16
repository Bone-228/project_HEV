using UnityEngine;


public class PlayerBeyBladeCollision : MonoBehaviour
{
    [SerializeField]
    protected float knockback = 10f;
    [SerializeField]
    private float damageMultiplier = 2f;
    private int minDamage = 1;

    private BeyBlade self;

    private void Awake()
    {
        self = GetComponent<BeyBlade>();   
    }

    private void OnCollisionEnter(Collision collision)
    {
        BeyBlade other = collision.collider.GetComponent<BeyBlade>();

        if (self == null || other == null) return;

        float impactForce = collision.relativeVelocity.magnitude;
        int calculatedDamage = Mathf.RoundToInt(impactForce * damageMultiplier);

        calculatedDamage = Mathf.Max(calculatedDamage, minDamage);

        Vector3 dir = (other.transform.position - transform.position).normalized;
        Rigidbody rb = other.GetComponent<Rigidbody>();
        rb.AddForce(dir * knockback * self.transform.localScale.magnitude, ForceMode.Impulse);



        other.TakeDamage(calculatedDamage);
    }
}
