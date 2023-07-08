using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private CardStrategy _cardStrat;
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
        t.position = _goalPos;
	}

    // perform this card's effect
    public void Play(BattleBoard board, int currentRow)
	{
        _cardStrat.Execute(board, currentRow);
	}

    // set this card's new goal position
    public void SetNewPos(Vector2 newPos)
	{
        _goalPos = newPos;
	}
}
