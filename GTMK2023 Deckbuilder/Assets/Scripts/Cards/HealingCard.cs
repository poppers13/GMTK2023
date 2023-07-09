using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingCard : Card
{
	[SerializeField] private int _healingGiven;

	public override void Play(BattleBoard board, int currentRow)
	{
		board.Hero.Heal(_healingGiven);
	}
}
