using UnityEngine;
using UnityEngine.UIElements;

public abstract class BeyBlade : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float spinSpeed = 500f;
    public float weight = 1f;
    public int maxHealth = 100;

    protected int currentHealth;
    protected Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        Rotate();
    }

    protected virtual void FixedUpdate()
    {
        Vector3 input = GetInput();
        Move(input);
    }

    protected virtual void Move(Vector3 input)
    {
        if (input.sqrMagnitude > 0)
        {
            rb.AddForce(input.normalized * moveSpeed * weight, ForceMode.Acceleration);
        }
    }

    protected abstract Vector3 GetInput();

    protected virtual void Rotate()
    {
        rb.AddTorque(Vector3.up * spinSpeed * Time.deltaTime, ForceMode.Acceleration);
    }

    public virtual void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        gameObject.SetActive(false);
    }
}
