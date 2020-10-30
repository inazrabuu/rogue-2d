using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public int curHp, maxHp, coins;
    public bool hasKey;
    public SpriteRenderer spriteRenderer;

    public LayerMask moveLayerMask;

    public void Move(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, moveLayerMask);
        if (hit.collider == null)
        {
            transform.position += new Vector3(direction.x, direction.y, 0);
        }
    }

    public void OnMoveUp(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Move(Vector2.up);
        }
    }

    public void OnMoveDown(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Move(Vector2.down);
        }
    }

    public void OnMoveLeft(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Move(Vector2.left);
        }
    }

    public void OnMoveRight(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Move(Vector2.right);
        }
    }

    public void OnAttackUp(InputAction.CallbackContext context)
    {

    }

    public void OnAttackDown(InputAction.CallbackContext context)
    {

    }

    public void OnAttackLeft(InputAction.CallbackContext context)
    {

    }

    public void OnAttackRight(InputAction.CallbackContext context)
    {

    }
}
