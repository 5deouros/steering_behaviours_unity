using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringManager : MonoBehaviour {

    [SerializeField] private bool seekOn = false;
    [SerializeField] private bool fleeOn = false;
    [SerializeField] private bool wanderOn = false;
    [SerializeField] private bool pursuitOn = false;
    [SerializeField] private bool evadeOn = false;
   // [SerializeField] private bool leaderFollowOn = false;

    [SerializeField] private SteeringBehaviour obj1;
    [SerializeField] private GameObject obj2;
    [SerializeField] private PlayerControlledObject obj2script;
    [SerializeField] private GameObject obj3;
    [SerializeField] private EnemyControlledObject obj3script;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (seekOn)
        {
            obj1.SeekAndArrive(obj2.transform.position);
        }

        if (fleeOn)
        {
            obj1.Flee(obj3.transform.position);
        }

        if (wanderOn)
        {
            obj1.Wander();
        }

        if (pursuitOn)
        {
            obj1.Pursuit(obj2.transform.position, obj2script.velocity);
        }

        if (evadeOn)
        {
            obj1.Evade(obj3.transform.position, obj3script.velocity);
        }

       /* if (leaderFollowOn)
        {
            obj1.LeaderFollow(obj2script.velocity, obj2.transform.position);
        }*/

	}
}
