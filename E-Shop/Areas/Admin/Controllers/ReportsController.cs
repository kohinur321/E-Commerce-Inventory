using AspNetCore.Reporting;
using E_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace E_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReportsController : Controller
    {      
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        public ReportsController(
            IWebHostEnvironment webHostEnvironment,
            IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// RDLC Report Using Store Procedure
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ProductDetailsReports()
        {
            string order = "application/pdf";
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\CompleteOrder.rdlc";
            var data = new List<OrderDetailsViewModel>();
            string storedProcedure = "orderDetails";

            string connectionString = _configuration.GetConnectionString("EShop");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(storedProcedure, con);
                da.SelectCommand.CommandTimeout = 250;
                DataTable dt = new DataTable();
                da.Fill(dt);
                data = ConvertToList<OrderDetailsViewModel>(dt);
                con.Close();
            }
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("ReportName", "Order Details");
            parameters.Add("CompanyName", "E-Shop");
            parameters.Add("PrintOutDate", DateTime.Now.ToString("g"));
            LocalReport report = new LocalReport(path);
            report.AddDataSource("orderDataSet", data);
            var result = report.Execute(RenderType.Pdf, extension, parameters, order);
            return File(result.MainStream, order);
        }
        public List<T> ConvertToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>()
                    .Select(c => c.ColumnName)
                    .ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    try
                    {
                        if (columnNames.Contains(pro.Name))
                        {
                            PropertyInfo pI = objT.GetType().GetProperty(pro.Name);
                            pro.SetValue(objT, row[pro.Name] == DBNull.Value ? null : Convert.ChangeType(row[pro.Name], pI.PropertyType));
                        }

                    }
                    catch (Exception)
                    {

                    }
                }
                return objT;
            }).ToList();
        }
    }
}
