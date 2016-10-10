using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.XPath;
using IWshRuntimeLibrary;

namespace gdc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //change_gdc.Enabled = false;
        }

        private void change_gdc_Click(object sender, EventArgs e)
        {
            String fpath = this.textBox1.Text;
            string tf;
            string tf2 = "";
            tf = GetShortcutTarget(fpath);
            string[] sarray = tf.Split(new string[] {"bin\\gdc.exe"}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var i in sarray)
            {
                tf2 = i;
            }

            tf2 = tf2 + "\\etc\\config.xml";
            xmlWrite(tf2);
            MessageBox.Show("更新成功");
        }

        private void xmlWrite(string tf)
        {
            //string sxml =
            //    "    <Shortcut authenticationMode=\"standard\" passwordRequired=\"true\" askPasswordAfterRestart=\"true\" proxyBypass=\"false\" userName=\"\" loginBoxOnTop=\"true\" askPasswordAgain=\"true\" url=\"http://192.168.200.11:8888/gas/wa/r/gdc-tiptop-udm-intranet\" type=\"http\" customLoginFile=\"\" certificate=\"disabled\" proxyURL=\"\" name=\"顶立正式\" proxyType=\"monitor\" keepPassword=\"true\" allowPersistentSave=\"true\" kerberosREALM=\"\"/>";

            //var doc = new XDocument(
            //    new XElement("fjs",
            //        new XElement("Shortcuts",
            //            new XElement("Shortcut",
            //                new XAttribute("authenticationMode", "standard"),
            //                new XAttribute("passwordRequired", "true"),
            //                new XAttribute("askPasswordAfterRestart", "true"),
            //                new XAttribute("proxyBypass", "false"),
            //                new XAttribute("loginBoxOnTop", "true"),
            //                new XAttribute("askPasswordAgain", "true"),
            //                new XAttribute("url", "http://192.168.200.11:8888/gas/wa/r/gdc-tiptop-udm-intranet"),
            //                new XAttribute("type", "http"),
            //                new XAttribute("certificate", "disabled"),
            //                new XAttribute("name", "顶立正式"),
            //                new XAttribute("proxyType", "monitor"),
            //                new XAttribute("keepPassword", "true"),
            //                new XAttribute("allowPersistentSave", "true")
            //            )
            //        )
            //    )
            //);
            //doc.Save(tf);

            XElement xe = new XElement("Shortcut",
                new XAttribute("authenticationMode", "standard"),
                new XAttribute("passwordRequired", "true"),
                new XAttribute("askPasswordAfterRestart", "true"),
                new XAttribute("proxyBypass", "false"),
                new XAttribute("loginBoxOnTop", "true"),
                new XAttribute("askPasswordAgain", "true"),
                new XAttribute("url", "http://192.168.200.11:8888/gas/wa/r/gdc-tiptop-udm-intranet"),
                new XAttribute("type", "http"),
                new XAttribute("certificate", "disabled"),
                new XAttribute("name", "顶立正式"),
                new XAttribute("proxyType", "monitor"),
                new XAttribute("keepPassword", "true"),
                new XAttribute("allowPersistentSave", "true")
            );


            try
            {
                //定义并从xml文件中加载节点（根节点）
                XElement rootNode = XElement.Load(tf);
                //查询语句: 获取ID属性值等于"999999"的所有User节点
                IEnumerable<XElement> targetNodes = from target in rootNode.Descendants("Shortcut")
                    select target;

                //将获得的节点集合中的每一个节点依次从它相应的父节点中删除
                targetNodes.Remove();
                //保存对xml的更改操作
                rootNode.Save(tf);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


            try
            {
                //定义并从xml文件中加载节点（根节点）
                XElement rootNode = XElement.Load(tf);
                var find = rootNode.Elements("Shortcuts").FirstOrDefault();
                //MessageBox.Show(find.ToString());

                //定义一个新节点
                XElement newNode = new XElement(xe);
                //将此新节点添加到根节点下
                find.Add(newNode);
                //保存对xml的更改操作
                rootNode.Save(tf);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


            //var filePath =tf; //绝对路径或相对路径
            //var root = XElement.Load(filePath);
            //var find = root.Elements("fjs").Where(p => p.Attribute("name").Value == "2").Elements("Shortcuts");
            //if (find != null)
            //    find.Add();
            //root.Save(tf);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetCtrlDragEvent(this.textBox1);
            //change_gdc.Enabled = true;
        }

        public static void SetCtrlDragEvent(Control ctrl)
        {
            if (ctrl is TextBox)
            {
                TextBox tb = ctrl as TextBox;
                tb.AllowDrop = true;
                tb.DragEnter += (sender, e) => { e.Effect = DragDropEffects.Link; //拖动时的图标
                };
                tb.DragDrop +=
                    (sender, e) =>
                    {
                        ((TextBox) sender).Text =
                            ((System.Array) e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                    };
            }
        }

        private string GetShortcutTarget(string file)
        {
            try
            {
                if (System.IO.Path.GetExtension(file).ToLower() != ".lnk")
                {
                    throw new Exception("Supplied file must be a .LNK file");
                }

                FileStream fileStream = System.IO.File.Open(file, FileMode.Open, FileAccess.Read);
                using (System.IO.BinaryReader fileReader = new BinaryReader(fileStream))
                {
                    fileStream.Seek(0x14, SeekOrigin.Begin); // Seek to flags
                    uint flags = fileReader.ReadUInt32(); // Read flags
                    if ((flags & 1) == 1)
                    {
                        // Bit 1 set means we have to
                        // skip the shell item ID list
                        fileStream.Seek(0x4c, SeekOrigin.Begin); // Seek to the end of the header
                        uint offset = fileReader.ReadUInt16(); // Read the length of the Shell item ID list
                        fileStream.Seek(offset, SeekOrigin.Current); // Seek past it (to the file locator info)


                        long fileInfoStartsAt = fileStream.Position; // Store the offset where the file info
                        // structure begins
                        uint totalStructLength = fileReader.ReadUInt32(); // read the length of the whole struct
                        fileStream.Seek(0xc, SeekOrigin.Current); // seek to offset to base pathname
                        uint fileOffset = fileReader.ReadUInt32(); // read offset to base pathname
                        // the offset is from the beginning of the file info struct (fileInfoStartsAt)
                        fileStream.Seek((fileInfoStartsAt + fileOffset), SeekOrigin.Begin); // Seek to beginning of
                        // base pathname (target)
                        long pathLength = (totalStructLength + fileInfoStartsAt) - fileStream.Position - 2; // read
                        // the base pathname. I don't need the 2 terminating nulls.
                        char[] linkTarget = fileReader.ReadChars((int) pathLength); // should be unicode safe
                        var link = new string(linkTarget);

                        int begin = link.IndexOf("\0\0");
                        if (begin > -1)
                        {
                            int end = link.IndexOf("\\\\", begin + 2) + 2;
                            end = link.IndexOf('\0', end) + 1;

                            string firstPart = link.Substring(0, begin);
                            string secondPart = link.Substring(end);

                            return firstPart + secondPart;
                        }
                        else
                        {
                            return link;
                        }
                    }
                }
            }
            catch
            {
                return "";
            }
            return "";
        }
    }
}