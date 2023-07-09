using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : BattleEntity
{
    [SerializeField] private Deck _deck;
    [SerializeField] private int _defence; // how much defence the hero currently has

    // -- PROPERTIES --
    public Deck Deck
	{
        get { return _deck; }
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void TakeDamage(int damage)
    {
        var oldDefence = _defence;
        _defence -= damage;
        if (_defence < 0)
		{
            base.TakeDamage(Mathf.Abs(_defence)); // take the remainder as damage
            _defence = 0;
		}

        // show how much defence was lost, if any
        var defenceLost = _defence - oldDefence;
        if (defenceLost > 0)
		{
            _textManager.NewCustomLabel("Defence " + (_defence - oldDefence).ToString(), _textManager.defaultSkin, 1, new Vector2(100, 40), (Vector2)this.transform.position, new Vector2(0, 3)); // create a label for defended damage
        }
    }

    // gain defence for this turn
    public void GainDefence(int defenceGiven)
	{
        _defence += defenceGiven;
        _textManager.NewCustomLabel("Defence +" + defenceGiven.ToString(), _textManager.defaultSkin, 1, new Vector2(100, 40), (Vector2)this.transform.position, new Vector2(0, 3));
    }

    public void ResetDefence()
	{
        _defence = 0;
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
