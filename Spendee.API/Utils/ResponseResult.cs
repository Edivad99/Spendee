﻿namespace Spendee.API.Utils;

public class ResponseResult<T>
{
    public int StatusCode { get; set; }
    public T Result { get; set; }
}
