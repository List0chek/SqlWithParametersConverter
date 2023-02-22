using System.Windows;
using SqlWithParametersConverter.Engine;
using SqlWithParametersConverter.Engine.Converter;

namespace SqlWithParametersConverterUIVersion
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private void ConvertButton_Click(object sender, RoutedEventArgs e)
    {
      if (!string.IsNullOrWhiteSpace(this.SqlTextBox.Text) && !string.IsNullOrWhiteSpace(this.ParametersTextBox.Text))
      {
        try
        {
          var parameters = ParametersParser.GetParametersDictionaryFromString(this.ParametersTextBox.Text);
          if (parameters.Count == 0)
            return;

          var convertedSqlText = SqlTextConverter.ConvertToFinalSqlText(this.SqlTextBox.Text, parameters);

          this.SqlTextBox.Text = convertedSqlText;
        }
        catch
        {
          // Supress any exception while trying to convert
          return;
        }
      }
    }

    private void SqlTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
      if (string.IsNullOrWhiteSpace(this.SqlTextBox.Text))
        return;

      this.SqlTextBox.TextChanged -= SqlTextBox_TextChanged;

      var formattedSqlText = NSQLFormatter.Formatter.Format(this.SqlTextBox.Text);
      this.SqlTextBox.Text = StringUtils.TrimEndStringLines(formattedSqlText);

      this.SqlTextBox.TextChanged += SqlTextBox_TextChanged;
    }
  }
}
