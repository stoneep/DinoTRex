using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    private float jumpHeight = 2f;
    
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        anim.SetBool("isRun", true);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
        }
        else if (rb.position.y < startPosition.y)
        {
            isJump = false;
            isTop = false;
            transform.position = startPosition;
        }

        if (isJump)
        {
            if (rb.position.y <= jumpHeight - 0.1f && !isTop)
                transform.position = Vector2.Lerp(transform.position,new Vector2(transform.position.x, jumpHeight), jumpSpeed * Time.deltaTime);
        }
        else
        {
            isTop = true;
        }

        if (transform.position.y > startPosition.y && isTop)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPosition, jumpSpeed * Time.deltaTime);
        }
    }
}
