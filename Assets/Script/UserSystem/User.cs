using System;
using System.Collections;
using System.Collections.Generic;
/********************************************************************************

** 作者： tianhao

** 创始时间：2016-1-31

** 描述：

**    封装用户信息

*********************************************************************************/
public class User{
	private string name;
	private Dictionary<int,int> score=new Dictionary<int, int>();
	private int totalScore;

	public string Name
	{
		get { return name; }
		set { name = value; }
	}
	public Dictionary<int,int> Score
	{
		get { return score; }
	}
	public int TotalScore
	{
		get { return totalScore; }
		set { totalScore = value; }
	}
	//指定这个用户某个关卡的分数
	public void setScore(int stage,int score)
	{
		if (stage<=0) 
			throw new Exception ("关卡异常:" + stage);
		if (score<0) 
			throw new Exception ("得分信息错误："+score);
		if (Score.ContainsKey (stage)) 
		{
			Score[stage] = score;
			return;
		}
		Score.Add (stage,score);
	}
	public void delScore(int stage)
	{
		if (stage<=0) 
			throw new Exception ("关卡异常:" + stage);
		if (Score.ContainsKey (stage)) 
		{
		Score.Remove (stage);
		}
	}
	public int getScoreForStage(int stage)
	{
		if (stage<0) 
			throw new Exception ("关卡异常:" + stage);
		if (Score.ContainsKey (stage)) 
		{
			return Score[stage];
		}
		return 0;
	}
}
