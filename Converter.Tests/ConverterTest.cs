namespace Converter.Tests
{
  public class ConverterTest
  {
    #region Constants

    /// <summary>
    /// Parameters string.
    /// </summary>
    private static readonly string parametersString = "$1 = 't', $2 = '1', $3 = '1', $4 = '79', $5 = '3937', $6 = '5', $7 = '10', $8 = '11', $9 = '2014-10-15 00:00:00', $10 = '2014-10-16 00:00:00', $11 = '4', $12 = '3389', $13 = '10000', $14 = '1000'";

    /// <summary>
    /// String with sql, which needs to be converted.
    /// </summary>
    private static readonly string sqlText = @"select electronic0_.""id"" as col_0_0_,
electronic0_.Discriminator as col_1_0_
from
  public.""sungero_content_edoc"" electronic0_
where
  electronic0_.""id"" in (
    select
      cashboxrep1_.""id""
    from
      public.""sungero_content_edoc"" cashboxrep1_
    where
      cashboxrep1_.""discriminator"" = 'dcd051e6-cc01-4433-8a18-a9351c59e83f'
      and(
        exists(
          select
            accesscont2_.""id""
          from
            public.""sungero_system_accessctrlent"" accesscont2_
          where
            accesscont2_.""granted"" = $1
            and accesscont2_.""operationset"" & $2 = $3
            and (
              accesscont2_.""recipient"" in ($4, $5, $6, $7, $8)
            ) 
            and accesscont2_.""secureobject"" = cashboxrep1_.""secureobject""
        )
      )
      and cashboxrep1_.""created"" >= $9
      and $10 > cashboxrep1_.""created""
      and (cashboxrep1_.""author"" is not null)
      and cashboxrep1_.""author"" = $11
      and (
        cashboxrep1_.""crnumber_docflow_sungero"" is not null
      )
      and cashboxrep1_.""crnumber_docflow_sungero"" ~ * $12
    order by
      cashboxrep1_.""id"" desc
    limit
      $13
  )
order by
  electronic0_.""created"" DESC NULLS LAST,
  electronic0_.""id"" DESC NULLS LAST
limit
  $14";

    /// <summary>
    /// Etalon value of the result sqlText.
    /// </summary>
    private static readonly string sqlTextEtalon = @"select electronic0_.""id"" as col_0_0_,
electronic0_.Discriminator as col_1_0_
from
  public.""sungero_content_edoc"" electronic0_
where
  electronic0_.""id"" in (
    select
      cashboxrep1_.""id""
    from
      public.""sungero_content_edoc"" cashboxrep1_
    where
      cashboxrep1_.""discriminator"" = 'dcd051e6-cc01-4433-8a18-a9351c59e83f'
      and(
        exists(
          select
            accesscont2_.""id""
          from
            public.""sungero_system_accessctrlent"" accesscont2_
          where
            accesscont2_.""granted"" = 't'
            and accesscont2_.""operationset"" & 1 = 1
            and (
              accesscont2_.""recipient"" in (79, 3937, 5, 10, 11)
            ) 
            and accesscont2_.""secureobject"" = cashboxrep1_.""secureobject""
        )
      )
      and cashboxrep1_.""created"" >= '2014-10-15 00:00:00'
      and '2014-10-16 00:00:00' > cashboxrep1_.""created""
      and (cashboxrep1_.""author"" is not null)
      and cashboxrep1_.""author"" = 4
      and (
        cashboxrep1_.""crnumber_docflow_sungero"" is not null
      )
      and cashboxrep1_.""crnumber_docflow_sungero"" ~ * 3389
    order by
      cashboxrep1_.""id"" desc
    limit
      10000
  )
order by
  electronic0_.""created"" DESC NULLS LAST,
  electronic0_.""id"" DESC NULLS LAST
limit
  1000";

    #endregion

    #region Tests

    [Test]
    public void SqlWithParametersConverterTest()
    {
      var parameters = ParametersParser.GetParametersDictionaryFromString(parametersString);
      var result = SqlTextConverter.ConvertToFinalSqlText(sqlText, parameters);

      Assert.That(result, Is.EqualTo(sqlTextEtalon));
    }

    #endregion
  }
}