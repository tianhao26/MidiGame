using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using imidi_in;
//using midigame;
//using System.Windows.Forms;

//namespace midigame
//{

	

    /// <summary>
    /// 链接MIDI设备并解析MIDI键盘码
    /// </summary>
    class AnalysisSignal
    {
        MidiKeyCode keyCode;                                                //当前按下的键的键值
        static InputPort ip;
		String perString;
		public ChordKeycode chord = new ChordKeycode();                        //如果是和弦的话的和弦值               
		ChordsUtil chordUtil;
		public List<MidiKeyCode> midiKeyCodeList = new List<MidiKeyCode>();   //当前处于按下的状态的键的列表
		int keyBlank = 200;                                                  //设置和弦默认音符间的间隔时间  （比如规定一个和弦必须在1s内完成）
		public bool isChord = false;                                          //判断当前时间内有没有和弦被按下
		String  fragmentSigle ; 
		String  continueCode ; 
		int centerC =60;                                                       //设置中央C 的值
		List<MidiKeyCode> isChordKeyCodeList = new List<MidiKeyCode>();      //一段时间内按下过的键的列表，用于判断是否为和弦

		public string[] sigles; 

	
		public MidiKeyCode KeyCode
		{
			get { return keyCode; }
		}

		void theout(object source, System.Timers.ElapsedEventArgs e)//每秒执行一次
		{
			initIsChordKeyCodeList ();
			isChord = false;
		} 



		void initIsChordKeyCodeList()
		{
			isChordKeyCodeList.Clear ();
			//foreach (MidiKeyCode i in isChordKeyCodeList) 
		//	{
			//	i.
			//}
		}

		public int KeyBlank
		{
			get { return keyBlank; }
			set { keyBlank = value; }
		}

		public int CenterC
		{
			get { return centerC; }
			set { 
				chordUtil.CenterC = value;
				chordUtil.initChordList();
				centerC = value; 
			}
		}

		public AnalysisSignal()
		{
			System.Timers.Timer t = new System.Timers.Timer(1000);//实例化Timer类，设置间隔时间为10000毫秒；
			t.Elapsed += new System.Timers.ElapsedEventHandler(theout);//到达时间的时候执行事件；
			t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
			t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；

			chordUtil = new ChordsUtil();
		}

		public void analysisMidiKey(string sigleStr){

			MidiKeyCode recentKeyCode = new MidiKeyCode ();
			continueCode = isMidiSignal (sigleStr);
			sigles = continueCode.Split('d');


			for(int i=0;i<sigles.Length-1;i++)
			{
				sigles[i]+="d";
			}
			for(int j = 0;j<sigles.Length-1;j++)
			{
				recentKeyCode = analysisMidiKeyCode (sigles[j]);
				
				
				if (!recentKeyCode.IsRrelease) 
				{

					midiKeyCodeList.Add (recentKeyCode);
					isChordKeyCodeList.Add (recentKeyCode);

						keyCode = recentKeyCode;
					    if(chordUtil.isChord(isChordKeyCodeList)){
							isChord = true;
							chord = chordUtil.chord;
						    initIsChordKeyCodeList();
						}else{
							isChord = false;
							chord = null;
						}
					//}
					
				} else {
					foreach(MidiKeyCode i in midiKeyCodeList)
					{
						if(i.KeyCode == recentKeyCode.KeyCode)
						{
							midiKeyCodeList.Remove(i);
							break;
						}
					}

				}
			}
			
			



		}



	/// <summary>
	/// 判断是否与前一个字符串重复
	/// </summary>
	/// <returns><c>true</c>, if equel per string was ised, <c>false</c> otherwise.</returns>
	/// <param name="Str">String.</param>
	public bool isEquelPerString(String Str){
		if (perString == Str) {
						return true;
				} else {
			perString = Str;
			return false;
				}
		}


	/// <summary>
	/// Is the midi signal.
	/// </summary>
	/// <returns><c>true</c>, if midi signal was ised, <c>false</c> if midi signal was not ised.</returns>
	/// <param name="sigleStr">Sigle string.</param>
	String isMidiSignal(String sigleStr){
			if (fragmentSigle != null) 
			{
				fragmentSigle += sigleStr;
				sigleStr = fragmentSigle;
				fragmentSigle = null;

			}
			if(sigleStr.LastIndexOf("d") != (sigleStr.Length-1))
			{
				fragmentSigle = sigleStr.Substring(sigleStr.LastIndexOf("d")+1);
				sigleStr = sigleStr.Substring(0,sigleStr.Length - fragmentSigle.Length);
			}
			return sigleStr;
		}


	
        /// <summary>
        /// 解析MIDI码，获取midiSigle类型的对象
        /// </summary>
        /// <param name="sigleStr">成为焦点的控件获取到的键盘码，string类型</param>
        /// <returns>midiSigle</returns>
        MidiKeyCode analysisMidiKeyCode(string sigleStr)
        {
		    
            keyCode = new MidiKeyCode();
            long time_ticks ;
            int midi_cmd = 144 ;
            int key;
            int strength = 0;

			string[] sArray = sigleStr.Split('a');
			time_ticks = Convert.ToInt64(sArray[0]);
			sigleStr = sArray[1];
			
			string[] sArray1 = sigleStr.Split('b');
			midi_cmd = Convert.ToInt32(sArray1[0]);
			sigleStr = sArray1[1];
			
			string[] sArray2 = sigleStr.Split('c');
			key = Convert.ToInt32(sArray2[0]);
			sigleStr = sArray2[1];
			
			string[] sArray3 = sigleStr.Split('d');
			strength = Convert.ToInt32(sArray3[0]);
			
			
			keyCode.Time_ticks = time_ticks;
			keyCode.KeyCode = key;
			keyCode.Strength = strength;
				if(midi_cmd==144){
					keyCode.IsRrelease = false;
				}else{
					keyCode.IsRrelease = true;
				}


            return keyCode;
        }




        /// <summary>
        /// 打开MIDI设备
        /// </summary>
        /// <param name="modelId">用户选择的MIDI设备编号</param>
        public void midiStart(int modelId)
        {
            ip = new InputPort();
            ip.Open(modelId);
            ip.Start(); 
        }

        /// <summary>
        /// 关闭MIDI设备
        /// </summary>
        public void midiClose()
        {
            ip.Stop();
            ip.Close();
        }

        /// <summary>
        /// 解析键盘码，获取midiSigle类型的对象
        /// </summary>
        /// <param name="cmd">按下1或抬起按键0</param>
        /// <param name="keyId">按下键的id</param>
        /// <returns>midiSigle</returns>
        MidiKeyCode analysisKeySigle(int cmd,String keyId)
        {
            
            keyCode = new MidiKeyCode();
            long time_ticks;
            int strength = 0;

            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            time_ticks = Convert.ToInt64(ts.TotalSeconds);

            keyCode.Time_ticks = time_ticks;
            Console.WriteLine("MM"+keyId);
            keyCode.KeyCode = Convert.ToInt32(keyId[0])-65;
            Console.WriteLine("WW" + keyCode.KeyCode);
            keyCode.Strength = strength;
            if(cmd == 1)
            {
                keyCode.IsRrelease = false;
            }
            else if(cmd == 0)
            {
                keyCode.IsRrelease = true;
            }
            return keyCode;
        }

    }
//}