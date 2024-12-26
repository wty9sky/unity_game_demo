using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Destructible : MonoBehaviour
{
    public GameObject destroyVFX;

    public PickupSpawner pickupSpawner; // 掉落道具

    private void Awake() {
        pickupSpawner = GetComponent<PickupSpawner>();
    }

    public void DestroyObject()
    {
        if (destroyVFX != null)
        {
            // GameObject vfx = 
            Instantiate(destroyVFX, transform.position, transform.rotation);
            // Destroy(vfx, 1f);
        }

        pickupSpawner.DropItems();
        
        Destroy(gameObject);
    }
}
