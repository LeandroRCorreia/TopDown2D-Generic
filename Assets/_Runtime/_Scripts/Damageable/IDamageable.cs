
public interface IDamageable
{
    public event System.Action OnTakeDamageEvent;
    public bool IsInvincible {get;}
    void TakingDamage();

}
