using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] LayerMask walls;
    GameObject movePoint;
    Vector3 originalPosition;
    Animator anim;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        originalPosition = transform.position;
        movePoint = new GameObject("MovePoint");
        movePoint.transform.position = transform.position;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movePoint.transform.position, moveSpeed * Time.deltaTime); //Move towards the move point

        if (Vector2.Distance(transform.position, movePoint.transform.position) <= 0.01f) //checks if player arrived at the tile
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1 && !GameManager.instance.IsGamePaused()) //Gets horizontal input and checks if game is not paused
            {
                if (CanMove(movePoint.transform.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f))) //checks if player can move in the direction, starts animation and sounds
                {
                    SoundManager.instance.PlayFootsteps();
                    movePoint.transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                    anim.SetTrigger("right");
                    if (Input.GetAxisRaw("Horizontal") == -1)
                        spriteRenderer.flipX = true;
                    else
                        spriteRenderer.flipX = false;

                }
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1 && !GameManager.instance.IsGamePaused()) //Gets vertical input and checks if game is not paused
            {
                if (CanMove(movePoint.transform.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f))) //checks if player can move in the direction, starts animation and sounds
                {
                    SoundManager.instance.PlayFootsteps();
                    movePoint.transform.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);

                    if (Input.GetAxisRaw("Vertical") == -1)
                        anim.SetTrigger("down");
                    else
                        anim.SetTrigger("up");
                }
            }
            else
            {
                anim.SetTrigger("idle");
            }
        }
    }

    private bool CanMove(Vector3 point)
    {
        if (!Physics2D.OverlapCircle(point, 0.4f, walls))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
