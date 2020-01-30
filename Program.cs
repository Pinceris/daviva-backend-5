using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace daviva_backend_5
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();
        private const string Api = "https://backend.daviva.lt/public/Markes";
        private const string ApiRRR = "https://backend.daviva.lt/API/GetBrandasFromRRR";

        static async Task Main(string[] args)
        {
            List<string> Makes = await GetAllMarkesAsync<List<string>>(Api);

            var response = await client.GetStringAsync(ApiRRR);
            RRRModel.MarkesJson RRRMarkes = JsonConvert.DeserializeObject<RRRModel.MarkesJson>(response);
            List<string> ApiMakeList = new List<string>();
            foreach (var mark in RRRMarkes.Markes)
            {
                ApiMakeList.Add(mark.Name);
            }
            //1 Sarasas
            List<string> AtitinkanciosMarkes = TinkanciosMarkes(Makes, RRRMarkes);
            //2 Sarasas
            List<string> PublicMarkes = Makes.Except(ApiMakeList).ToList();
            //3 Sarasas
            List<string> APIMarkes = ApiMakeList.Except(Makes).ToList();

            bool outcome = await ProgramLoopAsync(Makes, RRRMarkes);

            while (!outcome) outcome = await ProgramLoopAsync(Makes, RRRMarkes);

        }
        static async Task<T> GetAllMarkesAsync<T>(string path)
        {
            T makes = default(T);
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                makes = await response.Content.ReadAsAsync<T>();
            }
            return makes;
        }
        private static List<string> TinkanciosMarkes(List<string> markes, RRRModel.MarkesJson rMarkes)
        {
            List<string> sarasas = new List<string>();

            foreach(var marke in rMarkes.Markes)
            {
                var temp = markes.Find(q => q == marke.Name);
                if (temp != null)
                {
                    sarasas.Add(marke.Name);
                }
            }

            return sarasas;
        }
        private static async Task<bool> ProgramLoopAsync(List<string> Makes, RRRModel.MarkesJson RRRMarkes)
        {
            Console.WriteLine("iveskite automobilio marke");
            string make = Console.ReadLine();

            if (Makes.Contains(make))
            {
                var RMarke = RRRMarkes.Markes.Find(q => q.Name == make);
                if (RMarke != null)
                {
                    List<string> models = await GetAllMarkesAsync<List<string>>("https://backend.daviva.lt/public/Modeliai?Name=" + make);
                    var response1 = await client.GetStringAsync("https://backend.daviva.lt/API/GetCarModelsFromRRR?BrandID=" + RMarke.Id);
                    RFullModel.RFullJson RFull = JsonConvert.DeserializeObject<RFullModel.RFullJson>(response1);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
