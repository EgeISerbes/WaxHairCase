using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Transform _C_Target;

    void Start()
    {
        transform.position = _C_Target.position;
    }


}
