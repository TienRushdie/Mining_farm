using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEditor;
public class playerController : MonoBehaviour
    
{
    float hor, vert;
    public float speed;
    int Capacity;
    Rigidbody2D rb;
    public GameObject ratingtable;
    public float Money;
    int c1,c2,c3;
    public Text moneytext;
    public GameObject err_txt;
    public Text nicktext;
    public Text[] texts = new Text[10];
    public GameObject Panel;
    public bool IsPanelActive;
    [SerializeField] private string udURL = "http://localhost/mining_farm_db/upload_data.php";

    Animator anim;
 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Money = PlayerPrefs.GetFloat("Balance");
        Capacity = PlayerPrefs.GetInt("Capacity");
        nicktext.text = PlayerPrefs.GetString("NickName");
        StartCoroutine(EarningMoney());
        Panel.SetActive(false);
        IsPanelActive = false;

    }
    private void Awake()
    {
       
    }
    public void BuyVideoCards(int i)
    {
        
        if (i == 1)
        {
            if (Money > 200)
            {
                c1 = int.Parse(texts[0].text.ToString());
                c2 = int.Parse(texts[1].text.ToString());
                c3 = int.Parse(texts[2].text.ToString());
                if (c1 < 10)
                {
                    Money -= 200;

                    c1 += 1;
                    texts[0].text = c1.ToString();
                    savingProgress(c1, c2, c3);
                    moneytext.text = Money.ToString();
                }
                else if (c1>= 10) { err_txt.SetActive(true); texts[3].text = "You have too many cards of this type"; Invoke("le", 5f); }

            }
            else { err_txt.SetActive(false); err_txt.SetActive(true); texts[3].text = "NOT ENOUGH MONEY"; Invoke("le", 3f); }
        }
        else if (i == 2)
        {
            if (Money > 1000)
            {
                c1 = int.Parse(texts[0].text.ToString());
                c2 = int.Parse(texts[1].text.ToString());
                c3 = int.Parse(texts[2].text.ToString());
                if (c2 < 10)
                {
                    Money -= 1000;

                    c2 += 1;
                    texts[1].text = c2.ToString();
                    savingProgress(c1, c2, c3);
                    moneytext.text = Money.ToString();
                }
                else if (c2 >= 10) { err_txt.SetActive(true); texts[3].text = "You have too many cards of this type"; Invoke("le", 5f); }


            }
            else { err_txt.SetActive(false); err_txt.SetActive(true); texts[3].text = "NOT ENOUGH MONEY"; Invoke("le", 3f); }

        }
        else if (i == 3)
        {

            if (Money > 2400)
            {
                c3 = int.Parse(texts[0].text.ToString());
                c2 = int.Parse(texts[1].text.ToString());
                c1 = int.Parse(texts[2].text.ToString());
                if (c1<10)
                {
                    Money -= 2400;

                    c1 += 1;
                    texts[2].text = c1.ToString();
                    savingProgress(c3, c2, c1);
                    moneytext.text = Money.ToString();
                }else if (c1 >= 10) { err_txt.SetActive(false); err_txt.SetActive(true);texts[3].text = "You have too many cards of this type"; Invoke("le", 5f); }
                
            }
            else { err_txt.SetActive(false); err_txt.SetActive(true); texts[3].text = "NOT ENOUGH MONEY"; Invoke("le", 3f); }
        }
    }
    public void savingProgress(int t1c,int t2c, int t3c)
    { 
        PlayerPrefs.SetString("NickName", nicktext.text);
        PlayerPrefs.SetFloat("Balance",Money);
        PlayerPrefs.SetInt("t1c", t1c);
        PlayerPrefs.SetInt("t2c", t2c);
        PlayerPrefs.SetInt("t3c",t3c);
        PlayerPrefs.SetInt("Capacity", (t1c * 1 + t2c * 2 + t3c * 3));
        Capacity = PlayerPrefs.GetInt("Capacity");
    }
    private void le()
    {
        err_txt.SetActive(false);
        c1 = int.Parse(texts[0].text.ToString());
        c2 = int.Parse(texts[1].text.ToString());
        c3 = int.Parse(texts[2].text.ToString());
        savingProgress(c1, c2, c3);
    }

    public void ExitFromGame() {
        UpdateBalance();
        
        SceneManager.LoadScene("Menu"); }
    public void OnApplicationQuit()
    {    
        UpdateBalance();
        
        
        
        
    }
    IEnumerator EarningMoney()
    {
        while (true)
        {
            Money = Money + Capacity*0.1f;
            moneytext.text = Money.ToString("0");
            yield return new WaitForSeconds(1);
        }
    }
  public void UpdateBalance()
    {
        WWWForm form = new WWWForm();
        form.AddField("nick", nicktext.text);
        form.AddField("money", Money.ToString());
        form.AddField("t1c", texts[0].text.ToString());
        form.AddField("t2c", texts[1].text.ToString());
        form.AddField("t3c", texts[2].text.ToString());
        Capacity =
            (int.Parse(texts[0].text.ToString())*1 +
            int.Parse(texts[1].text.ToString())*2 +
            int.Parse(texts[2].text.ToString())*3);
        form.AddField("cap", Capacity);

        WWW www = new WWW(udURL, form);
        

    }
    
  


  
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) { Panel.SetActive(false);ratingtable.SetActive(true); }
       
    }

}
