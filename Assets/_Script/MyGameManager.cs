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
    //public Canvas UI;
    public Player player;


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
    void Start()
    {
        turnManager = TurnManager.instance;
        if(player == null)
        {
            player = new Player();
        }
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
        //foreach (GameObject obj in this.listNode)
        //{
        //    if (obj.name == "A Side" || obj.name == "B Side")
        //    {
        //        Instantiate(specialUnitPrefab, obj.transform.GetChild(0).position, Quaternion.identity);
        //    }
        //    if (obj.name != "A Side" && obj.name != "B Side")
        //    {
        //        SpawnChess(obj.transform.GetChild(0),obj.GetComponent<Node>()._startNumchess,normalUnitPrefab);
        //    }
        //}       
    }

    public void Move(Node node)
    {
        if (StartNode == null && (node.name != "A Side" || node.name != "B Side"))
        {
            StartNode = node;
            return;
        }if(EndNode == null)
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

    //private void SpawnChess(Transform obj,int v,GameObject unit)
    //{
    //    List<Vector3> listPosition = new List<Vector3>();
    //    int i = 0;
    //    do
    //    {
    //        Vector3 position = new Vector3(Random.Range(-0.3F, 0.3F), 0, Random.Range(-0.3F, 0.3F));
    //        if (!listPosition.Contains(position))
    //        {
    //            Instantiate(unit, obj.position + position, Quaternion.identity);
    //            listPosition.Add(position);
    //            i++;
    //        }
    //    } while (i != v);
    //}

    public void addPoint()
    {
        this.player.Point += turnManager.point;
        turnManager.getPoint = false;
        turnManager.point = 0;
        Debug.Log(this.player.name + " - " + this.player.Point);
    }

    private void Update()
    {
        if (turnManager.getPoint == true)
        {
            addPoint();
        }
    }


    bool GameOver()
    {
        Node a_side = this.A_Side.GetComponent<Node>();
        Node b_side = this.B_Side.GetComponent<Node>();
        return a_side._Numchess == 0 && b_side._Numchess == 0;
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
