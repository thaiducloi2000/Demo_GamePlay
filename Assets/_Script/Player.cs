using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Player : NetworkBehaviour
{
    public string name;
    public int point = 0;
    public MyGameManager gameManager;
    public bool pause = true;
    public int count = 0;


    public string Name { get => name; set => name = value; }
    public int Point { get => point; set => point = value; }

    
    private void Awake()
    {
        gameManager = MyGameManager.instance;
    }

    public override void FixedUpdateNetwork()
    {
        GameObject node;
        if (GetInput(out NetworkInputData data))
        {
            //if ((data.buttons & NetworkInputData.MOUSEBUTTON1) != 0)
            //{
            //    pause = !pause;
            //}
            //if (pause) { 
                if (data.mynode == Mynode.A1)
                {
                    node = GameObject.Find("A1");
                    Node select = node.GetComponent<Node>();
                    gameManager.Move(select);
                    //pause = !pause;
                    count++;
                    Debug.Log(select.name);
                }
                else if (data.mynode == Mynode.A2)
                {
                    node = GameObject.Find("A2");
                    Node select = node.GetComponent<Node>();
                    gameManager.Move(select);
                    //pause = !pause;
                    count++;
                    Debug.Log(select.name);
                }
                else if (data.mynode == Mynode.A3)
                {
                    node = GameObject.Find("A3");
                    Node select = node.GetComponent<Node>();
                    gameManager.Move(select);
                    //pause = !pause;
                    count++;
                    Debug.Log(select.name);
                }
                else if (data.mynode == Mynode.A4)
                {
                    node = GameObject.Find("A4");
                    Node select = node.GetComponent<Node>();
                    gameManager.Move(select);
                    //pause = !pause;
                    count++;
                    Debug.Log(select.name);
                }
                else if (data.mynode == Mynode.A5)
                {
                    node = GameObject.Find("A5");
                    Node select = node.GetComponent<Node>();
                    gameManager.Move(select);
                    //pause = !pause;
                    count++;
                    Debug.Log(select.name);
                }
                else if (data.mynode == Mynode.B1)
                {
                    node = GameObject.Find("B1");
                    Node select = node.GetComponent<Node>();
                    gameManager.Move(select);
                    //pause = !pause;
                    count++;
                    Debug.Log(select.name);
                }
                else if (data.mynode == Mynode.B2)
                {
                    node = GameObject.Find("B2");
                    Node select = node.GetComponent<Node>();
                    gameManager.Move(select);
                    //pause = !pause;
                    count++;
                    Debug.Log(select.name);
                }
                else if (data.mynode == Mynode.B3)
                {
                    node = GameObject.Find("B3");
                    Node select = node.GetComponent<Node>();
                    gameManager.Move(select);
                    //pause = !pause;
                    count++;
                    Debug.Log(select.name);
                }
                else if (data.mynode == Mynode.B4)
                {
                    node = GameObject.Find("B4");
                    Node select = node.GetComponent<Node>();
                    gameManager.Move(select);
                    //pause = !pause;
                    count++;
                    Debug.Log(select.name);
                }
                else if (data.mynode == Mynode.B5)
                {
                    node = GameObject.Find("B5");
                    Node select = node.GetComponent<Node>();
                    gameManager.Move(select);
                    //pause = !pause;
                    count++;
                    Debug.Log(select.name);
                }
                else if (data.mynode == Mynode.A_Side)
                {
                    node = GameObject.Find("A Side");
                    Node select = node.GetComponent<Node>();
                    gameManager.Move(select);
                    //pause = !pause;
                    count++;
                    Debug.Log(select.name);
                }
                else if (data.mynode == Mynode.B_Side)
                {
                    node = GameObject.Find("B Side");
                    Node select = node.GetComponent<Node>();
                    gameManager.Move(select);
                    //pause = !pause;
                    count++;
                    Debug.Log(select.name);
                }
            //}
        }
    }
}
