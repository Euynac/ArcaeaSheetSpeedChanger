using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Arcaea.Song
{
    namespace SongListJson
    {
        public class Title_localized
        {
            /// <summary>
            /// 
            /// </summary>
            public string en { get; set; }
        }

        public class DifficultiesItem
        {
            /// <summary>
            /// 
            /// </summary>
            public int ratingClass { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string chartDesigner { get; set; }
            /// <summary>
            /// c7肘
            /// </summary>
            public string jacketDesigner { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int rating { get; set; }
            /// <summary>
            /// 
            /// </summary>
            //public int plusFingers { get; set; } //这啥
        }

        public class SongsItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Title_localized title_localized { get; set; }
            /// <summary>
            /// 少女フラクタル
            /// </summary>
            public string artist { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string bpm { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double bpm_base { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string @set { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string purchase { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int audioPreview { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int audioPreviewEnd { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int side { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string bg { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int date { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<DifficultiesItem> difficulties { get; set; }
        }
        public class Root
        {
            /// <summary>
            /// 
            /// </summary>
            public List<SongsItem> songs { get; set; }
        }

        public class SongListGenerator
        {
            Dictionary<string, SongsItem> songsMap = null;
            public bool isOfficial = false;//是否是按照songlist生成
            public bool isInitialized = false;//是否初始化过（每启动一次仅导入一次songlist）
            public bool OpenSonglist(string path)
            {
                isInitialized = true;
                if (!File.Exists(path)) return false;
                ReadSonglist(Euynac.Utility.EuyFile.ReadFile(path).ToString());
                isOfficial = true;
                return true;
            }
            private void ReadSonglist(string jsonData)
            {
                songsMap = new Dictionary<string, SongsItem>();
                Root root = JsonConvert.DeserializeObject<Root>(jsonData);
                foreach (SongsItem song in root.songs)
                {
                    songsMap.Add(song.id, song);
                }
            }
            public bool isExistSong(string originSongID)
            {
                return songsMap.ContainsKey(originSongID);
            }

            public StringBuilder RapidGenerateArcaoid(string originSongID, string speed, string songID, string songName, bool randomFun = false)
            {
                string songTitle = songName;
                string artist = "Arcaea";
                string bpm = "168";
                int prsRating = 9;
                string ftrChartDesigner = "Arcaea";
                int ftrRating = 10;
                if (songsMap != null && songsMap.ContainsKey(originSongID))
                {
                    songTitle = songsMap[originSongID].title_localized.en;
                    artist = songsMap[originSongID].artist;
                    bpm = songsMap[originSongID].bpm;
                    prsRating = songsMap[originSongID].difficulties[1].rating;
                    ftrChartDesigner = songsMap[originSongID].difficulties[2].chartDesigner;
                    ftrRating = songsMap[originSongID].difficulties[2].rating;
                }

                StringBuilder arcaoidData = new StringBuilder();
                songTitle = this.isOfficial ? songTitle + speed : songName;
                arcaoidData.Append(songTitle + '\n');
                arcaoidData.Append(ftrChartDesigner + '\n');
                arcaoidData.Append(artist + '\n');
                arcaoidData.Append(bpm + '\n');
                arcaoidData.Append(ftrRating.ToString() + '\n');
                arcaoidData.Append(prsRating.ToString() + '\n');
                arcaoidData.Append("?\n");
                arcaoidData.Append("CONFLICT\n");
                arcaoidData.Append(artist + '\n');
                arcaoidData.Append("CUSTOMBG=TRUE");
                return arcaoidData;

            }

            public string RapidGenerateSonglist(string originSongID, string speed, string songID, string songName, bool randomFun = false)
            {
                string songTitle = songName;
                string artist = "Arcaea";
                string bpm = "168";
                double bpm_base = 168;
                int side = 1;
                string bg = "single_conflict";
                int date = Euynac.Utility.Time.Get10TimeStamp(); ;
                string prsChartDesigner = "Arcaea";
                string prsJacketDesigner = "lowrio";
                int prsRating = 10;
                string ftrChartDesigner = "Arcaea";
                string ftrJacketDesigner = "lowrio";
                int ftrRating = 11;

                if (songsMap != null && songsMap.ContainsKey(originSongID))
                {
                    songTitle = songsMap[originSongID].title_localized.en;
                    artist = songsMap[originSongID].artist;
                    bpm = songsMap[originSongID].bpm;
                    bpm_base = songsMap[originSongID].bpm_base;
                    side = songsMap[originSongID].side;
                    bg = songsMap[originSongID].bg;
                    date = songsMap[originSongID].date;
                    prsChartDesigner = songsMap[originSongID].difficulties[1].chartDesigner;
                    prsJacketDesigner = songsMap[originSongID].difficulties[1].jacketDesigner;
                    prsRating = songsMap[originSongID].difficulties[1].rating;
                    ftrChartDesigner = songsMap[originSongID].difficulties[2].chartDesigner;
                    ftrJacketDesigner = songsMap[originSongID].difficulties[2].jacketDesigner;
                    ftrRating = songsMap[originSongID].difficulties[2].rating;
                }
                SongsItem root = new SongsItem();
                root.id = songID;
                Title_localized title_Localized = new Title_localized();
                title_Localized.en = this.isOfficial ? songTitle + speed : songName;
                root.title_localized = title_Localized;
                root.artist = artist;
                root.bpm = bpm;
                root.bpm_base = bpm_base;
                root.set = "base";//曲包id
                root.purchase = "";
                root.audioPreview = 5000;
                root.audioPreviewEnd = 25000;
                root.side = side;//考虑随机
                root.bg = bg;//这里可以考虑随机
                root.date = date;
                List<DifficultiesItem> difficulties = new List<DifficultiesItem>();
                DifficultiesItem past = new DifficultiesItem();
                past.ratingClass = 0;
                past.chartDesigner = "Arcaea";
                past.jacketDesigner = "lowrio";
                past.rating = 1;
                //past.plusFingers = 0;
                DifficultiesItem present = new DifficultiesItem();
                present.ratingClass = 1;
                present.chartDesigner = prsChartDesigner;
                present.jacketDesigner = prsJacketDesigner;
                present.rating = prsRating;
                //present.plusFingers = 0;
                DifficultiesItem future = new DifficultiesItem();
                future.ratingClass = 2;
                future.chartDesigner = ftrChartDesigner;
                future.jacketDesigner = ftrJacketDesigner;
                future.rating = ftrRating;
                //future.plusFingers = 0;
                difficulties.Add(past);
                difficulties.Add(present);
                difficulties.Add(future);
                root.difficulties = difficulties;
                return JsonConvert.SerializeObject(root);
            }
        }





    }
}

