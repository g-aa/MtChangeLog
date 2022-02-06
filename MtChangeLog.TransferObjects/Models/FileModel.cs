using MtChangeLog.TransferObjects.Views.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.TransferObjects.Models
{
    public class FileModel
    {
        public string Title { get; private set; }
        public IEnumerable<byte> Bytes { get; private set;}

        public FileModel() 
        {
            this.Title = "empty.txt";
            this.Bytes = new List<byte>();
        }

        public FileModel(string title, IEnumerable<byte> bytes) : this()
        {
            if (!string.IsNullOrEmpty(title) && bytes is not null) 
            {
                this.Title = title;
                this.Bytes = bytes;
            }
        }

        public FileModel(ProjectVersionHistoryView view)
        {
            if (view != null) 
            {
                this.Title = string.IsNullOrEmpty(view.Title) ? "noname" : $"ChangeLog-{view.Title}.txt";
                this.Bytes = Encoding.UTF8.GetBytes(string.Join(Environment.NewLine, view.History.Select(e => e.ToText())));
            }
        }
    }
}
