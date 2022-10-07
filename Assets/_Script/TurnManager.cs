using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;
    public bool swapTurn = false;
    public PlayerOffline player;


    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("There are more  than 1 instance");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        GameObject tmp_player = GameObject.Find("Player");
        if (tmp_player != null)
        {
            player = tmp_player.GetComponent<PlayerOffline>();
        }
    }

    public void Move(Node startNode,Node endNode)
    {
        //this.getPoint = false;
        int unit = startNode._Numchess;
        startNode._Numchess = 0;
        if (startNode.frontNode.name == endNode.name)
        {
            MovingFront(endNode, unit);
        }
        else if (startNode.backNode.name == endNode.name)
        {
            MovingBack(endNode, unit);
        }
        startNode.Reset();
        endNode.Reset();
    }

    public void MovingFront(Node frontNode,int numOfUnit)
    {
        if (numOfUnit == 0 && checkFinishMove(frontNode,0))
        {
            return ;
        }
        else if (numOfUnit == 0)
        {
            int unit = frontNode._Numchess;
            frontNode._Numchess = 0;
            MovingFront(frontNode.frontNode, unit); // Release when it have eating point
            //MovingFront(frontNode, frontNode._Numchess);
        }
        else if(numOfUnit >= 1)
        {
            frontNode._Numchess ++;
            MovingFront(frontNode.frontNode, numOfUnit - 1);
        }
        //Debug.Log(frontNode.name + " : " + frontNode._Numchess);
    }
    public void MovingBack(Node backNode, int numOfUnit)
    {
        if (numOfUnit == 0 && checkFinishMove(backNode,1))
        {
            return;
        }
        else if(numOfUnit == 0)
        {
            int unit = backNode._Numchess;
            backNode._Numchess = 0;
            MovingBack(backNode.backNode, unit); // Release when it have eating point
            //MovingBack(backNode, backNode._Numchess);
        }
        else if(numOfUnit >= 1)
        {
            backNode._Numchess ++;// bug here
            MovingBack(backNode.backNode, numOfUnit - 1);
        }
        //Debug.Log(backNode.name + " : " + backNode._Numchess);
    }
   
    public bool checkFinishMove(Node lastMoveNode,int status)
    {
        bool result = false;
        if (lastMoveNode.name == "A Side")
        {
            result = true;
        }
        else if (lastMoveNode.name == "B Side")
        {
            result = true;
        }
        else if (status == 1)
        {
            if (lastMoveNode._Numchess == 0 && lastMoveNode.backNode._Numchess == 0)
            {
                result = true;
            }
            else if(lastMoveNode._Numchess == 0 && (lastMoveNode.backNode.name == "B Side" || lastMoveNode.backNode.name == "A Side"))
            {
                //getPoint = true;
                player.point += lastMoveNode.backNode._Numchess;
                lastMoveNode.backNode._Numchess = 0;
                result = true;
            }
            else if(lastMoveNode._Numchess == 0 && lastMoveNode.backNode._Numchess != 0)
            {
                player.point += lastMoveNode.backNode._Numchess;
                lastMoveNode.backNode._Numchess = 0;
                result = true;
            }
        }
        else if(status == 0)
        {

            if (lastMoveNode._Numchess == 0 && lastMoveNode.frontNode._Numchess == 0)
            {
                result = true;
            }
            else if (lastMoveNode._Numchess == 0 && (lastMoveNode.frontNode.name == "B Side" || lastMoveNode.frontNode.name == "A Side"))
            {
                //getPoint = true;
                player.point += lastMoveNode.frontNode._Numchess;
                lastMoveNode.frontNode._Numchess = 0;
                result = true;
            }else if(lastMoveNode._Numchess == 0 && lastMoveNode.frontNode._Numchess != 0)
            {
                player.point += lastMoveNode.frontNode._Numchess;
                lastMoveNode.frontNode._Numchess = 0;
                result = true;
            }
        }
        return result;
    }
}
