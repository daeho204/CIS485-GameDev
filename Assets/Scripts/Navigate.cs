using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Navigate : MonoBehaviour
{
    public NavMeshAgent nav;
    public GameObject player;

    public int range;
    
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {   
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        Idle();


    }

    // Update is called once per frame
    void Update()
    {
    float dist = Vector3.Distance(this.transform.position, player.transform.position);

    if(dist < 4) {
        nav.destination = player.transform.position;
        Attack();
    
    } 
    else if ((4 < dist) && (dist < 10)) {
        nav.destination = player.transform.position;
        Run();
    } 
    else if ((10 < dist) && (dist < range)) {
       nav.destination = player.transform.position;
        Walk();
    } 
    else if (range < dist) {
        Idle();
    }

    }
    



    private void Idle()
    {
        animator.SetBool("IsIdle", true);
        animator.SetBool("IsWalk", false);
        animator.SetBool("IsRun", false);
        animator.SetBool("IsAttack", false);
    }

    private void Walk()
    {
        animator.SetBool("IsIdle", false);
        animator.SetBool("IsWalk", true);
        animator.SetBool("IsRun", false);
        animator.SetBool("IsAttack", false);
    }

    private void Run()
    {
        animator.SetBool("IsIdle", false);
        animator.SetBool("IsWalk", false);
        animator.SetBool("IsRun", true);
        animator.SetBool("IsAttack", false);
    }

    private void Attack()
    {
        animator.SetBool("IsIdle", false);
        animator.SetBool("IsWalk", false);
        animator.SetBool("IsRun", false);
        animator.SetBool("IsAttack", true);
    }
}





