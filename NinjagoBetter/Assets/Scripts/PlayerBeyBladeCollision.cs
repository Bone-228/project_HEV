using UnityEngine;

public class PlayerBeyBladeCollision : MonoBehaviour
{
    public float knockback = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        BeyBlade self = GetComponent<BeyBlade>();
        BeyBlade other = collision.collider.GetComponent<BeyBlade>();

        if (self == null || other == null) return;

        Vector3 dir = (other.transform.position - transform.position).normalized;
        Rigidbody rb = other.GetComponent<Rigidbody>();
        rb.AddForce(dir * knockback * self.weight, ForceMode.Impulse);

        other.TakeDamage(10);
    }
}
