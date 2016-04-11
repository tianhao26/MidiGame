using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System;
/********************************************************************************

** 作者： tianhao

** 创始时间：2016-3-2

** 描述：

**    关卡逻辑

*********************************************************************************/
public static class LevelSystem 
{
	private static string xmlPath = Application.streamingAssetsPath + "/levels.xml";
	// 加载Xml文件 
	public static List<Level> LoadLevels()
	{
		//创建Xml对象
		XmlDocument xmlDoc = new XmlDocument();
		//如果本地存在配置文件则读取配置文件
		//否则在本地创建配置文件的副本
		//为了跨平台及可读可写，需要使用Application.persistentDataPath
		//string filePath = Application.dataPath + "/levels.xml";
		if (!IOUntility.isFileExists (xmlPath)) {
			xmlDoc.LoadXml (((TextAsset)Resources.Load ("levels")).text);
			IOUntility.CreateFile (xmlPath, xmlDoc.InnerXml);
		} else {
			xmlDoc.Load(xmlPath);
		}
		XmlElement root = xmlDoc.DocumentElement;
		XmlNodeList levelsNode = root.SelectNodes("/levels/level");
		//初始化关卡列表
		List<Level> levels = new List<Level>();
		foreach (XmlElement xe in levelsNode) 
		{
			Level l=new Level();
			l.ID=xe.GetAttribute("id");
			l.Name=xe.GetAttribute("name");
			User user = UserUtil.getUser(PlayerPrefs.GetString("currentUser"));
			int lastLevelId = int.Parse(l.ID) - 1;
			if(user.getScoreForStage(lastLevelId)>=int.Parse(xe.GetAttribute("unlockScore"))){
				l.UnLock=true;
			}else{
				l.UnLock=false;
			}
			
			levels.Add(l);
		}
		
		return levels;
	}
	
	// 设置某一关卡的状态
	// <param name="name">关卡名称</param>
	// <param name="locked">是否解锁</param>
	public static void SetLevels(string name,bool unlock)
	{
		//创建Xml对象
		XmlDocument xmlDoc = new XmlDocument();
		string filePath=Application.persistentDataPath + "/levels.xml";
		xmlDoc.Load(filePath);
		XmlElement root = xmlDoc.DocumentElement;
		XmlNodeList levelsNode = root.SelectNodes("/levels/level");
		foreach (XmlElement xe in levelsNode) 
		{
			//根据名称找到对应的关卡
			if(xe.GetAttribute("name")==name)
			{
				//根据unlock重新为关卡赋值
				if(unlock){
					xe.SetAttribute("unlock","1");
				}else{
					xe.SetAttribute("unlock","0");
				}
			}
		}
		//保存文件
		xmlDoc.Save (filePath);
	}
}
