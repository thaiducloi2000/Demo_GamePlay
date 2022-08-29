using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    public GameObject normalUnitPrefab;
    public GameObject specialUnitPrefab;
    public List<Transform> normalUnit;
    public List<Transform> SpecialUnit;
    public List<GameObject> listNode;
    public static MyGameManager instance;
    private Node startNode;
    private Node endNode;
    TurnManager turnManager;
    public Canvas UI;


    public Node StartNode { get => startNode; set => startNode = value; }
    public Node EndNode { get => endNode; set => endNode = value; }

    private void Awake()
    {
        Application.targetFrameRate = 60;
        if (instance != null)
        {
            Debug.LogError("THere are more  than 1 instance");
            return;
        }
        instance = this;
    }
    void Start()
    {
        turnManager = TurnManager.instance;
        this.listNode = getListNode();
        foreach (GameObject obj in this.listNode)
        {
            Node node = obj.GetComponent<Node>();
            if (obj.name != "A Side" && obj.name != "B Side")
            {
                node._startNumchess = 5;
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
