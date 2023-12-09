using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=GLaDOS\\MARTINSQL;Initial Catalog=mystore;Integrated Security=True";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    String sql = "SELECT * FROM clients";
                    using(SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                                clientInfo.created_at = reader.GetDateTime(5).ToString();

                                listClients.Add(clientInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Exception: " + ex.ToString);
            }
        }
    }

    public class ClientInfo
    {
        public String id;
        public String name;
        public String email;
        public String phone;
        public String address;
        public String created_at;
    }
}
