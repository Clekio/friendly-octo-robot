using System.Collections;
using UnityEngine;

[RequireComponent (typeof (Scr_PlayerVictor))]
public class Scr_PlayerInputVictor : MonoBehaviour {

    Scr_PlayerVictor m_player;
    public bool FacingRight = true;

	// Use this for initialization
	void Start () {
        m_player = GetComponent<Scr_PlayerVictor>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!scr_Respawn.IsDeath)
        {
            Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (directionalInput.x > 0 && !FacingRight)
                Flip();
            else if (directionalInput.x < 0 && FacingRight)
                Flip();

            m_player.SetDirectionalInput(directionalInput);

            if (Input.GetKeyDown(KeyCode.Space))
                m_player.OnJumpInputDown();

            if (Input.GetKeyUp(KeyCode.Space))
                m_player.OnJumpInputUp();

            m_player.CrouchInputValue = Input.GetKey(KeyCode.LeftControl);//Input.GetButton("Crouch");
        }
        else
        {
            Vector2 directionalInput = Vector2.zero;
            m_player.SetDirectionalInput(directionalInput);
        }
    }

    private void Flip()
    {
        FacingRight = !FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
