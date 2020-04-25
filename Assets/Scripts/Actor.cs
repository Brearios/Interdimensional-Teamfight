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
    // Moved into code for each ability
    //public float abilityRange;
    //public float abilityCooldown;
    //public float abilityCooldownCount;
    //public string abilityName;
    //public int abilityHpDelta;
    //public float globalCooldown;
    //public float globalCooldownCount;
    // public float abilitySpeed; Unused
    //public Actor abilityTarget;
    public Actor highThreatTarget;
    public State currentState = State.Idle;
    Color currentColor;
    Color alphaColor;
    public GameObject body;
    public Image healthBar;
    public Image healthBG;
   
    public List<ScriptableAbility> MyAbilities;
    public List<ScriptableEffect> CurrentEffects;
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
        MyAbilities.Add(autoAtk);
        MyAbilities.Add(ability1);
        MyAbilities.Add(ability2);
        MyAbilities.Add(ability3);
        MyAbilities.Add(potion);

        // Moved to Ability Data
        //abilityName = ability.abilityName;
        //abilityRange = ability.abilityRange;
        //globalCooldown = unit.globalCooldown;
        //abilityCooldown = ability.abilityCooldown;
        foreach (ScriptableAbility ability in MyAbilities)
        {
            if (!ability.startsOnCooldown)
            {
                ability.cooldownCount = ability.cooldown;
            }
        }
        
        //abilityCooldownCount = ability.abilityStartingCooldownCredit;
        //abilityHpDelta = (abilityPower * ability.hpDelta);
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

        var pos = transform.position;
        pos.z = transform.position.y;
        transform.position = pos;

        foreach (ScriptableEffect effect in CurrentEffects)
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


        foreach (ScriptableAbility ability in MyAbilities)
        {
            if (ability.currentTarget == null)
            {
                if (!isTaunted)
                {
                    FindAbilityTarget(ability);
                    continue;
                }
                if ((isTaunted) && (ability.targetType != ScriptableAbility.TargetType.EnemyDamage))
                {
                    FindAbilityTarget(ability);
                    continue;
                }
                // Unnecessary code
                //if (isTaunted && ability.isTauntable)
                //{
                //    continue;
                //}

            }
        }
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



        var targetInRange = false;
        if (autoAtk.currentTarget != null)
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
            if (autoAtk.currentTarget != null)
            {
                MoveTowardsTarget();
            }

            else
            { currentState = State.Idle;
                NotifyListeners(); }
        }

        // Check for better targets every X seconds, frequency from scriptable unit

        // Moving this logic into Ability forEach loop
        //if (isTaunted == false)
        //{
        //    TargetCheckLoop();
        //}
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
        {
            if (autoAtk.currentTarget != null)
            {
                Debug.DrawLine(transform.position, autoAtk.currentTarget.transform.position);
            }
            if (ability1.currentTarget != null)
            {
                Debug.DrawLine(transform.position, ability1.currentTarget.transform.position, Color.red);
            }
            if (ability2.currentTarget != null)
            {
                Debug.DrawLine(transform.position, ability2.currentTarget.transform.position, Color.blue);
            }
            if (ability3.currentTarget != null)
            {
                Debug.DrawLine(transform.position, ability3.currentTarget.transform.position, Color.green);
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
        autoAtk.currentTarget = null;

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
                    autoAtk.currentTarget = currentActor;
                }
            }

        }
    }

    void FindPriorityEnemy()
    {
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
                    autoAtk.currentTarget = currentActor;
                }
            }
        }
    }

    void FindAbilityTarget(ScriptableAbility ability)
    {
        ability.currentTarget = null;
        Actor[] allActors = GameObject.FindObjectsOfType<Actor>();

        if (ability.targetType == ScriptableAbility.TargetType.EnemyDamage)
        {
            // Target is determined in FindPriorityEnemy to set who we move toward if out of range
            ability.currentTarget = highThreatTarget;
        }
        else if (ability.targetType == ScriptableAbility.TargetType.EnemyDebuff)
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
                        ability.currentTarget = currentActor;
                    }
                }
            }
        }
        else if (ability.targetType == ScriptableAbility.TargetType.FriendlyHeal)
        {
            float lowestHealthPercent = 1;

            foreach (Actor currentActor in allActors)
            {
                if (currentActor.team == team)
                {
                    float healthPercent = (currentActor.currHealth / currentActor.maxHealth);
                    if ((healthPercent < lowestHealthPercent) && (healthPercent > 0))
                    {
                        lowestHealthPercent = healthPercent;
                        ability.currentTarget = currentActor;
                    }
                }
                else
                {
                    continue;
                }

            }
        }
        else if (ability.targetType == ScriptableAbility.TargetType.FriendlyBuff)
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
                        ability.currentTarget = currentActor;
                    }
                }
            }
        }
        else if (ability.targetType == ScriptableAbility.TargetType.Self)
        {
            ability.currentTarget = this;
        }
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
        return (Vector2.Distance(autoAtk.currentTarget.transform.position, transform.position) <= atkRange);
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

        foreach (ScriptableAbility ability in MyAbilities)
        {
            ability.cooldownCount += GameManager.Instance.deltaTime;
            if (ability.cooldownCount >= ability.cooldown)
            {
                UseAbility(ability);
                ability.cooldownCount -= ability.cooldown;
            }
        }
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

            // Random Damage based on dmgVariance stat
            int hpChangeVaried = ApplyRandomness(attackDamage);

            GetComponent<Character>().Animator.SetBool("Slash", true);
            highThreatTarget.ChangeHealth(-hpChangeVaried, false);
            Debug.Log($"{unitName} attacked {highThreatTarget} for {hpChangeVaried}");

            //autoAtkTarget.ChangeHealth(-attackDamage, false);
            //Debug.Log($"{unitName} attacked {autoAtkTarget} for {attackDamage}");

            // autoAtkTarget.currHealth -= attackDamage; - old code
            if (highThreatTarget.currHealth <= 0)
            {
                highThreatTarget = null;
            }
        }
    }
    void UseAbility(ScriptableAbility ability)
    {
        if (ability.currentTarget != null)
        {
            if (ability == autoAtk)
            {
                GetComponent<Character>().Animator.SetBool("Slash", true);
                int hpChangeVaried = ApplyRandomness(attackDamage);
                ability.currentTarget.ChangeHealth(hpChangeVaried, true);
                Debug.Log($"{unitName} used {ability.abilityName} on {ability.currentTarget} for {hpChangeVaried}");
            }
            else if (ability.abilityName == "Taunt")
            {
                ApplyStatusEffect(ability.currentTarget, ability.effect);

                
                // Old Taunt implementation to double max team threat score
                //if ((ability.targetType == ScriptableAbility.TargetType.Self) && ability.name == "Taunt")
                //{
                //    // if (ability.name == "Taunt")
                //    // {
                //    float highestThreat = 0;
                //    Actor[] allActors = GameObject.FindObjectsOfType<Actor>();
                //    foreach (Actor teamThreatActor in allActors)
                //    {
                //        if (teamThreatActor.team != team)
                //        {
                //            continue;
                //        }
                //        else
                //        {

                //            if (teamThreatActor.ThreatScore > highestThreat)
                //            {
                //                highestThreat = ThreatScore;
                //            }
                //        }
                //    }
                //    /* This seems to be preventing taunting
                //    if (highestThreat == this.ThreatScore)
                //    {
                //        return;
                //    }
                //    */
                //    ThreatScore = (highestThreat * 2);
                //    hasTaunted = true;
                //    threatResetClock = 3;
                //    // }
            }
            if (ability.abilityName == "Stun")
            {
                ApplyStatusEffect(ability.currentTarget, ability.effect);
            }
            


            else if ((ability.targetType == ScriptableAbility.TargetType.Self) && ability.name == "Stealth")
            {
                ThreatScore = (ThreatScore / 2);
                isStealthed = true;
                threatResetClock = 3;
            }
            else
            {
                Debug.Log("Ability started.");
                
                // This would give them perfect reaction time

                //if (abilityTarget.currHealth <= 0)
                //{
                //    FindAbilityTarget();
                //}


                // beginAtkAnim = true;
                //if (abilityAnimationType == "Cast")
                //{
                //    GetComponent<Character>().Animator.SetBool("Slash", true);
                //}
                //if (abilityAnimationType == "Slash")
                //{ 
                //    GetComponent<Character>().Animator.SetBool("Slash", true);
                //}
                GetComponent<Character>().Animator.SetBool("Slash", true);
                int hpChangeVaried = ApplyRandomness(ability.hpDelta);
                ability.currentTarget.ChangeHealth(hpChangeVaried, true);
                Debug.Log($"{unitName} used {ability.abilityName} on {ability.currentTarget} for {hpChangeVaried}");

                // abilityTarget.ChangeHealth(abilityHpDelta, true);
                Debug.Log($"{unitName} used {ability.abilityName} on {ability.currentTarget} for {ability.hpDelta}");
            }
        }
        // abilityTarget.currHealth += ability.HpDelta; - old code


        // Code for things that don't just modify HP directly

        FindAbilityTarget(ability);
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
        if (hasTaunted)
        {
            threatResetClock -= GameManager.Instance.deltaTime;
            if (threatResetClock <= 0)
            {
                hasTaunted = false;
            }
        }
        if (isStealthed)
        {
            threatResetClock -= GameManager.Instance.deltaTime;
            if (threatResetClock <= 0)
            {
                isStealthed = false;
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

    //void TargetCheckLoop()
    //{
    //    targetCheckCount += GameManager.Instance.deltaTime;
    //    if (targetCheckCount >= targetCheckFrequency)
    //    {
    //        FindPriorityEnemy();
    //        FindAbilityTarget();
    //        targetCheckCount = 0;
    //    }
    //}
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
        actor.CurrentEffects.Add(effect);
    }

    public void ProcessStatusEffect (ScriptableEffect effect)
    {
        effect.remainingDuration += GameManager.Instance.deltaTime;
       
        if (effect.effect == ScriptableEffect.Effect.Taunt)
        {
            isTaunted = true;
            if (effect.remainingDuration >= effect.totalDuration)
            {
                AddToRemoveList(effect, isTaunted);

                // CurrentEffects.Remove(effect);
                // isTaunted = false;
            }
        }

        if (effect.effect == ScriptableEffect.Effect.Stun)
        {
            isStunned = true;
            if (effect.remainingDuration >= effect.totalDuration)
            {
                AddToRemoveList(effect, isStunned);

                // CurrentEffects.Remove(effect);
                // isStunned = false;
            }
        }

        // Code to process each type of effect here
    }

    public void AddToRemoveList(ScriptableEffect effect, bool removedBool)
    {
        int RemoveFromCurrentEffects = CurrentEffects.IndexOf(effect);
        CurrentEffectsRemovalInts.Add(RemoveFromCurrentEffects);
        removedBool = false;
    }

    public void RemoveExpiredEffect(int IndexToRemove)
    {
        CurrentEffects.RemoveAt(IndexToRemove);
    }
}