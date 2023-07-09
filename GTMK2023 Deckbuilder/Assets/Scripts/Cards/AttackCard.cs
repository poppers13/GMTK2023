using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCard : Card
{
	[SerializeField] private int _damageDealt;

	public override void Play(BattleBoard board, int currentRow)
	{
		var enemyList = board.Rows[currentRow];
		if (enemyList.Count > 0)
		{
			enemyList[0].TakeDamage(_damageDealt);
		}
	}
}
