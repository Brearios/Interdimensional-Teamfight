using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Actor : MonoBehaviour
{
    public enum Team { Neutral, Blue, Red };
    public enum State { Idle, Moving, Attacking };

    public ScriptableUnit unit;
    // public ScriptableTeam team;

    public string unitName;
    public float maxHealth;
    public float currHealth;
    public float healthPercent;
    public int attackDamage; // Later make this a random amount within a range
    public float globalCooldown;
    public float globalCooldownCount;
    public string role;
    public float atkRange;
    public float abilityRange;
    public float moveSpeed;
    public State currentState = State.Idle;
    public Team team = Team.Neutral;
    Color teamColor;
    Color currentColor;
    Color alphaColor;
    public Image healthBar;
    public Image healthBG;

    public Actor target;


    // Start is called before the first frame update
    void Start()
    {
        unitName = unit.unitName;
        maxHealth = unit.maxHealth;
        attackDamage = unit.attackDamage;
        globalCooldown = unit.globalCooldown;
        role = unit.role;
        atkRange = unit.atkRange;
        abilityRange = unit.abilityRange;
        moveSpeed = unit.moveSpeed;
        // teamName = Team.Neutral;
        // teamColor = team.color;

        // Set Health
        currHealth = maxHealth;
        // Fetch Material from Renderer
        currentColor = GetComponentInChildren<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        HealthBarManagement();
        if (GameManager.Instance.IsRunning == false)
        {
            return;
        }

        if (currHealth <= 0)
        {
            Destroy(gameObject);
            return;
        }

        /* if (currHealth > 0)
        {
            // UpdateAlpha();
        } */ 

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

    void HealthBarManagement()
    {
        healthBar.enabled = GameManager.Instance.DisplayHealth;
        healthBG.enabled = GameManager.Instance.DisplayHealth;
        healthPercent = (currHealth / maxHealth);
        healthBar.fillAmount = healthPercent;
    }

    void UpdateAlpha() // Set Opacity based upon Health
    {
        healthPercent = (currHealth / maxHealth);
        alphaColor = new Vector4(currentColor.r, currentColor.g, currentColor.b, healthPercent);
        GetComponentInChildren<SpriteRenderer>().color = alphaColor;
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

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
    }
    public bool CheckTargetRange()
    {
        return (Vector2.Distance(target.transform.position, transform.position) <= atkRange);
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
            if (0 < (target.transform.position.x - transform.position.x))
        {
                iTween.RotateFrom(gameObject, new Vector3(0, 0, -20), .4f);
            }
            else
            {
                iTween.RotateFrom(gameObject, new Vector3(0, 0, 20), .4f);
            }
        target.currHealth -= attackDamage;
        if (target.currHealth <= 0)
            target = null;
    }        
}

