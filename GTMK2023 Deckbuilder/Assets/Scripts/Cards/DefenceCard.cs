using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceCard : Card
{
	[SerializeField] private int _defenceGiven;
	[SerializeField] private AudioSource _sfx;
	public override void Play(BattleBoard board, int currentRow)
	{
		_sfx.Play();
		board.Hero.GainDefence(_defenceGiven);
	}
}
