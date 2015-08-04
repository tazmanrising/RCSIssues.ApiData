using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RCSIssues.ApiData.Models
{
	public class tblTips
	{
		public int id { get; set; }
		public int group { get; set; }
		public string title { get; set; }
		public string body { get; set; }
		public string createdby { get; set; }

		//[DataType(DataType.Date)]
		//[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		//[Display(Name = "Start Date")]
		public DateTime created { get; set; }
		public string updatedBy { get; set; }
		public DateTime updated { get; set; }
	}


}