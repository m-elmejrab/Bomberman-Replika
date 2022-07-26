using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    GameObject movePoint;
    [SerializeField] float moveSpeed;
    [SerializeField] LayerMask walls;
    [SerializeField] LayerMask enemies;

    bool moveSmartlyAwayFromPlayer = false;
    GameObject player;

    //Animator anim;

    void Start()
    {
        movePoint = new GameObject(transform.name + "MovePoint");
        movePoint.transform.position = transform.position;
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
                }
                break;
            case 1: //move right
                if (CanMove(movePoint.transform.position + new Vector3(1f, 0f, 0f)))
                {
                    movePoint.transform.position += new Vector3(1f, 0f, 0f);
                }
                break;
            case 2: //move down
                if (CanMove(movePoint.transform.position + new Vector3(0f, -1f, 0f)))
                {
                    movePoint.transform.position += new Vector3(0f, -1f, 0f);
                }
                break;
            case 3: //move left
                if (CanMove(movePoint.transform.position + new Vector3(-1f, 0f, 0f)))
                {
                    movePoint.transform.position += new Vector3(-1f, 0f, 0f);
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
            }
        }

        if (CanMove(movePoint.transform.position + new Vector3(1f, 0f, 0f)))
        {
            float distanceInThisDirection = Vector3.Distance(movePoint.transform.position + new Vector3(1f, 0f, 0f), player.transform.position);
            if (furthestDistanceFromPlayer == 0 || distanceInThisDirection > furthestDistanceFromPlayer)
            {
                furthestDistanceFromPlayer = distanceInThisDirection;
                bestPointToMove = movePoint.transform.position + new Vector3(1f, 0f, 0f);
            }
        }

        if (CanMove(movePoint.transform.position + new Vector3(0f, -1f, 0f)))
        {
            float distanceInThisDirection = Vector3.Distance(movePoint.transform.position + new Vector3(0f, -1f, 0f), player.transform.position);
            if (furthestDistanceFromPlayer == 0 || distanceInThisDirection > furthestDistanceFromPlayer)
            {
                furthestDistanceFromPlayer = distanceInThisDirection;
                bestPointToMove = movePoint.transform.position + new Vector3(0f, -1f, 0f);
            }
        }

        if (CanMove(movePoint.transform.position + new Vector3(-1f, 0f, 0f)))
        {
            float distanceInThisDirection = Vector3.Distance(movePoint.transform.position + new Vector3(-1f, 0f, 0f), player.transform.position);
            if (furthestDistanceFromPlayer == 0 || distanceInThisDirection > furthestDistanceFromPlayer)
            {
                furthestDistanceFromPlayer = distanceInThisDirection;
                bestPointToMove = movePoint.transform.position + new Vector3(-1f, 0f, 0f);
            }
        }

        movePoint.transform.position = bestPointToMove;
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
}
