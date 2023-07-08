using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardStrategy : MonoBehaviour
{
	public void Execute(BattleBoard board, int currentRow)
	{
		print("This card strategy has been run!");
	}
}
