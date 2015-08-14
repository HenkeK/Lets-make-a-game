using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class MultiplayerHandler : NetworkManager
{
	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	{
		Debug.Log("!ServAddPl");

		base.OnServerAddPlayer(conn, playerControllerId);

		//GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

		//if (numPlayers == 2)
		//{
		//	player.GetComponent<SpriteRenderer>().color = HexToColor("546EFFFF");
		//}

		//NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
	}

	public override void OnServerConnect(NetworkConnection conn)
	{
		Debug.Log("!ServerConn : " + conn.connectionId);
		base.OnServerConnect(conn);
	}

	public override void OnClientConnect(NetworkConnection conn)
	{
		Debug.Log("!ClientConn : " + conn.connectionId);
		base.OnClientConnect(conn);
	}

	Color HexToColor(string hex)
	{
		byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
		return new Color32(r, g, b, 255);
	}
}
