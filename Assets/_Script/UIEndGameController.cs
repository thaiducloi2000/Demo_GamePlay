using UnityEngine;
using TMPro;
public class UIEndGameController : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI title;
    [SerializeField] public TextMeshProUGUI name;
    [SerializeField] public TextMeshProUGUI point;
    public static UIEndGameController instance;
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("There are more  than 1 instance");
            return;
        }
        instance = this;
    }
    void Start()
    {
        this.title.gameObject.SetActive(false);
    }



    public void setPoint(string point,string name)
    {
        this.point.text = "Point  : " + point;
        this.name.text = "Player Name  : " + name;
    }

}
