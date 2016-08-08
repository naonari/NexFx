namespace NexFx.Controls
{
    /// <summary>
    /// 拡張コントロールコアのインターフェイスを表します。
    /// </summary>
    internal interface IExControlCore
    {
        /// <summary>
        /// キーとなる文字列を設定・取得します。
        /// </summary>
        string Key { get; set; }

        /// <summary>
        /// 拡張コントロールのサービス設定済みフラグを取得します。
        /// </summary>
        bool DoneSetExControlService { get; }

        /// <summary>
        /// 拡張コントロールのサービスを設定します。
        /// </summary>
        void SetExControlService();
    }
}
