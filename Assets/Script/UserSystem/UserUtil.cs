using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.IO;
using System;
/********************************************************************************

** 作者： tianhao

** 创始时间：2016-1-24

** 描述：

**    封装对游戏用户的各种操作

*********************************************************************************/
public static class UserUtil  {
	private static string xmlPath = Application.streamingAssetsPath + "/score.xml";

	//初始化保存用户的文件
	public static void init()
	{
		if (xmlPath == null)
			xmlPath = Application.streamingAssetsPath + "/score.xml";
		if(!File.Exists(xmlPath))
		{
			//创建最上一层的节点。
			XmlDocument xml = new XmlDocument();
			//创建最上一层的节点。
			XmlElement root = xml.CreateElement("users");
			
			xml.AppendChild(root);
			//最后保存文件
			xml.Save(xmlPath);
		}
	}
	//添加一个用户
	public static void addUser(string userName)
	{
		if (userName == null || userName.Equals ("")) 
			throw new Exception ("用户名为空");
		if (!File.Exists (xmlPath)) 
			init ();
		XmlDocument xml = new XmlDocument();
		xml.Load(xmlPath);
		XmlNode root = xml.SelectSingleNode("users");

		XmlNodeList xmlNodeList = xml.SelectSingleNode("users").ChildNodes;
		foreach(XmlElement xl1 in xmlNodeList)
		{
			if(xl1.GetAttribute("name")==userName)
			{
				return;
			}
		}
			
		XmlElement element = xml.CreateElement("user");
		element.SetAttribute("name",userName);
			
		root.AppendChild(element);
			
		xml.AppendChild(root);
		//最后保存文件
		xml.Save(xmlPath);
	}
	//判断一个用户是否存在
	public static bool isUserExist(string userName)
	{
		if (userName == null || userName.Equals ("")) 
						throw new Exception ("用户名为空");
		if (!File.Exists (xmlPath)) 
						init ();
		bool result = false;
		XmlDocument xml = new XmlDocument ();
		xml.Load(xmlPath);
		XmlNodeList xmlNodeList = xml.SelectSingleNode ("users").ChildNodes;
		//遍历所有子节点
		foreach (XmlElement xl1 in xmlNodeList) {
			if (xl1.GetAttribute ("name") == userName) {
				result = true;
			}
		}
		return result;
	}
	//判断某个用户某个关卡的分数是否存在
	public static bool isScoreExist(string userName,int stage)
	{
		if (userName == null || userName.Equals ("")) 
			throw new Exception ("用户名为空");
		if (stage<=0) 
			throw new Exception ("关卡异常:" + stage);
		if (!File.Exists (xmlPath)) 
			init ();
		bool result = false;
		XmlDocument xml = new XmlDocument ();
		xml.Load(xmlPath);
		XmlNodeList xmlNodeList = xml.SelectSingleNode ("users").ChildNodes;
		//遍历所有子节点
		foreach (XmlElement xl1 in xmlNodeList) {
			if (xl1.GetAttribute ("name") == userName) {
				foreach(XmlElement xl2 in xl1.ChildNodes)
				{
					if(int.Parse(xl2.GetAttribute("id"))==stage)
					{
						result = true;
					}
					
				}
			}
		}
		return result;
	}
	//给一个用户添加一个关卡的得分
	public static void addScoreToUser(string userName,int stage,int score) 
	{	
		if (userName == null || userName.Equals ("")) 
			throw new Exception ("用户名为空");
		if (stage<=0) 
			throw new Exception ("关卡异常:" + stage);
		if (score<0) 
			throw new Exception ("得分信息错误："+score);
		if (!File.Exists (xmlPath))
						init ();
		if (!isUserExist (userName))
						addUser(userName);
		if (isScoreExist (userName, stage)) 
		{
			updateScoreToUser(userName,stage,score);
			return;
		}
		XmlDocument xml = new XmlDocument ();
		xml.Load (xmlPath);
		XmlNodeList xmlNodeList = xml.SelectSingleNode ("users").ChildNodes;
		foreach (XmlElement xl1 in xmlNodeList) {
			if (xl1.GetAttribute ("name") == userName) {
				XmlElement element = xml.CreateElement ("stage");
				element.SetAttribute ("id", stage.ToString());
				element.SetAttribute ("score", score.ToString());
				xl1.AppendChild (element);
			}
		}
		xml.Save (xmlPath);
	}
	//修改一个用户某个关卡的分数
	public static void updateScoreToUser(string userName,int stage,int score)
	{
		if (userName == null || userName.Equals ("")) 
			throw new Exception ("用户名为空");
		if (stage<=0) 
			throw new Exception ("关卡异常:" + stage);
		if (score<0) 
			throw new Exception ("得分信息错误："+score);
		if (!File.Exists (xmlPath))
			init ();
		if (!isUserExist (userName))
			addUser(userName);
		if (!isScoreExist (userName, stage))
		{
			addScoreToUser(userName,stage,score);
			return;
		}
		XmlDocument xml = new XmlDocument();
		xml.Load(xmlPath);
		XmlNodeList xmlNodeList = xml.SelectSingleNode("users").ChildNodes;
		foreach(XmlElement xl1 in xmlNodeList)
		{
			if(xl1.GetAttribute("name")==userName)
			{
				foreach(XmlElement xl2 in xl1.ChildNodes)
				{
					if(int.Parse(xl2.GetAttribute("id"))==stage)
					{
						xl2.SetAttribute("score", score.ToString());
					}
					
				}
			}
		}
		xml.Save(xmlPath);
	}
	//获取一个用户的信息，返回的是一个用户对象
	public static User getUser(string userName)
	{
		if (userName == null || userName.Equals ("")) 
			return null;
		if (!File.Exists (xmlPath)) 
			init ();
		if (!isUserExist (userName))
			return null;
		User user = new User ();
		user.Name = userName;
		XmlDocument xml = new XmlDocument ();
		xml.Load(xmlPath);
		XmlNodeList xmlNodeList = xml.SelectSingleNode ("users").ChildNodes;
		foreach (XmlElement xl1 in xmlNodeList) {
			if (xl1.GetAttribute ("name") == userName) {
				int totalScore = 0;
				foreach(XmlElement xl2 in xl1.ChildNodes)
				{
					user.setScore(int.Parse(xl2.GetAttribute("id")),int.Parse(xl2.GetAttribute("score")));
					totalScore += int.Parse(xl2.GetAttribute("score"));
				}
				user.TotalScore = totalScore;
			}
		}
		return user;
	}
	//获取所有用户信息，返回的是一个用户对象的list
	public static List<User> getAllUsers()
	{
		if (!File.Exists (xmlPath)) 
			init ();
		List<User> userList = new List<User> ();
		XmlDocument xml = new XmlDocument ();
		xml.Load(xmlPath);
		XmlNodeList xmlNodeList = xml.SelectSingleNode ("users").ChildNodes;
		foreach (XmlElement xl1 in xmlNodeList) {
			User user = new User ();
			user.Name = xl1.GetAttribute ("name");
			int totalScore = 0;
			Debug.Log(Application.persistentDataPath);
			foreach(XmlElement xl2 in xl1.ChildNodes)
			{
				user.setScore(int.Parse(xl2.GetAttribute("id")),int.Parse(xl2.GetAttribute("score")));
				totalScore += int.Parse(xl2.GetAttribute("score"));
			}
			user.TotalScore = totalScore;
			userList.Add(user);
		}
		return userList;
	}
	//获取有序的前多少名玩家的list
	public static List<User> getTopUsers(int n)
	{	
		List<User> list = getAllUsers();
		IEnumerable<User> query = from items in list 
			orderby items.TotalScore descending select items; 
		int i = 0;
		list = new List<User> ();
		foreach (User user in query)
		 {
			i++;
			if(i>n) break;
			Debug.Log(user.Name+":"+user.TotalScore);
			list.Add(user);
		 }
		return list;
	}
	//获取某个关卡有序的前多少名玩家的list
	public static List<User> getTopUsers(int n,int stage)
	{	
		if (stage<=0) 
			throw new Exception ("关卡异常:" + stage);
		List<User> list = getAllUsers();
		IEnumerable<User> query = from items in list 
			orderby items.getScoreForStage(stage) descending select items; 
		int i = 0;
		list = new List<User> ();
		foreach (User user in query)
		{
			i++;
			if(i>n) break;
			Debug.Log(user.Name+":"+user.TotalScore);
			list.Add(user);
		}
		return list;
	}
}
