using System;
using UnityEngine;

public class Rock : Weapon
{
    public Rigidbody2D rb;

    public Vector2 force;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Move()
    {
        rb.AddForce(force);
    }

    public override void OnHitWith(Character obj)
    {
        if (obj is Player)
        {
            obj.TakeDamage(this.damage);
        }
    }

    void Start()
    {
        damage = 40;
        force = new Vector2(GetShootDirection() * 90, 400);
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
