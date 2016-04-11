using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	private ScoreUtil scoreUtil;
	public GameObject Strength;
	private GameObject mObject;
	public string note;
	public string force;
	private Test test;

	// Use this for initialization
	void Start () {
		test = (Test)GameObject.Find ("Main Camera").GetComponent ("Test");
		scoreUtil = test.scoreUtil;
	    mObject=(GameObject)Instantiate(Strength,transform.position,Quaternion.identity);
		//mObject.GetComponent<ShowStrength>().Value=Random.Range(20,40);
		mObject.GetComponent<ShowStrength>().Value = note;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.down * Time.deltaTime);
		mObject.transform.parent = this.transform;
		this.transform.rotation = GameObject.Find ("Main Camera").transform.rotation;
		this.transform.rotation=Quaternion.AngleAxis(180,Vector3.up);
		/*if (transform.position.y < -1.5f && transform.position.y > - 3.4f) {
						if ( test.currentKey!=null && test.currentKey.Equals(note) && gameObject.tag == "cube") {
								//当前假设命中力度为1，目标力度为2，误差阀值为2，后续根据业务动态调整
								scoreUtil.bingo(1,2,2);
								Destroy (this.gameObject);
						}
						if (Input.GetKeyDown (KeyCode.Q) && gameObject.tag == "sphere") {
								//当前假设命中力度为1，目标力度为2，误差阀值为2，后续根据业务动态调整
								scoreUtil.bingo(1,2,2);
								Destroy (this.gameObject);
						}
						if (Input.GetKeyDown (KeyCode.E) && gameObject.tag == "capsule") {
								//当前假设命中力度为1，目标力度为2，误差阀值为2，后续根据业务动态调整
								scoreUtil.bingo(1,2,2);
								Destroy (this.gameObject);
						}
		}*/
	}

	void ReceiveBroadcastMessage(string str){
		if (transform.position.y < -1.5f && transform.position.y > - 3.4f) {
			if (str.Equals(note)&& gameObject.tag == "cube") {
				//当前假设命中力度为1，目标力度为2，误差阀值为2，后续根据业务动态调整
				scoreUtil.bingo(1,2,2);
				test.PlaySound();
				Destroy (this.gameObject);
				}
		}
	}

}
