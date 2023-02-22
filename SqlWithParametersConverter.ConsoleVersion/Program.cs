using System.Text;
using SqlWithParametersConverter.Engine.Converter;

Console.WriteLine("Enter parameters string:");
var parametersString = Console.ReadLine();
if (string.IsNullOrWhiteSpace(parametersString))
{
  Console.WriteLine("Parameters string is null or empty");
  Console.ReadLine();
  return;
}

var parameters = ParametersParser.GetParametersDictionaryFromString(parametersString);
if (parameters.Count == 0)
{
  Console.WriteLine("Parameters string is incorrect");
  Console.ReadLine();
  return;
}

Console.WriteLine("Enter sqlText with /// as the last line:");
var sb = new StringBuilder();
string line;
while ((line = Console.ReadLine()) != "///")
{
  sb.Append(line);
}

var sqlText = sb.ToString();
if (string.IsNullOrWhiteSpace(sqlText))
{
  Console.WriteLine("SqlText is null or empty");
  Console.ReadLine();
  return;
}

var resultSqlText = SqlTextConverter.ConvertToFinalSqlText(sqlText, parameters);
var formattedSQL = NSQLFormatter.Formatter.Format(resultSqlText);

Console.WriteLine("\nResult sqlText: ");
Console.WriteLine(formattedSQL);

Console.ReadLine();
