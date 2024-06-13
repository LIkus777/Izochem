using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    public string[] staticDirections = { "Static", "Static Right" };
    public string[] runDirections = { "Run Nazad", "Run Right", "Run Vpered", "Run Left" };

    int lastDirection;
    int lastFlip = 0;
    
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
            } else {
                lastFlip = 0;
            }
        }

        anim.Play(directionArray[lastDirection]);
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