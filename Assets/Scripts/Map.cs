using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static Map Instance=null;
    public GameObject[] RedTeamRespawn; //Note to self Redo into lists and make it auto get the respawnpoints
    public GameObject[] BlueTeamRespawn;

    public void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
    }
    public void Respawn(GameObject player)
    {
        Player playerscript = player.GetComponent<Player>();
        playerscript.curHealth = 100;
        /*if (playerscript.team == 0)
        {
            player.transform.position = RedTeamRespawn[Random.Range(0, RedTeamRespawn.Length)].transform.position;
        }
        else
        {
            player.transform.position = BlueTeamRespawn[Random.Range(0, BlueTeamRespawn.Length)].transform.position;
        }
        */
    }
}
