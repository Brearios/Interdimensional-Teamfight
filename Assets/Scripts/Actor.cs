using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public enum Team { Neutral, Blue, Red }; // Green, Purple, Orange
    public enum State { Idle, Moving, Attacking };
    
    public int maxHealth;
    public int attackDamage;
    // Later make this a random amount within a range
    public int globalCooldown;
    public string role;
    public float range;
    public float speed;
    public Team team = Team.Neutral;
    public State currentState = State.Idle;

    public Actor target;
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsRunning == false)
        {
            return;
        }

        if (currentState == State.Idle)
        {
            FindNearestEnemy();
        }

        var targetInRange = false;
        if (target != null)
        {
            // This func should determine if we're in range of our target. Return a boolean
            // which will control our flow in the rest of the update loop.
            // targetInRange = CheckTargetRange();
        }

        if (targetInRange)
        {
            if (currentState != State.Attacking)
            {
                // This func should set the currentState to attacking and then start the attack timer
                // StartAttacking();
            }
            else
            {
                // Update the attack timer, and if the timer is up, perform an attack
                // UpdateAttackLoop();
            }
        }
        else
        {
            MoveTowardsTarget();
        }

    }

    void FindNearestEnemy ()
    {
        Actor[] allActors = GameObject.FindObjectsOfType<Actor>();

            float distanceToClosestActor = Mathf.Infinity;
            Actor closestActor = null;

            foreach (Actor currentActor in allActors)
            {
                if (currentActor.team == team) // I can't get these to compare - have tried Actor.Team, this.Team, and other formats
                {
                    break;
                }
                else
                {
                    float distanceToActor = (currentActor.transform.position - transform.position).sqrMagnitude;
                    if (distanceToActor < distanceToClosestActor)
                    {
                        distanceToClosestActor = distanceToActor;
                        closestActor = currentActor;
                    }
                }

            }

    }

    void MoveTowardsTarget ()
    {
        // It's OK if we set this every frame even if we're already moving.
        currentState = State.Moving; 

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
}
