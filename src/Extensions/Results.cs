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
        TokenType tokenType,
        string? text = null,
        (int start, int end)? position = null
    ) {
      Tokens._assert(result, result => {
        Assert.NotNull(result.Tokens);

        Token single;
        if(result is Success) {
          Assert.Equal(2, result.Tokens.Length);
          single = result.Tokens[0];
        }
        else {
          single = Assert.Single(result.Tokens);
        }

        single.Assert_Is(result, tokenType, text, position);
      }, $"Result does not contain a single token {(
          result is Success ? "followed by EOF" : ""
      )}.\n\tExpected: {tokenType}, EOF.\n\tActual: {string.Join(
          ", ", result.Tokens?.Select(t => t.Name) ?? []
      )}.");

      return result.Tokens![0];
    }
  }
}