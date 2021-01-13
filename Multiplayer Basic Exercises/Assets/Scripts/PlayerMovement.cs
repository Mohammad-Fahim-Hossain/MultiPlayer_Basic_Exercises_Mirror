using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField]
    private NavMeshAgent PlayerNavAgent;

    private Camera MainCamera;

    #region Server

    [Command]
    private void CmdMove(Vector3 Position)
    {
        if(!NavMesh.SamplePosition(Position, out NavMeshHit hit, 1f, NavMesh.AllAreas))
        {
            return;
        }

        PlayerNavAgent.SetDestination(hit.position);
    }
    #endregion

    #region Client
    public override void OnStartAuthority()
    {
        MainCamera = Camera.main;
    }

    [ClientCallback]
    public void Update()
    {
        if (!hasAuthority)
        {
            return;
        }

        if (!Input.GetMouseButtonDown(1))
        {
            return;
        }

        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);

        if(!Physics.Raycast(ray,out RaycastHit hit, Mathf.Infinity))
        {
            return;
        }

        CmdMove(hit.point);
    }
    #endregion
}
