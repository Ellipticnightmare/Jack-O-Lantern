using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessObj : MonoBehaviour
{
    public GameObject mainBod, lAxe, rAxe;
    public Animator anim;
    public EntityType ent;
    private float animTimer;
    public bool moveUp, moveDown;
    private void FixedUpdate()
    {
        if (animTimer > 0)
        {
            animTimer -= Time.deltaTime;
            if (moveUp)
            {
                mainBod.transform.Translate(0, .01f, 0);
            }
            if (moveDown)
            {
                mainBod.transform.Translate(0, -.01f, 0);
            }
        }
        else
        {
            anim.SetBool("Use", false);
            anim.SetBool("Use2", false);
            lAxe.tag = "Untagged";
            rAxe.tag = "Untagged";
            moveUp = false;
            moveDown = false;
        }
    }
    public void LeftClick()
    {
        switch (ent)
        {
            case EntityType.skeleton:
                anim.SetBool("Move", false);
                anim.SetBool("Use", true);
                lAxe.tag = "Breaker";
                rAxe.tag = "Breaker";
                animTimer = 1f;
                break;
            case EntityType.bird:
                moveUp = true;
                animTimer = 1f;
                break;
        }
    }
    public void RightClick()
    {
        switch (ent)
        {
            case EntityType.skeleton:
                anim.SetBool("Move", false);
                anim.SetBool("Use2", true);
                lAxe.tag = "Breaker";
                rAxe.tag = "Breaker";
                animTimer = 1f;
                break;
            case EntityType.bird:
                moveDown = true;
                animTimer = 1f;
                break;
        }
    }
    public enum EntityType
    {
        skeleton,
        bird
    };
}