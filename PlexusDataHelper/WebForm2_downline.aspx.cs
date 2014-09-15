using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlexusDataHelper
{
    public partial class WebForm2_downline : System.Web.UI.Page
    {
        Dictionary<int, int> treeNodeRelationship;
        List<DAO_PLEXUS_DATARECORD> plexRecords;
        string rMonth;
        string rYear;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (TreeView_plexTree.Nodes.Count == 0)
            {
                if (Page.Session["plexRecords"] != null)
                {
                    treeNodeRelationship = (Dictionary<int, int>)Page.Session["treeViewData"];
                    plexRecords = (List<DAO_PLEXUS_DATARECORD>)Page.Session["plexRecords"];
                    rMonth = Page.Session["ReportMonth"].ToString();
                    rYear = Page.Session["ReportYear"].ToString();

                    PopulateTreeView(-1, null);
                }
            }
        }

        public void Copy(TreeView treeview1, TreeView treeview2)
        {
            TreeNode newTn;
            foreach (TreeNode tn in treeview1.Nodes)
            {
                newTn = new TreeNode(tn.Text, tn.Value);
                CopyChilds(newTn, tn);
                treeview2.Nodes.Add(newTn);
            }
        }

        public void CopyChilds(TreeNode parent, TreeNode willCopied)
        {
            TreeNode newTn;
            foreach (TreeNode tn in willCopied.ChildNodes)
            {
                newTn = new TreeNode(tn.Text, tn.Value);
                parent.ChildNodes.Add(newTn);
            }
        }

        private void PopulateTreeView(int parentId, TreeNode treeNode)
        {
            foreach (var pair in treeNodeRelationship.Where(x => x.Value == parentId))
            {
                var key = pair.Key;
                var value = pair.Value;

                DAO_PLEXUS_DATARECORD dr = plexRecords.Find(x => x.AmbNum == key);

                TreeNode child = new TreeNode
                {
                    Text = getColorForTreeNode(dr) + dr.Name + " (" + dr.Level + ") - $" + String.Format("{0:0.00}", dr.Pv.ToString("0.00") + " - " + dr.JoinDate.ToShortDateString() + " - " + dr.TotalPoints + "</span>"),
                    Value = dr.AmbNum.ToString()
                };
                
                if (parentId == -1)
                {
                    
                    TreeView_plexTree.Nodes.Add(child);
                    PopulateTreeView(int.Parse(child.Value), child);
                }
                else
                {
                    treeNode.ChildNodes.Add(child);
                    PopulateTreeView(int.Parse(child.Value), child);
                }
            }
        }

        private bool IsTheSameMonth(DateTime date1)
        {
            return (date1.Year == Int32.Parse(rYear) && date1.Month == Int32.Parse(rMonth));
        }

        private string getColorForTreeNode(DAO_PLEXUS_DATARECORD dr)
        {
            //#36dbca = Active
            //#93db70 = New
            if (IsTheSameMonth(dr.JoinDate))
                {
                    return "<span style='background-color: #93db70;'>";
                }
            else if (dr.Active == "Y")
                {
                    return "<span style='background-color: #36dbca;'>";
                }
            return "<span>";
        }
    }
}