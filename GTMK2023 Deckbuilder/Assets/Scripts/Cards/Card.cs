using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private Vector2 _goalPos; // the position this object is currently trying to move to
    private Transform t;

    // Start is called before the first frame update
    void Start()
    {
        t = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // for now, just teleport to the current goal position instead of gradually moving
        transform.position = _goalPos;
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
