using UnityEngine;
using System.Collections;

public class Flock : MonoBehaviour {
	public Vector3 goalpos;
	public static float runspeed=1;
	public static float runrotationspeed=1;
    //public GameObject target;
	private float speed=1f;
	private float rotationspeed=0.5f;
	private float nieghbourdistance=3f;
//	private Transform Target;
	private Vector3 averageheading;
	private Vector3 averageposition;

	bool turning =false;

	void Start ()
	{
      //  gflock = target.GetComponent<GlobalFlock>();
		speed = Random.Range (1f, 5f)*runspeed;
	}

	void Update ()
	{
			if (Vector3.Distance (transform.position, goalpos) >= GlobalFlock.tankSize) {
		turning = true;
	} else {
		turning = false;
	}
		if (turning) {
					Vector3 direction = goalpos - transform.position;
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction),
				rotationspeed * Time.deltaTime*runrotationspeed);
			//when player is too close to the flock fiish will run 
			speed = Random.Range (1f, 5f)*runspeed;
		}
			else {
			if(Random.Range(0.5f,20)<1)
				applayRules ();
			}
		//Always walk and avoid 
		avoid ();
		this.transform.Translate (0,0,speed*Time.deltaTime);
	}
	void avoid()
	{

		GameObject[] gos;
		gos = GlobalFlock.allfish;
		Vector3 vavoid = Vector3.zero;
		float dist;
		foreach (GameObject go in gos) {

			if (go != this.gameObject) {
				dist = Vector3.Distance (go.transform.position, this.transform.position);
				if (dist < 5.0f) {

					vavoid = vavoid + (this.transform.position - go.transform.position);
					transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (vavoid), rotationspeed * Time.deltaTime*runrotationspeed);
				}
			
			}
		}	
	}

	void applayRules()
	{
	
		GameObject[] gos;
		gos = GlobalFlock.allfish;
		Vector3 vcenter = Vector3.zero;
		Vector3 vavoid = Vector3.zero;

		float dist;
		float gspeed = 0.1f;
		int groupSize = 0;
		foreach (GameObject go in gos) {
		
			if(go!=this.gameObject){
				dist = Vector3.Distance (go.transform.position, this.transform.position);
				if (dist <= nieghbourdistance) {
				
					vcenter += go.transform.position;
					groupSize++;
					if (dist < 2.0f) {
					
						vavoid = vavoid + (this.transform.position - go.transform.position);
					//	transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (vavoid), rotationspeed * Time.deltaTime);
					}
					Flock anotherFlock = go.GetComponent<Flock> ();
					gspeed = gspeed + anotherFlock.speed;
				}

			}
		}
			
		if (groupSize > 0) {
			vcenter = vcenter / groupSize + (goalpos - this.transform.position);
			speed = gspeed / groupSize;
			Vector3 direction = (vcenter + vavoid) - transform.position;
			if (direction != goalpos) {
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction), rotationspeed * Time.deltaTime*runrotationspeed);
			
			}
		
		}
	}

}
