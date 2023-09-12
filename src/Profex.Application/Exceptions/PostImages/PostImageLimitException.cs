namespace Profex.Application.Exceptions.PostImages;

public class PostImageLimitException:AlreadyExistsException
{
    public PostImageLimitException()
    {
        TitleMessage = "Post has already has 5 images, Limit 5 images";
    }
}
