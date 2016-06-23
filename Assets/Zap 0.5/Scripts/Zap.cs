using UnityEngine;
using System.Collections;

public class Zap : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Enemy")) {
			other.GetComponent<EnemyHealth>().TakeDamage(1);
			Destroy (gameObject);
		}
	}
}
