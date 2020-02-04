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
    public int abilityPower;
    public int xpWhenKilled;
    public int abilityHpDelta;
    public float globalCooldown;
    public float globalCooldownCount;
    public string role;
    public float atkRange;
    public float abilityRange;
    public float abilityCooldown;
    public float abilityCooldownCount;
    public float moveSpeed;
    public float abilitySpeed;
    public State currentState = State.Idle;
    Color currentColor;
    Color alphaColor;
    public GameObject body;
    public Image healthBar;
    public Image healthBG;
    public Actor autoAtkTarget;
    public Actor abilityTarget;
    public List<ScriptableEffects> CurrentEffects;
    public bool isDead;
    // public CharacterProfile MageStats;
    public float ThreatScore;
    public float targetCheckCount;
    public float targetCheckFrequency;
    public bool hasTaunted;
    public float tauntResetClock;
    // public int myTeam;
    public bool isPlayer;

    public GameObject FloatingTextPrefab;

    // Start is called before the first frame update
    void Start()
    {
        unitName = unit.unitName;
        maxHealth = unit.maxHealth;
        attackDamage = unit.attackDamage;
        abilityPower = unit.abilityPower;
        // MageStats = GameObject.FindObjectOfType<CharacterProfile>();
        globalCooldown = unit.globalCooldown;
        role = unit.role;
        atkRange = unit.atkRange;
        abilityRange = ability.abilityRange;
        moveSpeed = unit.moveSpeed;
        // teamName = Team.Neutral;
        // teamColor = team.color;
        abilityCooldown = ability.abilityCooldown;
        // Fetch Material from Renderer
        currentColor = GetComponentInChildren<SpriteRenderer>().color;
        isDead = false;
        xpWhenKilled = unit.xpWhenKilled;
        targetCheckCount = 0;
        targetCheckFrequency = unit.targetCheckFrequency;
        // PlayerCheck();
        ApplyStats();
        // Set Health
        currHealth = maxHealth;
        abilityHpDelta = (abilityPower * ability.hpDelta);
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
            isDead = true;
            GameManager.Instance.earnedBattleXP += xpWhenKilled; // this should be done in the GameManager at the end of the battle
            gameObject.SetActive(false); // -switching to SetActive should allow rezzing
            // Destroy(gameObject);
            return;
        }

        UpdateThreatScore();

        if (abilityTarget == null)
        {
            FindAbilityTarget();
        }
        /* if (currHealth > 0)
        {
            // UpdateAlpha();
        } */

        if (currentState == State.Idle)
        {
            FindPriorityEnemy();
        }

        TargetLineDisplay();



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
                if (gameObject != null)
                {
                    UpdateAttackLoop();
                }
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

        // Check for better targets every X seconds, frequency from scriptable unit
        TargetCheckLoop();
    }

    void HealthBarManagement()
    {
        healthBar.enabled = GameManager.Instance.DisplayHealth;
        healthBG.enabled = GameManager.Instance.DisplayHealth;
        healthPercent = (currHealth / maxHealth);
        healthBar.fillAmount = healthPercent;
    }

    void TargetLineDisplay()
    {
        if (GameManager.Instance.ShowTarget)
        {
            if (autoAtkTarget != null)
            {
                Debug.DrawLine(transform.position, autoAtkTarget.transform.position);
            }
            if (abilityTarget != null)
            {
                Debug.DrawLine(transform.position, abilityTarget.transform.position, Color.red);
            }
        }
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

    void FindPriorityEnemy()
    {
        Actor[] allActors = GameObject.FindObjectsOfType<Actor>();

        float highestThreatEnemy = -Mathf.Infinity;
        autoAtkTarget = null;

        foreach (Actor currentActor in allActors)
        {
            if (currentActor.team == team)
            {
                continue;
            }
            else
            {
                float enemyThreat = currentActor.ThreatScore;
                if (enemyThreat > highestThreatEnemy)
                {
                    highestThreatEnemy = enemyThreat;
                    autoAtkTarget = currentActor;
                }
            }

        }
    }

    void FindAbilityTarget()
    {
        if (ability.targetType == ScriptableAbility.TargetType.Enemy)
        {
            abilityTarget = autoAtkTarget;
            /*
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
            */
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
            if (gameObject != null)
            {
                UseAbility();
            }
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
        if (gameObject != null)
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
            autoAtkTarget.ChangeHealth(-attackDamage, false);
            // autoAtkTarget.currHealth -= attackDamage; - old code
            if (autoAtkTarget.currHealth <= 0)
                autoAtkTarget = null;
        }
    }
    void UseAbility()
    {
        if (abilityTarget != null)
        {
            if ((ability.targetType == ScriptableAbility.TargetType.Self) && ability.name == "Taunt")
            {
                // if (ability.name == "Taunt")
                // {
                float highestThreat = 0;
                Actor[] allActors = GameObject.FindObjectsOfType<Actor>();
                foreach (Actor teamThreatActor in allActors)
                {
                    if (teamThreatActor.team != team)
                    {
                        continue;
                    }
                    else
                    {

                        if (ThreatScore > highestThreat)
                        {
                            highestThreat = ThreatScore;
                        }
                    }
                }
                /* This seems to be preventing taunting
                if (highestThreat == this.ThreatScore)
                {
                    return;
                }
                */
                ThreatScore = (highestThreat * 2);
                hasTaunted = true;
                tauntResetClock = 3;
                // }

            }
            else
            {
                abilityTarget.ChangeHealth(abilityHpDelta, true);
            }
        }
        // abilityTarget.currHealth += ability.HpDelta; - old code


        // Code for things that don't just modify HP directly

        FindAbilityTarget();
        // Select a Target if usedOnFriends
        // Else use normal target
        // Call ability.abilityCode ??
        // Animation
        // Floating Combat Text (goes on recipient, though)
    }

    void ChangeHealth(float amount, bool wantFloatingText)
    {
        // Add or Subtract Health
        currHealth += amount;
        if (currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }
        // Animation/Knockback
        // Floating Combat Text
        if (abilityTarget != null)
        {
            if (FloatingTextPrefab)
            {
                if (wantFloatingText == true)
                {
                    ShowFloatingText(amount);
                }
            }
        }
    }

    void ShowFloatingText(float amount)
    {
        //Instantiate prefab at position of Actor with no rotation
        if (gameObject != null)
        {
            var go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
            if (amount > 0)
            {
                go.GetComponent<TextMesh>().color = Color.green;
            }
            if (amount < 0)
            {
                go.GetComponent<TextMesh>().color = Color.black;
            }
            if (amount == 0)
            {
                go.GetComponent<TextMesh>().color = Color.clear;
            }
            go.GetComponent<TextMesh>().text = amount.ToString();
        }
    }

    void UpdateThreatScore()
    {
        // MaxHealth over CurrHealth (so as health decreases, priority increases)
        // multiplied by sum of atk & ability power
        // over maxHealth, so that 
        if (hasTaunted)
        {
            tauntResetClock -= GameManager.Instance.deltaTime;
            if (tauntResetClock <= 0)
            {
                hasTaunted = false;
            }
        }
        else
        {
            ThreatScore = ((maxHealth / currHealth) * (attackDamage + abilityPower));
        }
    }

    void ApplyStats()
    {
        // Identify whether player character
        if (isPlayer)
        {
            CharacterProfile currentProfile = PlayerProfile.Instance.GetCharacterProfileForUnit(unit);
            if (currentProfile.atkArrayLevel > 0)
            {
                attackDamage = currentProfile.atk;
            }
            if (currentProfile.abilityArrayLevel > 0)
            {
                abilityPower = currentProfile.abilityPower;
            }
            if (currentProfile.healthArrayLevel > 0)
            {
                if (unit.role == "tank")
                {
                    maxHealth = (3 * currentProfile.health);
                }
                else
                {
                    maxHealth = currentProfile.health;
                }
            }
            
            //switch (unit.name)
            //{
            //    case "Mage":
            //        {
            //            attackDamage = PlayerProfile.Instance.mageHero.atk;
            //            maxHealth = PlayerProfile.Instance.mageHero.health;
            //            abilityPower = PlayerProfile.Instance.mageHero.abilityPower;
            //        }
            //        break;
            //    case "Priest":
            //        {
            //            attackDamage = PlayerProfile.Instance.priestHero.atk;
            //            maxHealth = PlayerProfile.Instance.priestHero.health;
            //            abilityPower = PlayerProfile.Instance.priestHero.abilityPower;
            //        }
            //        break;
            //    case "Warrior":
            //        {
            //            attackDamage = PlayerProfile.Instance.warriorHero.atk;
            //            maxHealth = PlayerProfile.Instance.warriorHero.health;
            //            abilityPower = PlayerProfile.Instance.warriorHero.abilityPower;
            //        }
            //        break;
            //}

        }
    }



    /*
    void ApplyMageStats()
    {
        if (unitName == "Mage" && MageStats.atk > 0)
        {
            attackDamage = MageStats.atk;
        }
        if (unitName == "Mage" && MageStats.health > 0)
        {
            maxHealth = MageStats.health;
        }
        if (unitName == "Mage" && MageStats.abilityPower > 0)
        {
            abilityPower = MageStats.abilityPower;
        }
    }
    */

    void TargetCheckLoop()
    {
        targetCheckCount += GameManager.Instance.deltaTime;
        if (targetCheckCount >= targetCheckFrequency)
        {
            FindPriorityEnemy();
            FindAbilityTarget();
            targetCheckCount = 0;
        }
    }
    void PlayerCheck()
    {
        //if (Actor.Team = Team.Blue)
        //{
        //    isPlayer = true;
        //}
        //else
        //{
        //    isPlayer = false;
        //}
    }
}
    

