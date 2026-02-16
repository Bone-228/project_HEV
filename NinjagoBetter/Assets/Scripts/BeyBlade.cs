using System;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class BeyBlade : MonoBehaviour
{
    [SerializeField]
    protected float moveSpeed = 5.0f;
    [SerializeField]
    protected float spinSpeed = 500f;
    [SerializeField]
    protected float weight = 1f;
    [SerializeField]
    protected int maxHealth = 100;

    public int CurrentHealth => currentHealth;
    public bool IsAlive => currentHealth >= 0;

    public event Action<BeyBlade> OnDeath;
    public event Action<BeyBlade, int> OnHealthChanged;


    protected Rigidbody rb;
    protected int currentHealth;
    protected bool canMove = true;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        rb.maxAngularVelocity = 1000f;
    }


    protected virtual void FixedUpdate()
    {
        if (!canMove || !IsAlive) return;

        Vector3 input = GetInput();
        Move(input);
        Rotate();
    }
    protected abstract Vector3 GetInput();

    protected virtual void Move(Vector3 input)
    {
        if (input.sqrMagnitude > 0)
        {
            rb.AddForce(input.normalized * moveSpeed * weight, ForceMode.Acceleration);
        }
    }


    protected virtual void Rotate()
    {
        rb.AddTorque(Vector3.up * spinSpeed * Time.deltaTime, ForceMode.Acceleration);
    }

    public virtual void TakeDamage(int dmg)
    {
        if (!IsAlive) return;


        currentHealth -= dmg;
        OnHealthChanged?.Invoke(this, currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        currentHealth = 0;
        canMove = false;

        OnDeath?.Invoke(this);

        gameObject.SetActive(false);
    }

    public void StopMovement()
    {
        canMove = false;
        rb.angularVelocity = Vector3.zero;
    }
}
