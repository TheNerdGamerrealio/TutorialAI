using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;
using System.Diagnostics;

namespace Tutorial_AI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SpeechSynthesizer s = new SpeechSynthesizer();
        SpeechRecognitionEngine sr = new SpeechRecognitionEngine();
        PromptBuilder pb = new PromptBuilder();

        private void Form1_Load(object sender, EventArgs e)
        {
            s.SelectVoiceByHints(VoiceGender.Female);
            Choices list = new Choices();
            list.Add(File.ReadAllLines(@"File path goes here"));
            //Add a file path here
            Grammar gm = new Grammar(new GrammarBuilder(list));

            try
            {
                sr.RequestRecognizerUpdate();
                sr.LoadGrammar(gm);
                sr.SpeechRecognized += Sr_SpeechRecognized;
                sr.SetInputToDefaultAudioDevice();
                sr.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch
            {
                return;
            }
        }

        public void Say(string phrase)
        {
            s.SpeakAsync(phrase);
            wake = false;
        }

        private bool wake = false;

        private void Sr_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string speechSaid = e.Result.Text;

            if (speechSaid == "hey anna")
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"File path goes here");
                //Add a file path here
                player.Play();
                wake = true;
            }

            if (wake)
            {
                switch (speechSaid)
                {
                    case ("hello"):
                        Say("hi");
                        break;

                    case ("how are you doing"):
                        Say("good, how about you");
                        break;

                    case ("open google"):
                        Say("opening google");
                        Process.Start("https://www.google.com");
                        break;

                    case ("open minecraft"):
                        Say("opening minecraft java edition");
                        Process.Start(@"C:\Program Files (x86)\Minecraft Launcher\MinecraftLauncher.exe");
                        break;

                    case ("close"):
                        Say("closing program");
                        SendKeys.Send("%{F4}");
                        break;

                    case ("exit"):
                        s.Speak("closing down sir, thank you for using anna ai");
                        Application.Exit();
                        break;
                }
            }

        }
    }
}
