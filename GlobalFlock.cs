using UnityEngine;
using System.Collections;

public class GlobalFlock : MonoBehaviour {
	public  GameObject   fishPref;
	public  static float tankSize =10;
	private static int   numFish  =10;
	public  static GameObject [] allfish=new GameObject[numFish];
    public GameObject Player;
    private float timer = 0;
    private bool is_inBorder = false;
    // Use this for initialization
    void Start () {

		for (int i = 0; i < numFish; i++) {

			Vector3 pos = new Vector3 (Random.Range (tankSize, tankSize+10), Random.Range (2, 3), Random.Range (tankSize, tankSize+10));
			allfish[i]= (GameObject)Instantiate(fishPref,pos,Quaternion.identity);
			allfish[i].transform.SetParent (this.transform,false);
			float randomscale = Random.Range (5,10);
			allfish [i].transform.localScale=new Vector3(randomscale,randomscale,randomscale);
			allfish [i].transform.SetParent (null);

            allfish[i].GetComponent<Flock>().goalpos = transform.position;
                }
	//Flock.goalpos = this.transform.position;
		Debug.Log ("GlobalFlock goalpos"+tankSize);
	}
    void Update()
    {
        //Flock.goalpos = this.transform.position;
        Border();
        if (is_inBorder)
        {
            for (int i = 0; i < numFish; i++)
                allfish[i].GetComponent<Flock>().goalpos = transform.position;
        }
       
    }
    void Border()
    {

        float x = Vector3.Distance(Player.transform.position, this.transform.position);
        if (x <= 30)
        {
            this.transform.Translate(0, 0, 20);
            timer = 4;
            is_inBorder = true;
        }
        else { is_inBorder = false; }


        if (timer >= 0)
        {
            Flock.runspeed = 5;
            Flock.runrotationspeed = 5;
            timer -= Time.deltaTime;
        }
        else
        {
            Flock.runspeed = 1;
            Flock.runrotationspeed = 1;
        }
    }
}
