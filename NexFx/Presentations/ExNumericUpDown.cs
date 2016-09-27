using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NexFx.Presentations
{
    /// <summary>
    /// 拡張NumericUpDownを表します。
    /// </summary>
    [ToolboxItem(true)]
    public class ExNumericUpDown : NumericUpDown , IExControl
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
            var ecs = new Services.ExControlService<ExNumericUpDown>(this);
            var eccs = new Services.ExControlCoreService<ExNumericUpDown>(this);

            // 拡張コントロールのサービス設定済みフラグを設定します。
            this.DoneSetExControlService = true;
        }
    }
}