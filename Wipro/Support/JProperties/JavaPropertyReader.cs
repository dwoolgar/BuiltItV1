using System.Collections;
using System.IO;
using System.Text;

namespace Wipro.Support.JProperties
{
	/// <summary>
	/// This class reads Java style properties from an input stream.  
	/// </summary>
	public class JavaPropertyReader
	{
		private const int MATCH_end_of_input = 1;
		private const int MATCH_terminator = 2;
		private const int MATCH_whitespace = 3;
		private const int MATCH_any = 4;

		private const int ACTION_add_to_key = 1;
		private const int ACTION_add_to_value = 2;
		private const int ACTION_store_property = 3;
		private const int ACTION_escape = 4;
		private const int ACTION_ignore = 5;

		private const int STATE_start = 0;
		private const int STATE_comment = 1;
		private const int STATE_key = 2;
		private const int STATE_key_escape = 3;
		private const int STATE_key_ws = 4;
		private const int STATE_before_separator = 5;
		private const int STATE_after_separator = 6;
		private const int STATE_value = 7;
		private const int STATE_value_escape = 8;
		private const int STATE_value_ws = 9;
		private const int STATE_finish = 10;

		private static string [] _stateNames = new string[] 
		{ "STATE_start", "STATE_comment", "STATE_key", "STATE_key_escape", "STATE_key_ws", 
			"STATE_before_separator", "STATE_after_separator", "STATE_value", "STATE_value_escape", 
			"STATE_value_ws", "STATE_finish" };
		
		private static int [][] states = new int[][] {
			new int[]{//STATE_start
				MATCH_end_of_input,	STATE_finish,			ACTION_ignore,
				MATCH_terminator,	STATE_start,			ACTION_ignore,
				'#',				STATE_comment,			ACTION_ignore,
				'!',				STATE_comment,			ACTION_ignore,
				MATCH_whitespace,	STATE_start,			ACTION_ignore,
				'\\',				STATE_key_escape,		ACTION_escape,
				':',				STATE_after_separator,	ACTION_ignore,
				'=',				STATE_after_separator,	ACTION_ignore,
				MATCH_any,			STATE_key,				ACTION_add_to_key,
			},
			new int[]{//STATE_comment
				MATCH_end_of_input,	STATE_finish,			ACTION_ignore,
				MATCH_terminator,	STATE_start,			ACTION_ignore,
				MATCH_any,			STATE_comment,			ACTION_ignore,
			},
			new int[]{//STATE_key
				MATCH_end_of_input,	STATE_finish,			ACTION_store_property,
				MATCH_terminator,	STATE_start,			ACTION_store_property,
				MATCH_whitespace,	STATE_before_separator,	ACTION_ignore,
				'\\',				STATE_key_escape,		ACTION_escape,
				':',				STATE_after_separator,	ACTION_ignore,
				'=',				STATE_after_separator,	ACTION_ignore,
				MATCH_any,			STATE_key,				ACTION_add_to_key,
			},
			new int[]{//STATE_key_escape
				MATCH_terminator,	STATE_key_ws,			ACTION_ignore,
				MATCH_any,			STATE_key,				ACTION_add_to_key,
			},
			new int[]{//STATE_key_ws
				MATCH_end_of_input,	STATE_finish,			ACTION_store_property,
				MATCH_terminator,	STATE_start,			ACTION_store_property,
				MATCH_whitespace,	STATE_key_ws,			ACTION_ignore,
				'\\',				STATE_key_escape,		ACTION_escape,
				':',				STATE_after_separator,	ACTION_ignore,
				'=',				STATE_after_separator,	ACTION_ignore,
				MATCH_any,			STATE_key,				ACTION_add_to_key,
			},
			new int[]{//STATE_before_separator
				MATCH_end_of_input,	STATE_finish,			ACTION_store_property,
				MATCH_terminator,	STATE_start,			ACTION_store_property,
				MATCH_whitespace,	STATE_before_separator,	ACTION_ignore,
				'\\',				STATE_value_escape,		ACTION_escape,
				':',				STATE_after_separator,	ACTION_ignore,
				'=',				STATE_after_separator,	ACTION_ignore,
				MATCH_any,			STATE_value,			ACTION_add_to_value,
			},
			new int[]{//STATE_after_separator
				MATCH_end_of_input,	STATE_finish,			ACTION_store_property,
				MATCH_terminator,	STATE_start,			ACTION_store_property,
				MATCH_whitespace,	STATE_after_separator,	ACTION_ignore,
				'\\',				STATE_value_escape,		ACTION_escape,
				MATCH_any,			STATE_value,			ACTION_add_to_value,
			},
			new int[]{//STATE_value
				MATCH_end_of_input,	STATE_finish,			ACTION_store_property,
				MATCH_terminator,	STATE_start,			ACTION_store_property,
				'\\',				STATE_value_escape,		ACTION_escape,
				MATCH_any,			STATE_value,			ACTION_add_to_value,
			},
			new int[]{//STATE_value_escape
				MATCH_terminator,	STATE_value_ws,			ACTION_ignore,
				MATCH_any,			STATE_value,				ACTION_add_to_value
			},
			new int[]{//STATE_value_ws
				MATCH_end_of_input,	STATE_finish,			ACTION_store_property,
				MATCH_terminator,	STATE_start,			ACTION_store_property,
				MATCH_whitespace,	STATE_value_ws,			ACTION_ignore,
				'\\',				STATE_value_escape,		ACTION_escape,
				MATCH_any,			STATE_value,			ACTION_add_to_value,
			}
		};

		private readonly Hashtable _hashtable;

		private const int BufferSize =  1000;

		private bool _escaped;
		private readonly StringBuilder _keyBuilder = new StringBuilder();
		private readonly StringBuilder _valueBuilder = new StringBuilder();
		
		/// <summary>
		/// Construct a reader passing a reference to a Hashtable (or JavaProperties) instance
		/// where the keys are to be stored.
		/// </summary>
		/// <param name="hashtable">A reference to a hashtable where the key-value pairs can be stored.</param>
		public JavaPropertyReader(Hashtable hashtable)
		{
			this._hashtable = hashtable;
		}

		public void Parse(Stream stream)
		{
			_reader = new BufferedStream(stream, BufferSize);

			var state = STATE_start;
			do
			{
				var ch = NextChar();

				var matched = false;

				for (var s = 0; s < states[state].Length; s += 3)
				{
					if (Matches(states[state][s], ch))
					{
						//Debug.WriteLine( stateNames[ state ] + ", " + (s/3) + ", " + ch + (ch>20?" (" + (char) ch + ")" : "") );
						matched = true;
						DoAction(states[state][s + 2], ch);

						state = states[state][s + 1];
						break;
					}
				}

				if (!matched)
				{
					throw new ParseException("Unexpected character at " + 1 + ": <<<" + ch + ">>>");
				}
			} while (state != STATE_finish);
		}

		private bool Matches(int match, int ch)
		{
			switch(match)
			{
				case MATCH_end_of_input:
					return ch == -1;

				case MATCH_terminator:
					if (ch == '\r')
					{
						if (PeekChar() == '\n')
						{
							_saved = false;
						}
						return true;
					}
					else if (ch == '\n')
					{
						return true;
					}
					return false;

				case MATCH_whitespace:
					return ch == ' ' || ch == '\t' || ch == '\f';

				case MATCH_any:
					return true;

				default:
					return ch == match;
			}
		}

		private void DoAction(int action, int ch)
		{
			switch(action)
			{
				case ACTION_add_to_key:
					_keyBuilder.Append(EscapedChar(ch));
					_escaped = false;
					break;

				case ACTION_add_to_value:
					_valueBuilder.Append(EscapedChar(ch));
					_escaped = false;
					break;

				case ACTION_store_property:
					//Debug.WriteLine( keyBuilder.ToString() + "=" + valueBuilder.ToString() );
                    // Corrected to avoid duplicate entry errors - thanks to David Tanner.
					_hashtable[_keyBuilder.ToString()] = _valueBuilder.ToString();
					_keyBuilder.Length = 0;
					_valueBuilder.Length = 0;
					_escaped = false;
					break;

				case ACTION_escape:
					_escaped = true;
					break;

				//case ACTION_ignore:
				default:
					_escaped = false;
					break;
			}
		}

		private char EscapedChar(int ch)
		{
			if (_escaped)
			{
				switch (ch)
				{
					case 't':
						return '\t';
					case 'r':
						return '\r';
					case 'n':
						return '\n';
					case 'f':
						return '\f';
					case 'u':
						var uch = 0;
						for (var i = 0; i < 4; i++)
						{
							ch = NextChar();
							if (ch >= '0' && ch <='9')
							{
								uch = (uch << 4) + ch - '0';
							}
							else if (ch >= 'a' && ch <='z')
							{
								uch = (uch << 4) + ch - 'a' + 10;
							}
							else if (ch >= 'A' && ch <='Z')
							{
								uch = (uch << 4) + ch - 'A' + 10;
							}
							else
							{
								throw new ParseException( "Invalid Unicode character." );
							}
						}
						return (char) uch;
				}
			}
			
			return (char) ch;
		}

		private BufferedStream _reader;
		private int _savedChar;
		private bool _saved;

		private int NextChar()
		{
		    if (!_saved) return _reader.ReadByte();
		    _saved = false;
		    return _savedChar;
		}
		
		private int PeekChar()
		{
			if(_saved)
			{
				return _savedChar;
			}
			
			_saved = true;
			return _savedChar = _reader.ReadByte();
		}

	}
}
