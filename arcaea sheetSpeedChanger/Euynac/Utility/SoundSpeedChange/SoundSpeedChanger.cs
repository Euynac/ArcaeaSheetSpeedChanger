using NAudio.MediaFoundation;
using NAudio.Vorbis;
using NAudio.Wave;
using soundtouch;
using SoxSharp;
using System;
using System.IO;

namespace Euynac.Utility.SoundSpeedChange
{
    public enum SupportedAudioFormat
    {
        OGG,
        MP3
    }
    class SoundSpeedChanger
    {
        public SupportedAudioFormat nowParseSoundFormat;
        private string soundFilePath;
        private WaveOut waveOut; //用于播放？
        public WaveStreamProcessor streamProcessor;


        public SoundSpeedChanger(string path)
        {
            this.soundFilePath = path;
        }


        public bool ExportAudio(string changedSoundName)
        {
            try
            {
                
                if(soundFilePath != null && streamProcessor != null)
                {
                    //MediaType mediaType;
                    switch (nowParseSoundFormat)
                    {
                        case SupportedAudioFormat.MP3:
                            var mediaType = MediaFoundationEncoder.SelectMediaType(AudioSubtypes.MFAudioFormat_MP3, new WaveFormat(44100, 2), 256000);
                            using (var reader = new MediaFoundationReader(soundFilePath)) //这里暂时没用，是导入已存在的音频文件
                            {
                                using (var encoder = new MediaFoundationEncoder(mediaType))
                                {

                                    encoder.Encode(changedSoundName, streamProcessor);//直接用被soundtouch处理的音频wav
                                }
                            }
                            return true;
                        case SupportedAudioFormat.OGG:
                            WaveFileWriter.CreateWaveFile(changedSoundName.Replace(".ogg", ".wav"), streamProcessor);
                            
                            using (Sox sox = new Sox(@"SOX\sox.exe"))
                            {
                                sox.OnProgress += sox_OnProgress;
                                sox.Output.SampleRate = 44100;
                                sox.Output.Compression = 7;//压缩率1-10
                                sox.Process(changedSoundName.Replace(".ogg", ".wav"), changedSoundName);
                            }
                            File.Delete(changedSoundName.Replace(".ogg", ".wav"));
                            return true;
                        default:
                            return false;
                    }

                    //var mediaType = MediaFoundationEncoder.SelectMediaType(AudioSubtypes.MFAudioFormat_MP3, new WaveFormat(44100, 2), 256000);
                    //using (var reader = new MediaFoundationReader(soundFilePath)) //这里暂时没用，是导入已存在的音频文件
                    //{
                    //    using (var encoder = new MediaFoundationEncoder(mediaType))
                    //    {

                    //        encoder.Encode(changedSoundName, streamProcessor);//直接用被soundtouch处理的音频wav
                    //    }
                    //}
                    //return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                //return false;
                throw;
            }
        }

        private void sox_OnProgress(object sender, ProgressEventArgs e) //SOX处理进度事件
        {
            ArcaeaSpeedChanger.GlobalVariable.soundParseProgress = 50 + e.Progress / 2;
        }

        /// <summary>
        /// Open MP3 file
        /// </summary>
        /// <returns>true if successful</returns>
        public bool OpenAudioFile(SupportedAudioFormat format)
        {
            try
            {
                WaveChannel32 inputStream;

                switch (format)
                {
                    case SupportedAudioFormat.MP3:
                        ArcaeaSpeedChanger.GlobalVariable.soundParseFormat = nowParseSoundFormat = SupportedAudioFormat.MP3;
                        Mp3FileReader mp3File = new Mp3FileReader(soundFilePath);
                        inputStream = new WaveChannel32(mp3File);
                        break;
                    case SupportedAudioFormat.OGG:
                        ArcaeaSpeedChanger.GlobalVariable.soundParseFormat = nowParseSoundFormat = SupportedAudioFormat.OGG;
                        VorbisWaveReader oggFile = new VorbisWaveReader(soundFilePath);
                        inputStream = new WaveChannel32(oggFile);
                        break;
                    default:
                        return false;
                }
                inputStream.PadWithZeroes = false;  // don't pad, otherwise the stream never ends
                streamProcessor = new WaveStreamProcessor(inputStream); //这个应该是被soundTouch处理过的wav

                waveOut = new WaveOut()   //waveOut应该是用来播放的
                {
                    DesiredLatency = 100
                };

                waveOut.Init(streamProcessor);  // inputStream);

                return true;
            }
            catch (Exception)
            {
                // Error in opening file
                waveOut = null;
                //return false;
                throw;
            }

        }


        public void SetTempo(double tempoValue)
        {
            if (this.streamProcessor != null)
                this.streamProcessor.st.Tempo = (float)tempoValue;
        }


    }






    /// <summary>
    /// NAudui WaveStream class for processing audio stream with SoundTouch effects
    /// </summary>
    public class WaveStreamProcessor : WaveStream
    {
        private WaveChannel32 inputStr;
        public SoundTouch st;

        private byte[] bytebuffer = new byte[4096];
        private float[] floatbuffer = new float[1024];
        bool endReached = false;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="input">WaveChannel32 stream used for processor stream input</param>
        public WaveStreamProcessor(WaveChannel32 input)
        {
            inputStr = input;
            st = new SoundTouch();
            st.Channels = (uint)input.WaveFormat.Channels;
            st.SampleRate = (uint)input.WaveFormat.SampleRate;
        }

        /// <summary>
        /// True if end of stream reached
        /// </summary>
        public bool EndReached
        {
            get { return endReached; }
        }


        public override long Length
        {
            get
            {
                return inputStr.Length;
            }
        }


        public override long Position
        {
            get
            {
                return inputStr.Position;
            }

            set
            {
                inputStr.Position = value;
            }
        }


        public override WaveFormat WaveFormat
        {
            get
            {
                return inputStr.WaveFormat;
            }
        }

        /// <summary>
        /// Overridden Read function that returns samples processed with SoundTouch. Returns data in same format as
        /// WaveChannel32 i.e. stereo float samples.
        /// </summary>
        /// <param name="buffer">Buffer where to return sample data</param>
        /// <param name="offset">Offset from beginning of the buffer</param>
        /// <param name="count">Number of bytes to return</param>
        /// <returns>Number of bytes copied to buffer</returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            try
            {
                if(ArcaeaSpeedChanger.GlobalVariable.soundParseFormat == SupportedAudioFormat.OGG)
                {
                    if(ArcaeaSpeedChanger.GlobalVariable.soundParseProgress <= 50)
                    {
                        ArcaeaSpeedChanger.GlobalVariable.soundParseProgress += 2;
                    }
                }
                else
                {
                    ArcaeaSpeedChanger.GlobalVariable.soundParseProgress += 5;
                }
                //Console.WriteLine("Change!" + ArcaeaSpeedChanger.GlobalVariable.soundParseProgress);
                // Iterate until enough samples available for output:
                // - read samples from input stream
                // - put samples to SoundStretch processor
                while (st.AvailableSampleCount < count)
                {
                    int nbytes = inputStr.Read(bytebuffer, 0, bytebuffer.Length);
                    if (nbytes == 0)
                    {
                        // end of stream. flush final samples from SoundTouch buffers to output
                        if (endReached == false)
                        {
                            endReached = true;  // do only once to avoid continuous flushing
                            st.Flush();
                        }
                        break;
                    }

                    // binary copy data from "byte[]" to "float[]" buffer
                    Buffer.BlockCopy(bytebuffer, 0, floatbuffer, 0, nbytes);
                    st.PutSamples(floatbuffer, (uint)(nbytes / 8));
                }

                // ensure that buffer is large enough to receive desired amount of data out
                if (floatbuffer.Length < count / 4)
                {
                    floatbuffer = new float[count / 4];
                }
                // get processed output samples from SoundTouch
                int numsamples = (int)st.ReceiveSamples(floatbuffer, (uint)(count / 8));
                // binary copy data from "float[]" to "byte[]" buffer
                Buffer.BlockCopy(floatbuffer, 0, buffer, offset, numsamples * 8);
                return numsamples * 8;  // number of bytes
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Clear the internal processor buffers. Call this if seeking or rewinding to new position within the stream.
        /// </summary>
        public void Clear()
        {
            st.Clear();
            endReached = false;
        }
    }
}
