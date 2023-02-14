using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState{
    Idle, Walking, Flying, Stunned
}

public class PlayerController : MonoBehaviour
{
    public PlayerState curState;

    public float moveSpeed;
    public float flyingSpeed;
    public bool grounded;
    public float stunDuration;
    private float stunStartTime;

    public Rigidbody2D rig;
    public Animator anim;
    public ParticleSystem jetpackParticle;

    void Move ()
    {
        float dir = Input.GetAxis("Horizontal");

        if (dir > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        else if (dir < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        rig.velocity = new Vector2(dir * moveSpeed, rig.velocity.y);
    }

    void Fly ()
    {
        rig.AddForce(Vector2.up * flyingSpeed, ForceMode2D.Impulse);

        if (!jetpackParticle.isPlaying)
        {
            jetpackParticle.Play();
        }
    }

    bool IsGrounded ()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.85f), Vector2.down, 0.3f);
        
        if(hit.collider != null)
        {
            if(hit.collider.CompareTag("Floor"))
            {
                return true;
            }
        }
        return false;
    }

// sets the player's state
void SetState ()
{
    // don't worry about changing states if the player's stunned
    if (curState != PlayerState.Stunned)
    {
        // idle
        if (rig.velocity.magnitude == 0 && grounded)
        {
            curState = PlayerState.Idle;
        }
        // walking
        if (rig.velocity.x != 0 && grounded)
        {
            curState = PlayerState.Walking;
        }
        // flying
        if (rig.velocity.magnitude != 0 && !grounded)
        {
            curState = PlayerState.Flying;
        }
            
    }
    // tell the animator we've changed states
        anim.SetInteger("State", (int)curState);
}

    public void Stun ()
    {
        curState = PlayerState.Stunned;
        rig.velocity = Vector2.down * 3;
        stunStartTime = Time.time;
        jetpackParticle.Stop();
    }   

    // checks for user input to control player
    void CheckInputs ()
    {
        if (curState != PlayerState.Stunned)
        {
        // movement
        Move();
        // flying
        if (Input.GetKey(KeyCode.UpArrow))
            Fly();
        else
            jetpackParticle.Stop();
        }
    // update our current state
     SetState();
    }

    void FixedUpdate ()
{
    grounded = IsGrounded();
    CheckInputs();
    // is the player stunned?
    if(curState == PlayerState.Stunned)
    {
        // has the player been stunned for the duration?
        if(Time.time - stunStartTime >= stunDuration)
        {
            curState = PlayerState.Idle;
        }
    }
}

// called when the player enters another object's collider
void OnTriggerEnter2D (Collider2D col)
{
    // if the player isn't already stunned, stun them if the object was an obstacle
    if(curState != PlayerState.Stunned)
    {
        if(col.GetComponent<Obstacle>())
        {
            Stun();
        }
    }
}

}
