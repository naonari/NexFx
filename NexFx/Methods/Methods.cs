using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace NexFx.ExMethods
{
    public static class ExStringMethods
    {
        /// <summary>
        /// 指定した文字を含むかを判定します。
        /// </summary>
        /// <param name="s">対象の文字列。</param>
        /// <param name="c">対象の文字。</param>
        /// <returns>指定した文字を含むかの判定値を返します。</returns>
        public static bool IncludeChar(this string s, char c)
        {
            return s.IndexOf(c) >= 0;
        }

        // シフトJISのエンコード。
        private static Encoding encShiftJis = Encoding.GetEncoding("Shift_JIS");

        /// <summary>
        /// 全角文字が含むかを判定します。
        /// </summary>
        /// <param name="s">対象の文字列。</param>
        /// <returns>全角文字を含むかの判定値を返します。</returns>
        public static bool IncludeFullWide(this string s)
        {
            return s.Length != encShiftJis.GetByteCount(s);
        }

        /// <summary>
        /// 全角文字が含むかを判定します。
        /// </summary>
        /// <param name="s">対象の文字列。</param>
        /// <returns>全角文字を含むかの判定値を返します。</returns>
        public static bool IncludeHalfWide(this string s)
        {
            return s.Length * 2 != encShiftJis.GetByteCount(s);
        }

        /// <summary>
        /// 文字列を反転させます。
        /// </summary>
        /// <param name="s">対象の文字列。</param>
        /// <returns>反転した文字列を返します。</returns>
        public static string Reversed(this string s)
        {
            return string.Join(string.Empty, s.Reverse());
        }

        /// <summary>
        /// 変換可能かを判定します。
        /// </summary>
        /// <typeparam name="T">変換対象の型。</typeparam>
        /// <param name="s">対象の文字列。</param>
        /// <returns>変換可能かの判定値を返します。</returns>
        public static bool EnableParse<T>(this string s) where T : struct
        {
            if (typeof(T) == typeof(bool))
            {
                var b = default(bool);
                return bool.TryParse(s, out b);
            }
            else if (typeof(T) == typeof(decimal))
            {
                var d = default(decimal);
                return decimal.TryParse(s, out d);
            }
            else if (typeof(T) == typeof(decimal))
            {
                var f = default(decimal);
                return decimal.TryParse(s, out f);
            }
            else if (typeof(T) == typeof(float))
            {
                var f = default(float);
                return float.TryParse(s, out f);
            }
            else if (typeof(T) == typeof(double))
            {
                var f = default(double);
                return double.TryParse(s, out f);
            }
            else if (typeof(T) == typeof(short))
            {
                var f = default(short);
                return short.TryParse(s, out f);
            }
            else if (typeof(T) == typeof(int))
            {
                var f = default(int);
                return int.TryParse(s, out f);
            }
            else if (typeof(T) == typeof(long))
            {
                var f = default(long);
                return long.TryParse(s, out f);
            }
            else if (typeof(T) == typeof(ushort))
            {
                var f = default(ushort);
                return ushort.TryParse(s, out f);
            }
            else if (typeof(T) == typeof(uint))
            {
                var f = default(uint);
                return uint.TryParse(s, out f);
            }
            else if (typeof(T) == typeof(ulong))
            {
                var f = default(ulong);
                return ulong.TryParse(s, out f);
            }
            else if (typeof(T) == typeof(DateTime))
            {
                var f = default(DateTime);
                return DateTime.TryParse(s, out f);
            }
            else if (typeof(T) == typeof(TimeSpan))
            {
                var f = default(TimeSpan);
                return TimeSpan.TryParse(s, out f);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 任意の値型へ変換します。
        /// 変換が出来ない場合は既定値を返します。
        /// </summary>
        /// <typeparam name="T">任意の値型。</typeparam>
        /// <param name="s">対象の文字列。</param>
        /// <param name="defaultValue">既定値。</param>
        /// <returns>
        /// 変換した値を返します。
        /// 変換出来ない場合は既定値を返します。
        /// </returns>
        public static T ParseOrDefault<T>(this string s, T defaultValue = default(T)) where T: struct
        {
            try
            {
                // 型変換を取得します。
                var converter = TypeDescriptor.GetConverter(typeof(T));

                // 型変換のインスンタンスを判定します。
                if (converter != null)
                {
                    // 型変換をした値を返します。
                    return (T)converter.ConvertFromString(s);
                }

                // 既定値を返します。
                return defaultValue;
            }
            catch
            {
                // 既定値を返します。
                return defaultValue;
            }
        }
    }

    public static class ExDictionaryMethods
    {
        /// <summary>
        /// 指定した値を持つキーを全て取得します。
        /// </summary>
        /// <typeparam name="TKey">キーの型。</typeparam>
        /// <typeparam name="TValue">値の型。</typeparam>
        /// <param name="dic">対象のディクショナリ。</param>
        /// <param name="value">指定した値。</param>
        /// <returns>指定した値を持つキーの列挙子を返します。</returns>
        public static IEnumerable<TKey> MatchAllKey<TKey, TValue>(this Dictionary<TKey, TValue> dic, TValue value)
        {
            if (dic.Values.Contains(value))
            {
                return dic.Where(x => x.Value.Equals(value)).Select(x => x.Key);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 指定した値を持つキーを取得します。
        /// </summary>
        /// <typeparam name="TKey">キーの型。</typeparam>
        /// <typeparam name="TValue">値の型。</typeparam>
        /// <param name="dic">対象のディクショナリ。</param>
        /// <param name="value">指定した値。</param>
        /// <param name="defaultKey">キーが存在しない場合のデフォルト値。</param>
        /// <returns>指定した値を持つキーを返します。</returns>
        public static TKey FirstOrDefaultKey<TKey, TValue>(this Dictionary<TKey, TValue> dic, TValue value, TKey defaultKey = default(TKey))
        {
            if (dic.Values.Contains(value))
            {
                return dic.First(x => x.Value.Equals(value)).Key;
            }
            else
            {
                return defaultKey;
            }
        }
    }
}
