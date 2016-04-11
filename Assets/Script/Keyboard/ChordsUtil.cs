using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using imidi_in;
using System.Xml;
using System.IO;
//using midigame;
//using System.Windows.Forms;

//namespace midigame
//{
	public class ChordsUtil
	{
		public ChordKeycode chord = new ChordKeycode();
		public List<ChordKeycode> chordCodeList; 
		public int  centerC =72; 
		//public  string xmlPath ;


		public string  test ;


		public int CenterC
		{
			get { return centerC; }
			set { centerC = value; }
		}


		public ChordsUtil ()
		{
			chordCodeList = new List<ChordKeycode> ();
			initChordList ();
		}


		int keyToCode(string key)
		{
			if (key == "C" || key == "c")
			{
				return 0;
			}
			if (key == "D" || key == "d")
			{
				return 2;
			}
			if (key == "E" || key == "e")
			{
				return 4;
			}
			if (key == "F" || key == "f")
			{
				return 5;
			}
			if (key == "G" || key == "g")
			{
				return 7;
			}
			if (key == "A" || key == "a")
			{
				return 9;
			}
			if (key == "B" || key == "b")
			{
				return 11;
			}
			return 999;
		}

		public void initChordList()
		{
		

			//xmlPath = System.Environment.CurrentDirectory+"\\Assets\\chords.xml"; 
			XmlDocument doc = new XmlDocument();
			doc.Load(@"Assets\chords.xml");

			// 得到根节点bookstore
			XmlNode xn = doc.SelectSingleNode("tunes");
			 // 得到根节点的所有子节点
		    XmlNodeList xn0 = xn.ChildNodes; 
			foreach (XmlNode xn1 in xn0)
			{
				XmlElement xe1 = (XmlElement)xn1;

				//chord.Tune = xe1.GetAttribute("name").ToString();
				//chord.WithSound = chord.Tune + centerC;


				XmlNodeList xntt = xn1.ChildNodes; 
				foreach (XmlNode xn2 in xntt)
				{

					ChordKeycode chordtmp = new ChordKeycode(); 

					XmlNodeList xnth = xn2.ChildNodes; 
					foreach (XmlNode xn3 in xnth)
					{
						//List<int> chordskeysCode = new List<int>();  //和弦包含的键
						XmlElement xe3 = (XmlElement)xn3;
						chordtmp.ChordskeysCode.Add(Convert.ToInt32(xe3.GetAttribute("code")));
					}

					XmlElement xe2 = (XmlElement)xn2;
					chordtmp.KindString = xe2.GetAttribute("name").ToString();
					chordtmp.Kind = xe2.GetAttribute("code").ToString();
					chordtmp.Tune = xe1.GetAttribute("name").ToString();
					chordtmp.WithSound.KeyCode = centerC+keyToCode(chordtmp.Tune);
					chordtmp.ChordName = chordtmp.Tune+chordtmp.Kind;
					chordCodeList.Add(chordtmp);
				}
			}	
		}



		public bool isChord(List<MidiKeyCode> midiKeyCodeList)
		{		


			if(midiKeyCodeList.Count<3)
			{
				return false;
			}


			List<int> allKey = new List<int>(); 
			List<int>rootKey = new List<int>();          //中央c右侧的音认为是根音
			//bool flag1 = false;
			List<int> chordKey = new List<int>();          //中央c侧的音认为是

			MidiKeyCode midirootKey = new MidiKeyCode();    
			List<MidiKeyCode> midichordKey = new List<MidiKeyCode>();    
			midichordKey = midiKeyCodeList;
			
			for (int i=0;i<midiKeyCodeList.Count;i++)
			{	
	
				allKey.Add(midiKeyCodeList[i].KeyCode);
				if(midiKeyCodeList[i].KeyCode>=centerC)
				{

					rootKey.Add(midiKeyCodeList[i].KeyCode);
					if(midichordKey.Count>0)
					{
						midichordKey.Remove(midiKeyCodeList[i]);
					}
				}
			}

			if(midichordKey.Count<3)
			{
				return false;
			}
			
			chordKey = allKey.Except(rootKey).ToList();


			bool flag = true;
			foreach (ChordKeycode i in chordCodeList)
			{	
				//MessageBox.Show("十si个和铉!");	
				//MessageBox.Show(tmp.Count.ToString());
				if(i.ChordskeysCode.Count!=chordKey.Count){
					//MessageBox.Show("Count!=");
					flag = false;
				}else{
					flag = true;
					for(int k = 0 ;k<i.ChordskeysCode.Count;k++)
					{
						if(i.ChordskeysCode[k]>=12){
							i.ChordskeysCode[k] = i.ChordskeysCode[k]%12;
						}
					}
					for(int j=0;j<i.ChordskeysCode.Count;j++)
					{
						//MessageBox.Show(i.ChordskeysCode[j].ToString()+"OOOOO"+chordKey[j]);
						if(!i.ChordskeysCode.Contains(chordKey[j]%12))
						{
							flag = false;
							break;
						}
					}
				}

				if(flag)
				{
					chord = i;
					chord.Chordskeys = midichordKey;
					chord.ChordskeysCode = chordKey;
					//MessageBox.Show(midichordKey[0].KeyCode.ToString());
					return flag;
				}

			}

			return flag;	  	
		}
	}
//}

