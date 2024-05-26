using static Indra.Astra.Lexer;

namespace Indra.Astra.Tests {
  public static class LexerTokenTestValidationExtensions {
    public static Token Assert_Is(
        this Token token,
        Result result,
        TokenType type,
        string? text = null,
        (int start, int end)? position = null
    ) {
      Tokens._assert(token, token => {
        token.Assert_Is(type, position);
        Assert.Equal(text, token.GetSourceText(result.Source));
      }, $"Token does not match expected values.\n\tExpected: {type}{(
     text is not null
         ? $" = \"{text}\""
         : ""
      )}{(
          position is not null
              ? $" @ ({position.Value.start} -> {position.Value.end})"
              : ""
      )}.\n\tActual: {token.Name} = \"{token.GetSourceText(result.Source)}\" @ ({token.Start} -> {token.End}).");

      return token;
    }

    public static Token Assert_Is(
        this Token token,
        TokenType? type = null,
        (int start, int end)? position = null
    ) {
      Tokens._assert(token, token => {
        if(type is not null) {
          Assert.Equal(type, token.Type);
        }

        if(position is not null) {
          Assert.Equal(position.Value.start, token.Start);
          Assert.Equal(position.Value.end, token.End);
        }
      }, $"Token does not match expected values.\n\tExpected: {type}{(
      position is not null
              ? $" @ ({position.Value.start} -> {position.Value.end})"
              : ""
      )}.\n\tActual: {token.Name} @ ({token.Start} -> {token.End}).");

      return token;
    }

    public static Token Assert_IsNot(
        this Token token,
        TokenType type
    ) {
      Tokens._assert(token, token
          => Assert.NotEqual(type, token.Type),
      $"Token should not be of type {type}.\n\tActual: {token.Name}.");

      return token;
    }
  }
}