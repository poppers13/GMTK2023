using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : BattleEntity
{
    [SerializeField] private Deck _deck;
    [SerializeField] private int _defence; // how much defence the hero currently has

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // coroutine: loop through each row, playing one card on each
    public IEnumerator ExecuteTurn(BattleBoard board)
	{
        for (var currentRow = 0; currentRow < 5; currentRow++)
		{
            yield return new WaitForSeconds(board.ActionWaitTime); // wait between every action
            _deck.PlayTopCard(board, currentRow);
		}

        // once done, make it the enemies' turn
        board.State = GameState.ENEMYTURN;
	}
}
