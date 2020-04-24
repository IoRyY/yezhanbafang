using Microsoft.AspNetCore.Mvc;

namespace yezhanbafang.sd.WebAPI.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BLLController : ControllerBase
    {
        // POST: api/BLL
        [HttpPost]
        public BllClass Post(BllClass bcin)
        {
            //这里处理实际业务,该引用的引用
            return bcin;
        }
    }
}
