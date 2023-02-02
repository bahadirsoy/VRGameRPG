using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    private int health;
    private NavMeshAgent navMeshAgent;
    private float speed;

    [SerializeField] private GameObject bloodStreamEffect;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform destination;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;

        navMeshAgent = GetComponent<NavMeshAgent>();
        createRandomSpeedFoNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.destination = destination.position;
    }

    public void getBulletDamage(Transform bloodStreamEffectPosition)
    {
        health -= 50;
        Destroy(Instantiate(bloodStreamEffect, bloodStreamEffectPosition.position, transform.rotation), 5f);

        if (health <= 0)
        {
            animator.SetTrigger("isDead");
            navMeshAgent.enabled = false;
            Destroy(gameObject, 5f);
        }
    }

    public void createRandomSpeedFoNavMesh()
    {
        speed = Random.Range(0.5f, 3f);
        navMeshAgent.speed = speed;

        if(speed < 1.2)
        {
            animator.SetBool("isWalking", true);
        } else
        {
            animator.SetBool("isRunning", true);
        }
    }
}
