using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Indra.Astra.Tokens;

using static Indra.Astra.Lexer;

namespace Indra.Astra.Tests {
  public static class LexerResultTestValidationExtensions {
    public static Success Assert_IsSuccess(this Result result) {
      Tokens._assert(
          result,
          result => Assert.True(result.IsSuccess),
          $"Expected success, but encountered unexpected error(s).\n\t{string.Join(
            "\n\t",
            result.Errors?.Select(e => e.Message) ?? []
          )}"
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
        IToken type,
        string? text = null,
        (int start, int end)? position = null,
        TokenType[]? ignore = null
    ) {
      Tokens._assert(result, result => {
        Assert.NotNull(result.Tokens);

        if(result.TryAssert_IsSingle(out Token? found, ignore)) {
          found.Assert_Is(result, type, text, position);
        }
        else if(found is null) {
          Assert.Fail($"No token of type: {type.Name} found in result.");
        }
        else {
          Assert.Fail($"Unexpected second non-ws token of type: {found.Name}, found in what should be a single non-ws-token result.");
        }
      }, $"Result does not contain a single valid token {(
          result is Success ? "followed by EOF" : ""
      )}.\n\tExpected: {type.Name}, EOF.\n\tActual: {string.Join(
          ", ", result.Tokens?.Where(t => ignore is null || !ignore.Contains(t.Type)).Select(t => t.Name) ?? []
      )}.");

      return result.Tokens![0];
    }

    public static bool TryAssert_IsSingle(this Result result, [NotNullWhen(true)] out Token? found, TokenType[]? ignore = null) {
      found = null;
      if(result is Success success) {
        foreach(Token token in success.Tokens) {
          if(token.Type is IWhitespace || (ignore is not null && ignore.Contains(token.Type))) {
            continue;
          }
          else if(found is not null) {
            found = token;
            return false;
          }
          else {
            found = token;
          }
        }

        Assert.Equal(EndOfFile.Type, success.Tokens[^1].Type);
      }
      else {
        Assert.NotNull(result.Tokens);
        found = Assert.Single(result.Tokens);
      }

      return found is not null;
    }

    public static Result Assert_IsSequence(
      this Result result,
      params TokenType[] types
    ) {
      Tokens._assert(result, result => {
        Assert.NotNull(result.Tokens);
        Assert.Equal(types.Where(t => t is not None).Count(), result.Tokens.Length - 1);

        int index =0;
        foreach(TokenType type in types) {
          if(type == None.Type) {
            continue;
          }
          else {
            result.Tokens[index++].Assert_Is(result, type);
          }
        }

        result.Tokens[^1].Assert_Is(result, EndOfFile.Type);
      }, $"Result does not contain the expected sequence of tokens.\n\tExpected: {string.Join(
          ", ", types.Select(t => t.Name)
      )}.\n\tActual: {string.Join(
          ", ", result.Tokens?.Select(t => t.Name) ?? []
      )}.");

      return result;
    }

    public static Token Assert_At(
      this Result result,
      int index,
      IToken type,
      string? text = null,
      (int start, int end)? position = null
    ) {
      Tokens._assert(result, result => {
        Assert.NotNull(result.Tokens);
        Assert.True(index >= 0 && index < result.Tokens.Length);

        result.Tokens[index].Assert_Is(result, type, text, position);
      }, $"Result does not contain a token at the specified index.\n\tIndex: {index}.\n\tExpected: {type.Name}{(
          text is not null
              ? $" = \"{text}\""
              : ""
        )}{(
          position is not null
              ? $" @ ({position.Value.start} -> {position.Value.end})"
              : ""
      )}.\n\tActual: {((result.Tokens?.Length ?? -1) > index
        ? result.Tokens![index].Name
        : None.Type.Name
      )}.");

      return result.Tokens![index];
    }
  }
}

