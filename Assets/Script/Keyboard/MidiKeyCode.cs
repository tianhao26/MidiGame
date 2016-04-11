using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//namespace midigame
//{
    public class MidiKeyCode
    {
        private long time_ticks;
        private int keyCode;
        private int strength;
		private bool isRrelease;

		public bool IsRrelease
		{
			get { return isRrelease; }
			set { isRrelease = value; }
		}


        public long Time_ticks
        {
            get { return time_ticks; }
            set { time_ticks = value; }
        }
        
		public int KeyCode
		{
			get { return keyCode; }
			set { keyCode = value; }
        }
        

        public int Strength
        {
            get { return strength; }
            set { strength = value; }
        }
    }


	public class ChordKeycode
	{
		List<int> chordskeysCode = new List<int>();              //和弦包含的键值
		List<MidiKeyCode> chordskeys = new List<MidiKeyCode>();  //和弦包含的键
		String chordName;                           			 //和弦名C
		MidiKeyCode withSound = new MidiKeyCode ();		   	     //和弦根音
		String chordKeyCodeString;                 			 	 //预留字段
		String tune ;                               			 //和弦调
		String kind;                                			 //和弦种类（m）
		String kindString;                          			 //和弦种类中文示意（小三和旋）
		String power;                               			 //完成此和铉力度
		long time_ticks; 							    	   //完成此和铉时的时间戳

		public long Time_ticks
		{
			get { return time_ticks; }
			set { time_ticks = value; }
		}

		public List<MidiKeyCode> Chordskeys
		{
			get { return chordskeys; }
			set { chordskeys = value; }
		}

		public String Power
		{
			get { return power; }
			set { power = value; }
		}

		public String KindString
		{
			get { return kindString; }
			set { kindString = value; }
		}


		public String Kind
		{
			get { return kind; }
			set { kind = value; }
		}

		public String Tune
		{
			get { return tune; }
			set { tune = value; }
		}

		public String ChordKeyCodeString
		{
			get { return chordKeyCodeString; }
			set { chordKeyCodeString = value; }
		}


		public void removeAllChord()
		{
			chordskeysCode.Clear();
		}


		public List<int> ChordskeysCode
		{
			get { return chordskeysCode; }
			set { chordskeysCode = value; }
		}

		public String ChordName
		{
			get { return chordName; }
			set { chordName = value; }
		}

		public MidiKeyCode WithSound
		{
			get { return withSound; }
			set { withSound = value; }
		}
		
	}
//}
