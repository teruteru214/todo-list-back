using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tasks.Data;
using Tasks.Models;
using System.Text.Json;

namespace Tasks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("bulk-create")]
        public async Task<IActionResult> CreateTasks([FromBody] List<TaskModel> tasks)
        {
            if (tasks == null || tasks.Count == 0)
            {
                return BadRequest(new { Message = "タスクのリストが空です。" });
            }

            foreach (var task in tasks)
            {
                task.CreateDate = DateTime.Now;
                task.UpdateDate = null;
                _context.Tasks.Add(task);
            }

            await _context.SaveChangesAsync();
            return Ok(new { Message = "複数のタスクが正常に作成されました。" });
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return Ok(tasks);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskModel task)
        {
            var existingTask = await _context.Tasks.FindAsync(id);
            if (existingTask == null)
            {
                return NotFound(new { Message = "タスクが見つかりません。" });
            }

            existingTask.Name = task.Name;
            existingTask.Schedule = task.Schedule;
            existingTask.Complete = task.Complete;
            existingTask.UpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok(new { Message = "タスクが正常に更新されました。" });
        }

        [HttpPatch("{id}/toggle-complete")]
        public async Task<IActionResult> ToggleComplete(int id, [FromBody] JsonElement body)
        {
            if (!body.TryGetProperty("complete", out var completeElement))
            {
                return BadRequest(new { Message = "'complete' プロパティが見つかりません。" });
            }

            if (completeElement.ValueKind != JsonValueKind.True && completeElement.ValueKind != JsonValueKind.False)
            {
                return BadRequest(new { Message = "'complete' の値が true または false ではありません。" });
            }

            bool newComplete = completeElement.GetBoolean();

            var existingTask = await _context.Tasks.FindAsync(id);
            if (existingTask == null)
            {
                return NotFound(new { Message = "タスクが見つかりません。" });
            }

            existingTask.Complete = newComplete;
            existingTask.UpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok(new { Message = "タスクの完了状態が正常に更新されました。" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var existingTask = await _context.Tasks.FindAsync(id);
            if (existingTask == null)
            {
                return NotFound(new { Message = "タスクが見つかりません。" });
            }

            _context.Tasks.Remove(existingTask);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "タスクが正常に削除されました。" });
        }
    }
}
