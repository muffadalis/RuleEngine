using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
public class WizardRules
{
    public double Premium { get; set; }
    public QuoteDecision Decision { get; set; }

    public QuoteDecision RunRule(Guid quoteId)
    {
        Decision = QuoteDecision.Accept;

        dynamic Want_Product = GetPropFormValue("Want_Product", quoteId);
        dynamic Product_Value = GetPropFormValue("Product_Value", quoteId);
        if (Convert.ToBoolean(Want_Product))
        {
            Premium = Product_Value * 2.5;
        }
        else
        {
            Decision = QuoteDecision.Refer;
        }


        return Decision;
    }

    private dynamic GetPropFormValue(string tagName, Guid quoteId)
    {
        dynamic internalResult = null;

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Wizard3DB"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.CommandText = @"WIZ_RTF_GET_PROP_FORM_VALUE_FROM_QUOTE_ID";

            SqlParameter paramId = new SqlParameter("@id", SqlDbType.UniqueIdentifier);
            paramId.Direction = ParameterDirection.Input;
            paramId.Value = quoteId;
            cmd.Parameters.Add(paramId);

            SqlParameter paramTagName = new SqlParameter("@tagName", SqlDbType.NVarChar, 1000);
            paramTagName.Direction = ParameterDirection.Input;
            paramTagName.Value = tagName;
            cmd.Parameters.Add(paramTagName);

            SqlParameter paramValueInt = new SqlParameter("@valueInt", SqlDbType.Int);
            paramValueInt.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(paramValueInt);

            SqlParameter paramValueFloat = new SqlParameter("@valueFloat", SqlDbType.Float);
            paramValueFloat.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(paramValueFloat);

            SqlParameter paramValueText = new SqlParameter("@valueText", SqlDbType.NVarChar, 65536);
            paramValueText.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(paramValueText);

            SqlParameter paramValueDate = new SqlParameter("@valueDate", SqlDbType.DateTime);
            paramValueDate.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(paramValueDate);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Dispose();

            if (paramValueInt.Value != DBNull.Value) internalResult = (int)paramValueInt.Value;
            if (paramValueFloat.Value != DBNull.Value) internalResult = (double)paramValueFloat.Value;
            if (paramValueText.Value != DBNull.Value) internalResult = paramValueText.Value.ToString();
            if (paramValueDate.Value != DBNull.Value) internalResult = (DateTime)paramValueDate.Value;

        } //using

        return internalResult;
    }
}

public enum QuoteDecision
{
    None = 0,
    Accept,
    Refer,
    Decline,
    NotRequired
}