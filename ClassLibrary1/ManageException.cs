using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
   public class ManageException
    {
        public void DownLoad(string url)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    var res = client.DownloadString(url);
                }
            }
            //esegue questo soloe se staut è protocol error C#6
            catch (WebException e) when(e.Status ==WebExceptionStatus.ProtocolError)
            {
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


        }

    
}
