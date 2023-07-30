using UnityEngine;

[CreateAssetMenu(fileName = "MovementParams", menuName = "ScriptableObjects/MovementParams")]
public class MovementParams : ScriptableObject
{

    [SerializeField] [Range(0.5f, 10)] private float maxSpeed = 5;
    [SerializeField] [Range(20f, 80f)] private float acceleration = 60f;



    public float MaxSpeed => maxSpeed;

    public float Acceleration => acceleration;


}
