using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    SHUFFLING,
    HEROTURN,
    ENEMYTURN,
    WAITFORENEMIES
}

public class BattleBoard : MonoBehaviour
{
    private List<List<Enemy>> _rows = new List<List<Enemy>>();
    [SerializeField] private Hero _hero;
    [SerializeField] private GameState _state;
    [SerializeField] private TextMesh _timerLabel; // shows the time left in the shuffle
    [SerializeField] private float _actionWaitTime = 1f; // how long to wait between actions on turns (should be global but whatevs)
    private int _enemiesPerWave = 6;

    // DEFINED THIS WAY FOR INITIALIZATION SO IT'LL APPEAR IN THE INSPECTOR CUZ I CAN'T BE BOTHERED OTHERWISE
    [SerializeField] private List<GameObject> _row1;
    [SerializeField] private List<GameObject> _row2;
    [SerializeField] private List<GameObject> _row3;
    [SerializeField] private List<GameObject> _row4;
    [SerializeField] private List<GameObject> _row5;

    // grid size properties (assumes transform is top-left of grid)
    private float _gridWidth = 1.5f; // in units, how wide each enemy grid square is
    private float _gridHeight = 1.2f; // in units, how tall each enemy grid square is

    private float _shuffleTimeMax = 30.0f; // how long the player can spend shuffling
    private float _shuffleTime;

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
    public float ShuffleTime
	{
        get { return _shuffleTime; }
        set { _shuffleTime = value; }
	}


    // -- METHODS --

    // take all given prefabs and chuck their Enemy component references in the rows list
    void Start()
    {
        // create five empty rows to place enemies into
        for (var i = 0; i < 5; i++)
		{
            _rows.Add(new List<Enemy>());
        }

        _shuffleTime = _shuffleTimeMax;

        var objRows = new List<List<GameObject>>();
        objRows.Add(_row1);
        objRows.Add(_row2);
        objRows.Add(_row3);
        objRows.Add(_row4);
        objRows.Add(_row5);

        for (var rowNum = 0; rowNum < 5; rowNum++)
		{
            foreach (GameObject o in objRows[rowNum])
            {
                print("Row " + rowNum + ": adding " + o.name);
                var newObj = Instantiate(o); // create an instance of the prefab stored
                _rows[rowNum].Add(newObj.GetComponent<Enemy>()); // add the enemy component to the list
            }
        }

        MoveEnemies(); // move enemies to their proper positions
    }

    // Update is called once per frame
    void Update()
    {
        // figure out what to do based on state
        switch (_state) {
            case GameState.SHUFFLING: // the player shuffling their cards
                _shuffleTime -= Time.deltaTime;
				if (_shuffleTime <= 0)
				{
					_shuffleTime = _shuffleTimeMax;
					_state = GameState.HEROTURN;
					var heroCoroutine = _hero.ExecuteTurn(this);
					_hero.StartCoroutine(heroCoroutine);
					_hero.Deck.UpdateVisibility(); // to unhighlight cards
                    _timerLabel.text = ""; // hide the timer
				}
                else
				{
                    _timerLabel.text = _shuffleTime.ToString("0.0");
				}
				break;
            case GameState.HEROTURN:
                // nothing needs to be done, since the hero's turn coroutine will be running
                break;
            case GameState.ENEMYTURN:
                // this is a pretty shoddy way of doing it, but ~fuck it~
                // if there are any destroyed enemies among gus, remove them from referencing
                foreach (List<Enemy> row in _rows)
				{
                    var enemiesToRemove = new List<Enemy>();
                    foreach (Enemy e in row)
					{
                        if (e == null)
						{
                            enemiesToRemove.Add(e);
						}
					}
                    // only remove enemies *after* all are tracked, otherwise you might skip things
                    foreach (Enemy e in enemiesToRemove)
					{
                        row.Remove(e);
					}
				}
                MoveEnemies(); // move all enemies, in case any were in fact obliterated
                var enemyCoroutine = RunEnemies();
                StartCoroutine(enemyCoroutine);
                _state = GameState.WAITFORENEMIES;
				break;
            case GameState.WAITFORENEMIES:
                // again, nothing needs to be done, since a coroutine got run
                break;
		}
	}

    // move all enemies to their assigned positions in the board grid
    public void MoveEnemies()
	{
        for (var r = 0; r < 5; r++)
		{
            var row = _rows[r];
            for (var c = 0; c < row.Count; c++)
			{
                var enemy = row[c];

                var newx = this.transform.position.x + (_gridWidth * c);
                var newy = this.transform.position.y - (_gridHeight * r);

                enemy.SetNewPos(new Vector3(newx, newy, 0));
			}
		}
	}

    private IEnumerator RunEnemies()
	{
        foreach (var row in _rows)
		{
            foreach (var enemy in row)
			{
                enemy.ExecuteTurn(this);
                yield return new WaitForSeconds(_actionWaitTime);
            }
		}

        // if all enemies are dead, you win (ideally, spawn new ones and add new cards)
        var enemyCount = 0;
        foreach (List<Enemy> row in _rows)
		{
            enemyCount += row.Count;
		}
        if (enemyCount == 0)
		{
            SceneManager.LoadScene("YouWin");

            // pick a random enemy from list and create it
   //         for (var i = 0; i < _enemiesPerWave; i++)
			//{
   //             var spawnRow = rows[Random.Range(0, 4);
			//}
		}

        // once done, set back to shuffling and reset cursor
        _state = GameState.SHUFFLING;
        _hero.Deck.ResetCursor();
        _hero.ResetDefence(); // set defence back to 0
        _hero.Deck.UpdateVisibility(); // to re-highlight the selected cards
	}
}
