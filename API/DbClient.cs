using System.Collections.Generic;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using API.Models;

namespace API
{
    public class DbClient
    {
        IFirebaseClient _client;
        IFirebaseConfig _config = new FirebaseConfig
        {
            AuthSecret = "NYRFDl2OMBTtfljvSqloTsTmg71kKwFUM44NJe2c",
            BasePath = $"https://web-lab3-6cd42-default-rtdb.europe-west1.firebasedatabase.app/"
        };       

        public DbClient()
        {
            _client = new FireSharp.FirebaseClient(_config);    
            
        }
        
        public async Task<List<Collapse>> GetAll()
        {
            var result = new List<Collapse>();

            FirebaseResponse response = await _client.GetAsync("Collapse/");
            if (response.Body == "null")
                return null;            
            
            var records = response.ResultAs<Dictionary<string, Collapse>>();

            foreach (var record in records)
            {
                result.Add(new Collapse
                {
                    Id = record.Value.Id,
                    Name = record.Value.Name,
                    Content = record.Value.Content,
                });
            }
            return result;
        }

        public async Task<bool> PostCollapse(Collapse collapse)
        {
            SetResponse response = await _client.SetAsync("Collapse/"+collapse.Id, collapse);
            
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public async Task<bool> Delete(int id)
        {
            FirebaseResponse response = await _client.DeleteAsync("Collapse/" + id);
            if (response.StatusCode == System.Net.HttpStatusCode.OK) 
               return true;
            return false;
        }
    }
}
