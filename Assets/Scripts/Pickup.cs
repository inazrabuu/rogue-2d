using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    Coin,
    Health
}

public class Pickup : MonoBehaviour
{
    public PickupType type;
    public int value = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (type == PickupType.Coin)
            {
                other.GetComponent<Player>().AddCoin(value);
                Destroy(gameObject);
            } else if (type == PickupType.Health)
            {
                if (other.GetComponent<Player>().AddHealth(value))
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
