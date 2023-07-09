using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MovingEntity
{
    // perform this card's effect
    public virtual void Play(BattleBoard board, int currentRow)
    {
        print("This card has been played!");
    }
}
