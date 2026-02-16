using UnityEngine;


public class PlayerBeyBladeCollision : MonoBehaviour
{
    [SerializeField]
    protected float knockback = 10f;
    [SerializeField]
    private int damage = 10;

    private BeyBlade self;

    private void Awake()
    {
        self = GetComponent<BeyBlade>();   
    }

    private void OnCollisionEnter(Collision collision)
    {
        BeyBlade other = collision.collider.GetComponent<BeyBlade>();

        if (self == null || other == null) return;

        Vector3 dir = (other.transform.position - transform.position).normalized;
        Rigidbody rb = other.GetComponent<Rigidbody>();
        rb.AddForce(dir * knockback * self.transform.localScale.magnitude, ForceMode.Impulse);

        other.TakeDamage(damage);
    }
}
