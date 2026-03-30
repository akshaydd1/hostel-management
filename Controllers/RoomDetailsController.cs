using Microsoft.AspNetCore.Mvc;
using HostelManagementApi.DTOs.Requests;
using HostelManagementApi.DTOs.Responses;
using HostelManagementApi.Services.Interfaces;

namespace HostelManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomDetailController : ControllerBase
    {
        private readonly IRoomDetailService _roomDetailService;

        public RoomDetailController(IRoomDetailService roomDetailService)
        {
            _roomDetailService = roomDetailService;
        }

        // GET /api/RoomDetail/view
        [HttpGet("view")]
        public async Task<IActionResult> GetAllRoomDetails()
        {
            var roomDetails = await _roomDetailService.GetAllRoomDetailsAsync();
            return Ok(ApiResponse<IEnumerable<RoomDetailResponse>>.SuccessResponse(roomDetails));
        }

        // GET /api/RoomDetail/view/{id}
        [HttpGet("view/{id}")]
        public async Task<IActionResult> GetRoomDetailById(int id)
        {
            var roomDetail = await _roomDetailService.GetRoomDetailByIdAsync(id);
            if (roomDetail == null)
                return NotFound(ApiResponse<RoomDetailResponse>.FailResponse($"Room detail with ID {id} not found."));

            return Ok(ApiResponse<RoomDetailResponse>.SuccessResponse(roomDetail));
        }

        // POST /api/RoomDetail/insert
        [HttpPost("insert")]
        public async Task<IActionResult> CreateRoomDetail([FromBody] CreateRoomDetailRequest request)
        {
            var roomDetail = await _roomDetailService.CreateRoomDetailAsync(request);
            return CreatedAtAction(nameof(GetRoomDetailById), new { id = roomDetail.Id },
                ApiResponse<RoomDetailResponse>.SuccessResponse(roomDetail, "Room detail created successfully."));
        }

        // PUT /api/RoomDetail/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateRoomDetail(int id, [FromBody] UpdateRoomDetailRequest request)
        {
            var roomDetail = await _roomDetailService.UpdateRoomDetailAsync(id, request);
            if (roomDetail == null)
                return NotFound(ApiResponse<RoomDetailResponse>.FailResponse($"Room detail with ID {id} not found."));

            return Ok(ApiResponse<RoomDetailResponse>.SuccessResponse(roomDetail, "Room detail updated successfully."));
        }

        // DELETE /api/RoomDetail/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteRoomDetail(int id)
        {
            var deleted = await _roomDetailService.DeleteRoomDetailAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.FailResponse($"Room detail with ID {id} not found."));

            return Ok(ApiResponse<string>.SuccessResponse("Deleted", "Room detail deleted successfully."));
        }
    }
}
