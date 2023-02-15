using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text.RegularExpressions;

public class LoginMenuScript : MonoBehaviour
{
	public Text logintext;
	public Text passwordtext;
	int Capacity,t1c,t2c,t3c;
	public float Money;
	public int id;
	string Nick;
	[SerializeField] private string loginURL = "http://localhost/mining_farm_db/login.php";
    [SerializeField] private string rdURL = "http://localhost/mining_farm_db/load_data.php?user_id=";

	void Start()
	{
		
	}
	bool IsValid(string value, int min, int max, string field) // валидация имени и пароля
	{
		if (value.Length < min)
		{
			print("В поле [ " + field + " ] недостаточно символов, нужно минимум [ " + min + " ]");
			return false;
		}
		else if (value.Length > max)
		{
			print("В поле [ " + field + " ] допустимый максимум символов, не более [ " + max + " ]");
			return false;
		}
		else if (Regex.IsMatch(value, @"[^\w\.@-]"))
		{
			print("В поле [ " + field + " ] содержаться недопустимые символы.");
			return false;
		}

		return true;
	}

	void Update()
	{
	
	}
	
	public void Login2()
	{
		if (!IsValid(logintext.text, 5, 15, "Login") && !IsValid(passwordtext.text, 5, 15, "Password")) return;	
	    StartCoroutine(Load_data());
		Invoke("SceneLoading", 1.5f);
	}
	void SceneLoading()
    {
		SceneManager.LoadScene("SampleScene");

	}
	public void Login1()
    {
		StartCoroutine(CheckLogin());
	
	}
	IEnumerator CheckLogin()
	{
		WWWForm form = new WWWForm();
		WWW Query = new WWW("http://localhost/mining_farm_db/login.php?login=" + logintext.text.ToString() + "&password=" + passwordtext.text.ToString());
		print("http://localhost/mining_farm_db/login.php?login=" + logintext.text.ToString() + "&password=" + passwordtext.text.ToString());
		yield return Query;
		id = int.Parse(Query.text);
		print(id);
		rdURL= "http://localhost/mining_farm_db/load_data.php?user_id=" + id.ToString();
		print(rdURL);
	}
		
	IEnumerator Load_data()
    {

		WWW www = new(rdURL);
		yield return www;
		var result = www.text;
		var split = result.Split(' ');
		Nick = (split[0]);
		Money = int.Parse(split[1]);
		Capacity = int.Parse(split[2]);
		t1c = int.Parse(split[3]);
		t2c = int.Parse(split[4]);
		t3c = int.Parse(split[5]);
		PlayerPrefs.SetString("NickName", Nick);
		PlayerPrefs.SetFloat("Balance", Money);
		PlayerPrefs.SetInt("t1c", t1c);
		PlayerPrefs.SetInt("t2c", t2c);
		PlayerPrefs.SetInt("t3c", t3c);
		PlayerPrefs.SetInt("Capacity", Capacity);
		

		print("Конец света " + Nick + " " + Money + " " + Capacity);




	}
	 


}
