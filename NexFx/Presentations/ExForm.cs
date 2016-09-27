using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace NexFx.Presentations
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
        /// 前回終了時の表示位置を再現します。(画面読込時の値のみ有効。)
        /// </summary>
        [Browsable(true)]
        [Category("動作")]
        [DefaultValue(false)]
        [Description("前回終了時の表示位置を再現します。(画面読込時の値のみ有効。)")]
        public bool DuplicateLastTimePosition { get; set; } = false;

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
            // 画面の表示位置を設定します。
            this.SetFormPosition();

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
        /// フォーム終了後の処理を行います。
        /// </summary>
        private void ExForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 画面位置を保存します。
            this.SaveFormPosition();
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

        // 画面位置設定ファイルのパス。
        private string _formConfigPath;

        // 画面位置設定ファイルの拡張子。
        private static readonly string POSITION_CONFIG_EXTENSION = ".positionConfig";

        // トップエレメント。
        private static readonly string TOP_ELEMENT = "configuration";

        // ポジションエレメント。
        private static readonly string POSITION_ELEMENT = "position";

        // 左部位置属性。
        private static readonly string LEFT_ATTRIBUTE = "left";

        // 上部位置属性。
        private static readonly string TOP_ATTRIBUTE = "top";

        /// <summary>
        /// 画面位置を設定します。
        /// </summary>
        private void SetFormPosition()
        {
            // 画面位置設定ファイルのパスを取得します。
            this._formConfigPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), this.Name + POSITION_CONFIG_EXTENSION);

            // 前回終了時の表示位置を再現するかを判定します。
            if (!this.DuplicateLastTimePosition) return;

            // 画面位置設定ファイルが存在しない場合は終了します。
            if (!File.Exists(this._formConfigPath)) return;

            // 画面位置設定ファイルの読み込みます。
            var xDoc = XDocument.Load(this._formConfigPath);

            // 画面位置設定ファイルから値を取得します。。
            var leftString = xDoc.Element(TOP_ELEMENT).Element(POSITION_ELEMENT).Attribute(LEFT_ATTRIBUTE).Value;
            var topString = xDoc.Element(TOP_ELEMENT).Element(POSITION_ELEMENT).Attribute(TOP_ATTRIBUTE).Value;

            // 位置用の変数。
            int left, top;

            // 取得した値をキャストします。
            if (!int.TryParse(leftString, out left) || !int.TryParse(topString, out top)) return;

            // 位置を設定します。
            this.StartPosition = FormStartPosition.Manual;
            this.Left = left;
            this.Top = top;
        }

        /// <summary>
        /// 画面位置を保存します。
        /// </summary>
        private void SaveFormPosition()
        {
            // 前回終了時の表示位置を再現するかを判定します。
            if (!this.DuplicateLastTimePosition) return;

            // 位置を取得します。
            var left = this.Left;
            var top = this.Top;

            // 画面位置設定ファイルが存在を判定します。
            if (File.Exists(this._formConfigPath))
            {
                // 画面位置設定ファイルの読み込みます。
                var xDoc = XDocument.Load(this._formConfigPath);

                // 画面位置設定ファイルから値を取得します。
                xDoc.Element(TOP_ELEMENT).Element(POSITION_ELEMENT).Attribute(LEFT_ATTRIBUTE).Value = left.ToString();
                xDoc.Element(TOP_ELEMENT).Element(POSITION_ELEMENT).Attribute(TOP_ATTRIBUTE).Value = top.ToString();

                // 画面位置設定ファイルを保存します。
                xDoc.Save(this._formConfigPath);
            }
            else
            {
                // 画面位置設定ファイルの定義します。
                var xDoc = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                         new XElement(TOP_ELEMENT, new XElement(POSITION_ELEMENT, new XAttribute(LEFT_ATTRIBUTE, left.ToString()), new XAttribute(TOP_ATTRIBUTE, top.ToString()))));

                // 画面位置設定ファイルを保存します。
                xDoc.Save(this._formConfigPath);
            }
        }
    }
}
