using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingDamage : MonoBehaviour
{

    [field: SerializeField] public TextMeshProUGUI floatingDamageText {get; set;}
    [SerializeField] private float speedY;
    [SerializeField] [Range(0.15f, 2f)] private float maxLifeTime;
    private float finalTime = -1;

    private void Start() 
    {
        finalTime = Time.time + maxLifeTime;
    }

    void Update()
    {   
        transform.position += Vector3.up * speedY * Time.deltaTime;
        

        if(Time.time >= finalTime)
        {
            //TODO: OBJ POOL RETURN TO POOL
            Destroy(gameObject);
        }
    }

}
