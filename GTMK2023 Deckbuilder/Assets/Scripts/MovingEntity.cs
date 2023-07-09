using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEntity : MonoBehaviour
{
    // variables for movement
    private float maxMoveSpeed = 30f; // units per second
    private float minMoveSpeed = 1.5f; // units per second
    private float distToSpeed = 5f; // how far enough away to trigger max speed?
    private Vector3 _goalPos; // the position this object is currently trying to move to

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var dist = Vector2.Distance(transform.position, _goalPos); // only want 2d distance
        var multiplier = Mathf.InverseLerp(0, distToSpeed, dist); // value between 0-1
        var speed = (maxMoveSpeed - minMoveSpeed) * multiplier * Time.deltaTime;

        var vectorBetween = (Vector2)_goalPos - (Vector2)transform.position; // ignore z value
        var moveVector = vectorBetween.normalized * speed;
        transform.position += new Vector3(moveVector.x, moveVector.y, 0);
    }

    // set this card's new goal position
    public void SetNewPos(Vector3 newPos)
    {
        _goalPos = newPos;
    }
}
