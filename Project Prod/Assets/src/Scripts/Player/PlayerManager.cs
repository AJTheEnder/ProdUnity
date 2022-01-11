using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Variables
    public static PlayerManager instance { get; private set; }


    private bool isInSafeZone = false;

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);    // Suppression d'une instance précédente (sécurité...sécurité...)

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public bool getisSafe()
    {
        return isInSafeZone;
    }
    public void setIsSafe(bool var)
    {
        isInSafeZone = var;
        Debug.Log("isInSafeZone " + isInSafeZone); 
    }
}
