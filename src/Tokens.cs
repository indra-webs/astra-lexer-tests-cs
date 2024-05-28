using static Indra.Astra.Lexer;
using Meep.Tech.Text;

namespace Indra.Astra.Tests {
    public class Tokens {

        protected Lexer Lexer = null!;

        public Tokens()
        => Lexer = new Lexer();

        #region Single: Alone
        /// <summary>
        /// Test that a token is correctly identified when it is the only token in the input.
        /// </summary>
        [Theory]
        // Separators
        [InlineData(",", TokenType.COMMA)]
        [InlineData(";", TokenType.SEMICOLON)]

        // Colon assigners
        [InlineData(":", TokenType.COLON_ASSIGNER)]
        [InlineData(":::", TokenType.TRIPLE_COLON_ASSIGNER)]

        // Angle assigners
        [InlineData(">>", TokenType.DOUBLE_RIGHT_ANGLE)]
        [InlineData("<<", TokenType.DOUBLE_LEFT_ANGLE)]
        [InlineData(">>>", TokenType.TRIPLE_RIGHT_ANGLE)]
        [InlineData("<<<", TokenType.TRIPLE_LEFT_ANGLE)]

        // Underscores
        [InlineData("_", TokenType.UNDERSCORE)]
        [InlineData("__", TokenType.DOUBLE_UNDERSCORE)]
        [InlineData("___", TokenType.TRIPLE_UNDERSCORE)]

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
        [InlineData("=", TokenType.EQUALS)]

        // Double character lookups
        [InlineData("##", TokenType.DOUBLE_HASH)]
        [InlineData("##", TokenType.DOC_HASH_COMMENT)]
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
        [InlineData("+>", TokenType.RIGHT_PLUS_ARROW)]
        [InlineData("<+", TokenType.LEFT_PLUS_ARROW)]
        [InlineData("->", TokenType.RIGHT_DASH_ARROW)]
        [InlineData("<-", TokenType.LEFT_DASH_ARROW)]
        [InlineData("=>", TokenType.RIGHT_EQUALS_ARROW)]
        [InlineData("<=", TokenType.LEFT_EQUALS_ARROW)]
        [InlineData("~>", TokenType.RIGHT_TILDE_ARROW)]
        [InlineData("<~", TokenType.LEFT_TILDE_ARROW)]
        [InlineData(":>", TokenType.COLON_RIGHT_ANGLE)]

        // Compound assigners
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

        public void Alone(string text, TokenType type)
            => Lexer.Lex(text)
                .Assert_IsSuccess()
                .Assert_IsSingle(type, text, (0, text.Length));

        #endregion

        #region Single: Padded

        public enum Padding {
            Any = 0,
            None_Left = 1,
            None_Right = 2,
            None = None_Left | None_Right,
            Space_Left = 4,
            Space_Right = 8,
            Spaces = Space_Left | Space_Right,
            Tab_Left = 16,
            Tab_Right = 32,
            Tabs = Tab_Left | Tab_Right,
            NewLine_Left = 64,
            NewLine_Right = 128,
            NewLines = NewLine_Left | NewLine_Right,
            Some_Left = Space_Left | Tab_Left | NewLine_Left,
            Some_Right = Space_Right | Tab_Right | NewLine_Right,
            Some = Some_Left | Some_Right,
        }

        public void Padded(string text, TokenType type, params string?[][] padding) {
            foreach(string?[] pads in padding ?? [[" "], ["\t"], ["\n"]]) {
                string before = pads.Length > 0
                    ? pads[0] ?? ""
                    : "";

                string after = pads.Length == 2
                    ? pads[1] ?? ""
                    : pads.Length == 1
                        ? pads[0] ?? ""
                        : "";

                string spaced
                    = before + text + after;

                Lexer.Lex(spaced)
                    .Assert_IsSuccess()
                    .Assert_IsSingle(type, text, (before.Length, before.Length + text.Length));
            }
        }
        #endregion

        #region Single: Open Delimiter => Failure: Unmatched
        /// <summary>
        /// Test that an open delimiter is correctly identified and
        ///     throws an unmatched delimiter error when 
        ///      it is the only token in the input.
        /// </summary>
        [Theory]
        // Brackets
        [InlineData("(", TokenType.OPEN_PARENTHESIS)]
        [InlineData(")", TokenType.CLOSE_PARENTHESIS)]
        [InlineData("[", TokenType.OPEN_BRACKET)]
        [InlineData("]", TokenType.CLOSE_BRACKET)]
        [InlineData("{", TokenType.OPEN_BRACE)]
        [InlineData("}", TokenType.CLOSE_BRACE)]

        // Quotes
        [InlineData("'", TokenType.SINGLE_QUOTE)]
        [InlineData("\"", TokenType.DOUBLE_QUOTE)]
        [InlineData("`", TokenType.BACKTICK)]
        public void Alone_OpenDelimiter_IsFailure_Unmatched(string text, TokenType type)
            => Lexer.Lex(text)
                .Assert_IsFailure(ErrorCode.UNMATCHED_DELIMITER)
                .Assert_IsSingle(type, text, (0, text.Length));
        #endregion

        #region Alone: Not Allowed => Success: Other Result
        [Theory]
        // Brackets
        [InlineData("<", TokenType.OPEN_ANGLE, TokenType.LEFT_CHEVRON)]
        [InlineData("<", TokenType.LESS_THAN, TokenType.LEFT_CHEVRON)]
        [InlineData(">", TokenType.CLOSE_ANGLE, TokenType.RIGHT_CHEVRON)]
        [InlineData(">", TokenType.GREATER_THAN, TokenType.RIGHT_CHEVRON)]

        // Comments
        [InlineData("//", TokenType.EOL_SLASH_COMMENT, TokenType.DOUBLE_DIVISION)]
        [InlineData("#", TokenType.EOL_HASH_COMMENT, TokenType.HASH)]
        [InlineData("##", TokenType.DOC_HASH_COMMENT, TokenType.DOUBLE_HASH)]
        [InlineData("/*", TokenType.OPEN_BLOCK_COMMENT, TokenType.SLASH, TokenType.STAR)]
        [InlineData("*/", TokenType.CLOSE_BLOCK_COMMENT, TokenType.STAR, TokenType.SLASH)]

        // Math operators
        [InlineData("+", TokenType.PLUS, TokenType.CROSS)]
        [InlineData("-", TokenType.MINUS, TokenType.DASH)]
        [InlineData("*", TokenType.TIMES, TokenType.STAR)]
        [InlineData("/", TokenType.DIVISION, TokenType.SLASH)]

        // Colons
        [InlineData(":", TokenType.COLON_CALLER, TokenType.COLON_ASSIGNER)]
        [InlineData("::", TokenType.DOUBLE_COLON_ASSIGNER, TokenType.DOUBLE_COLON_PREFIX)]
        public void Alone_NotAllowed_OtherResult(
            string text,
            TokenType notAllowed,
            params TokenType[] actualTypes
        ) {
            Success result
                = Lexer.Lex(text)
                .Assert_IsSuccess();

            for(int i = 0; i < actualTypes.Length; i++) {
                result.Tokens[i]
                    .Assert_IsNot(notAllowed)
                    .Assert_Is(actualTypes[i]);
            }
        }
        #endregion

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