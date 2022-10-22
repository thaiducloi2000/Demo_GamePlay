using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOffline : MonoBehaviour
{
    public string name="";
    public int point = 0;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
}
