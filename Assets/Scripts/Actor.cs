using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public enum Team { Neutral, Blue, Red }; // Green, Purple, Orange
    public enum State { Idle, Moving, Attacking };

    public string unitName;
    public float maxHealth;
    public float currHealth;
    public float healthPercent;
    public int attackDamage; // Later make this a random amount within a range
    public float globalCooldown;
    public float globalCooldownCount;
    public string role;
    public float range;
    public float speed;
    public Team team = Team.Neutral;
    public State currentState = State.Idle;
    Color currentColor;
    Color alphaColor;

    public Actor target;


    // Start is called before the first frame update
    void Start()
    {
        // Set Health
        currHealth = maxHealth;
        // Fetch Material from Renderer
        currentColor = GetComponent<Renderer>().material.color;

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsRunning == false)
        {
            return;
        }

        if (currHealth <= 0)
        {
            Destroy(gameObject);
            return;
        }

        if (currHealth > 0)
        {
            UpdateAlpha();
        }

        if (currentState == State.Idle)
        {
            FindNearestEnemy();
        }

        var targetInRange = false;
        if (target != null)
        {
            targetInRange = CheckTargetRange();
            // This func should determine if we're in range of our target. Return a boolean
            // which will control our flow in the rest of the update loop.
            // targetInRange = CheckTargetRange();
        }

        if (targetInRange)
        {
            if (currentState != State.Attacking)
            {
                StartAttacking();
                // This func should set the currentState to attacking and then start the attack timer
                // StartAttacking();
            }
            else
            {
                UpdateAttackLoop();
                // Update the attack timer, and if the timer is up, perform an attack
                // UpdateAttackLoop();
            }
        }

        else
        {
            if (target != null)
            {
                MoveTowardsTarget();
            }

            else
            { currentState = State.Idle; }
        }

    }

    void UpdateAlpha() // Set Opacity based upon Health
    {
        healthPercent = (currHealth / maxHealth);
        alphaColor = new Vector4(currentColor.r, currentColor.g, currentColor.b, healthPercent);
        GetComponent<Renderer>().material.color = alphaColor;
    }

    void FindNearestEnemy()
    {
        Actor[] allActors = GameObject.FindObjectsOfType<Actor>();

        float distanceToClosestActor = Mathf.Infinity;
        target = null;

        foreach (Actor currentActor in allActors)
        {
            if (currentActor.team == team) // I can't get these to compare - have tried Actor.Team, this.Team, and other formats (was due to using break, not continue)
            {
                continue;
            }
            else
            {
                float distanceToActor = (currentActor.transform.position - transform.position).sqrMagnitude;
                if (distanceToActor < distanceToClosestActor)
                {
                    distanceToClosestActor = distanceToActor;
                    target = currentActor;
                }
            }

        }
    }


    void MoveTowardsTarget()
    {
        // It's OK if we set this every frame even if we're already moving.
        currentState = State.Moving;

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
    public bool CheckTargetRange()
    {
        return (Vector2.Distance(target.transform.position, transform.position) <= range);
    }

    void StartAttacking()
    {
        currentState = State.Attacking;
        globalCooldownCount = 0f;

    }

    void UpdateAttackLoop()
    {
        globalCooldownCount += Time.deltaTime;
        if (globalCooldownCount >= globalCooldown)
        {
            PerformAttack();
            globalCooldownCount -= globalCooldown;
        }
    }

    void PerformAttack()
    {
        // Attack with "animation"
        iTween.PunchRotation(gameObject, new Vector3(0, 0, 35), .4f);
        target.currHealth -= attackDamage;
        if (target.currHealth <= 0)
            target = null;
    }
}

