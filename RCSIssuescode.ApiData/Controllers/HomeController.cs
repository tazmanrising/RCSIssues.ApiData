using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RCSIssues.ApiData.Models;
using PagedList;

namespace RCSIssues.ApiData.Controllers
{
	public class HomeController : Controller
	{

		public static T ConvertFromDBVal<T>(object obj)
		{
			if (obj == null || obj == DBNull.Value)
			{
				return default(T); // returns the default value for the type
			}
			else
			{
				return (T)obj;
			}
		}
		
		
		public ActionResult Index()
		{
			ViewBag.Title = "Home Page";

			return View();
		}


		public ActionResult Grid()
		{
			return View();

		}

		public ActionResult GridDataBinding()
		{
			return View();
		}


		//public ActionResult GridWidget()
		//{
		//	//use this method ???
		//	return View();
		//}


		public ViewResult GridWidget(string sortOrder, string currentFilter, string searchString, int? page, string builtString)
		{
			
			ViewBag.CurrentSort = sortOrder;
			ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
			ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

			ViewBag.builtString = builtString;


			if (searchString != null)
			{
				page = 1;
			}
			else
			{
				searchString = currentFilter;
			}

			ViewBag.CurrentFilter = searchString;

			//List<RCSIssues.ApiData.Models.Problem> probl

			List<tblTips> tips = new List<tblTips>();

			

			using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString()))
			{

				//RCS Issues
				//string querystring = "Select problem, count(*) as counted from tblRCSIssues group by problem";

				// Gate Codes
				//SELECT ID as id , Dist as district, Address as address, ApartmentName as location, RAS as ras, Incode as incode, Forty as forty, CoordinatesEW as ewcoord, CoordinatesNS as nscoord, Comments as comment  FROM tblGateCodes'


				// doc home News Tips
				string querystring = "SELECT [id],[group],[title],[body],[createdBy],[created],[updatedBy],[updated] FROM [tblTips] ";
				// if (!String.IsNullOrEmpty(builtString)) { }
				if (builtString != "" || builtString != "WHERE Choose a Column >   " || builtString != null)
				{
					querystring += " " + builtString;
				}

				//SqlCommand sqlCmd = new SqlCommand(querystring, con);
				


				// DataTable example
				//var list = dt.AsEnumerable().Select(r => new Person() { 
				//	Name = (string) r["Name"], 
				//	Age = (int) r["Age"] }
				//).ToList()

				// OR 
				//var list = (from tr in dt.AsEnumerable()
				//			select new Person()
				//			{
				//				Name = tr.Field<string>("Name"),
				//				Age = tr.Field<int>("Age")
				//			}).ToList();


				

				using(var command = con.CreateCommand())
				{
					command.CommandText = querystring;
					command.CommandType = CommandType.Text;

					con.Open();
					using(var reader = command.ExecuteReader())
					{
						
						while(reader.Read())
						{

							//Unable to cast object of type 'System.DBNull' to type 'System.String`

							tips.Add(
								new tblTips() {
									id =  ConvertFromDBVal<int>(reader["id"]),  //(int)reader["id"],
									group = ConvertFromDBVal<int>(reader["group"]),  //(int) reader["group"],
									title = ConvertFromDBVal<string>(reader["title"]), //(string) reader["title"],
									body = ConvertFromDBVal<string>(reader["body"]), //(string) reader["body"],
									createdby = ConvertFromDBVal<string>(reader["createdBy"]), //(string) reader["createdBy"],
									created = ConvertFromDBVal<DateTime>(reader["created"]), //(DateTime) reader["created"],
									updatedBy = ConvertFromDBVal<string>(reader["updatedBy"]), //(string) reader["updatedBy"],
									updated = ConvertFromDBVal<DateTime>(reader["updated"]), //(DateTime) reader["updated"]
								});


						}

					}


				}

				//SqlDataAdapter da = new SqlDataAdapter();
				//da.SelectCommand = sqlCmd;
				//DataTable dt = new DataTable();
				//da.Fill(dt);

				//List<RCSIssues.ApiData.Models.Problem> problems = new List<RCSIssues.ApiData.Models.Problem>();
				
				//var tip = from s in tips
						  //select s;


				int pageSize = 10;
				int pageNumber = (page ?? 1);
				var x = tips.Count();

				ViewBag.TotalCount = x.ToString();

				//foreach (DataRow dtrow in dt.Rows)
				//{
				//	RCSIssues.ApiData.Models.Problem problem = new RCSIssues.ApiData.Models.Problem();
				//	problem.ProblemName = dtrow[0].ToString();
				//	problem.Counted = Convert.ToInt32(dtrow[1]);

				//	problems.Add(problem);

				//}

				//return problems;
				return View(tips.ToPagedList(pageNumber, pageSize));

			}


			

			//return View();




		}



	}





}
