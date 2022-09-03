using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    private CharacterMovement _c_Movement;
    public float VelocityZ
    {
        get => _c_Movement.velocityZ;
        set => _c_Movement.velocityZ = value;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
