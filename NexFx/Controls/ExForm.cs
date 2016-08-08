using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace NexFx.Controls
{
    /// <summary>
    /// 拡張Formを表します。
    /// </summary>
    [ToolboxItem(true)]
    public partial class ExForm : Form, IExControlCore
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
        /// Enterキーによるフォーカス遷移の有効値を示します。(画面読込時の値のみ有効。)
        /// </summary>
        [Browsable(true)]
        [Category("動作")]
        [DefaultValue(true)]
        [Description("Enterキーによるフォーカス遷移の有効値を示します。(画面読込時の値のみ有効。)")]
        public bool EnableEnterTransition { get; set; } = true;

        /// <summary>
        /// Escキーによる画面終了の有効値を示します。(画面読込時の値のみ有効。)
        /// </summary>
        [Browsable(true)]
        [Category("動作")]
        [DefaultValue(false)]
        [Description("Escキーによる画面終了の有効値を示します。(画面読込時の値のみ有効。)")]
        public bool EnableEscClose { get; set; } = false;

        /// <summary>
        /// 拡張コントロールのサービス設定済みフラグを取得します。
        /// </summary>
        [Browsable(false)]
        public bool DoneSetExControlService { get; private set; } = false;

        /// <summary>
        /// 背景色をグラデーション表示するかを設定します。
        /// </summary>
        /// <returns></returns>
        [Browsable(true)]
        [Category("動作")]
        [DefaultValue(true)]
        [Description("背景色をグラデーション表示するかを設定します。")]
        public bool EnableBackgroundGradation { get; set; } = true;

        /// <summary>
        /// 背景色グラデーションする色を指定します。
        /// </summary>
        /// <returns></returns>
        [Browsable(true)]
        [Category("表示")]
        [DefaultValue(typeof(SystemColors), "Control")]
        [Description("背景色グラデーションする色を指定します。")]
        public Color GradationColor1 { get; set; } = SystemColors.Control;

        /// <summary>
        /// 背景色グラデーションする色を指定します。
        /// </summary>
        /// <returns></returns>
        [Browsable(true)]
        [Category("表示")]
        [DefaultValue(typeof(SystemColors), "Control")]
        [Description("背景色グラデーションする色を指定します。")]
        public Color GradationColor2 { get; set; } = SystemColors.Control;

        /// <summary>
        /// グラデーションの方向を設定します。
        /// </summary>
        /// <returns></returns>
        [Browsable(true)]
        [Category("表示")]
        [DefaultValue(typeof(LinearGradientMode), "ForwardDiagonal")]
        [Description("グラデーションの方向を設定します。")]
        public LinearGradientMode LinearGradientMode { get; set; } = LinearGradientMode.ForwardDiagonal;

        /// <summary>
        /// コンストラクタ定義。
        /// </summary>
        public ExForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 画面読込時の処理を行います。
        /// </summary>
        private void ExForm_Load(object sender, EventArgs e)
        {
            // Enterキーによるフォーカス遷移の有効値を判定します。
            if (this.EnableEnterTransition)
            {
                // AcceptButtonを無効します。
                this.AcceptButton = null;

                // キーイベントをフォームで受け取ります。
                this.KeyPreview = true;

                // KeyDownイベントハンドラを追加
                this.KeyDown += new KeyEventHandler(this.EnterKey_KeyDown);
            }

            // Escキーによる画面終了の有効値を判定します。
            if (this.EnableEscClose)
            {
                // KeyDownイベントハンドラを追加
                this.KeyDown += new KeyEventHandler(this.EscKey_KeyDown);
            }

            // 拡張コントロールをサービスに設定します。
            this.SetExControlService();
        }

        /// <summary>
        /// 画面描画時の処理を行います。
        /// </summary>
        private void ExForm_Paint(object sender, PaintEventArgs e)
        {
            if (this.EnableBackgroundGradation)
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, GradationColor1, GradationColor2, this.LinearGradientMode))
                {
                    e.Graphics.FillRectangle(brush, this.ClientRectangle);
                }
            }
        }

        /// <summary>
        /// Escキー押下時の処理を行います。
        /// </summary>
        private void EscKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                //アプリケーションを終了する。
                this.Close();

                e.Handled = true;
                //.NET Framework 2.0以降
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Enterキー押下時の処理を行います。
        /// </summary>
        private void EnterKey_KeyDown(object sender, KeyEventArgs e)
        {
            // 押下されたキーを判定します。
            if (e.KeyCode.Equals(Keys.Enter) && !e.Alt && !e.Control)
            {
                //Shiftが押されている時は前のコントロールのフォーカスを移動
                this.ProcessTabKey(!e.Shift);

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// 拡張コントロールをサービスに設定します。
        /// </summary>
       　public void SetExControlService()
        {
            // 自身のインスタンスをサービスに渡します。
            var eccs = new Services.ExControlCoreService<ExForm>(this);

            // 格納されているコントロールにて走査します。
            foreach (Control ctrl in this.Controls)
            {
                // 拡張コントロールコアに変換します。
                var exCtrl = ctrl as IExControlCore;

                // 拡張コントロールに変換されているかを判定、またサービス設定済みフラグを判定します。
                if (exCtrl != null && !exCtrl.DoneSetExControlService)
                    // 拡張コントロールのサービス設定済みフラグを取得します。
                    exCtrl.SetExControlService();
            }

            // 拡張コントロールのサービス設定済みフラグを設定します。
            this.DoneSetExControlService = true;
        }
    }
}
