using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;

public class Bot_AI : MonoBehaviour
{
    public new const string name = "BOT";
    public int point = 0;
    public const float waitTime = 10f;
    public float countTime = 10f;
    public bool isBot = false;
    
    public List<Node> nodes;
    MyGameManager gameManager;

    public void loadNode()
    {
        foreach (GameObject obj in gameManager.listNode)
        {
            Node node = obj.gameObject.GetComponent<Node>();
            if (!nodes.Contains(node) && node.name.Contains("B"))
            {
                nodes.Add(node);
            }
            else if (node.name == "A Side")
            {
                nodes.Add(node);
            }
        }
    }
    void Start()
    {
        gameManager = MyGameManager.instance;
        nodes = new List<Node>();
        loadNode();
    }

    private void Update()
    {
        if (isBot == true)
        {
            if (countTime <= 0f)
            {
                BotMove();
                this.isBot = false;
                countTime = waitTime;
            }
            countTime -= Time.deltaTime;
        }
    }


    public void BotMove()
    {
        System.Random rand = new System.Random();
        int num_1 = rand.Next(0,2);
        int num_2;
        do
        {
            num_2 = rand.Next(0,nodes.Count);
        }while(nodes[num_2]._Numchess == 0 || nodes[num_2].name == "A Side" || nodes[num_2].name == "B Side");
        Debug.Log(nodes[num_2].name);
        GameObject obj = GameObject.FindGameObjectWithTag("ChessBoard").gameObject;
        if (num_1 == 0)
        {
            for (int i = 0; i < obj.transform.childCount; i++)
            {
                if(obj.transform.GetChild(i).name == nodes[num_2].name)
                {
                    Node node = obj.transform.GetChild(i).gameObject.GetComponent<Node>();
                    gameManager.Move(node);
                    gameManager.Move(node.frontNode);
                }
            }
        }
        else if(num_1 == 1)
        {
            for (int i = 0; i < obj.transform.childCount; i++)
            {
                if (obj.transform.GetChild(i).name == nodes[num_2].name)
                {
                    Node node = obj.transform.GetChild(i).gameObject.GetComponent<Node>();
                    gameManager.Move(node);
                    gameManager.Move(node.backNode);
                }
            }
        }
        isBot = false;
    }
}
