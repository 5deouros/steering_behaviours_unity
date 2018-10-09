using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehaviour : MonoBehaviour
{

    private Vector2 velocity;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float maxForce;
    [SerializeField] private float mass;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float slowRadius;
    [SerializeField] private float wanderDeltaMagnitude;
    [SerializeField] private float distanceBehindLeader;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SeekAndArrive(Vector2 targetpos)
    {
        //transform.position = new Vector3 (transform.position.x + velocity.x, transform.position.y + velocity.y, 0);
        //velocity = (targetpos - new Vector2(transform.position.x, transform.position.y)).normalized * maxVelocity;
        Vector2 desiredVelocity = (targetpos - new Vector2(transform.position.x, transform.position.y)).normalized * maxVelocity;
        Vector2 steering = desiredVelocity - velocity;

        steering = Vector2.ClampMagnitude(steering, maxForce);
        steering = steering / mass;

        velocity = Vector2.ClampMagnitude((velocity + steering), maxSpeed);
        transform.position = new Vector2(transform.position.x, transform.position.y) + velocity;

        //Arriving
        desiredVelocity = targetpos - new Vector2(transform.position.x, transform.position.y);
        float distance = targetpos.sqrMagnitude;

        if (distance < slowRadius)
        {
            desiredVelocity = desiredVelocity.normalized * maxVelocity * (distance / slowRadius);
        }
        else
        {
            desiredVelocity = desiredVelocity.normalized * maxVelocity;
        }

        steering = desiredVelocity - velocity;
        velocity = Vector2.ClampMagnitude(velocity + steering, maxSpeed);
        transform.position = new Vector2(transform.position.x, transform.position.y) + velocity;

    }

    public void Flee(Vector2 targetpos)
    {
        //transform.position = new Vector3 (transform.position.x + velocity.x, transform.position.y + velocity.y, 0);
        //velocity = (targetpos - new Vector2(transform.position.x, transform.position.y)).normalized * maxVelocity;
        Vector2 desiredVelocity = (targetpos - new Vector2(transform.position.x, transform.position.y)).normalized * maxVelocity;
        Vector2 steering = -desiredVelocity - velocity;

        steering = Vector2.ClampMagnitude(steering, maxForce);
        steering = steering / mass;

        velocity = Vector2.ClampMagnitude((velocity + steering), maxSpeed);
        transform.position = new Vector2(transform.position.x, transform.position.y) + velocity;
    }

    public void Wander()
    {
        float angle = Random.Range(-2, 2) * Mathf.Deg2Rad;
        float co = Mathf.Sin(angle);
        float ca = Mathf.Cos(angle);

        velocity = velocity + new Vector2(co, ca).normalized * wanderDeltaMagnitude;
        transform.position = new Vector2(transform.position.x, transform.position.y) + velocity;
    }

    public void Pursuit(Vector2 targetpos, Vector2 targetvel)
    {
        int t = 2;
        Vector2 futurePosition = targetpos + targetvel * t;
        SeekAndArrive(futurePosition);
    }

    public void Evade(Vector2 targetpos, Vector2 targetvel)
    {
        int t = 2;
        Vector2 futurePosition = targetpos + targetvel * t;
        Flee(futurePosition);
    }

    public void LeaderFollow(Vector2 leaderVel, Vector2 leaderPos)
    {
        Vector2 followpoint = leaderVel * -1;
        followpoint = followpoint.normalized;
        followpoint = followpoint * distanceBehindLeader;

        Vector2 behind = leaderPos + followpoint;
        SeekAndArrive(behind);
        //Vector2 force = LeaderSeekAndArrive(followpoint);

        //return force;
    }

    public Vector2 LeaderSeekAndArrive(Vector2 targetpos)
      {
          //transform.position = new Vector3 (transform.position.x + velocity.x, transform.position.y + velocity.y, 0);
          //velocity = (targetpos - new Vector2(transform.position.x, transform.position.y)).normalized * maxVelocity;
          Vector2 desiredVelocity = (targetpos - new Vector2(transform.position.x, transform.position.y)).normalized * maxVelocity;
          Vector2 steering = desiredVelocity - velocity;

          steering = Vector2.ClampMagnitude(steering, maxForce);
          steering = steering / mass;

          velocity = Vector2.ClampMagnitude((velocity + steering), maxSpeed);
          transform.position = new Vector2(transform.position.x, transform.position.y) + velocity;

          //Arriving
          desiredVelocity = targetpos - new Vector2(transform.position.x, transform.position.y);
            float distance = targetpos.sqrMagnitude;

            if (distance < slowRadius)
             {
            desiredVelocity = desiredVelocity.normalized * maxVelocity * (distance / slowRadius);
             }
            else
             {
                desiredVelocity = desiredVelocity.normalized * maxVelocity;
             }

            steering = desiredVelocity - velocity;
            velocity = Vector2.ClampMagnitude(velocity + steering, maxSpeed);
            return velocity;
        }
    }