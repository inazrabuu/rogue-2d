﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player player;

    public int health;
    public int damage;

    public GameObject deathDropPrefab;
    public SpriteRenderer spriteRenderer;

    public LayerMask moveLayerMask;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void Move()
    {
        if (Random.value < 0.5f)
        {
            return;
        }

        Vector3 direction = Vector3.zero;
        bool canMove = false;

        while (canMove == false)
        {
            direction = GetRandomDirection();

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, moveLayerMask);

            if (hit.collider == null)
            {
                canMove = true;
            }
        }

        transform.position += direction;
    }

    Vector3 GetRandomDirection()
    {
        int random = Random.Range(0, 4);

        if (random == 0)
        {
            return Vector3.up;
        } else if (random == 1)
        {
            return Vector3.down;
        } else if (random == 2)
        {
            return Vector3.left;
        } else if (random == 3)
        {
            return Vector3.right;
        }

        return Vector3.zero;
    }
}
