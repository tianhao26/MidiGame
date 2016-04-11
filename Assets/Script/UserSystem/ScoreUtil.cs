using UnityEngine;
using System;
/********************************************************************************

** 作者： tianhao

** 创始时间：2016-2-1

** 描述：

**      封装计分操作，该类的实例是同步的，意即同一时间内只有一个线程可以访问该类的某个实例

*********************************************************************************/
[System.Runtime.Remoting.Contexts.Synchronization]
public class ScoreUtil : System.ContextBoundObject {
	private int wrong = 0;//当前错误的个数
	private int correct = 0;//当前正确命中对象个数
	private double perScore = 0;//当前命中游戏对象得分
	private double score = 0;//当前总得分
	private double forceRation = 0;//当前命中力度匹配程度

	public int Correct
	{
		get{ return correct; }
		set{ correct = value; }
	}

	public int Wrong
	{
		get{ return wrong; }
		set{ wrong = value; }
	}

	public double PerScore
	{
		get{ return perScore; }
		set{ perScore = value; }
	}

	public double Score
	{
		get{ return score; }
		set{ score = value; }
	}

	public double ForceRation
	{
		get{ return forceRation; }
		set{ forceRation = value; }
	}

	//初始化方法，使用该对象前先调用
	public void init()
	{
		perScore = 0;
		score = 0;
		correct = 0;
	}

	//增加总分
	public void addScore(double score)
	{
		if (score < 0)
			throw new Exception ("分数异常：" + score);
		Score+=score;
		//Debug.Log("得分，当前总分数："+Score);
	}

	//增加一次正确命中
	public void addCorrect()
	{
		if (correct < 0)
			throw new Exception ("当前正确命中数量异常：" + correct);
		correct++;
	}

	//增加一次错误命中
	public void addWrong()
	{
		if (wrong < 0)
			throw new Exception ("当前正确命中数量异常：" + wrong);
		wrong++;
	}

	//按键错误发生时的回调函数
	public void defeat()
	{
		addWrong ();
		//minusScore(1);
	}

	//扣分的方法
	public void minusScore(double score){
		if (score < 0)
			throw new Exception ("分数异常：" + score);
		Score-=score;
	}

	//一次命中游戏对象时调用，计算当前命中得分
	//force为此次命中力度值，target为目标力度值，threshold为误差阀值即命中力度与目标力度最大可相差多少
	public void bingo(int force,int target,int threshold)
	{
		if (threshold <= 0)
			throw new Exception ("误差阀值异常：" + threshold);
		addCorrect ();
		ForceRation = getForceRation (force, target, threshold);
		PerScore = 1 + ForceRation;
		//Debug.Log("命中，当前命中分数："+PerScore);
		addScore (perScore);
	}

	//计算正确率
	public float getCorrectRation(int total)
	{
		if(total < 0) throw new Exception("目前已经生成的游戏对象数量异常："+total);
		if (total == 0)
						return 0;
		Debug.Log (total);
		return Correct / total;
	}

	//计算力度匹配程度
	public double getForceRation(int force,int target,int threshold)
	{
		if (threshold <= 0)
						throw new Exception ("误差阀值异常：" + threshold);
		if (Math.Sqrt (Math.Pow ((force - target), 2)) / threshold > 1)
						return 0;
		return 1 - Math.Sqrt (Math.Pow ((force - target), 2)) / threshold;
	}

	//关卡结束时调用，判断当前分数是否达到及格线
	public bool isPassStage(int passLine)
	{
		if (passLine < 0)
						throw new Exception ("及格线异常：" + passLine);
		if (score >= passLine)
						return true;
		return false;
	}
}
