using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] SpriteRenderer doorSprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (door != null && doorSprite != null)
        {
            door.GetComponent<SpriteRenderer>().sprite = doorSprite.sprite;
            door.GetComponent<BoxCollider2D>().enabled = false;
            this.gameObject.SetActive(false);
        }
    }
}
