using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	int health = 1;
	Animator animator;
	bool isDead;
	Rigidbody2D rb2D;
	public GameObject laserBurst;

	void Start () {
		animator = GetComponent<Animator> ();
		rb2D = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		if (health <= 0 && !isDead) {
			GameController.instance.addScore ();
			Die ();
		}
	}

	void Die()
	{
		isDead = true;
		GameObject laser = Instantiate (laserBurst, transform.position, Quaternion.identity)as GameObject;
		Destroy (laser, 0.5f);
		float random = (float)Random.Range (100, 500);
		Vector2 force = new Vector2 (0f, random);

		rb2D.AddForce (force);
		rb2D.gravityScale = 3;
		Destroy (gameObject,1.5f);
	}

	public void TakeDamage(int dmg)
	{
		health -= dmg;
	}
}
