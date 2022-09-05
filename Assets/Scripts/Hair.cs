using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hair : Interactable
{
    private System.Action onWaxCollission;
    

   public  void Init(System.Action onCollision)
    {
        onWaxCollission = onCollision;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("WaxBall"))
        {
            onWaxCollission();

        }
    }
}
