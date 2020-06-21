using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class ManageException
    {

        public void GeneroEccezioneNonGestita()
        {
            throw new Exception();
        }
        
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
            catch (WebException e) when (e.Status == WebExceptionStatus.ProtocolError)
            {
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {

            }


        }

    }

    public class MiaException : Exception
    {
        
    }
    public class MiaException1 : InvalidOperationException
    {
        public MiaException1()
        {
        }

        public MiaException1(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MiaException1(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
