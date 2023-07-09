using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceCard : Card
{
	[SerializeField] private int _defenceGiven;

	public override void Play(BattleBoard board, int currentRow)
	{
		board.Hero.GainDefence(_defenceGiven);
	}
}
