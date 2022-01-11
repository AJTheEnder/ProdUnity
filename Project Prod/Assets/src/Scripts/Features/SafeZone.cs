using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Creator: Kevin
 * Add this script in a gameObject with a boxcollider with isTrigger 
 */

public class SafeZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider pl)
    {
        if (pl.gameObject.CompareTag("Player")) // Tag of the player
        {
            PlayerManager.instance.setIsSafe(true); // Bool in PlayerManager script 
        }
    }

    private void OnTriggerExit(Collider pl)
    {
        if (pl.gameObject.CompareTag("Player")) // Tag of the player
        {
            PlayerManager.instance.setIsSafe(false);  // Bool in PlayerManager script 
        }
    }
}
