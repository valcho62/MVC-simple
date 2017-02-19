using System.Collections;
using System.Collections.Generic;

namespace SimpleHttpServer.Models
{
    public class HttpSession
    {
        private IDictionary<string, string> parameters;
        public string Id { get; set; }

        public HttpSession(string id)
        {
            this.parameters =new Dictionary<string, string>();
            this.Id = id;
        }

        public string this[string key]
        {
            get { return this[key]; }
        }

        public void Clear()
        {
            parameters = new Dictionary<string, string>();
        }

        public void Add(string key, string value)
        {
            parameters.Add(key,value);
        }
    }
}