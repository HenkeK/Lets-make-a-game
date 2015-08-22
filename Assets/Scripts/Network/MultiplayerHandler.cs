using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class MultiplayerHandler : NetworkManager
{
	public Dictionary<int, string> sideInfo = new Dictionary<int, string>();
	

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	{
		GameObject player;

		player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
		player.name = "Player" + (NetworkServer.connections.Count + 1);
		player.GetComponent<Player>().scoreBoard = GameObject.Find("Score_" + sideInfo[conn.connectionId]);

		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

		//Debug.Log("!ServAddPl : " + NetworkServer.connections[NetworkServer.connections.Count + 1]);
	}

	public override void OnServerConnect(NetworkConnection conn)
	{
		sideInfo.Add(conn.connectionId, "Left");
		ClientScene.AddPlayer(conn, 0);

		// Default
		//base.OnServerConnect(conn);
	}

	public override void OnClientConnect(NetworkConnection conn)
	{
		sideInfo.Add(conn.connectionId, "Right");
		ClientScene.AddPlayer(conn, 0);

		// Default
		//base.OnClientConnect(conn);
	}

	public override void OnClientDisconnect(NetworkConnection conn)
	{
		sideInfo.Remove(conn.connectionId);
		base.OnClientDisconnect(conn);
	}
}
