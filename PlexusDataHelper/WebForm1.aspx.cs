using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Odbc;
using System.Data;
using System.Web.ModelBinding;
using System.Text;
using System.IO;
namespace PlexusDataHelper
{
	public partial class WebForm1 : System.Web.UI.Page
	{
		private const string ASCENDING = " ASC";
		private const string DESCENDING = " DESC";

        private void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                processFBImage();
                dropDownRecordsPerPage.SelectedValue = "25";
            }

            if (Session["setOfData"] != null)
            {
                GridView_plexRecords.DataSource = (List<DAO_PLEXUS_DATARECORD>)Session["setOfData"];
                GridView_plexRecords.DataBind();

                hideUpload();
            }
            else
            {
                showUpload();
            }
        }

        private void hideUpload()
        {
            FileUpload1.Visible = false;
            Submit1.Visible = false;
            NewUpload.Visible = true;
        }

        private void showUpload()
        {
            FileUpload1.Visible = true;
            Submit1.Visible = true;
            NewUpload.Visible = false;
        }

        private void processFBImage()
        {
            if (DropDownList_topReports.SelectedValue == "sa")
            {
                fbImage.Visible = false;
            }
            else
            {
                fbImage.Visible = true;
            }
        }

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
            if (Session["FileName"] == null)
                Session.Add("FileName", "Please upload a csv file");

            if (Session["TotalPoints"] == null)
                Session.Add("TotalPoints", 0);

            if (Session["ReportMonth"] == null)
                Session.Add("ReportMonth", DateTime.Now.Month);

            if (Session["ReportYear"] == null)
                Session.Add("ReportYear", DateTime.Now.Year);

            if (Session["DropDownList_topReports"] == null)
                Session.Add("DropDownList_topReports", "sa");
			
			InitializeComponent();

            if (Label_FileName != null)
                Label_FileName.Text = Session["FileName"].ToString();

            if (Label_TotPoints != null)
                Label_TotPoints.Text = Session["TotalPoints"].ToString();

            if (monthPicker != null)
                monthPicker.SelectedValue = Session["ReportMonth"].ToString();

            if (yearPicker != null)
                yearPicker.SelectedValue = Session["ReportYear"].ToString();

            AddSortImage(0, null, false);

            base.OnInit(e);
		}

        override protected void OnLoadComplete(EventArgs e)
		{
            base.OnLoadComplete(e);
		}

		private void InitializeComponent()
		{
            this.Submit1.ServerClick += new System.EventHandler(this.Submit1_ServerClick);
            this.NewUpload.ServerClick += new System.EventHandler(this.NewUpload_ServerClick);
            this.Submit_SetReporingMonth_NOW.ServerClick += new System.EventHandler(this.Submit_SetReporingMonth_NOW_ServerClick);
            this.Submit_SetReporingMonth.ServerClick += new System.EventHandler(this.Submit_SetReporingMonth_ServerClick);
            this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion

		public static string PhoneNumber(string value)
		{
			value = new System.Text.RegularExpressions.Regex(@"\D")
				.Replace(value, string.Empty);
			value = value.TrimStart('1');
			if (value.Length == 7)
				return Convert.ToInt64(value).ToString("###-####");
			if (value.Length == 10)
				return Convert.ToInt64(value).ToString("(###) ###-####");
			if (value.Length > 10)
				return Convert.ToInt64(value)
					.ToString("(###) ###-#### " + new String('#', (value.Length - 10)));
			return value;
		}

		private string getRankValue(string inRank)
		{
			switch (inRank)
			{
				case "DA": return "Diamond";
				case "SA": return "Silver";
				case "SrA": return "Senior Ambassador";
				case "A": return "Ambassador";
				case "SrGA": return "Senior Gold";
				case "SrRA": return "Senior Ruby";
				case "RA": return "Ruby";
				case "GA": return "Gold";
				case "EA": return "Emerald";
				case "SpA": return "Sapphire";
				case "ASSC": return "Associate";
				default: return inRank;
			}
		}

		private string cleanString(string inVal)
		{
			if (String.IsNullOrEmpty(inVal))
			{
				return "";
			}
			else
			{
				return inVal.Trim().Substring(0, inVal.Trim().Length - 1);
			}
		}


        List<DAO_PLEXUS_DATARECORD> plexRecords = new List<DAO_PLEXUS_DATARECORD>();

		private Boolean getSuccessfullCSVFile()
		{
			try
			{
				if ((FileUpload1.PostedFile != null) && (FileUpload1.PostedFile.ContentLength > 0))
				{
					try
					{
						if (FileUpload1.HasFile)
						{
							using (Stream fileStream = FileUpload1.PostedFile.InputStream)
							using (StreamReader sr = new StreamReader(fileStream))
							{
								string idNum = null;
								sr.ReadLine();
								int numActive = 0;
								int totPoints = 0;
								Session.Add("TotalPoints", 0);
                                plexRecords.Clear();

								while ((idNum = sr.ReadLine()) != null)
								{
									string[] seps = new string[] { ",\"" };
									string[] myItem = idNum.Split(seps, StringSplitOptions.None);
									
									DAO_PLEXUS_DATARECORD pRecord = new DAO_PLEXUS_DATARECORD();
									pRecord.Level = Int32.Parse(new String(myItem[0].ToCharArray().Where(c => Char.IsDigit(c)).ToArray()));
									pRecord.AmbNum = Int32.Parse(new String(myItem[1].ToCharArray().Where(c => Char.IsDigit(c)).ToArray()));
									pRecord.Name = cleanString(myItem[2].ToString());
									pRecord.PayLvl = Int32.Parse(new String(myItem[5].ToCharArray().Where(c => Char.IsDigit(c)).ToArray()));
									pRecord.JoinDate = DateTime.Parse(cleanString(myItem[6].ToString()));
									pRecord.Points = Int32.Parse(new String(myItem[3].ToCharArray().Where(c => Char.IsDigit(c)).ToArray()));
									pRecord.Cq = cleanString(myItem[7].ToString());;
									pRecord.Active = cleanString(myItem[8].ToString());
									pRecord.Rank = getRankValue(cleanString(myItem[9].ToString()));
									pRecord.Customers = Int32.Parse(new String(myItem[10].ToCharArray().Where(c => Char.IsDigit(c)).ToArray()));
									pRecord.Pv = double.Parse(cleanString(myItem[4].ToString()));
									pRecord.Phone = PhoneNumber(cleanString(myItem[11].ToString()));
									pRecord.Email = cleanString(myItem[12].ToString());
                                    pRecord.TotalPoints = 0;
									plexRecords.Add(pRecord);

									if (pRecord.Active == "Y")
									{
										numActive = numActive + 1;
										Session["TotalActive"] = numActive;
									}

									totPoints = totPoints + pRecord.Points;
								}
                                Session["setOfData"] = plexRecords;
                                Session["TotalPoints"] = totPoints;
                                Session["FileName"] = FileUpload1.PostedFile.FileName;

								Label_TotPoints.Text = Session["TotalPoints"].ToString();
								Label_FileName.Text = Session["FileName"].ToString();
							}

						}
						else
						{
							//Do Something here at some point
						}
					}
					catch (Exception ex)
					{

						//Response.Write("ERROR: " + ex.Message + "<br/>" + ex.StackTrace + "<br/><br/>");
						Response.Write("Please make sure you are uploading the \"Ambassador Genealogy Report\" that you got off of your myplexusproducts.com page<br/>");
						Response.Write("The file MUST be exported as a CSV file<br/><br/>");
						Response.Write("How?<br/>");
						Response.Write("To the right of the \"PROCESS REPORT\" button change the \"Export Options\" to \"Comma-delimited (*.csv)\"");

						//Note: Exception.Message returns a detailed message that describes the current exception. 
						//For security reasons, we do not recommend that you return Exception.Message to end users in 
						//production environments. It would be better to return a generic error message. 
					}
					finally
					{
						try
						{
							//string SaveLocation = Server.MapPath("Data") + "\\" + fn;
							//File1.PostedFile.SaveAs(SaveLocation);
							Response.Write("The file has been uploaded and processed.");
							GridView_plexRecords.DataSource = (List<DAO_PLEXUS_DATARECORD>)Session["setOfData"];
							GridView_plexRecords.DataBind();
						}
						catch (Exception ex)
						{
							Response.Write("Error: " + ex.Message);
							//Note: Exception.Message returns a detailed message that describes the current exception. 
							//For security reasons, we do not recommend that you return Exception.Message to end users in 
							//production environments. It would be better to return a generic error message. 
						}
						//conn.Close();
					}
                    return true;
				}
				else
				{
					Response.Write("Please select a file to upload.");
                    return false;
				}
			}
			catch (Exception ex)
			{
				Response.Write("Houston we have a problem: " + ex.Message);
                return false;
			}
		}

		private Boolean loadDataForGridView()
		{
			return getSuccessfullCSVFile();
		}

		private SortDirection GridViewSortDirection
		{
			get
			{
				if (Session["sortDirection"] == null)
					Session["sortDirection"] = SortDirection.Descending;
				return (SortDirection)Session["sortDirection"];
			}
			set { Session["sortDirection"] = value; }
		}

		private string GridViewSortExpression
		{
			get
			{
				if (Session["SortExpression"] == null)
					Session["SortExpression"] =  "-1";
				return Session["SortExpression"].ToString();
			}
			set { Session["SortExpression"] = value; }
		}

		protected void GridView_plexRecords_Sorting(object sender, GridViewSortEventArgs e)
		{
			string sortExpression = e.SortExpression;

			GridViewSortExpression = sortExpression;

            if (GridViewSortDirection == SortDirection.Descending)
            {
				GridViewSortDirection = SortDirection.Ascending;
                SortGridView(sortExpression, ASCENDING, getFilterCriteria());
			}
            else
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridView(sortExpression, DESCENDING, getFilterCriteria());
            }
		}

		protected void GridView_plexRecords_RowCreated(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.Header)
			{
				int sortColumnIndex = GetSortColumnIndex();
				if (sortColumnIndex != -1)
				{
					AddSortImage(sortColumnIndex, e.Row, true);
				}
			}
		}

		private int GetSortColumnIndex()
		{
			foreach (DataControlField field in GridView_plexRecords.Columns)
			{
                if (GridViewSortExpression == "-1")
                {
                    return -1;
                }

				if (field.SortExpression == GridViewSortExpression)
				{
					return GridView_plexRecords.Columns.IndexOf(field);
				}
			}
			return -1;
		}

		private void AddSortImage(int columnIndex, GridViewRow headerRow, Boolean show)
		{
			Image sortImage = new Image();
			if (GridViewSortDirection == SortDirection.Ascending)
			{
                sortImage.ImageUrl = "style/uparrow.gif";
				sortImage.AlternateText = "Ascending Order";
			}
			else
			{
				sortImage.ImageUrl = "style/downarrow.gif";
				sortImage.AlternateText = "Descending Order";
			}

            if (show)
            {   
                sortImage.Visible = true;
                headerRow.Cells[columnIndex].Controls.Add(sortImage);
            }
            else
                sortImage.Visible = false;
		}

		private void SortGridView(string sortExpression, string direction, string filter)
		{
			List<DAO_PLEXUS_DATARECORD> allRecords = (List<DAO_PLEXUS_DATARECORD>)Session["setOfData"];

			DataTable dt = new DataTable();
			DataColumn col_level = new DataColumn("Level", typeof(Int32));
			DataColumn col_ambNum = new DataColumn("AmbNum", typeof(Int32));
			DataColumn col_name = new DataColumn("Name", typeof(String));
			DataColumn col_payLvl = new DataColumn("PayLvl", typeof(String));
			DataColumn col_joinDate = new DataColumn("JoinDate", typeof(DateTime));
			DataColumn col_point = new DataColumn("Points", typeof(Int32));
			DataColumn col_cq = new DataColumn("Cq", typeof(String));
			DataColumn col_active = new DataColumn("Active", typeof(String));
			DataColumn col_rank = new DataColumn("Rank", typeof(String));
			DataColumn col_cust = new DataColumn("Customers", typeof(Int32));
			DataColumn col_pv = new DataColumn("Pv", typeof(double));
			DataColumn col_phone = new DataColumn("Phone", typeof(String));
            DataColumn col_email = new DataColumn("Email", typeof(String));
            DataColumn col_totPoints = new DataColumn("TotalPoints", typeof(Int32));
			
			dt.Columns.Add(col_level);
			dt.Columns.Add(col_ambNum);
			dt.Columns.Add(col_name);
			dt.Columns.Add(col_payLvl);
			dt.Columns.Add(col_joinDate);
			dt.Columns.Add(col_point);
			dt.Columns.Add(col_cq);
			dt.Columns.Add(col_active);
			dt.Columns.Add(col_rank);
			dt.Columns.Add(col_cust);
			dt.Columns.Add(col_pv);
            dt.Columns.Add(col_phone);
            dt.Columns.Add(col_email);
            dt.Columns.Add(col_totPoints);

			if (allRecords == null)
				return;

			foreach (DAO_PLEXUS_DATARECORD rec in allRecords)
			{
				DataRow drow = dt.NewRow();
				drow[col_level] = rec.Level;
				drow[col_ambNum] = rec.AmbNum;
				drow[col_name] = rec.Name;
				drow[col_payLvl] = rec.PayLvl;
				drow[col_joinDate] = rec.JoinDate;
				drow[col_point] = rec.Points;
				drow[col_cq] = rec.Cq;
				drow[col_active] = rec.Active;
				drow[col_rank] = rec.Rank;
				drow[col_cust] = rec.Customers;
				drow[col_pv] = rec.Pv;
				drow[col_phone] = rec.Phone;
                drow[col_email] = rec.Email;
                drow[col_totPoints] = rec.TotalPoints;
				dt.Rows.Add(drow);
			}

			DataView dv = new DataView(dt);
            if (sortExpression != "-1")
    			dv.Sort = sortExpression + direction;
            dv.RowFilter = filter; 
			GridView_plexRecords.DataSource = dv;
			GridView_plexRecords.DataBind();
		}

        private void Submit_SetReporingMonth_NOW_ServerClick(object sender, System.EventArgs e)
        {
            Session.Remove("ReportMonth");
            Session.Remove("ReportYear");

            Session.Add("ReportMonth", DateTime.Now.Month);
            Session.Add("ReportYear", DateTime.Now.Year);

            monthPicker.SelectedValue = Session["ReportMonth"].ToString();
            yearPicker.SelectedValue = Session["ReportYear"].ToString();

            if (Session["SortExpression"] != null)
            {
                if (GridViewSortDirection == SortDirection.Ascending)
                {
                    SortGridView(Session["SortExpression"].ToString(), ASCENDING, getFilterCriteria());
                }

                else
                {
                    SortGridView(Session["SortExpression"].ToString(), DESCENDING, getFilterCriteria());
                }
            }
            Response.Redirect("index.aspx");
        }

        private void Submit_SetReporingMonth_ServerClick(object sender, System.EventArgs e)
        {
            Session.Remove("ReportMonth");
            Session.Remove("ReportYear");

            Session.Add("ReportMonth", monthPicker.SelectedValue);
            Session.Add("ReportYear", yearPicker.SelectedValue);

            if (Session["SortExpression"] != null)
            {
                if (GridViewSortDirection == SortDirection.Ascending)
                {
                    SortGridView(Session["SortExpression"].ToString(), ASCENDING, getFilterCriteria());
                }

                else
                {
                    SortGridView(Session["SortExpression"].ToString(), DESCENDING, getFilterCriteria());
                }
            }
            Response.Redirect("index.aspx");
        }

		private void Submit1_ServerClick(object sender, System.EventArgs e)
		{
            DropDownList_topReports.SelectedValue = "sa";
            processFBImage();
            if (loadDataForGridView())
            {
                treeNodeRelationship.Clear();
                createTreeNodeRelationship();
                updatePayPoints();

                Session["treeViewData"] = treeNodeRelationship;
                Session["plexRecords"] = plexRecords;

                hideUpload();
            }
            else
                showUpload();
		}

        private void NewUpload_ServerClick(object sender, System.EventArgs e)
        {
            showUpload();
        }

        private void updatePayPoints()
        {
            foreach (DAO_PLEXUS_DATARECORD rec in plexRecords)
            {
                if ((rec.Pv >= 100) && (rec.TotalPoints == 0))
                    rec.TotalPoints = getAccuratePayPoints(rec.AmbNum);
            }
            setFilterCriteria("1=1");
            //Session["SortExpression"] = "Pv";
            GridViewSortDirection = SortDirection.Descending;
            SortGridView("", "", getFilterCriteria());
        }

		protected void GridView_plexRecords_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			GridView_plexRecords.PageIndex = e.NewPageIndex;
			
			if (GridViewSortDirection == SortDirection.Ascending)
			{
                SortGridView(Session["SortExpression"].ToString(), ASCENDING, getFilterCriteria());
			}

			else
			{
				SortGridView(Session["SortExpression"].ToString(), DESCENDING, getFilterCriteria());
			} 
		}

        protected void DropDownList_topReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session.Remove("top25_1");
            Session.Remove("top25_1_4");
            Session.Remove("top25_1_7");
            Session.Remove("top25_1_12");

            Session["DropDownList_topReports"] = DropDownList_topReports.SelectedValue;

            if (DropDownList_topReports.SelectedValue == "sa")
            {
                setFilterCriteria("1=1");
                Session["SortExpression"] = "Pv";
                GridViewSortDirection = SortDirection.Descending;
                SortGridView("Pv", DESCENDING, getFilterCriteria());
            }

            if (DropDownList_topReports.SelectedValue == "t25pv")
            {
                setFilterCriteria("([Level] <= 1) and ([Level] <> 0) and ([Pv] > 0)");
                Session["SortExpression"] = "Pv";
                GridViewSortDirection = SortDirection.Descending;
                SortGridView("Pv", DESCENDING, getFilterCriteria());
                prepTop25Popup(10, "top25_1");

                setFilterCriteria("([Level] <= 4) and ([Level] <> 0) and ([Pv] > 0)");
                SortGridView("Pv", DESCENDING, getFilterCriteria());
                prepTop25Popup(10, "top25_1_4");

                setFilterCriteria("([Level] <= 7) and ([Level] <> 0) and ([Pv] > 0)");
                SortGridView("Pv", DESCENDING, getFilterCriteria());
                prepTop25Popup(10, "top25_1_7");

                setFilterCriteria("([Level] <= 12) and ([Level] <> 0) and ([Pv] > 0)");
                SortGridView("Pv", DESCENDING, getFilterCriteria());
                prepTop25Popup(10, "top25_1_12");

            }

            if (DropDownList_topReports.SelectedValue == "sc")
            {
                setFilterCriteria("([Active] = 'Y') and ([Pv] < 100) and ([Pv] > 65) and ([Level] <= 1) and ([Level] <> 0)");
                Session["SortExpression"] = "Pv";
                GridViewSortDirection = SortDirection.Descending;
                SortGridView("Pv", DESCENDING, getFilterCriteria());
                prepTop25Popup(10, "top25_1");

                setFilterCriteria("([Active] = 'Y') and ([Pv] < 100) and ([Pv] > 65) and ([Level] <= 4) and ([Level] <> 0)");
                SortGridView("Pv", DESCENDING, getFilterCriteria());
                prepTop25Popup(10, "top25_1_4");

                setFilterCriteria("([Active] = 'Y') and ([Pv] < 100) and ([Pv] > 65) and ([Level] <= 7) and ([Level] <> 0)");
                SortGridView("Pv", DESCENDING, getFilterCriteria());
                prepTop25Popup(10, "top25_1_7");

                setFilterCriteria("([Active] = 'Y') and ([Pv] < 100) and ([Pv] > 65) and ([Level] <= 12) and ([Level] <> 0)");
                SortGridView("Pv", DESCENDING, getFilterCriteria());
                prepTop25Popup(10, "top25_1_12");
            }
            processFBImage();
        }

        private void prepTop25Popup(int colToDisplay, string top25sessionKey)
        {
            if (GridView_plexRecords.Rows.Count > 0)
            {
                string s = "";
                for (int i = 0; i < (GridView_plexRecords.Rows.Count > 25 ? 25 : GridView_plexRecords.Rows.Count); i++)
                {
                    if (DropDownList_topReports.SelectedValue == "sc")
                    {
                        s = s + "<b>#" + (i + 1) + " " + GridView_plexRecords.Rows[i].Cells[2].Text + "</b> (" + GridView_plexRecords.Rows[i].Cells[0].Text + ") - " + GridView_plexRecords.Rows[i].Cells[colToDisplay].Text + " - "
                            + GridView_plexRecords.Rows[i].Cells[11].Text + " <BR/>";
                    }
                    else
                    {
                        s = s + "<b>#" + (i + 1) + " " + GridView_plexRecords.Rows[i].Cells[2].Text + "</b> (" + GridView_plexRecords.Rows[i].Cells[0].Text + ") - " + GridView_plexRecords.Rows[i].Cells[colToDisplay].Text + "<BR/>";
                    }
                }
                Session.Add("dropDownSelection", DropDownList_topReports.SelectedItem.Text);
                Session.Add(top25sessionKey, s);
            }
        }       

		protected void dropDownRecordsPerPage_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (dropDownRecordsPerPage.SelectedValue != "All")
			{
				GridView_plexRecords.PageSize = Int32.Parse(dropDownRecordsPerPage.SelectedValue);
			}
			else
			{
				GridView_plexRecords.PageSize = ((List<DAO_PLEXUS_DATARECORD>)Session["setOfData"]).Count;
			}

			if (GridViewSortDirection == SortDirection.Ascending)
			{
                SortGridView(GridViewSortExpression, ASCENDING, getFilterCriteria());
			}

			else
			{
                SortGridView(GridViewSortExpression, DESCENDING, getFilterCriteria());
			}
			
		}

        private string getFilterCriteria()
        {
            if (Session["filerCriteria"] == null)
            {
                return "1=1";
            }
            else
            {
                string fc = Session["filerCriteria"].ToString();
                return fc;
            }
        }

        private void setFilterCriteria(string infilterCriteria) { Session["filerCriteria"] = infilterCriteria; }

		protected void dropDownRecordsPerPage_Init(object sender, EventArgs e)
		{
			GridView_plexRecords.PageSize = 25;
		}

        Dictionary<int, int> treeNodeRelationship = new Dictionary<int, int>();
            
        private void createTreeNodeRelationship()
        {
            int currentParent = -1;
            int currentLevel = -1;
            int previousLevel = -1;

            foreach (DAO_PLEXUS_DATARECORD row in (List<DAO_PLEXUS_DATARECORD>)Session["setOfData"])
            {
                currentLevel = row.Level;
                
                if (currentLevel == 0)
                {
                    currentParent = row.AmbNum;
                    previousLevel = row.Level;
                    treeNodeRelationship.Add(row.AmbNum, -1);
                }
                else
                {
                    if (currentLevel > previousLevel)
                    {
                        treeNodeRelationship.Add(row.AmbNum, currentParent);
                        previousLevel = row.Level;
                        currentParent = row.AmbNum;
                    }
                    else
                    {
                        treeNodeRelationship.Add(row.AmbNum, lookUp(currentParent, (previousLevel - currentLevel) + 1));
                        currentParent = row.AmbNum;
                        previousLevel = row.Level;
                    }
                }
            }

            PopulateTreeView(-1, null);
        }

        public TreeView myTree = new TreeView();
        
        private void PopulateTreeView(int parentId, TreeNode treeNode)
        {
            foreach (var pair in treeNodeRelationship.Where(x => x.Value == parentId))
            {
                var key = pair.Key;
                var value = pair.Value;

                DAO_PLEXUS_DATARECORD dr = plexRecords.Find(x => x.AmbNum == key);

                TreeNode child = new TreeNode
                {
                    Text = dr.AmbNum.ToString(),
                    Value = dr.AmbNum.ToString()
                };

                if (parentId == -1)
                {
                    myTree.Nodes.Add(child);
                    PopulateTreeView(int.Parse(child.Value), child);
                }
                else
                {
                    treeNode.ChildNodes.Add(child);
                    PopulateTreeView(int.Parse(child.Value), child);
                }
            }

        }

        private int lookUp(int selfID, int numLevelsUp)
        {
            int parentID = -1;
                
            for (int i = 0; i < numLevelsUp; i++)
            {
                treeNodeRelationship.TryGetValue(selfID, out parentID);
                selfID = parentID;
            }

            return parentID;
        }

        List<int> downChildren = new List<int>();

        private List<int> lookDown(int ambNum)
        {
            downChildren.Clear();
            moreChildren(ambNum);
            return downChildren;
        }

        private void moreChildren(int ambNum)
        {
            List<int> tmpDownChildren = new List<int>();

            foreach (var pair in treeNodeRelationship.Where(x => x.Value == ambNum))
            {
                var key = pair.Key;
                tmpDownChildren.Add(key);
                downChildren.Add(key);
            }

            foreach (int child in tmpDownChildren)
            {
                moreChildren(child);
            }
        }

        private int getAccuratePayPoints(int ambNum)
        {
            int parentAmbNum = ambNum;
            int runningPoints = 0;

            DAO_PLEXUS_DATARECORD parentAmbN = plexRecords.Find(x => x.AmbNum == ambNum);

            foreach (int childAmbNum in lookDown(ambNum))
            {
                DAO_PLEXUS_DATARECORD dr = plexRecords.Find(x => x.AmbNum == childAmbNum);

                int inContextPayLevel = dr.PayLvl - parentAmbN.PayLvl;
                int inContextLevel = dr.Level - parentAmbN.Level;

                if ((dr.Points != 0) && (dr.Level <= 7))
                {
                    //runningPoints = runningPoints + payPoints(getAccuratePayLevel(childAmbNum, ambNum));
                    runningPoints = runningPoints + payPoints(inContextPayLevel);
                }
                else if ((dr.Pv > 100) && (dr.Level > 7) && (dr.Pv != 199.00))
                {
                    //runningPoints = runningPoints + payPoints(getAccuratePayLevel(childAmbNum, ambNum));
                    runningPoints = runningPoints + payPoints(inContextPayLevel);
                }
            }
            return runningPoints;
        }

        private int payPoints(int payLevel)
        {
            switch(payLevel)
            {
                case 1: return 5;
                case 2: return 5;
                case 3: return 5;
                case 4: return 4;
                case 5: return 3;
                case 6: return 2;
                case 7: return 1;
                default: return 0;
            }
        }

        private int getAccuratePayLevel(int childAmbNum, int parentAmbNum)
        {
            currentlevel = 1;
            foundIt = false;
            compressLevel = 0;

            foundIt = recursivePayLevelEngine(childAmbNum, parentAmbNum);

            if (foundIt)
                return currentlevel - compressLevel;
            else
                return -1;
        }

        int currentlevel;
        int compressLevel;
        Boolean foundIt;

        private Boolean recursivePayLevelEngine(int childAmbNum, int parentAmbNum)
        {
            List<int> tmp = new List<int>();

            foreach (var pair in treeNodeRelationship.Where(x => x.Value == parentAmbNum))
            {
                var key = pair.Key;
                var value = pair.Value;

                DAO_PLEXUS_DATARECORD dr = plexRecords.Find(x => x.AmbNum == key);

                //if (dr.Pv >= 100)
                tmp.Add(key);
            }

            if (tmp.Contains(childAmbNum))
            {
                foundIt = true;
            }
            else
            {
                currentlevel = currentlevel + 1;

                foreach (int child in tmp)
                {
                    foundIt = recursivePayLevelEngine(childAmbNum, child);
                    if (!foundIt)
                    {
                        currentlevel = currentlevel - 1;
                    }
                    else
                    {
                        DAO_PLEXUS_DATARECORD dr = plexRecords.Find(x => x.AmbNum == child);

                        if (dr.Pv < 100)
                        {
                            compressLevel = compressLevel + 1;
                        }
                        return foundIt;
                    }
                }
            }

            return foundIt;
        }

        private bool IsTheSameMonth(DateTime date1)
        {
            return (date1.Year == Int32.Parse(Session["ReportYear"].ToString()) && date1.Month == Int32.Parse(Session["ReportMonth"].ToString()));
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //#36dbca = Active
                //#93db70 = New

                if (IsTheSameMonth((DateTime.Parse(e.Row.Cells[4].Text))))
                {
                    foreach (TableCell cell in e.Row.Cells)
                    {
                        cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#93db70");
                        cell.ForeColor = System.Drawing.Color.Black;
                    }
                }
                else if (e.Row.Cells[7].Text == "Y")
                {
                    foreach (TableCell cell in e.Row.Cells)
                    {
                        cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#36dbca");
                        cell.ForeColor = System.Drawing.Color.Black;
                    }
                }
            }
        }
    }


	[Serializable]
	public class DAO_PLEXUS_DATARECORD
	{
		private int _level;
		private int _ambNum;
		private string _name;
		private int _payLvl;
		private DateTime _joinDate;
		private int _points;
		private string _cq;
		private string _active;
		private string _rank;
		private int _cust;
		private double _pv;
		private string _phone;
        private string _email;
        private int _totalPoints;

		public DAO_PLEXUS_DATARECORD()
		{ }

		public int Level { get { return _level; } set { _level = value; } }
		public int AmbNum { get { return _ambNum; } set { _ambNum = value; } }
		public string Name { get { return _name; } set { _name = value; } }
		public int PayLvl { get { return _payLvl; } set { _payLvl = value; } }
		public DateTime JoinDate { get { return _joinDate; } set { _joinDate = value; } }
		public int Points { get { return _points; } set { _points = value; } }
		public string Cq { get { return _cq; } set { _cq = value; } }
		public string Active { get { return _active; } set { _active = value; } }
		public string Rank { get { return _rank; } set { _rank = value; } }
		public int Customers { get { return _cust; } set { _cust = value; } }
		public double Pv { get { return _pv; } set { _pv = value; } }
        public string Phone { get { return _phone; } set { _phone = value; } }
        public string Email { get { return _email; } set { _email = value; } }
        public int TotalPoints { get { return _totalPoints; } set { _totalPoints = value; } }
	}

}
