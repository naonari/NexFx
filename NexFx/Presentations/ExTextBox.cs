using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NexFx.Presentations
{
    /// <summary>
    /// 拡張TextBoxを表します。
    /// </summary>
    [ToolboxItem(true)]
    public class ExTextBox : TextBox, IExControl
    {
        /// <summary>
        /// キーとなる文字列を設定・取得します。
        /// </summary>
        [Browsable(true)]
        [Category("動作")]
        [DefaultValue("")]
        [Description("キーとなる文字列を設定・取得します。")]
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// フォーカス時に前景色・背景色を変更するかの判定値を設定・取得します。
        /// </summary>
        [Browsable(true)]
        [Category("表示")]
        [DefaultValue(true)]
        [Description("フォーカス時に前景色・背景色を変更するかの判定値を設定・取得します。")]
        public bool EnableChangeFocusedColor { get; set; } = true;

        /// <summary>
        /// 見出し用のコントロールを設定・取得します。
        /// </summary>
        [Browsable(true)]
        [Category("表示")]
        [Description("見出し用のコントロールを設定・取得します。")]
        public Control CaptionControl { get; set; }

        /// <summary>
        /// フォーカス時の前景色を設定・取得します。
        /// </summary>
        [Browsable(true)]
        [Category("表示")]
        [DefaultValue(typeof(SystemColors), "WindowText")]
        [Description("フォーカス時の前景色を設定・取得します。")]
        public Color FocusedForeColor { get; set; } = SystemColors.WindowText;

        /// <summary>
        /// フォーカス時の背景色を設定・取得します。
        /// </summary>
        [Browsable(true)]
        [Category("表示")]
        [DefaultValue(typeof(SystemColors), "Window")]
        [Description("フォーカス時の背景色を設定・取得します。")]
        public Color FocusedBackColor { get; set; } = SystemColors.Window;

        private string _placeholderText = string.Empty;

        /// <summary>
        /// テキストが空の場合に表示する文字列を取得・設定します。
        /// </summary>
        [Browsable(true)]
        [Category("表示")]
        [DefaultValue("")]
        [Description("テキストが空の場合に表示する文字列です。")]
        [RefreshProperties(RefreshProperties.Repaint)]
        public string PlaceholderText
        {
            get
            {
                return this._placeholderText;
            }
            set
            {
                this._placeholderText = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// 拡張コントロールのサービス設定済みフラグを取得します。
        /// </summary>
        [Browsable(false)]
        public bool DoneSetExControlService { get; private set; } = false;

        /// <summary>
        /// 拡張コントロールのサービスを設定します。
        /// </summary>
        public void SetExControlService()
        {
            // 自身のインスタンスをサービスに渡します。
            var ecs = new Services.ExControlService<ExTextBox>(this);
            var eccs = new Services.ExControlCoreService<ExTextBox>(this);

            // 拡張コントロールのサービス設定済みフラグを設定します。
            this.DoneSetExControlService = true;
        }

        ///<summary>
        ///描画拡張APIの処理を行います。
        ///</summary>
        ///<param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            const int WM_PAINT = 0x000F;
            base.WndProc(ref m);
            if (m.Msg == WM_PAINT && string.IsNullOrEmpty(this.Text) && string.IsNullOrEmpty(this.PlaceholderText) == false)
            {
                using (Graphics g = Graphics.FromHwnd(this.Handle))
                {   //テキストボックス内の適切な座標に描画
                    Rectangle rect = this.ClientRectangle;
                    rect.Offset(1, 1);
                    TextRenderer.DrawText(g, this.PlaceholderText, this.Font,
                        rect, SystemColors.ControlDark, TextFormatFlags.Top | TextFormatFlags.Left);
                }
            }
        }
    }
}