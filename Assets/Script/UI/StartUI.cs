using UnityEngine;
using System.Collections;
/********************************************************************************

** 作者： tianhao

** 创始时间：2016-3-24

** 描述：

**    用户登录界面

*********************************************************************************/
public class StartUI : MonoBehaviour {

	public void newGame()
	{
		PlayerPrefs.SetString ("isNewUser","true");
		Application.LoadLevel ("login");
		Destroy(this);
	}

	public void loadGame()
	{
		PlayerPrefs.SetString ("isNewUser","false");
		Application.LoadLevel ("login");
		Destroy(this);
	}

	public void exitGame()
	{
		Application.Quit ();
	}
	
}
