using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Liste des points d'ancrage pour la routine")]
    private GameObject[] anchorPointsTab;

    [SerializeField]
    [Tooltip("Prochaine destination")]
    private GameObject Destination;

    [SerializeField]
    [Tooltip("Le vigil est-il alarmé ?")]
    private bool vigilIsAlarmed;

    [SerializeField]
    [Tooltip("La cible du vigil")]
    private GameObject player;

    [SerializeField]
    [Tooltip("Le joueur est-il détecté par un piège")]
    private bool playerIsTrapped;

    [SerializeField]
    [Tooltip("Le joueur est-il visible")]
    private bool playerIsVisible;

    //Index du point d'ancrage actuel dans la liste de la routine
    private int index;

    //private int randomAnchorPointChoice;

    //Le navMesh pour la navigation de l'IA
    private NavMeshAgent vigilAgent;

    // Start is called before the first frame update
    void Start()
    {
        //Initialisation des bools relatifs à l'état du joueur et de l'IA
        vigilIsAlarmed = false;
        playerIsTrapped = false;
        playerIsVisible = false;

        //Recherche de la cible
        player = GameObject.FindGameObjectWithTag("Player");

        //Recherche de tous les points d'ancrages présents sur la map
        anchorPointsTab = GameObject.FindGameObjectsWithTag("AnchorPoint");
        
        //randomAnchorPointChoice = Random.Range(0, anchorPointsTab.Length);
        //Debug.Log(randomAnchorPointChoice);
        
        //Récupération du composant navMesh du vigil
        vigilAgent = this.GetComponent<NavMeshAgent>();
        
        //Initialisation de l'index à 0
        index = 0;
        Debug.Log(index);

        //Mise en place de la première destination du vigil
        Destination = anchorPointsTab[index];
        vigilAgent.SetDestination(Destination.transform.position);
        
        //Passage au point d'ancrage suivant
        ++index;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Si la position du vigil et celle du ppoint de destination correspondent en x et z
        if (this.transform.position.x == Destination.transform.position.x
            && this.transform.position.z == Destination.transform.position.z)
        {
            //randomAnchorPointChoice = Random.Range(0, anchorPointsTab.Length);
            //Debug.Log(randomAnchorPointChoice);
            
            //Si il reste encore des points d'ancrage dans la liste
            if (index < anchorPointsTab.Length)
            {
                //Destination suivante
                Destination = anchorPointsTab[index];
                vigilAgent.SetDestination(Destination.transform.position);

                //Si le joueur est vu ou détecté par un piège
                if (vigilIsAlarmed == true)
                {
                    Debug.Log("Entrée dans le if alarmed");
                    //Si le joueur est détecté par un piège
                    if (playerIsTrapped == true)
                    {
                        Debug.Log("Entrée dans le if tarpped");
                        //Le vigil se déplace à l'endroit où le joueur à été vu
                        Transform playerPositionWhenTrapped = player.transform;
                        vigilAgent.SetDestination(playerPositionWhenTrapped.position);
                        
                        /*Tant que le vigil ne se trouve pas à la position en x et z de quand le joueur à
                        été détecté ou que le joueur n'est pas vue par le vigil*/ 
                        while ((this.transform.position.x != playerPositionWhenTrapped.position.x
                            && this.transform.position.z != playerPositionWhenTrapped.position.z)
                            || playerIsVisible == false)
                        {
                            //Déplacement sans aucune autre action
                            ;
                        }
                    }
                    //Point particulier : "si le joueur est vu par le vigil"
                    if_player_is_visible:
                    //Mise en mémoire de la position du joueur
                    Transform playerPositionWhenSeen = player.transform;
                    //Tant que le joueur est visible
                    while (playerIsVisible == true)
                    {
                        //Le vigil poursuit le joueur
                        playerPositionWhenSeen = player.transform;
                        vigilAgent.SetDestination(playerPositionWhenSeen.position);
                    }
                    /*Quand le joueur n'est plus visible le vigil se dirige vers la dernière position du
                    joueur avant qu'il ne sorte de sa zone de vision. Tant qu'il ne se situe pas à cette
                    position*/
                    while (this.transform.position.x != playerPositionWhenSeen.position.x
                            && this.transform.position.z != playerPositionWhenSeen.position.z)
                    {
                        //Si entre temps le joueur rerentre dans sa zone de vision
                        if (playerIsVisible == true)
                        {
                            //retour au point particulier "si le joueur est vu par le vigil"
                            goto if_player_is_visible;
                        }
                    }
                }
                /*A la fin de tout ce protocole le vigil retourne à la destination où il devait se 
                rendre avant qu'il ne soit alerté*/
                vigilAgent.SetDestination(Destination.transform.position);

                //Passage au point d'ancrage suivant
                ++index;
                Debug.Log(index);
            }
            //Si il ne reste plus de point d'angrage dans la liste
            else
            {
                //Retour au début de la liste
                index = 0;
                Debug.Log(index);
            }
        }
    }

    //Fonction permettant de mettre à jour le status du vigil qui définit donc son comportement
    public void setVigilStatus(bool isAlarmed, bool isTrapped, bool isVisible)
    {
        //Le vigil est-il alarmé ?
        vigilIsAlarmed = isAlarmed;
        Debug.Log(isAlarmed);
        //Le joueur est-il détecté par un piège ?
        playerIsTrapped = isTrapped;
        Debug.Log(isTrapped);
        //Le joueur est-il visible par le vigil ?
        playerIsVisible = isVisible;
        Debug.Log(isVisible);
    }
}
