using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Node : MonoBehaviour
{
    public int _Numchess;
    public int _startNumchess;
    public bool _isSelected;
    public Color _defaultColor ;
    public Node backNode;
    public Node frontNode;
    public Action<Node> onClickNode;
    public MyGameManager gameManager;
    //TurnManager turnManager;

    void Start()
    {
        _isSelected= false;
        this._defaultColor = this.GetComponent<Renderer>().material.color;
        _Numchess = _startNumchess;
        //turnManager = TurnManager.instance;
        gameManager = MyGameManager.instance;
    }

    void FixedUpdate()
    {
        
    }

    private void checkSelected()
    {
        Renderer rend = this.GetComponent<Renderer>();
        if (!_isSelected)
        {
            this._isSelected = true;
            rend.material.color = Color.red;
            Debug.Log(this.name + " is selected...");
        }
        else
        {
            this._isSelected = false;
            rend.material.color = _defaultColor;
            Debug.Log(this.name + " is not selected...");
        }
    }

    private void OnMouseDown()
    {
        checkSelected();
        //Renderer rend = this.GetComponent<Renderer>();
        if (this._Numchess == 0)
        {
            Debug.Log("Can't Select This....");
        }
        if ((this.name == "A Side" || this.name == "B Side") && (this.frontNode._isSelected == true || this.backNode._isSelected == true))
        {
            Debug.Log("Can't Select This....");
        }
        if (this._isSelected == true)
        {
            gameManager.Move(this);
        }
        //if (this.frontNode._isSelected)
        //{
        //    Debug.Log("Moving Front");
        //    turnManager.MovingFront(this.frontNode, this._tmpNumchess);
        //    turnManager.reset(this, this.frontNode);
        //}
        //else if (this.backNode._isSelected)
        //{
        //    Debug.Log("Moving Back");
        //    turnManager.MovingBack(this.backNode, this._tmpNumchess);
        //    turnManager.reset(this, this.backNode);
        //}
    }
}
