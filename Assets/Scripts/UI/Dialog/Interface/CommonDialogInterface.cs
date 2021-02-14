public class CommonDialogRequest
{
    /// <summary>
    /// ダイアログタイプ
    /// </summary>
    public CommonDialogType type { get; set; }

    /// <summary>
    /// タイトル
    /// </summary>
    public string title { get; set; } = "タイトル";

    /// <summary>
    /// 本文
    /// </summary>
    public string body { get; set; }
}

public class CommonDialogResponse
{
    /// <summary>
    /// レスポンスタイプ
    /// </summary>
    public CommonDialogResponseType responseType { get; set; }
}

public enum CommonDialogResponseType
{
    Yes,
    No,
}

public enum CommonDialogType
{
    OkAndNo,
    OnlyOk,
}