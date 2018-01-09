using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forex
{
    public interface ILogger
    {
        void Write(string message = "");
        void Close();
    }

    public class FileLogger : ILogger
    {
        private string path;
        private string directoryName;
        private DateTime createTime;
        private StringBuilder stringBuilder;

        public FileLogger(string path)
        {
            this.path = path;
            this.createTime = DateTime.Now;
            this.stringBuilder = new StringBuilder();
        }

        public void Write(string message = "")
        {
            this.stringBuilder.AppendLine(message);
        }

        private void CreateDirectory()
        {

            this.directoryName = Path.Combine(this.path, this.createTime.ToString("yy-MM-dd", new CultureInfo("Ru-ru")));
            if (!Directory.Exists(this.directoryName))
            {
                Directory.CreateDirectory(this.directoryName);
            }

            
        }

        private string GetFileName()
        {
            string fileName = this.createTime.ToString("HH_mm_ss") +".txt";
            return Path.Combine(this.directoryName, fileName);
        }

        public void Close()
        {
            CreateDirectory();

            File.WriteAllText(GetFileName(), this.stringBuilder.ToString());
        }
    }

    public class ConsoleLogger : ILogger
    {
        public void Write(string message = "")
        {
            Console.WriteLine(message);
        }

        public void Close()
        {
            Console.WriteLine("Finish");
        }
    }

    public class EmptyLogger : ILogger
    {
        public void Write(string message = "")
        {
            return;
        }

        public void Close()
        {
            return;
        }
    }
}
