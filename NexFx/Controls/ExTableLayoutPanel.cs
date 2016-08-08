using System.ComponentModel;
using System.Windows.Forms;

namespace NexFx.Controls
{
    /// <summary>
    /// 拡張TableLayoutPanelを表します。
    /// </summary>
    [ToolboxItem(true)]
    public class ExTableLayoutPanel : TableLayoutPanel, IExControlCore
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
        /// 拡張コントロールのサービス設定済みフラグを取得します。
        /// </summary>
        [Browsable(false)]
        public bool DoneSetExControlService { get; private set; } = false;

        /// <summary>
        /// 拡張コントロールをサービスに設定します。
        /// </summary>
       　public void SetExControlService()
        {
            // 自身のインスタンスをサービスに渡します。
            var eccs = new Services.ExControlCoreService<ExTableLayoutPanel>(this);

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