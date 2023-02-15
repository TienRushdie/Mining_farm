using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LeaderBoard_strings : MonoBehaviour
{
    // Start is called before the first frame update
    public  Text Rating_id;
    public  Text Rating_nick;
    public  Text Rating_Money;
    public static GameObject me;

    private void Start()
    {
        lb_srting l = new lb_srting(Rating_id, Rating_nick, Rating_Money,me);
        me=l.LL(Rating_id.text, Rating_nick.text, Rating_Money.text);
    }
    public class lb_srting
    {

        public  GameObject me;
        private Text rating_id1;
        private Text rating_nick1;
        private Text rating_Money1;
      
        public lb_srting()
        {

        }

        public lb_srting(Text rating_id1, Text rating_nick1, Text rating_Money1, GameObject me)
        {
            this.rating_id1 = rating_id1;
            this.rating_nick1 = rating_nick1;
            this.rating_Money1 = rating_Money1;
            this.me = me;
            
        }

        lb_srting(string id, string NickName, string Money, object Rating_id,object Rating_nick,object Rating_money)
        {
             Rating_id= id;
            Rating_nick= NickName;
            Rating_money= Money;
        }


        public GameObject LL(string id, string NickName, string Money)
        {
            rating_id1.text = id;
            rating_nick1.text = NickName;
            rating_Money1.text = Money;
            return me;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
