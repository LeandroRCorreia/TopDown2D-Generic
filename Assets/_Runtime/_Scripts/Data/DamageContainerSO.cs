using UnityEngine;

[CreateAssetMenu(fileName = "DamageContainer", menuName = "ScriptableObjects/DamageContainer")]
public class DamageContainerSO : ScriptableObject
{
    [SerializeField] private DamageStatus damageParams;

    public DamageStatus DamageParameters => damageParams;

}

[System.Serializable]
public struct DamageStatus
{
    public int Strength;
    [Range(0f, 1f)] public float critChance;
    public float critDamageMultiplier;
}

[System.Serializable]
public struct DefensiveStatus
{
    public int maxHealth;
    public int defense;
    
}
