using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkPrefabRef _playerPrefab;
    private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();
    [SerializeField] private int countPlayer = 0;
    private NetworkRunner _runner;
    public string nodeName;

    public void OnConnectedToServer(NetworkRunner runner)
    {
        Debug.Log("Connecting to Server...");
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        throw new NotImplementedException();
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        request.Accept();
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        throw new NotImplementedException();
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        throw new NotImplementedException();
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        throw new NotImplementedException();
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        var data = new NetworkInputData();
        if (this.nodeName == "A1")
        {
            data.mynode = Mynode.A1;
        }
        else if (this.nodeName == "A2")
        {
            data.mynode = Mynode.A2;
        }
        else if (this.nodeName == "A3")
        {
            data.mynode = Mynode.A3;
        }
        else if (this.nodeName == "A4")
        {
            data.mynode = Mynode.A4;
        }
        else if (this.nodeName == "A5")
        {
            data.mynode = Mynode.A5;
        }
        else if (this.nodeName == "B1")
        {
            data.mynode = Mynode.B1;
        }
        else if (this.nodeName == "B2")
        {
            data.mynode = Mynode.B2;
        }
        else if (this.nodeName == "B3")
        {
            data.mynode = Mynode.B3;
        }
        else if (this.nodeName == "B4")
        {
            data.mynode = Mynode.B4;
        }
        else if (this.nodeName == "B5")
        {
            data.mynode = Mynode.B5;
        }
        else if (this.nodeName == "A Side")
        {
            
            data.mynode = Mynode.A_Side;
        }
        else if (this.nodeName == "B Side")
        {
            data.mynode = Mynode.B_Side;
        }
        this.nodeName = "";
        input.Set(data);
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        throw new NotImplementedException();
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsPlayer)
        {
            if (countPlayer < 1)
            {
                GameObject camera = GameObject.Find("Camera");
                camera.SetActive(false);
                NetworkObject networkPlayerObject = runner.Spawn(_playerPrefab, new Vector3(2,4,-3), Quaternion.identity, player);

                _spawnedCharacters.Add(player, networkPlayerObject);
                countPlayer++;
            }
            else if (countPlayer == 1)
            {
                NetworkObject networkPlayerObject = runner.Spawn(_playerPrefab, new Vector3(2, 4, -3), Quaternion.identity, player);
                
                _spawnedCharacters.Add(player, networkPlayerObject);
            }
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
        {
            runner.Despawn(networkObject);
            _spawnedCharacters.Remove(player);
        }
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
        //throw new NotImplementedException();
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        //throw new NotImplementedException();
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        //throw new NotImplementedException();
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        //throw new NotImplementedException();
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        throw new NotImplementedException();
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        throw new NotImplementedException();
    }

    async void StartGame(GameMode mode)
    {
        _runner = gameObject.AddComponent<NetworkRunner>();
        _runner.ProvideInput = true;
        await _runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            SessionName = "Test Room",
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>(),
        });
    }
   
    private void OnGUI()
    {
        if (_runner == null)
        {
            if (GUI.Button(new Rect(960, 500, 400, 100), "Host"))
            {
                StartGame(GameMode.Host);
            }
            if (GUI.Button(new Rect(960, 600, 400, 100), "Join"))
            {
                StartGame(GameMode.Client);
            }
        }
    }
}
