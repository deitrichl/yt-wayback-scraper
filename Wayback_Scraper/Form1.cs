using System;
using System.Drawing.Text;
using System.Runtime.Intrinsics.Arm;
using System.Web;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;
using System.Runtime.InteropServices;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Management.Automation;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Wayback_Scraper
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Sets conversion and resolution to default
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;

            //Makes window movable
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);

            //Confirms that video select list is empty & download list files are deleted on start up
            listBox1.Items.Clear();
            if (File.Exists(@".\files\selectF.txt"))
            {
                File.Delete(@".\files\selectF.txt");
            };
            if (File.Exists(@".\files\select2.txt"))
            {
                File.Delete(@".\files\select2.txt");
            };

            //Create base directories
            if (!Directory.Exists(@".\files\"))
            {
                Directory.CreateDirectory(@".\files\");
            }

            if (!Directory.Exists(@".\videos\"))
            {
                Directory.CreateDirectory(@".\videos\");
            }

            if (!Directory.Exists(@".\tmp\"))
            {
                Directory.CreateDirectory(@".\tmp\");
            }

            //Checks if save directory file has been made. If not, use default .\videos
            if (File.Exists(@".\files\save_directory.txt"))
            {
                textBox3.Text = File.ReadAllText(@".\files\save_directory.txt");
            }
            else
            {
                string defaultDirectory = @".\videos";
                textBox3.Text = defaultDirectory.ToString();

            }



        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //Allows the Enter press when a url is entered
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, new EventArgs());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Resets variables for loops and deletes dummy files used for cmd lines
            int n = 0;
            int o = 0;
            int p = 0;

            if (File.Exists(@".\files\dummy1.txt"))
            {
                File.Delete(@".\files\dummy1.txt");
            };

            if (File.Exists(@".\files\dummy2.txt"))
            {
                File.Delete(@".\files\dummy2.txt");
            };



            // Write inputted url to file
            using (StreamWriter writer = new(@".\files\new_url1.txt"))
            {
                writer.WriteLine(textBox1.Text);
            }

            if (checkBox1.Checked == true) //If the checkbox is checked, skip selection
            {
                // Check if video or playlist
                if (textBox1.Text.Contains("playlist"))
                {
                    listBox1.Items.Clear();
                    /* Receive entered url from new_url1.txt.
                     * Receive available video IDs from playlist, save to url_list1.txt.
                     * Then recieve both available and unavailable IDs from playlist, save to url_list2.txt
                     */
                    string strCmdText;
                    strCmdText = @"/C ""yt-dlp.exe --get-id -a "".\files\new_url1.txt"" -i > "".\files\url_list1.txt"" & yt-dlp.exe --get-id --flat-playlist -a "".\files\new_url1.txt"" -i > "".\files\url_list2.txt"" & yt-dlp.exe --print id https://www.youtube.com/watch?v=jNQXAC9IVRw > "".\files\dummy1.txt""";
                    System.Diagnostics.Process.Start("CMD.exe", strCmdText);
                    Console.Read();

                    while (n < 5)
                    {
                        // Get only deleted IDs
                        if (File.Exists(@".\files\dummy1.txt"))
                        {
                            var file1Lines = File.ReadAllLines(@".\files\url_list1.txt");
                            var file2Lines = File.ReadAllLines(@".\files\url_list2.txt");
                            IEnumerable<String> deleted_ids = file2Lines.Except(file1Lines);

                            // Append IDs to file
                            File.WriteAllLines(@".\files\del_url1.txt", deleted_ids);
                            n = 6;
                            break;
                        }
                        Thread.Sleep(1000);

                    }

                    while (o < 5)
                    {
                        if (n == 6)
                        {

                            //Convert IDs to Wayback Machine urls
                            List<string> lines = new List<string>();
                            foreach (var line in File.ReadAllLines(@".\files\del_url1.txt"))
                            {
                                lines.Add("https://web.archive.org/web/20120523122213/http://wayback-fakeurl.archive.org/yt/" + line);
                            }

                            File.WriteAllLines(@".\files\del_url2.txt", lines.ToArray());

                            select1_func();

                            //Check if deleted videos are on the Wayback Machine and if so download them
                            string download_str = "del_url2.txt";
                            download_func(ref download_str);

                            o = 6;
                            break;
                        }
                        Thread.Sleep(1000);

                    }

                }
                else if (textBox1.Text.Contains(",")) //For listing multiple urls in the textbox
                {
                    listBox1.Items.Clear();
                    string linebroken1 = textBox1.Text.Replace(",", System.Environment.NewLine);
                    string linebroken2 = linebroken1.Replace(" ", "");
                    File.WriteAllText(@".\files\del_url0.txt", linebroken2);

                    List<string> lines0 = new List<string>();
                    foreach (var line in File.ReadAllLines(@".\files\del_url0.txt"))
                    {
                        if (line.Length >= 11)
                        {
                            string validIds = line.Substring(line.Length - 11);
                            lines0.Add(validIds);
                        }
                    }
                    File.WriteAllLines(@".\files\del_url1.txt", lines0.ToArray());

                    //Convert IDs to Wayback Machine urls
                    List<string> lines = new List<string>();
                    foreach (var line in File.ReadAllLines(@".\files\del_url1.txt"))
                    {
                        lines.Add("https://web.archive.org/web/20120523122213/http://wayback-fakeurl.archive.org/yt/" + line);
                    }

                    File.WriteAllLines(@".\files\del_url2.txt", lines.ToArray());

                    select1_func();

                    //Check if deleted videos are on the Wayback Machine and if so download them
                    string download_str = "del_url2.txt";
                    download_func(ref download_str);

                }
                else
                {
                    //If the link is not a playlist (ie. video)

                    //Extract the ID from url
                    var url = textBox1.Text;

                    //Confirms that url is valid
                    if (!string.IsNullOrEmpty(url) && Uri.IsWellFormedUriString(url, UriKind.Absolute))
                    {
                        listBox1.Items.Clear();

                        var uri = new Uri(url);
                        var query = HttpUtility.ParseQueryString(uri.Query);
                        var videoId = string.Empty;

                        if (query.AllKeys.Contains("v"))
                        {
                            videoId = query["v"];
                            File.WriteAllText(@".\files\del_url1.txt", videoId);

                        }
                        else
                        {
                            videoId = uri.Segments.Last();
                            File.WriteAllText(@".\files\del_url1.txt", videoId);
                        }


                        //Convert ID to Wayback Machine urls
                        List<string> lines = new List<string>();
                        foreach (var line in File.ReadAllLines(@".\files\del_url1.txt"))
                        {
                            lines.Add("https://web.archive.org/web/20120523122213/http://wayback-fakeurl.archive.org/yt/" + line);
                        }

                        File.WriteAllLines(@".\files\del_url2.txt", lines.ToArray());

                        select1_func();

                        //Check if deleted video is on the Wayback Machine and if so download it
                        string download_str = "del_url2.txt";
                        download_func(ref download_str);



                    }
                    else if (url.Length == 11)
                    {
                        File.WriteAllText(@".\files\del_url1.txt", url);


                        //Convert ID to Wayback Machine urls
                        List<string> lines = new List<string>();
                        foreach (var line in File.ReadAllLines(@".\files\del_url1.txt"))
                        {
                            lines.Add("https://web.archive.org/web/20120523122213/http://wayback-fakeurl.archive.org/yt/" + line);
                        }

                        File.WriteAllLines(@".\files\del_url2.txt", lines.ToArray());

                        select1_func();

                        //Check if deleted video is on the Wayback Machine and if so download it
                        string download_str = "del_url2.txt";
                        download_func(ref download_str);

                    }
                    else //if url is invalid
                    {
                        string message = "Please enter a valid Youtube URL and try again.";
                        string title = "Invalid URL";
                        MessageBox.Show(message, title);
                    }
                }

            }
            else //Don't skip selection
            {

                // Check if video or playlist
                if (textBox1.Text.Contains("playlist"))
                {
                    listBox1.Items.Clear();
                    /* Receive entered url from new_url1.txt.
                     * Receive available video IDs from playlist, save to url_list1.txt.
                     * Then recieve both available and unavailable IDs from playlist, save to url_list2.txt
                     */
                    string strCmdText;
                    strCmdText = @"/C ""yt-dlp.exe --get-id -a "".\files\new_url1.txt"" -i > "".\files\url_list1.txt"" & yt-dlp.exe --get-id --flat-playlist -a "".\files\new_url1.txt"" -i > "".\files\url_list2.txt"" & yt-dlp.exe --print id https://www.youtube.com/watch?v=jNQXAC9IVRw > "".\files\dummy1.txt""";
                    System.Diagnostics.Process.Start("CMD.exe", strCmdText);
                    Console.Read();

                    while (n < 5)
                    {
                        // Get only deleted IDs
                        if (File.Exists(@".\files\dummy1.txt"))
                        {
                            var file1Lines = File.ReadAllLines(@".\files\url_list1.txt");
                            var file2Lines = File.ReadAllLines(@".\files\url_list2.txt");
                            IEnumerable<String> deleted_ids = file2Lines.Except(file1Lines);

                            // Append IDs to file
                            File.WriteAllLines(@".\files\del_url1.txt", deleted_ids);
                            n = 6;
                            break;
                        }
                        Thread.Sleep(1000);

                    }

                    while (o < 5)
                    {
                        if (n == 6)
                        {

                            //Convert IDs to Wayback Machine urls
                            List<string> lines = new List<string>();
                            foreach (var line in File.ReadAllLines(@".\files\del_url1.txt"))
                            {
                                lines.Add("https://web.archive.org/web/20120523122213/http://wayback-fakeurl.archive.org/yt/" + line);
                            }

                            File.WriteAllLines(@".\files\del_url2.txt", lines.ToArray());

                            //Check if deleted videos are on the Wayback Machine and if so, return name and ID
                            strCmdText = @"/C ""yt-dlp.exe --verbose -ci --skip-download --print ""%(title)s | %(id)s"" --batch-file="".\files\del_url2.txt"" -i > "".\files\select1.txt"" & yt-dlp.exe --print id https://www.youtube.com/watch?v=jNQXAC9IVRw > "".\files\dummy2.txt""""";
                            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
                            Console.Read();

                            o = 6;
                            break;
                        }
                        Thread.Sleep(1000);

                    }

                    while (p < 5)
                    {
                        //Convert only seletable IDS to wayback urls

                        if (File.Exists(@".\files\dummy2.txt"))
                        {

                            List<string> lines4 = new List<string>();


                            foreach (var line in File.ReadAllLines(@".\files\select1.txt"))
                            {
                                listBox1.Items.Add(line);
                                string lines3 = line.Substring(line.Length - 11);
                                lines4.Add("https://web.archive.org/web/20120523122213/http://wayback-fakeurl.archive.org/yt/" + lines3);

                            }
                            File.WriteAllLines(@".\files\select2.txt", lines4.ToArray());

                            //Return error message if no deleted videos are in playlist or on the WM
                            var info = new FileInfo(@".\files\select2.txt");
                            if (info.Length == 0)
                            {
                                string message = "Deleted videos from this playlist were not found on the Wayback Machine.";
                                string title = "No archived videos found";
                                MessageBox.Show(message, title);
                            }


                            p = 6;
                            break;
                        }

                        Thread.Sleep(1000);
                    }
                }
                else if (textBox1.Text.Contains(",")) //For listing multiple urls in the textbox
                {
                    listBox1.Items.Clear();
                    string linebroken1 = textBox1.Text.Replace(",", System.Environment.NewLine);
                    string linebroken2 = linebroken1.Replace(" ", "");
                    File.WriteAllText(@".\files\del_url0.txt", linebroken2);

                    List<string> lines0 = new List<string>();
                    foreach (var line in File.ReadAllLines(@".\files\del_url0.txt"))
                    {
                        if (line.Length >= 11)
                        {
                            string validIds = line.Substring(line.Length - 11);
                            lines0.Add(validIds);
                        }
                    }
                    File.WriteAllLines(@".\files\del_url1.txt", lines0.ToArray());

                    //Convert IDs to Wayback Machine urls
                    List<string> lines = new List<string>();
                    foreach (var line in File.ReadAllLines(@".\files\del_url1.txt"))
                    {
                        lines.Add("https://web.archive.org/web/20120523122213/http://wayback-fakeurl.archive.org/yt/" + line);
                    }

                    File.WriteAllLines(@".\files\del_url2.txt", lines.ToArray());

                    //Check if deleted videos are on the Wayback Machine and if so, return name and ID
                    string strCmdText;
                    strCmdText = @"/C ""yt-dlp.exe --verbose -ci --skip-download --print ""%(title)s | %(id)s"" --batch-file="".\files\del_url2.txt"" -i > "".\files\select1.txt"" & yt-dlp.exe --print id https://www.youtube.com/watch?v=jNQXAC9IVRw > "".\files\dummy2.txt""""";
                    System.Diagnostics.Process.Start("CMD.exe", strCmdText);
                    Console.Read();


                    while (p < 5)
                    {
                        //Convert only seletable IDS to wayback urls

                        if (File.Exists(@".\files\dummy2.txt"))
                        {

                            List<string> lines4 = new List<string>();


                            foreach (var line in File.ReadAllLines(@".\files\select1.txt"))
                            {
                                listBox1.Items.Add(line);
                                string lines3 = line.Substring(line.Length - 11);
                                lines4.Add("https://web.archive.org/web/20120523122213/http://wayback-fakeurl.archive.org/yt/" + lines3);

                            }
                            File.WriteAllLines(@".\files\select2.txt", lines4.ToArray());

                            //Return error message if no deleted videos are in playlist or on the WM
                            var info = new FileInfo(@".\files\select2.txt");
                            if (info.Length == 0)
                            {
                                string message = "Deleted videos from this playlist were not found on the Wayback Machine.";
                                string title = "No archived videos found";
                                MessageBox.Show(message, title);
                            }


                            p = 6;
                            break;
                        }

                        Thread.Sleep(1000);
                    }

                }
                else
                {



                    //If the link is not a playlist (ie. video)

                    //Extract the ID from url
                    var url = textBox1.Text;

                    //Confirms that url is valid
                    if (!string.IsNullOrEmpty(url) && Uri.IsWellFormedUriString(url, UriKind.Absolute))
                    {
                        listBox1.Items.Clear();

                        var uri = new Uri(url);
                        var query = HttpUtility.ParseQueryString(uri.Query);
                        var videoId = string.Empty;

                        if (query.AllKeys.Contains("v"))
                        {
                            videoId = query["v"];
                            File.WriteAllText(@".\files\del_url1.txt", videoId);

                        }
                        else
                        {
                            videoId = uri.Segments.Last();
                            File.WriteAllText(@".\files\del_url1.txt", videoId);
                        }


                        //Convert ID to Wayback Machine urls
                        List<string> lines = new List<string>();
                        foreach (var line in File.ReadAllLines(@".\files\del_url1.txt"))
                        {
                            lines.Add("https://web.archive.org/web/20120523122213/http://wayback-fakeurl.archive.org/yt/" + line);
                        }

                        File.WriteAllLines(@".\files\del_url2.txt", lines.ToArray());

                        //Check if deleted video is on the Wayback Machine and if so, get name and ID
                        string strCmdText;
                        strCmdText = @"/C ""yt-dlp.exe --verbose -ci --skip-download --print ""%(title)s | %(id)s"" --batch-file="".\files\del_url2.txt"" -i > "".\files\select1.txt"" & yt-dlp.exe --print id https://www.youtube.com/watch?v=jNQXAC9IVRw > "".\files\dummy2.txt""""";
                        System.Diagnostics.Process.Start("CMD.exe", strCmdText);
                        Console.Read();

                        while (p < 5)
                        {
                            //Convert ID back to Wayback URL

                            if (File.Exists(@".\files\dummy2.txt"))
                            {

                                List<string> lines4 = new List<string>();


                                foreach (var line in File.ReadAllLines(@".\files\select1.txt"))
                                {
                                    listBox1.Items.Add(line);
                                    string lines3 = line.Substring(line.Length - 11);
                                    lines4.Add("https://web.archive.org/web/20120523122213/http://wayback-fakeurl.archive.org/yt/" + lines3);

                                }
                                File.WriteAllLines(@".\files\select2.txt", lines4.ToArray());

                                //If no video is found on WM, return an error message
                                var info = new FileInfo(@".\files\select2.txt");
                                if (info.Length == 0)
                                {
                                    string message = "Deleted video was not found on the Wayback Machine.";
                                    string title = "No archived video found";
                                    MessageBox.Show(message, title);
                                }


                                p = 6;
                                break;
                            }

                            Thread.Sleep(1000);
                        }

                    }
                    else if (url.Length == 11)
                    {
                        File.WriteAllText(@".\files\del_url1.txt", url);


                        //Convert ID to Wayback Machine urls
                        List<string> lines = new List<string>();
                        foreach (var line in File.ReadAllLines(@".\files\del_url1.txt"))
                        {
                            lines.Add("https://web.archive.org/web/20120523122213/http://wayback-fakeurl.archive.org/yt/" + line);
                        }

                        File.WriteAllLines(@".\files\del_url2.txt", lines.ToArray());

                        //Check if deleted video is on the Wayback Machine and if so, get name and ID
                        string strCmdText;
                        strCmdText = @"/C ""yt-dlp.exe --verbose -ci --skip-download --print ""%(title)s | %(id)s"" --batch-file="".\files\del_url2.txt"" -i > "".\files\select1.txt"" & yt-dlp.exe --print id https://www.youtube.com/watch?v=jNQXAC9IVRw > "".\files\dummy2.txt""""";
                        System.Diagnostics.Process.Start("CMD.exe", strCmdText);
                        Console.Read();

                        while (p < 5)
                        {
                            //Convert ID back to Wayback URL

                            if (File.Exists(@".\files\dummy2.txt"))
                            {

                                List<string> lines4 = new List<string>();


                                foreach (var line in File.ReadAllLines(@".\files\select1.txt"))
                                {
                                    listBox1.Items.Add(line);
                                    string lines3 = line.Substring(line.Length - 11);
                                    lines4.Add("https://web.archive.org/web/20120523122213/http://wayback-fakeurl.archive.org/yt/" + lines3);

                                }
                                File.WriteAllLines(@".\files\select2.txt", lines4.ToArray());

                                //If no video is found on WM, return an error message
                                var info = new FileInfo(@".\files\select2.txt");
                                if (info.Length == 0)
                                {
                                    string message = "Deleted video was not found on the Wayback Machine.";
                                    string title = "No archived video found";
                                    MessageBox.Show(message, title);
                                }


                                p = 6;
                                break;
                            }

                            Thread.Sleep(1000);
                        }

                    }
                    else //if url is invalid
                    {
                        string message = "Please enter a valid Youtube URL and try again.";
                        string title = "Invalid URL";
                        MessageBox.Show(message, title);
                    }



                }



            }
        }

        //---------------------------------------------------------------------------------------------------------//
        //---------------------------------------------------------------------------------------------------------//


        private void button2_Click(object sender, EventArgs e) //change directory
        {
            //Select save directory
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Choose a directory to save videos.";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                //Stores chosen save directory to file 
                string sSelectedPath = fbd.SelectedPath;
                textBox3.Text = sSelectedPath.ToString();
                File.WriteAllText(@".\files\save_directory.txt", textBox3.Text);
            }
        }

        private void button3_Click(object sender, EventArgs e) //paste button
        {
            if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                textBox1.Text = Clipboard.GetText(TextDataFormat.Text);
            }
        }

        //---------------------------------------------------------------------------------------------------------//
        //---------------------------------------------------------------------------------------------------------//

        private void button4_Click(object sender, EventArgs e) //Download selected
        {

            //Download the videos selected in the listBox
            List<string> download_all = new List<string>();


            foreach (string str in listBox1.SelectedItems)
            {
                var index = listBox1.Items.IndexOf(str);

                download_all.Add(File.ReadLines(@".\files\select2.txt").Skip(index).Take(1).First());

            }
            File.WriteAllLines(@".\files\selectF.txt", download_all.ToArray());

            string download_str = "selectF.txt";
            download_func(ref download_str);

        }

        private void button5_Click(object sender, EventArgs e) //Download all
        {
            //When the button is pressed, download all videos in listBox
            string download_str = "select2.txt";
            download_func(ref download_str);


        }


        private void button6_Click(object sender, EventArgs e) //Allows the x to close the window
        {
            this.Close();
        }

        //---------------------------------------------------------------------------------------------------------//
        //---------------------------------------------------------------------------------------------------------//


        //Makes window movable
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        //---------------------------------------------------------------------------------------------------------//
        //---------------------------------------------------------------------------------------------------------//


        private void button7_Click(object sender, EventArgs e) //Update yt-dlp
        {

            string strCmdText;
            strCmdText = @"/K ""yt-dlp.exe -U""";
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
            Console.Read();
        }

        //For opening video directory
        private void button8_Click(object sender, EventArgs e)
        {
            if (File.Exists(@".\files\save_directory.txt"))
            {
                System.Diagnostics.Process.Start("explorer.exe", @"" + textBox3.Text);
            }
            else
            {
                string strCmdText;
                strCmdText = @"/C ""cd .\videos\ & start .""";
                System.Diagnostics.Process.Start("CMD.exe", strCmdText);
                Console.Read();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //---------------------------------------------------------------------------------------------------------//
        //---------------------------------------------------------------------------------------------------------//

        public void download_func(ref string downBatch)
        {
            int i = 0;
            if (File.Exists(@".\files\dummy3.txt"))
            {
                File.Delete(@".\files\dummy3.txt");
            };
            if (File.Exists(@".\files\dummy4.txt"))
            {
                File.Delete(@".\files\dummy4.txt");
            }


            string vidRes1 = "";
            set_ResoF(ref vidRes1); //Resolution name

            string vidDem1 = "";
            set_DemiF(ref vidDem1); //Sets the resolution in cmd below

            string vfyes = "";
            set_Vf(ref vfyes); //For if the -vf command is used in ffmpeg

            string vidTyp1 = "";
            set_Type(ref vidTyp1); //Filetype for conversion

            string destin = " " + textBox3.Text; //Destination

            string strCmdTextOrig;
            strCmdTextOrig = @"/C ""yt-dlp.exe --verbose -ci --batch-file="".\files\\""" + downBatch + @""""" -o """ + textBox3.Text + @"""\%(title)s.%(ext)s"""" & yt-dlp.exe --print id https://www.youtube.com/watch?v=jNQXAC9IVRw > "".\files\dummy4.txt""""";

            string strCmdText;
            strCmdText = @"/C ""yt-dlp.exe --exec ""ffmpeg -i .\tmp\""\%(id)s"""".%(ext)s""" + vfyes + vidDem1 + destin + @"""""" + @"""\%(id)s""" + vidRes1 + vidTyp1 + @""""""" --verbose -ci --batch-file="".\files\\""" + downBatch + @""""" -o ""\tmp\%(id)s.%(ext)s"" & yt-dlp.exe --print id https://www.youtube.com/watch?v=jNQXAC9IVRw > "".\files\dummy3.txt""""";

            if (comboBox3.SelectedIndex == 0 && comboBox2.SelectedIndex == 0 && comboBox1.SelectedIndex == 0) //No type or resolution set
            {
                System.Diagnostics.Process.Start("CMD.exe", strCmdTextOrig);
                Console.Read();
            }
            else if (comboBox3.SelectedIndex == 1 && comboBox4.SelectedIndex == 0) //No type or resolution set
            {
                System.Diagnostics.Process.Start("CMD.exe", strCmdTextOrig);
                Console.Read();
            }
            else
            {
                System.Diagnostics.Process.Start("CMD.exe", strCmdText);
                Console.Read();
            }


            while (i < 5)
            {

                if (File.Exists(@".\files\dummy3.txt"))
                {

                    Id_ToTitle();

                    System.IO.DirectoryInfo di = new DirectoryInfo(@".\tmp\"); //Empty tmp file after converting
                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }

                    i = 6;
                    break;
                }

                if (File.Exists(@".\files\dummy4.txt"))
                {
                    i = 6;
                    break;
                }
                Thread.Sleep(1000);

            }
        }

        public void select1_func() //For renaming converted files
        {
            //Check if deleted video is on the Wayback Machine and if so, get name and ID
            string strCmdSelec;
            strCmdSelec = @"/C ""yt-dlp.exe --verbose -ci --skip-download --print ""%(title)s | %(id)s"" --batch-file="".\files\del_url2.txt"" -i > "".\files\select1.txt""""";

            if (comboBox3.SelectedIndex == 0 && comboBox2.SelectedIndex == 0 && comboBox1.SelectedIndex == 0) //No type or resolution set
            {

            }
            else if (comboBox3.SelectedIndex == 1 && comboBox4.SelectedIndex == 0) //No type or resolution set
            {

            }
            else
            {
                System.Diagnostics.Process.Start("CMD.exe", strCmdSelec);
                Console.Read();
            }
        }


        public void set_ResoF(ref string ResoF)
        {
            List<string> resolutionL = new List<string> { "_", "[144p]", "[240p]", "[360p]", "[480p]", "[720p]", "[1080p]", "[1440p]", "[2160p]" };
            if (comboBox3.SelectedIndex == 0)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (comboBox1.SelectedIndex == i)
                    {
                        ResoF = resolutionL[i];

                    }
                }
            }
            else
            {
                ResoF = "_";
            }
        }


        public void set_DemiF(ref string DemiF)
        {
            List<string> resolutionD = new List<string> { "", "256:144", "426:240", "640:360", "854:480", "1280:720", "1920:1080", "2560:1440", "3840:2160" };
            if (comboBox3.SelectedIndex == 0)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (comboBox1.SelectedIndex == i)
                    {
                        if (i > 0)
                        {
                            DemiF = "scale=" + resolutionD[i];

                        }
                        else
                        {
                            DemiF = "";
                        }

                    }
                }
            }
            else
            {
                DemiF = "";
            }
        }

        public void set_Vf(ref string defVf)
        {
            if (comboBox3.SelectedIndex == 0)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    defVf = " ";
                }
                else
                {
                    defVf = " -vf ";
                }
            }
            else
            {
                defVf = " ";
            }
        }

        public void set_Type(ref string TypeF)
        {
            List<string> typelistV = new List<string> { ".%(ext)s", ".3g2", ".3gp", ".avi", ".flv", ".mkv", ".mov", ".mp4", ".mpeg", ".ogv", ".webm", ".wmv" };
            List<string> typelistA = new List<string> { ".%(ext)s", ".aac", ".aiff", ".flac", ".m4a", ".m4r", ".mmf", ".mp3", ".ogg", ".opus", ".wav", ".wma" };

            if (comboBox3.SelectedIndex == 0)
            {
                for (int i = 0; i < 12; i++)
                {
                    if (comboBox2.SelectedIndex == i)
                    {
                        TypeF = typelistV[i];
                    }

                }
            }
            else
            {
                for (int i = 0; i < 12; i++)
                {
                    if (comboBox4.SelectedIndex == i)
                    {
                        TypeF = typelistA[i];
                    }
                }

            }
        }

        public void set_Type2(ref string TypeF) //For renaming
        {
            List<string> typelistV = new List<string> { ".*", ".3g2", ".3gp", ".avi", ".flv", ".mkv", ".mov", ".mp4", ".mpeg", ".ogv", ".webm", ".wmv" };
            List<string> typelistA = new List<string> { ".*", ".aac", ".aiff", ".flac", ".m4a", ".m4r", ".mmf", ".mp3", ".ogg", ".opus", ".wav", ".wma" };

            if (comboBox3.SelectedIndex == 0)
            {
                for (int i = 0; i < 12; i++)
                {
                    if (comboBox2.SelectedIndex == i)
                    {
                        TypeF = typelistV[i];
                    }

                }
            }
            else
            {
                for (int i = 0; i < 12; i++)
                {
                    if (comboBox4.SelectedIndex == i)
                    {
                        TypeF = typelistA[i];
                    }
                }

            }
        }



        private static string MakeValidFileName(string name) //When renaming files, replace forbidden characters with an underscore
        {
            string invalidChars = System.Text.RegularExpressions.Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
            string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

            return System.Text.RegularExpressions.Regex.Replace(name, invalidRegStr, "_");
        }

        private void Id_ToTitle() //This batch renames downloaded files after they are converted by type and name
        {
            foreach (var line in File.ReadAllLines(@".\files\select1.txt"))
            {
                if (line.Length >= 14)
                {
                    string vidName = line.Substring(line.Length - 11);      //Youtube ID
                    string newName = line.Substring(0, line.Length - 14);   //Youtube video name
                    string vidRes = "";
                    set_ResoF(ref vidRes);                                  //Resolution in name
                    string vidPath = textBox3.Text + @"\";                  //Retrieves user's set path
                    string fileExt = "";
                    set_Type2(ref fileExt);

                    var files1 = Directory.GetFiles(vidPath, vidName + vidRes + fileExt); //Defines variable by video name without extension
                    if (files1.Length > 0)                                             //Confirms file with name exists in destination
                    {
                        string fileType = Path.GetExtension(Directory.GetFiles(textBox3.Text + @"\", vidName + vidRes + fileExt)[0]); //Get extension based on file possesing the correct name

                        string nameRes = Path.GetFileNameWithoutExtension(textBox3.Text + @"\" + vidName + vidRes + fileType);     //ID and Resolution
                        string oldFile = vidPath + nameRes + fileType;                                                             //Directory with ID and Resolution
                        string newFile = vidPath + MakeValidFileName(newName) + vidRes + fileType;                                 //Directory with Video title with and Resolution


                        if (File.Exists(newFile))
                        {

                        }
                        else
                        {
                            System.IO.File.Move(oldFile, newFile); //Rename
                        }
                    }
                }
            }
        }

        //---------------------------------------------------------------------------------------------------------//
        //---------------------------------------------------------------------------------------------------------//

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e) //Swapping between video and audio comboboxes
        {
            if (comboBox3.SelectedIndex == 0) //video
            {
                comboBox2.Visible = true;
                comboBox4.Visible = false;
                comboBox1.Visible = true;


            }
            else                             //audio
            {
                comboBox4.Visible = true;
                comboBox2.Visible = false;
                comboBox1.Visible = false; //Disables quality adjustment

            }

        }

        private void button9_Click(object sender, EventArgs e) //Download from text file
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Title = "Select a Text File",
                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "txt files (*.txt) | *.txt",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                List<string> fileLine2 = new List<string>();

                foreach (var line in File.ReadAllLines(openFileDialog1.FileName))
                {
                    if (line.Length >= 11)
                    {
                        string fileLine1 = line.Substring(line.Length - 11);
                        fileLine2.Add(fileLine1);

                    }
                    File.WriteAllLines(@".\files\del_url1.txt", fileLine2.ToArray());
                }

                if (checkBox1.Checked == true) //If the checkbox is checked, skip selection
                {
                    listBox1.Items.Clear();

                    //Convert IDs to Wayback Machine urls
                    List<string> lines = new List<string>();
                    foreach (var line in File.ReadAllLines(@".\files\del_url1.txt"))
                    {
                        lines.Add("https://web.archive.org/web/20120523122213/http://wayback-fakeurl.archive.org/yt/" + line);
                    }

                    File.WriteAllLines(@".\files\del_url2.txt", lines.ToArray());

                    select1_func();

                    //Check if deleted videos are on the Wayback Machine and if so download them
                    string download_str = "del_url2.txt";
                    download_func(ref download_str);

                }
                else //Don't skip selection
                {
                    listBox1.Items.Clear();

                    // Resets variable for loops and deletes dummy files used for cmd lines
                    int p = 0;
                    if (File.Exists(@".\files\dummy2.txt"))
                    {
                        File.Delete(@".\files\dummy2.txt");
                    };


                    //Convert IDs to Wayback Machine urls
                    List<string> lines = new List<string>();
                    foreach (var line in File.ReadAllLines(@".\files\del_url1.txt"))
                    {
                        lines.Add("https://web.archive.org/web/20120523122213/http://wayback-fakeurl.archive.org/yt/" + line);
                    }

                    File.WriteAllLines(@".\files\del_url2.txt", lines.ToArray());

                    //Check if deleted videos are on the Wayback Machine and if so, return name and ID
                    string strCmdText;
                    strCmdText = @"/C ""yt-dlp.exe --verbose -ci --skip-download --print ""%(title)s | %(id)s"" --batch-file="".\files\del_url2.txt"" -i > "".\files\select1.txt"" & yt-dlp.exe --print id https://www.youtube.com/watch?v=jNQXAC9IVRw > "".\files\dummy2.txt""""";
                    System.Diagnostics.Process.Start("CMD.exe", strCmdText);
                    Console.Read();

                    while (p < 5)
                    {
                        //Convert only seletable IDS to wayback urls

                        if (File.Exists(@".\files\dummy2.txt"))
                        {

                            List<string> lines4 = new List<string>();


                            foreach (var line in File.ReadAllLines(@".\files\select1.txt"))
                            {
                                listBox1.Items.Add(line);
                                string lines3 = line.Substring(line.Length - 11);
                                lines4.Add("https://web.archive.org/web/20120523122213/http://wayback-fakeurl.archive.org/yt/" + lines3);

                            }
                            File.WriteAllLines(@".\files\select2.txt", lines4.ToArray());

                            //Return error message if no deleted videos are in playlist or on the WM
                            var info = new FileInfo(@".\files\select2.txt");
                            if (info.Length == 0)
                            {
                                string message = "Deleted videos from this playlist were not found on the Wayback Machine.";
                                string title = "No archived videos found";
                                MessageBox.Show(message, title);
                            }


                            p = 6;
                            break;
                        }

                        Thread.Sleep(1000);
                    }

                }


            }
        }
    }
}