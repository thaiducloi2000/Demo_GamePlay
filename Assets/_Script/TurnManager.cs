using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;
    public bool getPoint = false;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("There are more  than 1 instance");
            return;
        }
        instance = this;
    }


    public void Move(Node startNode,Node endNode)
    {
        if (startNode.frontNode.name == endNode.name)
        {
            int unit = startNode._Numchess;
            startNode._Numchess = 0;
            MovingFront(endNode, unit);
            reset(startNode,endNode);
        }
        else if (startNode.backNode.name == endNode.name)
        {
            int unit = startNode._Numchess;
            startNode._Numchess = 0;
            MovingBack(endNode, unit);
            reset(startNode, endNode);
        }
    }

    public void MovingFront(Node frontNode,int numOfUnit)
    {
        if (numOfUnit == 0 && checkFinishMove(frontNode,0))
        {
            return;
        }
        if (numOfUnit == 0)
        {
            int unit = frontNode._Numchess;
            frontNode._Numchess = 0;
            MovingFront(frontNode.frontNode, unit); // Release when it have eating point
            //MovingFront(frontNode, frontNode._Numchess);
        }
        if(numOfUnit > 0)
        {
            frontNode._Numchess += 1;
            MovingFront(frontNode.frontNode, numOfUnit - 1);
        }
              
    }
    public void MovingBack(Node backNode, int numOfUnit)
    {
        if(numOfUnit == 0 && checkFinishMove(backNode,1))
        {
            return;
        }if(numOfUnit == 0)
        {
            int unit = backNode._Numchess;
            backNode._Numchess = 0;
            MovingBack(backNode.backNode, unit); // Release when it have eating point
            //MovingBack(backNode, backNode._Numchess);
        }
        if(numOfUnit > 0)
        {
            backNode._Numchess += 1;
            MovingBack(backNode.backNode, numOfUnit - 1);
        }        
    }
   
    public bool checkFinishMove(Node lastMoveNode,int status)
    {
        if (lastMoveNode.name == "A Side")
        {
            return true;
        }
        if (lastMoveNode.name == "B Side")
        {
            return true;
        }
        if (status == 1)
        {
            if (lastMoveNode.backNode._Numchess == 0 && lastMoveNode.backNode.backNode._Numchess == 0)
            {
                return true;
            }
            else if(lastMoveNode.backNode._Numchess == 0 && (lastMoveNode.backNode.backNode.name == "B Side" || lastMoveNode.backNode.backNode.name == "A Side"))
            {
                Debug.Log("Eat Chess Back");
                getPoint = true;
                lastMoveNode.backNode.backNode._Numchess = 0;
                return true;
            }
            else if(lastMoveNode.backNode._Numchess == 0 && lastMoveNode.backNode.backNode._Numchess != 0)
            {
                Debug.Log("Eat Chess Back");
                getPoint = true;
                lastMoveNode.backNode.backNode._Numchess = 0;
                return true;
            }
        }
        if(status == 0)
        {

            if (lastMoveNode.frontNode._Numchess == 0 && lastMoveNode.frontNode.frontNode._Numchess == 0)
            {
                return true;
            }
            else if (lastMoveNode.frontNode._Numchess == 0 && (lastMoveNode.frontNode.frontNode.name == "B Side" || lastMoveNode.frontNode.frontNode.name == "A Side"))
            {
                Debug.Log("Eat Chess Front");
                getPoint = true;
                lastMoveNode.frontNode.frontNode._Numchess = 0;
                return true;
            }else if(lastMoveNode.frontNode._Numchess == 0 && lastMoveNode.frontNode.frontNode._Numchess != 0)
            {
                Debug.Log("Eat Chess Front");
                getPoint = true;
                lastMoveNode.frontNode.frontNode._Numchess = 0;
                return true;
            }
        }
        return false;
    }

    public void reset(Node selectNode,Node choiceNode)
    {
        Renderer rend_1=selectNode.GetComponent<Renderer>();
        Renderer rend_2= choiceNode.GetComponent<Renderer>();
        rend_1.material.color=Color.white;
        rend_1.material.color = selectNode._defaultColor;
        rend_2.material.color = choiceNode._defaultColor;
        selectNode._isSelected = false;
        choiceNode._isSelected = false;
    }
}
