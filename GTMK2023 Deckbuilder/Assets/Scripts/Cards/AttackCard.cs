using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCard : Card
{
	[SerializeField] private int _damageDealt;

	public override void Play(BattleBoard board, int currentRow)
	{
		board.Rows[currentRow][0].TakeDamage(_damageDealt);
	}
}
