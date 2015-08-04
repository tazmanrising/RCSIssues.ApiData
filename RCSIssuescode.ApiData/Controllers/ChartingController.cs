using RCSIssues.ApiData.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RCSIssues.ApiData.Controllers
{
    public class ChartingController : ApiController
    {



		//[HttpGet]
		[HttpPost]
		[ActionName("GetProblem")]
		public List<Problem> Get()  //Get(int id) 
		{
			

			using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString()))
			{

				string querystring = "Select problem, count(*) as counted from tblRCSIssues group by problem"; 
				SqlCommand sqlCmd = new SqlCommand(querystring, con);
			
				SqlDataAdapter da = new SqlDataAdapter();
				da.SelectCommand = sqlCmd;
				DataTable dt = new DataTable();
				da.Fill(dt);

				
				List<Problem> problems = new List<Problem>();

				foreach (DataRow dtrow in dt.Rows)
				{
					Problem problem = new Problem();
					problem.ProblemName = dtrow[0].ToString();
					problem.Counted = Convert.ToInt32(dtrow[1]);

					problems.Add(problem);

				}

				return problems;


			}


		}



		




    }

}
