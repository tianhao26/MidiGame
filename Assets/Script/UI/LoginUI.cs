using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/********************************************************************************

** 作者： tianhao

** 创始时间：2016-3-24

** 描述：

**    用户登录界面

*********************************************************************************/
public class LoginUI : MonoBehaviour {
	private string isNewUser;
	// Use this for initialization
	void Start () {
		isNewUser = PlayerPrefs.GetString("isNewUser");
		GameObject userList = GameObject.Find ("list");
		GameObject input = GameObject.Find ("input");
		if (isNewUser.Equals ("false")) {
				input.active = false;
				userList.active = true;
				List<User> users = UserUtil.getAllUsers ();
				UIPopupList root = (UIPopupList)GameObject.Find ("list").GetComponent ("UIPopupList");
				foreach (User user in users) {
						root.items.Add (user.Name);
				}
		} else {
			input.active = true;
			userList.active = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void begin()
	{
		string userName = null;
		if (isNewUser.Equals ("false")) {
				userName = ((UILabel)GameObject.Find ("list label").GetComponent ("UILabel")).text;
				PlayerPrefs.SetString ("currentUser", userName);
				Application.LoadLevel ("chose");
				Destroy (this);
		} else {
				userName = ((UILabel)GameObject.Find ("input label").GetComponent ("UILabel")).text;
				UserUtil.addUser(userName);
				PlayerPrefs.SetString ("currentUser", userName);
				Application.LoadLevel ("chose");
				Destroy (this);
		}
	}
}
