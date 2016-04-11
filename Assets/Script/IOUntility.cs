using UnityEngine;
using System.Collections;
using System.IO;
/********************************************************************************

** 作者： tianhao

** 创始时间：2016-3-2

** 描述：

**    文件操作工具类

*********************************************************************************/
public static class IOUntility
{

	// 创建文件夹    
	public static void CreateFolder(string path)
	{
		if(!Directory.Exists(path)){
			Directory.CreateDirectory(path);
		}
	}

	// 创建文件
	// <param name="filePath">文件路径</param>
	// <param name="content">文件内容</param>
	public static void CreateFile(string filePath,string content)
	{
		//文件流
		StreamWriter writer;
		//判断文件目录是否存在
		//不存在则先创建目录
		Debug.Log (filePath);
		string folder = filePath.Substring (0, filePath.LastIndexOf ("//"));
		CreateFolder (folder);
		//如果文件不存在则创建，存在则追加内容
		FileInfo file=new FileInfo(filePath);
		if(!file.Exists){
			writer=file.CreateText();
		}else{
			file.Delete();
			writer=file.CreateText();
		}
		
		//写入内容
		writer.Write(content);
		writer.Close();
		writer.Dispose();
	}

	// 判断文件是否存在
	// <param name="path">文件路径</param>
	public static bool isFileExists(string path)
	{
		FileInfo file=new FileInfo(path);
		return file.Exists;
	}
	
	public static void DeleteFile(string fileName)
	{
		if(!File.Exists(fileName)) return;
		File.Delete(fileName);
	}
}
