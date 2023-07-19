
public interface IWeapon
{
    bool IsAttacking {get;}
    float AttackDuration {get;}
    float AttackCooldown {get;}
    void OnAttackWeapon();

}
