using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleEntity : MovingEntity
{
    protected TextManager _textManager;
    [SerializeField] private int _health;
    [SerializeField] private int _healthMax;

    [SerializeField] private UnityEvent _onDeath;

    // PROPERTIES
    public int Health
	{
        get { return _health; }
	}
    public int HealthMax
	{
        get { return _healthMax; }
	}

	// METHODS
    // find the text manager in the scene
	void Start()
	{
        _textManager = GameObject.Find("GameManager").GetComponent<TextManager>();
	}

	public virtual void TakeDamage(int damage)
	{
        var oldHealth = _health;
        _health = Mathf.Max(_health - damage, 0);
        _textManager.NewCustomLabel("Health " + (_health - oldHealth).ToString(), _textManager.defaultSkin, 1, new Vector2(100, 40), (Vector2)this.transform.position, new Vector2(0, 3)); // create a label for damage

        if (_health == 0) // die
		{
            _onDeath.Invoke(); // call all methods associated with this entity dying
            Destroy(gameObject);
		}
	}

    public void Heal(int healing)
    {
        _health = Mathf.Min(_health + healing, _healthMax);
    }

    // draw your own health slightly below you
	private void OnGUI()
	{
        GUI.Label(new Rect(new Vector2(transform.position.x, transform.position.y - 0.7f), new Vector2(100, 40)), _health.ToString() + "/" + _healthMax.ToString());
	}
}
