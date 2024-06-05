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
        if(text is not null) {
          token.Assert_Text(result.Source, text);
        }
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
        if(type.HasValue) {
          token.Assert_Type(type.Value);
        }

        if(position is not null) {
          token.Assert_Position(position.Value.start, position.Value.end);
        }
      }, $"Token does not match expected values.\n\tExpected: {type}{(
        position is not null
          ? $" @ ({position.Value.start} -> {position.Value.end})"
          : ""
      )}.\n\tActual: {token.Name} @ ({token.Start} -> {token.End}).");

      return token;
    }

    public static void Assert_Type(this Token token, TokenType type)
        => Tokens._assert(token, token =>
            Assert.Equal(type, token.Type),
            $"Incorrect Type:\n\tExpected: {type}.\n\tActual: {token.Type}.");

    public static void Assert_Position(this Token token, int start, int end)
      => Tokens._assert(token, token => {
        Assert.Equal(start, token.Start);
        Assert.Equal(end, token.End);
      }, $"Incorrect Position:\n\tExpected: ({start} -> {end}).\n\tActual: ({token.Start} -> {token.End}).");

    public static void Assert_Text(this Token token, string source, string? text)
      => Tokens._assert(token, token =>
          Assert.Equal(text, token.GetSourceText(source)),
      $"Incorrect Text:\n\tExpected: \"{text}\".\n\tActual: \"{token.GetSourceText(source)}\".");

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