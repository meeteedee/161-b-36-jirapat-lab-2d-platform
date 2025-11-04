using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int damage;
    public IShootable Shooter;

    public abstract void Move();
    public abstract void OnHitWith(Character character);

    public void InitWeapon(int newDamage, IShootable newShooter)
    {
        damage = newDamage;
        Shooter = newShooter;
    }

    public int GetShootDirection()
    {
        if (Shooter == null || Shooter.ShootPoint == null || Shooter.ShootPoint.parent == null) return 1;
        float value = Shooter.ShootPoint.position.x - Shooter.ShootPoint.parent.position.x;
        return (value > 0) ? 1 : -1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var character = other.GetComponent<Character>();
        if (character == null) return;

        // กันโดนเจ้าของ
        if (Shooter is Component shooterComp)
        {
            var ownerChar = shooterComp.GetComponent<Character>();
            if (character == ownerChar) return;
        }

        OnHitWith(character);
        Destroy(gameObject);
    }
}