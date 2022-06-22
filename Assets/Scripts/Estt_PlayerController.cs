using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estt_PlayerController : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    [SerializeField] private float walkSpeed ;
    [SerializeField] private float rotateSpeed ;
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        characterMove();
    }

    void characterMove(){
        animator.SetBool("Moving",false);
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        if(horizontal != 0 || vertical != 0){
            animator.SetBool("Moving",true);
            transform.Translate(new Vector3(0, 0, vertical) * (walkSpeed * Time.deltaTime));
            transform.Rotate(new Vector3(0, horizontal, 0) * (rotateSpeed * Time.deltaTime));
        }
    }
}
