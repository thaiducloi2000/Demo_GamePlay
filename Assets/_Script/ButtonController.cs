// this file to controll the tranfer scence
// each time click on button will create a new funtion to call that scence

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonController : MonoBehaviour
{
    [SerializeField] private string playGame = "MainScene";
    public GameObject player;
    public InputField playerName;
    public void PlayGameButton()
    {
        loadPlayer();
        SceneManager.LoadSceneAsync(playGame);
    }

    public void loadPlayer()
    {
        PlayerOffline player1 = player.GetComponent<PlayerOffline>();
        player1.name = playerName.text;
    }

}
