using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int _Numchess;
    public int _CurrentNumChess;
    public int _startNumchess;
    public bool _isSelected;
    public Color _defaultColor ;
    public Node backNode;
    public Node frontNode;
    public MyGameManager gameManager;
    public GameObject chess;
    //TurnManager turnManager;

    void Start()
    {
        _isSelected= false;
        this._defaultColor = this.GetComponent<Renderer>().material.color;
        if (this.name == "A Side" || this.name == "B Side")
        {
            chess = Resources.Load<GameObject>("_Prefabs/SpecialUnit");
        }
        else
        {
            chess = Resources.Load<GameObject>("_Prefabs/Chess");
        }
        _Numchess = _CurrentNumChess = _startNumchess;
        //turnManager = TurnManager.instance;
        gameManager = MyGameManager.instance;
        SpawnUnit(_startNumchess);
    }

    void SpawnUnit(int num)
    {
        Transform point = this.gameObject.transform.GetChild(0);
        List<Vector3> listPosition = new List<Vector3>();
        for (int i = 0; i < num; i++)
        {
            Vector3 position = new Vector3(Random.Range(-0.3F, 0.3F), 0, Random.Range(-0.3F, 0.3F));
            if (this.name == "A Side" || this.name == "B Side")
            {
                   Instantiate(chess, this.gameObject.transform.GetChild(0).position + position, Quaternion.identity);
            }
            else
            {
                if (!listPosition.Contains(position))
                {
                    GameObject child = Instantiate(chess, point.position + position, Quaternion.identity);
                    child.transform.parent = this.transform;
                    listPosition.Add(position);
                }
            }
        }
    }

    void DeleteChangeNumChess(int num)
    {

        for (int i = 0; i < num; i++)
        {
            Transform pos = this.GetComponent<Transform>();
            GameObject chess = pos.GetChild(1).gameObject;
            Destroy(chess);
        }
    }

    void FixedUpdate()
    {
        if(_CurrentNumChess != _Numchess)
        {
            if(_CurrentNumChess - _Numchess < 0)
            {
                int Changenumber = _Numchess - _CurrentNumChess;
                SpawnUnit(Changenumber);
                _CurrentNumChess = _Numchess;
                Debug.Log(this.name + " Spawn " + Changenumber);
            }
            else if(_CurrentNumChess - _Numchess > 0)
            {
                int Changenumber = _CurrentNumChess - _Numchess;
                DeleteChangeNumChess(Changenumber);
                _CurrentNumChess = _Numchess;
                Debug.Log(this.name + " Delete " + Changenumber);
            }
            else if (_CurrentNumChess - _Numchess == 0)
            {
                DeleteChangeNumChess(_CurrentNumChess);
                _CurrentNumChess = _Numchess = 0;
                Debug.Log(this.name + " = 0");
            }
        }
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
    }
}
