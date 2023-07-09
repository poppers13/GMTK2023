using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleEntity : MovingEntity
{
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
    public void TakeDamage(int damage)
	{
        _health = Mathf.Max(_health - damage, 0);

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
}
