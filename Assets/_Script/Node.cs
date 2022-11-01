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
    public GameObject collider;
    //public GameObject spawner;
    TurnManager turnManager;

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
        turnManager = TurnManager.instance;
        gameManager = MyGameManager.instance;
        //spawner = GameObject.Find("Spawner");
        SpawnUnit(_startNumchess,chess);
    }

    void SpawnUnit(int num,GameObject unit)
    {
        Transform point = this.gameObject.transform.GetChild(0);
        List<Vector3> listPosition = new List<Vector3>();
        for (int i = 0; i < num; i++)
        {
            Vector3 position = new Vector3(Random.Range(-0.3F, 0.3F), 0, Random.Range(-0.3F, 0.3F));
            if (this.name == "A Side" || this.name == "B Side")
            {
                    GameObject child = Instantiate(unit, this.gameObject.transform.GetChild(0).position + position, Quaternion.identity);
                    child.transform.parent = this.transform;
                    listPosition.Add(position);
            }
            else
            {
                if (!listPosition.Contains(position))
                {
                    GameObject child = Instantiate(unit, point.position + position, Quaternion.identity);
                    child.transform.parent = this.transform;
                    listPosition.Add(position);
                }
            }
        }
        //this.collider.SetActive(false);
    }

    void DeleteChangeNumChess(int num)
    {
        Debug.Log(this.name + " - " + num);
        for (int i = 1; i <= num; i++)
        {
            Transform pos = this.GetComponent<Transform>();
            GameObject chess = pos.GetChild(i).gameObject;
            Animator ani = chess.GetComponent<Animator>();
            ani.SetBool("isDestroy",true);
        }
    }

    public void checkChessNum()
    {
        if (this._CurrentNumChess != this._Numchess)
        {
            if (this._CurrentNumChess - this._Numchess > 0)
            {
                int Changenumber = this._CurrentNumChess - this._Numchess;
                DeleteChangeNumChess(Changenumber);
                this._CurrentNumChess = this._Numchess;
            }
            else if (_CurrentNumChess - _Numchess == 0)
            {
                DeleteChangeNumChess(_CurrentNumChess);
                this._CurrentNumChess = this._Numchess = 0;
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
        }
        else
        {
            this._isSelected = false;
            rend.material.color = _defaultColor;
        }
    }

    private void OnMouseDown()
    {
        if (!gameManager.EndGame())
        {
            checkSelected();
            //Renderer rend = this.GetComponent<Renderer>();
            if (gameManager.isPlayerMove() == true)
            {
                if (this._Numchess == 0)
                {
                    //return;
                }
                if ((this.name == "A Side" || this.name == "B Side") && (this.frontNode._isSelected == true || this.backNode._isSelected == true))
                {
                    //return;
                }
                if (this._isSelected == true)
                {
                    //Spawner player = this.spawner.GetComponent<Spawner>();
                    gameManager.Move(this);
                    //collider.SetActive(true);
                    //player.nodeName = this.name;
                }
            }
        }
    }

    public void Reset()
    {
        Renderer rend_1 = this.GetComponent<Renderer>();
        rend_1.material.color = this._defaultColor;
        this._isSelected = false;
    }
}
