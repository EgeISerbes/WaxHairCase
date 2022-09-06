using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hair : Interactable
{
    private System.Action onWaxCollission;
    [SerializeField] private Animator _animator;
    private bool _isWaxed = false;
   public  void Init(System.Action onCollision)
    {
        onWaxCollission = onCollision;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WaxBall") && !_isWaxed)
        {
            _isWaxed = true;
            onWaxCollission();
            State(false);
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("WaxBall"))
    //    {
    //        onWaxCollission();
    //        State(false);
    //    }
    //}
    public void State(bool hasStarted)
    {

        _animator.SetBool("hasStarted", hasStarted);
        
    }
}
