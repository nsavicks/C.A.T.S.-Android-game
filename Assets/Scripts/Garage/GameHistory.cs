using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHistory : MonoBehaviour
{

    public GameObject content;
    public GameObject prefab;

    private Player player;
    private List<GamePlayed> games;

    private Sprite win;
    private Sprite lose;

    // Start is called before the first frame update
    void Start()
    {
        win = Resources.Load<Sprite>("grey_boxCheckmark");
        lose = Resources.Load<Sprite>("grey_boxCross");

        string nickname = PlayerPrefs.GetString("player");

        player = DatabaseDataAcces.getPlayerWithNickname(nickname);

        games = DatabaseDataAcces.getGamesPlayed(player.id);

        for(int i = games.Count - 1; i >= 0; i--)
        {
            GamePlayed g = games[i];

            GameObject item = Instantiate(prefab);
            Image image = item.transform.Find("Image").GetComponent<Image>();
            Text player2 = item.transform.Find("player2").GetComponent<Text>();

            if (g.winner == 1)
            {
                image.sprite = win;
            }
            else
            {
                image.sprite = lose;
            }

            player2.text = g.secondPlayer.nickname;

  

            item.transform.SetParent(content.transform, false);
            

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
