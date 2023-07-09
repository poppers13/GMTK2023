using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    SHUFFLING,
    HEROTURN,
    ENEMYTURN
}

public class BattleBoard : MonoBehaviour
{
    private List<List<Enemy>> _rows = new List<List<Enemy>>();
    [SerializeField] private Hero _hero;
    [SerializeField] private GameState _state;
    [SerializeField] private float _actionWaitTime = 1f; // how long to wait between actions on turns (should be global but whatevs)

    // DEFINED THIS WAY FOR INITIALIZATION SO IT'LL APPEAR IN THE INSPECTOR CUZ I CAN'T BE BOTHERED OTHERWISE
    [SerializeField] private List<GameObject> _row1;
    [SerializeField] private List<GameObject> _row2;
    [SerializeField] private List<GameObject> _row3;
    [SerializeField] private List<GameObject> _row4;
    [SerializeField] private List<GameObject> _row5;

    // -- PROPERTIES --
    public Hero Hero
	{
        get { return _hero; }
	}
    public List<List<Enemy>> Rows
	{
        get { return _rows; }
	}
    public GameState State
	{
        get { return _state; }
        set { _state = value; }
	}
    public float ActionWaitTime
	{
        get { return _actionWaitTime; }
	}


    // -- METHODS --

    // take all given prefabs and chuck their Enemy component references in the rows list
    void Start()
    {
        var objRows = new List<List<GameObject>>();
        objRows.Add(_row1);
        objRows.Add(_row2);
        objRows.Add(_row3);
        objRows.Add(_row4);
        objRows.Add(_row5);

        var rowNum = 0;
        foreach (List<GameObject> objRow in objRows)
		{
            foreach (GameObject o in objRow)
			{
                _rows[rowNum].Add(o.GetComponent<Enemy>()); // add the enemy component to the list
			}
            rowNum++;
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
