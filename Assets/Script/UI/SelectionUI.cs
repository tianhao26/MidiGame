using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/********************************************************************************

** 作者： tianhao

** 创始时间：2016-3-3

** 描述：

**    关卡选择界面,根据配置文件信息动态生成关卡列表界面

*********************************************************************************/
public class SelectionUI : MonoBehaviour {
	public UIAtlas Atlas;
	//关卡列表
	private List<Level> m_levels;
	// Use this for initialization
	void Start () {
		//获取关卡
		m_levels = LevelSystem.LoadLevels ();
		foreach (Level l in m_levels) 
		{
			CreateLevelButton(l);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {

		/*foreach (Level l in m_levels) 
		{
			if(int.Parse(l.ID)%3==0){
				if (GUI.Button(new Rect(525, 100+(int.Parse(l.ID)/3-1)*200, 150, 150), l.Name, "")) {
					if(l.UnLock){
						//此处编写已解锁关卡逻辑
					}
				}
			}
			if(int.Parse(l.ID)%3==2){
				if (GUI.Button(new Rect(325, 100+(int.Parse(l.ID)/3)*200, 150, 150), l.Name, "")) {
					if(l.UnLock){
						//此处编写已解锁关卡逻辑
					}
				}
			}
			if(int.Parse(l.ID)%3==1){
				if (GUI.Button(new Rect(125, 100+(int.Parse(l.ID)/3)*200, 150, 150), l.Name, "")) {
					if(l.UnLock){
						//此处编写已解锁关卡逻辑
					}
				}
			}
		}*/

	}

	private void CreateLevelButton(Level level)  
	{  
		//获得深度（要创建button的Panle的深度）  
		int depth = NGUITools.CalculateNextDepth(GameObject.Find ("UI Root"));  
		//创建button物体,命名、设tag、摆所在panle的相对位置。  
		GameObject go = NGUITools.AddChild(GameObject.Find ("UI Root"));  
		go.name = level.Name;  
		go.tag = "Untagged";  
		go.transform.localPosition = new Vector3(-180 + (int.Parse(level.ID)%3==0?2:int.Parse(level.ID)%3 - 1) * 180, 100-(int.Parse(level.ID)-1)/3*100, 0);  
		
		//添加button的背景图片UISprite  
		UISprite bg = NGUITools.AddWidget<UISprite>(go);  
		bg.type = UISprite.Type.Sliced;  
		bg.name = "Background";  
		bg.depth = depth;  
		//背景图片使用的图集  
		bg.atlas = Atlas;  
		//图集中使用的精灵名字  
		bg.spriteName = level.ID.ToString();
		bg.transform.localScale = new Vector3(150f, 40f, 1f);
		//跳转位置（必要）  
		//bg.MakePixelPerfect();  

		//添加碰撞（有碰撞才能接收鼠标/触摸），大小与Button背景一致  
		/*BoxCollider box= */ 
		//box.center = new Vector3(0,0,-1);  
		//box.size = new Vector3(bg.transform.localScale.x, bg.transform.localScale.y, 0);  

		if (level.UnLock) {
			NGUITools.AddWidgetCollider(go);
			BoxCollider box = (BoxCollider)go.GetComponent ("BoxCollider");
			box.center = new Vector3(0,0,-1);  
			box.size = new Vector3(bg.transform.localScale.x, bg.transform.localScale.y, 0); 
			//添加UIButton触发事件的必要组件，并关联之前生成的UISprite  
			go.AddComponent<UIButton>().tweenTarget = bg.gameObject;
			UIButton button = (UIButton)go.GetComponent ("UIButton");
			//button.enabled = false;
			//添加动态效果组件（大小、位移、音效）。（可选）  
			go.AddComponent<UIButtonScale>();  
			go.AddComponent<UIButtonOffset>();
			//go.AddComponent<UIButtonSound>();
			UIEventListener.Get(go).onClick = btnHandler;
		}
	}

	void btnHandler(GameObject button)
	{
		PlayerPrefs.SetString("selectedLevel",button.name);
		Application.LoadLevel ("Edition1");
		Destroy(this);
	}
}
