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
    TurnManager turnManager;
    //UIEndGameController ui_endgame;
    //public Canvas UI;
    //public Player player;


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
        instance = this;
    }
    private void FixedUpdate()
    {
        EndGame();
        //ui_endgame.setPoint(turnManager.player.point.ToString(), turnManager.player.name.ToString());
    }
    void Start()
    {
        turnManager = TurnManager.instance;
        //ui_endgame = UIEndGameController.instance;
        //if (player == null)
        //{
        //    player = new Player();
        //}
        this.listNode = getListNode();
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
            return;
        }else if(EndNode == null)
        {
            EndNode = node;
        }
        if (StartNode != null && EndNode != null)
        {
            turnManager.Move(StartNode,EndNode);
            StartNode = null;
            EndNode = null;
        }
    }


    void EndGame()
    {
        Node a_side = this.A_Side.GetComponent<Node>();
        Node b_side = this.B_Side.GetComponent<Node>();
        if (a_side._CurrentNumChess == 0 && b_side._CurrentNumChess == 0)
        {
            //ui_endgame.title.gameObject.SetActive(true);
            Debug.Log("Game Over");
        }
    }

    private List<GameObject> getListNode()
    {
        GameObject _chessboard=GameObject.FindGameObjectWithTag("ChessBoard").gameObject;
        List<GameObject> listNode = new List<GameObject>();

        for(int i = 0; i<_chessboard.transform.childCount; i++)
        {
            listNode.Add(_chessboard.transform.GetChild(i).gameObject);
        }
        return listNode;
    }

}
