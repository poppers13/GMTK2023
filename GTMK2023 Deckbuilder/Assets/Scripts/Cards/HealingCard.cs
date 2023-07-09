using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingCard : Card
{
	[SerializeField] private int _healingGiven;
	[SerializeField] private AudioSource _sfx;

	public override void Play(BattleBoard board, int currentRow)
	{
		_sfx.Play();
		board.Hero.Heal(_healingGiven);
	}
}
