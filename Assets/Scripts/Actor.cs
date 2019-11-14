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
        else
        {
            float distanceToClosestActor = Mathf.Infinity;
            Actor closestActor = null;
            Actor[] allActors = GameObject.FindObjectsOfType<Actor>();

            foreach (Actor currentActor in allActors)
            {
                if (currentActor.myTeam == myTeam) // I can't get these to compare - have tried Actor.Team, this.Team, and other formats
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

                {
                    transform.position = Vector2.MoveTowards(transform.position, closestActor.transform.position, speed * Time.deltaTime);
                }
            }
        }
    }
}
