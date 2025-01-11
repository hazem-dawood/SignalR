namespace SignalR.Application.Models;

public class RequestPaginationDto
{
    private int _pageNumber;
    private int _length;

    public int Length
    {
        get => _length;
        set
        {
            _length = value;
            if (_length > 20) //max length is 20
                _length = 20;
        }
    }

    public int PageNumber
    {
        get => _pageNumber;
        set
        {
            _pageNumber = value;  //min number is 1
            if (_pageNumber < 1)
                _pageNumber = 1;
        }
    }

}