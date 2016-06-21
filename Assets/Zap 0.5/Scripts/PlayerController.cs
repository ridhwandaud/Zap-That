using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

[RequireComponent(typeof (Rigidbody2D))]
[RequireComponent(typeof (AudioSource))]
public class PlayerController: MonoBehaviour 
{
	//Components
	Rigidbody2D rb2D;
	AudioSource audioSource;
	Animator spriteAnimator;
	Animator rotateAnimator;

	//Handling
	[Range(100, 500)]
	public float zapSpeed;
	public AudioClip playerSound;
	Transform rotateChild,spriteChild;
	public GameObject zap;

	//boolean
	bool isMoving;
	bool isHolding;
	public bool usingHold;
	public bool usingTouchPosition;
	public bool usingMousePosition;
	public bool usingSpaceBar;
	public bool usingDetectUI;

	private int fingerId= -1;


	void Start () 
	{
		if (Application.isMobilePlatform) {
			fingerId = 0; //for mobile and unity
		}
		rb2D = GetComponent<Rigidbody2D> ();
		audioSource = GetComponent<AudioSource> ();

		rotateChild = transform.GetChild (0);
		rotateAnimator = rotateChild.GetComponent<Animator> ();
		spriteAnimator = GetComponent<Animator> ();

	}

	void Update () 
	{
		//Input for Android and IOS
		if (usingHold)
		{
			TouchAndHold ();
		}
			
		//Input for PC
		if (usingSpaceBar) 
		{
			SpaceBar ();
		}

	}

	void TouchAndHold()
	{
		for (var i = 0; i < Input.touchCount; ++i) 
		{ 
			Touch touch = Input.GetTouch(i);
			int pointerID = touch.fingerId;

			if(usingDetectUI)
			{
				if(EventSystem.current.IsPointerOverGameObject(pointerID))
				{
					// touch on ui
					return;
				}
			}

			if (touch.phase == TouchPhase.Began ) //Hold
			{
				// if active , rotate animation will start
				Debug.Log("Begin");
				ReadyToJumpWithRotateArrow();
			}  
			else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) //Release
			{
				Debug.Log("Shoot");
				MoveUsingRotateArrow ();
			}

		}
	}

	void SpaceBar()
	{
		if (Input.GetButtonDown ("Jump")) //Hold SpaceBar
		{
			ReadyToJumpWithRotateArrow();
		}
		if (Input.GetButtonUp ("Jump")) //Release SpaceBar
		{
			MoveUsingRotateArrow ();
		}
	}
		

	void ReadyToJumpWithRotateArrow()
	{
		rotateChild.gameObject.SetActive (true); // if active , rotate animation will start

		// play aim animation 
		spriteAnimator.SetBool("Aim",true);
	}

	void MoveUsingRotateArrow()
	{
		//spriteAnimator.SetBool("isReadyToJump",false);
		rotateChild.gameObject.SetActive(false);

		Shoot ();
	
		//spriteAnimator.SetBool("isMoving",true);
		//audioSource.clip = playerSound;
		//audioSource.Play();
	}

	void Shoot()
	{
		spriteAnimator.SetBool("Aim",false);
		GameObject bullet = (GameObject)Instantiate(zap,transform.position, Quaternion.identity);
		bullet.transform.rotation = rotateChild.rotation;// this is to follow rotating child 
		bullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * zapSpeed); //to add force according to rotation
	}

	/** Touch UI */

	void TouchUI()
	{
		if(!EventSystem.current.IsPointerOverGameObject())
		{
			// touch on ui
			return;
		}
	}
}