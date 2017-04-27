using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using NServiceBus;
using Owin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Zsus.Noe.Approve
{
    public partial class NoeService : ServiceBase
    {
        //public string baseAddress = "http://localhost:9000/";
        public string baseAddress = "http://*:9000/";
        private IDisposable _server = null;

        public NoeService()
        {
            InitializeComponent();
        }

        public void ConsoleOnStart()
        {
            string[] args = new string[] { "" };
            OnStart(args);
        }

        protected override void OnStart(string[] args)
        {
            _server = WebApp.Start<Startup>(url: baseAddress);

        }

        public void ConsoleOnStop()
        {
            OnStop();
        }

        protected override void OnStop()
        {
            if (_server != null)
            {
                _server.Dispose();
            }
            base.OnStop();
        }
    }
}
