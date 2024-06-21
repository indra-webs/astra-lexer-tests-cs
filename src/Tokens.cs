using Indra.Astra.Tokens;

using Meep.Tech.Collections;

namespace Indra.Astra.Tests {
    public class Tokens {
        public class Alone {
            public Lexer Lexer { get; }

            public Alone()
                => Lexer = new Lexer();

            #region Alone; With Padding

            public enum Padding {
                None = None_Left | None_Right,
                None_Left = 1,
                None_Right = 2,
                Spaces = Space_Left | Space_Right,
                Space_Left = 4,
                Space_Right = 8,
                Tabs = Tab_Left | Tab_Right,
                Tab_Left = 16,
                Tab_Right = 32,
                NewLines = NewLine_Left | NewLine_Right,
                NewLine_Left = 64,
                NewLine_Right = 128,
                Around = Left | Right,
                Left = Space_Left | Tab_Left | NewLine_Left,
                Right = Space_Right | Tab_Right | NewLine_Right,
                Optional = Around | None,
                Optional_Left = Left | None_Left,
                Optional_Right = Right | None_Right
            }

            public static IEnumerable<(IStatic type, string value)> GetAllStaticTokens()
                => Types.Static
                    .Where(t => t is not IWhitespace and not IEmpty)
                    .Select(t => (t, t.Value))
                    .ToArray();

            public static IEnumerable<object[]> GetAllStaticTokens_WithPadding
                => GetAllStaticTokens()
                    .Select(t => (t.type, t.value, padding: Padding.Optional))
                    .Update(t => t.type is Backslash, t => (t.type, t.value, Padding.Optional_Left | Padding.None_Right))
                    .Update(t => t.type is TripleDash, t => (t.type, t.value, Padding.None_Left | Padding.Optional_Right))
                    .Skip(t => t.type is CloseBlockComment)
                    .Select(t => new object[] { t.type, t.value, t.padding })
                    .ToArray();

            /// <summary>
            /// Test that a token is correctly identified when it is the only non-whitespace token in the input.
            /// </summary>
            [Theory]
            [MemberData(nameof(GetAllStaticTokens_WithPadding))]
            public void WithPadding(IStatic type, string text, Padding padding = Padding.Optional) {
                List<char> right = [],
                left = [];

                if(padding.HasFlag(Padding.None_Left)) {
                    left.Add('\0');
                }

                if(padding.HasFlag(Padding.None_Right)) {
                    right.Add('\0');
                }

                if(padding.HasFlag(Padding.Space_Left)) {
                    left.Add(' ');
                }

                if(padding.HasFlag(Padding.Space_Right)) {
                    right.Add(' ');
                }

                if(padding.HasFlag(Padding.Tab_Left)) {
                    left.Add('\t');
                }

                if(padding.HasFlag(Padding.Tab_Right)) {
                    right.Add('\t');
                }

                if(padding.HasFlag(Padding.NewLine_Left)) {
                    left.Add('\n');
                }

                if(padding.HasFlag(Padding.NewLine_Right)) {
                    right.Add('\n');
                }

                (char l, char r)[] paddingVariants =
                left.SelectMany(l => right.Select(r => (l, r))).ToArray();

                foreach((char l, char r) in paddingVariants) {
                    _assert((l, r), (padding) => {
                        string padded = $"{l}{text}{r}".Trim('\0');

                        Lexer.Lex(padded)
                            .Assert_IsSuccess()
                            .Assert_IsSingle(type, text, (l is '\0' ? 0 : 1, text.Length + (l is '\0' ? 0 : 1)));
                    }, $"Padding variant: '{debugPadding(l)}' + '{text}' + '{debugPadding(r)}' failed.");

                    static string debugPadding(char c)
                        => c switch {
                            '\0' => "",
                            ' ' => "\\s",
                            '\t' => "\\t",
                            '\n' => "\\n",
                            _ => c.ToString()
                        };
                }
            }
            #endregion

            #region Alone; With Separator (Touching)

            public enum Seperators {
                None = None_Left | None_Right,
                None_Left = 1,
                None_Right = 2,
                Comma = Comma_Left | Comma_Right,
                Comma_Left = 4,
                Comma_Right = 8,
                Semicolon = Semicolon_Left | Semicolon_Right,
                Semicolon_Left = 16,
                Semicolon_Right = 32,
                Around = Left | Right,
                Left = Comma_Left | Semicolon_Left,
                Right = Comma_Right | Semicolon_Right,
                Optional_Left = Left | None_Left,
                Optional_Right = Right | None_Right,
                Optional = Optional_Left | Optional_Right
            }

            public static IEnumerable<object[]> GetAllStaticTokens_WithSeparators
                => GetAllStaticTokens()
                    .Select(t => (t.type, t.value, separators: Seperators.Optional))
                    .Skip(t => t.type is OpenBlockComment)
                    .Skip(t => t.type is CloseBlockComment)
                    .Update(t => t.type is Backslash, t => (t.type, t.value, separators: Seperators.Optional_Left | Seperators.None_Right))
                    .Update(t => t.type is SemiColon, t => (t.type, t.value, separators: Seperators.Comma | Seperators.None))
                    .Update(t => t.type is DoubleSemiColon, t => (t.type, t.value, separators: Seperators.Comma | Seperators.None))
                    .Update(t => t.type is TripleDash, t => (t.type, t.value, separators: Seperators.None_Left | Seperators.Optional_Right))
                    .Select(t => new object[] { t.type, t.value, t.separators })
                    .ToArray();

            [Theory]
            [MemberData(nameof(GetAllStaticTokens_WithSeparators))]
            public void WithSeparators(TokenType type, string text, Seperators seperators = Seperators.Optional) {
                List<char> right = [],
                        left = [];

                if(seperators.HasFlag(Seperators.Comma_Left)) {
                    left.Add(',');
                }

                if(seperators.HasFlag(Seperators.Comma_Right)) {
                    right.Add(',');
                }

                if(seperators.HasFlag(Seperators.Semicolon_Left)) {
                    left.Add(';');
                }

                if(seperators.HasFlag(Seperators.Semicolon_Right)) {
                    right.Add(';');
                }

                if(seperators.HasFlag(Seperators.None_Left)) {
                    left.Add('\0');
                }

                if(seperators.HasFlag(Seperators.None_Right)) {
                    right.Add('\0');
                }

                (char l, char r)[] paddingVariants =
                        left.SelectMany(l => right.Select(r => (l, r))).ToArray();

                foreach((char l, char r) in paddingVariants) {
                    _assert((l, r), (padding) => {
                        string left = getSeperatorText(l);
                        string right = getSeperatorText(r);
                        string withSeperators
                            = $"{left}{text}{right}";

                        TokenType leftSeparator = getSeparatorToken(l);
                        TokenType rightSeparator = getSeparatorToken(r);
                        Lexer.Lex(withSeperators)
                            .Assert_IsSuccess()
                            .Assert_IsSequence(
                                leftSeparator,
                                type,
                                rightSeparator
                            ).Assert_At(
                                leftSeparator is None
                                    ? 0
                                    : 1,
                                type,
                                text,
                                (start: left.Length,
                                    end: text.Length + left.Length)
                            );
                    }, $"Padding variant: '{left}' + '{text}' + '{right}'` failed.");

                    static TokenType getSeparatorToken(char c)
                        => c switch {
                            ',' => Comma.Type,
                            ';' => SemiColon.Type,
                            '\0' => None.Type,
                            _ => throw new NotImplementedException(),
                        };

                    static string getSeperatorText(char c)
                        => c switch {
                            '\0' => "",
                            ',' => ",",
                            ';' => ";",
                            _ => c.ToString()
                        };
                }
            }

            #endregion

            //     #region Alone; In Quotes

            //     public enum Quotes {
            //         Single = 1,
            //         Double = 2,
            //         Backtick = 4,
            //         All = Single | Double | Backtick
            //     }

            //     [Theory]
            //     // Separators
            //     [InlineData(",", Comma)]
            //     [InlineData(";", Semicolon)]

            //     // Delimiters
            //     [InlineData("(", Left_parenthesis)]
            //     [InlineData(")", Right_parenthesis)]
            //     [InlineData("[", Left_bracket)]
            //     [InlineData("]", Right_bracket)]
            //     [InlineData("{", Left_brace)]
            //     [InlineData("}", Right_brace)]
            //     [InlineData("<", Left_angle)]
            //     [InlineData(">", Right_angle)]

            //     // Single charachter assigners
            //     [InlineData(": ", Colon)]
            //     [InlineData("=", Equals)]

            //     // Single character lookups
            //     [InlineData(".", Dot)]
            //     [InlineData("#", Hash)]
            //     [InlineData("/", Slash)]

            //     // Single character operators
            //     [InlineData(":", Colon)]
            //     [InlineData("+", Plus)]
            //     [InlineData("-", Dash)]
            //     [InlineData("*", Star)]
            //     [InlineData("%", Percent)]
            //     [InlineData(" / ", Slash)]
            //     [InlineData("~", Tilde)]
            //     [InlineData("&", And)]
            //     [InlineData("|", Pipe)]
            //     [InlineData("?", Question)]
            //     [InlineData("!", Bang)]

            //     // Double character lookups
            //     [InlineData("##", Double_hash)]
            //     [InlineData("//", Double_slash)]
            //     [InlineData("..", Double_dot)]

            //     // Double character operators
            //     [InlineData("++", Double_plus)]
            //     [InlineData("--", Double_dash)]
            //     [InlineData("**", Double_times)]
            //     [InlineData("%%", Double_percent)]
            //     [InlineData("~~", Double_tilde)]
            //     [InlineData("&&", Double_and)]
            //     [InlineData("||", Double_pipe)]
            //     [InlineData("==", Double_equals)]
            //     [InlineData("=<", Equals_less)]
            //     [InlineData(">=", Greater_equals)]
            //     [InlineData("!!", Double_bang)]
            //     [InlineData("??", Double_question)]

            //     // Two character arrow assigners
            //     [InlineData(">>", Double_right_angle)]
            //     [InlineData("<<", Double_left_angle)]
            //     [InlineData("+>", Right_plus_arrow)]
            //     [InlineData("<+", Left_plus_arrow)]
            //     [InlineData("->", Right_dash_arrow)]
            //     [InlineData("<-", Left_dash_arrow)]
            //     [InlineData("=>", Right_equals_arrow)]
            //     [InlineData("<=", Left_equals_arrow)]
            //     [InlineData("~>", Right_tilde_arrow)]
            //     [InlineData("<~", Left_tilde_arrow)]

            //     // Compound two charachter assigners
            //     [InlineData(".=", Dot_equals)]
            //     [InlineData("/=", Division_equals)]
            //     [InlineData("?=", Question_equals)]
            //     [InlineData("!=", Bang_equals)]
            //     [InlineData(":=", Colon_equals)]
            //     [InlineData("+=", Plus_equals)]
            //     [InlineData("-=", Minus_equals)]
            //     [InlineData("*=", Times_equals)]
            //     [InlineData("%=", Percent_equals)]
            //     [InlineData("~=", Tilde_equals)]
            //     [InlineData("=~", Equals_tilde)]
            //     [InlineData("#=", Hash_equals)]

            //     // Tripple character lookups
            //     [InlineData("...", Triple_dot)]

            //     // Triple character assigners
            //     [InlineData(":::", Triple_colon)]
            //     [InlineData(">>>", Triple_right_angle)]
            //     [InlineData("<<<", Triple_left_angle)]

            //     // Three character assigners
            //     [InlineData("??=", Double_question_equals)]
            //     [InlineData("!!=", Double_bang_equals)]
            //     [InlineData("##=", Double_hash_equals)]

            //     // Four character symbols
            //     [InlineData("##::", Double_hash_double_colon)]
            //     [InlineData("::>>", Double_colon_double_right_angle)]
            //     public void InQuotes(string text, expected, Quotes quotes = Quotes.All) {
            //         List<char> quoteChars = [];
            //         if(quotes.HasFlag(Quotes.Single)) {
            //             quoteChars.Add('\'');
            //         }

            //         if(quotes.HasFlag(Quotes.Double)) {
            //             quoteChars.Add('"');
            //         }

            //         if(quotes.HasFlag(Quotes.Backtick)) {
            //             quoteChars.Add('`');
            //         }

            //         foreach(char quote in quoteChars) {
            //             Lexer.Lex($"{quote}{text}{quote}")
            //                 .Assert_IsSuccess()
            //                 .Assert_IsSingle(
            //                     expected,
            //                     ignore: [quote switch {
            //                         '\'' => SingleQuote,
            //                         '"' => DoubleQuote,
            //                         '`' => Backtick,
            //                         _ => throw new InvalidOperationException("Invalid quote character")
            //                     }]
            //                 );
            //         }
            //     }

            //     #endregion

            //     #region Alone; Not Allowed
            //     /// <summary>
            //     /// Test that an open delimiter is correctly identified and
            //     ///     throws an unmatched delimiter error when 
            //     ///      it is the only token in the input.
            //     /// </summary>
            //     [Theory]
            //     // Brackets
            //     [InlineData("(", LeftParenthesis, ErrorCode.UNMATCHED_DELIMITER)]
            //     [InlineData(")", RightParenthesis, ErrorCode.UNMATCHED_DELIMITER)]
            //     [InlineData("[", LeftBracket, ErrorCode.UNMATCHED_DELIMITER)]
            //     [InlineData("]", RightBracket, ErrorCode.UNMATCHED_DELIMITER)]
            //     [InlineData("{", LeftBrace, ErrorCode.UNMATCHED_DELIMITER)]
            //     [InlineData("}", RightBrace, ErrorCode.UNMATCHED_DELIMITER)]

            //     // Quotes
            //     [InlineData("'", SingleQuote, ErrorCode.UNMATCHED_DELIMITER)]
            //     [InlineData("\"", DoubleQuote, ErrorCode.UNMATCHED_DELIMITER)]
            //     [InlineData("`", Backtick, ErrorCode.UNMATCHED_DELIMITER)]
            //     public void NotAllowed(string text, type, ErrorCode error)
            //         => Lexer.Lex(text)
            //             .Assert_IsFailure(error)
            //             .Assert_IsSingle(type, text, (0, text.Length));
            //     #endregion

            //     #region Alone; Other Result
            //     /// <summary>
            //     /// Tests for tokens that cannot be output alone, 
            //     ///     and result in a different token instead.
            //     ///  
            //     /// (Note: These tests exist mainly for coverage purposes)
            //     /// </summary>
            //     [Theory]
            //     [InlineData("<", LessThan, Left_chevron)]
            //     [InlineData(">", GreaterThan, Right_chevron)]
            //     [InlineData("<", LeftAngle, Left_chevron)]
            //     [InlineData(">", RightAngle, Right_chevron)]
            //     public void OtherResult(string text, unavailable, actual) {
            //         if(Lexer.Lex(text)
            //             .Assert_IsSuccess()
            //             .Assert_IsSingle(out Token? token)) {
            //             Assert.NotEqual(unavailable, token.Type);
            //             Assert.Equal(actual, token.Type);
            //         }
            //         else if(token is null) {
            //             Assert.Fail($"Expected a single token of type: {actual}, in the result but found none");
            //         }
            //         else {
            //             Assert.Fail($"Expected a single token of type: {actual}, in the result but found a second token of type: {token.Type} as well.");
            //         }
            //     }
            //     #endregion
            // 
        }

        public class Together {
            public Lexer Lexer { get; }
            public Together()
                => Lexer = new Lexer();

            #region Together: With Seperator (Between)

            #endregion
        }

        internal static void _assert<T>(
            T value,
            Action<T> assert,
            string failureMessage
        ) {
            try {
                assert(value);
            }
            catch(System.Exception e) {
                throw new Exception(failureMessage, e);
            }
        }

        public class Exception(string message, System.Exception innerException)
            : System.Exception(message, innerException) { }
    }
}