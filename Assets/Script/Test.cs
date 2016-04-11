using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {
	public static GameObject note1;
	public static GameObject note2;
	public static GameObject note3;
	public ScoreUtil scoreUtil = new ScoreUtil ();
	public Material material;
	private float ratioScaleTempH = Screen.height / 960.0f;
	private float ratioScaleTemp = Screen.width / 540.0f;
	private int count = 0;
	private string content = null;
	private Midi midi;
	public UnityMidiSynth midiPlayer;
	private AnalysisSignal analysisSignal;
	private MidiKeyCode midikeycode;
	private ChordKeycode midichordcode;
	private ArrayList goList = new ArrayList();
	private ArrayList nullList = new ArrayList();
	public AudioClip theSound;
	public GameObject[] notes =
	{
		note1,
		note2,
		note3
	};
	float newfire;
	// Use this for initialization
	void Start () {
		scoreUtil.init ();
		string xmlPath = Application.streamingAssetsPath + "/" + PlayerPrefs.GetString("selectedLevel") + ".xml";
		midi = Midi.getMidi (xmlPath);
		midiPlayer.StopAll();
		midiPlayer.Play(PlayerPrefs.GetString("selectedLevel")+".mid");

		analysisSignal = new AnalysisSignal ();
		analysisSignal.midiStart (0);
		midikeycode = new MidiKeyCode ();
		midichordcode = new ChordKeycode ();
	}
	// Update is called once per frame
	void Update () {
		//此处判断游戏关卡结束条件,关卡对象总数暂时写100个，后续根据midi文件生成
		//if(Time.time>midi.TotalLength/1000+5)
		//{
		//	Application.LoadLevel ("end");
		//	Destroy(this);
		//	analysisSignal.midiClose();
		//}
		content = "实时得分:"+scoreUtil.PerScore.ToString()+"\r\n";
		content += "力度匹配程度:"+scoreUtil.ForceRation.ToString()+"\r\n";
		content += "总得分:"+scoreUtil.Score.ToString()+"\r\n";
		content += "当前正确率:"+scoreUtil.Correct+"/"+count+"\r\n";

		if (count < midi.Count) {
			example(midi.NoteDelta[count]/1000f);
		}
		foreach(int index in nullList){
			goList.RemoveAt(index);
		}
		nullList = new ArrayList ();
		/*if (Input.GetKeyDown (KeyCode.W)) {
			foreach(GameObject go in goList){
				if(go==null){
					nullList.Add(goList.IndexOf(go));
				}else{
					go.BroadcastMessage ("ReceiveBroadcastMessage","C5");
				}
			}
		}*/
		if (Input.anyKey)
		{
			if (Input.GetKeyDown (KeyCode.W)) {
				foreach(GameObject go in goList){
					if(go==null){
						nullList.Add(goList.IndexOf(go));
					}else{
						go.BroadcastMessage ("ReceiveBroadcastMessage","C5");
					}
				}
			}
			if(!analysisSignal.isEquelPerString(Input.inputString) && Input.inputString!=""){
				analysisSignal.analysisMidiKey(Input.inputString);
				midikeycode = analysisSignal.KeyCode;
				if(midikeycode!=null&&midikeycode.KeyCode!=null&&Midi.getNote(midikeycode.KeyCode)!=null){
					foreach(GameObject go in goList){
						if(go==null){
							nullList.Add(goList.IndexOf(go));
						}else{
							go.BroadcastMessage ("ReceiveBroadcastMessage",Midi.getNote(midikeycode.KeyCode));
						}
					}
				}
				Debug.Log("键值："+midikeycode.KeyCode);
				//Debug.Log("键是否为释放状态："+midikeycode.IsRrelease);
				//Debug.Log("按键力度："+midikeycode.Strength);
				//Debug.Log("按键时间戳："+midikeycode.Time_ticks);
			}
		}
	}
	void  example(float nextfire)
	{
		if (Time.time-newfire > nextfire) 
		{
			//Debug.Log(Time.time-newfire);
			//Vector3 x = Camera.main.ScreenToWorldPoint(new Vector3 (0,0,0));
			//Vector3 y = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width,Screen.height,0));
			Vector3 position = new Vector3 (Random.Range (-5,5), 5.2f, 0);
			//Debug.Log(position);
			GameObject go = Instantiate (notes[0], position, Quaternion.identity) as GameObject;
			//Debug.Log(midi.Note[count].ToString());
			go.GetComponent<Move>().note = midi.Note[count];
			go.GetComponent<Move>().force = midi.Force[count].ToString();
			go.renderer.material.mainTexture = Resources.Load(midi.Note[count].ToString()) as Texture;
			//go.transform.rotation=Quaternion.AngleAxis(180,Vector3.up);
			goList.Add(go);
			//go.transform.localScale= new Vector3(2,2,2);
			newfire = Time.time;
			count++;
		}
	}
	void OnGUI()
	{
		GUI.color = Color.black;
		GUI.Label(new Rect(100*ratioScaleTemp, 100*ratioScaleTempH, 800*ratioScaleTemp, 600*ratioScaleTempH), 
		          content);
		foreach(int index in nullList){
			goList.RemoveAt(index);
		}
		nullList = new ArrayList ();
		/*if (Event.current.isKey)
		{  
			if (Input.GetKeyDown (KeyCode.W)) {
				foreach(GameObject go in goList){
					if(go==null){
						nullList.Add(goList.IndexOf(go));
					}else{
						go.BroadcastMessage ("ReceiveBroadcastMessage","C5");
					}
				}
			}
			if(!analysisSignal.isEquelPerString(Input.inputString) && Input.inputString!=""){
				analysisSignal.analysisMidiKey(Input.inputString);
				midikeycode = analysisSignal.KeyCode;
				if(midikeycode!=null&&midikeycode.KeyCode!=null&&Midi.getNote(midikeycode.KeyCode)!=null){
					foreach(GameObject go in goList){
						if(go==null){
							nullList.Add(goList.IndexOf(go));
						}else{
						go.BroadcastMessage ("ReceiveBroadcastMessage",Midi.getNote(midikeycode.KeyCode));
						}
					}
				}
				Debug.Log("键值："+midikeycode.KeyCode);
				//Debug.Log("键是否为释放状态："+midikeycode.IsRrelease);
				//Debug.Log("按键力度："+midikeycode.Strength);
				//Debug.Log("按键时间戳："+midikeycode.Time_ticks);
			}
		}*/
	}
	//在物体可以被击中的有效区域画一条线
	void OnPostRender() {
		if (!material) 
		{
			Debug.Log("材质资源未赋值！");
			return;
		}
		material.SetPass (0);
		GL.LoadOrtho ();
		GL.Begin (GL.LINES);
		Vector3 v = Camera.mainCamera.WorldToScreenPoint(new Vector3(0,-1.5f,0));
		GL.Vertex (new Vector3(0,v.y/Screen.height,0));
		GL.Vertex (new Vector3(1,v.y/Screen.height,0));
		GL.End ();
	}
	//播放音效
	public void PlaySound()
	{	
		GameObject.Find ("Main Camera").audio.PlayOneShot(theSound);
	}
}
