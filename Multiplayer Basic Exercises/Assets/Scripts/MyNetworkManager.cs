using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MyNetworkManager : NetworkManager
{

    

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);

        MyNetworkPlayerBehav Player = conn.identity.GetComponent<MyNetworkPlayerBehav>();

        Player.SetRandomPlayerColor(new Color(Random.Range(0f, 1f), 
                                              Random.Range(0f, 1f), 
                                              Random.Range(0f, 1f)));

        Player.SetPlayerName($"Player {numPlayers}");

        
    }

}
