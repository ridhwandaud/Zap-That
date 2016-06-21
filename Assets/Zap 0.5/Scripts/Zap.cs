using UnityEngine;
using System.Collections;

public class Zap : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Enemy")) {
			GameController.instance.addScore ();
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
	}
}
