using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ParserLBC
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            webBrowser.NavigationCompleted += WebBrowser_NavigationCompleted;
            webBrowser.Navigate(new Uri(""));
        }

        private async void WebBrowser_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            string html = await webBrowser.InvokeScriptAsync("eval", new string[] { "document.documentElement.innerHTML;" });

            int cursor = 0;
            string token = "\"subject\":";
            int count = 0;
            while (cursor < html.Length)
            {
                count++;
                cursor = html.IndexOf(token, cursor);
                if (cursor == -1)
                    break;
                cursor += token.Length;
                cursor += 1; //skip "
                int endOfSubject = html.IndexOf("\"", cursor);
                System.Diagnostics.Debug.Write(html.Substring(cursor, endOfSubject - cursor) + "\n");
            }
            System.Diagnostics.Debug.Write(count);


        }
    }
}
