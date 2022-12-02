using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
namespace latest.Pages.Modules
{
    public class IndexModel : PageModel
    {
        public List<ModuleInfo> listModules = new List<ModuleInfo>();
        public void OnGet()
        {
            try
            {

                string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=myTimer;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = "select * from TimerData";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ModuleInfo moduleInfo = new ModuleInfo();
                                moduleInfo.Id = "" + reader.GetInt32(0);
                                moduleInfo.Module_Name = reader.GetString(1);
                                moduleInfo.Module_Code = reader.GetString(2);
                                moduleInfo.Number_Of_Credits = "" + reader.GetInt32(3);
                                moduleInfo.Class_Hours_Per_Week = "" + reader.GetInt32(4);
                                moduleInfo.Number_Of_Weeks_In_Semester = "" + reader.GetInt32(5);
                                moduleInfo.Study_Hours_Remaining = "" + reader.GetInt32(6);
                                moduleInfo.created_at = reader.GetDateTime(7).ToString();

                                listModules.Add(moduleInfo);
                            }
                        }
                    }

                }


            }

            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }

        }
        public class ModuleInfo
        {
            public string Id;
            public string Module_Name;
            public string Module_Code;
            public string Number_Of_Credits;
            public string Class_Hours_Per_Week;
            public string Number_Of_Weeks_In_Semester;
            public string Study_Hours_Remaining;
            public string created_at;

        }
    }
}
