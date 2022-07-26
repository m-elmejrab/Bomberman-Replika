using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    GameObject movePoint;
    [SerializeField] float moveSpeed;
    [SerializeField] LayerMask walls;
    [SerializeField] LayerMask enemies;

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
}
