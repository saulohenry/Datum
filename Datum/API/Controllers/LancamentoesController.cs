using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using Domain;
using System.Net.Http;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LancamentoesController : ControllerBase
    {
        private readonly APIContext _context;

        public LancamentoesController(APIContext context)
        {
            _context = context;
        }

        // GET: api/Lancamentoes
        [HttpGet]
        public HttpContent GetLancamento()
        {
            HttpClient client = new HttpClient();
            Task<HttpResponseMessage> message = client.GetAsync("http://localhost:57728/api/Lancamentoes");
            return message.Result.Content;
        }

        // GET: api/Lancamentoes/5
        [HttpGet("{id}")]
        public HttpContent GetLancamento(int id)
        {
            HttpClient client = new HttpClient();
            Task<HttpResponseMessage> message = client.GetAsync("http://localhost:57728/api/Lancamentoes/" + id);
            return message.Result.Content;            
        }

        // PUT: api/Lancamentoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public HttpContent PutLancamento(int id, Lancamento lancamento)
        {
            HttpClient client = new HttpClient();
            string json = JsonConvert.SerializeObject(lancamento);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            
            Task<HttpResponseMessage> message = client.PutAsync("http://localhost:57728/api/Lancamentoes/" + id, httpContent);
            
            return message.Result.Content;
        }

        // POST: api/Lancamentoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public HttpContent PostLancamento(Lancamento lancamento)
        {
            HttpClient client = new HttpClient();
            string json = JsonConvert.SerializeObject(lancamento);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            Task<HttpResponseMessage> message = client.PostAsync("http://localhost:57728/api/Lancamentoes/" + lancamento.id, httpContent);

            return message.Result.Content;
        }

        // DELETE: api/Lancamentoes/5
        [HttpDelete("{id}")]
        public HttpContent DeleteLancamento(int id)
        {
            HttpClient client = new HttpClient();
            Task<HttpResponseMessage> message = client.DeleteAsync("http://localhost:57728/api/Lancamentoes/" + id);

            return message.Result.Content;
        }

        private bool LancamentoExists(int id)
        {
            return _context.Lancamento.Any(e => e.id == id);
        }
    }
}
