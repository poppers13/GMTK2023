using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackStrategy : EnemyStrategy
{
	[SerializeField] private int _damageDealt = 5;

	public override void Execute(BattleBoard board)
	{
		board.Hero.TakeDamage(_damageDealt);
	}
}
