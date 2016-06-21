using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	int health = 1;

	void Start () {
	
	}

	void Update () {
		if (health <= 0) {
			GameController.instance.addScore ();
			Destroy (gameObject);
		}
	}

	public void TakeDamage(int dmg)
	{
		health -= dmg;
	}
}
