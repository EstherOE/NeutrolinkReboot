using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
	public int health=3;
	
	public void TakeDamage(int reduce)
	{
		health -= reduce;
		Debug.Log(health);
		if(health <=0)
		{
			Die();
		}
	}
	
	
	void Die()
	{
		Destroy(gameObject);
	}
}
