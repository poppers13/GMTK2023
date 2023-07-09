using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleEntity : MovingEntity
{
    [SerializeField] protected Camera _cam;
    [SerializeField] private TextMesh _healthLabel; // the label showing the current health of this entity
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
	protected void Awake()
	{
        //_textManager = GameObject.Find("GameManager").GetComponent<TextManager>();
        UpdateHealthLabel();
	}

    private void UpdateHealthLabel()
	{
        if (_healthLabel != null)
		{
            _healthLabel.text = _health.ToString() + "/" + _healthMax.ToString();
        }
	}

	public virtual void TakeDamage(int damage)
	{
        var oldHealth = _health;
        _health = Mathf.Max(_health - damage, 0);
        //_textManager.NewCustomLabel("Health " + (_health - oldHealth).ToString(), _textManager.defaultSkin, 1, new Vector2(100, 40), (Vector2)this.transform.position, new Vector2(0, 3)); // create a label for damage
        UpdateHealthLabel();

        if (_health == 0) // die
		{
            _onDeath.Invoke(); // call all methods associated with this entity dying
            Destroy(gameObject);
		}
	}

    public void Heal(int healing)
    {
        _health = Mathf.Min(_health + healing, _healthMax);
        UpdateHealthLabel();
    }
}
