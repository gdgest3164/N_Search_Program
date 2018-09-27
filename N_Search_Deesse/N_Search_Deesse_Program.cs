using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Threading;
using Excel = Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;

namespace N_Search_Deesse
{
    public partial class N_Search_Deesse_Program : Form
    {
        int count_timer = 0;
        int url_n=0;
        int keyword_num = 0;
        //string blog_result="";
        string now_keyword = "";
        string save_type = "";
        ArrayList urls = new ArrayList();
        //대상 키워드 가져오기
        ArrayList array_AList = new ArrayList();
        ArrayList array_No_AList = new ArrayList();

        //Thread worker;
        bool isPause = false;
        //===============================================================================================
        // 디자인 꾸미기
        //===============================================================================================
        public Color color_one = Color.FromArgb(27, 28, 32);
        public Color color_two = Color.FromArgb(35, 40, 50);

        public N_Search_Deesse_Program()
        {
            InitializeComponent();
            timer_txt.Text = "";
            Minute_unit.Items.Add("5분 후");
            Minute_unit.Items.Add("10분 후");
            Minute_unit.Items.Add("20분 후");
            Minute_unit.Items.Add("30분 후");
            Minute_unit.Items.Add("40분 후");
            Minute_unit.Items.Add("50분 후");
            Minute_unit.SelectedItem = "5분 후";
            //Loading_txt2.Visible = false;

            
        }

        private void minibtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            Application.Exit();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            //Environment.Exit(0);
        }

        //폼 움직임
        public struct POINT
        {
            public int X, Y;
        }
        [DllImport("user32.dll")] // 현재 마우스 위치를 얻기위한 API함수.
        public extern static void GetCursorPos(out POINT point);

        Point FormLocation; // 현재 폼 위치
        POINT LastLocation = new POINT(); // 방금 전의 마우스 위치
        POINT CurrentLocation = new POINT(); // 현재 마우스 위치
                                             // 폼이 움직일 양 = CurrentLocation - LastLocation.
        bool IsMouseMoveStart = false; // 현재 마우스 움직이기 기능이 켜져있는가.
                                       // 만약 이게 없으면 그냥 폼위에서 마우스가 움직이면 폼이 움직이게 될거임.
        private void N_Search_Deesse_top_MouseDown(object sender, MouseEventArgs e)
        {
            GetCursorPos(out CurrentLocation);
            FormLocation = this.Location;
            IsMouseMoveStart = true;
        }

        private void N_Search_Deesse_top_MouseMove(object sender, MouseEventArgs e)
        {
            // 마우스의 움직임에 맞춰서 폼을 움직인다.

            if (!IsMouseMoveStart) return;

            GetCursorPos(out LastLocation);
            FormLocation.X -= (CurrentLocation.X - LastLocation.X);
            FormLocation.Y -= (CurrentLocation.Y - LastLocation.Y);
            this.Location = FormLocation;
            CurrentLocation = LastLocation;
        }

        private void N_Search_Deesse_top_MouseUp(object sender, MouseEventArgs e)
        {
            IsMouseMoveStart = false;
        }

        private void minibtn_MouseHover(object sender, EventArgs e)
        {
            minibtn.BackColor = color_two;
        }

        private void minibtn_MouseLeave(object sender, EventArgs e)
        {
            minibtn.BackColor = color_one;
        }

        private void closebtn_MouseHover(object sender, EventArgs e)
        {
            closebtn.BackColor = color_two;
        }

        private void closebtn_MouseLeave(object sender, EventArgs e)
        {
            closebtn.BackColor = color_one;
        }

        private void N_Search_Deesse_Program_Load(object sender, EventArgs e)
        {
            emali_list1.Items.Clear();
            emali_list1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            
            //ListViewItem lvi = new ListViewItem("test");
            //lvi.SubItems.Add("test2");
            //lvi.SubItems.Add("test3");
            //emali_list.Items.Add(lvi);

            this.keyword_input.AutoSize = false;
            this.not_keyword_input.AutoSize = false;
            this.keyword_input.Size = new Size(227, 25);
            this.not_keyword_input.Size = new Size(227, 25);
        }

        private void keyword_add_btn_Click(object sender, EventArgs e)
        {
            if (keyword_input.Text == "")
            {
                MessageBoxs.Show("키워드를 입력하세요.", "msg", "확인");
            }
            else
            {
                this.keyword_list.Items.Add(keyword_input.Text);
                keyword_input.Text = "";
                keyword_input.Focus();
            }
        }

        private void not_keyword_add_btn_Click(object sender, EventArgs e)
        {
            if (not_keyword_input.Text == "")
            {
                MessageBoxs.Show("키워드를 입력하세요.", "msg", "확인");
            }
            else
            {
                this.not_keyword_list.Items.Add(not_keyword_input.Text);
                not_keyword_input.Text = "";
                not_keyword_input.Focus();
            }
        }

        private void keyword_input_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                keyword_add_btn.PerformClick();
            }
        }

        private void not_keyword_input_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                not_keyword_add_btn.PerformClick();
            }
        }

        //===============================================================================================
        // 이제부터 추출기능 시작!
        //===============================================================================================

        /*private WebRequest request;
        private HttpWebResponse response;
        private HtmlAgilityPack.HtmlDocument doc;
        private Stream dataStream;*/

            //대상 키워드, 제외 키워드 수집 후, 실제 추출 함수로 이동. 
        private void button1_Click(object sender, EventArgs e)
        {
            emali_list1.Items.Clear();

            isPause = false;
            keyword_num = 0;
            url_n = 0;

            if (loop_check.Checked)
            {
                count_timer = Convert.ToInt32(Regex.Replace(Minute_unit.SelectedItem.ToString(), @"\D", "")) * 60;
            }
            else
            {
                count_timer = 0;
            }

            if (keyword_list.Items.Count <= 0)
            {
                MessageBoxs.Show("키워드를 추가 해주세요.", "msg", "확인");
                return;
            }

            array_AList.Clear();
            //대상키워드
            for (int k = 0; k < keyword_list.Items.Count; k++)
            {
                array_AList.Add(keyword_list.Items[k].ToString());
            }

            array_No_AList.Clear();
            //제외 키워드 가져오기            
            for (int k = 0; k < not_keyword_list.Items.Count; k++)
            {
                array_No_AList.Add(not_keyword_list.Items[k].ToString());
            }

            stop_btn.Enabled = true;
            crol_btn.Enabled = false;

            //m_thread =new Thread(new ThreadStart(search_blog));
            //m_thread.Name = "keyword";
            //m_thread.IsBackground = true;
            //m_thread.Start();

            search_blog();
            //this.worker = new Thread(new ThreadStart(this.search_blog));
            //this.worker.Start();
        }

        //실제 추출 함수
        private void search_blog()
        {
            loading_gif.Visible = true;
            Loading_txt2.Visible = true;
            
            ArrayList doc_text = new ArrayList();
            ArrayList title_doc_text = new ArrayList();

            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.Minimum = 0;
            progressBar.Step = 1;
            progressBar.Value = 0;
            Application.DoEvents();
            //if (array_AList.Count < keyword_num) return;
            //MessageBox.Show(array_AList.Count.ToString()+" "+keyword_num.ToString());
            //네이버 검색 api
            string query = array_AList[keyword_num].ToString(); // 검색할 키워드
            now_keyword = array_AList[keyword_num].ToString();
            //sort=sim, date
            Application.DoEvents();
            string sort = "sim";
            if (new_order.Checked)
            {
                sort = "date";
            }
            else if (accuracy_order.Checked)
            {
                sort = "sim";
            }
            while (isPause)
            {
                Application.DoEvents();
                Thread.Sleep(1);
            }

            string url = "https://openapi.naver.com/v1/search/blog.json?query=" + query + "&display="+page_num.Text+"&sort="+ sort; // 결과가 JSON 포맷
            Application.DoEvents();                                               // string url = "https://openapi.naver.com/v1/search/blog.xml?query=" + query;  // 결과가 XML 포맷
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-Naver-Client-Id", "sJ0znk4nPpAoS7g7Ojg7"); // 클라이언트 아이디
            request.Headers.Add("X-Naver-Client-Secret", "YC1NEpPYnO");       // 클라이언트 시크릿
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string status = response.StatusCode.ToString();
            if (status == "OK")
            {
                Application.DoEvents();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                string text = reader.ReadToEnd();
                //Log.Show(text);

                JObject rss = JObject.Parse(text);
                var totalCount = Convert.ToInt32(rss["total"]);
                doc_text.Clear();
                title_doc_text.Clear();
                urls.Clear();
                if (totalCount > 0)
                {
                    int line = 0;

                    foreach (var item in rss["items"])
                    {
                        Application.DoEvents();
                        doc_text.Add(rss["items"][line]["link"].ToString().Replace("amp;", ""));
                        title_doc_text.Add(rss["items"][line]["title"].ToString());
                        line++;
                    }
                    //Log.Show(doc_text.Count+""+doc_text[0]);
                    Application.DoEvents();

                    //String test = "";
                    for (int i = 0; i < doc_text.Count; i++)
                    {
                        Application.DoEvents();
                        //제외 키워드 
                        int isNotKeyword = 0;
                        if (not_keyword_list.Items.Count > 0) {
                            for(int nk=0; nk< not_keyword_list.Items.Count; nk++)
                            {
                                Application.DoEvents();
                                if (title_doc_text[i].ToString().Contains(not_keyword_list.Items[nk].ToString()))
                                {
                                    isNotKeyword = 1;
                                    break;
                                }
                            }
                        }
                        if(isNotKeyword==0) urls.Add(doc_text[i]);
                        //test += doc_text[i]+"\r\n";
                    }
                    progressBar.Maximum = urls.Count;
                    //MessageBoxs.Show(urls.Count.ToString(),"","확인");
                    //Log.Show(urls.Count.ToString());
                }
                else
                {
                    MessageBoxs.Show("검색결과가 없습니다.", "MSG", "확인");
                }
            }
            else
            {
                MessageBoxs.Show("Error 발생=" + status, "MGS", "확인");
            }
            while (isPause)
            {
                Application.DoEvents();
                Thread.Sleep(1);
            }
            //블로그 추출 시작
            web_loop(url_n);
        }

        private void webbrowser_visit_list(string url)
        {
            webBrowser.ScriptErrorsSuppressed = true;
            webBrowser.Navigate(url);
            
            //MessageBoxs.Show("ok", "", "ok");

        }

        private void web_loop(int url_ns)
        {
            loading_gif.Visible = true;
            Loading_txt2.Visible = true;
            while (isPause)
            {
                Application.DoEvents();
                Thread.Sleep(1);
            }
            //MessageBox.Show(urls[url_ns].ToString());
            HtmlWeb website = new HtmlWeb();
            
            if (urls[url_ns].ToString().Contains("tistory") || urls[url_ns].ToString().Contains("egloos") || urls[url_ns].ToString().Contains("daum"))
            {
                Application.DoEvents();
                url_n += 1;
                progressBar.PerformStep();
                if (array_AList.Count > url_n)
                {
                    web_loop(url_n);
                }
                else
                {
                    Application.DoEvents();
                    if (loop_check.Checked)
                    {
                        //엑셀 저장
                        excel_btn.PerformClick();
                        if (loop_check.Checked)
                        {
                            count_timer = Convert.ToInt32(Regex.Replace(Minute_unit.SelectedItem.ToString(), @"\D", "")) * 60;
                        }
                        else
                        {
                            count_timer = 0;
                        }
                        //타이머
                        timer.Enabled = true;
                        timer.Start();
                        Delay(Convert.ToInt32(Regex.Replace(Minute_unit.SelectedItem.ToString(), @"\D", "")) * 60 * 1000);

                        timer.Stop();
                        timer_txt.Text = "";
                        stop_btn.Enabled = false;
                        restart_btn.Enabled = false;
                        crol_btn.Enabled = true;
                        crol_btn.PerformClick();
                    }
                    else
                    {
                        //MessageBox.Show("이메일 추출 끝");
                        progressBar.Value = 0;
                        stop_btn.Enabled = false;
                        restart_btn.Enabled = false;
                        crol_btn.Enabled = true;
                        loading_gif.Visible = false;
                        Loading_txt2.Visible = false;
                        return;
                    }
                }
            }

            //progressBar.PerformStep();
            while (isPause)
            {
                Application.DoEvents();
                Thread.Sleep(1);
            }
            HtmlAgilityPack.HtmlDocument rootDocument = website.Load(urls[url_ns].ToString());
            HtmlNode iframe_id = rootDocument.GetElementbyId("mainFrame");
            //Log.Show(rootDocument.GetElementbyId("mainFrame").GetAttributeValue("src",""));
            string url_test = "";
            if (iframe_id != null)
            {
                Application.DoEvents();
                if (iframe_id.GetAttributeValue("src", "").Contains("blog.naver.com"))
                {
                    webbrowser_visit_list(iframe_id.GetAttributeValue("src", ""));
                    url_test = iframe_id.GetAttributeValue("src", "");
                }
                else
                {
                    webbrowser_visit_list("http://blog.naver.com" + iframe_id.GetAttributeValue("src", ""));
                    url_test = "http://blog.naver.com" + iframe_id.GetAttributeValue("src", "");
                }
                //cm - head  cm_cur _viewMore                
            }
            else
            {
                iframe_id = rootDocument.GetElementbyId("screenFrame");
                if (iframe_id != null)
                {
                    Application.DoEvents();
                    if (iframe_id.GetAttributeValue("src", "").Contains("blog.naver.com"))
                    {
                        webbrowser_visit_list(iframe_id.GetAttributeValue("src", ""));
                        url_test = iframe_id.GetAttributeValue("src", "");
                    }
                    else
                    {
                        webbrowser_visit_list("http://blog.naver.com" + iframe_id.GetAttributeValue("src", ""));
                        url_test = "http://blog.naver.com" + iframe_id.GetAttributeValue("src", "");
                    }
                    rootDocument = website.Load(url_test);
                    iframe_id = rootDocument.GetElementbyId("mainFrame");
                    if (iframe_id != null)
                    {
                        Application.DoEvents();
                        if (iframe_id.GetAttributeValue("src", "").Contains("blog.naver.com"))
                        {
                            webbrowser_visit_list(iframe_id.GetAttributeValue("src", ""));
                            url_test = iframe_id.GetAttributeValue("src", "");
                        }
                        else
                        {
                            webbrowser_visit_list("http://blog.naver.com" + iframe_id.GetAttributeValue("src", ""));
                            url_test = "http://blog.naver.com" + iframe_id.GetAttributeValue("src", "");
                        }
                        //cm - head  cm_cur _viewMore                
                    }
                }
                else
                {
                    url_n += 1;
                    progressBar.PerformStep();
                    web_loop(url_n);
                }
            }
            //MessageBox.Show(url_test);
        }

        private void keyword_del_btn_Click(object sender, EventArgs e)
        {
            if (keyword_list.SelectedItem != null)
                keyword_list.Items.Remove(keyword_list.SelectedItem);
            else
                MessageBoxs.Show("삭제할 키워드를 선택 해주세요.", "", "확인");
        }

        private void not_keyword_del_btn_Click(object sender, EventArgs e)
        {
            if (not_keyword_list.SelectedItem != null)
                not_keyword_list.Items.Remove(not_keyword_list.SelectedItem);
            else
                MessageBoxs.Show("삭제할 키워드를 선택 해주세요.", "", "확인");
        }

        private void N_Search_Deesse_Program_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            Application.Exit();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }


        System.Windows.Forms.Timer m_pageHasntChangedTimer = null;
        int re_load = 0;
        private void webBrowser_DocumentCompleted_1(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (m_pageHasntChangedTimer != null)
            {
                m_pageHasntChangedTimer.Dispose();
            }
            while (isPause)
            {
                Application.DoEvents();
                Thread.Sleep(1);
            }
            m_pageHasntChangedTimer = new System.Windows.Forms.Timer();
            EventHandler checker = delegate (object o1, EventArgs e1) {
                if (WebBrowserReadyState.Complete == webBrowser.ReadyState)
                {
                    Application.DoEvents();
                    if (re_load == 1)
                    {
                        m_pageHasntChangedTimer.Dispose();
                        loading_gif.Visible = false;
                        Loading_txt2.Visible = false;
                        //MessageBox.Show("ok");
                        OnWebpageReallyLoaded();
                        re_load = 0;
                    }
                    else
                    {
                        re_load = 1;
                    }
                }
            };
            m_pageHasntChangedTimer.Tick += checker;
            m_pageHasntChangedTimer.Interval = 200;
            m_pageHasntChangedTimer.Start();
        }
        private void OnWebpageReallyLoaded()
        {            
            if (webBrowser.Document != null)
            {
                Application.DoEvents();
                //blog_result += webBrowser.Document.Url.ToString() + " " + webBrowser.Document.GetElementById("visitor-list").InnerText + "\r\n";
                if (webBrowser.Document.GetElementById("visitor-list") != null)
                {
                    //var visit_link = webBrowser.Document.GetElementById("visitor-list").GetElementsByTagName("li");
                    //MessageBox.Show(webBrowser.Document.GetElementById("visitor-list").InnerHtml);
                    
                    HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();

                    webBrowser.Document.GetElementById("visitor-list-i").InvokeMember("Click");     //다녀간 블로거 클릭 이벤트
                    Application.DoEvents();
                    if (webBrowser.Document.GetElementById("visitor-list").InnerHtml != null)
                    {
                        Application.DoEvents();
                        htmlDoc.LoadHtml(webBrowser.Document.GetElementById("visitor-list").InnerHtml);
                        //string id_url = "";
                        foreach (HtmlNode link in htmlDoc.DocumentNode.SelectNodes("//div/ul/li/a"))
                        {
                            Application.DoEvents();
                            //id_url += link.GetAttributeValue("href", "") + "\r\n";
                            List_Write(link.GetAttributeValue("href", ""));
                        }
                    }
                    //Log.Show(id_url);
                }
                //else
                    //MessageBox.Show("방문자가 없음");
            }
            url_n += 1;
            progressBar.PerformStep();
            if (urls.Count > url_n)
            {
                web_loop(url_n);
            }
            else
            {
                keyword_num++;
                if (array_AList.Count > keyword_num)
                {
                    //crol_btn.PerformClick();
                    search_blog();
                }
                else
                {
                    if (loop_check.Checked)
                    {
                        //엑셀 저장
                        excel_btn.PerformClick();

                        //타이머
                        //MessageBox.Show(Regex.Replace(Minute_unit.SelectedItem.ToString(), @"\D", ""));
                        //MessageBox.Show(Convert.ToString(Convert.ToInt32(Regex.Replace(Minute_unit.SelectedItem.ToString(), @"\D", "")) * 60));
                        timer.Enabled = true;
                        timer.Start();
                        //Thread.Sleep(Convert.ToInt32(Regex.Replace(Minute_unit.SelectedItem.ToString(), @"\D", "")) * 60 * 60);
                        Delay(Convert.ToInt32(Regex.Replace(Minute_unit.SelectedItem.ToString(), @"\D", "")) * 60 * 1000);
                        
                        timer.Stop();
                        timer_txt.Text = "";
                        stop_btn.Enabled = false;
                        restart_btn.Enabled = false;
                        crol_btn.Enabled = true;
                        crol_btn.PerformClick();
                        //return;
                    }
                    else
                    {
                        progressBar.Value = 0;
                        //MessageBox.Show("이메일 추출 끝");
                        stop_btn.Enabled = false;
                        restart_btn.Enabled = false;
                        crol_btn.Enabled = true;
                        return;
                    }
                }
            }
        }

        private static DateTime Delay(int MS)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                Application.DoEvents();
                ThisMoment = DateTime.Now;
            }

            return DateTime.Now;
        }

        private void List_Write(string str)
        {
            ListViewItem lvi = new ListViewItem(str.Replace("http://","").Replace("https://", "").Replace("blog.naver.com/", "").Replace(".blog.me/", "") + "@naver.com");
            lvi.SubItems.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            lvi.SubItems.Add(now_keyword);
            emali_list1.Items.Add(lvi);
            emali_list1.EnsureVisible(emali_list1.Items.Count - 1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            isPause = true;
            url_n = 0;
            keyword_num = 0;
            now_keyword = "";

            stop_btn.Enabled = false;
            restart_btn.Enabled = false;
            crol_btn.Enabled = true;
            loading_gif.Visible = false;
            Loading_txt2.Visible = false;

            emali_list1.Items.Clear();
        }

        //중지버튼
        private void button2_Click(object sender, EventArgs e)
        {
            stop_btn.Enabled = false;
            restart_btn.Enabled = true;
            loading_gif.Visible = false;
            Loading_txt2.Visible = false;
            //this.worker.Suspend();
            isPause = true;
        }

        private void restart_Click(object sender, EventArgs e)
        {
            stop_btn.Enabled = true;
            restart_btn.Enabled = false;
            //this.worker.Resume();
            isPause = false;
        }

        File_Save frm;
        private void button3_Click(object sender, EventArgs e)
        {
            save_type = "excel";
            if (emali_list1.Items.Count > 0)
            {
                //File_Save.Loading("엑셀",emali_list);
                using(frm = new File_Save(SaveData))
                {
                    frm.label2.Text = "엑셀로 다운중...";
                    //frm.progressBar1.Style = ProgressBarStyle.Marquee;
                    frm.progressBar1.Style = ProgressBarStyle.Continuous;
                    frm.progressBar1.Minimum = 0;
                    frm.progressBar1.Maximum = emali_list1.Items.Count;
                    frm.progressBar1.Step = 1;
                    frm.progressBar1.Value = 0;
                    frm.ShowDialog(this);
                }
            }
            else
            {
                MessageBoxs.Show("추출결과가 없습니다.", "", "확인");
            }
        }

        void SaveData()
        {
            if (save_type == "excel")
            {
                //엑셀 쓰기
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook wb = excelApp.Workbooks.Add(true);
                Excel._Worksheet workSheet = wb.Worksheets.get_Item(1) as Excel._Worksheet;

                workSheet.Cells[1, 1] = "이메일";
                workSheet.Cells[1, 1].ColumnWidth = 30;
                workSheet.Cells[1, 2] = "추출일시";
                workSheet.Cells[1, 2].ColumnWidth = 20;
                workSheet.Cells[1, 3] = "키워드";
                workSheet.Cells[1, 3].ColumnWidth = 15;

                //MessageBox.Show(emali_list.Items.Count.ToString());
                for (int et = 0; et < emali_list1.Items.Count; et++)
                {
                    for (int et2 = 0; et2 < 3; et2++)
                    {
                        //Application.DoEvents();
                        workSheet.Cells[(et + 2), (et2 + 1)] = emali_list1.Items[et].SubItems[et2].ToString().Replace("ListViewSubItem: {", "").Replace("}", "");
                    }
                    frm.progressBar1.PerformStep();
                    
                    Application.DoEvents();
                    Thread.Sleep(1);
                }
                ExcelDispose(excelApp, wb, workSheet);
            }
            else
            {

                string savePath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "/" + DateTime.Now.ToString("MMddhhmmss") + " 이메일 추출 리스트.txt";
                string textvalue = "";
                for (int et = 0; et < emali_list1.Items.Count; et++)
                {
                    for (int et2 = 0; et2 < 3; et2++)
                    {
                        Application.DoEvents();
                        textvalue += emali_list1.Items[et].SubItems[et2].ToString().Replace("ListViewSubItem: {", "").Replace("}", "");
                        textvalue += "\t\t\t";
                    }
                    textvalue += "\r\n";
                    frm.progressBar1.PerformStep();
                    Application.DoEvents();
                    Thread.Sleep(1);
                }
                File.WriteAllText(savePath, textvalue, Encoding.Default);
                //MessageBoxs.Show("텍스트로 다운 완료", "success", "확인");
            }
        
        }

        public static void ExcelDispose(Excel.Application excelApp, Excel.Workbook wb, Excel._Worksheet workSheet)
        {
            wb.SaveAs(@Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "/" + DateTime.Now.ToString("MMddhhmmss") + " 이메일 추출 리스트.xls", Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
            Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            wb.Close(Type.Missing, Type.Missing, Type.Missing);

            excelApp.Quit();
            releaseObject(excelApp);
            releaseObject(workSheet);
            releaseObject(wb);
            //MessageBoxs.Show("엑셀파일 저장 완료", "success", "확인");
        }

        #region 메모리해제
        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception e)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }
        #endregion*/

        private void button4_Click(object sender, EventArgs e)
        {
            save_type = "text";
            if (emali_list1.Items.Count > 0)
            {
                //File_Save.Loading("엑셀",emali_list);
                using (frm = new File_Save(SaveData))
                {
                    frm.label2.Text = "텍스트로 다운중...";
                    frm.progressBar1.Style = ProgressBarStyle.Continuous;
                    frm.progressBar1.Minimum = 0;
                    frm.progressBar1.Maximum = emali_list1.Items.Count;
                    frm.progressBar1.Step = 1;
                    frm.progressBar1.Value = 0;
                    frm.ShowDialog(this);
                }
            }
            else
            {
                MessageBoxs.Show("추출결과가 없습니다.", "", "확인");
            }
        }

        private void page_num_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void page_num_KeyUp(object sender, KeyEventArgs e)
        {
            if (page_num.Text != "")
            {
                if (Convert.ToInt32(page_num.Text) > 100)
                {
                    page_num.Text = "100";
                }
            }
        }

        private void loop_check_CheckedChanged(object sender, EventArgs e)
        {
            if (loop_check.Checked)
            {
                Minute_unit.Enabled = true;
            }
            else
            {
                Minute_unit.Enabled = false;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            string hours = Convert.ToInt32(count_timer / 3600).ToString();
            string monute = Convert.ToInt32(((count_timer % 3600) / 60)).ToString();
            string second = Convert.ToInt32((count_timer % 3600) % 60).ToString();

            while (isPause)
            {
                Application.DoEvents();
                Thread.Sleep(1);
            }

            if (Convert.ToInt32(hours) < 10) hours = "0" + hours ;
            if (Convert.ToInt32(monute) < 10) monute = "0" + monute;
            if (Convert.ToInt32(second) < 10) second = "0" + second;

            //timer_txt.Text = DateTime.Now.ToLongTimeString();
            timer_text.Text = hours + "시 " + monute + "분 " + second + "초 남음";
            count_timer--;
        }

        private void webBrowser_FileDownload(object sender, EventArgs e)
        {
            Application.DoEvents();
            
        }

        private void webBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            Application.DoEvents();
        }

        private void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            Application.DoEvents();
        }

        private void webBrowser_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            Application.DoEvents();
        }

        private void webBrowser_NewWindow(object sender, CancelEventArgs e)
        {
            Application.DoEvents();
        }
    }
}
