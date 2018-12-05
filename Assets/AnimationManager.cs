using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {
    Animator anim;
    public float timer,maneuvertimer;
    public Direction previousDirection;
    public bool forcestop;
    public GameObject jumpsensor, slidesensor, colsensor;
    bool backingup;
    public bool checkForCollision;
	// Use this for initialization
	void Awake () {
        previousDirection = Direction.none;
        anim = this.GetComponent<Animator>();
        timer = 100;
        maneuvertimer = 100;
        backingup = false;
	}

    private void Update()
    {
        timer += 1 * Time.deltaTime;
        maneuvertimer += 1 * Time.deltaTime;


        if (backingup)
        {
            if (!colsensor.GetComponent<Sensor>().isInside)
            {
                anim.SetTrigger("Stop");
                previousDirection = Direction.none;
                this.GetComponent<Unit>().RequestPath();
                backingup = false;
            }
        }
        else
        {

            if (forcestop)
            {
                ResetAnim();
                anim.SetTrigger("Stop");
            }

            //just some vertical correction
            if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("Maneuver"))
            {
                if (this.transform.position.y < -0.03f)
                {
                    transform.Translate(Vector3.up * Time.deltaTime * 0.5f);
                }
                if (this.transform.position.y > 0.03f)
                {
                    transform.Translate(Vector3.down * Time.deltaTime * 0.5f);
                }
            }

            if (maneuvertimer > 0.7f)
            {
                if (jumpsensor.GetComponent<Sensor>().isInside)
                {
                    anim.SetTrigger("Jump");
                }
                else if (slidesensor.GetComponent<Sensor>().isInside)
                {
                    anim.SetTrigger("Slide");
                }
                maneuvertimer = 0;
            }

            if (checkForCollision)
            {
                if (colsensor.GetComponent<Sensor>().isInside)
                {
                    ResetAnim();
                    anim.ResetTrigger("Stop");
                    backingup = true;
                    anim.SetTrigger("Backup");
                }
            }
        }

    }

    public void ChangeAnim(Direction newD)
    {
        if (!backingup)
        {
            if (timer > 0.25f)
            {
                if (newD != previousDirection)
                {
                    switch (newD)
                    {
                        case Direction.forward:
                            ResetAnim();
                            anim.SetBool("Forward", true);
                            break;
                        case Direction.left:
                            ResetAnim();
                            anim.SetBool("Left", true);
                            break;
                        case Direction.right:
                            ResetAnim();
                            anim.SetBool("Right", true);
                            break;
                        case Direction.idleleft:
                            ResetAnim();
                            anim.SetBool("IdleLeft", true);
                            break;
                        case Direction.idleright:
                            ResetAnim();
                            anim.SetBool("IdleRight", true);
                            break;
                        case Direction.sharpleft:
                            ResetAnim();
                            anim.SetTrigger("SLeft");
                            break;
                        case Direction.sharpright:
                            ResetAnim();
                            anim.SetTrigger("SRight");
                            break;
                        case Direction.none:
                            ResetAnim();
                            anim.SetTrigger("Stop");
                            break;
                        default:
                            break;
                    }

                    previousDirection = newD;
                }
                timer = 0;
            }
        }
    }

    public void ResetAnim()
    {
        anim.SetBool("Forward", false);
        anim.SetBool("Left", false);
        anim.SetBool("Right", false);
        anim.SetBool("IdleLeft", false);
        anim.SetBool("IdleRight", false);
    }
}
