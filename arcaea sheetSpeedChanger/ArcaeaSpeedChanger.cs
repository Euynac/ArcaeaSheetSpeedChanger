using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using Euynac.WinformUI;
using Euynac.Utility.SoundSpeedChange;
using Euynac.Utility;
using System.Runtime.InteropServices;
using System.Threading;
using Arcaea.Song.SongListJson;

namespace ArcaeaSpeedChanger
{

    public partial class SheetChanger : ControlLayer
    {

        #region 成员
        //谱面变速
        private string sheetUrl;
        private string sheetName;
        private string sheetDirectory; //末尾带\
        private double speed;
        private int nowParse; //用于检测当前谱面变速处理什么类

        //音频变速
        private SupportedAudioFormat nowParseSoundFormat;//当前处理的音频类型
        private string soundUrl;
        private string soundName;
        private string soundDirectory;//末尾带\

        //快速打包
        private bool arcaoid = false;
        private bool rapidPack = false;
        private string packPath;//末尾不带\
        private int nowPack = 0; //快速打包 2为带小节线变速，1为原速
        SongListGenerator songListGenerator;
        #endregion


        #region 控制台调试代码
        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();
        [DllImport("kernel32.dll")]
        public static extern Boolean FreeConsole();
        #endregion

        #region 界面初始化操作

        public SheetChanger()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(82, 67, 95);
            TransparencyKey = BackColor;
            GlobalVar.arcaeaFont = new PersonalFont(PersonalResource.GetTempURL(global::ArcaeaSpeedChanger.Properties.Resources.Exo_Regular, "Arcaea.ttf"));
            PersonalResource.GetTempURL(global::ArcaeaSpeedChanger.Properties.Resources.SoundTouch, "SoundTouch.dll");
            rapidPackCheckBox.Font = GlobalVar.arcaeaFont.GetFont(9);
            rapidPackCheckBox.ForeColor = Color.White;
            soundCheckBox.Font = GlobalVar.arcaeaFont.GetFont(9);
            soundCheckBox.ForeColor = Color.White;
            arcaoidCheckBox.Font = GlobalVar.arcaeaFont.GetFont(9);
            arcaoidCheckBox.ForeColor = Color.White;
            barLineCheckBox.Font = GlobalVar.arcaeaFont.GetFont(9);
            barLineCheckBox.ForeColor = Color.White;
            GlobalVar.arcaeaFont.ArcaeaButton(generateButton, 18);
            GlobalVar.arcaeaFont.ArcaeaButton(openFileButton, 18);
            GlobalVar.arcaeaFont.ArcaeaLabel(label1, 12);
            GlobalVar.arcaeaFont.ArcaeaLabel(label2, 12);
            GlobalVar.arcaeaFont.ArcaeaLabel(sheetUrlLabel, 12);
            InitialButton();
            progressBar.Visible = false;
            progressBar.Maximum = 100;
            // AllocConsole();
            songListGenerator = new SongListGenerator();


        }
        private void InitialButton()
        {
            ImageButton imgOpenFileBtn = new ImageButton(openFileButton, global::ArcaeaSpeedChanger.Properties.Resources.shortBlackBtnLine);
            imgOpenFileBtn.SetMouseLeaveButton(global::ArcaeaSpeedChanger.Properties.Resources.shortBlackBtnLine);
            imgOpenFileBtn.SetMouseEnterButton(global::ArcaeaSpeedChanger.Properties.Resources.shortBlackBtnLine_3d);
            imgOpenFileBtn.SetMousePressedButton(global::ArcaeaSpeedChanger.Properties.Resources.shortBlackBtnLine_pressed);
            imgOpenFileBtn.SetMouseDisabledButton(global::ArcaeaSpeedChanger.Properties.Resources.shortBlackBtnLine_disabled);
            
            ImageButton imgGenerateBtn = new ImageButton(generateButton, global::ArcaeaSpeedChanger.Properties.Resources.shortBlackBtnLine);
            imgGenerateBtn.SetMouseLeaveButton(global::ArcaeaSpeedChanger.Properties.Resources.shortBlackBtnLine);
            imgGenerateBtn.SetMouseEnterButton(global::ArcaeaSpeedChanger.Properties.Resources.shortBlackBtnLine_3d);
            imgGenerateBtn.SetMousePressedButton(global::ArcaeaSpeedChanger.Properties.Resources.shortBlackBtnLine_pressed);
            imgGenerateBtn.SetMouseDisabledButton(global::ArcaeaSpeedChanger.Properties.Resources.shortBlackBtnLine_disabled);
        }


        private void SheetChanger_Load(object sender, EventArgs e)
        {
            SpeedComboBox.Items.Add("0.5");
            SpeedComboBox.Items.Add("0.55");
            SpeedComboBox.Items.Add("0.6");
            SpeedComboBox.Items.Add("0.65");
            SpeedComboBox.Items.Add("0.7");
            SpeedComboBox.Items.Add("0.75");
            SpeedComboBox.Items.Add("0.8");
            SpeedComboBox.Items.Add("0.85");
            SpeedComboBox.Items.Add("0.9");
            SpeedComboBox.Items.Add("0.95");
            SpeedComboBox.Items.Add("1.05");
            SpeedComboBox.Items.Add("1.1");
            SpeedComboBox.Items.Add("1.15");
            SpeedComboBox.Items.Add("1.2");
            SpeedComboBox.Items.Add("1.25");
            SpeedComboBox.Items.Add("1.3");
            SpeedComboBox.Items.Add("1.35");
            SpeedComboBox.Items.Add("1.4");
            SpeedComboBox.Items.Add("1.45");
            SpeedComboBox.Items.Add("1.5");
            SpeedComboBox.Items.Add("1.55");
            SpeedComboBox.Items.Add("1.6");
            SpeedComboBox.Items.Add("1.65");
            SpeedComboBox.Items.Add("1.7");
            SpeedComboBox.Items.Add("1.75");
            SpeedComboBox.Items.Add("1.8");
            SpeedComboBox.Items.Add("1.85");
            SpeedComboBox.Items.Add("1.9");
            SpeedComboBox.Items.Add("1.95");
            SpeedComboBox.Items.Add("2.0");
        }
        #endregion

        #region 界面控件事件操作

        private void GenerateSqlDataBtn_Click(object sender, EventArgs e)
        {
            if (!songListGenerator.OpenSonglist("songlist"))
            {
                ShowArcaeaDialog("songlist文件不存在！");
            }
            else
            {
                ShowArcaeaDialog("成功导入songlist文件，将按songlist信息生成");
                EuyFile.WriteFile("SongData.txt", songListGenerator.GenerateAllSongData());
                ShowArcaeaDialog("歌曲数据生成成功！");
            }
        }
        private void CombineJsonBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "谱面文件(*.txt;*.aff)|*.txt;*.aff";
            openFileDialog.Multiselect = true; //是否可以多选
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                StringBuilder combinedJson = new StringBuilder();
                //多个文件
                string[] jsonNames = openFileDialog.FileNames;
                for (int i = 0; i < jsonNames.Length; i++)
                {
                    StringBuilder tmp = EuyFile.ReadFile(jsonNames[i]);
                    if (i == 0)
                    {
                        combinedJson = tmp;
                    }
                    else
                    {
                        combinedJson.Append(new StringBuilder(",\n" + tmp));
                    }
                }
                EuyFile.WriteFile(Directory.GetParent(jsonNames[0]).FullName + "\\combinedJson.txt", combinedJson);
                ShowArcaeaDialog("合并json成功！");
            }

        }
        private void SoundCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.soundUrl = null;
            this.sheetUrl = null;
            this.sheetUrlLabel.Text = "C:\\arcaea";
            if (soundCheckBox.Checked)
            {
                this.label2.Text = "谱面/音频路径";
            }
            else
            {
                this.label2.Text = "谱面路径";
            }

        }
        private void ArcaoidCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (arcaoidCheckBox.Checked)
            {
                this.arcaoid = true;
            }
            else
            {
                this.arcaoid = false;
            }
        }
        private void RapidPackCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (rapidPackCheckBox.Checked)
            {
                if (!songListGenerator.isInitialized)
                {
                    if (!songListGenerator.OpenSonglist("songlist"))
                    {
                        ShowArcaeaDialog("songlist文件不存在！将按照默认值生成json");
                    }
                    else
                    {
                        ShowArcaeaDialog("成功导入songlist文件，将按songlist信息写入相应json");
                    }
                    
                } 
                this.barLineCheckBox.Checked = true;
                this.soundCheckBox.Checked = true;
                this.barLineCheckBox.Enabled = false;
                this.soundCheckBox.Enabled = false;
                rapidPack = true;
            }
            else
            {
                this.barLineCheckBox.Checked = false;
                this.soundCheckBox.Checked = false;
                this.barLineCheckBox.Enabled = true;
                this.soundCheckBox.Enabled = true;
                rapidPack = false;
            }
        }
        private void SpeedComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.speed = Convert.ToDouble(SpeedComboBox.Text);
        }
        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog
                {
                    Filter = "arcaea谱面文件(*.aff;*.txt)|*.aff;*.txt"
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    sheetUrlLabel.Text = dialog.FileName;
                    this.sheetUrl = dialog.FileName;
                    this.sheetName = dialog.SafeFileName;
                    this.sheetDirectory = this.sheetUrl.Replace(sheetName, "");
                    if (soundCheckBox.Checked)
                    {
                        OpenFileDialog soundOpenDialog = new OpenFileDialog
                        {
                            Filter = "OGG音频文件(*.ogg)|*.ogg|MP3音频文件(*.mp3)|*.mp3"
                        };
                        if (soundOpenDialog.ShowDialog() == DialogResult.OK)
                        {
                            switch(soundOpenDialog.FilterIndex)
                            {
                                case 1:
                                    nowParseSoundFormat = SupportedAudioFormat.OGG;
                                    break;
                                case 2:
                                    nowParseSoundFormat = SupportedAudioFormat.MP3;
                                    break;
                            }
                            if (!File.Exists(@"SOX\sox.exe") && nowParseSoundFormat == SupportedAudioFormat.OGG)
                            {
                                ShowArcaeaDialog("OGG转换需要sox.exe支持, 请将SOX文件夹放在变速器同一个目录下！");
                            } else
                            {
                                sheetUrlLabel.Text += "\n" + soundOpenDialog.FileName;
                                this.soundUrl = soundOpenDialog.FileName;
                                this.soundName = soundOpenDialog.SafeFileName;
                                this.soundDirectory = this.soundUrl.Replace(soundName, "");
                            }

                        }
                        else
                        {
                            this.soundUrl = null;
                        }
                    }
                }
                else
                {
                    this.sheetUrl = null;
                }
            }
            catch (Exception)
            {
                AskAuthor();
                throw;
            }

        }

        private void SheetChanger_MouseClick(object sender, MouseEventArgs e)
        {
            label1.Focus();
        }

        private void ShowArcaeaDialog(String text = "")
        {
            ArcaeaDialog arcaeaDialog = new ArcaeaDialog(text);
            arcaeaDialog.Show();
            arcaeaDialog.Location = arcaeaDialog.GetParentFormCenterPoint(this);
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 音频变速与多线程相关
        public void ChangeAudioSpeed()
        {
            openFileButton.Enabled = false;
            generateButton.Enabled = false;
            Control.CheckForIllegalCrossThreadCalls = false;//如果不会好几个线程同时操作一个控件就可使用
            Thread thread = ProgressListener();
            thread.Start();
            //Task.Factory.StartNew(() =>
            //{
            //    SoundSpeedChanger soundSpeedChanger = new SoundSpeedChanger(soundUrl);
            //    soundSpeedChanger.OpenMp3File();
            //    soundSpeedChanger.SetTempo(this.speed);
            //    soundSpeedChanger.ExportMp3(this.soundDirectory + this.speed.ToString() + "x base.mp3");
            //    thread.Abort();
            //});
            Thread soundChangerThread = new Thread(new ThreadStart(() => {
                SoundSpeedChanger soundSpeedChanger = new SoundSpeedChanger(soundUrl);
                soundSpeedChanger.OpenAudioFile(nowParseSoundFormat);
                soundSpeedChanger.SetTempo(this.speed);
                string exportedSoundFormat;
                switch(nowParseSoundFormat)
                {
                    case SupportedAudioFormat.MP3:
                        exportedSoundFormat = ".mp3";
                        break;
                    case SupportedAudioFormat.OGG:
                        exportedSoundFormat = ".ogg";
                        break;
                    default:
                        exportedSoundFormat = ""; //如果不写就可能会导致其未赋值
                        break;
                }
                if(!rapidPack)
                {
                    soundSpeedChanger.ExportAudio(this.soundDirectory + this.speed.ToString() + "x base" + exportedSoundFormat);
                }
                else
                {
                    soundSpeedChanger.ExportAudio(this.packPath + "\\" + "base" + exportedSoundFormat);
                }
                thread.Abort();
            }));
            soundChangerThread.Start();
        }


        private Thread ProgressListener()
        {
            //AllocConsole();
            progressBar.Value = 0;
            progressBar.Visible = true;
            Thread listener = new Thread(new ThreadStart(ListeningProgress));
            return listener;
        }
        private void ListeningProgress()
        {
            try
            {
                while (true)
                {
                    //Console.WriteLine(1);
                    if (progressBar.Value != GlobalVariable.soundParseProgress)
                    {
                        //Console.WriteLine("not Same!" + GlobalVariable.soundParseProgress);
                        if (GlobalVariable.soundParseProgress > 100)
                        {
                            progressBar.Value = 100;
                            break;
                        }
                        progressBar.Value = GlobalVariable.soundParseProgress;
                    }
                    Thread.Sleep(50);
                }
            }
            catch (ThreadAbortException)
            {

            }
            finally
            {
                progressBar.Value = 100;
                GlobalVariable.soundParseProgress = 0;
                this.Invoke(new EventHandler(delegate
                {
                    if(!rapidPack)
                        ChangeSheetSpeed(sheetUrl);
                    else
                    {
                        //生成aff
                        nowPack = 2;//打包2.aff 
                        StringBuilder sheet = ChangeSheetSpeed(sheetUrl);
                        Euynac.Utility.EuyFile.WriteFile(this.packPath + "\\" + "2.aff", sheet);
                        nowPack = 1;//打包1.aff
                        sheet = ChangeSheetSpeed(sheetUrl);
                        Euynac.Utility.EuyFile.WriteFile(this.packPath + "\\" + "1.aff", sheet);
                        Euynac.Utility.EuyFile.WriteFile(this.packPath + "\\" + "0.aff", sheet);
                        ShowArcaeaDialog("生成成功，位于原谱面目录下");
                    }
                }));
                Thread.Sleep(1000);
                progressBar.Visible = false;
                this.generateButton.Enabled = true;
                this.openFileButton.Enabled = true;

            }

        }

        #endregion

        #region 生成操作
        private void GenerateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.sheetUrl != null)
                {
                    if (SpeedComboBox.Text != "")
                    {
                        this.speed = Convert.ToDouble(SpeedComboBox.Text);
                        if (this.soundCheckBox.Checked)
                        {
                            if (this.soundUrl != null)
                            {
                                if (!rapidPack)
                                {
                                    ChangeAudioSpeed();
                                }
                                else
                                {
                                    RapidPackSongList();
                                }
                            }
                            else
                            {
                                ShowArcaeaDialog("音频/谱面文件未选或出错，请重新选择！");
                            }
                        }
                        else
                        {
                            ChangeSheetSpeed(sheetUrl);
                        }
                    }
                    else
                    {
                        ShowArcaeaDialog("请选择变速倍数！");
                    }
                }
                else
                {
                    ShowArcaeaDialog("谱面文件未选或出错，请重新选择！");
                }
            }
            catch (Exception)
            {
                AskAuthor();
                throw;
            }

        }

        private void RapidPackSongList()//快速打包 2.aff为带小节线变速 1.aff无小节线变速
        {
            string songOriginID = Directory.GetParent(this.sheetUrl).Name;
            string songName = songOriginID + " " + this.speed.ToString() + "x";
            string directoryName = songOriginID + this.speed.ToString("f2").Replace(".", "");
            string songlistPath = Path.Combine(this.sheetDirectory, directoryName);
            this.packPath = songlistPath;
            Directory.CreateDirectory(songlistPath);

            if(songListGenerator.isOfficial)
                if(!songListGenerator.isExistSong(songOriginID))
                {
                    ShowArcaeaDialog("歌曲信息未在songlist中查找到，将按照默认值生成json");
                }

            //生成Arcaoid文件
            if(arcaoid)
            {
                StringBuilder arcaoidData = songListGenerator.RapidGenerateArcaoid(songOriginID, " " + this.speed.ToString() + "x", directoryName, songName);
                EuyFile.WriteFile(songlistPath + "\\ARCAOID.txt", arcaoidData);
            }
            else    //生成songlist
            {
                string songlist = songListGenerator.RapidGenerateSonglist(songOriginID, " " + this.speed.ToString() + "x", directoryName, songName);
                Euynac.Utility.EuyFile.WriteFile(sheetDirectory + songName + ".txt", new StringBuilder(songlist));
            }

            ////生成aff
            //nowPack = 2;//打包2.aff 
            //StringBuilder sheet = ChangeSheetSpeed(sheetUrl);
            //Euynac.Utility.File.WriteFile(songlistPath + "\\" + "2.aff", sheet);
            //nowPack = 1;//打包1.aff
            //sheet = ChangeSheetSpeed(sheetUrl);
            //Euynac.Utility.File.WriteFile(songlistPath + "\\" + "1.aff", sheet);
            //Euynac.Utility.File.WriteFile(songlistPath + "\\" + "0.aff", sheet);

            ////生成音频
            ChangeAudioSpeed();
            //生成图片
            if (File.Exists(sheetDirectory + "base.jpg") && File.Exists(sheetDirectory + "base_256.jpg"))
            {
                if(!File.Exists(packPath + "\\base.jpg"))
                    File.Copy(sheetDirectory + "base.jpg", packPath + "\\base.jpg");
                if (!arcaoid)
                {
                    if (!File.Exists(packPath + "\\base_256.jpg"))
                        File.Copy(sheetDirectory + "base_256.jpg", packPath + "\\base_256.jpg");
                }
            }
        }
        #endregion

        #region 谱面变速核心代码
        private StringBuilder ChangeSheetSpeed(string sheetUrl)
        {
            try
            {
                StreamReader reader = new StreamReader(sheetUrl, Encoding.Default);
                String line;
                StringBuilder result = new StringBuilder();
                //AllocConsole();

                String[] patterns = {
                    @"^\(\d+,\d+\);",
                    @"^arc\(\d+,",
                    @"^hold\(\d+,",
                    @"^timing\(\d+,",
                    @"AudioOffset:[-]?\d+"
                };
                bool isSheet = false;
                while ((line = reader.ReadLine()) != null)
                {
                    int i = 0;
                    nowParse = i;
                    bool matchFlag = false;
                    foreach (string pattern in patterns)
                    {
                        if (Regex.IsMatch(line, pattern))
                        {
                            string tmp = AlterSheet(line, pattern, i);
                            //Console.WriteLine(tmp);
                            result.Append(tmp + '\n');
                            matchFlag = true;
                            isSheet = true;
                            break;
                        }
                        i++;
                        nowParse = i;
                    }
                    if (!matchFlag)
                    {
                        result.Append(line + '\n');
                    }
                }
                reader.Close();
                if (!isSheet)
                {
                    if(!rapidPack)
                    {
                        ShowArcaeaDialog("生成失败，不是谱面文件！");
                    }
                    return null;
                }
                else
                {
                    if(!rapidPack)
                    {
                        Euynac.Utility.EuyFile.WriteFile(this.sheetDirectory + this.speed.ToString() + "x sheet.aff", result);
                        ShowArcaeaDialog("生成成功，位于原谱面目录下");
                    }
                    return result;
                }
            }
            catch (Exception)
            {
                AskAuthor();
                throw;
            }

        }

        

        //以下是核心变速代码
        private string ReplaceInteger(Match m)
        {
            try
            {
                string str = m.ToString();
                double number = Convert.ToDouble(str);
                number = Math.Round(number / this.speed);
                return number.ToString();
            }
            catch (Exception)
            {
                AskAuthor();
                throw;
            }

        }
        private string ReplaceDouble(Match m)
        {
            string str = m.ToString();
            double number = Convert.ToDouble(str);
            number = Math.Round(number * this.speed, 2);
            return number.ToString("f2");
        }
        private string ChangeTimingBPM(Match m)
        {
            string str = m.ToString();
            Regex regex = new Regex(@"[-]?\d+\.\d+");
            return regex.Replace(str, new MatchEvaluator(ReplaceDouble));
        }
        private string ChangeTimeStamp(Match m)
        {
            try
            {

                string str = m.ToString();
                if (nowParse == 3)
                {

                    Regex regex1 = new Regex(@"\(\d+");
                    if (regex1.IsMatch(str))
                    {
                        nowParse = 0;
                        string str1 = regex1.Replace(str, new MatchEvaluator(ChangeTimeStamp));
                        Regex regex2 = new Regex(@",[-]?\d+\.\d+");

                        if(rapidPack)
                        {
                            if(nowPack == 2)
                            {
                                return regex2.Replace(str1, new MatchEvaluator(ChangeTimingBPM));
                            }
                            else if(nowPack == 1)
                            {
                                return str1;
                            }
                            
                        }
                        else
                        {
                            if (barLineCheckBox.Checked)
                            {
                                return regex2.Replace(str1, new MatchEvaluator(ChangeTimingBPM));
                            }
                            else
                            {
                                return str1;
                            }
                        }
                    }


                }
                Regex regex = new Regex(@"\d+");
                return regex.Replace(str, new MatchEvaluator(ReplaceInteger));//这个函数是把匹配的拿去调用替换函数替换匹配的字段
            }
            catch (Exception)
            {
                AskAuthor();
                throw;
            }

        }

        private string AlterSheet(string sheet, string pattern, int i)
        {
            try
            {
                //i = 0 tap类
                if (i == 0)
                {
                    Regex regex = new Regex(@"\(\d+,");
                    string result = regex.Replace(sheet, new MatchEvaluator(ChangeTimeStamp));
                    return result;
                }
                //i = 1 arc类
                else if (i == 1)
                {
                    Regex regex = new Regex(@"(arc\(\d+,\d+)|(arctap\(\d+)");
                    string result = regex.Replace(sheet, new MatchEvaluator(ChangeTimeStamp));
                    return result;
                }
                //i = 2 hold类
                else if (i == 2)
                {
                    Regex regex = new Regex(@"hold\(\d+,\d+");
                    string result = regex.Replace(sheet, new MatchEvaluator(ChangeTimeStamp));
                    return result;
                }
                //i = 3 timing类
                else if (i == 3)
                {
                    Regex regex = new Regex(@"timing\(\d+,[-]?\d+.\d+");
                    string result = regex.Replace(sheet, new MatchEvaluator(ChangeTimeStamp));
                    return result;
                }
                //i = 4 audioOffset类
                else if (i == 4)
                {
                    Regex regex = new Regex(@"AudioOffset:[-]?\d+");
                    string result = regex.Replace(sheet, new MatchEvaluator(ChangeTimeStamp));
                    return result;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {
                AskAuthor();
                throw;
            }

        }
        #endregion

        #region 报错处理
        private void AskAuthor()
        {
            //MessageBox.Show("啊咧？被您找到BUG了...联系七奏870838080 协助修复BUG" + Environment.NewLine + "错误信息:" + e.Message);
            ShowArcaeaDialog("啊咧？被您找到BUG了..." + Environment.NewLine + "欢迎联系七奏QQ870838080协助修复");
        }


        #endregion


    }
    public static class GlobalVariable //C#无全局变量 只好用静态类了
    {
        public static int soundParseProgress = 0;
        public static SupportedAudioFormat soundParseFormat;
    }
}
