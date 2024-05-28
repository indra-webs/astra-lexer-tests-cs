using static Indra.Astra.Lexer;

namespace Indra.Astra.Tests {
    public class Tokens {
        public class Alone {
            public Lexer Lexer { get; }

            public Alone()
                => Lexer = new Lexer();

            // #region Each: Alone
            // /// <summary>
            // /// Test that a token is correctly identified when it is the only token in the input.
            // /// </summary>
            // [Theory]
            // // Separators
            // [InlineData(",", TokenType.COMMA)]
            // [InlineData(";", TokenType.SEMICOLON)]

            // // Colon assigners
            // [InlineData(":", TokenType.COLON_ASSIGNER)]
            // [InlineData(":::", TokenType.TRIPLE_COLON_ASSIGNER)]

            // // Angle assigners
            // [InlineData(">>", TokenType.DOUBLE_RIGHT_ANGLE)]
            // [InlineData("<<", TokenType.DOUBLE_LEFT_ANGLE)]
            // [InlineData(">>>", TokenType.TRIPLE_RIGHT_ANGLE)]
            // [InlineData("<<<", TokenType.TRIPLE_LEFT_ANGLE)]

            // // Underscores
            // [InlineData("_", TokenType.UNDERSCORE)]
            // [InlineData("__", TokenType.DOUBLE_UNDERSCORE)]
            // [InlineData("___", TokenType.TRIPLE_UNDERSCORE)]

            // // Single character lookups
            // [InlineData("#", TokenType.HASH)]
            // [InlineData(".", TokenType.DOT)]
            // [InlineData("/", TokenType.SLASH)]
            // [InlineData("<", TokenType.LEFT_CHEVRON)]
            // [InlineData(">", TokenType.RIGHT_CHEVRON)]

            // // Single character operators
            // [InlineData("+", TokenType.CROSS)]
            // [InlineData("-", TokenType.DASH)]
            // [InlineData("*", TokenType.STAR)]
            // [InlineData("%", TokenType.PERCENT)]
            // [InlineData("~", TokenType.TILDE)]
            // [InlineData("&", TokenType.AMPERSAND)]
            // [InlineData("|", TokenType.PIPE)]
            // [InlineData("?", TokenType.QUESTION)]
            // [InlineData("!", TokenType.BANG)]
            // [InlineData("=", TokenType.EQUALS)]

            // // Double character lookups
            // [InlineData("##", TokenType.DOUBLE_HASH)]
            // [InlineData("//", TokenType.DOUBLE_DIVISION)]
            // [InlineData("..", TokenType.DOUBLE_DOT)]

            // // Two character lookups
            // [InlineData("!#", TokenType.BANG_HASH)]
            // [InlineData("?#", TokenType.QUESTION_HASH)]
            // [InlineData(".!", TokenType.DOT_BANG)]
            // [InlineData(".?", TokenType.DOT_QUESTION)]
            // [InlineData(".#", TokenType.DOT_HASH)]

            // // Double character operators
            // [InlineData("++", TokenType.DOUBLE_PLUS)]
            // [InlineData("--", TokenType.DOUBLE_DASH)]
            // [InlineData("**", TokenType.DOUBLE_TIMES)]
            // [InlineData("%%", TokenType.DOUBLE_PERCENT)]
            // [InlineData("~~", TokenType.DOUBLE_TILDE)]
            // [InlineData("&&", TokenType.DOUBLE_AMPERSAND)]
            // [InlineData("||", TokenType.DOUBLE_PIPE)]
            // [InlineData("==", TokenType.DOUBLE_EQUALS)]
            // [InlineData("=<", TokenType.EQUALS_OR_LESS)]
            // [InlineData(">=", TokenType.GREATER_OR_EQUALS)]
            // [InlineData("!!", TokenType.DOUBLE_BANG)]
            // [InlineData("??", TokenType.DOUBLE_QUESTION)]

            // // Two character arrow assigners
            // [InlineData("+>", TokenType.RIGHT_PLUS_ARROW)]
            // [InlineData("<+", TokenType.LEFT_PLUS_ARROW)]
            // [InlineData("->", TokenType.RIGHT_DASH_ARROW)]
            // [InlineData("<-", TokenType.LEFT_DASH_ARROW)]
            // [InlineData("=>", TokenType.RIGHT_EQUALS_ARROW)]
            // [InlineData("<=", TokenType.LEFT_EQUALS_ARROW)]
            // [InlineData("~>", TokenType.RIGHT_TILDE_ARROW)]
            // [InlineData("<~", TokenType.LEFT_TILDE_ARROW)]
            // [InlineData(":>", TokenType.COLON_RIGHT_ANGLE)]

            // // Compound assigners
            // [InlineData(".=", TokenType.DOT_EQUALS)]
            // [InlineData("/=", TokenType.DIVISION_EQUALS)]
            // [InlineData("?=", TokenType.QUESTION_EQUALS)]
            // [InlineData("!=", TokenType.BANG_EQUALS)]
            // [InlineData(":=", TokenType.COLON_EQUALS)]
            // [InlineData("+=", TokenType.PLUS_EQUALS)]
            // [InlineData("-=", TokenType.MINUS_EQUALS)]
            // [InlineData("*=", TokenType.TIMES_EQUALS)]
            // [InlineData("%=", TokenType.PERCENT_EQUALS)]
            // [InlineData("~=", TokenType.TILDE_EQUALS)]
            // [InlineData("=~", TokenType.EQUALS_TILDE)]
            // [InlineData("#=", TokenType.HASH_EQUALS)]

            // // Three character lookups
            // [InlineData("...", TokenType.TRIPLE_DOT)]
            // [InlineData(".!#", TokenType.DOT_BANG_HASH)]
            // [InlineData(".?#", TokenType.DOT_QUESTION_HASH)]
            // [InlineData("!.#", TokenType.BANG_DOT_HASH)]
            // [InlineData("?.#", TokenType.QUESTION_DOT_HASH)]
            // [InlineData("..?", TokenType.DOUBLE_DOT_QUESTION)]
            // [InlineData("..!", TokenType.DOUBLE_DOT_BANG)]
            // [InlineData("..#", TokenType.DOUBLE_DOT_HASH)]

            // // Three character assigners
            // [InlineData("??=", TokenType.DOUBLE_QUESTION_EQUALS)]
            // [InlineData("!!=", TokenType.DOUBLE_BANG_EQUALS)]
            // [InlineData("##=", TokenType.DOUBLE_HASH_EQUALS)]
            // [InlineData("::=", TokenType.DOUBLE_COLON_EQUALS)]
            // [InlineData("::>", TokenType.DOUBLE_COLON_RIGHT_ANGLE)]
            // [InlineData(":>>", TokenType.COLON_DOUBLE_RIGHT_ANGLE)]
            // [InlineData("##:", TokenType.DOUBLE_HASH_COLON)]

            // // Four character symbols
            // [InlineData("##::", TokenType.DOUBLE_HASH_DOUBLE_COLON)]
            // [InlineData("::>>", TokenType.DOUBLE_COLON_DOUBLE_RIGHT_ANGLE)]

            // public void Allowed(string text, TokenType type)
            //     => Lexer.Lex(text)
            //         .Assert_IsSuccess()
            //         .Assert_IsSingle(type, text, (0, text.Length));

            // #endregion

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
                Some = Some_Left | Some_Right,
                Some_Left = Space_Left | Tab_Left | NewLine_Left,
                Some_Right = Space_Right | Tab_Right | NewLine_Right,
                Any = Some | None,
                Any_Left = Some_Left | None_Left,
                Any_Right = Some_Right | None_Right
            }

            /// <summary>
            /// Test that a token is correctly identified when it is the only non-whitespace token in the input.
            /// </summary>
            [Theory]
            // Separators
            [InlineData(",", TokenType.COMMA)]
            [InlineData(";", TokenType.SEMICOLON)]

            // Underscores
            [InlineData("_", TokenType.UNDERSCORE)]
            [InlineData("__", TokenType.DOUBLE_UNDERSCORE)]
            [InlineData("___", TokenType.TRIPLE_UNDERSCORE)]

            // Single charachter assigners
            [InlineData(":", TokenType.COLON_ASSIGNER)]
            [InlineData("=", TokenType.EQUALS)]

            // Single character lookups
            [InlineData("#", TokenType.HASH)]
            [InlineData(".", TokenType.DOT)]
            [InlineData("/", TokenType.SLASH)]
            [InlineData("<", TokenType.LEFT_CHEVRON)]
            [InlineData(">", TokenType.RIGHT_CHEVRON)]

            // Single character operators
            [InlineData("+", TokenType.CROSS)]
            [InlineData("-", TokenType.DASH)]
            [InlineData("*", TokenType.STAR)]
            [InlineData("%", TokenType.PERCENT)]
            [InlineData("~", TokenType.TILDE)]
            [InlineData("&", TokenType.AMPERSAND)]
            [InlineData("|", TokenType.PIPE)]
            [InlineData("?", TokenType.QUESTION)]
            [InlineData("!", TokenType.BANG)]

            // Double character lookups
            [InlineData("##", TokenType.DOUBLE_HASH)]
            [InlineData("//", TokenType.DOUBLE_DIVISION)]
            [InlineData("..", TokenType.DOUBLE_DOT)]

            // Two character lookups
            [InlineData("!#", TokenType.BANG_HASH)]
            [InlineData("?#", TokenType.QUESTION_HASH)]
            [InlineData(".!", TokenType.DOT_BANG)]
            [InlineData(".?", TokenType.DOT_QUESTION)]
            [InlineData(".#", TokenType.DOT_HASH)]

            // Double character operators
            [InlineData("++", TokenType.DOUBLE_PLUS)]
            [InlineData("--", TokenType.DOUBLE_DASH)]
            [InlineData("**", TokenType.DOUBLE_TIMES)]
            [InlineData("%%", TokenType.DOUBLE_PERCENT)]
            [InlineData("~~", TokenType.DOUBLE_TILDE)]
            [InlineData("&&", TokenType.DOUBLE_AMPERSAND)]
            [InlineData("||", TokenType.DOUBLE_PIPE)]
            [InlineData("==", TokenType.DOUBLE_EQUALS)]
            [InlineData("=<", TokenType.EQUALS_OR_LESS)]
            [InlineData(">=", TokenType.GREATER_OR_EQUALS)]
            [InlineData("!!", TokenType.DOUBLE_BANG)]
            [InlineData("??", TokenType.DOUBLE_QUESTION)]

            // Two character arrow assigners
            [InlineData(">>", TokenType.DOUBLE_RIGHT_ANGLE)]
            [InlineData("<<", TokenType.DOUBLE_LEFT_ANGLE)]
            [InlineData("+>", TokenType.RIGHT_PLUS_ARROW)]
            [InlineData("<+", TokenType.LEFT_PLUS_ARROW)]
            [InlineData("->", TokenType.RIGHT_DASH_ARROW)]
            [InlineData("<-", TokenType.LEFT_DASH_ARROW)]
            [InlineData("=>", TokenType.RIGHT_EQUALS_ARROW)]
            [InlineData("<=", TokenType.LEFT_EQUALS_ARROW)]
            [InlineData("~>", TokenType.RIGHT_TILDE_ARROW)]
            [InlineData("<~", TokenType.LEFT_TILDE_ARROW)]
            [InlineData(":>", TokenType.COLON_RIGHT_ANGLE)]

            // Compound two charachter assigners
            [InlineData(".=", TokenType.DOT_EQUALS)]
            [InlineData("/=", TokenType.DIVISION_EQUALS)]
            [InlineData("?=", TokenType.QUESTION_EQUALS)]
            [InlineData("!=", TokenType.BANG_EQUALS)]
            [InlineData(":=", TokenType.COLON_EQUALS)]
            [InlineData("+=", TokenType.PLUS_EQUALS)]
            [InlineData("-=", TokenType.MINUS_EQUALS)]
            [InlineData("*=", TokenType.TIMES_EQUALS)]
            [InlineData("%=", TokenType.PERCENT_EQUALS)]
            [InlineData("~=", TokenType.TILDE_EQUALS)]
            [InlineData("=~", TokenType.EQUALS_TILDE)]
            [InlineData("#=", TokenType.HASH_EQUALS)]

            // Three character lookups
            [InlineData("...", TokenType.TRIPLE_DOT)]
            [InlineData(".!#", TokenType.DOT_BANG_HASH)]
            [InlineData(".?#", TokenType.DOT_QUESTION_HASH)]
            [InlineData("!.#", TokenType.BANG_DOT_HASH)]
            [InlineData("?.#", TokenType.QUESTION_DOT_HASH)]
            [InlineData("..?", TokenType.DOUBLE_DOT_QUESTION)]
            [InlineData("..!", TokenType.DOUBLE_DOT_BANG)]
            [InlineData("..#", TokenType.DOUBLE_DOT_HASH)]

            // Triple character assigners
            [InlineData(":::", TokenType.TRIPLE_COLON_ASSIGNER)]
            [InlineData(">>>", TokenType.TRIPLE_RIGHT_ANGLE)]
            [InlineData("<<<", TokenType.TRIPLE_LEFT_ANGLE)]

            // Three character assigners
            [InlineData("??=", TokenType.DOUBLE_QUESTION_EQUALS)]
            [InlineData("!!=", TokenType.DOUBLE_BANG_EQUALS)]
            [InlineData("##=", TokenType.DOUBLE_HASH_EQUALS)]
            [InlineData("::=", TokenType.DOUBLE_COLON_EQUALS)]
            [InlineData("::>", TokenType.DOUBLE_COLON_RIGHT_ANGLE)]
            [InlineData(":>>", TokenType.COLON_DOUBLE_RIGHT_ANGLE)]
            [InlineData("##:", TokenType.DOUBLE_HASH_COLON)]

            // Four character symbols
            [InlineData("##::", TokenType.DOUBLE_HASH_DOUBLE_COLON)]
            [InlineData("::>>", TokenType.DOUBLE_COLON_DOUBLE_RIGHT_ANGLE)]
            public void WithPadding(string text, TokenType type, Padding padding = Padding.Any) {
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
                    }, $"Padding variant: '{debugPadding(l)}' + '{text}' + '{debugPadding(r)}'` failed.");

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
                Some = Some_Left | Some_Right,
                Some_Left = Comma_Left | Semicolon_Left,
                Some_Right = Comma_Right | Semicolon_Right,
                Any = Any_Left | Any_Right,
                Any_Left = Some_Left | None_Left,
                Any_Right = Some_Right | None_Right
            }

            [Theory]
            [InlineData(":", TokenType.COLON_CALLER, Seperators.Any_Left | Seperators.Some_Right)]
            [InlineData(":", TokenType.COLON_ASSIGNER, Seperators.Any & ~Seperators.Some_Right)]
            public void WithSeparators(string text, TokenType type, Seperators separator = Seperators.Any) {
                List<char> right = [],
                    left = [];

                if(separator.HasFlag(Seperators.Comma_Left)) {
                    left.Add(',');
                }

                if(separator.HasFlag(Seperators.Comma_Right)) {
                    right.Add(',');
                }

                if(separator.HasFlag(Seperators.Semicolon_Left)) {
                    left.Add(';');
                }

                if(separator.HasFlag(Seperators.Semicolon_Right)) {
                    right.Add(';');
                }

                if(separator.HasFlag(Seperators.None_Left)) {
                    left.Add('\0');
                }

                if(separator.HasFlag(Seperators.None_Right)) {
                    right.Add('\0');
                }

                (char l, char r)[] paddingVariants =
                    left.SelectMany(l => right.Select(r => (l, r))).ToArray();

                foreach((char l, char r) in paddingVariants) {
                    _assert((l, r), (padding) => {
                        string padded = $"{l}{text}{r}".Trim('\0');

                        Lexer.Lex(padded)
                            .Assert_IsSuccess()
                            .Assert_IsSingle(
                                type,
                                text,
                                (start: l is '\0' ? 0 : 1,
                                    end: text.Length + (l is '\0' ? 0 : 1)),
                                ignore: [TokenType.COMMA, TokenType.SEMICOLON]
                            );
                    }, $"Padding variant: '{debugSeperator(l)}' + '{text}' + '{debugSeperator(r)}'` failed.");

                    static string debugSeperator(char c)
                        => c switch {
                            '\0' => "",
                            ',' => ",",
                            ';' => ";",
                            _ => c.ToString()
                        };
                }
            }

            #endregion

            #region Alone; Not Allowed
            /// <summary>
            /// Test that an open delimiter is correctly identified and
            ///     throws an unmatched delimiter error when 
            ///      it is the only token in the input.
            /// </summary>
            [Theory]
            // Brackets
            [InlineData("(", TokenType.OPEN_PARENTHESIS, ErrorCode.UNMATCHED_DELIMITER)]
            [InlineData(")", TokenType.CLOSE_PARENTHESIS, ErrorCode.UNMATCHED_DELIMITER)]
            [InlineData("[", TokenType.OPEN_BRACKET, ErrorCode.UNMATCHED_DELIMITER)]
            [InlineData("]", TokenType.CLOSE_BRACKET, ErrorCode.UNMATCHED_DELIMITER)]
            [InlineData("{", TokenType.OPEN_BRACE, ErrorCode.UNMATCHED_DELIMITER)]
            [InlineData("}", TokenType.CLOSE_BRACE, ErrorCode.UNMATCHED_DELIMITER)]

            // Quotes
            [InlineData("'", TokenType.SINGLE_QUOTE, ErrorCode.UNMATCHED_DELIMITER)]
            [InlineData("\"", TokenType.DOUBLE_QUOTE, ErrorCode.UNMATCHED_DELIMITER)]
            [InlineData("`", TokenType.BACKTICK, ErrorCode.UNMATCHED_DELIMITER)]
            public void Alone_Delimiters_NotAllowed(string text, TokenType type, ErrorCode error)
                => Lexer.Lex(text)
                    .Assert_IsFailure(error)
                    .Assert_IsSingle(type, text, (0, text.Length));
            #endregion
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