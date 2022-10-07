using UnityEngine;
using Fusion;
public enum Mynode
{
    A1 = 1,
    A2 = 2,
    A3 = 3,
    A4 = 4,
    A5 = 5,
    B1 = 6,
    B2 = 7,
    B3 = 8,
    B4 = 9,
    B5 = 10,
    A_Side = 11,
    B_Side = 12,
}

public struct NetworkInputData : INetworkInput
{
    public Mynode mynode;
}
