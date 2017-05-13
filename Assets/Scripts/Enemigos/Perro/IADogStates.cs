using UnityEngine;
using System.Collections;

public interface IADogStates

{

	void UpdateState();

	void OnTriggerEnter2D (Collider2D other);

	void ToIADogFollowing();

	void ToIADogResting();

}