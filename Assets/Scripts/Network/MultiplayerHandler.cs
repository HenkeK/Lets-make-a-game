using UnityEngine;
using System.Collections;

public class MultiplayerHandler : MonoBehaviour
{
	private const string typeName = "BoxFighter";
	private const string gameName = "Testing";
	public int port = 25565;

	void Start()
	{
		StartServer();
	}

	void Update()
	{

	}

	private void StartServer()
	{
		Network.InitializeServer(2, port, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}
}
