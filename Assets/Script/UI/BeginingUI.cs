using UnityEngine;
using System.Collections;
/********************************************************************************

** 作者： tianhao

** 创始时间：2016-1-24

** 描述：

**    用户登录界面

*********************************************************************************/
public class BeginingUI : MonoBehaviour {
	
	private string userName="请输入用户名...";
	private float ratioScaleTempH = Screen.height / 960.0f;
	private float ratioScaleTemp = Screen.width / 540.0f;
	private bool doWindow0 = false;

	// Use this for initialization
	void Start () {
		UserUtil.init ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI()  
	{  
		userName = GUI.TextField(new Rect(200*ratioScaleTemp,600*ratioScaleTempH,150*ratioScaleTemp,80*ratioScaleTempH),userName,15);
		if (doWindow0) {
			GUI.Window(0, new Rect(150*ratioScaleTemp,400*ratioScaleTempH,300*ratioScaleTemp,500*ratioScaleTempH),doWindow,"提示");
		}
		if(GUI.Button(new Rect(200*ratioScaleTemp,800*ratioScaleTempH,150*ratioScaleTemp,80*ratioScaleTempH),"确定"))  
		{  
			if(userName.Equals("")||userName.Equals("请输入用户名...")){
				doWindow0=true;
			}else{
				PlayerPrefs.SetString("currentUser",userName);
				print(PlayerPrefs.GetString("currentUser"));
				UserUtil.addUser(userName);
				print(UserUtil.isUserExist(userName));
				Application.LoadLevel ("Edition1");
				Destroy(this);
				//userUtil.addScoreToUser(userName,"stage1",75);
				//userUtil.getTopUsers (2);
			}
		}
	}

	void doWindow(int id) {
		GUI.Label(new Rect(90*ratioScaleTemp, 175*ratioScaleTempH, 200*ratioScaleTemp, 200*ratioScaleTempH), "请输入一个有效的用户名！");
		if (GUI.Button (new Rect (120*ratioScaleTemp, 350*ratioScaleTempH, 70*ratioScaleTemp, 50*ratioScaleTempH), "确认")) {
			doWindow0=false;
		}
	}
}
