using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelEvent : MonoBehaviour
{
	//当前关卡
	public Level level;
	
	public void OnClick()
	{
		if(level.UnLock){
			//假设关卡的名称即为对应场景的名称
			//Application.LoadLevel(level.Name);
			Debug.Log ("当前选择的关卡是:"+level.Name);
		}else{
			Debug.Log ("抱歉!当前关卡尚未解锁!");
		}
		
	}
	
}