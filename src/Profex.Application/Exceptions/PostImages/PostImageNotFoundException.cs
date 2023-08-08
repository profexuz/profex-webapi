namespace Profex.Application.Exceptions.PostImages;

public class PostImageNotFoundException : NotFoundException
{
    public PostImageNotFoundException()
    {
        this.TitleMessage = "Post image not found";
    }
}
