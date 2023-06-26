using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;

public class LoginMenuScript : MonoBehaviour
{
	public Text logintext;
	public GameObject passwordtext, errtext;
	int Capacity,t1c,t2c,t3c;
	public GameObject LoginPanel, RegisterPanel;
	public float Money;
	public int id,code,a,b;
	string Nick;
	int task;
	public Text[] texts = new Text[10];
	[SerializeField] private string loginURL = "http://localhost/mining_farm_db/login.php";
    [SerializeField] private string rdURL = "http://localhost/mining_farm_db/load_data.php?user_id=";

	void Start()
	{
		LoginPanel.SetActive(true);
		RegisterPanel.SetActive(false);
		a = Random.Range(1, 50);
		b = Random.Range(1, 50);
		task = a + b;
		texts[6].text = "Proove that you're human: " + a + " + " + b + " =?";
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
		if (!IsValid(logintext.text, 5, 15, "Login") && !IsValid(passwordtext.GetComponent<InputField>().text.ToString(), 5, 15, "Password")) return;
		if (int.Parse(texts[5].text) == task)
		{
			StartCoroutine(Load_data());
			Invoke("SceneLoading", 1.5f);
        }
        else { texts[6].color = Color.red; }
	}
	void SceneLoading()
    {
		SceneManager.LoadScene("SampleScene");

	}
	public void Login1(int i)
    {
		if (i == 1)
		{
			StartCoroutine(CheckLogin(logintext.ToString()));
		}
		else if(i == 2)
        {
			LoginPanel.SetActive(false);
			RegisterPanel.SetActive(true);

        }
		else if(i==3)
		{

			LoginPanel.SetActive(true);
			RegisterPanel.SetActive(false);
			a= Random.Range(10, 50);
			b= Random.Range(10, 50);
			task = a + b;
			texts[6].text = "Proove that you're human: " + a + " + " + b + " = ?";
		}
		else if (i == 99)
        {
			print("ss");
			Application.Quit();
        }
		else if (i == 10)
        {
            if (int.Parse(texts[3].text) == code)
            {
				StartCoroutine(chackreg());
            }
			else
            {
				print("no");
            }
        }
		else if (i == 15)
        {
			if (!IsValid(texts[0].text, 5, 15, "Login") && !IsValid(texts[1].text.ToString(), 5, 15, "Password")) return;
			StartCoroutine(Load_data());
			Invoke("SceneLoading", 1.5f);
		}
	
	}

	
	public void checkmail()
    {
		code = Random.Range(100, 1000);
		MailMessage message = new MailMessage();
		message.From = new MailAddress("almaz.mining@yandex.ru");
		message.To.Add(new MailAddress(texts[2].text));
		message.Subject = "Код подтверждения почты";
		message.IsBodyHtml = true;
		// адрес smtp-сервера и порт, с которого будем отправлять письмо
		SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 587);
		// логин и пароль
		smtp.Credentials = new NetworkCredential("almaz.mining@yandex.ru", "aaaiiiOp0");
		smtp.EnableSsl = true;
		message.Body = "Ваш код подтверждения: "+code.ToString();
		
		smtp.Send(message);
		print("send");
	}
	IEnumerator CheckLogin(string login)
	{
		WWWForm form = new WWWForm();
		WWW Query = new WWW("http://localhost/mining_farm_db/login.php?login=" + logintext.text.ToString() + "&password=" + passwordtext.GetComponent<InputField>().text.ToString());
		print("http://localhost/mining_farm_db/login.php?login=" + logintext.text.ToString() + "&password=" + passwordtext.GetComponent<InputField>().text.ToString());
		yield return Query;
		var result = Query.text;
		var split = result.Split(' ');
		bool a = int.TryParse(split[0],out int asa);
		if (a==false) { print("lox"); }
		else
		{
			id = int.Parse(Query.text);
			print(id);
			rdURL = "http://localhost/mining_farm_db/load_data.php?user_id=" + id.ToString();
			print(rdURL);
			PlayerPrefs.SetInt("Quiz_p_ID", id);
		}
	}
	IEnumerator chackreg()
	{
		WWWForm form = new WWWForm();
		WWW Query = new WWW("http://localhost/mining_farm_db/register.php?login=" + texts[0].text.ToString() + "&password=" + texts[1].text.ToString() + "&email=" + texts[2].text);
		yield return Query;
		print(Query.text);
		id = int.Parse(Query.text);
		rdURL = "http://localhost/mining_farm_db/load_data.php?user_id=" + id.ToString();
		print(rdURL);

	}

	IEnumerator Load_data()
    {
		WWW www = new(rdURL);
		yield return www;
		var result = www.text;
		var split = result.Split(',');
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
