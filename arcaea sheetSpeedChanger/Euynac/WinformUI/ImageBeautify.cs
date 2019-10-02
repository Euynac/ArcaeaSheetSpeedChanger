using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Euynac.WinformUI
{
    public static class GlobalVar
    {
        public static PersonalFont arcaeaFont;
    }
    public class PersonalFont
    {
        private PrivateFontCollection pfc;
        public PersonalFont(string path)
        {
            pfc = new PrivateFontCollection();
            pfc.AddFontFile(path);
        }
        public Font GetFont(int fontSize)
        {
            Font font = new Font(pfc.Families[0], fontSize);
            return font;
        }
                    
        public void ArcaeaButton(Button button, int fontSize)
        {
            ImageButton.SetImageButton(button);
            button.Font = this.GetFont(fontSize);
            button.ForeColor = Color.White;
        }
        public void ArcaeaLabel(Label label, int fontSize)
        {
            label.Font = this.GetFont(fontSize);
            label.ForeColor = Color.White;
        }
    }
    class ImageButton
    {
        private System.Windows.Forms.Button button;
        private Bitmap normalImg;
        private Bitmap pressedImg;
        private Bitmap mouseOverImg;
        private Bitmap disabledImg;
        public ImageButton(System.Windows.Forms.Button button, Bitmap backGround)
        {
            this.button = button;
            this.normalImg = backGround;
            this.button.BackgroundImage = normalImg;
            SetImageButton(button);
        }
        public static void SetImageButton(System.Windows.Forms.Button button)
        {
            button.BackColor = System.Drawing.Color.Transparent;//透明
            button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;//没有边框
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;//鼠标移上去不会有多余阴影
            button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            button.Margin = System.Windows.Forms.Padding.Empty;//使图片贴边
        }
        public void SetMouseDisabledButton(Bitmap img)
        {
            this.disabledImg = img;
            this.button.EnabledChanged += new EventHandler(Button_EnabledChanged);
        }
        public void Button_EnabledChanged(object sender, EventArgs e)
        {
            if(!this.button.Enabled)
            {
                this.button.BackgroundImage = this.disabledImg;
            }
            else
            {
                this.button.BackgroundImage = this.normalImg;
            }
                
        }
        public void SetMouseEnterButton(Bitmap img)
        {
            this.mouseOverImg = img;
            this.button.MouseEnter += new EventHandler(Button_MouseEnter);
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            if(this.button.Enabled)
            {
                this.button.BackgroundImage = this.mouseOverImg;
            }
        }

        public void SetMouseLeaveButton(Bitmap img)
        {
            this.normalImg = img;
            this.button.MouseLeave += Button_MouseLeave;
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            if (this.button.Enabled)
            {
                this.button.BackgroundImage = this.normalImg;
            }
        }

        public void SetMousePressedButton(Bitmap img)
        {
            this.pressedImg = img;
            this.button.MouseDown += Button_MouseDown;
        }

        private void Button_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (this.button.Enabled)
            {
                this.button.BackgroundImage = this.pressedImg;
            }
        }
    }
    #region 图像虚化处理
    class ImageBeautify
    {
            
        public enum BLURSTYLE
        {
            TOP, LEFT, RIGHT, BOTTOM
        }
        public Bitmap SetEdgeBlur(string imagePath, Color backColor, int blurRange, BLURSTYLE blurStyle)
        {
            Bitmap b = new Bitmap(imagePath);
            Graphics g = Graphics.FromImage(b);
            Rectangle rect = new System.Drawing.Rectangle(b.Width - blurRange, 0, blurRange, b.Height);

            switch (blurStyle)
            {
                case BLURSTYLE.TOP:
                    {
                        rect = new System.Drawing.Rectangle(0, 0, b.Width, blurRange);
                        using (LinearGradientBrush brush = new LinearGradientBrush(rect, backColor, Color.FromArgb(0, backColor), LinearGradientMode.Vertical))
                        {
                            g.FillRectangle(brush, rect);

                        }
                    }
                    break;
                case BLURSTYLE.LEFT:
                    {
                        rect = new System.Drawing.Rectangle(0, 0, blurRange, b.Height);
                        using (LinearGradientBrush brush = new LinearGradientBrush(rect, backColor, Color.FromArgb(0, backColor), LinearGradientMode.Horizontal))
                        {
                            g.FillRectangle(brush, rect);

                        }
                    }
                    break;
                case BLURSTYLE.RIGHT:
                    {
                        rect = new System.Drawing.Rectangle(b.Width - blurRange, 0, blurRange, b.Height);
                        using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color.FromArgb(0, backColor), backColor, LinearGradientMode.Horizontal))
                        {
                            g.FillRectangle(brush, rect);

                        }
                    }
                    break;
                case BLURSTYLE.BOTTOM:
                    {
                        rect = new System.Drawing.Rectangle(0, b.Height - blurRange, b.Width, blurRange);
                        using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color.FromArgb(0, backColor), backColor, LinearGradientMode.Vertical))
                        {
                            g.FillRectangle(brush, rect);
                        }
                    }
                    break;
                default:
                    break;
            }
            return b;
        }
        public Bitmap SetEdgeBlur(Bitmap bitmap, Color backColor, int blurRange, BLURSTYLE blurStyle)
        {
            Bitmap b = bitmap;
            Graphics g = Graphics.FromImage(b);
            Rectangle rect = new System.Drawing.Rectangle(b.Width - blurRange, 0, blurRange, b.Height);

            switch (blurStyle)
            {
                case BLURSTYLE.TOP:
                    {
                        rect = new System.Drawing.Rectangle(0, 0, b.Width, blurRange);
                        using (LinearGradientBrush brush = new LinearGradientBrush(rect, backColor, Color.FromArgb(0, backColor), LinearGradientMode.Vertical))
                        {
                            g.FillRectangle(brush, rect);

                        }
                    }
                    break;
                case BLURSTYLE.LEFT:
                    {
                        rect = new System.Drawing.Rectangle(0, 0, blurRange, b.Height);
                        using (LinearGradientBrush brush = new LinearGradientBrush(rect, backColor, Color.FromArgb(0, backColor), LinearGradientMode.Horizontal))
                        {
                            g.FillRectangle(brush, rect);

                        }
                    }
                    break;
                case BLURSTYLE.RIGHT:
                    {
                        rect = new System.Drawing.Rectangle(b.Width - blurRange, 0, blurRange, b.Height);
                        using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color.FromArgb(0, backColor), backColor, LinearGradientMode.Horizontal))
                        {
                            g.FillRectangle(brush, rect);

                        }
                    }
                    break;
                case BLURSTYLE.BOTTOM:
                    {
                        rect = new System.Drawing.Rectangle(0, b.Height - blurRange, b.Width, blurRange);
                        using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color.FromArgb(0, backColor), backColor, LinearGradientMode.Vertical))
                        {
                            g.FillRectangle(brush, rect);
                        }
                    }
                    break;
                default:
                    break;
            }
            return b;
        }
        public Bitmap SetAllEdgeBlur(Bitmap bitmap, Color backColor, int blurRange)
        {
            Bitmap b = bitmap;
            Graphics g = Graphics.FromImage(b);
            Rectangle rect = new System.Drawing.Rectangle(b.Width - blurRange, 0, blurRange, b.Height);
            rect = new System.Drawing.Rectangle(0, 0, b.Width, blurRange);
            using (LinearGradientBrush brush = new LinearGradientBrush(rect, backColor, Color.FromArgb(0, backColor), LinearGradientMode.Vertical))
            {
                g.FillRectangle(brush, rect);

            }
            rect = new System.Drawing.Rectangle(0, 0, blurRange, b.Height);
            using (LinearGradientBrush brush = new LinearGradientBrush(rect, backColor, Color.FromArgb(0, backColor), LinearGradientMode.Horizontal))
            {
                g.FillRectangle(brush, rect);

            }
            rect = new System.Drawing.Rectangle(b.Width - blurRange, 0, blurRange, b.Height);
            using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color.FromArgb(0, backColor), backColor, LinearGradientMode.Horizontal))
            {
                g.FillRectangle(brush, rect);

            }
            rect = new System.Drawing.Rectangle(0, b.Height - blurRange, b.Width, blurRange);
            using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color.FromArgb(0, backColor), backColor, LinearGradientMode.Vertical))
            {
                g.FillRectangle(brush, rect);
            }
            return b;
        }
    }
    #endregion
        
}
