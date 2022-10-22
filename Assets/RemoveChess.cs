using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveChess : MonoBehaviour
{
    public void DeleteChess()
    {
        Destroy(this.gameObject);
    }
}
