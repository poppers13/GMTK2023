using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStrategy : MonoBehaviour
{
    public abstract void Execute(BattleBoard board);
}
