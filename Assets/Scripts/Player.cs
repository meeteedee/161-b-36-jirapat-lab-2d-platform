using UnityEngine;

public class Player : Character, IShootable
{
    [field: SerializeField] public GameObject Bullet { get; set; }
    [field: SerializeField] public Transform ShootPoint { get; set; }
    [field: SerializeField] public float ReloadTime { get; set; }
    [field: SerializeField] public float WaitTime { get; set; }

    void Start()
    {
        base.Initialize(100);
        ReloadTime = 1.0f;
        WaitTime = 0.0f;
    }
    private void FixedUpdate()
    {
        WaitTime += Time.fixedDeltaTime;
    }
    private void Update()
    {
        Shoot();
    }

    public void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && WaitTime >= ReloadTime)
        {
            var bullet = Instantiate(Bullet, ShootPoint.position, Quaternion.identity);
            Banana banana = bullet.GetComponent<Banana>();

            if (banana != null)
                banana.InitWeapon(20, this);
            WaitTime = 0.0f;
        }
    }

    public void OnHitWith(Enemy enemy)
    {
        TakeDamage(enemy.DamageHit);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            OnHitWith(enemy);
            Debug.Log($"{this.name} collides with {enemy.name}!");
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    
}
   
    

    

    

