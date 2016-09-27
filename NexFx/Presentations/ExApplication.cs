using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace NexFx.Presentations
{
    public sealed class ExApplication
    {
        /// <summary>
        /// 現在のスレッドで標準のアプリケーション メッセージ ループの実行を開始し、指定したフォームを表示します。
        /// </summary>
        /// <param name="mainForm">表示するフォームを表す System.Windows.Forms.Form。</param>
        /// <param name="forbidMultiple">多重起動の抑止を表す値。</param>
        public static void Run(Form mainForm, bool forbidMultiple)
        {
            // 多重起動の禁止するかを判定します。
            if (forbidMultiple)
            {
                // Mutexをインスタンス化します。
                using (var mutex = new Mutex(false, Assembly.GetCallingAssembly().GetName().Name))
                {
                    // 多重起動の判定を行います。
                    if (mutex.WaitOne(0, false))
                    {
                        // アプリケーションを実行します。
                        Application.Run(mainForm);
                    }

                    // 本メソッドが実行されるまでガベージコレクションの対象から除外します。
                    GC.KeepAlive(mutex);

                    // Mutexを閉じます。
                    mutex.Close();
                }
            }
            else
            {
                // アプリケーションを実行します。
                Application.Run(mainForm);
            }
        }

        /// <summary>
        /// System.Windows.Forms.ApplicationContext を使用して、現在のスレッドで標準のアプリケーション メッセージ ループの実行を開始します。
        /// </summary>
        /// <param name="context">アプリケーションが実行される System.Windows.Forms.ApplicationContext。</param>
        /// <param name="forbidMultiple">多重起動の抑止を表す値。</param>
        public static void Run(ApplicationContext context, bool forbidMultiple)
        {
            // 多重起動の禁止するかを判定します。
            if (forbidMultiple)
            {
                // Mutexをインスタンス化します。
                using (var mutex = new Mutex(false, Assembly.GetCallingAssembly().GetName().Name))
                {
                    // 多重起動の判定を行います。
                    if (mutex.WaitOne(0, false))
                    {
                        // アプリケーションを実行します。
                        Application.Run(context);
                    }

                    // 本メソッドが実行されるまでガベージコレクションの対象から除外します。
                    GC.KeepAlive(mutex);

                    // Mutexを閉じます。
                    mutex.Close();
                }
            }
            else
            {
                // アプリケーションを実行します。
                Application.Run(context);
            }
        }

        /// <summary>
        /// 現在のスレッドで標準のアプリケーション メッセージ ループの実行を、フォームなしで開始します。
        /// </summary>
        /// <param name="forbidMultiple">多重起動の抑止を表す値。</param>
        public static void Run(bool forbidMultiple)
        {
            // 多重起動の禁止するかを判定します。
            if (forbidMultiple)
            {
                // Mutexをインスタンス化します。
                using (var mutex = new Mutex(false, Assembly.GetCallingAssembly().GetName().Name))
                {
                    // 多重起動の判定を行います。
                    if (mutex.WaitOne(0, false))
                    {
                        // アプリケーションを実行します。
                        Application.Run();
                    }

                    // 本メソッドが実行されるまでガベージコレクションの対象から除外します。
                    GC.KeepAlive(mutex);

                    // Mutexを閉じます。
                    mutex.Close();
                }
            }
            else
            {
                // アプリケーションを実行します。
                Application.Run();
            }
        }
    }
}
