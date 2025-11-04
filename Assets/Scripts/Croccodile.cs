using UnityEngine;
public class Croccodile : Enemy , IShootable
{
    public Player player; //target to atk
    
    [field: SerializeField] public GameObject Bullet { get; set; }
    [field: SerializeField] public Transform ShootPoint { get; set; }
     public float ReloadTime { get; set; }
     public float WaitTime { get; set; }
    
    [SerializeField] private float atkRange ;
    void Start()
    {
        base.Initialize(50);
        DamageHit = 30;
        //set atk range and target
        atkRange = 6.0f;
        player = GameObject.FindFirstObjectByType<Player>();

        WaitTime = 0.0f;
        ReloadTime = 5.0f;
    }
 
    private void FixedUpdate()
    {
        WaitTime += Time.fixedDeltaTime;
        Behavior();
    }
    public override void Behavior()
    {
        Vector2 distance = transform.position - player.transform.position;

        if (distance.magnitude <= atkRange && WaitTime >= ReloadTime)
        {
            Debug.Log($"{player.name} is in the {this.name}'s atk range!");
            Shoot();
        }
    }


    

    public void Shoot() 
    {
        anim.SetTrigger("Shoot");
        var bullet = Instantiate(Bullet, ShootPoint.position, Quaternion.identity);
        Rock rock = bullet.GetComponent<Rock>();
        rock.InitWeapon(30, this);
        WaitTime = 0;
    }
}
