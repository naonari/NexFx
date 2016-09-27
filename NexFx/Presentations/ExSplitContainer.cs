using System.ComponentModel;
using System.Windows.Forms;

namespace NexFx.Presentations
{
    /// <summary>
    /// 拡張SplitContainerを表します。
    /// </summary>
    [ToolboxItem(true)]
    public class ExSplitContainer : SplitContainer , IExControlCore
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
        /// ExSplitContainer.Orientationに応じて、ExSplitContainerの左側または上側の拡張パネルを取得します。
        /// </summary>
        public new ExPanel Panel1 { get; set; } = new ExPanel();

        /// <summary>
        /// ExSplitContainer.Orientationに応じて、ExSplitContainerの右側または下側の拡張パネルを取得します。
        /// </summary>
        public new ExPanel Panel2 { get; set; } = new ExPanel();

        /// <summary>
        /// 拡張コントロールをサービスに設定します。
        /// </summary>
       　public void SetExControlService()
        {
            // 自身のインスタンスをサービスに渡します。
            var eccs = new Services.ExControlCoreService<ExSplitContainer>(this);

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