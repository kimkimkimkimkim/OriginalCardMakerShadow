﻿public class CommonDialogRequest
{
    /// <summary>
    /// タイトル
    /// </summary>
    public string title { get; set; }

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