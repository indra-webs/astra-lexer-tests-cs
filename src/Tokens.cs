using static Indra.Astra.Lexer;

namespace Indra.Astra.Tests {
    public class Tokens {
        protected Lexer Lexer = null!;

        public Tokens()
        => Lexer = new Lexer();

        [Theory]
        // Separators
        [InlineData(TokenType.COMMA, ",")]
        [InlineData(TokenType.SEMICOLON, ";")]

        // Colon assigners
        [InlineData(TokenType.COLON_ASSIGNER, ":")]
        [InlineData(TokenType.TRIPLE_COLON_ASSIGNER, ":::")]

        // Angle assigners
        [InlineData(TokenType.DOUBLE_RIGHT_ANGLE, ">>")]
        [InlineData(TokenType.DOUBLE_LEFT_ANGLE, "<<")]
        [InlineData(TokenType.TRIPLE_RIGHT_ANGLE, ">>>")]
        [InlineData(TokenType.TRIPLE_LEFT_ANGLE, "<<<")]

        // Underscores
        [InlineData(TokenType.UNDERSCORE, "_")]
        [InlineData(TokenType.DOUBLE_UNDERSCORE, "__")]
        [InlineData(TokenType.TRIPLE_UNDERSCORE, "___")]

        // Single character lookups
        [InlineData(TokenType.HASH, "#")]
        [InlineData(TokenType.DOT, ".")]
        [InlineData(TokenType.SLASH, "/")]
        [InlineData(TokenType.LEFT_CHEVRON, "<")]
        [InlineData(TokenType.RIGHT_CHEVRON, ">")]

        // Single character operators
        [InlineData(TokenType.CROSS, "+")]
        [InlineData(TokenType.DASH, "-")]
        [InlineData(TokenType.STAR, "*")]
        [InlineData(TokenType.PERCENT, "%")]
        [InlineData(TokenType.TILDE, "~")]
        [InlineData(TokenType.AMPERSAND, "&")]
        [InlineData(TokenType.PIPE, "|")]
        [InlineData(TokenType.QUESTION, "?")]
        [InlineData(TokenType.BANG, "!")]
        [InlineData(TokenType.EQUALS, "=")]

        // Double character lookups
        [InlineData(TokenType.DOUBLE_HASH, "##")]
        [InlineData(TokenType.DOUBLE_DIVISION, "//")]
        [InlineData(TokenType.DOUBLE_DOT, "..")]

        // Two character lookups
        [InlineData(TokenType.BANG_HASH, "!#")]
        [InlineData(TokenType.QUESTION_HASH, "?#")]
        [InlineData(TokenType.DOT_BANG, ".!")]
        [InlineData(TokenType.DOT_QUESTION, ".?")]
        [InlineData(TokenType.DOT_HASH, ".#")]

        // Double character operators
        [InlineData(TokenType.DOUBLE_PLUS, "++")]
        [InlineData(TokenType.DOUBLE_DASH, "--")]
        [InlineData(TokenType.DOUBLE_TIMES, "**")]
        [InlineData(TokenType.DOUBLE_PERCENT, "%%")]
        [InlineData(TokenType.DOUBLE_TILDE, "~~")]
        [InlineData(TokenType.DOUBLE_AMPERSAND, "&&")]
        [InlineData(TokenType.DOUBLE_PIPE, "||")]
        [InlineData(TokenType.DOUBLE_EQUALS, "==")]
        [InlineData(TokenType.EQUALS_OR_LESS, "=<")]
        [InlineData(TokenType.GREATER_OR_EQUALS, ">=")]
        [InlineData(TokenType.DOUBLE_BANG, "!!")]
        [InlineData(TokenType.DOUBLE_QUESTION, "??")]

        // Two character arrow assigners
        [InlineData(TokenType.RIGHT_PLUS_ARROW, "+>")]
        [InlineData(TokenType.LEFT_PLUS_ARROW, "<+")]
        [InlineData(TokenType.RIGHT_DASH_ARROW, "->")]
        [InlineData(TokenType.LEFT_DASH_ARROW, "<-")]
        [InlineData(TokenType.RIGHT_EQUALS_ARROW, "=>")]
        [InlineData(TokenType.LEFT_EQUALS_ARROW, "<=")]
        [InlineData(TokenType.RIGHT_TILDE_ARROW, "~>")]
        [InlineData(TokenType.LEFT_TILDE_ARROW, "<~")]
        [InlineData(TokenType.COLON_RIGHT_ANGLE, ":>")]

        // Compound assigners
        [InlineData(TokenType.DOT_EQUALS, ".=")]
        [InlineData(TokenType.DIVISION_EQUALS, "/=")]
        [InlineData(TokenType.QUESTION_EQUALS, "?=")]
        [InlineData(TokenType.BANG_EQUALS, "!=")]
        [InlineData(TokenType.COLON_EQUALS, ":=")]
        [InlineData(TokenType.PLUS_EQUALS, "+=")]
        [InlineData(TokenType.MINUS_EQUALS, "-=")]
        [InlineData(TokenType.TIMES_EQUALS, "*=")]
        [InlineData(TokenType.PERCENT_EQUALS, "%=")]
        [InlineData(TokenType.TILDE_EQUALS, "~=")]
        [InlineData(TokenType.EQUALS_TILDE, "=~")]
        [InlineData(TokenType.HASH_EQUALS, "#=")]

        // Three character lookups
        [InlineData(TokenType.TRIPLE_DOT, "...")]
        [InlineData(TokenType.DOT_BANG_HASH, ".!#")]
        [InlineData(TokenType.DOT_QUESTION_HASH, ".?#")]
        [InlineData(TokenType.BANG_DOT_HASH, "!.#")]
        [InlineData(TokenType.QUESTION_DOT_HASH, "?.#")]
        [InlineData(TokenType.DOUBLE_DOT_QUESTION, "..?")]
        [InlineData(TokenType.DOUBLE_DOT_BANG, "..!")]
        [InlineData(TokenType.DOUBLE_DOT_HASH, "..#")]

        // Three character assigners
        [InlineData(TokenType.DOUBLE_QUESTION_EQUALS, "??=")]
        [InlineData(TokenType.DOUBLE_BANG_EQUALS, "!!=")]
        [InlineData(TokenType.DOUBLE_HASH_EQUALS, "##=")]
        [InlineData(TokenType.DOUBLE_COLON_EQUALS, "::=")]
        [InlineData(TokenType.DOUBLE_COLON_RIGHT_ANGLE, "::>")]
        [InlineData(TokenType.COLON_DOUBLE_RIGHT_ANGLE, ":>>")]
        [InlineData(TokenType.DOUBLE_HASH_COLON, "##:")]

        // Four character symbols
        [InlineData(TokenType.DOUBLE_HASH_DOUBLE_COLON, "##::")]
        [InlineData(TokenType.DOUBLE_COLON_DOUBLE_RIGHT_ANGLE, "::>>")]

        public void Alone(TokenType type, string text)
            => Lexer.Lex(text)
                .Assert_IsSuccess()
                .Assert_IsSingle(type, text, (0, text.Length));

        [Theory]
        // Brackets
        [InlineData(TokenType.OPEN_PARENTHESIS, "(")]
        [InlineData(TokenType.CLOSE_PARENTHESIS, ")")]
        [InlineData(TokenType.OPEN_BRACKET, "[")]
        [InlineData(TokenType.CLOSE_BRACKET, "]")]
        [InlineData(TokenType.OPEN_BRACE, "{")]
        [InlineData(TokenType.CLOSE_BRACE, "}")]

        // Quotes
        [InlineData(TokenType.SINGLE_QUOTE, "'")]
        [InlineData(TokenType.DOUBLE_QUOTE, "\"")]
        [InlineData(TokenType.BACKTICK, "`")]
        public void Alone_OpenDelimiter_IsFailure_Unmatched(TokenType type, string text)
            => Lexer.Lex(text)
                .Assert_IsFailure(ErrorCode.UNMATCHED_DELIMITER)
                .Assert_IsSingle(type, text, (0, text.Length));

        [Theory]
        // Brackets
        [InlineData(TokenType.OPEN_ANGLE, "<", TokenType.LEFT_CHEVRON)]
        [InlineData(TokenType.CLOSE_ANGLE, ">", TokenType.RIGHT_CHEVRON)]

        // Comments
        [InlineData(TokenType.EOL_SLASH_COMMENT, "//", TokenType.DOUBLE_DIVISION)]
        [InlineData(TokenType.EOL_HASH_COMMENT, "#", TokenType.HASH)]
        [InlineData(TokenType.DOC_HASH_COMMENT, "##", TokenType.DOUBLE_HASH)]
        [InlineData(TokenType.OPEN_BLOCK_COMMENT, "/*", TokenType.SLASH, TokenType.STAR)]
        [InlineData(TokenType.CLOSE_BLOCK_COMMENT, "*/", TokenType.STAR, TokenType.SLASH)]

        // Math operators
        [InlineData(TokenType.PLUS, "+", TokenType.CROSS)]
        [InlineData(TokenType.MINUS, "-", TokenType.DASH)]
        [InlineData(TokenType.TIMES, "*", TokenType.STAR)]
        [InlineData(TokenType.DIVISION, "/", TokenType.SLASH)]

        // Colons
        [InlineData(TokenType.COLON_CALLER, ":", TokenType.COLON_ASSIGNER)]
        [InlineData(TokenType.DOUBLE_COLON_ASSIGNER, "::", TokenType.DOUBLE_COLON_PREFIX)]
        public void Alone_NotAllowed_OtherResult(
            TokenType notAllowed,
            string text,
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