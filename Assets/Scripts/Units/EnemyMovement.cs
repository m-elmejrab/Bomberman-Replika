using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    GameObject movePoint;
    [SerializeField] float moveSpeed;
    [SerializeField] LayerMask walls;
    [SerializeField] LayerMask enemies;

    bool moveSmartlyAwayFromPlayer = false;
    GameObject player;
    Animator anim;
    SpriteRenderer spriteRenderer;
    MoveDirection direction;

    void Start()
    {
        movePoint = new GameObject(transform.name + "MovePoint");
        movePoint.transform.position = transform.position;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movePoint.transform.position, moveSpeed * Time.deltaTime); //Move towards the move point

        if (Vector2.Distance(transform.position, movePoint.transform.position) <= 0.01f) //checks if enemy arrived at the tile
        {
            if (moveSmartlyAwayFromPlayer)
            {
                MoveSmartly();
            }
            else
            {
                MoveRandomly();
            }
        }
    }

    private void MoveRandomly()
    {
        int direction = Random.Range(0, 4);
        switch (direction)
        {
            case 0: //move up
                if (CanMove(movePoint.transform.position + new Vector3(0f, 1f, 0f)))
                {
                    movePoint.transform.position += new Vector3(0f, 1f, 0f);
                    anim.SetTrigger("up");
                }
                break;
            case 1: //move right
                if (CanMove(movePoint.transform.position + new Vector3(1f, 0f, 0f)))
                {
                    movePoint.transform.position += new Vector3(1f, 0f, 0f);
                    anim.SetTrigger("right");
                    spriteRenderer.flipX = false;

                }
                break;
            case 2: //move down
                if (CanMove(movePoint.transform.position + new Vector3(0f, -1f, 0f)))
                {
                    movePoint.transform.position += new Vector3(0f, -1f, 0f);
                    anim.SetTrigger("down");

                }
                break;
            case 3: //move left
                if (CanMove(movePoint.transform.position + new Vector3(-1f, 0f, 0f)))
                {
                    movePoint.transform.position += new Vector3(-1f, 0f, 0f);
                    anim.SetTrigger("right");
                    spriteRenderer.flipX = true;

                }
                break;
            default:
                break;
        }
    }

    private void MoveSmartly()
    {
        float furthestDistanceFromPlayer = 0f;
        Vector3 bestPointToMove = Vector3.zero;

        //Calculate the distance in each direction, choose the furthest from player
        if (CanMove(movePoint.transform.position + new Vector3(0f, 1f, 0f)))
        {
            float distanceInThisDirection = Vector3.Distance(movePoint.transform.position + new Vector3(0f, 1f, 0f), player.transform.position);
            if (furthestDistanceFromPlayer == 0 || distanceInThisDirection > furthestDistanceFromPlayer)
            {
                furthestDistanceFromPlayer = distanceInThisDirection;
                bestPointToMove = movePoint.transform.position + new Vector3(0f, 1f, 0f);
                direction = MoveDirection.Up;
            }
        }

        if (CanMove(movePoint.transform.position + new Vector3(1f, 0f, 0f)))
        {
            float distanceInThisDirection = Vector3.Distance(movePoint.transform.position + new Vector3(1f, 0f, 0f), player.transform.position);
            if (furthestDistanceFromPlayer == 0 || distanceInThisDirection > furthestDistanceFromPlayer)
            {
                furthestDistanceFromPlayer = distanceInThisDirection;
                bestPointToMove = movePoint.transform.position + new Vector3(1f, 0f, 0f);
                direction = MoveDirection.Right;
            }
        }

        if (CanMove(movePoint.transform.position + new Vector3(0f, -1f, 0f)))
        {
            float distanceInThisDirection = Vector3.Distance(movePoint.transform.position + new Vector3(0f, -1f, 0f), player.transform.position);
            if (furthestDistanceFromPlayer == 0 || distanceInThisDirection > furthestDistanceFromPlayer)
            {
                furthestDistanceFromPlayer = distanceInThisDirection;
                bestPointToMove = movePoint.transform.position + new Vector3(0f, -1f, 0f);
                direction = MoveDirection.Down;
            }
        }

        if (CanMove(movePoint.transform.position + new Vector3(-1f, 0f, 0f)))
        {
            float distanceInThisDirection = Vector3.Distance(movePoint.transform.position + new Vector3(-1f, 0f, 0f), player.transform.position);
            if (furthestDistanceFromPlayer == 0 || distanceInThisDirection > furthestDistanceFromPlayer)
            {
                furthestDistanceFromPlayer = distanceInThisDirection;
                bestPointToMove = movePoint.transform.position + new Vector3(-1f, 0f, 0f);
                direction = MoveDirection.Left;
            }
        }

        movePoint.transform.position = bestPointToMove;

        switch (direction) //animate the enemy
        {
            case MoveDirection.Right:
                anim.SetTrigger("right");
                spriteRenderer.flipX = false;
                break;
            case MoveDirection.Left:
                anim.SetTrigger("right");
                spriteRenderer.flipX = true;
                break;
            case MoveDirection.Up:
                anim.SetTrigger("up");
                break;
            case MoveDirection.Down:
                anim.SetTrigger("down");
                break;
            default:
                break;
        }
    }

    private bool CanMove(Vector3 point)
    {
        if (!Physics2D.OverlapCircle(point, 0.4f, walls) && !Physics2D.OverlapCircle(point, 0.4f, enemies))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void StartMovingSmartly(GameObject playerObject)
    {
        player = playerObject;
        moveSmartlyAwayFromPlayer = true;
    }

    private enum MoveDirection
    {
        Right,
        Left,
        Up,
        Down
    }
}
