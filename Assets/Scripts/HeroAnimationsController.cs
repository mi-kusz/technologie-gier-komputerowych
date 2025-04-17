using UnityEngine;
using UnityEngine.AI;

public class HeroAnimationsController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;

    private void Update()
    {
        Vector3 velocity = agent.velocity;

        float speed = velocity.magnitude;

        animator.SetFloat("Speed", speed);

        if (speed < 0.1f) 
        {
            animator.SetFloat("Speed", 0);
        }
    }
}