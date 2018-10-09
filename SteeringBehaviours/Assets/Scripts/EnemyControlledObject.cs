using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControlledObject : MonoBehaviour {

    private float horAxis = 0f;
    private float vertAxis = 0f;

    public Vector2 velocity;

    [SerializeField] private float speed = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.D))
        {
            horAxis = speed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            horAxis = -speed;
        }
        else
        {
            horAxis = 0;
        }


        if (Input.GetKey(KeyCode.W))
        {
            vertAxis = speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            vertAxis = -speed;
        }
        else
        {
            vertAxis = 0;
        }

        velocity = new Vector2(horAxis, vertAxis);
        transform.position = new Vector3(transform.position.x + velocity.x, transform.position.y + velocity.y, 0);

    }

}
