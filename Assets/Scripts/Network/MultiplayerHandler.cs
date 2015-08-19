using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class MultiplayerHandler : NetworkManager
{
	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	{
		GameObject player;

		player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
		player.name = "Player" + (NetworkServer.connections.Count + 1);

		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

		//Debug.Log("!ServAddPl : " + NetworkServer.connections[NetworkServer.connections.Count + 1]);
	}

	public override void OnServerConnect(NetworkConnection conn)
	{
		Debug.Log("!ServerConn : " + conn.connectionId);
		ClientScene.AddPlayer(conn, 0);

		// Default
		//base.OnServerConnect(conn);
	}

	public override void OnClientConnect(NetworkConnection conn)
	{
		Debug.Log("!ClientConn : " + conn.connectionId);
		ClientScene.AddPlayer(conn, 0);

		// Default
		//base.OnClientConnect(conn);
	}
}
