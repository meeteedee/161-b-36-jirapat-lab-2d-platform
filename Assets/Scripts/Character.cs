using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private int health;
    public int Health
    {
        get => health;
        private set
        {
            int clamped = Mathf.Max(0, value);
            if (clamped == health) return;

            health = clamped;
            OnHealthChanged?.Invoke(health, MaxHealth);

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public int MaxHealth { get; private set; }

    public event Action<int, int> OnHealthChanged;

    protected Animator anim;
    protected Rigidbody2D rb;

    public void Initialize(int startHealth)
    {
        MaxHealth = Mathf.Max(1, startHealth);
        health = MaxHealth;
        OnHealthChanged?.Invoke(health, MaxHealth);

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Debug.Log($"{name} is Initialized Health : {Health}/{MaxHealth}");
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0) return;
        Health = Health - damage;
        Debug.Log($"{name} took damaged {damage} Current Health:{Health}/{MaxHealth}");
    }

    public void Heal(int amount)
    {
        if (amount <= 0) return;
        Health = Mathf.Min(Health + amount, MaxHealth);
    }
}