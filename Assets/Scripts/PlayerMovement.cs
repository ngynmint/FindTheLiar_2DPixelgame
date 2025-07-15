using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Sprite PlayerMoveRight;
    public Sprite PlayerFront_Idle;
    public Sprite PlayerBack_Idle;
    public Sprite PlayerMoveLeft;
    public AudioSource walkingAudio;


    private SpriteRenderer spriteRenderer;
    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()

    {
        if (!canMove)
            return;


        Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        transform.Translate(move * 6f * Time.deltaTime);

        if (move.x > 0)
            spriteRenderer.sprite = PlayerMoveRight;
        else if (move.x < 0)
            spriteRenderer.sprite = PlayerMoveLeft;
        else if (move.y > 0)
            spriteRenderer.sprite = PlayerBack_Idle;
        else if (move.y < 0)
            spriteRenderer.sprite = PlayerFront_Idle;
            if (move != Vector2.zero)

        {
            if (!walkingAudio.isPlaying)
                walkingAudio.Play();
        }
        else
        {
            if (walkingAudio.isPlaying)
                walkingAudio.Stop();
        }
    }
}
