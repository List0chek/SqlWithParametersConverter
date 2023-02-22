using System.Text;
using System.Text.RegularExpressions;

namespace SqlWithParametersConverter.Engine.Converter
{
  public static class SqlTextConverter
  {
    /// <summary>
    /// Convert to sqlText with parameters values.
    /// </summary>
    /// <param name="sqlTextInput">SqlText input.</param>
    /// <param name="parameters">Parameters as dictionary.</param>
    /// <returns>SqlText with parameters.</returns>
    public static string ConvertToFinalSqlText(string sqlTextInput, Dictionary<string, string> parameters)
    {
      string regexPattern = "\\$\\d+";
      string replacedSqlText = Regex.Replace(sqlTextInput, regexPattern, match =>
      {
        if (parameters.ContainsKey(match.Value))
        {
          var value = parameters[match.Value];

          if (char.TryParse(value, out var charValue) && char.IsLetter(charValue))
            return AppendAndPrependApostrophe(value);

          if (DateTime.TryParse(value, out var dateTimeValue))
            return AppendAndPrependApostrophe(value);

          return value;
        }
        else
        {
          throw new ArgumentException("Keys of parameters and sqlText parameters keys doesn't match");
        }
      });

      return replacedSqlText;
    }

    /// <summary>
    /// Add apostrophes at the start and at the end of the value.
    /// </summary>
    /// <param name="input">Raw value.</param>
    /// <returns>String with apostrophes.</returns>
    private static string AppendAndPrependApostrophe(string input)
    {
      char apostrophe = '\'';
      var sb = new StringBuilder(input);
      sb.Insert(0, apostrophe);
      sb.Append(apostrophe);

      return sb.ToString();
    }
  }
}
