using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plataformaPlanta : MonoBehaviour {

	bool open = false;

	public float retraso = 4;

	public Animator anim;

	private void OnParticleCollision(GameObject other)
	{
		if (!open) 
		{
			open = true;
			anim.SetBool ("mojado", open);
		}
	}

	// Update is called once per frame
	void Update () 
	{
		
	}

	private IEnumerator DelayTimerSecado (float delayLength){
		yield return new WaitForSeconds (delayLength);
		Debug.Log ("tiempo completo");

	}
}
