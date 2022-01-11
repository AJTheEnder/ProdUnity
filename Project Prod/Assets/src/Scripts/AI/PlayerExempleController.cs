using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExempleController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("L'IA qui cherche le joueur")]
    private GameObject vigil;

    public Transform anchor;

    // Start is called before the first frame update
    void Start()
    {
        vigil = GameObject.FindGameObjectWithTag("Vigil");
    }

    // Update is called once per frame
    void Update()
    {
        if (vigil.transform.position.x == anchor.position.x
            && vigil.transform.position.z == anchor.position.z)
        {
            //vigil.GetComponent<AIController>().setVigilStatus(true, true, false);
        }
    }
}
