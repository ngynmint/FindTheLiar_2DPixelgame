using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  public Sprite PlayerMoveRight;
    public Sprite PlayerFront_Idle;
    public Sprite PlayerBack_Idle;
    public Sprite PlayerMoveLeft;
  

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
         Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (move.x > 0)
            spriteRenderer.sprite = PlayerMoveRight;
        else if (move.x < 0)
            spriteRenderer.sprite = PlayerMoveLeft;
        else if (move.y > 0)
            spriteRenderer.sprite = PlayerBack_Idle;
        else if (move.y < 0)
            spriteRenderer.sprite = PlayerFront_Idle;
    }
}
