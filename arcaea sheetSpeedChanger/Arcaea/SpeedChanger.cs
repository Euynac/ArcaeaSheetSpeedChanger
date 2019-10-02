//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;

//namespace Arcaea.Song
//{
//    class SpeedChanger
//    {
//        private string sheetUrl;
//        private string sheetName;
//        private string sheetDirectory;
//        private string soundUrl;
//        private string soundName;
//        private string soundDirectory;
//        private double speed;
//        private int nowParse; //用于检测当前谱面变速处理什么类



//        #region 谱面变速核心代码
//        private void ChangeSheetSpeed(string sheetUrl)
//        {
//            try
//            {
//                StreamReader reader = new StreamReader(sheetUrl, Encoding.Default);
//                String line;
//                StringBuilder result = new StringBuilder();

//                String[] patterns = {
//                    @"^\(\d+,\d+\);",
//                    @"^arc\(\d+,",
//                    @"^hold\(\d+,",
//                    @"^timing\(\d+,",
//                    @"AudioOffset:[-]?\d+"
//                };
//                bool isSheet = false;
//                while ((line = reader.ReadLine()) != null)
//                {
//                    int i = 0;
//                    nowParse = i;
//                    bool matchFlag = false;
//                    foreach (string pattern in patterns)
//                    {
//                        if (Regex.IsMatch(line, pattern))
//                        {
//                            string tmp = AlterSheet(line, pattern, i);
//                            //Console.WriteLine(tmp);
//                            result.Append(tmp + '\n');
//                            matchFlag = true;
//                            isSheet = true;
//                            break;
//                        }
//                        i++;
//                        nowParse = i;
//                    }
//                    if (!matchFlag)
//                    {
//                        result.Append(line + '\n');
//                    }
//                }
//                if (!isSheet)
//                {
//                    ShowArcaeaDialog("生成失败，不是谱面文件！");
//                }
//                else
//                {
//                    FileStream resultFile = new FileStream(this.sheetDirectory + this.speed.ToString() + "x sheet.aff", FileMode.Create);
//                    StreamWriter writer = new StreamWriter(resultFile);
//                    writer.Write(result);
//                    writer.Flush();
//                    writer.Close();
//                    resultFile.Close();
//                    ShowArcaeaDialog("生成成功，位于原谱面目录下");
//                }
//                reader.Close();
//            }
//            catch (Exception)
//            {
//                AskAuthor();
//                throw;
//            }

//        }

//        private void RapidPackSongList()//快速打包 2.aff为带小节线变速 1.aff无小节线变速
//        {
//            string songName = this.speed.ToString() + "x" + Directory.GetParent(this.sheetUrl).Name;
//            string directoryName = this.speed.ToString().Replace(".", "") + Directory.GetParent(this.sheetUrl).Name;
//            Directory.CreateDirectory(Path.Combine(this.sheetDirectory, directoryName));
//        }

//        //以下是核心变速代码
//        private string ReplaceInteger(Match m)
//        {
//            try
//            {
//                string str = m.ToString();
//                double number = Convert.ToDouble(str);
//                number = Math.Round(number / this.speed);
//                return number.ToString();
//            }
//            catch (Exception)
//            {
//                AskAuthor();
//                throw;
//            }

//        }
//        private string ReplaceDouble(Match m)
//        {
//            string str = m.ToString();
//            double number = Convert.ToDouble(str);
//            number = Math.Round(number * this.speed, 2);
//            return number.ToString("f2");
//        }
//        private string ChangeTimingBPM(Match m)
//        {
//            string str = m.ToString();
//            Regex regex = new Regex(@"[-]?\d+\.\d+");
//            return regex.Replace(str, new MatchEvaluator(ReplaceDouble));
//        }
//        private string ChangeTimeStamp(Match m)
//        {
//            try
//            {

//                string str = m.ToString();
//                if (nowParse == 3)
//                {

//                    Regex regex1 = new Regex(@"\(\d+");
//                    if (regex1.IsMatch(str))
//                    {
//                        nowParse = 0;
//                        string str1 = regex1.Replace(str, new MatchEvaluator(ChangeTimeStamp));
//                        Regex regex2 = new Regex(@",[-]?\d+\.\d+");

//                        if (barLineCheckBox.Checked)
//                        {
//                            return regex2.Replace(str1, new MatchEvaluator(ChangeTimingBPM));
//                        }
//                        else
//                        {
//                            return str1;
//                        }
//                    }


//                }
//                Regex regex = new Regex(@"\d+");
//                return regex.Replace(str, new MatchEvaluator(ReplaceInteger));//这个函数是把匹配的拿去调用替换函数替换匹配的字段
//            }
//            catch (Exception)
//            {
//                AskAuthor();
//                throw;
//            }

//        }

//        private string AlterSheet(string sheet, string pattern, int i)
//        {
//            try
//            {
//                //i = 0 tap类
//                if (i == 0)
//                {
//                    Regex regex = new Regex(@"\(\d+,");
//                    string result = regex.Replace(sheet, new MatchEvaluator(ChangeTimeStamp));
//                    return result;
//                }
//                //i = 1 arc类
//                else if (i == 1)
//                {
//                    Regex regex = new Regex(@"(arc\(\d+,\d+)|(arctap\(\d+)");
//                    string result = regex.Replace(sheet, new MatchEvaluator(ChangeTimeStamp));
//                    return result;
//                }
//                //i = 2 hold类
//                else if (i == 2)
//                {
//                    Regex regex = new Regex(@"hold\(\d+,\d+");
//                    string result = regex.Replace(sheet, new MatchEvaluator(ChangeTimeStamp));
//                    return result;
//                }
//                //i = 3 timing类
//                else if (i == 3)
//                {
//                    Regex regex = new Regex(@"timing\(\d+,[-]?\d+.\d+");
//                    string result = regex.Replace(sheet, new MatchEvaluator(ChangeTimeStamp));
//                    return result;
//                }
//                //i = 4 audioOffset类
//                else if (i == 4)
//                {
//                    Regex regex = new Regex(@"AudioOffset:[-]?\d+");
//                    string result = regex.Replace(sheet, new MatchEvaluator(ChangeTimeStamp));
//                    return result;
//                }
//                else
//                {
//                    return "";
//                }
//            }
//            catch (Exception)
//            {
//                AskAuthor();
//                throw;
//            }

//        }
//        #endregion
//    }
//}
