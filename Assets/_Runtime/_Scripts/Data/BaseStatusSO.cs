using UnityEngine;

[CreateAssetMenu(fileName = "BaseStatus", menuName = "ScriptableObjects/Status")]
public class BaseStatusSO : ScriptableObject
{
    [SerializeField] private DamageStatus damageStatus;
    [SerializeField] private DefensiveStatus defenseStatus;

    public int Strength => damageStatus.Strength;
    public float CritChance => damageStatus.critChance;
    public float DamamageMultiplier => damageStatus.critDamageMultiplier;

    public int MaxHealth => defenseStatus.maxHealth;
    public float Defense => defenseStatus.defense;

    public float PercentageMitigatedDefense => Defense / (Defense + 100);


}
