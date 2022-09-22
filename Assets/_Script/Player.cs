using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
    public string name;
    public int point = 0;


    public string Name { get => name; set => name = value; }
    public int Point { get => point; set => point = value; }
}
