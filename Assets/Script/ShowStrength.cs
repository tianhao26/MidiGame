using UnityEngine;
using System.Collections;

public class ShowStrength : MonoBehaviour {
	//目标位置
	private Vector3 mTarget;
	//屏幕坐标
	private Vector3 mScreen;
	//力度大小
	public string Value;

	//文本宽度
	public float ContentWidth=100;
	//文本高度
	public float ContentHeight=50;
	//GUI坐标
	private Vector2 mPoint;
	//销毁时间
	public float FreeTime=0.5f;
	// Use this for initialization
	void Start () {
		//获取目标位置
		mTarget=transform.position;
		//获取屏幕坐标
		mScreen= Camera.main.WorldToScreenPoint(mTarget);
		//将屏幕坐标转化为GUI坐标
		mPoint=new Vector2(mScreen.x,Screen.height-mScreen.y);
	}
	
	// Update is called once per frame
	void Update () {
		//重新计算坐标
		mTarget=transform.position;
		//获取屏幕坐标
		mScreen= Camera.main.WorldToScreenPoint(mTarget);
		//将屏幕坐标转化为GUI坐标
		mPoint=new Vector2(mScreen.x,Screen.height-mScreen.y);
	}
	void OnGUI()
	{
		GUI.color = Color.red;
		//保证目标在摄像机前方
		if(mScreen.z>0)
		{
			//内部使用GUI坐标进行绘制
			GUI.Label(new Rect(mPoint.x,mPoint.y,ContentWidth,ContentHeight),Value.ToString());
		}
	}
}
