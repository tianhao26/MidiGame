using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System;
/********************************************************************************

** 作者： tianhao

** 创始时间：2016-3-1

** 描述：

**    封装midi文件解析出的信息，用以生成游戏关卡

*********************************************************************************/
public class Midi {
	private long totalLength;//总时长,暂定以毫秒为单位
	private int count;//音符总数
	private string[] note;//每个音符名
	private long[] noteDelta;//每个音符之间时间间隔
	private int[] force;//每个音符需要的力度

	public long TotalLength
	{
		get { return totalLength; }
		set { totalLength = value; }
	}

	public int Count
	{
		get { return count; }
		set { count = value; }
	}

	public string[] Note
	{
		get { return note; }
		set { note = value; }
	}

	public long[] NoteDelta
	{
		get { return noteDelta; }
		set { noteDelta = value; }
	}

	public int[] Force
	{
		get { return force; }
		set { force = value; }
	}

	//根据文件生成对象
	public static Midi getMidi(string xmlPath)
	{
		Midi midi = new Midi ();
		XmlDocument xml = new XmlDocument ();
		xml.Load(xmlPath);
		XmlNode root = xml.SelectSingleNode("music");
		midi.TotalLength = long.Parse (root.Attributes ["lenth"].Value.ToString());
		midi.Count = int.Parse(root.Attributes["number"].Value.ToString());
		midi.Note = new string[midi.Count];
		midi.NoteDelta = new long[midi.Count];
		midi.Force = new int[midi.Count];
		XmlNodeList xmlNodeList = root.ChildNodes;
		int count = 0;
		foreach (XmlElement xl1 in xmlNodeList) {
			midi.Note[count] = getNote(xl1.GetAttribute("code"));
			midi.Force[count] = Convert.ToInt32(xl1.GetAttribute("force"), 16);
			midi.NoteDelta[count] = long.Parse(xl1.GetAttribute("delta"));
			count++;
		}
		return midi;
	}

	private static string getNote(string src)
	{
		string result = null;
		int value = Convert.ToInt32 (src, 16);
		int N = value % 12;
		int O = value / 12;
		switch (N)
		{
		case 0 :
			result = "C";
			break;
		case 1 :
			result = "#C";
			break;
		case 2 :
			result = "D";
			break;
		case 3 :
			result = "#D";
			break;
		case 4 :
			result = "E";
			break;
		case 5 :
			result = "F";
			break;
		case 6 :
			result = "#F";
			break;
		case 7 :
			result = "G";
			break;
		case 8 :
			result = "#G";
			break;
		case 9 :
			result = "A";
			break;
		case 10 :
			result = "#A";
			break;
		case 11 :
			result = "B";
			break;
		default :
			throw new Exception("Wrong note code!");
			break;
		}
		result += O;
		return result;
	}

	public static string getNote(int src)
	{
		string result = null;
		int N = src % 12;
		int O = src / 12;
		switch (N)
		{
		case 0 :
			result = "C";
			break;
		case 1 :
			result = "#C";
			break;
		case 2 :
			result = "D";
			break;
		case 3 :
			result = "#D";
			break;
		case 4 :
			result = "E";
			break;
		case 5 :
			result = "F";
			break;
		case 6 :
			result = "#F";
			break;
		case 7 :
			result = "G";
			break;
		case 8 :
			result = "#G";
			break;
		case 9 :
			result = "A";
			break;
		case 10 :
			result = "#A";
			break;
		case 11 :
			result = "B";
			break;
		default :
			throw new Exception("Wrong note code!");
			break;
		}
		result += O;
		return result;
	}
}
