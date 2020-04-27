using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ProductMaintenance
{
    class ProductDB
    {
        public static Product GetProduct(string productCode)
        {
            SqlConnection connection = MMABooksDB.GetConnection();
            SqlCommand selectCommand = new SqlCommand(
                "SELECT * FROM Products WHERE ProductCode = @ProductCode", connection);
            selectCommand.Parameters.AddWithValue("@ProductCode", productCode);

            try
            {
                connection.Open();
                SqlDataReader productReader = selectCommand.ExecuteReader(CommandBehavior.SingleRow);

                if(productReader.Read())
                {
                    Product product = new Product();
                    product.Code = productReader["ProductCode"].ToString();
                    product.Description = productReader["Description"].ToString();
                    product.Price = (decimal)productReader["UnitPrice"];
                    return product;
                }
                else
                {
                    return null;
                }
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool UpdateProduct(Product updatedProduct)
        {
            SqlConnection connection = MMABooksDB.GetConnection();
            SqlCommand updateCommand = new SqlCommand(
                "UPDATE Products SET " +
                "Description = @NewDescription, " +
                "UnitPrice = @NewPrice " +
                "WHERE ProductCode = @OldProductCode", connection);
            updateCommand.Parameters.AddWithValue(
                "@NewDescription", updatedProduct.Description);
            updateCommand.Parameters.AddWithValue(
                "@NewPrice", updatedProduct.Price);
            updateCommand.Parameters.AddWithValue(
                "@OldProductCode", updatedProduct.Code);

            try
            {
                connection.Open();
                int count = updateCommand.ExecuteNonQuery();
                if (count > 0) return true;
                else return false;
            }
            catch(SqlException exception)
            {
                throw exception;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool AddProduct(Product product)
        {
            SqlConnection connection = MMABooksDB.GetConnection();
            SqlCommand insertCommand =
                new SqlCommand(
                    "INSERT Products " +
                    "(ProductCode, Description, UnitPrice) " +
                    "VALUES (@ProductCode, @Description, @UnitPrice)", connection);
            insertCommand.Parameters.AddWithValue(
                "@ProductCode", product.Code);
            insertCommand.Parameters.AddWithValue(
                "@Description", product.Description);
            insertCommand.Parameters.AddWithValue(
                "@UnitPrice", product.Price);

            try
            {
                connection.Open();
                int count = insertCommand.ExecuteNonQuery();

                if (count > 0) return true;
                else return false;
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool DeleteProduct(Product product)
        {
            SqlConnection connection = MMABooksDB.GetConnection();
            SqlCommand deleteCommand = new SqlCommand(
                "DELETE FROM Products " +
                "WHERE ProductCode = @ProductCode ", connection);
            deleteCommand.Parameters.AddWithValue(
                "@ProductCode", product.Code);

            try
            {
                connection.Open();
                int count = deleteCommand.ExecuteNonQuery();
                if (count > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
