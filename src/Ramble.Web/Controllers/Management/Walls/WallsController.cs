using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ramble.Services;
using Ramble.Services.Repository.Wall;
using Ramble.Services.Repository.WallEntry;
using Ramble.Web.Controllers.Management.Walls.Models;

namespace Ramble.Web.Controllers.Management
{
    [Authorize]
    [Route("api/v1/management/walls")]
    public class WallsController : ApiControllerBase
    {
        public WallsController(IMediator mediator, ILogger<ApiControllerBase> logger) : base(mediator, logger)
        {
        }

        [HttpGet("")]
        public ActionResult GetAllWalls()
        {
            return Ok();
        }

        [HttpPost("")]
        public async Task<ActionResult<int>> CreateWall(CreateWall model)
        {
            var result = await Mediator.Send(model);
            if (result.IsError)
                return BadRequest();

            return Ok(result.Value);
        }

        [HttpGet("{wallId}")]
        public ActionResult GetWall(int wallId)
        {
            return Ok();
        }

        [HttpPost("{wallId}")]
        public ActionResult UpdateWall(int wallId, UpdateWallModel model)
        {
            return Ok();
        }

        [HttpDelete("{wallId}")]
        public ActionResult DeleteWall(int wallId)
        {
            return Ok();
        }

        [HttpGet("{wallId}/entries")]
        public ActionResult GetAllWallEntries(int wallId)
        {
            return Ok();
        }

        [HttpPost("{wallId}/entries")]
        public async Task<ActionResult> CreateWallEntry(int wallId, CreateWallEntryModel model)
        {
            var result = await Mediator.Send(new CreateWallEntry
            {
                WallId = wallId,
                EntryType = model.EntryType,
                EntryContent = model.EntryContent,
                EntryTimestamp = model.EntryTimestamp
            });

            if (result.IsError)
                return BadRequest();

            return Ok(result.Value);
        }

        [HttpGet("{wallId}/entries/{entryId}")]
        public ActionResult GetWallEntry(int wallId, int entryId)
        {
            return Ok();
        }

        [HttpPost("{wallId}/entries/{entryId}")]
        public async Task<ActionResult> UpdateWallEntry(int wallId, int entryId, UpdateWallEntryModel model)
        {
            var result = await Mediator.Send(new UpdateWallEntry
            {
                WallEntryId = entryId,
                EntryContent = model.EntryContent,
                EntryTimestamp = model.EntryTimestamp
            });

            if (result.IsError)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{wallId}/entries/{entryId}")]
        public async Task<ActionResult> DeleteWallEntry(int wallId, int entryId)
        {
            var result = await Mediator.Send(new DeleteWallEntry(entryId));
            if (result.IsError && result.ErrorCode != RequestResultErrorCode.NotFound)
                return BadRequest();

            return Ok();
        }
    }
}