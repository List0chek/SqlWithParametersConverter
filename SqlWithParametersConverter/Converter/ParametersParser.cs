namespace SqlWithParametersConverter.Engine.Converter
{
  public static class ParametersParser
  {
    /// <summary>
    /// Parse string with parameters into dictionary with parameters.
    /// </summary>
    /// <param name="parametersString">String with parameters.</param>
    /// <returns>Dictionary with parameters.</returns>
    /// Example of the string with parameters: "$1 = 't', $2 = '1', $3 = '1', $4 = '79', $5 = '3937', $6 = '5', $7 = '10', $8 = '11', $9 = '2014-10-15 00:00:00', $10 = '2014-10-16 00:00:00', $11 = '4', $12 = '3389', $13 = '10000', $14 = '1000'"
    public static Dictionary<string, string> GetParametersDictionaryFromString(string parametersString)
    {
      string[] keyValuePairs = parametersString.Split(", ");
      var parameters = new Dictionary<string, string>();
      foreach (string pair in keyValuePairs)
      {
        string[] parts = pair.Split("=");

        var key = parts[0].Trim();
        string value = parts[1].Trim(' ', '\'');

        parameters.Add(key, value);
      }

      return parameters;
    }
  }
}
