public class CardSelectPhotoDialogRequest
{
}

public class CardSelectPhotoDialogResponse
{
    /// <summary>
    /// 写真取得タイプ
    /// </summary>
    public PhotoType photoType { get; set; }
}

public enum PhotoType
{
    None,
    Camera,
    Library,
}