using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Camera_Player : MonoBehaviour
{
    public GameObject ratingtable;
    public GameObject Panel;
    public GameObject ProfilePanel;
    public GameObject ShopPanel;
    public bool IsPanelActive;
    public Text moneytext;
    [SerializeField] private string URLL = "http://localhost/mining_farm_db/rating.php";
    [SerializeField] private string URLLL = "http://localhost/mining_farm_db/profile.php?user_Nick=";
    public string[,] top10=new string[5,3];
    playerController playerController;
    public Text[] rating=new Text[18];
    public Text[] profile_info = new Text[7];
    void Start()
    {
        Panel.SetActive(false);
        ProfilePanel.SetActive(false);
        IsPanelActive = false;
        ShopPanel.SetActive(false);
        URLLL += PlayerPrefs.GetString("NickName");
        Invoke("Load_Profile_Info", 2f);

    }

    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) { ProfilePanel.SetActive(false); ShopPanel.SetActive(false); }
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);
            Transform objecthit = hit.transform;
            
            if (objecthit.gameObject != null)
            {
                Debug.Log("Hit Collider: " + hit.transform.name);
            }
            else
            {
                Debug.Log("Null Hit");
            }


            if (hit.transform.tag == "RatingTable")
            {
                ratingtable.SetActive(false);
                Panel.gameObject.SetActive(true);
                IsPanelActive = true;
                StartCoroutine(Load_Rating());
               
            }
            if (hit.transform.tag == "Shop")
            {
                ShopPanel.SetActive(true);
                

            }
            if (hit.transform.tag == "clone")
            {
                SceneManager.LoadScene(2);
            }


        }
    }
    public void Profile()
    {   ProfilePanel.SetActive(true);
        print("Kurwa");
        StartCoroutine(Load_Profile_Info());
    }
    public void pExit()
    {
        ProfilePanel.SetActive(false);
        ShopPanel.SetActive(false);
        Load_Profile_Info();
    }
   public IEnumerator Load_Profile_Info()
    {
        WWW www = new(URLLL);
        yield return www;
        var result = www.text;
        var split = result.Split(' ');
        profile_info[0].text="Login: " + split[0];
        profile_info[1].text="NickName: "+split[1];
        profile_info[2].text="Balance: "+moneytext.text;
        profile_info[3].text="Capacity: "+PlayerPrefs.GetInt("Capacity");
        profile_info[4].text= PlayerPrefs.GetInt("t1c").ToString(); 
        profile_info[5].text= PlayerPrefs.GetInt("t2c").ToString(); 
        profile_info[6].text= PlayerPrefs.GetInt("t3c").ToString(); 

    }
    


    IEnumerator Load_Rating()
    {
        int g = 0;
        WWW www = new(URLL);
        yield return www;
        var result = www.text;
        var split = result.Split(',');
        print(split[0]);
        int i = 0;
        foreach (var item in split)
        {
            if (i == 5) { break; }
            var splitItem = item.Split(' ');

            for (int j = 0; j < splitItem.Length; j++)
            {
                top10[i, j] = splitItem[j];
                if (splitItem[j] == PlayerPrefs.GetString("NickName"))
                {
                    print(PlayerPrefs.GetString("NickName"));
                    rating[j].color = Color.red;
                    rating[j-1].color = Color.red;
                    rating[j+1].color = Color.red;
                    rating[15].text = "";
                    rating[16].text = PlayerPrefs.GetString("NickName");
                    rating[17].text = PlayerPrefs.GetFloat("Balance").ToString();

                }
                else
                {
                    rating[15].text = "";
                    rating[16].text = PlayerPrefs.GetString("NickName");
                    rating[17].text = PlayerPrefs.GetFloat("Balance").ToString();
                }
            }
            rating[g].text = (i + 1).ToString();
            g++;
            rating[g].text = top10[i, 1];
            g++;
            rating[g].text = top10[i, 2];
            g++;
            i++;
        }
    }
}
