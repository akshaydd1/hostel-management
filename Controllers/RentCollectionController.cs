using Microsoft.AspNetCore.Mvc;
using HostelManagementApi.DTOs.Requests;
using HostelManagementApi.DTOs.Responses;
using HostelManagementApi.Services.Interfaces;

namespace HostelManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentCollectionController : ControllerBase
    {
        private readonly IRentCollectionService _rentCollectionService;

        public RentCollectionController(IRentCollectionService rentCollectionService)
        {
            _rentCollectionService = rentCollectionService;
        }

        // GET /api/RentCollection/view
        [HttpGet("view")]
        public async Task<IActionResult> GetAllRentCollections()
        {
            var rentCollections = await _rentCollectionService.GetAllRentCollectionsAsync();
            return Ok(ApiResponse<IEnumerable<RentCollectionResponse>>.SuccessResponse(rentCollections));
        }

        // GET /api/RentCollection/view/{id}
        [HttpGet("view/{id}")]
        public async Task<IActionResult> GetRentCollectionById(int id)
        {
            var rentCollection = await _rentCollectionService.GetRentCollectionByIdAsync(id);
            if (rentCollection == null)
                return NotFound(ApiResponse<RentCollectionResponse>.FailResponse($"Rent collection with ID {id} not found."));

            return Ok(ApiResponse<RentCollectionResponse>.SuccessResponse(rentCollection));
        }

        // GET /api/RentCollection/view/student/{studentId}
        [HttpGet("view/student/{studentId}")]
        public async Task<IActionResult> GetRentCollectionsByStudentId(int studentId)
        {
            var rentCollections = await _rentCollectionService.GetRentCollectionsByStudentIdAsync(studentId);
            return Ok(ApiResponse<IEnumerable<RentCollectionResponse>>.SuccessResponse(rentCollections));
        }

        // GET /api/RentCollection/view/room/{roomId}
        [HttpGet("view/room/{roomId}")]
        public async Task<IActionResult> GetRentCollectionsByRoomId(int roomId)
        {
            var rentCollections = await _rentCollectionService.GetRentCollectionsByRoomIdAsync(roomId);
            return Ok(ApiResponse<IEnumerable<RentCollectionResponse>>.SuccessResponse(rentCollections));
        }

        // POST /api/RentCollection/insert
        [HttpPost("insert")]
        public async Task<IActionResult> CreateRentCollection([FromBody] CreateRentCollectionRequest request)
        {
            var rentCollection = await _rentCollectionService.CreateRentCollectionAsync(request);
            return CreatedAtAction(nameof(GetRentCollectionById), new { id = rentCollection.Id },
                ApiResponse<RentCollectionResponse>.SuccessResponse(rentCollection, "Rent collection created successfully."));
        }

        // PUT /api/RentCollection/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateRentCollection(int id, [FromBody] UpdateRentCollectionRequest request)
        {
            var rentCollection = await _rentCollectionService.UpdateRentCollectionAsync(id, request);
            if (rentCollection == null)
                return NotFound(ApiResponse<RentCollectionResponse>.FailResponse($"Rent collection with ID {id} not found."));

            return Ok(ApiResponse<RentCollectionResponse>.SuccessResponse(rentCollection, "Rent collection updated successfully."));
        }

        // DELETE /api/RentCollection/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteRentCollection(int id)
        {
            var deleted = await _rentCollectionService.DeleteRentCollectionAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.FailResponse($"Rent collection with ID {id} not found."));

            return Ok(ApiResponse<string>.SuccessResponse("Deleted", "Rent collection deleted successfully."));
        }
    }
}
