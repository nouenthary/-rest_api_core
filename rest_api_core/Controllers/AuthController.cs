using Microsoft.AspNetCore.Mvc;
using rest_api_core.Models;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
public class AuthController : Controller
{
    // GET
    [HttpGet("profile")]
    public IActionResult Profile()
    {
        var user1 = new User()
        {
            Id = 1,
            Birthday = new DateTime(),
            Name = "John",  
            Salary = 1000,
            Commission = 1.5f
        };
        
        var user2 = new User()
        {
            Id = 2,
            Birthday = new DateTime(),
            Name = "Mono",  
            Salary = 2000,
            Commission = 3.5f
        };
        
        var user3 = new User()
        {
            Id = 3,
            Birthday = new DateTime(),
            Name = "Thary",  
            Salary = 3000,
            Commission = 6.5f
        };
        
        var users = new List<User>()
        {
            user1, user2, user3
        };

        var total = users.Where(c => c.Id == 1).Sum(x => x.Salary);
        
        var commission =  users.Sum(x => x.Commission);
        
        return Json(new
        {
            data =  "created profile.",
            code = 200,
            message = "success",
            users = users,
            total = total,
            commission = commission
        });
    }
    
    
    [HttpPost("login")]
    public IActionResult Login
        (
            [FromBody] LoginDTO loginDTO
        )
    {
        // return Json(data: loginDTO);
        try
        {
            return Json(new
            {
                data =  "created",
                code = 200,
                message = "success",
                email = loginDTO.email,
                password = loginDTO.password,
            });
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    
    [HttpPost("upload")]
    public async Task<IActionResult> UpdateImage(IFormFile imageFile)
    {
        if (imageFile == null || imageFile.Length == 0)
            return BadRequest("No file uploaded.");

        // Optional: Validate file type
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        var extension = Path.GetExtension(imageFile.FileName).ToLower();
        if (!allowedExtensions.Contains(extension))
            return BadRequest("Unsupported file type.");

        // Unique file name
        var fileName = $"{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

        // Save file
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(stream);
        }

        // Update the DB (e.g., User or Product image path)
        //var user = await _dbContext.Users.FindAsync(id);
        //if (user == null) return NotFound();

        // Optional: Delete old image
       // if (!string.IsNullOrEmpty(user.ImagePath))
        //{
          //  var oldPath = Path.Combine("wwwroot/images", user.ImagePath);
          //  if (System.IO.File.Exists(oldPath))
            //      System.IO.File.Delete(oldPath);
        //}

        //user.ImagePath = fileName;
       // await _dbContext.SaveChangesAsync();

        return Ok(new { message = "Image updated successfully", fileName });
    }

    
}