using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Threading;
using System.IO;

namespace GoFund
{
    public partial class Form1 : Form
    {

        HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();

        

        List<Fund> myList = new List<Fund>();
        string sPage = "http://fund.eastmoney.com/";
        string line = null;
        string[] subline = null;
        string sContent = null;

        public Form1()
        {
            InitializeComponent();
            timer1.Enabled = true;

            try
            {
                //读取基金资料
                FileStream fs = new FileStream(@"D:\CodeRepo\GoFund\GoFund\bin\Release\AllFunds.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs);

                while ((line = sr.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line == "") continue;
                    subline = line.Split(',');
                    myList.Add(new Fund() { Id = subline[0], Name = subline[1], Has = (subline[2] == "y") ? " 持有" : " 未持有" });
                }

                sr.Close();
                fs.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                web.CacheOnly = false;
                web.CachePath = null;
                web.UsingCache = false;
                

                //获取估值
                foreach (Fund objFund in myList)
                {

                    HtmlAgilityPack.HtmlDocument doc = web.Load(sPage + objFund.Id + ".html"); // HtmlDocument 类，负责操作html文档
                    HtmlNode ranking = doc.DocumentNode.SelectSingleNode("//div[@class='Rdata']"); // 近一周同类排名
                    
                    
                    var rate = doc.GetElementbyId("gz_gszzl").InnerText;
                    if (rate.Contains("+") == true)
                    {
                        rate = "<font color=\"red\">" + rate + "</font>";
                    }                                          

                    var date = doc.GetElementbyId("gz_gztime").InnerHtml;
                    if (rate == "--")  rate = "0.00%";
                    if (date == "--") date = "暂无";
                    sContent = sContent + objFund.Id + " " + "<a href=\"" + sPage + objFund.Id + ".html\" target=\"_blank\">" + objFund.Name + "</a>" + "\t" + "估算涨幅：" + rate + "\t" + " 更新时间：" + date + "\t"  + "近一周排名："+ ranking.InnerText + objFund.Has + "<br>";

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                throw;
            }
            finally
            {
                webBrowser1.DocumentText = sContent;
                sContent = null;
            }                        
            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            button1_Click(null, null);
        }
    }
}
