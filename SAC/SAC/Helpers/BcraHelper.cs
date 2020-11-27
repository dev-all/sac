using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SAC.Helpers
{

    public static class BcraHelper
    {
        public static float GetCotizacion()
        {
            return 0;
            //IEnumerable<StudentViewModel> students = null;

            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("http://localhost:64189/api/");
            //    //HTTP GET
            //    var responseTask = client.GetAsync("student");
            //    responseTask.Wait();

            //    var result = responseTask.Result;
            //    if (result.IsSuccessStatusCode)
            //    {
            //        var readTask = result.Content.ReadAsAsync<IList<StudentViewModel>>();
            //        readTask.Wait();

            //        students = readTask.Result;
            //    }
            //    else //web api sent error response 
            //    {
            //        //log response status here..

            //        students = Enumerable.Empty<StudentViewModel>();

            //        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            //    }
            //}
        }



        public static System.Drawing.Image Base64ToImage(string base64String)
        {
            // Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            // Convert byte[] to Image
            var ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image imagen = System.Drawing.Image.FromStream(ms, true);
            return imagen;
        }
    }

}