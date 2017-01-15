/*
 * Copyright 2009-14 Simon J. Williams.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 * http://www.kajabity.com
 */

using System;
using System.Collections;
using System.IO;
using System.Text;

namespace Wipro.Support.JProperties
{
    /// <summary>
    /// Use this class for writing a set of key value pair strings to an
    /// output stream using the Java properties format.
    /// </summary>
    public class JavaPropertyWriter
    {
        private static readonly char[] Hex = new char[] { '0','1','2','3','4','5','6','7','8','9','A','B','C','D','E','F' };

        private readonly Hashtable _hashtable;

        /// <summary>
        /// Construct an instance of this class.
        /// </summary>
        /// <param name="hashtable">The Hashtable (or JavaProperties) instance
        /// whose values are to be written.</param>
        public JavaPropertyWriter(Hashtable hashtable)
        {
            this._hashtable = hashtable;
        }

        /// <summary>
        /// Write the properties to the output stream.
        /// </summary>
        /// <param name="stream">The output stream where the properties are written.</param>
        /// <param name="comments">Optional comments that are placed at the beginning of the output.</param>
        public void Write(Stream stream, string comments)
        {
            //  Create a writer to output to an ISO-8859-1 encoding (code page 28592).
            var writer = new StreamWriter(stream, Encoding.GetEncoding(28592));

            //  28592              iso-8859-2                   Central European (ISO)
            //  28591              iso-8859-1                   Western European (ISO)
            //  from http://msdn.microsoft.com/en-us/library/system.text.encodinginfo.getencoding.aspx

            if (comments != null)
            {
                writer.WriteLine("# " + comments);
            }

            writer.WriteLine("# " + DateTime.Now);

            for (var e = _hashtable.Keys.GetEnumerator(); e.MoveNext();)
            {
                var key = e.Current.ToString();
                var val = _hashtable[ key ].ToString();

                writer.WriteLine(escapeKey(key) + "=" + escapeValue(val));
            }

            writer.Flush();
        }

        /// <summary>
        /// Escape the string as a Key with character set ISO-8859-1 -
        /// the characters 0-127 are US-ASCII and we will escape any others.  The passed string is Unicode which extends
        /// ISO-8859-1 - so all is well.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string escapeKey(string s)
        {
            var buf = new StringBuilder();
            var first = true;

            foreach (var c in s)
            {
                //  Avoid confusing with a comment: '!' (33), '#' (35).
                if (first)
                {
                    first = false;
                    if (c == '!' || c == '#')
                    {
                        buf.Append('\\');
                    }
                }

                switch (c)
                {
                    case '\t':  //  =09 U+0009  HORIZONTAL TABULATION   \t
                        buf.Append('\\').Append('t');
                        break;
                    case '\n':  //  =0A U+000A  LINE FEED               \n
                        buf.Append('\\').Append('n');
                        break;
                    case '\f':  //  =0C U+000C  FORM FEED               \f
                        buf.Append('\\').Append('f');
                        break;
                    case '\r':  //  =0D U+000D  CARRIAGE RETURN         \r
                        buf.Append('\\').Append('r');
                        break;

                    case ' ':   //  32: ' '
                    case ':':   //  58: ':'
                    case '=':   //  61: '='
                    case '\\':  //  92: '\'
                        buf.Append('\\').Append(c);
                        break;

                    default:
                        if (c > 31 && c < 127)
                        {
                            buf.Append(c);
                        }
                        else
                        {
                            buf.Append('\\').Append('u');
                            buf.Append(Hex[(c >> 12) & 0xF]);
                            buf.Append(Hex[(c >> 8) & 0xF]);
                            buf.Append(Hex[(c >> 4) & 0xF]);
                            buf.Append(Hex[c & 0xF]);
                        }
                        break;
                }
            }

            return buf.ToString();
        }

        private string escapeValue(string s)
        {
            var buf = new StringBuilder();
            var first = true;

            foreach (var c in s)
            {
                //  Handle value starting with whitespace.
                if (first)
                {
                    first = false;
                    if (c == ' ')
                    {
                        buf.Append('\\').Append(' ');
                        continue;
                    }
                    else if (c == '\t') //  =09 U+0009  HORIZONTAL TABULATION   \t
                    {
                        buf.Append('\\').Append('t');
                        continue;
                    }
                }

                switch (c)
                {
                    case '\t':  //  =09 U+0009  HORIZONTAL TABULATION   \t
                        buf.Append('\t');  //OK after first position.
                        break;
                    case '\n':  //  =0A U+000A  LINE FEED               \n
                        buf.Append('\\').Append('n');
                        break;
                    case '\f':  //  =0C U+000C  FORM FEED               \f
                        buf.Append('\\').Append('f');
                        break;
                    case '\r':  //  =0D U+000D  CARRIAGE RETURN         \r
                        buf.Append('\\').Append('r');
                        break;
                    case '\\':  //  92: '\'
                        buf.Append('\\').Append(c);
                        break;

                    default:
                        if (c > 31 && c < 127)
                        {
                            buf.Append(c);
                        }
                        else
                        {
                            buf.Append('\\').Append('u');
                            buf.Append(Hex[(c >> 12) & 0xF]);
                            buf.Append(Hex[(c >> 8) & 0xF]);
                            buf.Append(Hex[(c >> 4) & 0xF]);
                            buf.Append(Hex[c & 0xF]);
                        }
                        break;
                }
            }

            return buf.ToString();
        }

    }
}
