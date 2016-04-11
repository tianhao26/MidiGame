using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/********************************************************************************

** 作者： tianhao

** 创始时间：2016-2-3

** 描述：

**    积分排名界面

*********************************************************************************/
public class EndingUi : MonoBehaviour {
	private float ratioScaleTempH = Screen.height / 960.0f;
	private float ratioScaleTemp = Screen.width / 540.0f;
	private string content = "积分榜：\r\n";

	// Use this for initialization
	void Start () {
		List<User> userList = UserUtil.getTopUsers (10);
		foreach (User user in userList)
		{
			//此处根据user对像拼接界面所需展现的信息，暂时只显示用户名和总分，待业务确定后具体修改
			content += user.Name+":"+user.TotalScore+"\r\n";
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI()  
	{
		//简陋的排行版界面，待美化
		GUI.Label(new Rect(100*ratioScaleTemp, 100*ratioScaleTempH, 800*ratioScaleTemp, 600*ratioScaleTempH), 
		          content);
	}
}
