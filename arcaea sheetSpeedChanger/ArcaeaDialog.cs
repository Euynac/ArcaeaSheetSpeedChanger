using Euynac.WinformUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcaeaSpeedChanger
{
    public partial class ArcaeaDialog : ControlLayer
    {
        public ArcaeaDialog(String text = "")
        {
            InitializeComponent();
            SetDialogText(text);
            this.SetAnimateSpeed(1);
            this.BackColor = Color.FromArgb(82, 67, 95);
            TransparencyKey = BackColor;
            GlobalVar.arcaeaFont.ArcaeaLabel(messageLabel, 10);
            messageLabel.ForeColor = Color.Black;
            GlobalVar.arcaeaFont.ArcaeaLabel(titleLabel, 12);
            GlobalVar.arcaeaFont.ArcaeaButton(confirmBtn, 12);
            ImageButton imgConfirmBtn = new ImageButton(confirmBtn, global::ArcaeaSpeedChanger.Properties.Resources.button_single);
            imgConfirmBtn.SetMouseLeaveButton(global::ArcaeaSpeedChanger.Properties.Resources.button_single);
            imgConfirmBtn.SetMouseEnterButton(global::ArcaeaSpeedChanger.Properties.Resources.button_single_3d);
            imgConfirmBtn.SetMousePressedButton(global::ArcaeaSpeedChanger.Properties.Resources.button_single_pressed);
        }

        private void ConfirmBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            this.SkinClose();
        }

        public void SetDialogText(String text)
        {
            this.messageLabel.Text = text;
        }
        public void SetDialogTitle(String text)
        {
            this.titleLabel.Text = text;
        }
    }
}
