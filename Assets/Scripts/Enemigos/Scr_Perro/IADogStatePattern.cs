using UnityEngine;
using System.Collections;

public class IADogStatePattern : MonoBehaviour 
{
	public float followSpeed = 4f;
	public bool close = false;

	[HideInInspector] public Transform target;
	[HideInInspector] public Transform followTarget;
	[HideInInspector] public IADogStates currentState;
	[HideInInspector] public IADogFollowing iaDogFollowing;
	[HideInInspector] public IADogResting iaDogResting;

	private void Awake()
	{
		iaDogFollowing = new IADogFollowing (this);
		iaDogResting = new IADogResting (this);

	}

	// Use this for initialization
	void Start () 
	{
		currentState = iaDogResting;
	}

	// Update is called once per frame
	void Update () 
	{
		currentState.UpdateState ();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		currentState.OnTriggerEnter2D (other);
	}
}