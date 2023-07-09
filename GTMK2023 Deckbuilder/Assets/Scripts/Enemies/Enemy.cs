using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BattleEntity
{
    [SerializeField] private int _waitTurns;
    private int _turnsLeft;
    [SerializeField] private EnemyStrategy _enemyStrat;

	private void Start()
	{
		_turnsLeft = _waitTurns;
	}

	// count down; if countdown over, take effect
	public void ExecuteTurn(BattleBoard board)
	{
		_turnsLeft--;
		if (_turnsLeft <= 0)
		{
			_turnsLeft = _waitTurns;
			_enemyStrat.Execute(board);
		}
	}
}
