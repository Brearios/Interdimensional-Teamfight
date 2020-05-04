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

    public List<IChangeState> stateChangeListeners = new List<IChangeState>();  // List of Monobehaviours that need to listen for state changes

    public string unitName;
    public float maxHealth;
    public float currHealth;
    public float healthPercent;
    public int attackDamage; // Later make this a random amount within a range
    public int abilityPower;
    public int xpWhenKilled;
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
    public List<EffectProcessor> CurrentEffects = new List<EffectProcessor>();
    public List<int> CurrentEffectsRemovalInts;
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

        if (isDead)
        {
            return;
        }

        if (currHealth <= 0)
        {
            Die();
        }

        if (GameManager.Instance.DeclareVictory == true)
        {
            return;
        }

        var pos = transform.position;
        pos.z = transform.position.y;
        transform.position = pos;

        foreach (EffectProcessor effect in CurrentEffects)
        {
            ProcessStatusEffect(effect);
        }

        foreach (int RemoveFromCurrentEffects in CurrentEffectsRemovalInts)
        {
            RemoveExpiredEffect(RemoveFromCurrentEffects);
        }
        CurrentEffectsRemovalInts.Clear();


        UpdateThreatScore();

        if (isStunned)
        {
            return;
        }


        //foreach (AbilityProcessor ability in AbilityProcessors)
        //{
        //    if (ability.currentTarget = null)
        //    { 
        //        // Taunt only affects damage ability targeting
        //        if ((!isTaunted) || (ability.abilityData.targetType != ScriptableAbility.TargetType.EnemyDamage))
        //        {
        //            FindAbilityTarget(ability);
        //        }
        //    }
        //}


        /* if (currHealth > 0)
        {
            // UpdateAlpha();
        } */

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

    void TargetLineDisplay()
    {
        foreach (AbilityProcessor currentProcessor in AbilityProcessors)
        {
            if ((currentProcessor.abilityData.targetType == ScriptableAbility.TargetType.EnemyDamage) && (currentProcessor.currentTarget != null))
            {
                Debug.DrawLine(transform.position, currentProcessor.currentTarget.transform.position);
            }
            if ((currentProcessor.abilityData.targetType == ScriptableAbility.TargetType.EnemyDebuff) && (currentProcessor.currentTarget != null))
            {
                Debug.DrawLine(transform.position, currentProcessor.currentTarget.transform.position, Color.red);
            }
            if ((currentProcessor.abilityData.targetType == ScriptableAbility.TargetType.FriendlyBuff) && (currentProcessor.currentTarget != null))
            {
                Debug.DrawLine(transform.position, currentProcessor.currentTarget.transform.position, Color.blue);
            }
            if ((currentProcessor.abilityData.targetType == ScriptableAbility.TargetType.FriendlyHeal) && (currentProcessor.currentTarget != null))
            {
                Debug.DrawLine(transform.position, currentProcessor.currentTarget.transform.position, Color.green);
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

        if (abilityProcessor.abilityData.targetType == ScriptableAbility.TargetType.EnemyDamage)
        {
            // Target is determined in FindPriorityEnemy to set who we move toward if out of range
            abilityProcessor.currentTarget = highThreatTarget;
        }
        else if (abilityProcessor.abilityData.targetType == ScriptableAbility.TargetType.EnemyDebuff)
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
        else if (abilityProcessor.abilityData.targetType == ScriptableAbility.TargetType.FriendlyHeal)
        {
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
        else if (abilityProcessor.abilityData.targetType == ScriptableAbility.TargetType.FriendlyBuff)
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
            if (Processor.cooldownCount > Processor.abilityData.cooldown)
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
            if ((ability.abilityData.targetType == ScriptableAbility.TargetType.EnemyDamage) && (ability.abilityData.isAutoAtk))
            {
                GetComponent<Character>().Animator.SetBool("Slash", true);
                int hpChangeVaried = ApplyRandomness(attackDamage * -1);
                ability.currentTarget.ChangeHealth(hpChangeVaried, true);
                Debug.Log($"{unitName} used {ability.abilityData.abilityName} on {ability.currentTarget} for {hpChangeVaried}");
            }
            else if (ability.abilityData.targetType == ScriptableAbility.TargetType.EnemyDamage)
            {
                GetComponent<Character>().Animator.SetBool("Slash", true);
                int hpChangeVaried = ApplyRandomness(abilityPower * ability.abilityData.hpDelta);
                ability.currentTarget.ChangeHealth(hpChangeVaried, true);
                Debug.Log($"{unitName} used {ability.abilityData.abilityName} on {ability.currentTarget} for {hpChangeVaried}");
            }
            else if (ability.abilityData.abilityName == "Taunt")
            {
                ability.currentTarget.highThreatTarget = this;
                ApplyStatusEffect(ability.currentTarget, ability.abilityData.effect);
            }
            if (ability.abilityData.abilityName == "Stun")
            {
                ApplyStatusEffect(ability.currentTarget, ability.abilityData.effect);
            }
            else if ((ability.abilityData.targetType == ScriptableAbility.TargetType.Self) && ability.abilityData.name == "Stealth")
            {
                ThreatScore = (ThreatScore / 2);
                isStealthed = true;
                threatResetClock = 3;
            }
            else if (ability.abilityData.targetType == ScriptableAbility.TargetType.FriendlyHeal)
            {               


                GetComponent<Character>().Animator.SetBool("Slash", true);
                int hpChangeVaried = ApplyRandomness(abilityPower * ability.abilityData.hpDelta);
                ability.currentTarget.ChangeHealth(hpChangeVaried, true);
                Debug.Log($"{unitName} used {ability.abilityData.abilityName} on {ability.currentTarget} for {hpChangeVaried}");

                // abilityTarget.ChangeHealth(abilityHpDelta, true);
                // Debug.Log($"{unitName} used {ability.abilityData.abilityName} on {ability.currentTarget} for {ability.abilityData.hpDelta}");
            }
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

    public void ApplyStatusEffect (Actor actor, ScriptableEffect effect)
    {

        EffectProcessor effectData = new EffectProcessor(effect);
        actor.CurrentEffects.Add(effectData);
    }

    public void ProcessStatusEffect (EffectProcessor effect)
    {
        effect.remainingDuration += GameManager.Instance.deltaTime;
       
        if (effect.effectData.effect == ScriptableEffect.Effect.Taunt)
        {
            isTaunted = true;
            if (effect.remainingDuration >= effect.effectData.totalDuration)
            {
                AddToRemoveList(effect);
                isTaunted = false;
            }
        }

        if (effect.effectData.effect == ScriptableEffect.Effect.Stun)
        {
            isStunned = true;
            if (effect.remainingDuration >= effect.effectData.totalDuration)
            {
                AddToRemoveList(effect);
                isStunned = false;
            }
        }

        // Code to process each type of effect here
    }

    public void AddToRemoveList(EffectProcessor effect)
    {
        int RemoveFromCurrentEffects = CurrentEffects.IndexOf(effect);
        CurrentEffectsRemovalInts.Add(RemoveFromCurrentEffects);
    }

    public void RemoveExpiredEffect(int IndexToRemove)
    {
        CurrentEffects.RemoveAt(IndexToRemove);
    }
}