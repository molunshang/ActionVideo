using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ActionVideo.Control
{
    public class VideoSearchHandler : SearchHandler
    {
        public event Action<SearchHandler, string> QueryConfirmed;
        protected override void OnQueryConfirmed()
        {
            base.OnQueryConfirmed();
            QueryConfirmed?.Invoke(this, Query);
        }
    }
}
