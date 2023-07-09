using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCard : Card
{
	[SerializeField] private int _damageDealt;
	[SerializeField] private AudioSource _sfx;

	public override void Play(BattleBoard board, int currentRow)
	{
		_sfx.Play();
		var enemyList = board.Rows[currentRow];
		if (enemyList.Count > 0)
		{
			enemyList[0].TakeDamage(_damageDealt);
		}
	}
}
