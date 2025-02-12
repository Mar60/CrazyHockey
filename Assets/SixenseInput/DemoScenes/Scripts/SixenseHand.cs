
//
// Copyright (C) 2013 Sixense Entertainment Inc.
// All Rights Reserved
//

using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class SixenseHand : MonoBehaviour
{
	public SixenseHands	m_hand;
	public SixenseInput.Controller m_controller = null;

	Animator 	m_animator;
	float 		m_fLastTriggerVal;
	Vector3		m_initialPosition;
	Quaternion 	m_initialRotation;
    private float initialHandPosition;
    private int cpt=0;
    private bool buttonDown = false;
    public AudioClip soundGoalClip;
    private AudioSource soundGoalSource;

    protected void Start() 
	{
		// get the Animator
		m_animator = gameObject.GetComponent<Animator>();
		m_initialRotation = transform.localRotation;
		m_initialPosition = transform.localPosition;
        soundGoalSource = CreateSound(soundGoalClip);

    }
    private AudioSource CreateSound(AudioClip clip)
    {
        //Cr�ation de la souvelle source audio et configuration de ses propri�t�s
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.clip = clip;
        source.volume = 10;
        source.loop = false;
        return source;
    }

    protected void Update()
	{
		if ( m_controller == null )
		{
			m_controller = SixenseInput.GetController( m_hand );
		}

		else if ( m_animator != null )
		{
			UpdateHandAnimation();
		}

        if (m_hand == SixenseHands.RIGHT ? m_controller.GetButtonDown(SixenseButtons.TWO) : m_controller.GetButtonDown(SixenseButtons.ONE))
        {
            initialHandPosition = m_controller.Position.z;
            Debug.Log(initialHandPosition);
        }
    }


    // Updates the animated object from controller input.
    protected void UpdateHandAnimation()
	{
		// Point
		if ( m_hand == SixenseHands.RIGHT ? m_controller.GetButton(SixenseButtons.ONE) : m_controller.GetButton(SixenseButtons.TWO) )
		{
			m_animator.SetBool( "Point", true );
		}
		else
		{
			m_animator.SetBool( "Point", false );
		}
		
		// Grip Ball
		if ( m_hand == SixenseHands.RIGHT ? m_controller.GetButton(SixenseButtons.TWO) : m_controller.GetButton(SixenseButtons.ONE)  )
		{
			m_animator.SetBool( "GripBall", true );
		}
		else
		{
			m_animator.SetBool( "GripBall", false );
		}
				
		// Hold Book
		if ( m_hand == SixenseHands.RIGHT ? m_controller.GetButton(SixenseButtons.THREE) : m_controller.GetButton(SixenseButtons.FOUR) )
		{
			m_animator.SetBool( "HoldBook", true );
		}
		else
		{
			m_animator.SetBool( "HoldBook", false );
		}
				
		// Fist
		float fTriggerVal = m_controller.Trigger;
		fTriggerVal = Mathf.Lerp( m_fLastTriggerVal, fTriggerVal, 0.1f );
		m_fLastTriggerVal = fTriggerVal;
		
		if ( fTriggerVal > 0.01f )
		{
			m_animator.SetBool( "Fist", true );
		}
		else
		{
			m_animator.SetBool( "Fist", false );
		}
		
		m_animator.SetFloat("FistAmount", fTriggerVal);
		
		// Idle
		if ( m_animator.GetBool("Fist") == false &&  
			 m_animator.GetBool("HoldBook") == false && 
			 m_animator.GetBool("GripBall") == false && 
			 m_animator.GetBool("Point") == false )
		{
			m_animator.SetBool("Idle", true);
		}
		else
		{
			m_animator.SetBool("Idle", false);
		}
	}


	public Quaternion InitialRotation
	{
		get { return m_initialRotation; }
	}
	
	public Vector3 InitialPosition
	{
		get { return m_initialPosition; }
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered");
        Vector3 forceVector;
        forceVector = other.gameObject.GetComponent<Renderer>().bounds.center - GetComponent<Transform>().position;
        if (other.gameObject.name == "Ball")
        {
            GetComponentInParent<BonusPlayerManager>().setLastPlayerTouched();
            if (other.gameObject.GetComponent<Control>().trapmat)
            {
                GetComponentInParent<BonusPlayerManager>().setBlur();
                other.gameObject.GetComponent<Control>().setDefault();
            }

            //other.gameObject.GetComponent<Rigidbody>().AddForce(forceVector * 500);
            if (m_hand == SixenseHands.RIGHT ? m_controller.GetButton(SixenseButtons.TWO) : m_controller.GetButton(SixenseButtons.ONE))
            {
                Debug.Log(m_controller.Position.z);
                if(m_controller.Position.z > 0 && initialHandPosition < -100)
                {
                    
                    other.gameObject.GetComponent<Control>().setForce(forceVector);
                    other.gameObject.GetComponent<Control>().addforce();
                    soundGoalSource.Play();

                }
                else
                {
                    GetComponent<RigidbodyFirstPersonController>().setSlowed();
                    other.gameObject.GetComponent<Control>().setForce(forceVector);
                    other.gameObject.GetComponent<Control>().addforce();
                    soundGoalSource.Play();

                }
            }

        }

    }
}

