using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    public GameObject normalUnitPrefab;
    public GameObject specialUnitPrefab;
    public List<Transform> normalUnit;
    public List<Transform> SpecialUnit;
    public List<GameObject> listNode;
    public static MyGameManager instance;
    public GameObject A_Side;
    public GameObject B_Side;
    private Node startNode;
    private Node endNode;
    public Bot_AI bot;
    TurnManager turnManager;
    UIEndGameController ui_endgame;
    //public Time time = 15 ;


    public Node StartNode { get => startNode; set => startNode = value; }
    public Node EndNode { get => endNode; set => endNode = value; }

    private void Awake()
    {
        Application.targetFrameRate = 60;
        if (instance != null)
        {
            Debug.LogError("There are more  than 1 instance");
            return;
        }
        this.listNode = getListNode();
        instance = this;
    }
    private void FixedUpdate()
    {
        EndGame();
        ui_endgame.setPoint(turnManager.player.point.ToString(), turnManager.player.name.ToString(),turnManager.bot.point.ToString());
    }
    void Start()
    {
        turnManager = TurnManager.instance;
        ui_endgame = UIEndGameController.instance;
        
        foreach (GameObject obj in this.listNode)
        {
            Node node = obj.GetComponent<Node>();
            if (obj.name != "A Side" && obj.name != "B Side")
            {
                node._startNumchess = 5 ;
            }
            else
            {
                node._startNumchess = 1;
            }
        }
    }

    public void Move(Node node)
    {
        if (StartNode == null && (node.name != "A Side" || node.name != "B Side") && node._Numchess != 0)
        {
            StartNode = node;
            HightLight(node);
            return;
        }else if(EndNode == null)
        {
            EndNode = node;
        }
        if (StartNode != null && EndNode != null)
        {
            turnManager.Move(StartNode,EndNode);
            StartNode.backNode.Reset();
            StartNode.frontNode.Reset();
            StartNode = null;
            EndNode = null;
        }
    }


    public void HightLight(Node node)
    {
        Renderer nodeColorFront = node.frontNode.GetComponent<Renderer>();
        Renderer nodeColorBack = node.backNode.GetComponent<Renderer>();
        nodeColorFront.material.color = Color.blue;
        nodeColorBack.material.color = Color.blue;
    }

    public bool EndGame()
    {
        Node a_side = this.A_Side.GetComponent<Node>();
        Node b_side = this.B_Side.GetComponent<Node>();
        if (a_side._CurrentNumChess == 0 && b_side._CurrentNumChess == 0)
        {
            ui_endgame.title.gameObject.SetActive(true);
            return true;
        }
        return false;
    }

    private List<GameObject> getListNode()
    {
        GameObject _chessboard=GameObject.FindGameObjectWithTag("ChessBoard").gameObject;
        List<GameObject> listNode = new List<GameObject>();

        for(int i = 0; i<_chessboard.transform.childCount; i++)
        {
            if(_chessboard.transform.GetChild(i).gameObject.name != "Colider")
            listNode.Add(_chessboard.transform.GetChild(i).gameObject);
        }
        return listNode;
    }

    public bool isPlayerMove()
    {
        return !turnManager.swapTurn;
    }
}
