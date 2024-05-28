using Meep.Tech.Text;

using static Indra.Astra.Lexer;

namespace Indra.Astra.Tests {
  public static class LexerResultTestValidationExtensions {
    public static Success Assert_IsSuccess(this Result result) {
      Tokens._assert(
          result,
          result => Assert.True(result.IsSuccess),
          $"Expected success, but encountered unexpected error(s).\n\t{string.Join("\n\t", result.Errors?.Select(e => e.Message) ?? [])}"
      );

      return Assert.IsType<Success>(result);
    }

    public static Failure Assert_IsFailure(
        this Result result,
        params ErrorCode[] expectedErrors
    ) {
      Tokens._assert(
          result,
          result => Assert.False(result.IsSuccess),
          $"Expected a failure result with error(s): {string.Join(
              ", ", expectedErrors.Select(e => e.ToString())
          )}, but got a successful one consisting of the following tokens: \n\t{string.Join(
              ", ", result.Tokens?.Select(t => t.Name) ?? []
          )}"
      );

      return Assert.IsType<Failure>(result);
    }

    public static Token Assert_IsSingle(
        this Result result,
        TokenType type,
        string? text = null,
        (int start, int end)? position = null,
        TokenType[]? ignore = null
    ) {
      Tokens._assert(result, result => {
        Assert.NotNull(result.Tokens);

        Token single = null!;
        if(result is Success) {
          foreach(Token token in result.Tokens) {
            if(token.Type.IsWhiteSpace() || (ignore is not null && ignore.Contains(token.Type))) {
              continue;
            }
            else if(single is not null) {
              Assert.Fail($"Unexpected second non-ws token of type: {token.Name}, found in what should be a single non-ws-token result.");
            }
            else {
              single = token;
            }
          }

          Assert.Equal(TokenType.EOF, result.Tokens[^1].Type);
        }
        else {
          single = Assert.Single(result.Tokens);
        }

        if(single is null) {
          Assert.Fail($"No token of type: {type} found in result.");
        }
        else {
          single.Assert_Is(result, type, text, position);
        }
      }, $"Result does not contain a single valid token {(
          result is Success ? "followed by EOF" : ""
      )}.\n\tExpected: {type}, EOF.\n\tActual: {string.Join(
          ", ", result.Tokens?.Select(t => t.Name) ?? []
      )}.");

      return result.Tokens![0];
    }
  }
}