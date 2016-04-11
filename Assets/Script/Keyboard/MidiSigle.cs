using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    class MidiSigle
    {
        private long time_ticks;
        private int midi_cmd;
        private int key;
        private int strength;

        public long Time_ticks
        {
            get { return time_ticks; }
            set { time_ticks = value; }
        }
        
        public int Midi_cmd
        {
            get { return midi_cmd; }
            set { midi_cmd = value; }
        }
        

        public int Key
        {
            get { return key; }
            set { key = value; }
        }

        public int Strength
        {
            get { return strength; }
            set { strength = value; }
        }
    }
