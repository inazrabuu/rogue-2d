using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
            EnemyManager.Instance.OnPlayerMove();
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
        if (context.phase == InputActionPhase.Performed)
        {
            TryAttack(Vector2.up);
        }
    }

    public void OnAttackDown(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            TryAttack(Vector2.down);
        }
    }

    public void OnAttackLeft(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            TryAttack(Vector2.left);
        }
    }

    public void OnAttackRight(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            TryAttack(Vector2.right);
        }
    }

    void TryAttack(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, 1 << 9);
        if (hit.collider != null)
        {
            hit.transform.GetComponent<Enemy>().TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        curHp -= damage;

        if (curHp <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        StartCoroutine(DamageFlash());
    }

    IEnumerator DamageFlash()
    {
        Color color = spriteRenderer.color;
        spriteRenderer.color = Color.white;

        yield return new WaitForSeconds(.005f);
        spriteRenderer.color = color;
    }

    public void AddCoin(int amount)
    {
        coins += amount;
    }

    public bool AddHealth(int amount)
    {
        if (curHp + amount <= maxHp)
        {
            curHp += amount;
            return true;
        }

        return false;
    }
}
