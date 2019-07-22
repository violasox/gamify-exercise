using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Tilemaps;

public class CharController : MonoBehaviour
{
    public int worldSize = 5;
    public Tilemap worldMap;
    TilemapController worldMapController;
    Rigidbody2D body;
    float xUnit = 0.5f;
    float yUnit = 0.289f;
    Vector2Int curTile = new Vector2Int(0, 0);
    bool isMoving = false;
    float moveCountdown;
    float moveLockout = 0.5f;
    int xMove = 0;
    int yMove = 0;
    int neMove = 0;
    int seMove = 0;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        worldMapController = worldMap.GetComponent<TilemapController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            //float upRight = Input.GetAxis("Vertical");
            //float upLeft = Input.GetAxis("Horizontal");
            Vector2 position = body.position;
            if (xMove != 0 && yMove != 0)
            {
                position.x += (xUnit * xMove);
                position.y += (yUnit * yMove);
                Vector2Int newTile = curTile + new Vector2Int(neMove, seMove);
                Debug.Log(newTile.x + ", " + newTile.y);
                if (newTile.x >= 0 && newTile.x < worldSize)
                {
                    if (newTile.y >= 0 && newTile.y < worldSize)
                    {
                        body.MovePosition(position);
                        moveCountdown = moveLockout;
                        isMoving = true;
                        curTile = newTile;
                        worldMapController.RevealTile(newTile);
                    }
                }
                xMove = 0;
                yMove = 0;
                neMove = 0;
                seMove = 0;
            }
            //else if (Mathf.Abs(upLeft) > 0.1)
            //{
            //    position.x += (xUnit * Math.Sign(upLeft));
            //    position.y += (yUnit * Math.Sign(upLeft) * -1);
            //    body.MovePosition(position);
            //    moveCountdown = moveLockout;
            //    isMoving = true;
            //}
        }
        else
        {
            moveCountdown -= Time.deltaTime;
            if (moveCountdown <= 0.0f) { isMoving = false; }
        }

    }

    public void Move(int direction)
    {
        if (!isMoving)
        {
            switch (direction)
            {
                case 1:
                    xMove = 1;
                    yMove = 1;
                    neMove = 1;
                    break;
                case 2:
                    xMove = 1;
                    yMove = -1;
                    seMove = 1;
                    break;
                case 3:
                    xMove = -1;
                    yMove = -1;
                    neMove = -1;
                    break;
                case 4:
                    xMove = -1;
                    yMove = 1;
                    seMove = -1;
                    break;
                default:
                    xMove = 0;
                    yMove = 0;
                    neMove = 0;
                    seMove = 0;
                    break;
            }
        }
    }

}
