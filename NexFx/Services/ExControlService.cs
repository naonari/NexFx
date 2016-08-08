using System;
using System.Drawing;
using System.Windows.Forms;
using NexFx.Controls;

namespace NexFx.Services
{
    /// <summary>
    /// 拡張コントロールのサービスを表します。
    /// </summary>
    /// <typeparam name="T">拡張コントロールの型。</typeparam>
    internal class ExControlService<T>
        where T : Control, IExControl
    {
        /// <summary>拡張コントロール。</summary>
        private T _control;

        /// <summary>規定の前景色。</summary>
        private Color _defaultForeColor;

        /// <summary>見出し用のコントロールの規定の前景色。</summary>
        private Color _defaultCaptionForeColor;

        /// <summary>規定の背景色。</summary>
        private Color _defaultBackColor;

        /// <summary>見出し用のコントロールの規定の背景色。</summary>
        private Color _defaultCaptionBackColor;

        /// <summary>
        /// コンストラクタ定義。
        /// </summary>
        /// <param name="control">対象の拡張コントロールを設定します。</param>
        public ExControlService(T control)
        {
            // 拡張コントロールを格納。
            this._control = control;

            // 拡張コントロールのEnterイベントにサービスのEnterメソッドを付与。
            this._control.Enter += new EventHandler(this.Enter);

            // 拡張コントロールのLeaveイベントにサービスのLeaveメソッドを付与。
            this._control.Leave += new EventHandler(this.Leave);
        }

        /// <summary>
        /// 拡張コントロールのEnterイベントに付与される処理を行います。
        /// </summary>
        private void Enter(object sender, EventArgs e)
        {
            // フォーカス時に前景色・背景色を変更するかを判定します。
            if (this._control.EnableChangeFocusedColor)
            {
                // 拡張コントロールの前景色の規定値に現在の前景色を設定します。
                this._defaultForeColor = this._control.ForeColor;

                // 拡張コントロールの前景色にフォーカス時の前景色を設定します。
                this._control.ForeColor = this._control.FocusedForeColor;

                // 拡張コントロールがButtonコントロールかを判定、また背景色が規定値かを判定します。
                if (this._control is ExButton && (this._control as ExButton).BackColor == Button.DefaultBackColor)
                {
                    // ボタンの背景色の規定値にシステム定義色を設定します。
                    this._defaultBackColor = Color.Transparent;
                }
                else
                {
                    // 拡張コントロールの背景色の規定値に現在の背景色を設定します。
                    this._defaultBackColor = this._control.BackColor;
                }

                // 拡張コントロールの背景色にフォーカス時の背景色を設定します。
                this._control.BackColor = this._control.FocusedBackColor;

                // 見出し用のコントロールが設定されているかを判定します。
                if (this._control.CaptionControl != null)
                {
                    // 見出し用のコントロールの前景色の規定値に現在の前景色を設定します。
                    this._defaultCaptionForeColor = this._control.CaptionControl.ForeColor;

                    // 見出し用のコントロールの背景色の規定値に現在の背景色を設定します。
                    this._defaultCaptionBackColor = this._control.CaptionControl.BackColor;

                    // 見出し用のコントロールの前景色にフォーカス時の前景色を設定します。
                    this._control.CaptionControl.ForeColor = this._control.FocusedForeColor;

                    // 見出し用のコントロールの背景色にフォーカス時の背景色を設定します。
                    this._control.CaptionControl.BackColor = this._control.FocusedBackColor;
                }
            }
        }

        /// <summary>
        /// 拡張コントロールのLeaveイベントに付与される処理を行います。
        /// </summary>
        private void Leave(object sender, EventArgs e)
        {
            // フォーカス時に前景色・背景色を変更するかを判定します。
            if (this._control.EnableChangeFocusedColor)
            {
                // 拡張コントロールの前景色を規定値に戻します。
                this._control.ForeColor = this._defaultForeColor;

                // 拡張コントロールの背景色を規定値に戻します。
                this._control.BackColor = this._defaultBackColor;

                // 見出し用のコントロールが設定されているかを判定します。
                if (this._control.CaptionControl != null)
                {
                    // 見出し用のコントロールの前景色を規定値に戻します。
                    this._control.CaptionControl.ForeColor = this._defaultCaptionForeColor;

                    // 見出し用のコントロールの背景色を規定値に戻します。
                    this._control.CaptionControl.BackColor = this._defaultCaptionBackColor;
                }
            }
        }
    }
}