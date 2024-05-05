using AspSpeedTestWithMongoDb.Models;
using AspSpeedTestWithMongoDb.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AspSpeedTestWithMongoDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeedTestController : ControllerBase
    {

        private readonly DataService _dataService;

        public SpeedTestController(DataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet("AddDataToDb")]
        public async Task<IActionResult> AddDataToDb()
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
          var DataObjects=new List<DataDto>();
            for (int i = 0; i < 50000; i++)
            {
                var data = new 
                {
                    FirstObjectLine1 = $"FirstObjectLine1-->{i}",
                    SecondObjectLine1 = $"SecondObjectLine1-->{i}",
                    ListObjectLine1 = new List<object>
                    {
                        new  { FirstObjectLine2=$"FirstObjectLine2-->{i}" },
                        new  { SecondObjectLine2=$"SecondObjectLine2-->{i}" },
                        new  { FirstListObjectLine2=new List<object>
                        {
                         new  { FirstObjectLine3=$"FirstObjectLine3List1-->{i}" },
                         new  { SecondObjectLine3=$"SecondObjectLine3List1-->{i}" },
                        }
                        },
                        new  { SecondListObjectLine2=new List<object>
                        {

                            new  { FirstObjectLine3=$"FirstObjectLine3List2-->{i}" },
                            new  { SecondObjectLine3=$"SecondObjectLine3List2-->{i}" },
                        } }
                    },
                };


                  DataObjects.Add(new DataDto { DataObjects=data});
               
            }

            await _dataService.CreateManyAsync(DataObjects);
            //var stringData = JsonConvert.SerializeObject(DataList);
            //var ttt=JsonConvert.DeserializeObject<DataDto>(stringData);
            //var ddd = ttt.DataObjects;



            stopwatch.Stop();
            return Ok(stopwatch.ElapsedMilliseconds);
        }

        [HttpGet("GetDataFromDb")]
        public async Task<IActionResult> GetDataFromDb()
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            var res= await _dataService.GetAsync();
            var data = res.Select(x => x.DataObjects);
            dynamic firstdata=data.FirstOrDefault();
            var f = firstdata.FirstObjectLine1;
            List<dynamic> Lf = firstdata.ListObjectLine1;
           var Lf3 = Lf[3];
           
          //  var Lf3 = firstdata.ListObjectLine1.FirstListObjectLine2.FirstListObjectLine3;
            stopwatch.Stop();
            return Ok(stopwatch.ElapsedMilliseconds);
        }

    }
}
