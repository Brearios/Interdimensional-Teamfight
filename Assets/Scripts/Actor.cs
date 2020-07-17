using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.HeroEditor.Common.CharacterScripts;

public class Actor : MonoBehaviour
{
    public enum Team { Neutral, Blue, Red };
    public enum State { Idle, Moving, Attacking, Dead };
    public enum TargetType { Enemy, Friendly, Self };

    public ScriptableUnit unit;
    public ScriptableTeam team;
    public ScriptableAbility autoAtk;
    public ScriptableAbility ability1;
    public ScriptableAbility ability2;
    public ScriptableAbility ability3;
    public ScriptableAbility potion;

    //public AbilityUnlock autoAtkUnlock;
    //public AbilityUnlock ability1Unlock;
    //public AbilityUnlock ability2Unlock;
    //public AbilityUnlock ability3Unlock;
    //public AbilityUnlock potionUnlock;

    public List<IChangeState> stateChangeListeners = new List<IChangeState>();  // List of Monobehaviours that need to listen for state changes

    public string unitName;
    public float maxHealth;
    public float currHealth;
    public float healthPercent;
    public int attackDamage; // Later make this a random amount within a range
    public int abilityPower;
    public int xpWhenKilled;
    public int crystalsWhenKilled;
    public int goldWhenKilled;
    public string role;
    public float atkRange;
    public float moveSpeed;

    public Actor highThreatTarget;
    public State currentState = State.Idle;
    Color currentColor;
    Color alphaColor;
    public GameObject body;
    public Image healthBar;
    public Image healthBG;

    public List<AbilityProcessor> AbilityProcessors = new List<AbilityProcessor>();
    public List<EffectData> CurrentEffects = new List<EffectData>();
    public List<string> CurrentEffectsRemovalIds;
    public bool isDead;
    // public CharacterProfile MageStats;
    public float ThreatScore;
    public float targetCheckCount;
    public float targetCheckFrequency;
    public bool hasTaunted;
    public bool isTaunted;
    public bool isStunned;
    public float threatResetClock;
    // public int myTeam;
    public bool isPlayer;
    public bool beginAtkAnim;
    public bool isStealthed;
    public bool xpAdded;
    public string abilityAnimationType;
    public float dmgVariance;
    // public int hpChangeVaried; Moved to a local variable
    public int debuggingID;
    public bool debugShowTargetLines;

    public GameObject FloatingTextPrefab;

    // Start is called before the first frame update
    void Start()
    {
        unitName = unit.unitName;
        maxHealth = unit.maxHealth;
        attackDamage = unit.attackDamage;
        abilityPower = unit.abilityPower;
        // MageStats = GameObject.FindObjectOfType<CharacterProfile>();

        CharacterProfile currentProfile = PlayerProfile.Instance.GetCharacterProfileForUnit(unit);

        // Check that abilities exist/are defined and that they're unlocked before running them
    if (isPlayer)
        {
            // Old implementation
            //if ((autoAtk) && (currentProfile.AbilityUnlocks.Contains(autoAtkUnlock)))
            // New implementation based on length
            if ((autoAtk) && ((currentProfile.autoAtkUnlock == true)))
            {
                AbilityProcessor autoAtkProcessor = new AbilityProcessor(autoAtk);
                AbilityProcessors.Add(autoAtkProcessor);
            }
            if ((ability1) && ((currentProfile.ability1Unlock == true)))
            {
                AbilityProcessor ability1Processor = new AbilityProcessor(ability1);
                AbilityProcessors.Add(ability1Processor);
            }
            if ((ability2) && ((currentProfile.ability2Unlock == true)))
            {
                AbilityProcessor ability2Processor = new AbilityProcessor(ability2);
                AbilityProcessors.Add(ability2Processor);
            }
            if ((ability3) && ((currentProfile.ability3Unlock == true)))
            {
                AbilityProcessor ability3Processor = new AbilityProcessor(ability3);
                AbilityProcessors.Add(ability3Processor);
            }
            if ((potion) && ((currentProfile.potionUnlock == true)))
            {
                AbilityProcessor potionProcessor = new AbilityProcessor(potion);
                AbilityProcessors.Add(potionProcessor);
            }
        }
    else
        {
            if (autoAtk)
            {
                AbilityProcessor autoAtkProcessor = new AbilityProcessor(autoAtk);
                AbilityProcessors.Add(autoAtkProcessor);
            }
            if (ability1)
            {
                AbilityProcessor ability1Processor = new AbilityProcessor(ability1);
                AbilityProcessors.Add(ability1Processor);
            }
            if (ability2)
            {
                AbilityProcessor ability2Processor = new AbilityProcessor(ability2);
                AbilityProcessors.Add(ability2Processor);
            }
            if (ability3)
            {
                AbilityProcessor ability3Processor = new AbilityProcessor(ability3);
                AbilityProcessors.Add(ability3Processor);
            }
            if (potion)
            {
                AbilityProcessor potionProcessor = new AbilityProcessor(potion);
                AbilityProcessors.Add(potionProcessor);
            }
        }

        role = unit.role;
        atkRange = unit.atkRange;
        
        abilityAnimationType = unit.abilityAnimationType;
        // teamName = Team.Neutral;
        // teamColor = team.color;
        moveSpeed = unit.moveSpeed;
        // Fetch Material from Renderer
        currentColor = GetComponentInChildren<SpriteRenderer>().color;
        isDead = false;
        xpWhenKilled = unit.xpWhenKilled;
        crystalsWhenKilled = unit.crystalsWhenKilled;
        goldWhenKilled = unit.goldWhenKilled;
        targetCheckFrequency = unit.targetCheckFrequency;
        targetCheckCount = Random.Range(.4f, unit.targetCheckFrequency);
        // PlayerCheck();
        if (isPlayer)
        {
            ApplyStats();
            // ApplyGearUpgrades();
        }
        // Set Health
        currHealth = maxHealth;
        beginAtkAnim = false;
        xpAdded = false;
        dmgVariance = unit.dmgVariance;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.activeScene > 1)
        {
            HealthBarManagement();
        }
        // beginAtkAnim = false;

        if (GameManager.Instance.IsRunning == false)
        {
            return;
        }

        foreach (string RemoveFromCurrentEffects in CurrentEffectsRemovalIds)
        {
            RemoveExpiredEffect(RemoveFromCurrentEffects);
            Debug.Log("A status effect removal attempt was made.");
        }
        CurrentEffectsRemovalIds.Clear();

        if (isDead)
        {
            return;
        }

        if (currHealth <= 0)
        {
            Die();
        }

        if (GameManager.Instance.BattleOver == true)
        {
            return;
        }

        var pos = transform.position;
        pos.z = transform.position.y;
        transform.position = pos;

        // UpdateAlpha();

        foreach (EffectData effect in CurrentEffects)
        {
            ProcessStatusEffect(effect);
        }

        UpdateThreatScore();

        if (isStunned)
        {
            return;
        }

        if (currentState == State.Idle)
        {
            GetComponent<Character>().Animator.SetBool("Ready", true);
            FindPriorityEnemy();
        }

        if (GameManager.Instance.ShowTarget || debugShowTargetLines)
        {
            TargetLineDisplay();
        }



        bool targetInRange = false;
        if (highThreatTarget != null)
        {
            targetInRange = CheckTargetRange();
            // This func should determine if we're in range of our target. Return a boolean
            // which will control our flow in the rest of the update loop
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
            if (highThreatTarget != null)
            {
                MoveTowardsTarget();
            }

            else
            { currentState = State.Idle;
                NotifyListeners(); }
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

    // Fully broken right now
    void TargetLineDisplay()
    {
        foreach (AbilityProcessor currentProcessor in AbilityProcessors)
        {
            if ((currentProcessor.abilityData.targetType == ScriptableAbility.TargetType.Damage) && (currentProcessor.currentTarget != null))
            {
                Debug.DrawLine(transform.position, currentProcessor.currentTarget.transform.position);
            }
            if ((currentProcessor.abilityData.targetType == ScriptableAbility.TargetType.Debuff) && (currentProcessor.currentTarget != null))
            {
                Debug.DrawLine(transform.position, currentProcessor.currentTarget.transform.position, Color.red);
            }
            if ((currentProcessor.abilityData.targetType == ScriptableAbility.TargetType.Buff) && (currentProcessor.currentTarget != null))
            {
                Debug.DrawLine(transform.position, currentProcessor.currentTarget.transform.position, Color.blue);
            }
            if ((currentProcessor.abilityData.targetType == ScriptableAbility.TargetType.Heal) && (currentProcessor.currentTarget != null))
            {
                Debug.DrawLine(transform.position, currentProcessor.currentTarget.transform.position, Color.green);
            }
        }
    }

    void UpdateAlpha() // Set Opacity based on status
    {
        // Not working, perhaps because I went from shapes to sprites

        //if (isStealthed)
        //{
        //    alphaColor = new Vector4(currentColor.r, currentColor.g, currentColor.b, 50);
        //    GetComponentInChildren<SpriteRenderer>().color = alphaColor;
        //}
        //if (!isStealthed)
        //{
        //    alphaColor = new Vector4(currentColor.r, currentColor.g, currentColor.b, 100);
        //    GetComponentInChildren<SpriteRenderer>().color = alphaColor;
        //}

        //healthPercent = (currHealth / maxHealth);
        //alphaColor = new Vector4(currentColor.r, currentColor.g, currentColor.b, healthPercent);
        //GetComponentInChildren<SpriteRenderer>().color = alphaColor;
    }

    void FindNearestEnemy()
    {
        Actor[] allActors = GameObject.FindObjectsOfType<Actor>();

        float distanceToClosestActor = Mathf.Infinity;
        highThreatTarget = null;

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
                    highThreatTarget = currentActor;
                }
            }

        }
    }

    void FindPriorityEnemy()
    {
        if (isTaunted)
        {
            return;
        }

        Actor[] allActors = GameObject.FindObjectsOfType<Actor>();

        float highestThreatEnemy = -Mathf.Infinity;
        // Actor highThreatEnemy = null; Using Actor variable highThreatEnemy instead of local for wider usability

        foreach (Actor currentActor in allActors)
        {
            if (currentActor.team == team)
            {
                continue;
            }
            if (currentActor.isDead)
            {
                continue;
            }
            else
            {
                float distanceToActor = (currentActor.transform.position - transform.position).sqrMagnitude;
                float enemyThreat = currentActor.ThreatScore;
                float threatWithDistance = (enemyThreat - (distanceToActor / 5));
                if (threatWithDistance > highestThreatEnemy)
                {
                    highestThreatEnemy = threatWithDistance;
                    highThreatTarget = currentActor;
                }
            }
        }
    }

    void FindAbilityTarget(AbilityProcessor abilityProcessor)
    {
        Actor[] allActors = GameObject.FindObjectsOfType<Actor>();

        if ((abilityProcessor.abilityData.targetType == ScriptableAbility.TargetType.Damage) || (abilityProcessor.abilityData.targetType == ScriptableAbility.TargetType.Dot))
        {
            // Target is determined in FindPriorityEnemy to set who we move toward if out of range
            abilityProcessor.currentTarget = highThreatTarget;
        }
        else if (abilityProcessor.abilityData.targetType == ScriptableAbility.TargetType.Debuff)
        {
            float highestDamageEnemy = Mathf.NegativeInfinity;

            foreach (Actor currentActor in allActors)
            {
                if (currentActor.team == team)
                {
                    continue;
                }
                if (currentActor.isDead)
                {
                    continue;
                }
                else
                {
                    float damageEvaluationActor = (currentActor.attackDamage + currentActor.abilityPower);
                    if (damageEvaluationActor > highestDamageEnemy)
                    {
                        highestDamageEnemy = damageEvaluationActor;
                        abilityProcessor.currentTarget = currentActor;
                    }
                }
            }
        }
        else if ((abilityProcessor.abilityData.targetType == ScriptableAbility.TargetType.Heal) || (abilityProcessor.abilityData.targetType == ScriptableAbility.TargetType.Hot))
        {
            abilityProcessor.currentTarget = this;
            float lowestHealthPercent = 1;

            foreach (Actor currentActor in allActors)
            {
                if (currentActor.team == team)
                {
                    if (isDead)
                    {
                        continue;
                    }
                    float healthPercent = (currentActor.currHealth / currentActor.maxHealth);
                    if ((healthPercent < lowestHealthPercent) && (healthPercent > 0))
                    {
                        lowestHealthPercent = healthPercent;
                        abilityProcessor.currentTarget = currentActor;
                    }
                }
                else
                {
                    continue;
                }
            }
        }
        else if (abilityProcessor.abilityData.targetType == ScriptableAbility.TargetType.Buff)
        {
            float highestDamageAlly = Mathf.NegativeInfinity;

            foreach (Actor currentActor in allActors)
            {
                if (currentActor.team == team)
                {
                    continue;
                }
                if (currentActor.isDead)
                {
                    continue;
                }
                else
                {
                    float damageEvaluationActor = (currentActor.attackDamage + currentActor.abilityPower);
                    if (damageEvaluationActor > highestDamageAlly)
                    {
                        highestDamageAlly = damageEvaluationActor;
                        abilityProcessor.currentTarget = currentActor;
                    }
                }
            }
        }
        else if (abilityProcessor.abilityData.targetType == ScriptableAbility.TargetType.AoePercentDamage)
        {
            // I'm not sure if I need a target to avoid errors
            abilityProcessor.currentTarget = this;
        }
        //else if (ability.targetType == ScriptableAbility.TargetType.Self)
        //{
        //    continue;
        //}
    }


    void MoveTowardsTarget()
    {
        // It's OK if we set this every frame even if we're already moving.
        GetComponent<Character>().Animator.SetBool("Ready", false);
        currentState = State.Moving;
        NotifyListeners();
        GetComponent<Character>().Animator.SetBool("Walk", true);
        // GetComponent<Character>().Animator.Play("Walk"); Definitely not doing what I want

        transform.position = Vector2.MoveTowards(transform.position, highThreatTarget.transform.position, moveSpeed * GameManager.Instance.deltaTime);
    }
    public bool CheckTargetRange()
    {
        return (Vector2.Distance(highThreatTarget.transform.position, transform.position) <= autoAtk.abilityRange);
    }

    void StartAttacking()
    {
        currentState = State.Attacking;
        NotifyListeners();
        GetComponent<Character>().Animator.SetBool("Walk", false);
        GetComponent<Character>().Animator.SetBool("Ready", true);
        // globalCooldownCount = 0f; Unecessary due to conversion of autoAtk to ability
    }

    void UpdateAttackLoop()
    {
        // globalCooldownCount += GameManager.Instance.deltaTime; Unecessary due to conversion of autoAtk to ability

        foreach (AbilityProcessor Processor in AbilityProcessors)
        {
            Processor.cooldownCount += GameManager.Instance.deltaTime;
            if (Processor.cooldownCount >= Processor.abilityData.cooldown)
            {
                UseAbility(Processor);
                Processor.cooldownCount -= Processor.abilityData.cooldown;
            }
        }

        

        //foreach (ScriptableAbility ability in MyAbilities)
        //{
        //    ability.cooldownCount += GameManager.Instance.deltaTime;
        //    if (ability.cooldownCount >= ability.cooldown)
        //    {
        //        UseAbility(ability);
        //        ability.cooldownCount -= ability.cooldown;
        //    }
        //}

        // Unecessary due to conversion of autoAtk to ability
        //if (globalCooldownCount >= globalCooldown)
        //{
        //    PerformAttack();
        //    globalCooldownCount -= globalCooldown;
        //}
    }

    void PerformAttack()
    {
        if (gameObject != null)
        {
            // Attack with "animation"
            // if (0 < (autoAtkTarget.transform.position.x - transform.position.x))
            // beginAtkAnim = true;
            //{
            //    iTween.RotateFrom(body, new Vector3(0, 0, -20), .4f);
            //}
            //else
            //{
            //    iTween.RotateFrom(body, new Vector3(0, 0, 20), .4f);
            //}
            if (highThreatTarget.currHealth <= 0)
            {
                highThreatTarget = null;
            }

            // Random Damage based on dmgVariance stat
            int hpChangeVaried = ApplyRandomness(attackDamage);

            GetComponent<Character>().Animator.SetBool("Slash", true);
            highThreatTarget.ChangeHealth(-hpChangeVaried, false);
            Debug.Log($"{unitName} attacked {highThreatTarget} for {hpChangeVaried}.");

            //autoAtkTarget.ChangeHealth(-attackDamage, false);
            //Debug.Log($"{unitName} attacked {autoAtkTarget} for {attackDamage}");

            // autoAtkTarget.currHealth -= attackDamage; - old code
            
        }
    }
    void UseAbility(AbilityProcessor ability)
    {
        // Can't set target by ability
        // if (ability.currentTarget != null)


        // This would give them perfect reaction time

        //if (abilityTarget.currHealth <= 0)
        //{
        //    FindAbilityTarget();
        //}

        {
            // Auto Attack
            if ((ability.abilityData.targetType == ScriptableAbility.TargetType.Damage) && (ability.abilityData.isAutoAtk))
            {
                GetComponent<Character>().Animator.SetBool("Slash", true);
                int hpChangeVaried = ApplyRandomness(attackDamage * -1);
                ability.currentTarget.ChangeHealth(hpChangeVaried, true);
                Debug.Log($"{unitName} used {ability.abilityData.abilityName} on {ability.currentTarget} for {hpChangeVaried}");
            }
            // Direct Damage Ability
            else if (ability.abilityData.targetType == ScriptableAbility.TargetType.Damage)
            {
                GetComponent<Character>().Animator.SetBool("Slash", true);
                int hpChangeVaried = ApplyRandomness(abilityPower * ability.abilityData.hpDelta);
                ability.currentTarget.ChangeHealth(hpChangeVaried, true);
                Debug.Log($"{unitName} used {ability.abilityData.abilityName} on {ability.currentTarget} for {hpChangeVaried}");
            }
            // Taunt
            else if (ability.abilityData.abilityName == "Taunt")
            {
                ability.currentTarget.highThreatTarget = this;
                ApplyStatusEffect(this, ability.currentTarget, ability.abilityData.effect);
            }
            // Stun
            else if (ability.abilityData.abilityName == "Stun")
            {
                ApplyStatusEffect(this, ability.currentTarget, ability.abilityData.effect);
            }
            // Stealth
            else if ((ability.abilityData.targetType == ScriptableAbility.TargetType.Self) && ability.abilityData.name == "Stealth")
            {
                // This should be moved to StatusEffectProcessor, but it also works
                ThreatScore = (ThreatScore / 2);
                isStealthed = true;

                threatResetClock = 3;
            }
            // Heal
            else if (ability.abilityData.targetType == ScriptableAbility.TargetType.Heal)
            {               
                GetComponent<Character>().Animator.SetBool("Slash", true);
                int hpChangeVaried = ApplyRandomness(abilityPower * ability.abilityData.hpDelta);
                ability.currentTarget.ChangeHealth(hpChangeVaried, true);
                Debug.Log($"{unitName} used {ability.abilityData.abilityName} on {ability.currentTarget} for {hpChangeVaried}");

                // abilityTarget.ChangeHealth(abilityHpDelta, true);
                // Debug.Log($"{unitName} used {ability.abilityData.abilityName} on {ability.currentTarget} for {ability.abilityData.hpDelta}");
            }
            // Heal over Time
            else if (ability.abilityData.targetType == ScriptableAbility.TargetType.Hot)
            {
                ApplyStatusEffect(this, ability.currentTarget, ability.abilityData.effect);
            }
            // Damage over Time
            else if (ability.abilityData.targetType == ScriptableAbility.TargetType.Dot)
            {
                ApplyStatusEffect(this, ability.currentTarget, ability.abilityData.effect);
            }
            // AoE damage typically from bosses
            else if (ability.abilityData.targetType == ScriptableAbility.TargetType.AoePercentDamage)
            {
                Actor[] allActors = GameObject.FindObjectsOfType<Actor>();

                foreach (Actor AoePotentialTarget in allActors)
                {
                    if (AoePotentialTarget.team != team)
                    {
                        int roll = Random.Range(1, 100);
                        // The ability should hit "Chance to Hit" percent of the time, so if the roll is less than that percent, it should hit.
                        if (roll < ability.abilityData.aoeChancetoHit)
                        {
                            // Add randomness to the amount of damage done
                            int hpChangeVaried = ApplyRandomness(abilityPower * ability.abilityData.hpDelta);
                            AoePotentialTarget.ChangeHealth(hpChangeVaried, true);
                            Debug.Log($"{AoePotentialTarget.unitName} was hit by {ability.abilityData.abilityName} for {hpChangeVaried} damage.");
                        }
                    }
                }
            }
            // Other
            else
            {
                GetComponent<Character>().Animator.SetBool("Slash", true);
                int hpChangeVaried = ApplyRandomness(abilityPower * ability.abilityData.hpDelta);
                ability.currentTarget.ChangeHealth(hpChangeVaried, true);
                Debug.Log($"{unitName} used {ability.abilityData.abilityName} on {ability.currentTarget} for {hpChangeVaried}");
            }
        }


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

        // Log Damage/Healing - Does not account for overkill/overhealing
        if ((isPlayer) && (amount < 0)) 
        {
            GameManager.Instance.heroDamageTaken += Mathf.Abs(amount);
        }

        if ((isPlayer) && (amount > 0))
        {
            GameManager.Instance.heroHealingDone += amount;
        }

        if ((!isPlayer) && (amount < 0))
        {
            GameManager.Instance.heroDamageDone += Mathf.Abs(amount);
        }

        if ((!isPlayer) && (amount > 0))
        {
            GameManager.Instance.enemyHealingDone += amount;
        }

        // Animation/Knockback

        // Floating Combat Text

        // if (abilitytarget != null)
        // {
        if (FloatingTextPrefab)
            {
                if (wantFloatingText == true)
                {
                    ShowFloatingText(amount);
                }
            }
        // }
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

        // Unnecessary since taunt now sets target directly on Taunted mob
        //if (hasTaunted)
        //{
        //    threatResetClock -= GameManager.Instance.deltaTime;
        //    if (threatResetClock <= 0)
        //    {
        //        hasTaunted = false;
        //    }
        //}
        if (isStealthed)
        {
            // Currently threat is "locked in" once you stealth, until stealth ends.
            // I'm not sure if I like this.
            threatResetClock -= GameManager.Instance.deltaTime;
            if (threatResetClock <= 0)
            {
                isStealthed = false;
            }
        }
        else
        {
            if (isDead)
            {
                ThreatScore = Mathf.NegativeInfinity;
            }
            else
            {
                ThreatScore = (((maxHealth / currHealth) * (attackDamage + abilityPower)) - (currHealth / 5));
            }
        }
    }

    void ApplyStats()
    {
        // Identify whether player character
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
            if (unit.role == "Tank")
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

    // Unnecessary due to ability list change

    void TargetCheckLoop()
    {
        targetCheckCount += GameManager.Instance.deltaTime;
        if (targetCheckCount >= targetCheckFrequency)
        {
            FindPriorityEnemy();
            foreach (AbilityProcessor ability in AbilityProcessors)
        {
            FindAbilityTarget(ability);
        }
            targetCheckCount = 0;
        }
    }
    void PlayerCheck()
    {
        //if (Actor.Team == Team.Blue)
        //{
        //    isPlayer = true;
        //}
        //else
        //{
        //    isPlayer = false;
        //}
    }

    public int ApplyRandomness(int hpToVary)
    {
        if (dmgVariance == 0)
        {
            Debug.LogWarning($"{unitName}'s Damage Variance is set to 0.");
        }
        // Random chane from 0 to unit.DmgVariance
        float randomChange = Random.Range(0, (hpToVary * dmgVariance));
        Debug.Log($"{unitName}'s attack or ability will vary by {randomChange}.");

        // randomChange to int, rounded to the Nearest Whole Number
        int randomIntChange = System.Convert.ToInt32(randomChange);
        Debug.Log($"Rounding, {unitName}'s attack or ability will vary by {randomIntChange}.");

        // Randomly negative or positive
        int posOrNeg = Random.Range(1, 2);
        if (posOrNeg == 1) // negative if 1
        {
            randomIntChange *= -1;
        }

        // hpChangeVaried = hpToVary + randomChange
        int hpChangeVaried = (hpToVary + randomIntChange);
        Debug.Log($"{unitName}'s attack or ability will change target's HP by {hpChangeVaried}.");
        return hpChangeVaried;
    }

    public void RegisterListener(IChangeState listener)
    {
        stateChangeListeners.Add(listener);
    }

    public void NotifyListeners()
    {
        stateChangeListeners.ForEach(listener =>
        {
            listener.changeState();
        });
    }    

    public void Die()
    {
        isDead = true;
        TeamDeathCounter();
        healthBar.canvasRenderer.SetAlpha(0);
        healthBG.canvasRenderer.SetAlpha(0);
        {
            GetComponent<Character>().Animator.SetTrigger(Time.frameCount % 2 == 0 ? "DieBack" : "DieFront"); // Play animation randomly
        }
        //currentState = State.Dead;
        //NotifyListeners();

        //if (xpAdded == false && isPlayer == false)
        //{
        //    GameManager.Instance.earnedBattleXP += xpWhenKilled; // this should be done in the GameManager at the end of the battle
        //    xpAdded = true;
        //}
        //gameObject.SetActive(false); // -switching to SetActive should allow rezzing
        //// Destroy(gameObject);
        return;
    }

    public void TeamDeathCounter()
    {
        if (isPlayer)
        {
            GameManager.Instance.heroDeaths++;
        }
        if (!isPlayer)
        {
            GameManager.Instance.enemyDeaths++;
        }
    }
    public void ApplyStatusEffect (Actor caster, Actor actor, ScriptableEffect effect)
    {
        EffectData effectData = new EffectData(effect, caster.abilityPower);
        actor.CurrentEffects.Add(effectData);
    }

    public void ProcessStatusEffect (EffectData effect)
    {
        effect.remainingDuration += GameManager.Instance.deltaTime;

        if (effect.effectSettings.effect == ScriptableEffect.Effect.Stun)
        {
            isTaunted = true;
            if (effect.remainingDuration >= effect.effectSettings.totalDuration)
            {
                AddToRemoveList(effect);
                isTaunted = false;
            }
        }

        if (effect.effectSettings.effect == ScriptableEffect.Effect.Stun)
        {
            isStunned = true;
            if (effect.remainingDuration >= effect.effectSettings.totalDuration)
            {
                AddToRemoveList(effect);
                isStunned = false;
            }
        }

        if (effect.effectSettings.effect == ScriptableEffect.Effect.HealthOverTime)
        {
            effect.effectTickDurationCount += GameManager.Instance.deltaTime;
            if (effect.effectTickDurationCount >= effect.effectSettings.effectTickDuration)
            {
                ApplyOverTimeEffect(effect.hpDeltaPerTickInt, effect.effectSettings.effectName);
                effect.effectTickDurationCount -= effect.effectSettings.effectTickDuration;
            }
            if (effect.remainingDuration >= effect.effectSettings.totalDuration)
            {
                AddToRemoveList(effect);
            }
        }
    }

    public void ApplyOverTimeEffect(int hpDeltaPerTickInt, string effectName)
    {
        ChangeHealth(hpDeltaPerTickInt, true);
        Debug.Log($"{unitName} was affected by {effectName} effect for {hpDeltaPerTickInt}");
    }

    public void AddToRemoveList(EffectData effect)
    {
        // int RemoveFromCurrentEffects = CurrentEffects.IndexOf(effect);
        // CurrentEffectsRemovalInts.Add(RemoveFromCurrentEffects);
        CurrentEffectsRemovalIds.Add(effect.id);
    }

    public void RemoveExpiredEffect(string effectId)
    {
        // Find the removal Id and remove that item from the list
        CurrentEffects.Remove(CurrentEffects.Find(effectToRemove => effectToRemove.id == effectId));
    }
}