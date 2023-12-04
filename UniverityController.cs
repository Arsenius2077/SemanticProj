using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NastyaWebApi.Data;
using NastyaWebApi.Model;
using VDS.RDF;
using VDS.RDF.Query;

namespace NastyaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniverityController : ControllerBase
    {
        private readonly DBContext _context;

        public UniverityController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Univerity
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UniverityModel>>> GetuniverityModels()
        {
          if (_context.univerityModels == null)
          {
              return NotFound();
          }
            return await _context.univerityModels.ToListAsync();
        }

        // GET: api/Univerity/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UniverityModel>> GetUniverityModel(int id)
        {
          if (_context.univerityModels == null)
          {
              return NotFound();
          }
            var univerityModel = await _context.univerityModels.FindAsync(id);

            if (univerityModel == null)
            {
                return NotFound();
            }

            return univerityModel;
        }

        [HttpPost]
        //public async Task<ActionResult<IEnumerable<UniverityModel>>> PostDBPEDIAUniverityModel()
        //{
        //    if (_context.univerityModels == null)
        //    {
        //        return Problem("Entity set 'DBContext.univerityModels' is null.");
        //    }

        //    SparqlRemoteEndpoint endpoint = new SparqlRemoteEndpoint(new Uri("http://dbpedia.org/sparql"));

        //    // Combine all your queries into one
        //    SparqlResultSet results = endpoint.QueryWithResultSet(@"
        //    SELECT  DISTINCT SAMPLE(?university) ?name SAMPLE(?country) SAMPLE(?established) SAMPLE(?city) SAMPLE(?abstract)  WHERE {
        //    ?university dbo:affiliation dbr:European_University_Association.
        //    ?university dbp:country  ?country.
        //    ?university dbp:established ?established.
        //    ?university dbo:city ?city.
        //    ?university dbo:abstract ?abstract .
        //    ?university dbp:name ?name.
        //    } ORDER BY ?university");
        //    results.Distinct();

        //    List<UniverityModel> newUniversities = new List<UniverityModel>();

        //    // Process each result and create a new UniverityModel
        //    foreach (SparqlResult result in results)
        //    {
        //        UniverityModel universityModel = new UniverityModel
        //        {
        //            Link = ConvertToString(result, "university"),
        //            Name = ConvertToString(result, "name"),
        //            Country = ConvertToString(result, "country"),
        //            Established = ConvertToString(result, "established") ?? "Unknown",
        //            City = ConvertToString(result, "city"),
        //            About = ConvertToString(result, "abstract")
        //        };

        //        // Add the new UniverityModel to the context
        //        _context.univerityModels.Add(universityModel);
        //    }

        //    // Save all the new university models to the database at once
        //    await _context.SaveChangesAsync();

        //    // Return the newly created university models
        //    return CreatedAtAction(nameof(GetuniverityModels), newUniversities);
        //}

        //private static string ConvertToString(SparqlResult result, string variableName)
        //{
        //    if (result.TryGetValue(variableName, out var value))
        //    {
        //        // Handle URIs and Literals differently
        //        if (value is IUriNode uriNode)
        //        {
        //            return uriNode.Uri.AbsoluteUri;
        //        }
        //        else if (value is ILiteralNode literalNode)
        //        {
        //            return literalNode.Value;
        //        }
        //    }
        //    return null; // Return null to signify no data was found for this variable
        //}
        public async Task<ActionResult<UniverityModel>> PostDBPEDIAUniverityModel(UniverityModel univerityModel)
        {
            if (_context.univerityModels == null)
            {
                return Problem("Entity set 'DBContext.univerityModels'  is null.");
            }
            SparqlRemoteEndpoint endpoint = new SparqlRemoteEndpoint(new Uri("http://dbpedia.org/sparql"));

            SparqlResultSet Link = endpoint.QueryWithResultSet(@"
            SELECT  DISTINCT ?university WHERE {
            ?university dbo:affiliation dbr:European_University_Association.
            }");

            SparqlResultSet Name = endpoint.QueryWithResultSet(@"
            SELECT  DISTINCT ?name WHERE {
            ?university dbo:affiliation dbr:European_University_Association.
            ?university dbp:name ?name.
            }");

            SparqlResultSet Country = endpoint.QueryWithResultSet(@"
            SELECT  DISTINCT ?country WHERE {
            ?university dbo:affiliation dbr:European_University_Association.
            ?university dbp:country  ?country.
            }");

            SparqlResultSet DateOfEstablish = endpoint.QueryWithResultSet(@"
            SELECT  DISTINCT ?established WHERE {
            ?university dbo:affiliation dbr:European_University_Association.
            ?university dbp:established ?established.
            }");

            SparqlResultSet City = endpoint.QueryWithResultSet(@"
            SELECT  DISTINCT ?city WHERE {
            ?university dbo:affiliation dbr:European_University_Association.
            ?university dbo:city ?city.
            }");

            SparqlResultSet About = endpoint.QueryWithResultSet(@"
            SELECT  DISTINCT ?abstract WHERE {
            ?university dbo:affiliation dbr:European_University_Association.
            ?university dbo:abstract ?abstract .
            }");


            foreach (SparqlResult sparql in Link)
            {
                var tmp = sparql["Link"].ToString();
                univerityModel.Link = tmp;
                _context.univerityModels.Add(univerityModel);
                await _context.SaveChangesAsync();
            }

            foreach (SparqlResult sparql in Name)
            {
                var tmp = sparql["Name"].ToString();
                univerityModel.Name = tmp;
                _context.univerityModels.Add(univerityModel);
                await _context.SaveChangesAsync();
            }

            foreach (SparqlResult sparql in Country)
            {
                var tmp = sparql["Country"].ToString();
                univerityModel.Established = tmp;
                _context.univerityModels.Add(univerityModel);
                await _context.SaveChangesAsync();
            }

            foreach (SparqlResult sparql in DateOfEstablish)
            {
                var tmp = sparql["DateOfEstablish"].ToString();
                univerityModel.Country = tmp;
                _context.univerityModels.Add(univerityModel);
                await _context.SaveChangesAsync();
            }

            foreach (SparqlResult sparql in City)
            {
                var tmp = sparql["City"].ToString();
                univerityModel.City = tmp;
                _context.univerityModels.Add(univerityModel);
                await _context.SaveChangesAsync();
            }

            foreach (SparqlResult sparql in About)
            {
                var tmp = sparql["About"].ToString();
                univerityModel.About = tmp;
                _context.univerityModels.Add(univerityModel);
                await _context.SaveChangesAsync();
            }

            _context.univerityModels.Add(univerityModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUniverityModel", new { id = univerityModel.Id }, univerityModel);
        }

        // DELETE: api/Univerity/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUniverityModel(int id)
        {
            if (_context.univerityModels == null)
            {
                return NotFound();
            }
            var univerityModel = await _context.univerityModels.FindAsync(id);
            if (univerityModel == null)
            {
                return NotFound();
            }

            _context.univerityModels.Remove(univerityModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UniverityModelExists(int id)
        {
            return (_context.univerityModels?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
