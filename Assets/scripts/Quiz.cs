using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEditor;
using System;

public class Quiz : MonoBehaviour
{
    [SerializeField] private string URLLL = "http://localhost/mining_farm_db/Quiz.php";
    [SerializeField] private string URLL = "http://localhost/mining_farm_db/users_quiz.php";
    public Text[] quiz_info = new Text[5];
    public GameObject[] act1;
    [SerializeField] private Button myButton;
    public GameObject[] act2;

    string Correct_answer;
    private void Awake()
    {
        StartCoroutine(Load_Quiz());
    }
    void Start()
    {

        for (int i = 0; i < act1.Length; i++)
        {
            act1[i].SetActive(true);
        }
        for (int i = 0; i < act2.Length; i++)
        {
            act2[i].SetActive(false);
        }
    }
    public IEnumerator Load_Quiz()
    {
        WWW www = new(URLLL);
        yield return www;
        var result = www.text;
        var split = result.Split(':');
        quiz_info[0].text = split[0];
        string[] a = new string[split.Length - 1];
        a[0] = split[1];
        a[1] = split[2];
        a[2] = split[3];
        a[3] = split[4];
        Correct_answer = split[4].ToString();
        for (int i = 0; i < a.Length; i++)
        {
            string temp = a[i];
            
            int randomIndex = UnityEngine.Random.Range(i, a.Length);
            a[i] = a[randomIndex];
            a[randomIndex] = temp;
        }
        quiz_info[1].text = a[0].ToString();
        quiz_info[2].text = a[1].ToString();
        quiz_info[3].text = a[2].ToString();
        quiz_info[4].text = a[3].ToString();


    }
     public void Start_Quiz()
    {
        for(int i = 0; i < act1.Length; i++)
        {
            act1[i].SetActive(false);
        }
        for (int i = 0; i < 5; i++)
        {
            act2[i].SetActive(true);
        }
        //foreach (Button button in buttons)
        //{
        //    string buttonText = button.GetComponentInChildren<Text>().text;
        //    button.onClick.AddListener(delegate { AnswerClick(buttonText); });
        //}
    }
    void Exitt()
    {
        SceneManager.LoadScene(1);

    }
    public void AnswerClick(Text text)
    {
        string buttonText = text.text;
        for (int i = 0; i <5; i++)
        {
            act2[i].SetActive(false);
        }
        for (int i = 5; i <act2.Length; i++)
        {
            act2[i].SetActive(true);
        }
        if (buttonText == Correct_answer)
        {
            
            act2[5].SetActive(true);
            act2[6].GetComponent<Text>().text = "Поздравляем! Вы ответили верно. Ваша награда:";
            act2[7].GetComponent<Text>().text = "+100";
            float reward = PlayerPrefs.GetFloat("Balance");
            reward += 100;
            PlayerPrefs.SetFloat("Balance", reward);
         
        }
        else if (buttonText != Correct_answer)
        {
            
            act2[5].SetActive(true);
            act2[6].GetComponent<Text>().text = "Вы ответили неправильно. Не расстраивайтесь, ваша утешительная награда:";
            act2[7].GetComponent<Text>().text = "+10";
            float reward = PlayerPrefs.GetFloat("Balance");
            reward += 10;
            PlayerPrefs.SetFloat("Balance", reward);
           
        }

    }
  
    public void Exit()
    {
        Invoke("Exitt", 1f);

    }

    void Update()
    {
        
    }
}
