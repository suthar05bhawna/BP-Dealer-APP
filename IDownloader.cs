using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS
{
    public interface IDownloader : IApplicationService
    {
        Task DownloadFile(string url, string folder);
        event EventHandler<DownloadEventArgs> OnFileDownloaded;
        void OpenFile(string filePath);
    }
    public class DownloadEventArgs : EventArgs
    {
        public bool FileSaved = false;
        public string Url = "";
        public DownloadEventArgs(bool fileSaved, string url)
        {
            FileSaved = fileSaved;
           Url = url;
        }
    }
}
