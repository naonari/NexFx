using System.Drawing;
using System.Windows.Forms;

namespace NexFx.Controls
{
    /// <summary>
    /// 拡張コントロールのインターフェイスを表します。
    /// </summary>
    internal interface IExControl : IExControlCore
    {
        /// <summary>
        /// フォーカス時に前景色・背景色を変更するかの判定値を設定・取得します。
        /// </summary>
        bool EnableChangeFocusedColor { get; set; }

        /// <summary>
        /// 見出し用のコントロールを設定・取得します。
        /// </summary>
        Control CaptionControl { get; set; }

        /// <summary>
        /// フォーカス時の前景色を設定・取得します。
        /// </summary>
        Color FocusedForeColor { get; set; }

        /// <summary>
        /// フォーカス時の背景色を設定・取得します。
        /// </summary>
        Color FocusedBackColor { get; set; }
    }
}
