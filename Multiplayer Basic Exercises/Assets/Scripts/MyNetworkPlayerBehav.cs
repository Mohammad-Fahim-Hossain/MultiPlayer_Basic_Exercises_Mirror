using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class MyNetworkPlayerBehav : NetworkBehaviour
{
    [SerializeField]
    private TMP_Text PlayerTextUI=null;

    [SerializeField]
    private Renderer PlayerRender = null;

    [SyncVar(hook =nameof(ShowPlayerName))]
    [SerializeField]
    private string PlayerName = "Missing";

    [SyncVar(hook =nameof(ShowPlayerNewCOlor))]
    [SerializeField]
    private Color PlayerColor = Color.black;

    #region Server

    [Server]
    public void SetPlayerName(string PName)
    {
        PlayerName = PName;
    }

    [Server]
    public void SetRandomPlayerColor(Color NewColor)
    {
        PlayerColor = NewColor;
        
    }

    [Command]
    public void CmdSetPlayerName(string NewName)
    {

        if (NewName.Length < 3 || NewName.Length > 20)
        return;
        

        RpcLogPlayerName(NewName);
        SetPlayerName(NewName);
    }

    #endregion

    #region Client

    public void ShowPlayerName(string OldName, string NewName)
    {
        PlayerTextUI.text = NewName;
    }

    public void ShowPlayerNewCOlor(Color OldColor, Color NewColor)
    {
        PlayerRender.material.SetColor("_BaseColor", NewColor);
    }

    [ContextMenu("SetMyNewName")]
    private void SetMyNewName()
    {
        CmdSetPlayerName("A");
    }

    [ClientRpc]
    private void RpcLogPlayerName(string Name)
    {
        Debug.Log(Name);
    }


    #endregion


}
