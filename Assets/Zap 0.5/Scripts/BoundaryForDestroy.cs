using UnityEngine;
using System.Collections;

public class BoundaryForDestroy : MonoBehaviour {

	public PlayerController player;
	void OnTriggerExit2D(Collider2D coll){

		if (coll.CompareTag ("Zap")) {
			Destroy (coll.gameObject); //Destroy
			player.isDead = true;
			GameController.instance.GameOver ();
		}
	}
}
