
public interface IDamageable
{
    CurrentStatus EntityStatus {get; }
    
    event System.Action<DamageInfo> OnTakeDamageEvent;
    bool IsInvincible {get;}
    void TakingDamage(in IssuerDamageInfo damageInfo);


}
