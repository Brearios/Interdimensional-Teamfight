using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Actor : MonoBehaviour
{
    public enum Team { Neutral, Blue, Red };
    public enum State { Idle, Moving, Attacking };
    public enum TargetType { Enemy, Friendly, Self };

    public ScriptableUnit unit;
    public ScriptableTeam team;
    public ScriptableAbility ability;

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
    public float abilityCooldown;
    public float abilityCooldownCount;
    public float moveSpeed;
    public State currentState = State.Idle;
    Color currentColor;
    Color alphaColor;
    public GameObject body;
    public Image healthBar;
    public Image healthBG;
    public Actor autoAtkTarget;
    public Actor abilityTarget;
    public TargetType targetType;


    // Start is called before the first frame update
    void Start()
    {
        unitName = unit.unitName;
        maxHealth = unit.maxHealth;
        attackDamage = unit.attackDamage;
        globalCooldown = unit.globalCooldown;
        role = unit.role;
        atkRange = unit.atkRange;
        abilityRange = ability.abilityRange;
        moveSpeed = unit.moveSpeed;
        // teamName = Team.Neutral;
        // teamColor = team.color;
        abilityCooldown = ability.abilityCooldown;
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
            FindTargets();
        }

        var targetInRange = false;
        if (autoAtkTarget != null)
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
            if (autoAtkTarget != null)
            {
                MoveTowardsTarget();
            }

            else
            { currentState = State.Idle; }
        }

    }

    void FindTargets()
    {
        FindNearestEnemy();
        FindAbilityTarget();
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
        autoAtkTarget = null;

        foreach (Actor currentActor in allActors)
        {
            if (currentActor.team == team)
            {
                continue;
            }
            else
            {
                float distanceToActor = (currentActor.transform.position - transform.position).sqrMagnitude;
                if (distanceToActor < distanceToClosestActor)
                {
                    distanceToClosestActor = distanceToActor;
                    autoAtkTarget = currentActor;
                }
            }

        }
    }

    void FindAbilityTarget()
    {
        if (ability.targetType == ScriptableAbility.TargetType.Enemy)
        {
            
            Actor[] allActors = GameObject.FindObjectsOfType<Actor>();

            float distanceToClosestActor = Mathf.Infinity;

            foreach (Actor currAbilityActor in allActors)
            {
                if (currAbilityActor.team == team)
                {
                    continue;
                }
                else
                {
                    float distanceToActor = (currAbilityActor.transform.position - transform.position).sqrMagnitude;
                    if (distanceToActor < distanceToClosestActor)
                    {
                        distanceToClosestActor = distanceToActor;
                        abilityTarget = currAbilityActor;
                    }
                }

            }
        }
        else if (ability.targetType == ScriptableAbility.TargetType.Friendly)
        {
            Actor[] allActors = GameObject.FindObjectsOfType<Actor>();

            float lowestHealthPercent = 1;

            foreach (Actor currAbilityActor in allActors)
            {
                if (currAbilityActor.team == team)
                {
                    float healthPercent = (currAbilityActor.currHealth / currAbilityActor.maxHealth);
                    if (healthPercent < lowestHealthPercent)
                    {
                        lowestHealthPercent = healthPercent;
                        abilityTarget = currAbilityActor;
                    }
                }
                else
                {
                    continue;    
                }

            }
        }
        else if (ability.targetType == ScriptableAbility.TargetType.Self)
        {
            abilityTarget = this; 
        }

    }


    void MoveTowardsTarget()
    {
        // It's OK if we set this every frame even if we're already moving.
        currentState = State.Moving;

        transform.position = Vector2.MoveTowards(transform.position, autoAtkTarget.transform.position, moveSpeed * GameManager.Instance.deltaTime);
    }
    public bool CheckTargetRange()
    {
        return (Vector2.Distance(autoAtkTarget.transform.position, transform.position) <= atkRange);
    }

    void StartAttacking()
    {
        currentState = State.Attacking;
        globalCooldownCount = 0f;
    }

    void UpdateAttackLoop()
    {
        globalCooldownCount += GameManager.Instance.deltaTime;
        abilityCooldownCount += GameManager.Instance.deltaTime;
        if (abilityCooldownCount >= abilityCooldown)
        {
            UseAbility();
            abilityCooldownCount -= abilityCooldown;
        }

        if (globalCooldownCount >= globalCooldown)
        {
            PerformAttack();
            globalCooldownCount -= globalCooldown;
        }
    }

    void PerformAttack()
    {
        // Attack with "animation"
            if (0 < (autoAtkTarget.transform.position.x - transform.position.x))
        {
                iTween.RotateFrom(body, new Vector3(0, 0, -20), .4f);
            }
            else
            {
                iTween.RotateFrom(body, new Vector3(0, 0, 20), .4f);
            }
        autoAtkTarget.currHealth -= attackDamage;
        if (autoAtkTarget.currHealth <= 0)
            autoAtkTarget = null;
    }
    void UseAbility()
    {
        abilityTarget.currHealth += ability.HpDelta;
        if (abilityTarget.currHealth > abilityTarget.maxHealth)
        {
            abilityTarget.currHealth = abilityTarget.maxHealth;
        }
        FindAbilityTarget();
        // Select a Target if usedOnFriends
        // Else use normal target
        // Call ability.abilityCode ??
        // Animation
        // Floating Combat Text (goes on recipient, though)
    }
}

