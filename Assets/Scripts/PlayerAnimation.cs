using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    public GameObject player;


    public string[] staticDirections = { "Static", "Static Right" };
    public string[] runDirections = { "Run Nazad", "Run Right", "Run Vpered", "Run Left" };
    public string[] stoneAnimations = {"Stone" , "Throw"};

    int lastDirection;
    int lastFlip = 0;

    public SpriteRenderer spriteRenderer;


    public bool IsThrowing = false;

    public bool IsAttaking = false;
    public bool isMirror = false;
    
    
    private void Awake() {
        anim = GetComponent<Animator>();
    }



    public void SetDirection(Vector2 _direction) {
        string[] directionArray = null;
        if (_direction.magnitude < 0.01f) {
            directionArray = staticDirections;
            lastDirection = lastFlip;

        } else {
            directionArray = runDirections;
            lastDirection = DirectionToIndex(_direction);
            if (lastDirection == 1) {
                lastFlip = 1;
            } else if (lastDirection == 3) {
                lastFlip = 0;
            }
        }

        if(!IsAttaking){
            anim.Play(directionArray[lastDirection]);
        }
    }



public void PlayStoneAnimation(int index)
{
    IsAttaking = true;

    if (index >= 0 && index < stoneAnimations.Length)
    {
        if (isMirror)
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Mirror Layer"), 1f);
            anim.SetBool("IsMirror", true);
            transform.localEulerAngles = new Vector3(0f, 180f, 0f);
        }
        else
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Mirror Layer"), 0f);
            anim.SetBool("IsMirror", false);
            transform.localEulerAngles = Vector3.zero;
        }

        anim.Play(stoneAnimations[index]);
        

    }
    else
    {
        Debug.LogWarning("stone error ");
    }
}

    private int DirectionToIndex(Vector2 _direction) {
        Vector2 norDir = _direction.normalized;
        float step = 360 / 4;
        float offset = step / 2;
        float angle = Vector2.SignedAngle(Vector2.up, norDir);

        angle += offset;
        if (angle < 0) {
            angle += 360;
        }

        float stepCount = angle / step;

        int directionIndex = Mathf.FloorToInt(stepCount);
        return directionIndex; 
    }
}