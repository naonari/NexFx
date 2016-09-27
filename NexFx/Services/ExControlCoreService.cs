using System.Windows.Forms;
using NexFx.Presentations;

namespace NexFx.Services
{
    /// <summary>
    /// 拡張コントロールコアのサービスを表します。
    /// </summary>
    /// <typeparam name="T">拡張コントロールの型。</typeparam>
    internal class ExControlCoreService<T> where T : Control, IExControlCore
    {
        /// <summary>拡張コントロール。</summary>
        private T _control;

        /// <summary>
        /// コンストラクタ定義。
        /// </summary>
        /// <param name="control">対象の拡張コントロールを設定します。</param>
        public ExControlCoreService(T control)
        {
            // 拡張コントロールを格納。
            this._control = control;
        }
    }
}
