using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private Vector3 _goalPos; // the position this object is currently trying to move to
    private Transform t;

    // variables for movement
    private float maxMoveSpeed = 15f; // units per second
    private float minMoveSpeed = 1.5f; // units per second
    private float distToSpeed = 1.5f; // how far enough away to trigger max speed?

    // Start is called before the first frame update
    void Start()
    {
        t = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        var dist = Vector2.Distance(t.position, _goalPos); // only want 2d distance
        var multiplier = Mathf.InverseLerp(0, distToSpeed, dist); // value between 0-1
        var speed = (maxMoveSpeed - minMoveSpeed) * multiplier * Time.deltaTime;

        var vectorBetween = (Vector2)_goalPos - (Vector2)t.position; // ignore z value
        var moveVector = vectorBetween.normalized * speed;
        t.position += new Vector3(moveVector.x, moveVector.y, 0);
	}

    // set this card's new goal position
    public void SetNewPos(Vector3 newPos)
	{
        _goalPos = newPos;
	}

    // perform this card's effect
    public virtual void Play(BattleBoard board, int currentRow)
    {
        print("This card has been played!");
    }
}
