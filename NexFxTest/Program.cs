using System;
using System.Windows.Forms;
using NexFx.Presentations;
using NexFx.ExMethods;

namespace NexFxTest
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ExApplication.Run(new TestForm(), true);
        }
    }
}
