using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Euynac.WinformUI
{
    public partial class ControlLayer : Form
    {
        private SkinLayer Skin;
        private double animateSpeed = 20;
        public ControlLayer()
        {
            InitializeComponent();
            //SetStyles();//减少闪烁
            ShowInTaskbar = false;//禁止控件层显示到任务栏
            FormBorderStyle = FormBorderStyle.None;//设置无边框的窗口样式
            TransparencyKey = BackColor;//使控件层背景透明
        }
        private void ControlLayer_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                Opacity = 0;
                Skin = new SkinLayer(this);//创建皮肤层 
                BackgroundImage = null;//去除控件层背景
                Skin.Show();//显示皮肤层 
                AnimateShow();
            }
        }
        public SkinLayer GetSkin()
        {
            return Skin;
        }

        #region 属性
        private bool _skinmobile = true;
        [Category("Skin")]
        [Description("窗体是否可以移动")]
        [DefaultValue(typeof(bool), "true")]
        public bool SkinMovable
        {
            get { return _skinmobile; }
            set
            {
                if (_skinmobile != value)
                {
                    _skinmobile = value;
                }
            }
        }
        #endregion
        #region 减少闪烁
        private void SetStyles()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer, true);
            //强制分配样式重新应用到控件上
            UpdateStyles();
            base.AutoScaleMode = AutoScaleMode.None;
        }
        #endregion
        private void AnimateShow()
        {
            Task.Factory.StartNew(() =>
            {
                for (int i = 0; i <= animateSpeed; i++)
                {
                    BeginInvoke(new Action(() =>
                    {
                        Opacity = i / animateSpeed;
                        Skin.SetBits();
                    }));
                    Thread.Sleep(25);
                }
            });
        }


        //消除控件渲染闪动
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        /// <summary>
        /// 设置淡入速度 值越小越快
        /// </summary>
        /// <param name="speed"></param>
        public void SetAnimateSpeed(int speed) 
        {
            animateSpeed = speed;
        }

        ///<summary>
        ///关闭皮肤层
        ///</summary>
        public void SkinClose() 
        {
            this.Skin.Close();
        }

        /// <summary>
        ///  得到其父窗口的中点（因为双层窗体无法设置父容器 因此只能调用这个来间接达到居中效果）
        ///  实际无父窗体，要传入相对窗体的对象
        /// </summary>
        /// <returns></returns>
        public Point GetParentFormCenterPoint(Form parent)
        {
            return new Point(parent.Location.X + (int)((parent.Width - this.Width) / 2.0), parent.Location.Y + (int)((parent.Height - this.Height) / 2.0));
        }
    }
}
