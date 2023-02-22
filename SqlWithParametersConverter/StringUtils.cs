namespace SqlWithParametersConverter.Engine
{
  public static class StringUtils
  {
    /// <summary>
    /// Trim end of lines of string.
    /// </summary>
    /// <param name="input">Input string to trim.</param>
    public static string TrimEndStringLines(string input)
    {
      var result = string.Join(Environment.NewLine,
        input.Split(new[] { '\n', '\r' }, StringSplitOptions.None).Select(l => l.TrimEnd()));

      return result;
    }
  }
}
